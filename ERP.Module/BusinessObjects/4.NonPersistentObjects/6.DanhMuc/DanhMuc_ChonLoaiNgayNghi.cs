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
using ERP.Module.Enum.NhanSu;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NonPersistentObjects.DanhMuc
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn thông tin muốn copy")]
    [Appearance("LoaiNgayNghi.TatCaDonVi", TargetItems = "BoPhan", Enabled = false, Criteria = "TatCa")]
    public class DanhMuc_ChonLoaiNgayNghi : BaseObject, ICongTy, IBoPhan
    {
        //
        private CongTy _CongTy;
        private bool _TatCa = true;
        private BoPhan _BoPhan;
        private LoaiNgayNghiEnum _LoaiNgayNghi;

        [ModelDefault("Caption", "Lấy ngày nghỉ từ Công ty / Trường")]
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

        [ImmediatePostData]
        [ModelDefault("Caption", "Tất cả")]
        public bool TatCa
        {
            get
            {
                return _TatCa;
            }
            set
            {
                SetPropertyValue("TatCa", ref _TatCa, value);
            }
        }

        [ModelDefault("Caption", "Copy đến Công ty / Trường")]
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

        [ModelDefault("Caption", "Loại ngày nghỉ")]
        public LoaiNgayNghiEnum LoaiNgayNghi
        {
            get
            {
                return _LoaiNgayNghi;
            }
            set
            {
                SetPropertyValue("LoaiNgayNghi", ref _LoaiNgayNghi, value);
            }
        }

        public DanhMuc_ChonLoaiNgayNghi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }
    }

}
