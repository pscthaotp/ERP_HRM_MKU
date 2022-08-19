using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.TienLuong;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenNhomPhanBo")]
    [ModelDefault("Caption", "Nhóm phân bổ")]
    public class NhomPhanBo : BaseObject
    {
        private string _CostCenter;
        private string _MaQuanLyUIS;
        private string _TenNhomPhanBo;
        private LoaiNhomPhanBoEnum _LoaiNhomPhanBo;

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

        [ModelDefault("Caption", "Mã quản lý UIS")]    
        public string MaQuanLyUIS
        {
            get
            {
                return _MaQuanLyUIS;
            }
            set
            {
                SetPropertyValue("MaQuanLyUIS", ref _MaQuanLyUIS, value);
            }
        }

        [ModelDefault("Caption", "Tên nhóm phân bổ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenNhomPhanBo
        {
            get
            {
                return _TenNhomPhanBo;
            }
            set
            {
                SetPropertyValue("TenChiPhiTienLuong", ref _TenNhomPhanBo, value);
            }
        }

        [ModelDefault("Caption", "Loại nhóm phân bổ")]       
        public LoaiNhomPhanBoEnum LoaiNhomPhanBo
        {
            get
            {
                return _LoaiNhomPhanBo;
            }
            set
            {
                SetPropertyValue("LoaiNhomPhanBo", ref _LoaiNhomPhanBo, value);
            }
        }

        public NhomPhanBo(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            LoaiNhomPhanBo = LoaiNhomPhanBoEnum.TongCongTy;
        }
    }

}
