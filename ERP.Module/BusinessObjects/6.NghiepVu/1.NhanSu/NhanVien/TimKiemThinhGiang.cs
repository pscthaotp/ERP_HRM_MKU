using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.DieuKien;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_LocDuLieu")]
    [ModelDefault("Caption", "Tìm kiếm thỉnh giảng")]
    [ModelDefault("IsCloneable", "True")]
    public class TimKiemThinhGiang : BaseObject,ICongTy
    {
        private string _TenDieuKien;
        private string _DieuKienTimKiem;
        private CongTy _CongTy;

        [NonPersistent]
        [VisibleInDetailView(false)]
        [VisibleInListView(false)]
        public Type GetObjectType
        {
            get
            {
                return typeof(GiangVienThinhGiang);
            }
        }

        
        [ModelDefault("Caption", "Trường/")]
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

        [ModelDefault("Caption", "Tên điều kiện")]
        public string TenDieuKien
        {
            get
            {
                return _TenDieuKien;
            }
            set
            {
                SetPropertyValue("TenDieuKien", ref _TenDieuKien, value);
            }
        }

        [ModelDefault("Caption", "Điều kiện tìm kiếm")]
        [CriteriaOptions("GetObjectType")]
        [Size(SizeAttribute.Unlimited)]
        [ImmediatePostData]
        public string DieuKienTimKiem
        {
            get
            {
                return _DieuKienTimKiem;
            }
            set
            {
                SetPropertyValue("DieuKienTimKiem", ref _DieuKienTimKiem, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Danh sách giảng viên thỉnh giảng")]
        public XPCollection<ChiTietTimKiemThinhGiang> ListChiTietTimKiemThinhGiang { get; set; }

        public TimKiemThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ListChiTietTimKiemThinhGiang = new XPCollection<ChiTietTimKiemThinhGiang>(Session, false);
            //
            CongTy = Common.CongTy(Session);
        }
    }

}
