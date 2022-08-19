using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using ERP.Module.Commons;
using System.Data;
using DevExpress.ExpressApp.Web;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ThemChiTietTuVanTuyenSinh : ViewController
    {
        private ChiTietTuVanTuyenSinh _ChiTietTuVanTuyenSinh;
        private Non_ChiTietTuVanTuyenSinh _Non_ChiTietTuVanTuyenSinh;
        private ThongTinKhachHang _ThongTinKhachHang;
        private Session _session = null;
        private IObjectSpace _obs = null;
        private string message;
        public TuyenSinh_ThemChiTietTuVanTuyenSinh()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "ThongTinKhachHang_DetailView";
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //_obs.CommitChanges();
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@TuVanTuyenSinh", _Non_ChiTietTuVanTuyenSinh.TuVanTuyenSinh.Oid);
            param[1] = new SqlParameter("@ThongTinKhachHang", _Non_ChiTietTuVanTuyenSinh.ThongTinKhachHang.Oid);
            param[2] = new SqlParameter("@NgayTuVan", _Non_ChiTietTuVanTuyenSinh.NgayTuVan);
            param[3] = new SqlParameter("@HinhThucTuVan", _Non_ChiTietTuVanTuyenSinh.HinhThucTuVan.GetHashCode());
            param[4] = new SqlParameter("@NoiDung", _Non_ChiTietTuVanTuyenSinh.NoiDung);
            param[5] = new SqlParameter("@SecuritySystemUser", _Non_ChiTietTuVanTuyenSinh.SecuritySystemUser.Oid);
            param[6] = new SqlParameter("@LoaiKhachHang", _Non_ChiTietTuVanTuyenSinh.LoaiKhachHang.Oid);
            param[7] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
            param[7].Direction = ParameterDirection.Output;
            DataProvider.ExecuteNonQuery("spd_TuyenSinh_ThemChiTietTuVanTuyenSinh", System.Data.CommandType.StoredProcedure, param);

            string KQ = param[7].Value.ToString();
            if (KQ == "SUCCESS")
            {
                message = "alert('THÀNH CÔNG . Thêm thành công !!!');";
            }
            else
            {
                message = "alert('LỖI !!! . Thêm không thành công ');";
            }
            WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
            View.Refresh();
            View.ObjectSpace.Refresh();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View.Id.Equals("ThongTinKhachHang_DetailView"))
            {
                _ThongTinKhachHang = View.CurrentObject as ThongTinKhachHang;
                if (_ThongTinKhachHang != null)
                {
                    //
                    _obs = Application.CreateObjectSpace();
                    //
                    _session = ((XPObjectSpace)_obs).Session;
                    _Non_ChiTietTuVanTuyenSinh = new Non_ChiTietTuVanTuyenSinh(_session, _ThongTinKhachHang.ListTre,_ThongTinKhachHang.NamHoc,_ThongTinKhachHang.CongTy);
                    _Non_ChiTietTuVanTuyenSinh.ThongTinKhachHang = _ThongTinKhachHang;
                    DetailView detail = Application.CreateDetailView(_obs, _Non_ChiTietTuVanTuyenSinh);
                    detail.ViewEditMode = ViewEditMode.Edit;
                    e.View = detail;
                }
            }
        }
    }
}
