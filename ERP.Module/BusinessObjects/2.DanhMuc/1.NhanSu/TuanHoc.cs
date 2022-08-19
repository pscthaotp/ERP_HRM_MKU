using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenTuan")]
    [ModelDefault("Caption", "Tuần học")]
    [Appearance("TuanHoc.Enabled", TargetItems = "*", Enabled = false)]
    public class TuanHoc : BaseObject
    {
        private NamHoc _NamHoc;
        private int _SoThuTu;
        private string _TenTuan;
        private DateTime _TuNgay;
        private DateTime _DenNgay;

        public TuanHoc(Session session) : base(session) { }

        [Browsable(false)]
        [ModelDefault("Caption", "Năm học")]
        [Association("NamHoc-ListTuanHoc")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[RuleUniqueValue("", DefaultContexts.Save)]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
                if(!IsLoading)
                { TenTuan = "Tuần" + SoThuTu; }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên tuần")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTuan
        {
            get
            {
                return _TenTuan;
            }
            set
            {
                SetPropertyValue("TenTuan", ref _TenTuan, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        //[RuleRequiredField(DefaultContexts.Save)]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
    }
}
