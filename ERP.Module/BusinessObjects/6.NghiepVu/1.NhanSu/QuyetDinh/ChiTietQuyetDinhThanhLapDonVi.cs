using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết quyết định thành lập đơn vị")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhThanhLapDonVi;ThongTinNhanVien")]
    [Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    public class ChiTietQuyetDinhThanhLapDonVi : BaseObject
    {
        //
        private QuyetDinhThanhLapDonVi _QuyetDinhThanhLapDonVi;
        private ThongTinNhanVien _ThongTinNhanVien;
        private bool IsEnable = true;
        private BoPhan _BoPhan;


        [Browsable(false)]
        //[Association("QuyetDinhThanhLapDonVi-ListChiTietQuyetDinhThanhLapDonVi")]
        public QuyetDinhThanhLapDonVi QuyetDinhThanhLapDonVi
        {
            get
            {
                return _QuyetDinhThanhLapDonVi;
            }
            set
            {
                SetPropertyValue("QuyetDinhThanhLapDonVi", ref _QuyetDinhThanhLapDonVi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [ImmediatePostData]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectAll)]
        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
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



        public ChiTietQuyetDinhThanhLapDonVi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (ThongTinNhanVien != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("Oid=? && GCRecord IS NULL", ThongTinNhanVien);
                ThongTinNhanVien thongTinNhanVien = Session.FindObject<ThongTinNhanVien>(filter);
                if (thongTinNhanVien != null && thongTinNhanVien.TinhTrang.DaNghiViec == false)
                    IsEnable = false;
            }

            UpdateNhanVienList();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

        }


    }
}
