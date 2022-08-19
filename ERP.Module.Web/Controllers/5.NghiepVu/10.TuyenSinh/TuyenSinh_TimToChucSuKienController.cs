using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using DevExpress.Web;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Enum.Systems;
using DevExpress.ExpressApp.Web;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_TimToChucSuKienController : ViewController
    {
        DetailView _view;
        DuyetToChucSuKien _duyetToChucSuKien;

        public TuyenSinh_TimToChucSuKienController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnViewControlsCreated()
        {
            _view = View as DetailView;
            _duyetToChucSuKien = View.CurrentObject as DuyetToChucSuKien;

            if (_view != null && _duyetToChucSuKien != null)
            {
                ControlViewItem item = ((DetailView)View).FindItem("btnSearch") as ControlViewItem;
                {
                    if (item != null)
                    {
                        //Khởi tạo định dạng Captcha
                        if (item.Control != null)
                        {
                            item_ControlCreated(item, EventArgs.Empty);
                        }
                        else
                        {
                            item.ControlCreated += item_ControlCreated;
                        }
                    }
                }
            }
        }
        void item_ControlCreated(object sender, EventArgs e)
        {
            //
            ASPxButton btnSearch = ((ControlViewItem)sender).Control as ASPxButton;
            if (btnSearch != null)
            {
                btnSearch.Text = "Tìm kiếm";
                btnSearch.Width = 80;
                btnSearch.Click += (obj, ea) =>
                {
                    try
                    {
                        //
                        if (_duyetToChucSuKien != null)
                        {
                            //
                            IObjectSpace obs = View.ObjectSpace;
                            //Refesh list
                            if (_duyetToChucSuKien.ListChiTietDuyetToChucSuKien == null)
                                _duyetToChucSuKien.ListChiTietDuyetToChucSuKien = new XPCollection<ChiTietDuyetToChucSuKien>(((XPObjectSpace)obs).Session, false);
                            else
                                _duyetToChucSuKien.ListChiTietDuyetToChucSuKien.Reload();
                            //
                            DataTable dt = new DataTable();
                            SqlParameter[] param = new SqlParameter[3];
                            param[0] = new SqlParameter("@Criteria", StringHelpers_Web.XuLyDieuKien_ToChucSuKien(_duyetToChucSuKien.DieuKienTimKiem, obs, true));
                            param[1] = new SqlParameter("@Type", 1);
                            param[2] = new SqlParameter("@CongTy", Common.CongTy(((XPObjectSpace)obs).Session).Oid);
                            //
                            using (SqlCommand cmd = new SqlCommand("spd_TuyenSinh_ToChucSuKienCanDuyet"))
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
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        if (dr != null)
                                        {
                                            ToChucSuKien suKien = obs.GetObjectByKey<ToChucSuKien>(dr["Oid"]);
                                            //
                                            if (suKien != null)
                                            {
                                                ChiTietDuyetToChucSuKien data = new ChiTietDuyetToChucSuKien(((XPObjectSpace)obs).Session);
                                                data.Oid = new Guid(dr["Oid"].ToString());
                                                data.ToChucSuKien = suKien;
                                                data.NgayThucHien = suKien.NgayThucHien;
                                                data.KinhPhi = suKien.KinhPhi;
                                                data.DaDuyet = suKien.DaDuyet;
                                                //
                                                _duyetToChucSuKien.ListChiTietDuyetToChucSuKien.Add(data);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        //
                        View.Refresh();
                    }
                    catch (Exception ex)
                    {
                        string message = "alert('Xảy ra lỗi vui lòng kiểm tra dữ liệu.')";
                        WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
                    }
                };
            }
        }
    }
}