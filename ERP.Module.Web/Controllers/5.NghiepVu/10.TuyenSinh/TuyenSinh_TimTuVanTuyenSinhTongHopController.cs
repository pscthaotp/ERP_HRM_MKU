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
    public partial class TuyenSinh_TimTuVanTuyenSinhTongHopController : ViewController
    {
        DetailView _view;
        TuVanTuyenSinh_TongHop _tuVanTuyenSinh;

        public TuyenSinh_TimTuVanTuyenSinhTongHopController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnViewControlsCreated()
        {
            _view = View as DetailView;
            _tuVanTuyenSinh = View.CurrentObject as TuVanTuyenSinh_TongHop;

            if (_view != null && _tuVanTuyenSinh != null)
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
                        ChiTietTuVanTuyenSinh_TongHop data;//
                        if (_tuVanTuyenSinh != null)
                        {

                            //
                            IObjectSpace obs = View.ObjectSpace;
                            //Refesh list
                            if (_tuVanTuyenSinh.ListChiTietTuVanTuyenSinh == null)
                                _tuVanTuyenSinh.ListChiTietTuVanTuyenSinh = new XPCollection<ChiTietTuVanTuyenSinh_TongHop>(((XPObjectSpace)View.ObjectSpace).Session, false);
                            else
                                _tuVanTuyenSinh.ListChiTietTuVanTuyenSinh.Reload();

                            //
                            DataTable dt = new DataTable();
                            SqlParameter[] param = new SqlParameter[3];
                            param[0] = new SqlParameter("@Criteria", StringHelpers_Web.XuLyDieuKien_TuVanTuyenSinh(_tuVanTuyenSinh.DieuKienTimKiem, obs, true));
                            param[1] = new SqlParameter("@Type", 1);
                            param[2] = new SqlParameter("@CongTy", Common.CongTy(((XPObjectSpace)View.ObjectSpace).Session).Oid);
                            ////
                            using (SqlCommand cmd = new SqlCommand("spd_TuyenSinh_TimDanhSachKhachHangCanTuVan"))
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
                                            data = new ChiTietTuVanTuyenSinh_TongHop(((XPObjectSpace)View.ObjectSpace).Session);
                                            data.Oid = new Guid(dr["Oid"].ToString());
                                            data.MaKhachHang = dr["MaKhachHang"].ToString();
                                            data.HoTen = dr["HoTen"].ToString();
                                            data.GioiTinh = dr["GioiTinh"].ToString().Equals("0") ? GioiTinhEnum.Nam : GioiTinhEnum.Nu;
                                            data.Email = dr["Email"].ToString();
                                            data.DienThoai = dr["DienThoaiDiDong"].ToString();
                                            //
                                            string ghiChu = string.Empty;
                                            if (!string.IsNullOrEmpty(dr["GhiChu_SMS"].ToString()))
                                                ghiChu = "SMS: [" + dr["GhiChu_SMS"].ToString() + "] ; ";
                                            if (!string.IsNullOrEmpty(dr["GhiChu_Email"].ToString()))
                                                ghiChu += "Email: [" + dr["GhiChu_Email"].ToString() + "] ; ";
                                            if (!string.IsNullOrEmpty(dr["GhiChu_DienThoai"].ToString()))
                                                ghiChu += "Điện thoại: [" + dr["GhiChu_DienThoai"].ToString() + "] ; ";
                                            if (!string.IsNullOrEmpty(dr["GhiChu_TrucTiep"].ToString()))
                                                ghiChu += "Trực tiếp: [" + dr["GhiChu_TrucTiep"].ToString() + "] ; ";
                                            data.GhiChu = ghiChu;
                                            //
                                            _tuVanTuyenSinh.ListChiTietTuVanTuyenSinh.Add(data);
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
