using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenHocKy")]
    [ModelDefault("Caption", "Học kỳ")]
    [RuleCombinationOfPropertiesIsUnique("HocKy.Unique", DefaultContexts.Save, "NamHoc;MaQuanLy")]
    public class HocKy : BaseObject
    {
        private NamHoc _NamHoc;
        private DateTime _DenNgay;
        private DateTime _TuNgay;
        private DateTime _NgayMoXacNhan;
        private DateTime _NgayDongXacNhan;
        private string _MaQuanLy;
        private string _TenHocKy;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Năm học")]
        [Association("NamHoc-ListHocKy")]
        public NamHoc NamHoc
        {
            get
            {
                return _NamHoc;
            }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null && string.IsNullOrEmpty(MaQuanLy))
                {
                    int count = value.ListHocKy.Count + 1;
                    MaQuanLy = String.Format("HK0{0}", count);
                    TenHocKy = String.Format("Học kỳ {0}", count);
                }
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Tên học kỳ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenHocKy
        {
            get
            {
                return _TenHocKy;
            }
            set
            {
                SetPropertyValue("TenHocKy", ref _TenHocKy, value);
            }
        }

        [ModelDefault("Caption", "Từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ModelDefault("Caption", "Đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngày mở xác nhận")]
        public DateTime NgayMoXacNhan
        {
            get { return _NgayMoXacNhan; }
            set
            {
                SetPropertyValue("NgayMoXacNhan", ref _NgayMoXacNhan, value);
            }
        }

        [ModelDefault("Caption", "Ngày đóng xác nhận")]
        public DateTime NgayDongXacNhan
        {
            get { return _NgayDongXacNhan; }
            set
            {
                SetPropertyValue("NgayDongXacNhan", ref _NgayDongXacNhan, value);
            }
        }
        public HocKy(Session session) : base(session) { }
    }

}
