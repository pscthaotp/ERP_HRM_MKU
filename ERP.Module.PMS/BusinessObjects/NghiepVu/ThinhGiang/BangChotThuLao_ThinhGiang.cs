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
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.PMS.DanhMuc;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Bảng chốt thù lao(thỉnh giảng)")]
    [DefaultProperty("Caption")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1 and DaTinhThuLao = 1")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "CongTy;NamHoc;DotTinh", "Bảng chốt thông tin giảng dạy đã tồn tại")]
    public class BangChotThuLao_ThinhGiang : BaseObject
    {
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private CongTy _CongTy;
        private bool _Khoa;
        private bool _DaTinhThuLao;
        private DateTime _NgayChot;
        private DotTinhPMS _DotTinh; 


        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { SetPropertyValue("CongTy", ref _CongTy, value); }
        }

        [ModelDefault("Caption", "Năm học")]
        [VisibleInListView(false)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    HocKy = null;
                    //DotTinh = null; //ThuHuong tắt code không dùng
                    UpdateHocKy();
                    DotTinh = null;
                    UpdateDotTinh();
                }
            }
        }
          [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [ModelDefault("Caption", "Học kỳ")]
        //[DataSourceProperty("listHocKy")]  Đông tắt
        [VisibleInListView(false)]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);               
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [ModelDefault("Caption", "Đã tính thù lao")]
        [ModelDefault("AllowEdit","false")]
        [Browsable(false)]
        public bool DaTinhThuLao
        {
            get { return _DaTinhThuLao; }
            set { SetPropertyValue("DaTinhThuLao", ref _DaTinhThuLao, value); }
        }
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Đợt tính PMS")]
        [DataSourceProperty("listDotTinhPMS", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public DotTinhPMS DotTinh
        {
            get { return _DotTinh; }
            set { SetPropertyValue("DotTinh", ref _DotTinh, value); }
        }

        [ModelDefault("Caption", "Ngày chốt")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public DateTime NgayChot
        {
            get { return _NgayChot; }
            set { SetPropertyValue("NgayChot", ref _NgayChot, value); }
        }

        [Aggregated]
        [Association("BangChotThuLao_ThinhGiang-ListThongTinBangChotThuLao")]
        [ModelDefault("Caption", "Chi tiết thù lao")]
        public XPCollection<ThongTinBangChot> ListThongTinBangChotThuLao
        {
            get
            {
                return GetCollection<ThongTinBangChot>("ListThongTinBangChotThuLao");
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        public XPCollection<HocKy> listHocKy
        {
            get;
            set;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Đợt tính List")]
        public XPCollection<DotTinhPMS> listDotTinhPMS
        {
            get;
            set;
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                //return String.Format("{0} - Năm học  {1} {2}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");
                return String.Format("{0} - Năm học  {1} {2} {3}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "", DotTinh != null ? " - " + DotTinh.TenKy : "");      
            
            }
        }

        private bool _ThinhGiang = true;
        [NonPersistent]
        [Browsable(false)]
        public bool ThinhGiang
        {
            get { return _ThinhGiang; }
            set
            {
                SetPropertyValue("ThinhGiang", ref _ThinhGiang, value);
            }
        }


        public BangChotThuLao_ThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            NgayChot = DateTime.Now;
        }

        public void UpdateHocKy()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            XPCollection<HocKy> DS_HocKy = new XPCollection<HocKy>(Session, filter);
            if (listHocKy != null)
            {
                listHocKy.Reload();
            }
            else
            {
                listHocKy = new XPCollection<HocKy>(Session, false);
            }
            foreach (HocKy item in DS_HocKy)
            {
                listHocKy.Add(item);
            }
            OnChanged("listHocKy");
        }

        public void UpdateDotTinh()
        {
            //if (HocKy != null)
            //{
            //    if (listDotTinhPMS != null)
            //    {
            //        listDotTinhPMS.Reload();
            //    } 
            //    else 
            //    {
            //        listDotTinhPMS = new XPCollection<DotTinhPMS>(Session, false);
            //    }
            //    CriteriaOperator filter = CriteriaOperator.Parse("HocKy=?", HocKy.Oid); //Học kỳ này đã chọn theo  NamHoc 
            //    XPCollection<DotTinhPMS> DS_List = new XPCollection<DotTinhPMS>(Session, filter);
                
            //    foreach (DotTinhPMS item in DS_List)
            //    {
            //            listDotTinhPMS.Add(item);
            //    }
            
            //}
            //ThuHuong sửa 
            if (NamHoc != null)
            {
                if (listDotTinhPMS != null)
                {
                    listDotTinhPMS.Reload();
                }
                else
                {
                    listDotTinhPMS = new XPCollection<DotTinhPMS>(Session, false);
                }
                CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=?", NamHoc.Oid); //Lấy toàn bộ đợt tinh theo NamHoc 
                XPCollection<DotTinhPMS> DS_List = new XPCollection<DotTinhPMS>(Session, filter);

                foreach (DotTinhPMS item in DS_List)
                {
                    listDotTinhPMS.Add(item);
                }

            }
        }
      
    }
}
