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
    [ModelDefault("Caption", "Dịnh mức cố vấn học tập")]
    public class DinhMucCoVanHocTap : BaseObject
    {      
        private BacDaoTao _BacDaoTao;
        private HeDaoTao_PMS _HeDaoTao_PMS;
        private decimal _HeSoCVHT;      
        
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

        [ModelDefault("Caption", "Định mức CVHT")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal HeSoCVHT
        {
            get { return _HeSoCVHT; }
            set { SetPropertyValue("HeSoCVHT", ref _HeSoCVHT, value); }
        }

        public DinhMucCoVanHocTap(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}