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
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.Data.SqlClient;
using System.Data;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_TrucTiepThongBaoNhapHocController : ViewController<DetailView>
    {
        ThongBaoNhapHoc_TongHop obj;
        public TuyenSinh_TrucTiepThongBaoNhapHocController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            List<Guid> hocSinhList = Common.OidCustomList;
            Session session = ((XPObjectSpace)View.ObjectSpace).Session;
            //
            int sucessNumber = 0;
            int erorrNumber = 0;

            //Lấy một dòng dữ liệu đã chọn
            obj = View.CurrentObject as ThongBaoNhapHoc_TongHop;
            if (obj == null)
            {
                return;
            }
            else
            {
                if (obj.HinhThuc != Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.TrucTiep)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn hình thức trực tiếp!!!')");
                    return;
                }
                if (string.IsNullOrEmpty(obj.NoiDung))
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Nhập nội dung!!!')");
                    return;
                }
                if (hocSinhList.Count == 0)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn khách hàng!!!')");
                    return;
                }
            }
            //
            SecuritySystemUser_Custom currentUser = Common.SecuritySystemUser_GetCurrentUser();
            if (currentUser == null) return;
            //
            try
            {
                if (hocSinhList.Count > 0)
                {
                    //
                    DateTime dauNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                    DateTime cuoiNgay = dauNgay.AddDays(1).AddMinutes(-1);

                    //Tư vấn tuyển sinh
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and CongTy=?", obj.NamHoc.Oid, currentUser.CongTy.Oid);
                    ThongBaoNhapHoc thongBaoNhapHoc = session.FindObject<ThongBaoNhapHoc>(filter);
                    if (thongBaoNhapHoc == null)
                    {
                        thongBaoNhapHoc = new ThongBaoNhapHoc(session);
                        thongBaoNhapHoc.NamHoc = session.GetObjectByKey<NamHoc>(obj.NamHoc.Oid);
                        thongBaoNhapHoc.CongTy = session.GetObjectByKey<CongTy>(currentUser.CongTy.Oid);
                        //
                        View.ObjectSpace.CommitChanges();
                    }
                    //
                    DateTime ngayThongBao = DateTime.Now.Date;
                    //
                    foreach (var item in hocSinhList)
                    {
                        Module.NghiepVu.HocSinh.HocSinhs.HocSinh hocSinh = session.GetObjectByKey<Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(item);
                        if (hocSinh != null)
                        {
                            //Chi tiết tư vấn
                            filter = CriteriaOperator.Parse("ThongBaoNhapHoc=? and HocSinh=? and NgayThongBao =? and HinhThucThongBao = 1", thongBaoNhapHoc.Oid, item, ngayThongBao);
                            ChiTietThongBaoNhapHoc chiTiet = session.FindObject<ChiTietThongBaoNhapHoc>(filter);
                            if (chiTiet == null)
                            {
                                chiTiet = new ChiTietThongBaoNhapHoc(session);
                                chiTiet.HocSinh = hocSinh;
                                chiTiet.NgayThongBao = DateTime.Now;
                                chiTiet.NoiDung = obj.NoiDung;
                                chiTiet.Khoi = hocSinh.Lop.LopCha;
                                chiTiet.Lop = hocSinh.Lop;
                                chiTiet.HinhThucThongBao = Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.TrucTiep;
                                thongBaoNhapHoc.ListChiTietThongBaoNhapHoc.Add(chiTiet);
                                //
                                sucessNumber++;
                            }
                            else
                            {
                                erorrNumber++;
                            }
                        }
                        else
                        {
                            erorrNumber++;
                        }
                    }
                }
                //
                View.ObjectSpace.CommitChanges();
                string message = "alert('Xử lý thành công: " + sucessNumber + " học sinh. Thất bại: " + erorrNumber + ".')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                obj = ((DetailView)((View.ObjectSpace).Owner)).CurrentObject as ThongBaoNhapHoc_TongHop;
                obj.LoadDanhSachHocSinh();
                ((DetailView)((View.ObjectSpace).Owner)).Refresh();
            }
            catch (Exception ex)
            {
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Thất bại!!!')");
            }
        }

        private void TuyenSinh_TrucTiepThongBaoNhapHocController_Activated(object sender, EventArgs e)
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
