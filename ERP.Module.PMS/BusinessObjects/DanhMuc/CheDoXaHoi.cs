using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Chế độ xã hội")]
    [DefaultProperty("TenCheDo")]
    public class CheDoXaHoi : BaseObject
    {
        private string _MaQuanLy;
        private string _TenCheDo;
        private decimal _PhanTramGiamTru;
        private decimal _PhanTramGiamTru_NCKH;
        private decimal _PhanTramGiamTru_HDQL;
        private string _GhiChu;
        private bool _TinhTheoThang;
        //private bool _An;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleRequiredField(DefaultContexts.Save)]
        [VisibleInListView(false)]
        public string MaQuanLy 
        {
            get { return _MaQuanLy; }
            set { SetPropertyValue("MaQuanLy", ref _MaQuanLy, value); }
        }

        [ModelDefault("Caption", "Tên chế độ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenCheDo
        {
            get { return _TenCheDo; }
            set { SetPropertyValue("TenCheDo", ref _TenCheDo, value); }
        }

        [ModelDefault("Caption", "Giảm trừ giờ chuẩn(%)")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PhanTramGiamTru
        {
            get { return _PhanTramGiamTru; }
            set { SetPropertyValue("PhanTramGiamTru", ref _PhanTramGiamTru, value); }
        }

        [ModelDefault("Caption", "Giảm trừ NCKH (%)")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PhanTramGiamTru_NCKH
        {
            get { return _PhanTramGiamTru_NCKH; }
            set { SetPropertyValue("PhanTramGiamTru_NCKH", ref _PhanTramGiamTru_NCKH, value); }
        }

        [ModelDefault("Caption", "Giảm trừ HDQL(%)")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal PhanTramGiamTru_HDQL
        {
            get { return _PhanTramGiamTru_HDQL; }
            set { SetPropertyValue("PhanTramGiamTru_HDQL", ref _PhanTramGiamTru_HDQL, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Tính theo mốc thời gian")]
        public bool TinhTheoThang
        {
            get { return _TinhTheoThang; }
            set { SetPropertyValue("TinhTheoThang", ref _TinhTheoThang, value); }
        }

        //[ModelDefault("Caption", "Ẩn/Hiện")]
        //[NonPersistent]
        //[Browsable(false)]
        //[ImmediatePostData]
        //public bool An
        //{
        //    get { return _An; }
        //    set { SetPropertyValue("An", ref _An, value); }
        //}

        public CheDoXaHoi(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }

}
