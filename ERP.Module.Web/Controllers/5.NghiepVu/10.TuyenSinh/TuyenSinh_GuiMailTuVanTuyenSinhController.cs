using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using DevExpress.Data.Filtering;
using ERP.Module.HeThong;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Extends;
using DevExpress.Xpo;
using ERP.Module.Enum.Systems;
using System.Web;
using System.IO;
using System.Text;
using ERP.Module.DanhMuc.TuyenSinh;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_GuiMailTuVanTuyenSinhController : ViewController<DetailView>
    {
        public TuyenSinh_GuiMailTuVanTuyenSinhController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            List<Guid> khachHangList = Common.OidCustomList;
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            StringBuilder detailLog = new StringBuilder();
            TuVanTuyenSinh_TongHop obj = View.CurrentObject as TuVanTuyenSinh_TongHop;
            SecuritySystemUser_Custom currentUser = Common.SecuritySystemUser_GetCurrentUser();
            if (currentUser == null) return;
            //
            if (obj == null)
            {
                return;
            }
            else
            {
                if (obj.HinhThuc != Enum.TuyenSinh.HinhThucTuVanEnum.Email)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn hình thức Email!!!')");
                    return;
                }
                if (string.IsNullOrEmpty(obj.TieuDe))
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Nhập tiêu đề!!!')");
                    return;
                }
                if (string.IsNullOrEmpty(obj.NoiDung))
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
                if (khachHangList.Count > 0)
                {
                    //
                    DateTime dauNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                    DateTime cuoiNgay = dauNgay.AddDays(1).AddMinutes(-1);
                    Session session = ((XPObjectSpace)View.ObjectSpace).Session;

                    //Tư vấn tuyển sinh
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and CongTy=?", obj.NamHoc, currentUser.CongTy.Oid);
                    TuVanTuyenSinh tuVanTuyenSinh = session.FindObject<TuVanTuyenSinh>(filter);
                    if (tuVanTuyenSinh == null)
                    {
                        tuVanTuyenSinh = new TuVanTuyenSinh(session);
                        tuVanTuyenSinh.NamHoc = session.GetObjectByKey<NamHoc>(obj.NamHoc.Oid);
                        //
                        View.ObjectSpace.CommitChanges();
                    }
                    foreach (var item in khachHangList)
                    {
                        ThongTinKhachHang khachHang = session.GetObjectByKey<ThongTinKhachHang>(item);

                        if (khachHang != null && !string.IsNullOrEmpty(khachHang.Email))
                        {
                            //
                            bool daGuiMail = false;
                            DateTime ngayTuVan = DateTime.Now;

                            //Chi tiết tư vấn
                            filter = CriteriaOperator.Parse("TuVanTuyenSinh=? and ThongTinKhachHang=? and HinhThucTuVan = 2 and NgayTuVan=?", tuVanTuyenSinh.Oid, item, ngayTuVan);
                            ChiTietTuVanTuyenSinh chiTiet = session.FindObject<ChiTietTuVanTuyenSinh>(filter);
                            if (chiTiet == null)
                            {
                                chiTiet = new ChiTietTuVanTuyenSinh(session);
                                chiTiet.ThongTinKhachHang = khachHang;
                                chiTiet.NgayTuVan = ngayTuVan;
                                chiTiet.NoiDung = obj.NoiDung;
                                chiTiet.HinhThucTuVan = Enum.TuyenSinh.HinhThucTuVanEnum.Email;
                                chiTiet.TuVanTuyenSinh = tuVanTuyenSinh;
                                chiTiet.LoaiKhachHang = session.GetObjectByKey<LoaiKhachHang>(obj.LoaiKhachHang.Oid);
                            }

                            //Tránh gửi 1 người 1 ngày
                            filter = CriteriaOperator.Parse("SecuritySystemUser=? and  KeyMask=?", currentUser.Oid, chiTiet.Oid);
                            MailManager mail = session.FindObject<MailManager>(filter);
                            if (mail == null)
                            {
                                //
                                string emailgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Email;
                                string passgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Password;
                                string noidung = obj.NoiDung;
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
                                daGuiMail = MailHelpers.CreateMailManager(session, tieude, fullPath, emailgui, passgui, khachHang.Email, noidung, TrangThaiGuiMailEnum.DaGuiMailTuVanTuyenSinh, chiTiet.Oid);

                            }

                            //
                            if (daGuiMail)
                            {
                                //
                                View.ObjectSpace.CommitChanges();
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
                }
                //
                string message = "alert('Gửi thành công: " + sucessNumber + " dòng. Số dòng thất bại: " + erorrNumber + "')";
                if (erorrNumber > 0)
                {  // Lấy đường dẫn file xem lỗi khi gửi mail
                    string path = HttpContext.Current.Server.MapPath(Config.URLErorrImportExcel);
                    //Tạo file
                    Common.WriteDataToFile(path, detailLog.ToString());
                }
            }
            catch (Exception ex)
            {
                View.ObjectSpace.Refresh();
                //
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Thất bại!!!')");
            }
        }

        private void TuyenSinh_GuiMailTuVanTuyenSinhController_Activated(object sender, EventArgs e)
        {
            bool truyCap = false;
            if (View.Id.Equals("TuVanTuyenSinh_TongHop_DetailView"))
                truyCap = Commons.Common.IsWriteGranted<TuVanTuyenSinh_TongHop>();
            simpleAction1.Active["TruyCap"] = truyCap;
            //
            if (truyCap && Common.OidCustomList.Count > 0)
            {
                Common.OidCustomList = new List<Guid>();
            }
        }
    }
}
