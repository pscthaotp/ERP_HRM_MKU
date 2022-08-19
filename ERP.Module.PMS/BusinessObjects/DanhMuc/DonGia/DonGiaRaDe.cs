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
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.PMS.Enum;

namespace ERP.Module.PMS.DonGia
{
    [ModelDefault("Caption", "Đơn giá ra đề")]
     public class DonGiaRaDe : BaseObject
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private LoaiGiangVienEnum? _LoaiGiangVien;
        private LoaiHocPhan _LoaiHocPhan;
        private string _TenDonGia;
        //private int _SoLuong; //Hiện tại không sử dụng _ ThuHuong
        private decimal _SoLuongBoDe; 
        private decimal _DonGia;
        private bool _NgungApDung;

        //
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

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum? LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set { SetPropertyValue("LoaiGiangVien", ref _LoaiGiangVien, value); }
        }
        [ModelDefault("Caption", "Tên đơn giá")]
        //[VisibleInListView(false)]
        public string TenDonGia
        {
            get { return _TenDonGia; }
            set
            {
                SetPropertyValue("TenDonGia ", ref _TenDonGia, value);
            }
        }

        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhan LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set
            {
                SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value);
            }
        }

        //[ModelDefault("Caption", "Số lượng")]
        //[ModelDefault("DisplayFormat", "N0")]
        //[ModelDefault("EditMask", "N0")]
        //public int SoLuong
        //{
        //    get { return _SoLuong; }
        //    set
        //    {
        //        SetPropertyValue("SoLuong", ref _SoLuong, value);
        //    }
        //}

        [ModelDefault("Caption", "Bộ đề")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SoLuongBoDe
        {
            get { return _SoLuongBoDe; }
            set
            {
                SetPropertyValue("SoLuongBoDe", ref _SoLuongBoDe, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá DL")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [RuleRange("DonGiaRaDe", DefaultContexts.Save, 0.00, 1000000000, "Đơn giá > 0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set
            {
                SetPropertyValue("DonGia ", ref _DonGia, value);
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

        public DonGiaRaDe(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
        }
    }
}