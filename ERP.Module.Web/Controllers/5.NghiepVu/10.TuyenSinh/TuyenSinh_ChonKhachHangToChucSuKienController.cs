using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.HocSinh.Lops;
using DevExpress.Xpo;
using ERP.Module.Extends;
using System.Data;
using System.Linq;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChonKhachHangToChucSuKienController : ViewController
    {
        private ToChucSuKien _toChucSuKien;
        ToChucSuKien_ChonKhachHang _chonKH;
        IObjectSpace _obs;
        public TuyenSinh_ChonKhachHangToChucSuKienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {

            //
            _toChucSuKien = View.CurrentObject as ToChucSuKien;
            if (_toChucSuKien != null)
            {
                //
                _obs = Application.CreateObjectSpace();
                //
                _chonKH = _obs.CreateObject<ToChucSuKien_ChonKhachHang>();
                DetailView view = Application.CreateDetailView(_obs, _chonKH);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_chonKH != null && _chonKH.DieuKienTimKiem != null)
            {
                if (_toChucSuKien != null)
                {
                    //
                    DataTable dt = new DataTable();
                    SqlParameter[] param = new SqlParameter[3];
                    var congty = Common.SecuritySystemUser_GetCurrentUser().CongTy;
                    if (congty != null)
                        if (congty.Oid.Equals(Config.KeyTanPhu))
                            param[0] = new SqlParameter("@Criteria", StringHelpers_Web.XuLyDieuKien_ToChucSuKien_TP(_chonKH.DieuKienTimKiem, _obs, true));
                        else
                            param[0] = new SqlParameter("@Criteria", StringHelpers_Web.XuLyDieuKien_TuVanTuyenSinh(_chonKH.DieuKienTimKiem, _obs, true));

                    if (congty.Oid.Equals(Config.KeyTanPhu))
                        param[1] = new SqlParameter("@Type", 2);
                    else
                        param[1] = new SqlParameter("@Type", 1);
                   
                    param[2] = new SqlParameter("@CongTy", Common.CongTy(((XPObjectSpace)_obs).Session).Oid);
                    //

                    using (SqlCommand cmd = new SqlCommand("spd_TuyenSinh_TimDanhSachKhachHangToChucSuKien"))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (param != null)
                            cmd.Parameters.AddRange(param);
                        //
                        cmd.Connection = DataProvider.GetConnection();
                        //
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                            //
                            try
                            {
                                Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                                //
                                foreach (DataRow dr in dt.Rows)
                                {
                                    if (dr != null && dr["Oid"] != null)
                                    {
                                        ThongTinKhachHang khachHang = session.GetObjectByKey<ThongTinKhachHang>(new Guid(dr["Oid"].ToString()));
                                        //
                                        if (khachHang != null)
                                        {
                                            //LinQ - parse về dạng của list
                                            var kh = from x in _toChucSuKien.ListChiTietToChucSuKien
                                                     where (x.ThongTinKhachHang.Oid == khachHang.Oid && x.ToChucSuKien.Oid == _toChucSuKien.Oid)
                                                     select x;
                                            var chitiet = kh.FirstOrDefault() as ChiTietToChucSuKien;
                                            //Kiểm tra list nếu đã có khách hàng trong sự kiện rồi thì thêm vào list nữa
                                            if (chitiet == null)
                                            {
                                                ChiTietToChucSuKien data = new ChiTietToChucSuKien(session);
                                                data.ThongTinKhachHang = khachHang;
                                                data.ToChucSuKien = _toChucSuKien;
                                                //
                                                _toChucSuKien.ListChiTietToChucSuKien.Add(data);
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                //
                                string message = "alert('Xảy ra lỗi vui lòng kiểm tra dữ liệu.')";
                                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                            }

                        }
                        View.Refresh();
                    };
                }
            }
        }
        private void TuyenSinh_ChonKhachHangToChucSuKienController_Activated(object sender, EventArgs e)
        {
            //
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<ToChucSuKien>();
        }
    }
}
