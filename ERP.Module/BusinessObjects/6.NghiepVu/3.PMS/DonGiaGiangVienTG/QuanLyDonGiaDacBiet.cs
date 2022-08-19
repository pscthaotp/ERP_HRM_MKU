using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.PMS.HeSo;

namespace ERP.Module.NghiepVu.PMS.DonGiaGiangVienTG
{
    [DefaultClassOptions]
    [ModelDefault("Caption", "Quản lý đơn giá đặc biệt")]
    [DefaultProperty("TenQuanLyHeSo")]
    public class QuanLyDonGiaDacBiet : BaseObject
    {
        private BoPhan _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;


        [ModelDefault("Caption", "Trường")]
        [VisibleInDetailView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "false")]
        public BoPhan ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }


        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết đơn giá")]
        [Association("QuanLyDonGiaDacBiet-ListDonGiaGiangVienThinhGiangDacBiet")]
        public XPCollection<DonGiaGiangVienThinhGiangDacBiet> ListDonGiaGiangVienThinhGiangDacBiet
        {
            get
            {
                return GetCollection<DonGiaGiangVienThinhGiangDacBiet>("ListDonGiaGiangVienThinhGiangDacBiet");
            }
        }
        

        public QuanLyDonGiaDacBiet(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = Common.CongTy(Session);
        }

    }
}
