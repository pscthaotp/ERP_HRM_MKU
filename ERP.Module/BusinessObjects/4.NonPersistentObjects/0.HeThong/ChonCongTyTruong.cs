using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn Công ty/Trường")]
    [Appearance("BaoCao_DanhSach_BoNhiemNhanSu.TatCaDonVi", TargetItems = "CongTy", Enabled = false, Criteria = "TatCaDonVi")]
    public class ChonCongTyTruong : BaseObject, ICongTy
    {
        //
        private bool _TatCaDonVi = false;
        private CongTy _CongTy;       

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả")]
        public bool TatCaDonVi
        {
            get
            {
                return _TatCaDonVi;
            }
            set
            {
                SetPropertyValue("TatCaDonVi", ref _TatCaDonVi, value);
                if (!IsLoading && value == true)
                {
                    CongTy = null;
                }
            }
        }

        [ModelDefault("Caption", "Công ty/Trường")]
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


        public ChonCongTyTruong(Session session) : base(session) { }
       
    }

}
