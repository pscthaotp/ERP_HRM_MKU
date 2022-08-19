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
    [ModelDefault("Caption", "Quản lý đào tạo (thỉnh giảng)")]
    [DefaultProperty("Caption")]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null or Khoa = 1")]
    public class KhoiLuongGiangDay_ThinhGiang : ThongTinChungPMS
    {
        private bool _Khoa;
        private BangChotThuLao_ThinhGiang _BangChotThuLao;
        private DotTinhPMS _DotTinh;

        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        [VisibleInListView(false)]
        public BangChotThuLao_ThinhGiang BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
                if (BangChotThuLao != null)
                    Khoa = true;
                else
                    Khoa = false;
            }
        }

        [ModelDefault("Caption", "Đợt tính(Đồng bộ UIS)")]
        //[DataSourceProperty("DotTinhPMSList")]//Thực hiện lấy đợt tính không theo HocKy 
        [VisibleInListView(false)]//false
        public DotTinhPMS DotTinh
        {
            get { return _DotTinh; }
            set { SetPropertyValue("DotTinh", ref _DotTinh, value); }
        }
      
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        [Browsable(false)]
        //[NonPersistent]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [Aggregated]
        [Association("KhoiLuongGiangDay_ThinhGiang-ListChiTietKhoiLuongGiangDay_ThinhGiang")]
        [ModelDefault("Caption", "Chi tiết đào tạo")]
        public XPCollection<ChiTietKhoiLuongGiangDay> ListChiTietKhoiLuongGiangDay_ThinhGiang
        {
            get
            {
                return GetCollection<ChiTietKhoiLuongGiangDay>("ListChiTietKhoiLuongGiangDay_ThinhGiang");
            }
        }
        [Aggregated]
        [Association("KhoiLuongGiangDay_ThinhGiang-ListChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang")]
        [ModelDefault("Caption", "Chi tiết khóa luận - đồ án - chuyên đề (thỉnh giảng)")]
        public XPCollection<ChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang> ListChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang
        {
            get
            {
                return GetCollection<ChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang>("ListChiTietKhoaLuan_DoAn_ChuyenDe_ThinhGiang");
            }
        }
        
        //[Browsable(false)]
        //[ModelDefault("Caption", "Đợt PMS List")]
        //public XPCollection<DotTinhPMS> DotTinhPMSList
        //{
        //    get;
        //    set;
        //}

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
            //    return String.Format(" {0} {1} {2} {3}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.MaQuanLy : "", DotTinh != null ? " - Đợt " + DotTinh.Dot.ToString() : "");
                return String.Format(" {0} {1} {2}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.MaQuanLy : "");
            }
        }
        public KhoiLuongGiangDay_ThinhGiang(Session session) : base(session) { }
      
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //DotTinhPMSList = new XPCollection<DotTinhPMS>(Session, false);
            //UpdateDotTinh();
        }

        //public void UpdateDotTinh()
        //{
        //    if (HocKy != null)
        //    {
        //        CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ? and HocKy=?",NamHoc.Oid, HocKy.Oid);
        //        if (DotTinhPMSList != null)
        //        {
        //            DotTinhPMSList.Reload();

        //            XPCollection<DotTinhPMS> DS_List = new XPCollection<DotTinhPMS>(Session, filter);
        //            foreach (DotTinhPMS item in DS_List)
        //            {
        //                DotTinhPMSList.Add(item);
        //            }
        //        }
        //    }      
        //}
        //protected override void AfterLoadDotTinhChanged()
        //{
        //    UpdateDotTinh();
        //}
       
    }
}
