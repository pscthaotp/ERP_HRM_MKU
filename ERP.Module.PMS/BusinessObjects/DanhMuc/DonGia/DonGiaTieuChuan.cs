using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.BusinessObjects;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.PMS.DanhMuc;
using ERP.Module.PMS.Enum;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.PMS.DonGia
{
    [ModelDefault("Caption", "Đơn giá tiêu chuẩn")]
    public class DonGiaTieuChuan : BaseObject
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private ChucDanh _ChucDanh;
        private LoaiHocPhan _LoaiHocPhan;
        //private NhomNganh _NhomNganh; Hiện tại nhóm ngành này không sử dụng nữa 
        private NhomMon _NhomMon;
        private NganhHoc _NganhHoc;
        private KhoiNganh _KhoiNganh; 
        private LoaiGiangVienEnum? _LoaiGiangVien;
        //
        private decimal _DonGia;
        private decimal _DonGiaNgoaiDL;
        private bool _NgungApDung;
        private string _GhiChu;

        [ModelDefault("Caption", "Trường")]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { SetPropertyValue("CongTy", ref _CongTy, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }
        [ModelDefault("Caption", "Học hàm")]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set
            {
                SetPropertyValue("HocHam ", ref _HocHam, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon ", ref _TrinhDoChuyenMon, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        public ChucDanh ChucDanh
        {
            get { return _ChucDanh; }
            set
            {
                SetPropertyValue("ChucDanh ", ref _ChucDanh, value);
                OnChanged("Caption");//Mới thêm : Cập nhật Caption
            }
        }

        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhan LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set
            {
                SetPropertyValue("LoaiHocPhan ", ref _LoaiHocPhan, value);
            }
        }

        //[ModelDefault("Caption", "Nhóm ngành")]//Học vị
        ////[VisibleInListView(false)]
        //public NhomNganh NhomNganh
        //{
        //    get { return _NhomNganh; }
        //    set
        //    {
        //        SetPropertyValue("NhomNganh ", ref _NhomNganh, value);
        //    }
        //}


        [ModelDefault("Caption", "Nhóm môn")]
        public NhomMon NhomMon
        {
            get { return _NhomMon; }
            set { SetPropertyValue("NhomMon", ref _NhomMon, value); }
        }

        [ModelDefault("Caption", "Ngành học")]
        public NganhHoc NganhHoc
        {
            get { return _NganhHoc; }
            set { SetPropertyValue("NganhHoc", ref _NganhHoc, value); }
        }

        [ModelDefault("Caption", "Khối ngành")]
        public KhoiNganh KhoiNganh
        {
            get { return _KhoiNganh; }
            set { SetPropertyValue("KhoiNganh", ref _KhoiNganh, value); }
        }


        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum? LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set
            {
                SetPropertyValue("LoaiGiangVien ", ref _LoaiGiangVien, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá nội tỉnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DonGiaTrinhDo", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set
            {
                SetPropertyValue("DonGia ", ref _DonGia, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá ngoại tỉnh")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DonGiaNgoaiDL", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá ngoại tỉnh > 0")]
        public decimal DonGiaNgoaiDL
        {
            get { return _DonGiaNgoaiDL; }
            set
            {
                SetPropertyValue("DonGiaNgoaiDL ", ref _DonGiaNgoaiDL, value);
            }
        }

        [ModelDefault("Caption", "Ngưng áp dụng")]//Học vị
        //[VisibleInListView(false)]
        public bool NgungApDung
        {
            get { return _NgungApDung; }
            set
            {
                SetPropertyValue("NgungApDung ", ref _NgungApDung, value);
            }
        }
        [ModelDefault("Caption", "Lý do điều chỉnh/Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }


        [NonPersistent]
        [VisibleInListView(false)]
        public String Caption
        {
            get
            {
                return String.Format(" {0} {1} {2} ", HocHam != null ? " Học hàm: " + HocHam.TenHocHam : "", TrinhDoChuyenMon != null ? "- Trình độ: " + TrinhDoChuyenMon.TenTrinhDoChuyenMon : "", ChucDanh != null ? "- Chức danh: " + ChucDanh.TenChucDanh : "");
            }
        }
        public DonGiaTieuChuan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
        }
    }
}