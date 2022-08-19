using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.DinhBien;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [DefaultProperty("Caption")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Đăng ký tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique("DangKyTuyenDung", DefaultContexts.Save, "ViTriTuyenDung;BoPhan;QuanLyTuyenDung")]
    //[RuleCriteria("SoLuongTuyen > 0 and SoLuongTuyen <= SoLuongHopLe", CustomMessageTemplate = "Số lượng tuyển phải NHỎ HƠN HOẶC BẰNG Số lượng định biên !!!", SkipNullOrEmptyValues = false)]
    [Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "Duyet")]
    public class DangKyTuyenDung : BaseObject, IBoPhan
    {
        // Fields...
        private BoPhan _BoMon;
        private ViTriTuyenDung _ViTriTuyenDung;
        private decimal _LuongDuKien;
        private int _SoLuongDinhBien;
        private int _SoLuongNhanSu;
        private int _SoLuongTuyen;
        private int _SoLuongNgoaiDinhBien;
        private BoPhan _BoPhan;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private DateTime _ThoiGianLamViecDuKien;
        private bool _ThayThe;
        private bool _DuAnMoi;
        private bool _Duyet;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListDangKyTuyenDung")]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuanLyTuyenDung.ListViTriTuyenDung")]
        public ViTriTuyenDung ViTriTuyenDung
        {
            get
            {
                return _ViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("ViTriTuyenDung", ref _ViTriTuyenDung, value);
                if (!IsLoading && value != null)
                {
                    BoPhan = ViTriTuyenDung.BoPhan;
                    CapNhatSoLuongDinhBien();
                }
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
                    BoMon = null;
                    UpdateBoMonList();
                    CapNhatSoLuongDinhBien();
                    CapNhatSoLuongNhanSu();
                }
            }
        }

        [ModelDefault("Caption", "Bộ môn")]
        [DataSourceProperty("BoMonList")]
        public BoPhan BoMon
        {
            get
            {
                return _BoMon;
            }
            set
            {
                SetPropertyValue("BoMon", ref _BoMon, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số lượng định biên")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoLuongDinhBien
        {
            get
            {
                return _SoLuongDinhBien;
            }
            set
            {
                SetPropertyValue("SoLuongDinhBien", ref _SoLuongDinhBien, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số lượng nhân sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoLuongNhanSu
        {
            get
            {
                return _SoLuongNhanSu;
            }
            set
            {
                SetPropertyValue("SoLuongNhanSu", ref _SoLuongNhanSu, value);
            }
        }

        [NonPersistent]
        [ModelDefault("Caption", "Số lượng hợp lệ")]
        public int SoLuongHopLe
        {
            get
            {
                return _SoLuongDinhBien - _SoLuongNhanSu;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số lượng tuyển")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int SoLuongTuyen
        {
            get
            {
                return _SoLuongTuyen;
            }
            set
            {
                SetPropertyValue("SoLuongTuyen", ref _SoLuongTuyen, value);
                if (!IsLoading && value != 0)
                {
                    if (SoLuongTuyen - (_SoLuongDinhBien - _SoLuongNhanSu) >= 0)
                        SoLuongNgoaiDinhBien = SoLuongTuyen - (_SoLuongDinhBien - _SoLuongNhanSu);
                }
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Số lượng ngoài định biên")]
        public int SoLuongNgoaiDinhBien
        {
            get
            {
                return _SoLuongNgoaiDinhBien;
            }
            set
            {
                SetPropertyValue("SoLuongNgoaiDinhBien", ref _SoLuongNgoaiDinhBien, value);
            }
        }

        [ModelDefault("Caption", "Mức lương dự kiến")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        public decimal LuongDuKien
        {
            get
            {
                return _LuongDuKien;
            }
            set
            {
                SetPropertyValue("LuongDuKien", ref _LuongDuKien, value);
            }
        }

        [ModelDefault("Caption", "Thay thế nhân sự")]
        public bool ThayThe
        {
            get
            {
                return _ThayThe;
            }
            set
            {
                SetPropertyValue("ThayThe", ref _ThayThe, value);
            }
        }

        [ModelDefault("Caption", "Dự án mới")]
        public bool DuAnMoi
        {
            get
            {
                return _DuAnMoi;
            }
            set
            {
                SetPropertyValue("DuAnMoi", ref _DuAnMoi, value);
            }
        }

        [ModelDefault("Caption", "Thời gian làm việc dự kiến")]
        public DateTime ThoiGianLamViecDuKien
        {
            get
            {
                return _ThoiGianLamViecDuKien;
            }
            set
            {
                SetPropertyValue("ThoiGianLamViecDuKien", ref _ThoiGianLamViecDuKien, value);
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Trạng thái duyệt")]
        public bool Duyet
        {
            get
            {
                return _Duyet;
            }
            set
            {
                SetPropertyValue("Duyet", ref _Duyet, value);
            }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return ObjectFormatter.Format("{ViTriTuyenDung.TenViTriTuyenDung} {BoMon.TenBoPhan} {BoPhan.TenBoPhan}", this);
            }
        }

        public DangKyTuyenDung(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<BoPhan> BoMonList { get; set; }

        private void UpdateBoMonList()
        {
            if (BoMonList == null)
                BoMonList = new XPCollection<BoPhan>(Session);

            BoMonList.Criteria = CriteriaOperator.Parse("BoPhanCha=? and LoaiBoPhan=3",
                BoPhan);
        }

        private void CapNhatSoLuongDinhBien()
        {
            QuanLyDinhBienChucDanh quanLyDinhBienChucDanh = Session.FindObject<QuanLyDinhBienChucDanh>(CriteriaOperator.Parse("CongTy.Oid=? and NienDoTaiChinh.Oid=?", QuanLyTuyenDung.CongTy.Oid, QuanLyTuyenDung.NienDoTaiChinh.Oid));
            if (quanLyDinhBienChucDanh != null && BoPhan != null && ViTriTuyenDung != null)
            {
                DinhBienChucDanh dinhBien = Session.FindObject<DinhBienChucDanh>(CriteriaOperator.Parse("QuanLyDinhBienChucDanh.Oid=? and BoPhan.Oid=? and ChucDanh.Oid=?", quanLyDinhBienChucDanh.Oid, BoPhan.Oid, ViTriTuyenDung.ChucDanh.Oid));
                if (dinhBien != null)
                    SoLuongDinhBien = dinhBien.SoLuong;
                else
                    SoLuongDinhBien = 0;
            }
        }

        private void CapNhatSoLuongNhanSu()
        {
            if (BoPhan != null && ViTriTuyenDung != null)
            {
                CriteriaOperator filter1 = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
                CriteriaOperator filter2 = CriteriaOperator.Parse("ChucDanh.Oid=?", ViTriTuyenDung.ChucDanh.Oid);
                CriteriaOperator filter3 = CriteriaOperator.Parse("!TinhTrang.DaNghiViec");
                GroupOperator go = new GroupOperator(GroupOperatorType.And);
                go.Operands.Add(filter1);
                go.Operands.Add(filter2);
                go.Operands.Add(filter3);
                XPCollection<ThongTinNhanVien> nhanvienlist = new XPCollection<ThongTinNhanVien>(Session, go);
                if (nhanvienlist != null && nhanvienlist.Count > 0)
                    SoLuongNhanSu = nhanvienlist.Count;
                else
                    SoLuongNhanSu = 0;

            }
        }
    }
}
