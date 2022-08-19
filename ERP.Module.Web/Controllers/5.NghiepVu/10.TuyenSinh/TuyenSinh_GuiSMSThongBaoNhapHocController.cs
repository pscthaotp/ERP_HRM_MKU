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
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_GuiSMSThongBaoNhapHocController : ViewController<DetailView>
    {
        public TuyenSinh_GuiSMSThongBaoNhapHocController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            List<Guid> hocSinhList = Common.OidCustomList;
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            ThongBaoNhapHoc_TongHop obj = View.CurrentObject as ThongBaoNhapHoc_TongHop;
            //
            if (obj == null)
            {
                return;
            }
            else
            {
                if (obj.HinhThuc != Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.SMS)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn hình thức SMS!!!')");
                    return;
                }
                if (string.IsNullOrEmpty(obj.NoiDung))
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Nhập nội dung!!!')");
                    return;
                }
                if (hocSinhList.Count == 0)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn học sinh!!!')");
                    return;
                }
            }
            //
            SecuritySystemUser_Custom currentUser = Common.SecuritySystemUser_GetCurrentUser();
            if (currentUser == null) return;

            try
            {
                if (hocSinhList.Count > 0)
                {
                    //
                    Session session = ((XPObjectSpace)View.ObjectSpace).Session;

                    //Tư vấn tuyển sinh
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and CongTy=?", obj.NamHoc, currentUser.CongTy.Oid);
                    ThongBaoNhapHoc thongBaoNhapHoc = session.FindObject<ThongBaoNhapHoc>(filter);
                    if (thongBaoNhapHoc == null)
                    {
                        thongBaoNhapHoc = new ThongBaoNhapHoc(session);
                        thongBaoNhapHoc.NamHoc = session.GetObjectByKey<NamHoc>(obj.NamHoc.Oid);
                        //
                        View.ObjectSpace.CommitChanges();
                    }
                    //
                    DateTime ngayThongBao = DateTime.Now;
                    foreach (var item in hocSinhList)
                    {
                        Module.NghiepVu.HocSinh.HocSinhs.HocSinh hocSinh = session.GetObjectByKey<Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(item);
                        if (hocSinh != null && (!string.IsNullOrEmpty(hocSinh.DienThoaiCha) || !string.IsNullOrEmpty(hocSinh.DienThoaiMe)))
                        {
                            //
                            bool daGuiSMS = false;

                            //Chi tiết tư vấn
                            filter = CriteriaOperator.Parse("ThongBaoNhapHoc=? and HocSinh=? and NgayThongBao =? and HinhThucThongBao = 3", thongBaoNhapHoc.Oid, item, ngayThongBao);
                            ChiTietThongBaoNhapHoc chiTiet = session.FindObject<ChiTietThongBaoNhapHoc>(filter);
                            if (chiTiet == null)
                            {
                                chiTiet = new ChiTietThongBaoNhapHoc(session);
                                chiTiet.HocSinh = hocSinh;
                                chiTiet.NgayThongBao = DateTime.Now;

                                //Nội dung
                                string noidung = string.Format("{0}, Bé {1}, Ngày vào học {2}", obj.NoiDung.Trim(), hocSinh.HoTen, hocSinh.NgayVaoHoc.ToString("dd/MM/yyyy"));
                                chiTiet.NoiDung = noidung;
                                //
                                chiTiet.Khoi = hocSinh.Lop.LopCha;
                                chiTiet.Lop = hocSinh.Lop;
                                chiTiet.HinhThucThongBao = Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.SMS;
                                chiTiet.ThongBaoNhapHoc = thongBaoNhapHoc;

                                //Lấy số điện thoại
                                string sodienthoai = string.Empty;
                                if (!string.IsNullOrEmpty(hocSinh.DienThoaiCha))
                                    sodienthoai = hocSinh.DienThoaiCha;
                                else
                                    sodienthoai = hocSinh.DienThoaiMe;
                                //
                                daGuiSMS = SmsOTP.SendSMS(sodienthoai, noidung);
                            }
                            //
                            if (daGuiSMS)
                            {
                                //
                                View.ObjectSpace.CommitChanges();
                                //
                                sucessNumber++;
                            }
                            else
                            {
                                erorrNumber++;
                            }
                        }
                    }
                }
                //
                string message = "alert('Gửi thành công: " + sucessNumber + " dòng. Số dòng thất bại: " + erorrNumber + "')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
            }
            catch (Exception ex)
            {
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Thất bại!!!')");
            }
        }

        private void TuyenSinh_GuiSMSThongBaoNhapHocController_Activated(object sender, EventArgs e)
        {
            bool truyCap = Commons.Common.IsWriteGranted<ThongBaoNhapHoc_TongHop>();
            simpleAction1.Active["TruyCap"] = truyCap;
            //
            if (truyCap && Common.OidCustomList.Count > 0)
            {
                Common.OidCustomList = new List<Guid>();
            }
        }
    }
}
