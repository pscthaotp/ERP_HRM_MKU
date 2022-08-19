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
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_GuiThuMoiToChucSuKienController : ViewController
    {
        private ToChucSuKien _toChucSuKien;
        private ToChucSuKien_ThucHien _toChucSuKien_ThucHien;
        private IObjectSpace _obs;
        public TuyenSinh_GuiThuMoiToChucSuKienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            ChiTietToChucSuKien chiTietToChucSuKien = null;
            bool erorr = false;
            _obs = Application.CreateObjectSpace();
            string captionError = string.Empty;
            //

            if (View.SelectedObjects.Count > 0)
                chiTietToChucSuKien = View.SelectedObjects[0] as ChiTietToChucSuKien;
            //
            if (chiTietToChucSuKien == null)
            {
                erorr = true;
                captionError = "Chưa chọn khách hàng.";
            }
            else
            {
                //1. Kiểm tra dữ liệu trên form
                if (chiTietToChucSuKien.ToChucSuKien != null)
                {
                    if (string.IsNullOrEmpty(chiTietToChucSuKien.ToChucSuKien.TenSuKien)
                        || chiTietToChucSuKien.ToChucSuKien.NamHoc == null)
                    {
                        erorr = true;
                        captionError = "Nhập các dữ liệu bắt buộc.";
                    }

                    if (!chiTietToChucSuKien.ToChucSuKien.DaDuyet)
                    {
                        erorr = true;
                        captionError = "Sự kiện chưa được duyệt.";
                    }
                }
             
                //Tìm nếu chưa lưu thì lưu
                ChiTietToChucSuKien suKien = _obs.GetObjectByKey<ChiTietToChucSuKien>(chiTietToChucSuKien.Oid);
                if (suKien == null)
                {
                    try
                    {
                        View.ObjectSpace.CommitChanges();
                    }
                    catch (Exception ex) {
                        erorr = true;
                        captionError = "Lỗi khi lưu dữ liệu.";
                    }
                }
            }
            //
            DetailView view = null;
            if (erorr)
            {
                XuatLoiNghiepVuTuyenSinh thongBaoLoi = _obs.CreateObject<XuatLoiNghiepVuTuyenSinh>();
                thongBaoLoi.ThongBao = captionError;
                //
                view = Application.CreateDetailView(_obs, thongBaoLoi);
            }
            else
            {
                //
                _toChucSuKien_ThucHien = _obs.CreateObject<ToChucSuKien_ThucHien>();
                view = Application.CreateDetailView(_obs, _toChucSuKien_ThucHien);
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
            string errorEmail = "";
            string errorDienThoai = "";
            //
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            //
            if (_toChucSuKien_ThucHien == null)
            {
                return;
            }
            else
            {
                List<Guid> oidList = Common.OidCustomList;
                if (oidList.Count > 0)
                {
                    //
                    foreach (var item in oidList)
                    {
                        //
                        ChiTietToChucSuKien toChucSuKien = session.GetObjectByKey<ChiTietToChucSuKien>(item);
                        //
                        if (toChucSuKien != null && toChucSuKien.ThongTinKhachHang != null)
                        {
                            #region 1. Email
                            if (_toChucSuKien_ThucHien.HinhThucGui == Enum.TuyenSinh.HinhThucGuiSuKienEnum.Email)
                            {
                                if (!string.IsNullOrEmpty(_toChucSuKien_ThucHien.NoiDung) && !string.IsNullOrEmpty(toChucSuKien.ThongTinKhachHang.Email))
                                {
                                    //
                                    string emailgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Email;
                                    string passgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Password;
                                    string noidung = _toChucSuKien_ThucHien.NoiDung;
                                    string tieude = _toChucSuKien_ThucHien.TieuDe;
                                    string emailnhan = toChucSuKien.ThongTinKhachHang.Email;

                                    //Lưu tập tin đính kèm lại để gửi
                                    string fullPath = string.Empty;
                                    if (_toChucSuKien_ThucHien.TapTinDinhKem != null)
                                    {
                                        fullPath = HttpContext.Current.Server.MapPath("~/Downloads/" + Guid.NewGuid().ToString() + ".xsl");
                                        //
                                        File.WriteAllBytes(fullPath, _toChucSuKien_ThucHien.TapTinDinhKem.Content);
                                    }

                                    //Gửi mail Tổ chức sự kiện
                                    sucess = MailHelpers.CreateMailManager(session, tieude, fullPath, emailgui, passgui, emailnhan, noidung, TrangThaiGuiMailEnum.DaGuiMailToChucSuKien, Guid.NewGuid());

                                }
                                //
                                if (sucess)
                                {
                                    toChucSuKien.HinhThuc = Enum.TuyenSinh.HinhThucGuiSuKienEnum.Email;
                                    toChucSuKien.NgayGui = Common.GetServerCurrentTime();
                                    toChucSuKien.DaGuiLoiMoi = true;
                                    count += 1;
                                }
                                else
                                {
                                    error += 1;
                                    errorEmail = " Thông tin khách hàng không có dữ liệu về email.";
                                }
                            }
                            #endregion

                            #region 2. SMS
                            if (_toChucSuKien_ThucHien.HinhThucGui == Enum.TuyenSinh.HinhThucGuiSuKienEnum.SMS)
                            {
                                if (!string.IsNullOrEmpty(_toChucSuKien_ThucHien.NoiDung) && !string.IsNullOrEmpty(toChucSuKien.ThongTinKhachHang.DienThoaiDiDong))
                                {
                                    //Nội dung
                                    string noidung = string.Format("{0}", _toChucSuKien_ThucHien.NoiDung.Trim());
                                    //Điện thoại
                                    string sodienthoai = toChucSuKien.ThongTinKhachHang.DienThoaiDiDong.Trim();
                                    //
                                    sucess = SmsOTP.SendSMS(sodienthoai, noidung);
                                }
                                //
                                if (sucess)
                                {
                                    toChucSuKien.HinhThuc = Enum.TuyenSinh.HinhThucGuiSuKienEnum.SMS;
                                    toChucSuKien.NgayGui = Common.GetServerCurrentTime();
                                    toChucSuKien.DaGuiLoiMoi = true;
                                    count += 1;
                                }
                                else
                                {
                                    error += 1;
                                    errorDienThoai = " Thông tin khách hàng không có dữ liệu về điện thoại.";
                                }
                            }
                            #endregion

                            #region 3. Điện thoại
                            if (_toChucSuKien_ThucHien.HinhThucGui == Enum.TuyenSinh.HinhThucGuiSuKienEnum.DienThoai)
                            {
                                if (!string.IsNullOrEmpty(_toChucSuKien_ThucHien.NoiDung) && !string.IsNullOrEmpty(toChucSuKien.ThongTinKhachHang.DienThoaiDiDong))
                                {
                                    //Nội dung
                                    string noidung = string.Format("{0}", _toChucSuKien_ThucHien.NoiDung.Trim());
                                    //Điện thoại
                                    string sodienthoai = toChucSuKien.ThongTinKhachHang.DienThoaiDiDong.Trim();
                                    //
                                    sucess = true;
                                }
                                //
                                if (sucess)
                                {
                                    toChucSuKien.HinhThuc = Enum.TuyenSinh.HinhThucGuiSuKienEnum.DienThoai;
                                    toChucSuKien.NgayGui = Common.GetServerCurrentTime();
                                    toChucSuKien.DaGuiLoiMoi = true;
                                    count += 1;
                                }
                                else
                                {
                                    error += 1;
                                    errorDienThoai = " Thông tin khách hàng không có dữ liệu về điện thoại.";
                                }
                            }
                            #endregion
                        }
                    }
                }
            }
            //
            if (count > 0)
            {
                //
                View.ObjectSpace.CommitChanges();
                //
                string message = "alert('Gửi thành công: " + count + " khách hàng. Thất bại: " + error + "." + errorEmail + " " + errorDienThoai + "')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                if (count > 0)
                {
                    View.ObjectSpace.Refresh();
                }
            }
            else
            {
                //
                string message = "alert('Gửi thất bại." + errorEmail + "" + errorDienThoai + "')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                //
                View.ObjectSpace.Refresh();
            }
        }
        private void TuyenSinh_GuiThuMoiToChucSuKienController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Commons.Common.IsWriteGranted<ChiTietToChucSuKien>();

        }
    }
}
