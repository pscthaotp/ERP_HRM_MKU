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
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;

using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;
using System.Data;
using ERP.Module.Enum.TuyenSinh;
using DevExpress.XtraEditors;
using ERP.Module.Extends;

using ERP.Module.NonPersistentObjects.NgoaiKhoa;

namespace ERP.Module.Win.Controllers.NghiepVu.TuyenSinh
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class DangKyNgoaiKhoa_TimKiemConTroller : ViewController
    {
        IObjectSpace iob;
        TimKiem_NgoaiKhoa tk;
        public DangKyNgoaiKhoa_TimKiemConTroller()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }

        private void btTimKiem_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            tk = View.CurrentObject as TimKiem_NgoaiKhoa;
            if (tk != null)
            {
                //using (DialogUtil.Wait())
                {
                    iob = Application.CreateObjectSpace();
                    Session ses = (((XPObjectSpace)View.ObjectSpace).Session);

                    SqlParameter[] parameter = new SqlParameter[5];

                    parameter[0] = new SqlParameter("@CongTy", tk.CongTy.Oid);
                    parameter[1] = new SqlParameter("@NamHoc", tk.NamHoc.Oid);
                    parameter[2] = new SqlParameter("@LoaiPhi", tk.LoaiPhi != null ? tk.LoaiPhi.Oid : Guid.Empty);
                    parameter[3] = new SqlParameter("@Lop", tk.Lop != null ? tk.Lop.Oid : Guid.Empty);
                    parameter[4] = new SqlParameter("@TuNgay", tk.DangKyTuNgay != DateTime.MinValue ? tk.DangKyTuNgay.ToString("dd/MM/yyyy") : "");
                    DataTable dt = DataProvider.GetDataTable("spd_DangKyNgoaiKhoa_TimKiemDangKy", System.Data.CommandType.StoredProcedure, parameter);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        tk.ListDangKyNgoaiKhoa.Reload();
                        foreach (DataRow r in dt.Rows)
                        {
                            DangKyNgoaiKhoa_DanhSachHocSinh ct = new DangKyNgoaiKhoa_DanhSachHocSinh(ses);
                            ct.DangKyNgoaiKhoa = new Guid(r["Oid"].ToString());
                            ct.HocSinh = r["HoTen"].ToString();
                            //ct.MaQuanLy = r["MaQuanLy"].ToString();
                            ct.Lop = r["TenLop"].ToString();
                            ct.LoaiPhi = r["TenLoaiPhi"].ToString();
                            if (r["TenLopNgoaiKhoa"].ToString() != string.Empty)
                                ct.LopNgoaiKhoa = r["TenLopNgoaiKhoa"].ToString();
                            ct.DangKyTuNgay = Convert.ToDateTime(r["DangKyTuNgay"].ToString());
                            if (r["DangKyDenNgay"].ToString() != string.Empty)
                                ct.DangKyDenNgay = Convert.ToDateTime(r["DangKyDenNgay"].ToString());
                            if (r["NgayDuyet"].ToString() != string.Empty)
                                ct.NgayDuyet = Convert.ToDateTime(r["NgayDuyet"].ToString());
                            if (r["NgungGiaHanTu"].ToString() != string.Empty)
                                ct.NgungGiaHanTu = Convert.ToDateTime(r["NgungGiaHanTu"].ToString());
                            if (Convert.ToInt32(r["PhatSinhDotXuat"]) == 0)
                                ct.PhatSinhDotXuat = false;
                            else
                                ct.PhatSinhDotXuat = true;
                            if (Convert.ToInt32(r["TrangThaiNgoaiKhoa"]) == 0)
                                ct.TrangThaiNgoaiKhoa = TrangThaiNgoaiKhoaEnum.KhongChapNhan;
                            else
                                if (Convert.ToInt32(r["TrangThaiNgoaiKhoa"]) == 1)
                                    ct.TrangThaiNgoaiKhoa = TrangThaiNgoaiKhoaEnum.ChoXepLop;
                                else
                                    ct.TrangThaiNgoaiKhoa = TrangThaiNgoaiKhoaEnum.DaXepLop;
                            
                            tk.ListDangKyNgoaiKhoa.Add(ct);
                        }
                    }
                    //else
                    //{
                    //    XtraMessageBox.Show("Không tìm thấy thông tin đăng ký ngoại khóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
            }
        }
    }
}
