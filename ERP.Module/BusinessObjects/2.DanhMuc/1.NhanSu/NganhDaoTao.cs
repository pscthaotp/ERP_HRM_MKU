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
    [ModelDefault("Caption", "Ngành đào tạo")]
    [DefaultProperty("TenNganhDaoTao")]
    public class NganhDaoTao : BaseObject
    {
        private string _MaQuanLy;
        private string _TenNganhDaoTao;

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

        [ModelDefault("Caption", "Ngành đào tạo")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNganhDaoTao
        {
            get
            {
                return _TenNganhDaoTao;
            }
            set
            {
                SetPropertyValue("TenNganhDaoTao", ref _TenNganhDaoTao, value);
            }
        }

        //[Aggregated]
        //[ModelDefault("Caption", "Danh sách chuyên ngành đào tạo")]
        //[Association("NganhDaoTao-ListChuyenNganhDaoTao")]
        //public XPCollection<ChuyenNganhDaoTao> ListChuyenNganhDaoTao
        //{
        //    get
        //    {
        //        return GetCollection<ChuyenNganhDaoTao>("ListChuyenNganhDaoTao");
        //    }
        //}

        public NganhDaoTao(Session session) : base(session) { }
    }

}
