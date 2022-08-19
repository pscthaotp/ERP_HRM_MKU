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
    public partial class TuyenSinh_TimChamSocKhachHangTongHopController : ViewController
    {
        DetailView _view;
        ChamSocKhachHang_TongHop _chamSocKhachHang;

        public TuyenSinh_TimChamSocKhachHangTongHopController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnViewControlsCreated()
        {
            _view = View as DetailView;
            _chamSocKhachHang = View.CurrentObject as ChamSocKhachHang_TongHop;

            if (_view != null && _chamSocKhachHang != null)
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

            List<ThongTinKhachHang> khList = new List<ThongTinKhachHang>();
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
                        ChiTietChamSocKhachHang_TongHop data;//
                        if (_chamSocKhachHang != null)
                        {

                            //
                            IObjectSpace obs = View.ObjectSpace;
                            //Refesh list
                            if (_chamSocKhachHang.ListChiTietChamSoc == null)
                                _chamSocKhachHang.ListChiTietChamSoc = new XPCollection<ChiTietChamSocKhachHang_TongHop>(((XPObjectSpace)View.ObjectSpace).Session, false);
                            else
                                _chamSocKhachHang.ListChiTietChamSoc.Reload();

                            //
                            DataTable dt = new DataTable();
                            SqlParameter[] param = new SqlParameter[3];
                            param[0] = new SqlParameter("@Criteria", StringHelpers_Web.XuLyDieuKien_ChamSocKhachHang_TP(_chamSocKhachHang.DieuKienTimKiem, obs, true));
                            param[1] = new SqlParameter("@Type", 1);
                            param[2] = new SqlParameter("@CongTy", Common.CongTy(((XPObjectSpace)View.ObjectSpace).Session).Oid);
                            ////
                            using (SqlCommand cmd = new SqlCommand("spd_TuyenSinh_TimDanhSachKhachHangChamSocKhachHang"))
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
                                            data = new ChiTietChamSocKhachHang_TongHop(((XPObjectSpace)View.ObjectSpace).Session);
                                            data.Oid = new Guid(dr["OidKH"].ToString());
                                            data.MaKhachHang = dr["MaKhachHang"].ToString();
                                            data.HoTen = dr["HoTen"].ToString();
                                            data.GioiTinh = dr["GioiTinh"].ToString().Equals("0") ? GioiTinhEnum.Nam : GioiTinhEnum.Nu;
                                            data.Email = dr["Email"].ToString();
                                            data.DienThoai = dr["DienThoaiDiDong"].ToString();
                                            if (!string.IsNullOrEmpty(dr["OidHocSinh"].ToString()))
                                                data.OidPhu = new Guid(dr["OidHocSinh"].ToString());
                                            else
                                                data.OidPhu = Guid.Empty;

                                            data.MaHocSinh = dr["MaHocSinh"].ToString();
                                            data.HoTenHocSinh = dr["HoTenHocSinh"].ToString();
                                            data.GioiTinhHocSinh = dr["GioiTinhHocSinh"].ToString().Equals("0") ? GioiTinhEnum.Nam : GioiTinhEnum.Nu;
                                            data.KhoiSIS = dr["KhoiSIS"].ToString();

                                            //
                                            _chamSocKhachHang.ListChiTietChamSoc.Add(data);
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
