using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Editors;
using ERP.Module.PMS.BusinessObjects;

namespace ERP.Module.PMS.NghiepVu
{
    //[ModelDefault("IsCloneable", "True")]
    [DefaultProperty("Caption")]

    [ModelDefault("Caption", "Cấu hình quy đổi PMS")]

    public class CauHinhQuyDoiPMS : ThongTinChungPMS
    {
        [Association("CauHinhQuyDoiPMS-ListCongThuc")]
        [ModelDefault("Caption", "Danh sách công thức")]
        public XPCollection<ChiTietCauHinhQuyDoiPMS> ListCongThuc
        {
            get
            {
                return GetCollection<ChiTietCauHinhQuyDoiPMS>("ListCongThuc");
            }
        }

        private string ExpressionType
        {
            get
            {
                return "ERP.Module.PMS.NghiepVu.ChonGiaTriLapCongThucPMS";
            }
        }

        #region KhaiBao
        private string _CongThucTinhHeSoPMS;
        private bool _ThinhGiang;
        private bool _GDTC;
        private bool _NgungApDung;
        #endregion

        [ModelDefault("Caption", "Thỉnh giảng")]
        public bool ThinhGiang
        {
            get { return _ThinhGiang; }
            set { SetPropertyValue("ThinhGiang", ref _ThinhGiang, value); }
        }
        [ModelDefault("Caption", "Dạy GDTC")]
        public bool GDTC
        {
            get { return _GDTC; }
            set { SetPropertyValue("GDTC", ref _GDTC, value); }
        }
        [ModelDefault("Caption", "Công thức tính tổng hệ số")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.btnEdit_CongThucPMS")]
        [VisibleInListView(false)]
        public string CongThucTinhHeSoPMS
        {
            get { return _CongThucTinhHeSoPMS; }
            set { SetPropertyValue("CongThucTinhHeSoPMS", ref _CongThucTinhHeSoPMS, value); }
        }

        [ModelDefault("Caption", "Ngừng áp dụng")]
        public bool NgungApDung
        {
            get { return _NgungApDung; }
            set { SetPropertyValue("NgungApDung", ref _NgungApDung, value); }
        }      

        public CauHinhQuyDoiPMS(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }
}
