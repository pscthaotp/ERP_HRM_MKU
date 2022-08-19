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
using ERP.Module.HeThong;
using DevExpress.Xpo;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChamSocKhachHang_TongHopController : ViewController
    {
        //private ChamSocKhachHang _chamSocKhachHang;
        //ChamSocKhachHang_TongHop _chamSocKhachHang_ThucHien;
        public TuyenSinh_ChamSocKhachHang_TongHopController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            List<Guid[]> khachHangList = Common.OidCustomListArray;
            bool sucess = false;
            int sucessNumber = 0;
            int erorrNumber = 0;
            StringBuilder mainLog = new StringBuilder();
            StringBuilder detailLog = new StringBuilder();
            ChamSocKhachHang_TongHop obj = View.CurrentObject as ChamSocKhachHang_TongHop;
            SecuritySystemUser_Custom currentUser = Common.SecuritySystemUser_GetCurrentUser();

            if (currentUser == null) return;
            //
            if (obj == null)
            {
                return;
            }
            else
            {
                //if (obj.HinhThuc != Enum.TuyenSinh.HinhThucChamSocEnum.Email)
                //{
                //    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn hình thức Email!!!')");
                //    return;
                //}
                if (string.IsNullOrEmpty(obj.TieuDe))
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Nhập tiêu đề!!!')");
                    return;
                }
                if (obj.NoiDung == null)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Nhập nội dung!!!')");
                    return;
                }
                if (khachHangList.Count == 0)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn khách hàng!!!')");
                    return;
                }
            }
            try
            {
                if (string.IsNullOrEmpty(obj.NoiDungGuiMail))
                {
                    string noidung = "alert('Vui lòng nhập nội dung gửi đến khách hàng.')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", noidung);
                    return;
                }
                if (khachHangList.Count > 0)
                {
                    //
                    //DateTime dauNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                    //DateTime cuoiNgay = dauNgay.AddDays(1).AddMinutes(-1);
                    Session session = ((XPObjectSpace)View.ObjectSpace).Session;

                    //Chăm sóc khách hàng

                    foreach (var item in khachHangList)
                    {
                        if (obj.HinhThuc == Enum.TuyenSinh.HinhThucChamSocEnum.Email)
                        {
                            ThongTinKhachHang khachHang = session.GetObjectByKey<ThongTinKhachHang>(item[0]);

                            if (khachHang != null && !string.IsNullOrEmpty(khachHang.Email))
                            {
                                //
                                bool daGuiMail = false;
                                DateTime ngayTuVan = DateTime.Now;
                                //
                                string emailgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Email;
                                string passgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Password;
                                string noidung = obj.NoiDungGuiMail;
                                string tieude = obj.TieuDe;

                                //Lưu tập tin đính kèm lại để gửi
                                string fullPath = string.Empty;
                                if (obj.TapTinDinhKem != null)
                                {
                                    fullPath = HttpContext.Current.Server.MapPath("~/Downloads/" + Guid.NewGuid().ToString() + ".xsl");
                                    //
                                    File.WriteAllBytes(fullPath, obj.TapTinDinhKem.Content);
                                }
                                //WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('"+emailgui+" - "+ passgui+" - "+khachHang.Email+"')");
                                //Gửi mail thông báo
                                daGuiMail = MailHelpers.CreateMailManager(session, tieude, fullPath, emailgui, passgui, khachHang.Email, obj.NoiDungGuiMail, TrangThaiGuiMailEnum.DaGuiMailChamSocKhachHang, khachHang.Oid);

                                //
                                if (daGuiMail)
                                {
                                    ChamSocKhachHang chamsoc = new ChamSocKhachHang(session);
                                    chamsoc.NoiDung = obj.NoiDung;
                                    chamsoc.HinhThucChamSoc = obj.HinhThuc;
                                    chamsoc.GhiChu = obj.GhiChu;
                                    chamsoc.ThongTinKhachHang = khachHang;
                                    chamsoc.NgayThucHien = DateTime.Now;
                                    HoSoXetTuyen xetTuyen = session.GetObjectByKey<HoSoXetTuyen>(item[1]);
                                    if (xetTuyen != null)
                                        chamsoc.HoSoXetTuyen = xetTuyen;
                                    else
                                        chamsoc.HoSoXetTuyen = null;

                                    //
                                    //View.ObjectSpace.CommitChanges();
                                    //
                                    sucessNumber++;

                                }
                                else
                                {
                                    erorrNumber++;
                                    detailLog.AppendLine("Khách hàng [" + khachHang.HoTen + "] không gửi được email");
                                }
                            }
                            else
                            {
                                erorrNumber++;
                                detailLog.AppendLine("Khách hàng [" + khachHang.HoTen + "] không có địa chỉ email");
                            }
                        }
                        else if (obj.HinhThuc == Enum.TuyenSinh.HinhThucChamSocEnum.DienThoai)
                        {
                            ThongTinKhachHang khachHang = session.GetObjectByKey<ThongTinKhachHang>(item[0]);

                            if (khachHang != null && !string.IsNullOrEmpty(khachHang.DienThoaiDiDong))
                            {
                                //
                                bool daGuiMail = true;
                                if (daGuiMail)
                                {
                                    ChamSocKhachHang chamsoc = new ChamSocKhachHang(session);
                                    chamsoc.NoiDung = obj.NoiDung;
                                    chamsoc.HinhThucChamSoc = obj.HinhThuc;
                                    chamsoc.GhiChu = obj.GhiChu;
                                    chamsoc.ThongTinKhachHang = khachHang;
                                    chamsoc.NgayThucHien = DateTime.Now;
                                    HoSoXetTuyen xetTuyen = session.GetObjectByKey<HoSoXetTuyen>(item[1]);
                                    if (xetTuyen != null)
                                        chamsoc.HoSoXetTuyen = xetTuyen;
                                    else
                                        chamsoc.HoSoXetTuyen = null;
                                    //
                                    //View.ObjectSpace.CommitChanges();
                                    //
                                    sucessNumber++;
                                }
                            }
                            else
                            {
                                erorrNumber++;
                                detailLog.AppendLine("Khách hàng [" + khachHang.HoTen + "] không có số điện thoại.");
                            }
                        }
                        else if (obj.HinhThuc == Enum.TuyenSinh.HinhThucChamSocEnum.SMS)
                        {
                            ThongTinKhachHang khachHang = session.GetObjectByKey<ThongTinKhachHang>(item[0]);

                            if (khachHang != null && !string.IsNullOrEmpty(khachHang.DienThoaiDiDong))
                            {
                                //
                                bool daGuiSMS = true;
                                //daGuiSMS = SmsOTP.SendSMS(khachHang.DienThoaiDiDong, obj.NoiDungGuiMail);
                                if (daGuiSMS)
                                {
                                    ChamSocKhachHang chamsoc = new ChamSocKhachHang(session);
                                    chamsoc.NoiDung = obj.NoiDung;
                                    chamsoc.HinhThucChamSoc = obj.HinhThuc;
                                    chamsoc.GhiChu = obj.GhiChu;
                                    chamsoc.ThongTinKhachHang = khachHang;
                                    chamsoc.NgayThucHien = DateTime.Now;
                                    HoSoXetTuyen xetTuyen = session.GetObjectByKey<HoSoXetTuyen>(item[1]);
                                    if (xetTuyen != null)
                                        chamsoc.HoSoXetTuyen = xetTuyen;
                                    else
                                        chamsoc.HoSoXetTuyen = null;

                                    //
                                    //View.ObjectSpace.CommitChanges();
                                    //
                                    sucessNumber++;

                                }
                            }
                            else
                            {
                                erorrNumber++;
                                detailLog.AppendLine("Khách hàng [" + khachHang.HoTen + "] không có số điện thoại.");
                            }
                        }
                    }
                    if (erorrNumber == 0)
                        sucess = true;
                }
                else
                {
                    string messageKH = "alert('Vui lòng chọn khách hàng')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", messageKH);
                    return;
                }
            }
            catch (Exception ex)
            {
                View.ObjectSpace.Refresh();
                //
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Thất bại!!!')");
            }
            if (sucess)
            {
                //
                View.ObjectSpace.CommitChanges();
                string message = "alert('Xử lý thành công.')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                //Sau khi thêm mới refresh lại view.
                View.RefreshDataSource();

            }
            else
            {
                //
                string message = "alert('Thất bại " + erorrNumber + " khách hàng. Thành công " + sucessNumber + " khách hàng.')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                // Lấy đường dẫn file xem lỗi khi gửi mail
                string path = HttpContext.Current.Server.MapPath(Config.URLErorrImportExcel);
                //Tạo file
                Common.WriteDataToFile(path, detailLog.ToString());

            }

        }

        private void TuyenSinh_ChamSocKhachHang_TongHopController_Activated(object sender, EventArgs e)
        {
            bool truyCap = false;
            if (View.Id.Equals("ChamSocKhachHang_TongHop_DetailView"))
                truyCap = Commons.Common.IsWriteGranted<ChamSocKhachHang_TongHop>();
            simpleAction1.Active["TruyCap"] = truyCap;
            if (truyCap && Common.OidCustomListArray.Count > 0)
            {
                Common.OidCustomListArray = new List<Guid[]>();
            }

        }
    }
}
