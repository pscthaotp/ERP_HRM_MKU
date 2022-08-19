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
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.NghiepVu.TuyenSinh;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Commons;
using System.Data;
using DevExpress.ExpressApp.Web;
using System.Data.SqlClient;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_XoaChiTietTuVanTuyenSinh_ThongTinKhachHang_Controller : ViewController
    {
        private ThongTinKhachHang _ThongTinKhachHang;
        private Session _session = null;
        private IObjectSpace _obs = null;
        private string message;
        public TuyenSinh_XoaChiTietTuVanTuyenSinh_ThongTinKhachHang_Controller()
        {
            InitializeComponent();
            TargetViewId = "ThongTinKhachHang_ListChiTietTuVanTuyenSinh_ThongTinKhachHang_ListView";
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View.Id.Equals("ThongTinKhachHang_ListChiTietTuVanTuyenSinh_ThongTinKhachHang_ListView"))
            {
                _obs = Application.CreateObjectSpace();
                _ThongTinKhachHang = ((DetailView)((View.ObjectSpace).Owner)).CurrentObject as ThongTinKhachHang;
                _session = ((XPObjectSpace)_obs).Session;
                
                string DeleteQuery = "";
                foreach(Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang item in View.SelectedObjects)
                {
                    DeleteQuery += ""+item.Get_OidChiTietKhoiLuongGiangDay().ToString()+";";
                }
                
                SqlParameter[] parameter = new SqlParameter[5];
                parameter[0] = new SqlParameter("@ThongTinKhachHang", _ThongTinKhachHang.Oid);
                parameter[1] = new SqlParameter("@NamHoc", _ThongTinKhachHang.NamHoc.Oid);
                parameter[2] = new SqlParameter("@CongTy", _ThongTinKhachHang.CongTy.Oid);
                parameter[3] = new SqlParameter("@StringDelete", DeleteQuery);
                parameter[4] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
                parameter[4].Direction = ParameterDirection.Output;

                var kq = DataProvider.ExecuteNonQuery("spd_TuyenSinh_XoaChiTietTuVanTuyenSinh", CommandType.StoredProcedure, parameter);
                
                string KQ = parameter[4].Value.ToString();
                if (KQ == "SUCCESS")
                {
                    message = "alert('THÀNH CÔNG . Xóa thành công !!!');";
                }
                else
                {
                    message = "alert('LỖI !!! . Xóa không thành công !!!');";
                }

                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                _ThongTinKhachHang = ((DetailView)((View.ObjectSpace).Owner)).CurrentObject as ThongTinKhachHang;
                _ThongTinKhachHang.LoadChiTietTuyenSinh();
                View.Refresh();
                View.ObjectSpace.Refresh();
            }
        }
    }
}
