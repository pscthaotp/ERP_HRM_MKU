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
    [ModelDefault("Caption", "Chuyên ngành đào tạo")]
    [DefaultProperty("TenChuyenNganhDaoTao")]
    public class ChuyenNganhDaoTao : BaseObject
    {
        private string _MaQuanLy;
        private string _TenChuyenNganhDaoTao;
        private NganhDaoTao _NganhDaoTao;

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

        [ModelDefault("Caption", "Tên chuyên ngành đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChuyenNganhDaoTao
        {
            get
            {
                return _TenChuyenNganhDaoTao;
            }
            set
            {
                SetPropertyValue("TenChuyenNganhDaoTao", ref _TenChuyenNganhDaoTao, value);
            }
        }

        //[ModelDefault("Caption", "Ngành đào tạo")]
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
  
        public ChuyenNganhDaoTao(Session session) : base(session) { }
    }

}
