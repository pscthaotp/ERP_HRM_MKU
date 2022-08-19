using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.System;
using DevExpress.Persistent.Base;

namespace ERP.Module.MailMerge
{    
    [DefaultProperty("TenTaiLieu")]
    [ModelDefault("Caption", "Mẫu mail merge")]
    [ModelDefault("IsCloneable", "True")]
    [RuleCombinationOfPropertiesIsUnique("MailMergeTemplate1", DefaultContexts.Save, "CongTy;MaQuanLy;SuDungMacDinh;TenTaiLieu", TargetCriteria =  "PhanHe.TenPhanHe not like '%Nhân sự%'")]
    [RuleCombinationOfPropertiesIsUnique("MailMergeTemplate2", DefaultContexts.Save, "CongTy;TenTaiLieu", TargetCriteria = "PhanHe.TenPhanHe like '%Nhân sự%'")]
    public class MailMergeTemplate : BaseObject, ICongTy
    {
        //
        private bool _SuDungMacDinh;
        private byte[] _LuuTru;
        private string _TenTaiLieu;
        private string _TenTaiLieuTam;
        private string _MaQuanLy;
        private PhanHe _PhanHe;
        private DateTime _NgayLap;
        private DateTime _HieuLucDenNgay;
        private CongTy _CongTy;

        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
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

        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên tài liệu")]        
        public string TenTaiLieuTam
        {
            get
            {
                return _TenTaiLieuTam;
            }
            set
            {
                SetPropertyValue("TenTaiLieuTam", ref _TenTaiLieuTam, value);
                if (!IsLoading && !String.IsNullOrEmpty(value) && NgayLap != DateTime.MinValue && HieuLucDenNgay != DateTime.MinValue)
                    TenTaiLieu = String.Concat(TenTaiLieuTam, " [", NgayLap.ToString("dd/MM/yyyy"), " - ", HieuLucDenNgay.ToString("dd/MM/yyyy"), "]"," [",CongTy.TenBoPhan,"]");
            }
        }

        [Size(-1)]
        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tên tài liệu (full)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenTaiLieu
        {
            get
            {
                return _TenTaiLieu;
            }
            set
            {
                SetPropertyValue("TenTaiLieu", ref _TenTaiLieu, value);               
            }
        }

        [ModelDefault("Caption", "Phân hệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public PhanHe PhanHe
        {
            get
            {
                return _PhanHe;
            }
            set
            {
                SetPropertyValue("PhanHe", ref _PhanHe, value);
            }
        }

        //[Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading && value != DateTime.MinValue && !String.IsNullOrEmpty(TenTaiLieu))
                    TenTaiLieu = String.Concat(TenTaiLieuTam, " [", NgayLap.ToString("dd/MM/yyyy"), " - ", HieuLucDenNgay.ToString("dd/MM/yyyy"), "]", " [", CongTy.TenBoPhan, "]");
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Hiệu lực đến ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime HieuLucDenNgay
        {
            get
            {
                return _HieuLucDenNgay;
            }
            set
            {
                SetPropertyValue("HieuLucDenNgay", ref _HieuLucDenNgay, value);
                if (!IsLoading && value != DateTime.MinValue && !String.IsNullOrEmpty(TenTaiLieu))
                    TenTaiLieu = String.Concat(TenTaiLieuTam, " [", NgayLap.ToString("dd/MM/yyyy"), " - ", HieuLucDenNgay.ToString("dd/MM/yyyy"), "]", " [", CongTy.TenBoPhan, "]");
            }
        }

        [ModelDefault("Caption", "Sử dụng mặc định")]
        public bool SuDungMacDinh
        {
            get
            {
                return _SuDungMacDinh;
            }
            set
            {
                SetPropertyValue("SuDungMacDinh", ref _SuDungMacDinh, value);
            }
        }


        //[Browsable(false)]
        [ModelDefault("Caption", "Lưu trữ")]
        public byte[] LuuTru
        {
            get
            {
                return _LuuTru;
            }
            set
            {
                SetPropertyValue("LuuTru", ref _LuuTru, value);
            }
        }

        public MailMergeTemplate(Session session) : base(session) { }
    }

}
