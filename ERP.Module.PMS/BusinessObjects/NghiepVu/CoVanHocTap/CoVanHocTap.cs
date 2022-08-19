using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.PMS.BusinessObjects;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.PMS.DanhMuc;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Cố vấn học tập")]
    [Appearance("Hide_CoVanHocTap", TargetItems = "Caption", Visibility = ViewItemVisibility.Hide)]
    public class CoVanHocTap : BaseObject
    {
        private QuanLyCoVanHocTap _QuanLyCoVanHocTap;
        //
        private HocKy _HocKy;
        private NhanVien _NhanVien;
        private string _Lop;
        private decimal _SiSo; 
        private decimal _HeSoCVHT; 
        private string _GhiChu;
        private BacDaoTao _BacDaoTao;
        private HeDaoTao_PMS _HeDaoTao_PMS;

        [Association("QuanLyCoVanHocTap-ListCoVanHocTap")]
        [ModelDefault("Caption", "Quản lý cố vấn học tập")]
        [Browsable(false)]
        public QuanLyCoVanHocTap QuanLyCoVanHocTap
        {
            get
            {
                return _QuanLyCoVanHocTap;
            }
            set
            {
                SetPropertyValue("QuanLyCoVanHocTap", ref _QuanLyCoVanHocTap, value);
            }
        }
        //
        [ModelDefault("Caption", "Học kỳ")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [ModelDefault("Caption", "Giảng viên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Lớp")]
        public string Lop
        {
            get { return _Lop; }
            set { SetPropertyValue("Lop", ref _Lop, value); }
        }
        [ModelDefault("Caption", "Sỉ số")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal SiSo
        {
            get { return _SiSo; }
            set { SetPropertyValue("SiSo", ref _SiSo, value); }
        }
        [ModelDefault("Caption", "Hệ số CVHT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoCVHT
        {
            get { return _HeSoCVHT; }
            set { SetPropertyValue("HeSoCVHT", ref _HeSoCVHT, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        public HeDaoTao_PMS HeDaoTao_PMS
        {
            get { return _HeDaoTao_PMS; }
            set { SetPropertyValue("HeDaoTao_PMS", ref _HeDaoTao_PMS, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [ImmediatePostData]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public CoVanHocTap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}