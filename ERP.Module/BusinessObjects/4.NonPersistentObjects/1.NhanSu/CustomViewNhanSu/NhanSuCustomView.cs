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
    [ModelDefault("Caption", "Nhân viên (New)")]
    public class NhanSuCustomView : BaseObject
    {
        private Guid _Oid;
        public static int _DaNghiViec = 0;//0 là đang làm việc, 1 là nghỉ việc, 2 là chưa nhận việc

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
        public XPCollection<BoPhanView> BoPhanList { get; set; }


        [ModelDefault("Caption", "Danh sách nhân sự")]
        public XPCollection<NhanSuView> NhanSuList { get; set; }

        public NhanSuCustomView(Session session) : base(session) { }

        public void LoadData(Guid OidBoPhan)
        {
            using (ERP.Module.Extends.DialogUtil.AutoWait("Đang lấy danh sách nhân viên!"))
            {
                NhanSuList = new XPCollection<NhanSuView>(Session, false);

                InOperator filter = new InOperator("TaiBoMon", Common.Department_GetRoledDepartmentList_ByDepartment(Session.GetObjectByKey<BoPhan>(OidBoPhan)));
                XPCollection<BoPhan> dsbp = new XPCollection<BoPhan>(Session, CriteriaOperator.Parse("Oid = ?", OidBoPhan));
                foreach (var item in dsbp)
                {
                    if (item.ListBoPhanCon.Count > 0 || item.LoaiBoPhan == Enum.NhanSu.LoaiBoPhanEnum.PhongBan || item.LoaiBoPhan == Enum.NhanSu.LoaiBoPhanEnum.Khoi)//>
                    {
                        filter = new InOperator("BoPhan", Common.Department_GetRoledDepartmentList_ByDepartment(Session.GetObjectByKey<BoPhan>(OidBoPhan)));
                        break;
                    }
                }

                CriteriaOperator filter1 = null;
                if (_DaNghiViec == 0)
                    filter1 = CriteriaOperator.Parse("TinhTrang.DaNghiViec =?", false);
                else 
                    filter1 = CriteriaOperator.Parse("TinhTrang.DaNghiViec =?", true);

                XPCollection<ThongTinNhanVien> dsNhanVien = new XPCollection<ThongTinNhanVien>(Session, GroupOperator.And(filter, filter1));
                dsNhanVien.Sorting.Add(new SortProperty("ChucVu.CapBac", SortingDirection.Ascending));
                dsNhanVien.Sorting.Add(new SortProperty("Ten", SortingDirection.Ascending));
                foreach (var item in dsNhanVien)
                {
                    NhanSuView nhanVien = new NhanSuView(Session);
                    //
                    nhanVien.MaTapDoan = item.MaTapDoan;
                    nhanVien.MaNhanVien = item.MaNhanVien;                 
                    nhanVien.HoTen = item.HoTen;                                      
                    nhanVien.GioiTinh = item.GioiTinh;
                    nhanVien.NgaySinh = item.NgaySinh;
                    nhanVien.CMND = item.CMND;
                    nhanVien.DiaChiThuongTru = item.DiaChiThuongTru == null ? null : Session.GetObjectByKey<DiaChi>(item.DiaChiThuongTru.Oid);
                    nhanVien.NoiOHienNay = item.NoiOHienNay == null ? null : Session.GetObjectByKey<DiaChi>(item.NoiOHienNay.Oid);
                    nhanVien.Email = item.Email;
                    nhanVien.EmailNoiBo = item.EmailNoiBo;
                    nhanVien.DienThoaiDiDong = item.DienThoaiDiDong;
                    nhanVien.DienThoaiNoiBo = item.DienThoaiNoiBo;

                    nhanVien.CongTy = item.CongTy == null ? null : item.CongTy.TenBoPhan;
                    nhanVien.BoPhan = item.BoPhan == null ? null : item.BoPhan.TenBoPhan;
                    nhanVien.To = item.To != null ? item.To.TenTo : string.Empty;
                    nhanVien.ChucVu = item.ChucVu == null ? null : item.ChucVu.TenChucVu;
                    nhanVien.ChucDanh = item.ChucDanh == null ? null : item.ChucDanh.TenChucDanh;
                    nhanVien.LoaiNhanSu = item.LoaiNhanSu == null ? null : item.LoaiNhanSu.TenLoaiNhanSu;
                    nhanVien.LoaiHopDong = item.LoaiHopDong == null ? null : item.LoaiHopDong.TenLoaiHopDong;
                    nhanVien.HocHam = item.NhanVienTrinhDo.HocHam != null ? item.NhanVienTrinhDo.HocHam.TenHocHam : string.Empty;
                    nhanVien.TrinhDoChuyenMon = item.NhanVienTrinhDo.TrinhDoChuyenMon != null ? item.NhanVienTrinhDo.TrinhDoChuyenMon.TenTrinhDoChuyenMon : string.Empty;
                    nhanVien.TinhTrang = item.TinhTrang == null ? null : item.TinhTrang.TenTinhTrang;
                    if (_DaNghiViec == 1)
                        nhanVien.NgayNghiViec = item.NgayNghiViec;
 
                    nhanVien.NgayVaoTapDoan = item.NgayVaoTapDoan;
                    nhanVien.NgayVaoCongTy = item.NgayVaoCongTy;
                    //nhanVien.ThamNienLamViec = item.ThamNienLamViec != DateTime.MinValue ? String.Concat(DateTime.Now.Subtract(item.ThamNienLamViec).Days.ToString(), " ngày") : string.Empty;
                    nhanVien.GiangDay = item.GiangDay;
                    nhanVien.NhomPhanBo = item.NhomPhanBo == null ? null : item.NhomPhanBo.CostCenter;
                    //
                    nhanVien.Oid = item.Oid;
                    nhanVien.NhanSuCustomView = this;
                    //
                    NhanSuList.Add(nhanVien);
                }
                _Oid = Guid.Empty;
                OnChanged("NhanSuList");
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            BoPhanList = new XPCollection<BoPhanView>(Session, false);
            NhanSuList = new XPCollection<NhanSuView>(Session, false);
            //
            InOperator filter = new InOperator("Oid", Common.Department_GetRoledDepartmentList_ByCurrentUser());
            CriteriaOperator filter1 = CriteriaOperator.Parse("NgungHoatDong =?", false);
            XPCollection<BoPhan> danhSachNhanSu = new XPCollection<BoPhan>(Session, GroupOperator.And(filter, filter1));
            danhSachNhanSu.Sorting.Add(new SortProperty("TenBoPhan", SortingDirection.Ascending));

            foreach (var item in danhSachNhanSu)
            {
                BoPhanView boPhan = new BoPhanView(Session);
                boPhan.TenBoPhan = item.TenBoPhan;
                boPhan.Oid = item.Oid;
                boPhan.NhanSuCustomView = this;
                boPhan.LoaiBoPhan = item.LoaiBoPhan;
                boPhan.ListBoPhan = new XPCollection<BoPhanView>(Session, false);
                //
                BoPhanList.Add(boPhan);
            }

            foreach (var item in danhSachNhanSu)
            {
                if (item.BoPhanCha != null)
                {
                    int indexCha = BoPhanList.FindIndex<BoPhanView>(s => s.Oid.ToString() == item.BoPhanCha.Oid.ToString());
                    int index = BoPhanList.FindIndex<BoPhanView>(s => s.Oid.ToString() == item.Oid.ToString());
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
