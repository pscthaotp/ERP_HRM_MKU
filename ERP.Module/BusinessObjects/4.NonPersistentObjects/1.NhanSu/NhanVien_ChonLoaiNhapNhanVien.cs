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
    [Appearance("NhanVien_ChonBoPhan.TatCa", TargetItems = "BoPhan", Visibility = ViewItemVisibility.Hide, Criteria = "TatCa")]
    public class NhanVien_ChonBoPhan : OfficeBaseObject
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

        public NhanVien_ChonBoPhan(Session session) : base(session) { }
    }
    #endregion

    #region 2. Chọn quá trình import
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn quá trình")]
    public class NhanVien_ChonQuaTrinh : OfficeBaseObject
    {
        private LoaiQuaTrinhEnum _LoaiQuaTrinh;

        [ModelDefault("Caption", "Loại quá trình")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiQuaTrinhEnum LoaiQuaTrinh
        {
            get
            {
                return _LoaiQuaTrinh;
            }
            set
            {
                SetPropertyValue("LoaiQuaTrinh", ref _LoaiQuaTrinh, value);
            }
        }

        public NhanVien_ChonQuaTrinh(Session session) : base(session) { }
    }
    #endregion

    #region 3. Chọn nhân viên import
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn nhân viên")]
    [Appearance("NhanVien_ChonNhanVien.TatCa", TargetItems = "ThongTinNhanVien", Visibility = ViewItemVisibility.Hide, Criteria = "TatCa")]
    public class NhanVien_ChonNhanVien : OfficeBaseObject
    {
        private bool _TatCa = true;
        private ThongTinNhanVien _ThongTinNhanVien;

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
                    ThongTinNhanVien = null;
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "!TatCa")]
        [DataSourceProperty("NVList")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public NhanVien_ChonNhanVien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }

        /// <summary>
        /// Cập nhật danh sách nhân viên
        /// </summary>
        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
        }
    }
    #endregion
}
