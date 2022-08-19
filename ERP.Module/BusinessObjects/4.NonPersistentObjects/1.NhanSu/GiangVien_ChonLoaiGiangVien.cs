using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    #region 1. Chọn bộ phận import
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn bộ phận")]
    [Appearance("GiangVien_ChonLoaiGiangVien.TatCa", TargetItems = "BoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "TatCa")]
    public class GiangVien_ChonLoaiGiangVien : OfficeBaseObject
    {
        //
        private bool _TatCa = true;
        private BoPhan _BoPhan;

        [ModelDefault("Caption", "Tất cả")]
        [ImmediatePostData]
        public bool TatCa
        {
            get
            {
                return _TatCa;
            }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
                if (!IsLoading)
                    BoPhan = null;
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCa")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        public GiangVien_ChonLoaiGiangVien(Session session) : base(session) { }
    }
    #endregion
}
