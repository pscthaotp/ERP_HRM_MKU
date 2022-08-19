using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.NghiepVu.NhanSu.TuyenDung
{
    [DefaultProperty("Caption")]
    [ModelDefault("AllowLink", "False")]
    [ModelDefault("AllowUnlink", "False")]
    [ModelDefault("Caption", "Vị trí tuyển dụng")]
    [RuleCombinationOfPropertiesIsUnique("ViTriTuyenDung.Unique", DefaultContexts.Save, "QuanLyTuyenDung;MaQuanLy")]
    //[Appearance("Enabled", TargetItems = "*", Enabled = false, Criteria = "!IsEnable")]
    public class ViTriTuyenDung : BaseObject, IBoPhan
    {
        // Fields...
        //private LoaiNhanVienEnum _PhanLoai;
        private bool IsEnable = true;
        private BoPhan _BoPhan;
        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh;
        private QuanLyTuyenDung _QuanLyTuyenDung;
        private string _MaQuanLy;
        private string _TenViTriTuyenDung;
        private LoaiTuyenDung _LoaiTuyenDung;

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [Association("QuanLyTuyenDung-ListViTriTuyenDung")]
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

        [ModelDefault("Caption", "Số thứ tự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string MaQuanLy
        {
            get
            {
                return _MaQuanLy;
            }
            set
            {
                SetPropertyValue("MaQuanLy", ref _MaQuanLy, value);
            }
        }
        
        [ModelDefault("Caption", "Tên vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string TenViTriTuyenDung
        {
            get
            {
                return _TenViTriTuyenDung;
            }
            set
            {
                SetPropertyValue("TenViTriTuyenDung", ref _TenViTriTuyenDung, value);
            }
        }

        [ModelDefault("Caption", "Bộ phận")]
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
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVu
        {
            get
            {
                return _ChucVu;
            }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
                if (!IsLoading)
                {
                    ChucDanh = null;
                    CapNhatChucDanh();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức danh")]
        [DataSourceProperty("CDList")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
                if (!IsLoading && value != null)
                    TenViTriTuyenDung = ChucDanh.TenChucDanh; 
            }
        }

        [ModelDefault("Caption", "Loại tuyển dụng")]
        public LoaiTuyenDung LoaiTuyenDung
        {
            get
            {
                return _LoaiTuyenDung;
            }
            set
            {
                SetPropertyValue("LoaiTuyenDung", ref _LoaiTuyenDung, value);
            }
        }

        [NonCloneable]
        [NonPersistent]
        [Browsable(false)]
        public string Caption
        {
            get
            {
                string tenLoaiTuyenDung = string.Empty;
                if (LoaiTuyenDung == null)
                {
                    tenLoaiTuyenDung = TenViTriTuyenDung;
                }
                else
                {
                    tenLoaiTuyenDung = TenViTriTuyenDung + " " + LoaiTuyenDung.TenLoaiTuyenDung;
                }
                //
                return tenLoaiTuyenDung;
            }
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();

            CriteriaOperator filter = CriteriaOperator.Parse("ViTriTuyenDung=? && GCRecord IS NULL", this.Oid);
            DangKyTuyenDung dangKy = Session.FindObject<DangKyTuyenDung>(filter);
            if (dangKy != null)
                IsEnable = false;
        }

        [Browsable(false)]
        public XPCollection<ChucDanh> CDList { get; set; }

        public ViTriTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            XPCollection<LoaiTuyenDung> listLoaiTuyenDung = new XPCollection<LoaiTuyenDung>(Session);

            LoaiTuyenDung loaiTuyenDung = listLoaiTuyenDung.Session.FindObject<LoaiTuyenDung>(CriteriaOperator.Parse("TenLoaiTuyenDung = ?", "Cơ hữu"));

            LoaiTuyenDung = loaiTuyenDung;
        }

        public void CapNhatChucDanh()
        {
            if (CDList == null)
                CDList = new XPCollection<ChucDanh>(Session);
            //            
            if (ChucVu != null)
                CDList.Filter = CriteriaOperator.Parse("ChucVu.Oid=?", ChucVu.Oid);
        }
    }
}
