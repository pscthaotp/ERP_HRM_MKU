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

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_TimYKienKhachHangController : ViewController
    {
        DetailView _view;
        XyLyYKien _yKien;

        public TuyenSinh_TimYKienKhachHangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnViewControlsCreated()
        {
            _view = View as DetailView;
            _yKien = View.CurrentObject as XyLyYKien;

            if (_view != null && _yKien != null)
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
                        ChiTietXyLyYKien data;//
                        if (_yKien != null)
                        {

                            //
                            IObjectSpace obs = View.ObjectSpace;

                            //Refesh list
                            if (_yKien.ListChiTietXyLyYKien == null)
                                _yKien.ListChiTietXyLyYKien = new XPCollection<ChiTietXyLyYKien>(((XPObjectSpace)obs).Session, false);
                            else
                                _yKien.ListChiTietXyLyYKien.Reload();

                            //
                            DataTable dt = new DataTable();
                            SqlParameter[] param = new SqlParameter[3];
                            param[0] = new SqlParameter("@Criteria", StringHelpers_Web.XuLyDieuKien_YKien(_yKien.DieuKienTimKiem, obs, true));
                            param[1] = new SqlParameter("@Type", 1);
                            param[2] = new SqlParameter("@CongTy", Common.CongTy(((XPObjectSpace)obs).Session).Oid);
                            ////
                            using (SqlCommand cmd = new SqlCommand("spd_TuyenSinh_TimDanhSachYKienCanXuLy"))
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
                                            //
                                            YKienKhachHang yKien = obs.GetObjectByKey<YKienKhachHang>(new Guid(dr["Oid"].ToString()));
                                            if (yKien != null)
                                            {
                                                data = new ChiTietXyLyYKien(((XPObjectSpace)obs).Session);
                                                //
                                                data.Oid = new Guid(dr["Oid"].ToString());
                                                data.ThongTinKhachHang = obs.GetObjectByKey<ThongTinKhachHang>(yKien.ThongTinKhachHang.Oid);
                                                data.LoaiYKien = yKien.LoaiYKien;
                                                data.TrangThai = yKien.TrangThai;
                                                data.NoiDung = yKien.NoiDung;
                                                data.NgayTiepNhan = yKien.NgayTiepNhan;
                                                //
                                                _yKien.ListChiTietXyLyYKien.Add(data);
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
