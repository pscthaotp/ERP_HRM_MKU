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

    [ModelDefault("Caption", "Quản lý đào tạo (Chính quy)")]
    [DefaultProperty("Caption")]
    [Appearance("KhoiLuongGiangDay_Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null or Khoa = 1")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "CongTy;NamHoc;HocKy", "Khối lượng giảng dạy đã tồn tại")]
    public class KhoiLuongGiangDay : ThongTinChungPMS
    {
        private bool _Khoa;
        private BangChotThuLao _BangChotThuLao;
        private BacDaoTao _BacDaoTao;

        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [VisibleInListView(false)]
        public BangChotThuLao BangChotThuLao
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
       
        [ModelDefault("Caption", "Bậc đào tạo")]
        [VisibleInListView(false)]
        //[DataSourceProperty("listBacDaoTao")]
        public BacDaoTao BacDaoTao
        {
            get { return _BacDaoTao; }
            set { SetPropertyValue("BacDaoTao", ref _BacDaoTao, value); }
        }
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        //[NonPersistent]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }

        [Aggregated]
        [Association("KhoiLuongGiangDay-ListChiTiet")]
        [ModelDefault("Caption", "Chi tiết khối lượng giảng dạy")]
        public XPCollection<ChiTietKhoiLuongGiangDay> ListChiTiet
        {
            get
            {
                return GetCollection<ChiTietKhoiLuongGiangDay>("ListChiTiet");
            }
        }

        [Aggregated]
        [Association("KhoiLuongGiangDay-ListChiTietKhoaLuan_DoAn_ChuyenDe")]
        [ModelDefault("Caption", "Chi tiết khóa luận - đồ án - chuyên đề")]
        public XPCollection<ChiTietKhoaLuan_DoAn_ChuyenDe> ListChiTietKhoaLuan_DoAn_ChuyenDe
        {
            get
            {
                return GetCollection<ChiTietKhoaLuan_DoAn_ChuyenDe>("ListChiTietKhoaLuan_DoAn_ChuyenDe");
            }
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                return String.Format(" {0} {1}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? " - Năm học " + NamHoc.TenNamHoc : "");
            }
        }
        public KhoiLuongGiangDay(Session session) : base(session) { }
      
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
