using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_Category")]
    [DefaultProperty("TenTo")]
    [ModelDefault("Caption", "Tổ")]    
    [RuleCombinationOfPropertiesIsUnique("Mã quản lý, Tên tổ bị trùng", DefaultContexts.Save, "TenTo;MaQuanLy")]
    public class To : BaseObject
    {
        private decimal _STT;
        private string _MaQuanLy;
        private string _CostCenter;
        private string _TenTo;
        private LoaiBoPhanEnum _LoaiBoPhan = LoaiBoPhanEnum.BoMon;
        
        [ModelDefault("Caption", "Số thứ tự")]
        [ModelDefault("EditMask", "N3")]
        [ModelDefault("DisplayFormat", "N3")]
        public decimal STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

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

        [ModelDefault("Caption", "Mã phân bổ")]
        public string CostCenter
        {
            get
            {
                return _CostCenter;
            }
            set
            {
                SetPropertyValue("CostCenter", ref _CostCenter, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tên Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTo
        {
            get
            {
                return _TenTo;
            }
            set
            {
                SetPropertyValue("TenTo", ref _TenTo, value);                
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Loại đơn vị")]
        [ModelDefault("AllowEdit", "False")]
        public LoaiBoPhanEnum LoaiBoPhan
        {
            get
            {
                return _LoaiBoPhan;
            }
            set
            {
                SetPropertyValue("LoaiBoPhan", ref _LoaiBoPhan, value);
            }
        }        

        public To(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
