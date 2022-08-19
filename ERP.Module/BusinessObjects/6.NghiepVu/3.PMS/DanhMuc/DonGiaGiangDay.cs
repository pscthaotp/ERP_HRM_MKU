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

namespace ERP.Module.NghiepVu.PMS.DanhMuc
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("LoaiDonGia")]
    [ModelDefault("Caption", "Đơn giá giảng dạy")]
    public class DonGiaGiangDay : BaseObject
    {
        private BoPhan _ThongTinTruong;
        private NamHoc _NamHoc;
        //private HocKy _HocKy;

        private DonGiaGiangDayEnum _LoaiDonGia;
        private decimal _DonGia;

        [ModelDefault("Caption", "Trường")]
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
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        //[ModelDefault("Caption", "Học kỳ")]
        //[DataSourceProperty("NamHoc.ListHocKy")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInListView(false)]
        //public HocKy HocKy
        //{
        //    get { return _HocKy; }
        //    set { SetPropertyValue("HocKy", ref _HocKy, value); }
        //}

        [ModelDefault("Caption", "Loại đơn giá")]
        public DonGiaGiangDayEnum LoaiDonGia
        {
            get
            {
                return _LoaiDonGia;
            }
            set
            {
                SetPropertyValue("LoaiDonGia", ref _LoaiDonGia, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }
        public DonGiaGiangDay(Session session)
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
