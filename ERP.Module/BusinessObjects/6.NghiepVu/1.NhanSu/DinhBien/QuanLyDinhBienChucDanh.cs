using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.NghiepVu.NhanSu.DinhBien
{
    [DefaultClassOptions]
    [ImageName("BO_DinhBien")]
    [ModelDefault("Caption", "Quản lý định biên chức danh")]
    [RuleCombinationOfPropertiesIsUnique("QuanLyDinhBienChucDanh.Unique", DefaultContexts.Save, "CongTy;NienDoTaiChinh")]
    public class QuanLyDinhBienChucDanh : BaseObject, ICongTy
    {
        private CongTy _CongTy;
        private NienDoTaiChinh _NienDoTaiChinh;

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Trường")]      
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
        [ModelDefault("Caption", "Danh sách định biên")]
        [Association("QuanLyDinhBienChucDanh-ListDinhBienChucDanh")]
        public XPCollection<DinhBienChucDanh> ListDinhBienChucDanh
        {
            get
            {
                return GetCollection<DinhBienChucDanh>("ListDinhBienChucDanh");
            }
        }

        [Browsable(false)]
        public XPCollection<NienDoTaiChinh> NienDoTaiChinhList { get; set; }

        public QuanLyDinhBienChucDanh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //            
            CongTy = Common.CongTy(Session);
            //
            UpdateNienDoTaiChinh();
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
