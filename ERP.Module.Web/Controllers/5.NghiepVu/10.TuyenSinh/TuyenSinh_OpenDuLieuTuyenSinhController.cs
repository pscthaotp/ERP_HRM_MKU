using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ERP.Module.Commons;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.NghiepVu.TuyenSinh_TP;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_OpenDuLieuTuyenSinhController : ViewController
    {
        public TuyenSinh_OpenDuLieuTuyenSinhController()
        {
            InitializeComponent();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if(View.Id.Equals("DuyetThongBaoTuyenSinh_ListChiTietDuyetThongBaoTuyenSinh_ListView"))
            {
                ChiTietDuyetThongBaoTuyenSinh chiTietThongBaoTuyenSinh= View.CurrentObject as ChiTietDuyetThongBaoTuyenSinh;
                //
                if (chiTietThongBaoTuyenSinh != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    ThongBaoTuyenSinh thongBao = obs.FindObject<ThongBaoTuyenSinh>(CriteriaOperator.Parse("Oid=?", chiTietThongBaoTuyenSinh.Oid));
                    //
                    if (thongBao != null)
                    {
                        Application.ShowModelView<ThongBaoTuyenSinh>(obs, thongBao);
                    }
                }
            }
            if (View.Id.Equals("TuVanTuyenSinh_TongHop_ListChiTietTuVanTuyenSinh_ListView"))
            {
                ChiTietTuVanTuyenSinh_TongHop chiTietTuVanTuyenSinh = View.CurrentObject as ChiTietTuVanTuyenSinh_TongHop;
                //
                if (chiTietTuVanTuyenSinh != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    ThongTinKhachHang khachHang = obs.FindObject<ThongTinKhachHang>(CriteriaOperator.Parse("MaKhachHang=?", chiTietTuVanTuyenSinh.MaKhachHang));
                    //
                    if (khachHang != null)
                    {
                        Application.ShowModelView<ThongTinKhachHang>(obs, khachHang);
                    }
                }
            }

            if (View.Id.Equals("ThongBaoNhapHoc_TongHop_ListChiTietThongBaoNhapHoc_ListView"))
            {
                ChiTietThongBaoNhapHoc_TongHop chiTiet = View.CurrentObject as ChiTietThongBaoNhapHoc_TongHop;
                //
                if (chiTiet != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    Module.NghiepVu.HocSinh.HocSinhs.HocSinh hocSinh = obs.FindObject<Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(CriteriaOperator.Parse("MaQuanLy=?", chiTiet.MaHocSinh));
                    //
                    if (hocSinh != null)
                    {
                        Application.ShowModelView<HoSoNhapHoc>(obs, hocSinh.HoSoNhapHoc);
                    }
                }
            }

            if (View.Id.Equals("XyLyYKien_ListChiTietXyLyYKien_ListView"))
            {
                ChiTietXyLyYKien chiTiet = View.CurrentObject as ChiTietXyLyYKien;
                //
                if (chiTiet != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    YKienKhachHang yKien = obs.GetObjectByKey<YKienKhachHang>(chiTiet.Oid);
                    //
                    if (yKien != null)
                    {
                        Application.ShowEditModelView<YKienKhachHang>(obs, yKien);
                    }
                }
            }
            if(View.Id.Equals("TuVanTuyenSinh_ListChiTietTuVanTuyenSinh_ListView"))
            {
                ChiTietTuVanTuyenSinh chiTiet = View.CurrentObject as ChiTietTuVanTuyenSinh;
                //
                if (chiTiet != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    ThongTinKhachHang khachHang = obs.GetObjectByKey<ThongTinKhachHang>(chiTiet.ThongTinKhachHang.Oid);
                    //
                    if (khachHang != null)
                    {
                        Application.ShowEditModelView<ThongTinKhachHang>(obs, khachHang);
                    }
                }
            }
            if (View.Id.Equals("ToChucSuKien_ListChiTietToChucSuKien_ListView"))
            {
                ChiTietToChucSuKien chiTiet = View.CurrentObject as ChiTietToChucSuKien;
                //
                if (chiTiet != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    ThongTinKhachHang khachHang = obs.GetObjectByKey<ThongTinKhachHang>(chiTiet.ThongTinKhachHang.Oid);
                    //
                    if (khachHang != null)
                    {
                        Application.ShowEditModelView<ThongTinKhachHang>(obs, khachHang);
                    }
                }
            }
            if (View.Id.Equals("ChamSocKhachHang_ListView"))
            {
                ChamSocKhachHang chamSoc = View.CurrentObject as ChamSocKhachHang;
                //
                if (chamSoc != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    ThongTinKhachHang khachHang = obs.FindObject<ThongTinKhachHang>(CriteriaOperator.Parse("Oid=?", chamSoc.ThongTinKhachHang.Oid));
                    //
                    if (khachHang != null)
                    {
                        Application.ShowModelView<ThongTinKhachHang>(obs, khachHang);
                    }
                }
            }
            if (View.Id.Equals("YKienKhachHang_ListView"))
            {
                YKienKhachHang yKien = View.CurrentObject as YKienKhachHang;
                //
                if (yKien != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    ThongTinKhachHang khachHang = obs.FindObject<ThongTinKhachHang>(CriteriaOperator.Parse("Oid=?", yKien.ThongTinKhachHang.Oid));
                    //
                    if (khachHang != null)
                    {
                        Application.ShowModelView<ThongTinKhachHang>(obs, khachHang);
                    }
                }
            }
            //if (View.Id.Equals("YKienKhachHang_TP_ListView"))
            //{
            //    YKienKhachHang_TP yKien = View.CurrentObject as YKienKhachHang_TP;
            //    //
            //    if (yKien != null)
            //    {
            //        //Lấy dữ liệu mới nhất
            //        IObjectSpace obs = Application.CreateObjectSpace();
            //        //
            //        ThongTinKhachHang khachHang = obs.FindObject<ThongTinKhachHang>(CriteriaOperator.Parse("Oid=?", yKien.ThongTinKhachHang.Oid));
            //        //
            //        if (khachHang != null)
            //        {
            //            Application.ShowModelView<ThongTinKhachHang>(obs, khachHang);
            //        }
            //    }
            //}
            if (View.Id.Equals("DuyetToChucSuKien_ListChiTietDuyetToChucSuKien_ListView"))
            {
                ChiTietDuyetToChucSuKien chiTiet = View.CurrentObject as ChiTietDuyetToChucSuKien;
                //
                if (chiTiet != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    ToChucSuKien suKien = obs.GetObjectByKey<ToChucSuKien>(chiTiet.ToChucSuKien.Oid);
                    if (suKien != null)
                    {
                        Application.ShowModelView<ToChucSuKien>(obs, suKien);
                    }
                }
            }
            if (View.Id.Equals("HoSoNhapHoc_LienKetList_ListView"))
            {
                HoSoNhapHoc_LienKet chiTiet = View.CurrentObject as HoSoNhapHoc_LienKet;
                //
                if (chiTiet != null)
                {
                    //Lấy dữ liệu mới nhất
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    HoSoNhapHoc suKien = obs.GetObjectByKey<HoSoNhapHoc>(chiTiet.LienKet.Oid);
                    if (suKien != null)
                    {
                        Application.ShowModelView<HoSoNhapHoc>(obs, suKien);
                    }
                }
            }
        }

        private void TuyenSinh_OpenDuLieuTuyenSinhController_Activated(object sender, EventArgs e)
        {
            //
            if (View.Id.Equals("DuyetThongBaoTuyenSinh_ListChiTietDuyetThongBaoTuyenSinh_ListView")
                || View.Id.Equals("XyLyYKien_ListChiTietXyLyYKien_ListView")
                || View.Id.Equals("TuVanTuyenSinh_ListChiTietTuVanTuyenSinh_ListView")
                || View.Id.Equals("TuVanTuyenSinh_TongHop_ListChiTietTuVanTuyenSinh_ListView")
                || View.Id.Equals("ThongBaoNhapHoc_TongHop_ListChiTietThongBaoNhapHoc_ListView")
                || View.Id.Equals("ToChucSuKien_ListChiTietToChucSuKien_ListView")
                || View.Id.Equals("ChamSocKhachHang_ListView")
                || View.Id.Equals("YKienKhachHang_ListView")
                || View.Id.Equals("YKienKhachHang_TP_ListView")
                || View.Id.Equals("DuyetToChucSuKien_ListChiTietDuyetToChucSuKien_ListView")
                || View.Id.Equals("HoSoNhapHoc_LienKetList_ListView")
                )
            {
                simpleAction1.Active["TruyCap"] = true;
            }
            else
                simpleAction1.Active["TruyCap"] = false;
            //
        }
    }
}
