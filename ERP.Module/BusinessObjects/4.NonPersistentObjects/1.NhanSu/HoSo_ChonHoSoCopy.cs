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
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.TienLuong;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn hồ sơ muốn copy")]
    public class HoSo_ChonHoSoCopy : BaseObject, IBoPhan
    {
        //
        private CongTy _CongTy;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private CongTy _CongTyDen;
        private BoPhan _BoPhanDen;
        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh;
        private LoaiNhanSu _LoaiNhanSu;
        private NhomPhanBo _NhomPhanBo;
        private DateTime _NgayVao = DateTime.Now.Date;
        
        [ModelDefault("AllowEdit",  "False")]
        [ModelDefault("Caption", "Công ty đi")]
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
        [ModelDefault("Caption", "Đơn vị đi")]
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
                    ThongTinNhanVien = null;
                    UpdateNhanVienList();
                    if (BoPhan != null)
                        CongTy = BoPhan.CongTy;

                }
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NVList", DataSourcePropertyIsNullMode.SelectNothing)]
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

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Công ty đến")]
        public CongTy CongTyDen
        {
            get
            {
                return _CongTyDen;
            }
            set
            {
                SetPropertyValue("CongTyDen", ref _CongTyDen, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đơn vị đến")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BoPhan BoPhanDen
        {
            get
            {
                return _BoPhanDen;
            }
            set
            {
                SetPropertyValue("BoPhanDen", ref _BoPhanDen, value);
                if (!IsLoading)
                    CongTyDen = BoPhanDen.CongTy;
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ")]
        //[RuleRequiredField(DefaultContexts.Save)]
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
                    CapNhatChucDanh();
            }
        }

        [ModelDefault("Caption", "Chức danh")]
        //[DataSourceProperty("CDList")]
        [DataSourceProperty("CDList", DataSourcePropertyIsNullMode.SelectAll)]
        //[RuleRequiredField(DefaultContexts.Save)]
        public ChucDanh ChucDanh
        {
            get
            {
                return _ChucDanh;
            }
            set
            {
                SetPropertyValue("ChucDanh", ref _ChucDanh, value);
            }
        }

        [ModelDefault("Caption", "Loại nhân sự")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiNhanSu LoaiNhanSu
        {
            get
            {
                return _LoaiNhanSu;
            }
            set
            {
                SetPropertyValue("LoaiNhanSu", ref _LoaiNhanSu, value);
            }
        }

        [ModelDefault("Caption", "Ngày vào")]
        public DateTime NgayVao
        {
            get
            {
                return _NgayVao;
            }
            set
            {
                SetPropertyValue("NgayVao", ref _NgayVao, value);
            }
        }

        [ModelDefault("Caption", "Nhóm phân bổ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NhomPhanBo NhomPhanBo
        {
            get
            {
                return _NhomPhanBo;
            }
            set
            {
                SetPropertyValue("NhomPhanBo", ref _NhomPhanBo, value);
            }
        }

        public HoSo_ChonHoSoCopy(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        private void UpdateNhanVienList()
        {
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }
        /*Nguyen sua 
            //[Browsable(false)]
            //public XPCollection<ChucDanh> CDList { get; set; }
 
        */
        [Browsable(false)]
        public XPCollection<ChucDanh> CDList { get; set; }

        public void CapNhatChucDanh()
        {
            //Nguyen sua
                if (CDList == null)
                    CDList = new XPCollection<ChucDanh>(Session);
                
                if (ChucVu != null)
                    CDList.Filter = CriteriaOperator.Parse("ChucVu.Oid=?", ChucVu.Oid);
            

            //if (CDList == null)
            //    CDList = new XPCollection<ChucDanh>(Session, false);
            //else
            //    CDList.Reload();
            //
            //if (ChucVu != null)
            //{
            //    CriteriaOperator Filter = CriteriaOperator.Parse("ChucVu = ?", ChucVu.Oid);
            //    XPCollection<ChucDanh> list = new XPCollection<ChucDanh>(Session, Filter);
            //    foreach (ChucDanh item in list)
            //    {
            //        ChucDanh cd = new ChucDanh(Session);
            //        cd = item;
            //        CDList.Add(cd);
            //    }

            //}
       
        }
    }

}
