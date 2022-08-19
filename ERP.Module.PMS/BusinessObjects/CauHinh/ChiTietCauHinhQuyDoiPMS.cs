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
using ERP.Module.PMS.DanhMuc;


namespace ERP.Module.PMS.NghiepVu
{
    //[ModelDefault("IsCloneable", "True")]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Chi tiết cấu hình quy đổi PMS")]
    public class ChiTietCauHinhQuyDoiPMS : BaseObject
    {
        private CauHinhQuyDoiPMS _CauHinhQuyDoiPMS;
        [Association("CauHinhQuyDoiPMS-ListCongThuc")]
        [Browsable(false)]
        public CauHinhQuyDoiPMS CauHinhQuyDoiPMS
        {
            get
            {
                return _CauHinhQuyDoiPMS;
            }
            set
            {
                SetPropertyValue("CauHinhQuyDoiPMS", ref _CauHinhQuyDoiPMS, value);
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
        private string _MaQuanLy;
        private string _LoaiCongThuc;
        private LoaiHocPhan _LoaiHocPhan;
        private NgonNguGiangDay _NgonNguGiangDay;

        private string _CongThuc;
        private bool _KhongNhanTongHeSo;
        private bool _NgungApDung;
        #endregion

        [ModelDefault("Caption", "Diễn giải")]
        public string LoaiCongThuc
        {
            get { return _LoaiCongThuc; }
            set { SetPropertyValue("LoaiCongThuc", ref _LoaiCongThuc, value); }
        }
        [ModelDefault("Caption", "Mã quản lý")]
        public string MaQuanLy
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }
        [ModelDefault("Caption", "Loại học phần")]
        public LoaiHocPhan LoaiHocPhan
        {
            get { return _LoaiHocPhan; }
            set { SetPropertyValue("LoaiHocPhan", ref _LoaiHocPhan, value); }
        }
        [ModelDefault("Caption", "Ngôn ngữ giảng dạy")]
        public NgonNguGiangDay NgonNguGiangDay
        {
            get { return _NgonNguGiangDay; }
            set { SetPropertyValue("NgonNguGiangDay", ref _NgonNguGiangDay, value); }
        }
        [ModelDefault("Caption", "Công thức")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.btnEdit_CongThucPMS")]
        [VisibleInListView(false)]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string CongThuc
        {
            get { return _CongThuc; }
            set { SetPropertyValue("CongThuc", ref _CongThuc, value); }
        }

        [ModelDefault("Caption", "Không áp dụng tổng hệ số")]
        public bool KhongNhanTongHeSo
        {
            get { return _KhongNhanTongHeSo; }
            set { SetPropertyValue("KhongNhanTongHeSo", ref _KhongNhanTongHeSo, value); }
        }

        [ModelDefault("Caption", "Ngừng áp dụng")]
        public bool NgungApDung
        {
            get { return _NgungApDung; }
            set { SetPropertyValue("NgungApDung", ref _NgungApDung, value); }
        }

        public ChiTietCauHinhQuyDoiPMS(Session session) : base(session) { }

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
