using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChiPhiTienLuong")]
    [ModelDefault("Caption", "Chi phí tiền lương")]
    public class ChiPhiTienLuong : BaseObject
    {
        private string _CostCenter;
        private string _TenChiPhiTienLuong;
        private bool _NgoaiKhoa;       

        [ModelDefault("Caption", "Mã phân bổ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue("", DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Tên chi phí tiền lương")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenChiPhiTienLuong
        {
            get
            {
                return _TenChiPhiTienLuong;
            }
            set
            {
                SetPropertyValue("TenChiPhiTienLuong", ref _TenChiPhiTienLuong, value);
            }
        }

        [ModelDefault("Caption", "Chi phí ngoại khóa")]      
        public bool NgoaiKhoa
        {
            get
            {
                return _NgoaiKhoa;
            }
            set
            {
                SetPropertyValue("NgoaiKhoa", ref _NgoaiKhoa, value);
            }
        }
        public ChiPhiTienLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }
    }

}
