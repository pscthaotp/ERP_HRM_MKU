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
using ERP.Module.DanhMuc.TuyenSinh;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_TrucTiepTuVanTuyenSinhController : ViewController<DetailView>
    {
        public TuyenSinh_TrucTiepTuVanTuyenSinhController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            List<Guid> khachHangList = Common.OidCustomList;

            //Lấy một dòng dữ liệu đã chọn
            TuVanTuyenSinh_TongHop obj = View.CurrentObject as TuVanTuyenSinh_TongHop;
            if (obj == null)
            {
                return;
            }
            else
            {
                if (obj.HinhThuc != Enum.TuyenSinh.HinhThucTuVanEnum.TrucTiep)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn hình thức trực tiếp!!!')");
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
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            //
            SecuritySystemUser_Custom currentUser = Common.SecuritySystemUser_GetCurrentUser();
            if (currentUser == null) return;
            if (obj != null)
            {
                try
                {
                    if (khachHangList.Count > 0)
                    {
                        //
                        DateTime dauNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                        DateTime cuoiNgay = dauNgay.AddDays(1).AddMinutes(-1);
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;

                        //Tư vấn tuyển sinh
                        CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and CongTy=?", obj.NamHoc.Oid,currentUser.CongTy.Oid);
                        TuVanTuyenSinh tuVanTuyenSinh = session.FindObject<TuVanTuyenSinh>(filter);
                        if (tuVanTuyenSinh == null)
                        {
                            tuVanTuyenSinh = new TuVanTuyenSinh(session);
                            tuVanTuyenSinh.NamHoc = session.GetObjectByKey<NamHoc>(obj.NamHoc.Oid);
                            //
                            View.ObjectSpace.CommitChanges();
                        }
                        DateTime ngayTuVan = DateTime.Now;
                        //
                        foreach (var item in khachHangList)
                        {
                            ThongTinKhachHang khachHang = session.GetObjectByKey<ThongTinKhachHang>(item);
                            if (khachHang != null)
                            {
                                //Chi tiết tư vấn
                                filter = CriteriaOperator.Parse("TuVanTuyenSinh=? and ThongTinKhachHang=? and NgayTuVan = ? and HinhThucTuVan = 3", tuVanTuyenSinh.Oid, item, ngayTuVan);
                                ChiTietTuVanTuyenSinh chiTiet = session.FindObject<ChiTietTuVanTuyenSinh>(filter);
                                if (chiTiet == null)
                                {
                                    chiTiet = new ChiTietTuVanTuyenSinh(session);
                                    chiTiet.ThongTinKhachHang = khachHang;
                                    chiTiet.NgayTuVan = ngayTuVan;
                                    chiTiet.NoiDung = obj.NoiDung;
                                    chiTiet.HinhThucTuVan = Enum.TuyenSinh.HinhThucTuVanEnum.TrucTiep;
                                    chiTiet.TuVanTuyenSinh = tuVanTuyenSinh;
                                    chiTiet.LoaiKhachHang = session.GetObjectByKey<LoaiKhachHang>(obj.LoaiKhachHang.Oid);
                                    //
                                    View.ObjectSpace.CommitChanges();
                                    //
                                    sucessNumber++;
                                }
                                else
                                {
                                    erorrNumber += 1;
                                }
                            }
                            else
                            {
                                erorrNumber += 1;
                            }
                        }
                    }
                    //
                    string message = "alert('Xử lý thành công: " + sucessNumber + " khách hàng. Thất bại: " + erorrNumber + ".')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                }
                catch (Exception ex)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Thất bại!!!')");
                }
            }
        }

        private void TuyenSinh_TrucTiepTuVanTuyenSinhController_Activated(object sender, EventArgs e)
        {
            bool truyCap = Commons.Common.IsWriteGranted<TuVanTuyenSinh_TongHop>();
            simpleAction1.Active["TruyCap"] = truyCap;
            //
            if (truyCap && Common.OidCustomList.Count > 0)
            {
                Common.OidCustomList = new List<Guid>();
            }
        }
    }
}
