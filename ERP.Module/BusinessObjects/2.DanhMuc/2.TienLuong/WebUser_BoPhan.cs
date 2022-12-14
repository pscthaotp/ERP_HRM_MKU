using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
//
namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [DefaultProperty("Name")]
    [ImageName("BO_KyTinhLuong")]
    [ModelDefault("Caption", "Tài khoản - Bộ phận")]
    public class WebUser_BoPhan : BaseObject
    {
        private BoPhan _BoPhanID;
        private WebGroup _WebGroup;
        private QuyetDinh _QuyetDinh;
        private bool _ChucVuChinh;
        private Guid _IDWebUser;
        private ThongTinNhanVien _ThongTinNhanVien;

        [ModelDefault("Caption", "Bộ phận")]
        public BoPhan BoPhanID
        {
            get
            {
                return _BoPhanID;
            }
            set
            {
                SetPropertyValue("BoPhanID", ref _BoPhanID, value);
            }
        }

        [ModelDefault("Caption", "Nhóm tài khoản")]
        public WebGroup WebGroup
        {
            get
            {
                return _WebGroup;
            }
            set
            {
                SetPropertyValue("WebGroup", ref _WebGroup, value);
            }
        }

        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Chức vụ chính")]
        public bool ChucVuChinh
        {
            get
            {
                return _ChucVuChinh;
            }
            set
            {
                SetPropertyValue("ChucVuChinh", ref _ChucVuChinh, value);
            }
        }

        [ModelDefault("Caption", "Thông tin nhân viên")]
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

        public WebUser_BoPhan(Session session) :base(session) { }

        protected override void OnSaved()
        {
            base.OnSaved();

            //SqlParameter[] param = new SqlParameter[1];
            //param[0] = new SqlParameter("@WebUser_BoPhan", Oid);
            //DataProvider.ExecuteNonQuery("dbo.spd_WebChamCong_CapNhatIDWebUser", CommandType.StoredProcedure, param);
        }

    }

}
