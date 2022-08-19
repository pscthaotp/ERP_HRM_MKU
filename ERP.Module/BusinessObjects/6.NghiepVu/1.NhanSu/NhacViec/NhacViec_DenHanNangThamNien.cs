using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.BaseImpl;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Enum.Systems;

//
namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nhắc việc - Đến hạng nâng thâm niên")]
    public class NhacViec_DenHanNangThamNien : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ đến hạn nâng thâm niên")]
        public XPCollection<NhacViec_ChiTietDenHanNangThamNien> ChiTietDenHangNangThamNienList
        { get; set; }


        public NhacViec_DenHanNangThamNien(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Common.CauHinhChung_GetCauHinhChung != null && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec != null
               && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.TheoDoiDenHanNangThamNien)
            {
                ChiTietDenHangNangThamNienList = new XPCollection<NhacViec_ChiTietDenHanNangThamNien>(Session, false);
                //
                DateTime tuNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndDay).AddDays(Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.SoThangTruocKhiNangThamNien);
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                param[2] = new SqlParameter("@BoPhanPhanQuyen", Common.System_GetDeparment_Role_ByUser());
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_NhanSu_DanhSachDenHanNangThamNien", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietDenHanNangThamNien obj = new NhacViec_ChiTietDenHanNangThamNien(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.NgayTinhThamNien = nhanVien.NhanVienThongTinLuong.NgayHuongThamNien.AddYears(5);
                                    obj.NgachLuong = nhanVien.NhanVienThongTinLuong.NgachLuong;
                                    obj.NgayHuongThamNien = nhanVien.NhanVienThongTinLuong.NgayHuongThamNien;
                                    obj.GhiChu = "Đến hạn nâng thâm niên ngày " + obj.NgayTinhThamNien.ToString("d");
                                }
                            }
                            //
                            ChiTietDenHangNangThamNienList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
