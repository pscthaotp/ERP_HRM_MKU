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
using ERP.Module.Enum.Systems;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

//
namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nhắc việc - Đến hạn nâng lương")]
    public class NhacViec_DenHanNangLuong : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ đến hạn nâng")]
        public XPCollection<NhacViec_ChiTietDenHanNangLuong> ChiTietDenHangNangLuongList
        { get; set; }
        

        public NhacViec_DenHanNangLuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Common.CauHinhChung_GetCauHinhChung != null && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec != null
               && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.TheoDoiDenHanNangLuong)
            {
                ChiTietDenHangNangLuongList = new XPCollection<NhacViec_ChiTietDenHanNangLuong>(Session, false);
                //
                DateTime tuNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndDay).AddDays(Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.SoThangTruocKhiNangLuong);
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                param[2] = new SqlParameter("@BoPhanPhanQuyen", Common.System_GetDeparment_Role_ByUser());
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_NhanSu_DanhSachDenHanNangLuong", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietDenHanNangLuong obj = new NhacViec_ChiTietDenHanNangLuong(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.NgayHuongLuong = nhanVien.NhanVienThongTinLuong.NgayHuongLuong;
                                    obj.MocNangLuong = nhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh != DateTime.MinValue ? nhanVien.NhanVienThongTinLuong.MocNangLuongDieuChinh : nhanVien.NhanVienThongTinLuong.MocNangLuongLanSau;
                                    obj.NgachLuong = nhanVien.NhanVienThongTinLuong.NgachLuong;
                                    obj.BacLuong = nhanVien.NhanVienThongTinLuong.BacLuong;
                                    obj.PhanTramVuotKhung = nhanVien.NhanVienThongTinLuong.VuotKhung;
                                    obj.GhiChu = "Đến hạn nâng lương vào ngày " + obj.MocNangLuong.ToString("d"); ;
                                }
                            }

                            ChiTietDenHangNangLuongList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
