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
    [DefaultProperty("TenMonHoc")]
    [ModelDefault("Caption", "Môn học")]    
    //[RuleCombinationOfPropertiesIsUnique("Mã quản lý, Tên tổ bị trùng", DefaultContexts.Save, "TenTo;MaQuanLy")]
    public class MonHoc : BaseObject
    {
        private decimal _STT;
        private string _MaQuanLy;
        private string _TenMonHoc;
        //private string _CostCenter;
        //private LoaiBoPhanEnum _LoaiBoPhan = LoaiBoPhanEnum.BoMon;

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

        //[ImmediatePostData]
        [ModelDefault("Caption", "Tên Môn học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenMonHoc
        {
            get
            {
                return _TenMonHoc;
            }
            set
            {
                SetPropertyValue("TenMonHoc", ref _TenMonHoc, value);                
            }
        }

        //[ModelDefault("Caption", "Mã phân bổ")]
        //public string CostCenter
        //{
        //    get
        //    {
        //        return _CostCenter;
        //    }
        //    set
        //    {
        //        SetPropertyValue("CostCenter", ref _CostCenter, value);
        //    }
        //}

        //[ImmediatePostData]
        //[ModelDefault("Caption", "Loại đơn vị")]
        //[ModelDefault("AllowEdit", "False")]
        //public LoaiBoPhanEnum LoaiBoPhan
        //{
        //    get
        //    {
        //        return _LoaiBoPhan;
        //    }
        //    set
        //    {
        //        SetPropertyValue("LoaiBoPhan", ref _LoaiBoPhan, value);
        //    }
        //}        

        public MonHoc(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

    }
}
