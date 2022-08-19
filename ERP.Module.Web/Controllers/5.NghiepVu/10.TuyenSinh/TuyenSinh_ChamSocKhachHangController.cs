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
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Extends;
using System.Web;
using System.IO;
using ERP.Module.Enum.Systems;
using System.Text;
using ERP.Module.NghiepVu.TuyenSinh_TP;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChamSocKhachHangController : ViewController
    {
        private ChamSocKhachHang _chamSocKhachHang;
        ChamSocKhachHang_ThucHien _chamSocKhachHang_ThucHien;
        IObjectSpace _obs;
        public TuyenSinh_ChamSocKhachHangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View.SelectedObjects.Count > 0)
                _chamSocKhachHang = View.SelectedObjects[0] as ChamSocKhachHang;
            //
            _obs = Application.CreateObjectSpace(); //
            _chamSocKhachHang_ThucHien = _obs.CreateObject<ChamSocKhachHang_ThucHien>();
            //
            if (_chamSocKhachHang != null)
            {
                //
                _chamSocKhachHang_ThucHien.ThongTinKhachHang = _obs.GetObjectByKey<ThongTinKhachHang>(_chamSocKhachHang.ThongTinKhachHang.Oid);
            }
            DetailView view = Application.CreateDetailView(_obs, _chamSocKhachHang_ThucHien);
            view.ViewEditMode = ViewEditMode.Edit;
            e.View = view;
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            bool sucess = false;
            string error = "";
            StringBuilder mainLog = new StringBuilder();
            //
            if (_chamSocKhachHang_ThucHien != null && _chamSocKhachHang_ThucHien.ThongTinKhachHang != null)
            {
                //
                ChamSocKhachHang chamSocKhachHang = new ChamSocKhachHang(((XPObjectSpace)_obs).Session);
                chamSocKhachHang.ThongTinKhachHang = _obs.GetObjectByKey<ThongTinKhachHang>(_chamSocKhachHang_ThucHien.ThongTinKhachHang.Oid);
                if (_chamSocKhachHang_ThucHien.HoSoXetTuyen != null)
                    chamSocKhachHang.HoSoXetTuyen = _obs.GetObjectByKey<HoSoXetTuyen>(_chamSocKhachHang_ThucHien.HoSoXetTuyen.Oid);
                if (_chamSocKhachHang_ThucHien.NoiDung != null)
                    chamSocKhachHang.NoiDung = _chamSocKhachHang_ThucHien.NoiDung;
                else
                {
                    chamSocKhachHang.Delete();
                    return;
                }
                chamSocKhachHang.GhiChu = _chamSocKhachHang_ThucHien.GhiChu;
                chamSocKhachHang.HinhThucChamSoc = _chamSocKhachHang_ThucHien.HinhThucChamSoc;
                if (_chamSocKhachHang_ThucHien.HinhThucChamSoc == Enum.TuyenSinh.HinhThucChamSocEnum.Email) // Gửi mail
                {
                    if (_chamSocKhachHang_ThucHien.ThongTinKhachHang.Email != null)
                    {
                        //
                        string emailgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Email;
                        string passgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Password;
                        string noidung = _chamSocKhachHang_ThucHien.NoiDung.TenNoiDung;
                        string tieude = _chamSocKhachHang_ThucHien.TieuDe;
                        string emailnhan = _chamSocKhachHang_ThucHien.Email;

                        //Lưu tập tin đính kèm lại để gửi
                        string fullPath = string.Empty;
                        if (_chamSocKhachHang_ThucHien.TapTinDinhKem != null)
                        {
                            fullPath = HttpContext.Current.Server.MapPath("~/Downloads/" + _chamSocKhachHang_ThucHien.TapTinDinhKem.FileName);
                            //
                            File.WriteAllBytes(fullPath, _chamSocKhachHang_ThucHien.TapTinDinhKem.Content);
                        }
                        //Gửi mail chăm sóc khách hàng
                        sucess = MailHelpers.CreateMailManager(((XPObjectSpace)_obs).Session, tieude, fullPath, emailgui, passgui, emailnhan, noidung, TrangThaiGuiMailEnum.DaGuiMailChamSocKhachHang, Guid.NewGuid());
                        //
                        if (!_chamSocKhachHang_ThucHien.ThongTinKhachHang.DaChamSocKhachHang && sucess)
                            _chamSocKhachHang_ThucHien.ThongTinKhachHang.DaChamSocKhachHang = true;

                        //Nếu SMS lỗi thì xóa thông tin chăm sóc đã tạo(Kh xóa lỗi view.)
                        if (sucess == false)
                        {
                            chamSocKhachHang.Delete();
                        }
                    }
                    else
                    {
                        mainLog.AppendLine("Khách hàng [" + _chamSocKhachHang_ThucHien.ThongTinKhachHang.HoTen + "] không có địa chỉ Email.");
                    }
                }
                else if (_chamSocKhachHang_ThucHien.HinhThucChamSoc == Enum.TuyenSinh.HinhThucChamSocEnum.SMS) // Gửi SMS
                {
                    if (_chamSocKhachHang_ThucHien.ThongTinKhachHang.DienThoaiDiDong != null)
                    {
                        //Nội dung
                        string noidung = string.Format("{0}", _chamSocKhachHang_ThucHien.NoiDung.TenNoiDung.Trim());
                        // 
                        string sodienthoai = _chamSocKhachHang_ThucHien.DienThoai.Trim();
                        //SMS
                        sucess = SmsOTP.SendSMS(sodienthoai, noidung);
                        //
                        if (!_chamSocKhachHang_ThucHien.ThongTinKhachHang.DaChamSocKhachHang && sucess)
                            _chamSocKhachHang_ThucHien.ThongTinKhachHang.DaChamSocKhachHang = true;
                        //Nếu SMS lỗi thì xóa thông tin chăm sóc đã tạo(Kh xóa lỗi view.)
                        if (sucess == false)
                        {
                            chamSocKhachHang.Delete();
                        }
                    }
                    else
                    {
                        mainLog.AppendLine("Khách hàng [" + _chamSocKhachHang_ThucHien.ThongTinKhachHang.HoTen + "] không có số điện thoại.");
                    }
                }
                else if (_chamSocKhachHang_ThucHien.HinhThucChamSoc == Enum.TuyenSinh.HinhThucChamSocEnum.DienThoai) // Gọi điện thoại
                {
                    if (_chamSocKhachHang_ThucHien.ThongTinKhachHang.DienThoaiDiDong != null)
                    {
                        //Nội dung
                        string noidung = string.Format("{0}", _chamSocKhachHang_ThucHien.NoiDung.TenNoiDung.Trim());
                        //
                        string sodienthoai = _chamSocKhachHang_ThucHien.DienThoai.Trim();
                        //Điện thoại
                        sucess = true;
                        //
                        if (!_chamSocKhachHang_ThucHien.ThongTinKhachHang.DaChamSocKhachHang && sucess)
                            _chamSocKhachHang_ThucHien.ThongTinKhachHang.DaChamSocKhachHang = true;
                    }
                    else
                    {
                        mainLog.AppendLine("Khách hàng [" + _chamSocKhachHang_ThucHien.ThongTinKhachHang.HoTen + "] không có địa chỉ Email.");
                    }
                }
                else if (_chamSocKhachHang_ThucHien.HinhThucChamSoc == Enum.TuyenSinh.HinhThucChamSocEnum.TatCa)
                {
                    sucess = false;
                    error = " Trạng thái tất cả không hợp lệ.";
                    chamSocKhachHang.Delete();
                }
            }
            //
            if (sucess)
            {
                //
                View.ObjectSpace.CommitChanges();
                _obs.CommitChanges();
                //
                string message = "alert('Xử lý thành công.')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                //Sau khi thêm mới refresh lại view.
                View.RefreshDataSource();

            }
            else
            {
                //
                string message = "alert('Thất bại. " + error + "')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                // Lấy đường dẫn file xem lỗi khi gửi mail
                string path = HttpContext.Current.Server.MapPath(Config.URLErorrImportExcel);
                //Tạo file
                Common.WriteDataToFile(path, mainLog.ToString());

            }
        }
        private void TuyenSinh_ChamSocKhachHangController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Commons.Common.IsWriteGranted<ChamSocKhachHang>();

        }
    }
}
