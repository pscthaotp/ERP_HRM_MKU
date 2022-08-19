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
using System.Text;
using ERP.Module.DanhMuc.TuyenSinh;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_GuiTuVanTuyenSinhController : ViewController
    {
        private TuVanTuyenSinh _tuVanTuyenSinh = null;
        private TuVanTuyenSinh_ThucHien _tuVanTuyenSinh_ThucHien;
        public TuyenSinh_GuiTuVanTuyenSinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            bool erorr = false;
            ChiTietTuVanTuyenSinh chiTietTuVanTuyenSinh = null;
            IObjectSpace obs = Application.CreateObjectSpace();
            string captionErorr = string.Empty;

            //Lấy dữ liệu trên form
            _tuVanTuyenSinh = View.CurrentObject as TuVanTuyenSinh;
            //
            if (_tuVanTuyenSinh == null)
            {
                erorr = true;
            }
            //
            try
            {
                //1. Tìm chi tiết tư vấn
                if (Common.OidCustomList.Count > 0)
                {
                    chiTietTuVanTuyenSinh = obs.GetObjectByKey<ChiTietTuVanTuyenSinh>(Common.OidCustomList[0]);
                }
                //2. Bắt lỗi tạo 2 tư vấn tuyển sinh cùng năm học vs công ty
                if (_tuVanTuyenSinh != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and CongTy=? and Oid != ?", _tuVanTuyenSinh.NamHoc != null ? _tuVanTuyenSinh.NamHoc.Oid : Guid.Empty, _tuVanTuyenSinh.CongTy != null ? _tuVanTuyenSinh.CongTy.Oid : Guid.Empty, _tuVanTuyenSinh.Oid);
                    TuVanTuyenSinh tuVan = obs.FindObject<TuVanTuyenSinh>(filter);
                    if (tuVan != null)
                    {
                        erorr = true;
                        captionErorr = "Tư vấn theo Năm học [" + _tuVanTuyenSinh.NamHoc.TenNamHoc + "] đã tồn tại.";
                    }
                    //
                }
                //

            }
            catch
            {
                erorr = true;
                captionErorr = "Xảy ra lỗi khi lấy dữ liệu.";
            }
            //
            DetailView view = null;
            // 
            if (erorr)
            {
                XuatLoiNghiepVuTuyenSinh thongBaoLoi = obs.CreateObject<XuatLoiNghiepVuTuyenSinh>();
                thongBaoLoi.ThongBao = captionErorr;
                //
                view = Application.CreateDetailView(obs, thongBaoLoi);
            }
            else
            {
                _tuVanTuyenSinh_ThucHien = obs.CreateObject<TuVanTuyenSinh_ThucHien>();
                if (chiTietTuVanTuyenSinh != null)
                {
                    _tuVanTuyenSinh_ThucHien.ThongTinKhachHang = obs.GetObjectByKey<ThongTinKhachHang>(chiTietTuVanTuyenSinh.ThongTinKhachHang.Oid);
                    _tuVanTuyenSinh_ThucHien.NamHoc = obs.GetObjectByKey<NamHoc>(chiTietTuVanTuyenSinh.TuVanTuyenSinh.NamHoc.Oid);
                }
                else
                {
                    _tuVanTuyenSinh_ThucHien.NamHoc = obs.GetObjectByKey<NamHoc>(Common.GetCurrentNamHoc(((XPObjectSpace)obs).Session).Oid);
                }
                view = Application.CreateDetailView(obs, _tuVanTuyenSinh_ThucHien);
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
            StringBuilder mainLog = new StringBuilder();
            //
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            //
            if (_tuVanTuyenSinh_ThucHien == null || _tuVanTuyenSinh == null)
            {
                return;
            }
            else
            {
                //
                ChiTietTuVanTuyenSinh chiTiet = new ChiTietTuVanTuyenSinh(session);
                chiTiet.HinhThucTuVan = _tuVanTuyenSinh_ThucHien.HinhThuc;
                chiTiet.NoiDung = _tuVanTuyenSinh_ThucHien.NoiDung;
                if (_tuVanTuyenSinh_ThucHien.ThongTinKhachHang != null)
                    chiTiet.ThongTinKhachHang = session.GetObjectByKey<ThongTinKhachHang>(_tuVanTuyenSinh_ThucHien.ThongTinKhachHang.Oid);
                else
                {
                    chiTiet.Delete();
                    return;
                }
                chiTiet.TuVanTuyenSinh = _tuVanTuyenSinh;
                chiTiet.SecuritySystemUser = session.GetObjectByKey<ERP.Module.HeThong.SecuritySystemUser_Custom>(Common.SecuritySystemUser_GetCurrentUser().Oid);
                //
                if (chiTiet.ThongTinKhachHang != null)
                {
                    #region Email
                    if (chiTiet.HinhThucTuVan == Enum.TuyenSinh.HinhThucTuVanEnum.Email)
                    {
                        if (!string.IsNullOrEmpty(chiTiet.NoiDung) && !string.IsNullOrEmpty(chiTiet.ThongTinKhachHang.Email))
                        {
                            //
                            string emailgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Email;
                            string passgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Password;
                            string noidung = _tuVanTuyenSinh_ThucHien.NoiDung;
                            string tieude = _tuVanTuyenSinh_ThucHien.TieuDe;
                            string emailnhan = chiTiet.ThongTinKhachHang.Email;

                            //Lưu tập tin đính kèm lại để gửi
                            string fullPath = string.Empty;
                            if (_tuVanTuyenSinh_ThucHien.TapTinDinhKem != null)
                            {
                                fullPath = HttpContext.Current.Server.MapPath("~/Downloads/" + _tuVanTuyenSinh_ThucHien.TapTinDinhKem.FileName);
                                //
                                File.WriteAllBytes(fullPath, _tuVanTuyenSinh_ThucHien.TapTinDinhKem.Content);
                            }

                            //Gửi mail Tổ chức sự kiện
                            sucess = MailHelpers.CreateMailManager(session, tieude, fullPath, emailgui, passgui, emailnhan, noidung, TrangThaiGuiMailEnum.DaGuiMailTuVanTuyenSinh, Guid.NewGuid());
                            //
                            if (!chiTiet.ThongTinKhachHang.DaTuVanTuyenSinh && sucess)
                                chiTiet.ThongTinKhachHang.DaTuVanTuyenSinh = true;
                        }
                        //
                        if (sucess)
                        {
                            chiTiet.HinhThucTuVan = Enum.TuyenSinh.HinhThucTuVanEnum.Email;
                            chiTiet.NgayTuVan = Common.GetServerCurrentTime();
                            chiTiet.LoaiKhachHang = session.GetObjectByKey<LoaiKhachHang>(_tuVanTuyenSinh_ThucHien.LoaiKhachHang.Oid);
                            count += 1;
                        }
                        else
                        {
                            error += 1;
                            mainLog.AppendLine("Khách hàng [" + chiTiet.ThongTinKhachHang.HoTen + "] không gửi được email. ");
                        }
                    }
                    #endregion

                    #region SMS
                    else if (chiTiet.HinhThucTuVan == Enum.TuyenSinh.HinhThucTuVanEnum.SMS)
                    {
                        if (!string.IsNullOrEmpty(chiTiet.NoiDung) && !string.IsNullOrEmpty(chiTiet.ThongTinKhachHang.DienThoaiDiDong))
                        {
                            //Nội dung
                            string noidung = string.Format("{0}", _tuVanTuyenSinh_ThucHien.NoiDung.Trim());
                            //Điện thoại
                            string sodienthoai = chiTiet.ThongTinKhachHang.DienThoaiDiDong.Trim();
                            //
                            sucess = SmsOTP.SendSMS(sodienthoai, noidung);
                            //
                            if (!chiTiet.ThongTinKhachHang.DaTuVanTuyenSinh && sucess)
                                chiTiet.ThongTinKhachHang.DaTuVanTuyenSinh = true;
                        }
                        //
                        if (sucess)
                        {
                            chiTiet.HinhThucTuVan = Enum.TuyenSinh.HinhThucTuVanEnum.SMS;
                            chiTiet.NgayTuVan = Common.GetServerCurrentTime();
                            chiTiet.LoaiKhachHang = session.GetObjectByKey<LoaiKhachHang>(_tuVanTuyenSinh_ThucHien.LoaiKhachHang.Oid);
                            count += 1;
                        }
                        else
                        {
                            error += 1;
                            mainLog.AppendLine("Khách hàng [" + chiTiet.ThongTinKhachHang.HoTen + "] không gửi được email");
                        }
                    }
                    #endregion

                    #region 3. Hình thức khác
                    else if (chiTiet.HinhThucTuVan == Enum.TuyenSinh.HinhThucTuVanEnum.TrucTiep)
                    {
                        chiTiet.HinhThucTuVan = Enum.TuyenSinh.HinhThucTuVanEnum.TrucTiep;
                        chiTiet.NgayTuVan = Common.GetServerCurrentTime();
                        chiTiet.LoaiKhachHang = session.GetObjectByKey<LoaiKhachHang>(_tuVanTuyenSinh_ThucHien.LoaiKhachHang.Oid);
                        count += 1;
                    }
                    #endregion
                }
            }
            //
            if (count > 0)
            {
                //
                session.CommitTransaction();
                //
                string message = "alert('Gửi thành công: " + count + " khách hàng. Thất bại: " + error + ".')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                // Có lỗi thì mới xuất file
                if (error > 0)
                {
                    // Lấy đường dẫn file xem lỗi khi gửi mail
                    string path = HttpContext.Current.Server.MapPath(Config.URLErorrImportExcel);
                    //Tạo file
                    Common.WriteDataToFile(path, mainLog.ToString());
                }
                View.ObjectSpace.CommitChanges();
                View.Refresh();
            }
            else
            {
                string message = "alert('Gửi thất bại.')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                // Có lỗi thì mới xuất file
                if (error > 0)
                {
                    // Lấy đường dẫn file xem lỗi khi gửi mail
                    string path = HttpContext.Current.Server.MapPath(Config.URLErorrImportExcel);
                    //Tạo file
                    Common.WriteDataToFile(path, mainLog.ToString());
                }
                View.ObjectSpace.Refresh();
                View.Refresh();
            }
        }
        private void TuyenSinh_GuiTuVanTuyenSinhController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Commons.Common.IsWriteGranted<TuVanTuyenSinh>();

        }
    }
}
