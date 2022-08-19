using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.DanhMuc.NhanSu
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [DefaultProperty("TenChuongTrinh")]
    [ModelDefault("Caption", "Chương trình đào tạo")]
    [Appearance("ChuongTrinhDaoTao.VanBang", TargetItems = "LoaiChungChi", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiVanBangChungChi=1")]
    [Appearance("ChuongTrinhDaoTao.ChungChi", TargetItems = "TrinhDoChuyenMon;ChuyenMonDaoTao", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiVanBangChungChi=2")]
    public class ChuongTrinhDaoTao : BaseObject
    {
        // Fields...
        private string _MaQuanLy;      
        private string _TenChuongTrinh;
        private LoaiChuongTrinhDaoTaoEnum _LoaiChuongTrinh;
        private LoaiVanBangChungChiEnum _LoaiVanBangChungChi;
        private HinhThucDaoTao _HinhThucDaoTao;
        private ChuyenNganhDaoTao _ChuyenMonDaoTao;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private LoaiChungChi _LoaiChungChi;

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Mã quản lý")]
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

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên chương trình")]
        public string TenChuongTrinh
        {
            get
            {
                return _TenChuongTrinh;
            }
            set
            {
                SetPropertyValue("TenChuongTrinh", ref _TenChuongTrinh, value);
            }
        }

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Loại chương trình")]
        public LoaiChuongTrinhDaoTaoEnum LoaiChuongTrinh
        {
            get
            {
                return _LoaiChuongTrinh;
            }
            set
            {
                SetPropertyValue("LoaiChuongTrinh", ref _LoaiChuongTrinh, value);
            }
        }            
        
        [ModelDefault("Caption", "Loại văn bằng, chứng chỉ")]
        public LoaiVanBangChungChiEnum LoaiVanBangChungChi
        {
            get
            {
                return _LoaiVanBangChungChi;
            }
            set
            {
                SetPropertyValue("LoaiVanBangChungChi", ref _LoaiVanBangChungChi, value);
            }
        }           

        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _HinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTao", ref _HinhThucDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Trình độ đào tạo")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiVanBangChungChi=1")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiVanBangChungChi=1")]
        public ChuyenNganhDaoTao ChuyenMonDaoTao
        {
            get
            {
                return _ChuyenMonDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenMonDaoTao", ref _ChuyenMonDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Loại chứng chỉ")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LoaiVanBangChungChi=2")]
        public LoaiChungChi LoaiChungChi
        {
            get
            {
                return _LoaiChungChi;
            }
            set
            {
                SetPropertyValue("LoaiChungChi", ref _LoaiChungChi, value);
            }
        }

        public ChuongTrinhDaoTao(Session session) : base(session) { }            
    }

}
