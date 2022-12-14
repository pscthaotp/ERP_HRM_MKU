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
    [ModelDefault("Caption", "Chuyên môn đào tạo")]
    [DefaultProperty("TenChuyenMonDaoTao")]
    public class ChuyenMonDaoTao : BaseObject
    {
        private NganhDaoTao _NganhDaoTao;
        private string _MaQuanLy;
        private string _TenChuyenMonDaoTao;

        [ModelDefault("Caption", "Mã quản lý")]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên chuyên môn đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChuyenMonDaoTao
        {
            get
            {
                return _TenChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("TenChuyenMonDaoTao", ref _TenChuyenMonDaoTao, value);
            }
        }

        //[ModelDefault("Caption", "Ngành đào tạo")]
        ////[RuleRequiredField("", DefaultContexts.Save)]
        //[Association("NganhDaoTao-ListChuyenNganhDaoTao")]
        //public NganhDaoTao NganhDaoTao
        //{
        //    get
        //    {
        //        return _NganhDaoTao;
        //    }
        //    set
        //    {
        //        SetPropertyValue("NganhDaoTao", ref _NganhDaoTao, value);
        //    }
        //}

        public ChuyenMonDaoTao(Session session) : base(session) { }
    }

}
