using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NonPersistentObjects
{
    [ModelDefault("Caption", "Thông tin trường")]
    [NonPersistent]
    public class ThongTinClone : BaseObject
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public CongTy CongTy
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Năm học(Copy)")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);               
            }
        }

        [ModelDefault("Caption", "Học kỳ(Copy)")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public HocKy HocKy
        {
            get
            {
                return _HocKy;
            }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
            }
        }

        public ThongTinClone(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.

        }
    }
}
