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
    [ImageName("BO_TrinhDoChuyenMon")]
    [DefaultProperty("TenTrinhDoChuyenMon")]
    [ModelDefault("Caption", "Trình độ chuyên môn")]
    public class TrinhDoChuyenMon : BaseObject
    {
        private string _MaQuanLy;
        private string _TenTrinhDoChuyenMon;
        private decimal _CapDo;
        //
        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên trình độ chuyên môn")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTrinhDoChuyenMon
        {
            get
            {
                return _TenTrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TenTrinhDoChuyenMon", ref _TenTrinhDoChuyenMon, value);
            }
        }
        [ModelDefault("Caption", "Cấp độ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal CapDo
        {
            get
            {
                return _CapDo;
            }
            set
            {
                SetPropertyValue("CapDo", ref _CapDo, value);
            }
        }

        public TrinhDoChuyenMon(Session session) : base(session) { }
    }
}
