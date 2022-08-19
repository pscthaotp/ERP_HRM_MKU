using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.HeThong;
using ERP.Module.Commons;
//
namespace ERP.Module.NghiepVu.NhanSu.HoSoLuong
{
    [DefaultClassOptions]
    [ImageName("BO_TroCap")]
    [DefaultProperty("KyTinhLuong")]
    [ModelDefault("IsCloneable", "True")]
    [ModelDefault("Caption", "Hồ sơ tính lương")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "KyTinhLuong", "Hồ sơ tính lương cho kỳ này đã tồn tại.")]
    [Appearance("HoSoTinhLuong.Khoa", TargetItems = "*", Enabled = false, Criteria = "KhoaSo")]    
    public class HoSoTinhLuong : BaseObject,ICongTy
    {
        //
        private bool _KhoaSo;
        private KyTinhLuong _KyTinhLuong;
        private CongTy _CongTy;

        //Lưu trữ
        private DateTime _CreateDate;
        private SecuritySystemUser_Custom _CreateUser;

        //
        [ModelDefault("Caption", "Tháng")]
        [RuleRequiredField("Tháng", DefaultContexts.Save)]
        [DataSourceProperty("KyTinhLuongList")]
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

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        [ModelDefault("AllowEdit", "True")]//False
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }

        [ImmediatePostData]
        //[ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField("", DefaultContexts.Save)]
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
                    //Lấy danh sách kỳ tính lương chưa khóa sổ
                    KyTinhLuongList = Common.GetKyTinhLuongList_ByCompanyInfo(Session, CongTy);
                }
            }

        }

        [Browsable(false)]
        [ModelDefault("Caption", "Ngày tạo")]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                SetPropertyValue("CreateDate", ref _CreateDate, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Người tạo")]
        public SecuritySystemUser_Custom CreateUser
        {
            get
            {
                return _CreateUser;
            }
            set
            {
                SetPropertyValue("CreateUser", ref _CreateUser, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết lương - Mới")]
        [Association("HoSoTinhLuong-ListChiTietLuong")]
        public XPCollection<ChiTietLuong> ListChiTietLuong
        {
            get
            {
                return GetCollection<ChiTietLuong>("ListChiTietLuong");
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết lương - Cũ")]
        [Association("HoSoTinhLuong-ListChiTietLuong_Old")]
        public XPCollection<ChiTietLuong_Old> ListChiTietLuong_Old
        {
            get
            {
                return GetCollection<ChiTietLuong_Old>("ListChiTietLuong_Old");
            }
        }

        [Browsable(false)]
        public XPCollection<KyTinhLuong> KyTinhLuongList { get; set; }

        public HoSoTinhLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //Set mặc định
            CongTy = Common.CongTy(Session);
            //Lấy danh sách kỳ tính lương chưa khóa sổ
            KyTinhLuongList = Common.GetKyTinhLuongList_ByCompanyInfo(Session, CongTy);
            //
            if (KhoaSo == true)
                KyTinhLuong = Common.GetKyTinhLuong_ByDate(Session, Common.GetServerCurrentTime());
            //            
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //Lấy danh sách kỳ tính lương chưa khóa sổ
            KyTinhLuongList = Common.GetKyTinhLuongList_ByCompanyInfo(Session, CongTy);
        }
    }

}
