using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocSinh;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Extends;
using System.Web;
using System.IO;
using ERP.Module.Enum.Systems;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_GuiThongBaoNhapHocController : ViewController<DetailView>
    {
        private ThongBaoNhapHoc _thongBaoNhapHoc;
        private ThongBaoNhapHoc_ThucHien _thongBaoNhapHoc_ThucHien;
        public TuyenSinh_GuiThongBaoNhapHocController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            ChiTietThongBaoNhapHoc chiTietThongBaoNhapHoc = null;
            IObjectSpace obs = Application.CreateObjectSpace();
            bool erorr = false;
            string captionErorr = string.Empty;

            //Lấy dữ liệu trên form
            _thongBaoNhapHoc = View.CurrentObject as ThongBaoNhapHoc;
            //
            if (_thongBaoNhapHoc == null)
            {
                erorr = true;
            }

            //
            try
            {
                //1. Tìm chi tiết tư vấn
                if (Common.OidCustomList.Count > 0)
                {
                    chiTietThongBaoNhapHoc = obs.GetObjectByKey<ChiTietThongBaoNhapHoc>(Common.OidCustomList[0]);
                }
                //2. Bắt lỗi tạo 2 tư vấn thông báo cùng năm học vs công ty
                if (_thongBaoNhapHoc != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and CongTy=? and Oid != ?", _thongBaoNhapHoc.NamHoc != null ? _thongBaoNhapHoc.NamHoc.Oid : Guid.Empty, _thongBaoNhapHoc.CongTy != null ? _thongBaoNhapHoc.CongTy.Oid : Guid.Empty, _thongBaoNhapHoc.Oid);
                    ThongBaoNhapHoc thongBao = obs.FindObject<ThongBaoNhapHoc>(filter);
                    if (thongBao != null)
                    {
                        erorr = true;
                        captionErorr = "Thông báo theo Năm học [" + _thongBaoNhapHoc.NamHoc.TenNamHoc + "] đã tồn tại.";
                    }
                    //
                }
            }
            catch
            {
                erorr = true;
                captionErorr = "Xảy ra lỗi khi lấy dữ liệu.";
            }
            //
            DetailView view = null;
            if (erorr)
            {
                XuatLoiNghiepVuTuyenSinh thongBaoLoi = obs.CreateObject<XuatLoiNghiepVuTuyenSinh>();
                thongBaoLoi.ThongBao = captionErorr;
                //
                view = Application.CreateDetailView(obs, thongBaoLoi);
            }
            else
            {
                //
                if (chiTietThongBaoNhapHoc != null)
                {
                    _thongBaoNhapHoc_ThucHien = obs.CreateObject<ThongBaoNhapHoc_ThucHien>();
                    _thongBaoNhapHoc_ThucHien.NamHoc = obs.GetObjectByKey<NamHoc>(chiTietThongBaoNhapHoc.ThongBaoNhapHoc.NamHoc.Oid);
                    _thongBaoNhapHoc_ThucHien.HocSinh = obs.GetObjectByKey<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(chiTietThongBaoNhapHoc.HocSinh.Oid);
                }
                else
                {
                    _thongBaoNhapHoc_ThucHien = obs.CreateObject<ThongBaoNhapHoc_ThucHien>();
                }
                view = Application.CreateDetailView(obs, _thongBaoNhapHoc_ThucHien);
            }
            //
            view.ViewEditMode = ViewEditMode.Edit;
            e.View = view;
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            bool sucess = false;
            int count = 0;
            int error = 0;
            //
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            //
            if (_thongBaoNhapHoc_ThucHien == null || _thongBaoNhapHoc == null)
            {
                return;
            }
            else
            {
                //
                ChiTietThongBaoNhapHoc chiTiet = new ChiTietThongBaoNhapHoc(session);
                chiTiet.ThongBaoNhapHoc = _thongBaoNhapHoc;
                chiTiet.NoiDung = _thongBaoNhapHoc_ThucHien.NoiDung;
                chiTiet.HocSinh = session.GetObjectByKey<Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(_thongBaoNhapHoc_ThucHien.HocSinh.Oid);
                chiTiet.HinhThucThongBao = _thongBaoNhapHoc_ThucHien.HinhThuc;
                chiTiet.SecuritySystemUser = session.GetObjectByKey<Module.HeThong.SecuritySystemUser_Custom>(Common.SecuritySystemUser_GetCurrentUser().Oid);
                //
                #region 1. Email
                if (chiTiet.HinhThucThongBao == Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.Email)
                {
                    if (!string.IsNullOrEmpty(_thongBaoNhapHoc_ThucHien.NoiDung) && (!string.IsNullOrEmpty(chiTiet.HocSinh.EmailCha) || !string.IsNullOrEmpty(chiTiet.HocSinh.EmailMe)))
                    {
                        //
                        string emailgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Email;
                        string passgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Password;
                        string noidung = _thongBaoNhapHoc_ThucHien.NoiDung;
                        string tieude = _thongBaoNhapHoc_ThucHien.TieuDe;
                        string emailnhan = chiTiet.HocSinh.EmailCha.Trim();
                        if (string.IsNullOrEmpty(emailnhan))
                            emailnhan = chiTiet.HocSinh.EmailMe.Trim();

                        //Lưu tập tin đính kèm lại để gửi
                        string fullPath = string.Empty;
                        if (_thongBaoNhapHoc_ThucHien.TapTinDinhKem != null)
                        {
                            fullPath = HttpContext.Current.Server.MapPath("~/Downloads/" + Guid.NewGuid().ToString() + ".xsl");
                            //
                            File.WriteAllBytes(fullPath, _thongBaoNhapHoc_ThucHien.TapTinDinhKem.Content);
                        }

                        //Gửi mail Thông báo nhập học
                        sucess = MailHelpers.CreateMailManager(session, tieude, fullPath, emailgui, passgui, emailnhan, noidung, TrangThaiGuiMailEnum.DaGuiMailThongBaoNhapHoc, Guid.NewGuid());
                    }
                    //
                    if (sucess)
                    {
                        chiTiet.HinhThucThongBao = Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.Email;
                        chiTiet.NgayThongBao = Common.GetServerCurrentTime();

                        count += 1;
                    }
                    else
                    {
                        error += 1;
                    }
                }
                #endregion

                #region 2. SMS
                else if (chiTiet.HinhThucThongBao == Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.SMS)
                {
                    if (!string.IsNullOrEmpty(_thongBaoNhapHoc_ThucHien.NoiDung) && (!string.IsNullOrEmpty(chiTiet.HocSinh.DienThoaiCha) || !string.IsNullOrEmpty(chiTiet.HocSinh.DienThoaiMe)))
                    {
                        //Nội dung
                        string noidung = string.Format("{0}", _thongBaoNhapHoc_ThucHien.NoiDung.Trim());
                        //Điện thoại
                        string sodienthoai = chiTiet.HocSinh.DienThoaiCha.Trim();
                        if (string.IsNullOrEmpty(sodienthoai))
                            sodienthoai = chiTiet.HocSinh.DienThoaiMe.Trim();
                        //
                        sucess = SmsOTP.SendSMS(sodienthoai, noidung);
                    }
                    //
                    if (sucess)
                    {
                        chiTiet.HinhThucThongBao = Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.SMS;
                        chiTiet.NgayThongBao = Common.GetServerCurrentTime();

                        count += 1;
                    }
                    else
                    {
                        error += 1;
                    }
                }
                #endregion 

                #region 3. Hình thức khác
                else
                {
                    count += 1;
                }
                #endregion
            }
            //
            if (count > 0)
            {
                //
                session.CommitTransaction();
                //
                string message = "alert('Gửi thành công: " + count + " học sinh. Thất bại: " + error + ".')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                //
                View.ObjectSpace.CommitChanges();
                View.Refresh();
            }
            else
            {
                //
                string message = "alert('Gửi thất bại.')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                //
                View.ObjectSpace.Refresh();
                View.Refresh();
            }
        }
        private void TuyenSinh_GuiThongBaoNhapHocController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Commons.Common.IsWriteGranted<ThongBaoNhapHoc>();

        }
    }
}
