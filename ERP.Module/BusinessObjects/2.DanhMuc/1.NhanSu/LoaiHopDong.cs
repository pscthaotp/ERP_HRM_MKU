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
    [DefaultProperty("TenLoaiHopDong")]
    [ModelDefault("Caption", "Loại hợp đồng")]
    public class LoaiHopDong : BaseObject
    {
        private string _MaQuanLy;
        private string _TenLoaiHopDong;
        private decimal _CapDo;

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

        [ModelDefault("Caption", "Tên loại hợp đồng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenLoaiHopDong
        {
            get
            {
                return _TenLoaiHopDong;
            }
            set
            {
                SetPropertyValue("TenLoaiHopDong", ref _TenLoaiHopDong, value);
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

        public LoaiHopDong(Session session) : base(session) { }

    }

}
