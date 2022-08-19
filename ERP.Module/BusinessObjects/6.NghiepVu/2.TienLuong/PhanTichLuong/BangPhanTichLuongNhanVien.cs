using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.TienLuong.Luong
{
    [DefaultClassOptions]
    [ImageName("BO_BangLuong")] 
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("Caption", "Bảng lương phân tích lương nhân viên")]
    [Appearance("BangPhanTichLuongNhanVien.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "TrangThai is not null")]
    [RuleCombinationOfPropertiesIsUnique("BangPhanTichLuongNhanVien.Unique", DefaultContexts.Save, "KyTinhLuong;CongTy")]
    public class BangPhanTichLuongNhanVien : BaseObject, ICongTy
    {       
        private DateTime _NgayLap;
        private KyTinhLuong _KyTinhLuong;
        private CongTy _CongTy;
        private string _TrangThai;
        //

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [DataSourceCriteria("KhoaSo")]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
            }
        }

        [ModelDefault("Caption", "Ngày lập")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public DateTime NgayLap
        {
            get
            {
                return _NgayLap;
            }
            set
            {
                SetPropertyValue("NgayLap", ref _NgayLap, value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    KyTinhLuong = Common.GetKyTinhLuong_ByDate(Session,NgayLap);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField("", DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    KyTinhLuong = null;
                    UpdateKyTinhLuongList();
                }
            }
        }        

        [Aggregated]
        [ModelDefault("Caption", "Danh sách nhân viên")]
        [Association("BangPhanTichLuongNhanVien-ListPhanTichLuongNhanVien")]
        public XPCollection<PhanTichLuongNhanVien> ListPhanTichLuongNhanVien
        {
            get
            {
                return GetCollection<PhanTichLuongNhanVien>("ListPhanTichLuongNhanVien");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách chi phí")]
        [Association("BangPhanTichLuongNhanVien-ListPhanTichLuongCongTy")]
        public XPCollection<PhanTichLuongCongTy> ListPhanTichLuongCongTy
        {
            get
            {
                return GetCollection<PhanTichLuongCongTy>("ListPhanTichLuongCongTy");
            }
        }
       
        [Browsable(false)]
        [ModelDefault("Caption", "Trạng thái")]
        public string TrangThai
        {
            get
            {
                return _TrangThai;
            }
            set
            {
                SetPropertyValue("TrangThai", ref _TrangThai, value);
            }
        }

        public BangPhanTichLuongNhanVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            //
            if (CongTy != null)
                KyTinhLuongList = Common.GetKyTinhLuongBlockList_ByCompanyInfo(Session, CongTy);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            NgayLap = Common.GetServerCurrentTime();
            //           
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateKyTinhLuongList();
            //
        }
    }

}
