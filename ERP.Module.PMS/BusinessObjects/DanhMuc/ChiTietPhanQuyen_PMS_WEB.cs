using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Chi tiết  phân quyền sử dụng PMS (WEB)")]
    public class ChiTietPhanQuyen_PMS_WEB : BaseObject
    {
        #region Key
        private PhanQuyen_PMS_WEB _PhanQuyen_PMS_WEB;
        [ModelDefault("Caption", "Quản lý")]
        [RuleRequiredField("", DefaultContexts.Save)]
        [Association("PhanQuyen_PMS_WEB-ListChiTiet")]

        public PhanQuyen_PMS_WEB PhanQuyen_PMS_WEB
        {
            get
            {
                return _PhanQuyen_PMS_WEB;
            }
            set
            {
                SetPropertyValue("PhanQuyen_PMS_WEB", ref _PhanQuyen_PMS_WEB, value);
            }
        }

        private NhanVien _NhanVien;
        private LoaiHoatDong_PhanQuyen_WEB _LoaiHD_PhanQuyen;
        private bool _NgungSuDung;
        private string _GhiChu;

        [ModelDefault("Caption", "Cán bộ")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Loại HĐ phân quyền")]
        public LoaiHoatDong_PhanQuyen_WEB LoaiHD_PhanQuyen
        {
            get { return _LoaiHD_PhanQuyen; }
            set { SetPropertyValue("LoaiHD_PhanQuyen", ref _LoaiHD_PhanQuyen, value); }
        }

        [ModelDefault("Caption", "Ngưng sử dụng")]
        public bool NgungSuDung
        {
            get
            {
                return _NgungSuDung;
            }
            set
            {
                SetPropertyValue("NgungSuDung", ref _NgungSuDung, value);
            }
        }
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get{return _GhiChu;}
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        #endregion
        public ChiTietPhanQuyen_PMS_WEB(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            NgungSuDung = false;
        }
    }
}
