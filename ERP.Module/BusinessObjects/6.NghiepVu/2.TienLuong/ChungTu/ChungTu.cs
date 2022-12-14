using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;

namespace ERP.Module.NghiepVu.TienLuong.ChungTus
{
    [DefaultClassOptions]
    [ImageName("BO_ChungTu")]
    [DefaultProperty("SoChungTu")]
    [ModelDefault("Caption", "Chứng từ")]
    [Appearance("ChungTu.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "KyTinhLuong.KhoaSo")]
    [RuleCombinationOfPropertiesIsUnique("ChungTu", DefaultContexts.Save, "CongTy;SoChungTu")]
    public class ChungTu : BaseObject,ICongTy
    {
        private int _SoThuTu;
        private string _SoChungTu;  
        private DateTime _NgayLap;
        private KyTinhLuong _KyTinhLuong;
        private string _DienGiai;
        private decimal _TongTienChungTu;
        private string _SoTienBangChu;
        private bool _TinhThueTNCN;   
        private CongTy _CongTy;
        private string _LoaiChi;
        private bool _HienWeb;
        //
        [ImmediatePostData]
        [ModelDefault("Caption", "Số thứ tự")]
        public int SoThuTu
        {
            get
            {
                return _SoThuTu;
            }
            set
            {
                SetPropertyValue("SoThuTu", ref _SoThuTu, value);
                if (!IsLoading && NgayLap != DateTime.MinValue)
                {
                    if (SoThuTu > 0)
                        SoChungTu = string.Format("CK{0:0#}/{1:####}", value, NgayLap.Year);
                }
            }
        }

        [ModelDefault("Caption", "Số chứng từ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string SoChungTu
        {
            get
            {
                return _SoChungTu;
            }
            set
            {
                SetPropertyValue("SoChungTu", ref _SoChungTu, value);
            }
        }

        [ImmediatePostData]
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
                SetPropertyValue("NgayLap", ref _NgayLap,value);
                if (!IsLoading && value != DateTime.MinValue)
                {
                    KyTinhLuong = Common.GetKyTinhLuong_ByDate(Session, NgayLap);

                    //Tạo số thứ tự tự tăng
                    CriteriaOperator filter = CriteriaOperator.Parse("NgayLap>=? and NgayLap<=? and CongTy=?", value.SetTime(SetTimeEnum.StartYear), value.SetTime(SetTimeEnum.EndYear), CongTy.Oid);
                    object obj = Session.Evaluate<ChungTu>(CriteriaOperator.Parse("Max(SoThuTu)"), filter);
                    if (obj != null)
                        SoThuTu = (int)obj + 1;
                    //
                    if (SoThuTu == 0)
                        SoThuTu = 1;
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Kỳ tính lương")]
        [DataSourceProperty("KyTinhLuongList")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public KyTinhLuong KyTinhLuong
        {
            get
            {
                return _KyTinhLuong;
            }
            set
            {
                SetPropertyValue("KyTinhLuong", ref _KyTinhLuong, value);
                if (!IsLoading && value != null)
                    DienGiai = string.Format("Chuyển khoản lương nhân viên tháng {0:0#}/{1:####}", value.Thang, value.Nam);
            }
        }

        [ModelDefault("Caption", "Loại chi")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.TienLuong.ChungTus.chkComboxEdit_LoaiChi")]
        public string LoaiChi
        {
            get
            {
                return _LoaiChi;
            }
            set
            {
                SetPropertyValue("LoaiChi", ref _LoaiChi, value);
            }
        }

        [ModelDefault("Caption", "Tính Thuế TNCN")]
        public bool TinhThueTNCN
        {
            get
            {
                return _TinhThueTNCN;
            }
            set
            {
                SetPropertyValue("TinhThueTNCN", ref _TinhThueTNCN, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tổng tiền chứng từ")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal TongTienChungTu
        {
            get
            {
                return _TongTienChungTu;
            }
            set
            {
                SetPropertyValue("TongTienChungTu", ref _TongTienChungTu, value);
                if (!IsLoading)
                {
                    SoTienBangChu = Common.DocTien(value);
                }
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số tiền bằng chữ")]
        public string SoTienBangChu
        {
            get
            {
                return _SoTienBangChu;
            }
            set
            {
                SetPropertyValue("SoTienBangChu", ref _SoTienBangChu, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField("", DefaultContexts.Save)]
        //[ModelDefault("AllowEdit", "False")]
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

        [ModelDefault("Caption", "Diễn giải")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public string DienGiai
        {
            get
            {
                return _DienGiai;
            }
            set
            {
                SetPropertyValue("DienGiai", ref _DienGiai, value);
            }
        }

        [ModelDefault("Caption", "Hiện web")]
        public bool HienWeb
        {
            get
            {
                return _HienWeb;
            }
            set
            {
                SetPropertyValue("HienWeb", ref _HienWeb, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chứng từ chuyển khoản")]
        [Association("ChungTu-ListChungTuChuyenKhoan")]
        public XPCollection<ChungTuChuyenKhoan> ListChungTuChuyenKhoan
        {
            get
            {
                return GetCollection<ChungTuChuyenKhoan>("ListChungTuChuyenKhoan");
            }
        }
        
        public ChungTu(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        private void UpdateKyTinhLuongList()
        {
            //
            if (CongTy != null)
                KyTinhLuongList = Common.GetKyTinhLuongList_ByCompanyInfo(Session, CongTy);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            UpdateKyTinhLuongList();
            //
            NgayLap = Common.GetServerCurrentTime();
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
