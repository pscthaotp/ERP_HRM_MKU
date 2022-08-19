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
using DevExpress.ExpressApp.Web;
using ERP.Module.Commons;
using System.Data;
using System.Data.SqlClient;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChinhSuaChiTietTuVanTuyenSinh_ThongTinKhachHang_Controller : ViewController
    {
        private ChiTietTuVanTuyenSinh _ChiTietTuVanTuyenSinh;
        private Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang_ChinhSua _Non_ChiTietTuVanTuyenSinh;
        private Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang _Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang;
        private ThongTinKhachHang _ThongTinKhachHang;
        private Session _session = null;
        private IObjectSpace _obs = null;
        private Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang _Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang_temp;
        private string message;
        public TuyenSinh_ChinhSuaChiTietTuVanTuyenSinh_ThongTinKhachHang_Controller()
        {
            InitializeComponent();
            TargetViewId = "ThongTinKhachHang_ListChiTietTuVanTuyenSinh_ThongTinKhachHang_ListView";
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {


            _ThongTinKhachHang = ((DetailView)((View.ObjectSpace).Owner)).CurrentObject as ThongTinKhachHang;

            try
            {
                if (_Non_ChiTietTuVanTuyenSinh != null)
                {
                    SqlParameter[] parameter = new SqlParameter[8];
                    parameter[0] = new SqlParameter("@TuVanTuyenSinh", _Non_ChiTietTuVanTuyenSinh.TuVanTuyenSinh.Oid);
                    parameter[1] = new SqlParameter("@NgayTuVan", _Non_ChiTietTuVanTuyenSinh.NgayTuVan.ToString("yyyy-MM-dd HH:mm:ss"));
                    parameter[2] = new SqlParameter("@HinhThucTuVan", _Non_ChiTietTuVanTuyenSinh.HinhThucTuVan.GetHashCode());
                    parameter[3] = new SqlParameter("@NoiDung", _Non_ChiTietTuVanTuyenSinh.NoiDung);
                    parameter[4] = new SqlParameter("@LoiaKhachHang", _Non_ChiTietTuVanTuyenSinh.LoaiKhachHang.Oid);
                    parameter[5] = new SqlParameter("@ChiTietTuVanTuyensinh", _Non_ChiTietTuVanTuyenSinh.Get_OidChiTietKhoiLuongGiangDay());
                    parameter[6] = new SqlParameter("@ThongTinKhachHang", _ThongTinKhachHang.Oid);
                    parameter[7] = new SqlParameter("@KQ", SqlDbType.NVarChar, 200);
                    parameter[7].Direction = ParameterDirection.Output;
                    var kq = DataProvider.ExecuteNonQuery("spd_TuyenSinh_ChinhSuaChiTietTuVanTuyenSinh", CommandType.StoredProcedure, parameter);

                    string KQ = parameter[7].Value.ToString();

                    if (KQ == "SUCCESS")
                    {
                        message = "alert('THÀNH CÔNG . Chỉnh sửa thành công !!!');";
                    }
                    else
                    {
                        message = "alert('LỖI !!! . Chỉnh sửa không thành công');";
                    }
                }
            }
            catch (Exception ex) { }


            _ThongTinKhachHang = ((DetailView)((View.ObjectSpace).Owner)).CurrentObject as ThongTinKhachHang;
            _ThongTinKhachHang.LoadChiTietTuyenSinh();
            WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
            View.Refresh();
            View.ObjectSpace.Refresh();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View.Id.Equals("ThongTinKhachHang_ListChiTietTuVanTuyenSinh_ThongTinKhachHang_ListView"))
            {
                string message;
                if (View.SelectedObjects.Count > 1 || View.SelectedObjects.Count <= 0)
                {

                    message = "alert('Bạn không thể chọn trên 2 dòng hoặc không chọn dòng nào')";
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                    
                }
                else
                {
                    _Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang = View.SelectedObjects[0] as Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang;
                    if (_Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang != null)
                    {
                        _obs = Application.CreateObjectSpace();
                        _session = ((XPObjectSpace)_obs).Session;
                        
                        
                        _Non_ChiTietTuVanTuyenSinh = new Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang_ChinhSua(_session);
                        _Non_ChiTietTuVanTuyenSinh.Set_OidChiTietKhoiLuongGiangDay(_Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang.Get_OidChiTietKhoiLuongGiangDay());
                        _Non_ChiTietTuVanTuyenSinh.HinhThucTuVan = _Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang.HinhThucTuVan;
                        _Non_ChiTietTuVanTuyenSinh.LoaiKhachHang = _Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang.LoaiKhachHang;
                        _Non_ChiTietTuVanTuyenSinh.NgayTuVan = _Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang.NgayTuVan;
                        _Non_ChiTietTuVanTuyenSinh.NoiDung = _Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang.NoiDung;
                        _Non_ChiTietTuVanTuyenSinh.TuVanTuyenSinh = _Non_ChiTietTuVanTuyenSinh_ThongTinKhachHang.TuVanTuyenSinh;
                        _Non_ChiTietTuVanTuyenSinh.SecuritySystemUser = Common.SecuritySystemUser_GetCurrentUser(_session);
                        
                        
                        DetailView detail = Application.CreateDetailView(_obs, _Non_ChiTietTuVanTuyenSinh);
                        detail.ViewEditMode = ViewEditMode.Edit;
                        e.View = detail;
                    }
                }
            }
        }
    }
}
