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

    [ModelDefault("Caption", "Bảng chốt thù lao")]
    [DefaultProperty("Caption")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = " DaTinhThuLao = 1 and Khoa = 1")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "CongTy;NamHoc;HocKy", "Bảng chốt thông tin giảng dạy đã tồn tại")]

    public class BangChotThuLao : ThongTinChungPMS
    {
        private bool _Khoa;
        private bool _DaTinhThuLao;
        private DateTime _NgayChot;

        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [ModelDefault("Caption", "Đã tính thù lao")]
        [Browsable(false)]
        public bool DaTinhThuLao
        {
            get { return _DaTinhThuLao; }
            set { SetPropertyValue("DaTinhThuLao", ref _DaTinhThuLao, value); }
        }

        [ModelDefault("Caption", "Ngày chốt")]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayChot
        {
            get { return _NgayChot; }
            set { SetPropertyValue("NgayChot", ref _NgayChot, value); }
        }

        [Aggregated]
        [Association("BangChotThuLao-ListThongTinBangChot")]
        [ModelDefault("Caption", "Chi tiết")]
        public XPCollection<ThongTinBangChot> ListThongTinBangChot
        {
            get
            {
                return GetCollection<ThongTinBangChot>("ListThongTinBangChot");
            }
        }
      

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format("{0} - Năm học  {1} {2}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");      
            }
        }


        public BangChotThuLao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NgayChot = DateTime.Now;
        }
    }
}
