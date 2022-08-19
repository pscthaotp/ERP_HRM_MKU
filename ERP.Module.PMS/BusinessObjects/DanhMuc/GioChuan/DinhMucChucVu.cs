using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.CauHinhChungs;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.Commons;

namespace ERP.Module.PMS.GioChuan
{
    [ImageName("BO_ChuyenNgach")]
    //[DefaultProperty("TenNhomMonHoc")]
    [ModelDefault("Caption", "Định mức chức vụ")]
    //[Appearance("SoGioChuan", TargetItems = "SoGioChuan", BackColor = "Aquamarine", FontColor = "Red")]

   [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyGioChuan;ChucVu;ChucDanh;GhiChu", "Chức vụ đã tồn tại")]
   public class DinhMucChucVu : BaseObject
    {
        private QuanLyGioChuan _QuanLyGioChuan;

        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh; 
        private decimal _DinhMuc;
        private decimal _SoGioChuan;
        private decimal _SoGioDinhMuc_NCKH;
        private decimal _SoGioDinhMuc_Khac;
        private string _GhiChu;
        private bool _ChiTinhGioChuan;


        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyGioChuan-ListDinhMucChucVu")]
        [Browsable(false)]
        [ImmediatePostData]
        public QuanLyGioChuan QuanLyGioChuan
        {
            get
            {
                return _QuanLyGioChuan;
            }
            set
            {
                SetPropertyValue("QuanLyGioChuan", ref _QuanLyGioChuan, value);
            }
        }
        [ModelDefault("Caption", "Chức vụ")]
        [RuleRequiredField("", DefaultContexts.Save, "Chưa chọn chức vụ")]
        [ImmediatePostData]
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set
            {
                SetPropertyValue("ChucVu", ref _ChucVu, value);
                if (!IsLoading)
                {
                    Check(); 
                    ChucDanh = null;
                    ChucDanhList = null;
                    if (value != null)
                    {
                         UpdateChucDanh();
                    }
                }
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        [DataSourceProperty("ChucDanhList", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        public ChucDanh ChucDanh
        {
            get { return _ChucDanh; }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
                if (!IsLoading)
                    Check();
            }
        }

        [ModelDefault("Caption", "Định mức giảm trừ(%)")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        [ImmediatePostData]
        public decimal DinhMuc
        {
            get { return _DinhMuc; }
            set
            {
                SetPropertyValue("DinhMuc", ref _DinhMuc, value);
                if (!IsLoading)
                 {
                    if (QuanLyGioChuan != null)
                    {
                        SoGioChuan = (100-DinhMuc) * QuanLyGioChuan.GioChuanGiangDay_MacDinh / 100;
                        SoGioDinhMuc_NCHK = (100-DinhMuc) * QuanLyGioChuan.GioChuanNCKH_MacDinh / 100;
                        SoGioDinhMuc_Khac = (100-DinhMuc) * QuanLyGioChuan.GioChuanKhac_MacDinh / 100;
                    }
                }
            }
        }

        [ModelDefault("Caption", "Số giờ chuẩn")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoGioChuan
        {
            get { return _SoGioChuan; }
            set { SetPropertyValue("SoGioChuan", ref _SoGioChuan, value); }
        }
        [ModelDefault("Caption", "Số giờ định mức NCKH")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoGioDinhMuc_NCHK
        {
            get { return _SoGioDinhMuc_NCKH; }
            set { SetPropertyValue("SoGioDinhMuc_NCHK", ref _SoGioDinhMuc_NCKH, value); }
        }
        [ModelDefault("Caption", "Số giờ định mức khác")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoGioDinhMuc_Khac
        {
            get { return _SoGioDinhMuc_Khac; }
            set { SetPropertyValue("SoGioDinhMuc_Khac", ref _SoGioDinhMuc_Khac, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
                if (!IsLoading)
                    Check();
            }
        }

        [ModelDefault("Caption", "Chỉ tính giờ chuẩn")]
        public bool ChiTinhGioChuan
        {
            get { return _ChiTinhGioChuan; }
            set
            {
                SetPropertyValue("ChiTinhGioChuan", ref _ChiTinhGioChuan, value);
            }
        }

        private bool _KT;
        [NonPersistent]
        [Browsable(false)]
        [RuleFromBoolProperty("DinhMucChucVu.KT", DefaultContexts.Save, "Chức vụ, chức danh hoặc ghi chú không được rỗng!", SkipNullOrEmptyValues = false, UsedProperties = "QuanLyGioChuan")]
        public bool KT
        {
            get
            {
                return !_KT;
            }
            set
            {
                SetPropertyValue("KT", ref _KT, value);
            }
        }

        [Browsable(false)]
        public XPCollection<ChucDanh> ChucDanhList { get; set; }


        [Browsable(false)]
        public XPCollection<ChucVu> ChucVuList { get; set; }

        public DinhMucChucVu(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            KT = true;
            //DinhMuc = 0;
            SoGioChuan = 0;
            SoGioDinhMuc_NCHK = 0;
            SoGioDinhMuc_Khac = 0;
            ChucDanhList = new XPCollection<ChucDanh>(Session, false);
            if (QuanLyGioChuan != null)
            {
                SoGioChuan = (100 - DinhMuc) * QuanLyGioChuan.GioChuanGiangDay_MacDinh / 100;
                SoGioDinhMuc_NCHK = (100 - DinhMuc) * QuanLyGioChuan.GioChuanNCKH_MacDinh / 100;
                SoGioDinhMuc_Khac = (100 - DinhMuc) * QuanLyGioChuan.GioChuanKhac_MacDinh / 100;
            }
        }

        void Check()
        {
            #region Store

            {
                if (ChucVu == null && ChucDanh == null && GhiChu == string.Empty)
                {
                    KT = true;//không dc Save
                }
                else
                    KT = false;//Dc save
            }
            #endregion
        }

        //Cập nhật danh sách chức danh theo chức vụ
        private void UpdateChucDanh()
        {
            if (ChucVu == null)
                ChucDanhList = new XPCollection<ChucDanh>(Session, false);
            //
            if (ChucVu != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("ChucVu = ?", ChucVu);
                ChucDanhList = new XPCollection<ChucDanh>(Session, filter);
            }
        }

        //Lấy danh sách chức vụ hiện tại đang có 
        private void LoadChucVu()
        {
            if (QuanLyGioChuan != null)
            {
                ChucVuList = new XPCollection<ChucVu>(Session, false);
                
            }
            
        }
    }
}