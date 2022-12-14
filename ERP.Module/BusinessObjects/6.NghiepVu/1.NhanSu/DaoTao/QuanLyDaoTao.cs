using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Enum.NhanSu;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NghiepVu.NhanSu.DaoTao
{
    [DefaultClassOptions]
    [ImageName("BO_QuanLyDaoTao")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Quản lý đào tạo")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "CongTy;NienDoTaiChinh")]
    //[Appearance("QuanLyTuyenDung", TargetItems = "NamHoc", Enabled = false, Criteria = "NamHoc is not null")]

    public class QuanLyDaoTao : BaseObject, ICongTy
    {     
        private NienDoTaiChinh _NienDoTaiChinh;       
        private CongTy _CongTy;        

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    NienDoTaiChinh = null;
                    UpdateNienDoTaiChinh();
                }
            }
        }

        [ModelDefault("Caption", "Niên độ")]
        [DataSourceProperty("NienDoTaiChinhList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NienDoTaiChinh NienDoTaiChinh
        {
            get
            {
                return _NienDoTaiChinh;
            }
            set
            {
                SetPropertyValue("NienDoTaiChinh", ref _NienDoTaiChinh, value);
            }
        }        

        [Aggregated]
        [ModelDefault("Caption", "Đăng ký đào tạo")]
        [Association("QuanLyDaoTao-ListDangKyDaoTao")]
        public XPCollection<DangKyDaoTao> ListDangKyDaoTao
        {
            get
            {
                return GetCollection<DangKyDaoTao>("ListDangKyDaoTao");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Duyệt đăng ký đào tạo")]
        [Association("QuanLyDaoTao-ListDuyetDangKyDaoTao")]
        public XPCollection<DuyetDangKyDaoTao> ListDuyetDangKyDaoTao
        {
            get
            {
                return GetCollection<DuyetDangKyDaoTao>("ListDuyetDangKyDaoTao");
            }
        }

        [Browsable(false)]
        public XPCollection<NienDoTaiChinh> NienDoTaiChinhList { get; set; }

        public QuanLyDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected void UpdateNienDoTaiChinh()
        {
            if (NienDoTaiChinhList == null)
                NienDoTaiChinhList = new XPCollection<NienDoTaiChinh>(Session);

            if (CongTy != null)
                NienDoTaiChinhList.Criteria = CriteriaOperator.Parse("CongTy.Oid=?", this.CongTy.Oid);
            else
                NienDoTaiChinhList = null;
        }          
    }
}
