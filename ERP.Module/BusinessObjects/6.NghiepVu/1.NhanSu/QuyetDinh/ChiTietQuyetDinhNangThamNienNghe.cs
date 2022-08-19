using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết nâng thâm niên nghề")]
    [RuleCombinationOfPropertiesIsUnique(DefaultContexts.Save, "QuyetDinhNangThamNienNghe;ThongTinNhanVien")]
    public class ChiTietQuyetDinhNangThamNienNghe : BaseObject
    {
        //
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private QuyetDinhNangThamNienNghe _QuyetDinhNangThamNienNghe;

        private DateTime _NgayHuongThamNienCu;
        private DateTime _NgayHuongThamNienMoi;
        private decimal _ThamNienMoi;
        private decimal _ThamNienCu;
        private decimal _HSPCThamNienCu;
        private decimal _HSPCThamNienMoi;

        [Browsable(false)]
        [ModelDefault("Caption", "Quyết định nâng thâm niên nghề")]
        [Association("QuyetDinhNangThamNienNghe-ListChiTietQuyetDinhNangThamNienNghe")]
        public QuyetDinhNangThamNienNghe QuyetDinhNangThamNienNghe
        {
            get
            {
                return _QuyetDinhNangThamNienNghe;
            }
            set
            {
                SetPropertyValue("QuyetDinhNangThamNienNghe", ref _QuyetDinhNangThamNienNghe, value);
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
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                        BoPhan = value.BoPhan;
                    ThamNienCu = ThongTinNhanVien.NhanVienThongTinLuong.ThamNien;
                    NgayHuongThamNienCu = ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNien;
                    HSPCThamNienCu = ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien;
                }
            }
        }

        [ModelDefault("Caption", "% Thâm niên cũ")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal ThamNienCu
        {
            get
            {
                return _ThamNienCu;
            }
            set
            {
                SetPropertyValue("ThamNienCu", ref _ThamNienCu, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "HSPC Thâm niên cũ")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCThamNienCu
        {
            get
            {
                return _HSPCThamNienCu;
            }
            set
            {
                SetPropertyValue("HSPCThamNienCu", ref _HSPCThamNienCu, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên cũ")]
        public DateTime NgayHuongThamNienCu
        {
            get
            {
                return _NgayHuongThamNienCu;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienCu", ref _NgayHuongThamNienCu, value);
            }
        }

        [ModelDefault("Caption", "% Thâm niên mới")]
        [ModelDefault("EditMask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        [RuleRequiredField(DefaultContexts.Save)]
        public decimal ThamNienMoi
        {
            get
            {
                return _ThamNienMoi;
            }
            set
            {
                SetPropertyValue("ThamNienMoi", ref _ThamNienMoi, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "HSPC Thâm niên mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCThamNienMoi
        {
            get
            {
                return _HSPCThamNienMoi;
            }
            set
            {
                SetPropertyValue("HSPCThamNienMoi", ref _HSPCThamNienMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng thâm niên mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NgayHuongThamNienMoi
        {
            get
            {
                return _NgayHuongThamNienMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongThamNienMoi", ref _NgayHuongThamNienMoi, value);
            }
        }

        public ChiTietQuyetDinhNangThamNienNghe(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            UpdateNhanVienList();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateNhanVienList();
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!IsLoading && !IsDeleted
                && QuyetDinhNangThamNienNghe != null
                && !QuyetDinhNangThamNienNghe.IsDirty)
                QuyetDinhNangThamNienNghe.IsDirty = true;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {

                //Update đến ngày của diễn biến lương trước đó = ngày hưởng lương mới - 1
                ProcessesHelper.UpdateDienBienLuong(Session, QuyetDinhNangThamNienNghe, ThongTinNhanVien, NgayHuongThamNienMoi, true);

                if (QuyetDinhNangThamNienNghe.QuyetDinhMoi)
                {
                    //cập nhật thâm niên
                    ThongTinNhanVien.NhanVienThongTinLuong.ThamNien = ThamNienMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien = HSPCThamNienMoi;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNien = NgayHuongThamNienMoi;
                }

                //Diễn biến lương
                ProcessesHelper.CreateDienBienLuong(Session, QuyetDinhNangThamNienNghe, ThongTinNhanVien, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //Lấy lại dữ liệu cũ
                if (this.QuyetDinhNangThamNienNghe.QuyetDinhMoi)
                {
                    ThongTinNhanVien.NhanVienThongTinLuong.ThamNien = ThamNienCu;
                    ThongTinNhanVien.NhanVienThongTinLuong.NgayHuongThamNien = NgayHuongThamNienCu;
                    ThongTinNhanVien.NhanVienThongTinLuong.HSPCThamNien = HSPCThamNienCu;
                }

                //Xóa diễn biến lương
                ProcessesHelper.DeleteQuaTrinhNhanVien<DienBienLuong>(Session, this.QuyetDinhNangThamNienNghe.Oid, this.ThongTinNhanVien.Oid);
            }

            base.OnDeleting();
        }
    }

}
