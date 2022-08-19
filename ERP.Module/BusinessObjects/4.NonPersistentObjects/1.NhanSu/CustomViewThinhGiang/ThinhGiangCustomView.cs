using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Xpo.DB;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Thỉnh giảng (New)")]
    public class ThinhGiangCustomView : BaseObject
    {
        private Guid _Oid;
        public static int _DaNghiViec = 0;//0 là đang làm việc, 1 là nghỉ việc

        [Browsable(false)]
        public Guid Oid
        {
            get
            {
                return _Oid;
            }
            set
            {
                SetPropertyValue("Oid", ref _Oid, value);
            }
        }

        [ModelDefault("Caption", "Danh sách bộ phận")]
        public XPCollection<BoPhanTGView> BoPhanList { get; set; }

        [ModelDefault("Caption", "Danh sách thỉnh giảng")]
        public XPCollection<ThinhGiangView> ThinhGiangList { get; set; }

        public ThinhGiangCustomView(Session session) : base(session) { }

        public void LoadData(Guid OidBoPhan)
        {
            using (ERP.Module.Extends.DialogUtil.AutoWait())
            {
                ThinhGiangList = new XPCollection<ThinhGiangView>(Session, false);

                InOperator filter = new InOperator("BoPhan", Common.Department_GetRoledDepartmentList_ByDepartment(Session.GetObjectByKey<BoPhan>(OidBoPhan)));

                CriteriaOperator filter1 = null;
                if (_DaNghiViec == 0)
                    filter1 = CriteriaOperator.Parse("TinhTrang.DaNghiViec =?", false);
                else if (_DaNghiViec == 1)
                    filter1 = CriteriaOperator.Parse("TinhTrang.DaNghiViec =?", true);

                XPCollection<GiangVienThinhGiang> dsThinhGiang = new XPCollection<GiangVienThinhGiang>(Session, GroupOperator.And(filter, filter1));
                dsThinhGiang.Sorting.Add(new SortProperty("Ten", SortingDirection.Ascending));
                foreach (var item in dsThinhGiang)
                {
                    ThinhGiangView nhanVien = new ThinhGiangView(Session);
                    //
                    nhanVien.MaNhanVien = item.MaNhanVien;
                    nhanVien.MaTapDoan = item.MaTapDoan;
                    nhanVien.HoTen = item.HoTen;                   
                    nhanVien.GioiTinh = item.GioiTinh;
                    nhanVien.NgaySinh = item.NgaySinh;
                    nhanVien.CMND = item.CMND;
                    nhanVien.DiaChiThuongTru = item.DiaChiThuongTru == null ? null : Session.GetObjectByKey<DiaChi>(item.DiaChiThuongTru.Oid);
                    nhanVien.NoiOHienNay = item.NoiOHienNay == null ? null : Session.GetObjectByKey<DiaChi>(item.NoiOHienNay.Oid);
                    nhanVien.Email = item.Email;
                    nhanVien.DienThoaiDiDong = item.DienThoaiDiDong;
                    
                    nhanVien.NgayVaoCongTy = item.NgayVaoCongTy;
                    nhanVien.CongTy = item.CongTy == null ? null : item.CongTy.TenBoPhan;
                    nhanVien.BoPhan = item.BoPhan == null ? null : item.BoPhan.TenBoPhan;
                    nhanVien.TaiBoMon = item.TaiBoMon == null ? null : item.TaiBoMon.TenBoPhan;
                    nhanVien.ChucDanh = item.ChucDanh == null ? null : item.ChucDanh.TenChucDanh;                  
                    nhanVien.TinhTrang = item.TinhTrang == null ? null : item.TinhTrang.TenTinhTrang;

                    nhanVien.LoaiHopDong = item.LoaiHopDong == null ? null : item.LoaiHopDong.TenLoaiHopDong;
                    nhanVien.HopDongThinhGiang = item.HopDongThinhGiang == null ? null : item.HopDongThinhGiang.SoHopDong;
                    nhanVien.HocHam = item.NhanVienTrinhDo.HocHam == null ? null : item.NhanVienTrinhDo.HocHam.TenHocHam;
                    nhanVien.TrinhDoChuyenMon = item.NhanVienTrinhDo.TrinhDoChuyenMon == null ? null : item.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon;
                    nhanVien.ChuyenMonDaoTao = item.NhanVienTrinhDo.ChuyenNganhDaoTao == null ? null : item.NhanVienTrinhDo.ChuyenNganhDaoTao.TenChuyenNganhDaoTao;
                    //
                    nhanVien.Oid = item.Oid;
                    nhanVien.ThinhGiangCustomView = this;
                    //
                    ThinhGiangList.Add(nhanVien);
                }
                _Oid = Guid.Empty;
                OnChanged("ThinhGiangList");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            BoPhanList = new XPCollection<BoPhanTGView>(Session, false);
            ThinhGiangList = new XPCollection<ThinhGiangView>(Session, false);
            //
            InOperator filter = new InOperator("Oid",Common.Department_GetRoledDepartmentList_ByCurrentUser());
            CriteriaOperator filter1 = CriteriaOperator.Parse("NgungHoatDong =?", false);
            XPCollection<BoPhan> danhSachThinhGiang = new XPCollection<BoPhan>(Session, GroupOperator.And(filter, filter1));
            danhSachThinhGiang.Sorting.Add(new SortProperty("TenBoPhan", SortingDirection.Ascending));

            foreach (var item in danhSachThinhGiang)
            {
                BoPhanTGView boPhan = new BoPhanTGView(Session);
                boPhan.TenBoPhan = item.TenBoPhan;
                boPhan.Oid = item.Oid;
                boPhan.ThinhGiangCustomView = this;
                boPhan.ListBoPhan = new XPCollection<BoPhanTGView>(Session, false);
                //
                BoPhanList.Add(boPhan);
            }

            foreach (var item in danhSachThinhGiang)
            {
                if (item.BoPhanCha != null)
                {
                    int indexCha = BoPhanList.FindIndex<BoPhanTGView>(s => s.Oid.ToString() == item.BoPhanCha.Oid.ToString());
                    int index = BoPhanList.FindIndex<BoPhanTGView>(s => s.Oid.ToString() == item.Oid.ToString());
                    //
                    if (indexCha >= 0)
                    {
                        BoPhanList[index].BoPhanCha = BoPhanList[indexCha];
                        BoPhanList[indexCha].ListBoPhan.Add(BoPhanList[index]);
                    }
                }
            }

            bool Chon = Common.CauHinhChung_GetCauHinhChung.CauHinhHoSo.KhongHienNhanVienKhiChonCongTy;
            if (!Chon)
            {
                if (BoPhanList != null)
                    LoadData(Common.CongTy(Session).Oid);
            }
        }
    }
}
