using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Data.Filtering;
using System.ComponentModel;

namespace ERP.Module.PMS.BusinessObjects
{
    [ModelDefault ("Caption", "Thông tin chung PMS")]
    public class ThongTinChungPMS : BaseObject, ICongTy
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        [ModelDefault("Caption", "Trường")]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { SetPropertyValue("CongTy", ref _CongTy, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        //[VisibleInListView(false)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading)
                {
                   updateHocKyList();
                   HocKy = null;
                }
            }
        }
        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("HocKyList", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
                if (value != null)
                {
                    //ThuHuong tắt code 
                    //AfterLoadDotTinhChanged();
                }
            }
        }

        [Browsable(false)]
        public XPCollection<HocKy> HocKyList { get; set; }

       
        public ThongTinChungPMS(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
            CongTy = Commons.Common.CongTy(Session);
            NamHoc = Commons.Common.GetCurrentNamHoc(Session);
        }
        public void updateHocKyList()
        {
            if (HocKyList != null)
            {
                HocKyList.Reload();
            }
            else
            {
                HocKyList = new XPCollection<HocKy>(Session, false);
            }
            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            XPCollection<HocKy> ds = new XPCollection<HocKy>(Session, filter);
            foreach (HocKy item in ds)
            {
                HocKyList.Add(item);
            }
           
            //HocKyList.Criteria = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            //SortingCollection sortHK = new SortingCollection();
            //sortHK.Add(new SortProperty("TuNgay", DevExpress.Xpo.DB.SortingDirection.Ascending));
            //HocKyList.Sorting = sortHK;
            //OnChanged("HocKyList");
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (NamHoc != null)
                updateHocKyList();
        }

        //protected virtual void AfterLoadDotTinhChanged()
        //{
        //}
    }

}