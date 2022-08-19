using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.DanhMuc.TuyenSinh;
using ERP.Module.DanhMuc.NhanSu;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_NguoiDaiDienHoSoNhapHocController : ViewController<ListView>
    {
        public TuyenSinh_NguoiDaiDienHoSoNhapHocController()
        {
            InitializeComponent();
            RegisterActions(components);

        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //
            bool daChinhSua = false;
            {
                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                //
                GiaDinhTre giaDinhTre = View.SelectedObjects[0] as GiaDinhTre;
                if (giaDinhTre != null && giaDinhTre.HoSoNhapHoc != null)
                {
                    if (giaDinhTre.ThongTinKhachHang != null)
                    {
                        ThongTinKhachHang khCu = session.GetObjectByKey<ThongTinKhachHang>(giaDinhTre.ThongTinKhachHang.Oid);
                        if (khCu != null)
                        {
                            //Cập nhật loại khách hàng tiềm năng
                            khCu.LoaiKhachHang = Enum.TuyenSinh.LoaiKhachHangTuyenSinhEnum.KhachHangCu;
                            //
                            giaDinhTre.HoSoNhapHoc.ThongTinKhachHang = khCu;
                        }
                        else
                        {
                            //
                            giaDinhTre.HoSoNhapHoc.ThongTinKhachHang = giaDinhTre.ThongTinKhachHang;
                        }
                    }
                    else
                    {
                        CriteriaOperator filter = CriteriaOperator.Parse("CMND like ?", giaDinhTre.CMND);
                        ThongTinKhachHang khNew = session.FindObject<ThongTinKhachHang>(filter);
                        if (khNew == null)
                        {
                            khNew = new ThongTinKhachHang(session);
                            khNew.HoTen = giaDinhTre.HoTen;
                            khNew.CMND = giaDinhTre.CMND;
                            khNew.GioiTinh = giaDinhTre.GioiTinh;
                            khNew.DienThoaiDiDong = giaDinhTre.DienThoaiDiDong;
                            khNew.Email = giaDinhTre.Email;
                            khNew.LoaiKhachHang = Enum.TuyenSinh.LoaiKhachHangTuyenSinhEnum.KhachHangCu;
                            //
                            filter = CriteriaOperator.Parse("TenNguonThuThap like ?", "Cung cấp thông tin lúc làm hồ sơ");
                            NguonThuThap nguonThuThap = session.FindObject<NguonThuThap>(filter);
                            if (nguonThuThap == null)
                            {
                                nguonThuThap = new NguonThuThap(session);
                                nguonThuThap.MaQuanLy = "TT";
                                nguonThuThap.TenNguonThuThap = "Cung cấp thông tin lúc làm hồ sơ";
                            }
                            khNew.NguonThuThap = nguonThuThap;
                            
                            // Địa chỉ
                            if (giaDinhTre.DiaChiThuongTru != null)
                            {
                                DiaChi diaChiThuongTru = new DiaChi(session);
                                if (giaDinhTre.DiaChiThuongTru.QuocGia != null)
                                    diaChiThuongTru.QuocGia = session.GetObjectByKey<QuocGia>(giaDinhTre.DiaChiThuongTru.QuocGia.Oid);
                                if (giaDinhTre.DiaChiThuongTru.QuanHuyen != null)
                                    diaChiThuongTru.QuanHuyen = session.GetObjectByKey<QuanHuyen>(giaDinhTre.DiaChiThuongTru.QuanHuyen.Oid);
                                if (giaDinhTre.DiaChiThuongTru.TinhThanh != null)
                                    diaChiThuongTru.TinhThanh = session.GetObjectByKey<TinhThanh>(giaDinhTre.DiaChiThuongTru.TinhThanh.Oid);
                                if (giaDinhTre.DiaChiThuongTru.XaPhuong != null)
                                    diaChiThuongTru.XaPhuong = session.GetObjectByKey<XaPhuong>(giaDinhTre.DiaChiThuongTru.XaPhuong.Oid);
                                if (giaDinhTre.DiaChiThuongTru.SoNha != null)
                                    diaChiThuongTru.SoNha = giaDinhTre.DiaChiThuongTru.SoNha;
                                //
                                khNew.DiaChiThuongTru = diaChiThuongTru;
                            }
                            else
                            {
                                if (giaDinhTre.HoSoNhapHoc.DiaChiThuongTru != null)
                                {
                                    DiaChi diaChiThuongTru = new DiaChi(session);
                                    if (giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.QuocGia != null)
                                        diaChiThuongTru.QuocGia = session.GetObjectByKey<QuocGia>(giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.QuocGia.Oid);
                                    if (giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.QuanHuyen != null)
                                        diaChiThuongTru.QuanHuyen = session.GetObjectByKey<QuanHuyen>(giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.QuanHuyen.Oid);
                                    if (giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.TinhThanh != null)
                                        diaChiThuongTru.TinhThanh = session.GetObjectByKey<TinhThanh>(giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.TinhThanh.Oid);
                                    if (giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.XaPhuong != null)
                                        diaChiThuongTru.XaPhuong = session.GetObjectByKey<XaPhuong>(giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.XaPhuong.Oid);
                                    if (giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.SoNha != null)
                                        diaChiThuongTru.SoNha = giaDinhTre.HoSoNhapHoc.DiaChiThuongTru.SoNha;
                                    //
                                    khNew.DiaChiThuongTru = diaChiThuongTru;
                                }
                            }
                            //
                            giaDinhTre.HoSoNhapHoc.ThongTinKhachHang = khNew;
                        }
                    }
                    //
                    daChinhSua = true;
                }
            }
            //
            if (daChinhSua)
            {
                View.Refresh();
                //
            }
        }

        private void TuyenSinh_NguoiDaiDienHoSoNhapHocController_Activated(object sender, EventArgs e)
        {
            //
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<GiaDinhTre>();

        }
    }
}
