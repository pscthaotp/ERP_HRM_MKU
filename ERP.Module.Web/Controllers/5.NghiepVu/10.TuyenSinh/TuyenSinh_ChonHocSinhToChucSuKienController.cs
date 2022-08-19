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
    public partial class TuyenSinh_ChonHocSinhToChucSuKienController : ViewController
    {
        private ToChucSuKien _toChucSuKien;
        ToChucSuKien_ChonHocSinh _chonHS;
        IObjectSpace _obs;
        public TuyenSinh_ChonHocSinhToChucSuKienController()
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
                _chonHS = _obs.CreateObject<ToChucSuKien_ChonHocSinh>();
                DetailView view = Application.CreateDetailView(_obs, _chonHS);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_chonHS != null)
            {
                if (_toChucSuKien != null)
                {
                    //
                    DataTable dt = new DataTable();
                    SqlParameter[] param = new SqlParameter[3];
                    var congty = Common.SecuritySystemUser_GetCurrentUser().CongTy;
                    if (congty != null)
                        if (congty.Oid.Equals(Config.KeyTanPhu))
                            param[0] = new SqlParameter("@Criteria", StringHelpers_Web.XuLyDieuKien_HocSinh_TP(_chonHS.DieuKienTimKiem, _obs, true));
                        else
                            param[0] = new SqlParameter("@Criteria", StringHelpers_Web.XuLyDieuKien_HocSinh(_chonHS.DieuKienTimKiem, _obs, true));

                    if (congty.Oid.Equals(Config.KeyTanPhu))
                        param[1] = new SqlParameter("@Type", 2);
                    else
                        param[1] = new SqlParameter("@Type", 1);
                    //if (congty != null)
                        param[2] = new SqlParameter("@Lop",Common.System_GetClass_Role_ByUser());                    //

                    using (SqlCommand cmd = new SqlCommand("spd_TuyenSinh_TimDanhSachHocSinhToChucSuKien"))
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
                                if (!congty.Oid.Equals(Config.KeyTanPhu))
                                {
                                    Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                                    //
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        if (dr != null && dr["Oid"] != null)
                                        {
                                            ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh hocSinh = session.GetObjectByKey<ERP.Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(new Guid(dr["Oid"].ToString()));
                                            //
                                            if (hocSinh != null)
                                            {
                                                //LinQ - parse về dạng của list
                                                var hs = from x in _toChucSuKien.ListDanhSachHocSinh
                                                         where (x.HocSinh.Oid == hocSinh.Oid && x.ToChucSuKien.Oid == _toChucSuKien.Oid)
                                                         select x;
                                                var chitiet = hs.FirstOrDefault() as ChiTietToChucSuKien_HocSinh;
                                                //Kiểm tra list nếu đã có học sinh trong sự kiện rồi thì thêm vào list nữa
                                                if (chitiet == null)
                                                {
                                                    ChiTietToChucSuKien_HocSinh data = new ChiTietToChucSuKien_HocSinh(session);
                                                    data.HocSinh = hocSinh;
                                                    data.Lop = hocSinh.Lop;
                                                    data.ToChucSuKien = _toChucSuKien;
                                                    //
                                                    _toChucSuKien.ListDanhSachHocSinh.Add(data);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Session session = ((XPObjectSpace)View.ObjectSpace).Session;

                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        if (dr != null && dr["MAHOCSINH"] != null)
                                        {

                                            var hs = from x in _toChucSuKien.ListDanhSachHocSinh
                                                     where (x.MAHOCSINH_SIS == dr["MAHOCSINH"].ToString() && x.ToChucSuKien.Oid == _toChucSuKien.Oid)
                                                     select x;
                                            var chitiet = hs.FirstOrDefault() as ChiTietToChucSuKien_HocSinh;
                                            if (chitiet == null)
                                            {
                                                ChiTietToChucSuKien_HocSinh data = new ChiTietToChucSuKien_HocSinh(session);
                                                data.IsMultiRows = true;
                                                data.MAHOCSINH_SIS = dr["MAHOCSINH"].ToString();
                                                data.LOP_SIS = dr["LOPDANGHOC"].ToString();
                                                data.HOTEN_SIS = (dr["HOVALOT"].ToString() + " " + dr["TEN"].ToString()).Replace("  ", " ");
                                                data.HinhThuc = Enum.TuyenSinh.HinhThucGuiSuKienEnum.Email;
                                                data.ToChucSuKien = _toChucSuKien;
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
        private void TuyenSinh_ChonHocSinhToChucSuKienController_Activated(object sender, EventArgs e)
        {
            //
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<ToChucSuKien>();
        }
    }
}
