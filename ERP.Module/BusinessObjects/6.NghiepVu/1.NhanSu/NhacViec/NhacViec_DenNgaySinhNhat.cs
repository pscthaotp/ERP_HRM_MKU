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
    [ModelDefault("Caption", "Nhắc việc - Đến ngày sinh nhật")]
    public class NhacViec_DenNgaySinhNhat : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách sinh nhật cán bộ")]
        public XPCollection<NhacViec_ChiTietDenNgaySinhNhat> ChiTietDenNgaySinhNhatList
        { get; set; }


        public NhacViec_DenNgaySinhNhat(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Common.CauHinhChung_GetCauHinhChung != null && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec != null
               && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.TheoDoiSinhNhat)
            {
            ChiTietDenNgaySinhNhatList = new XPCollection<NhacViec_ChiTietDenNgaySinhNhat>(Session, false);
            //
            DateTime ngayHienTai = Common.GetServerCurrentTime();
            DateTime tuNgay = ngayHienTai.SetTime(SetTimeEnum.StartMonth);
            DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndDay).AddDays(Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.SoThangTruocKhiDenSinhNhat);
                //
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TuNgay", tuNgay);
            param[1] = new SqlParameter("@DenNgay", denNgay);
            param[2] = new SqlParameter("@BoPhanPhanQuyen", Common.System_GetDeparment_Role_ByUser());
                //
            using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_NhanSu_DanhSachSinhNhatCanBo", CommandType.StoredProcedure, param))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        NhacViec_ChiTietDenNgaySinhNhat obj = new NhacViec_ChiTietDenNgaySinhNhat(Session);
                        if (!item.IsNull("ThongTinNhanVien"))
                        {
                            ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                            if (nhanVien != null)
                            {
                                obj.ThongTinNhanVien = nhanVien;
                                obj.BoPhan = nhanVien.BoPhan;
                                obj.GioiTinh = nhanVien.GioiTinh;
                                obj.NgaySinh = nhanVien.NgaySinh;
                                try
                                {
                                    if (ngayHienTai.Month == 12 && nhanVien.NgaySinh.Month == 1)
                                        obj.NgaySinhNhat = Convert.ToDateTime(nhanVien.NgaySinh.Day.ToString() + "/" + nhanVien.NgaySinh.Month.ToString() + "/" + (ngayHienTai.Year + 1).ToString());
                                    else
                                        obj.NgaySinhNhat = Convert.ToDateTime(nhanVien.NgaySinh.Day.ToString() + "/" + nhanVien.NgaySinh.Month.ToString() + "/" + ngayHienTai.Year.ToString());

                                    obj.GhiChu = "Sinh nhật cán bộ ngày " + obj.NgaySinhNhat.ToString("d");
                                }
                                catch (Exception ex)
                                {
                                    if (ngayHienTai.Month == 12 && nhanVien.NgaySinh.Month == 1)
                                        obj.NgaySinhNhat = Convert.ToDateTime("01" + "/" + (nhanVien.NgaySinh.Month + 1).ToString() + "/" + (ngayHienTai.Year + 1).ToString());
                                    else
                                        obj.NgaySinhNhat = Convert.ToDateTime("01" + "/" + (nhanVien.NgaySinh.Month + 1).ToString() + "/" + ngayHienTai.Year.ToString());
                                    
                                    obj.GhiChu = "Sinh nhật cán bộ ngày " + obj.NgaySinhNhat.ToString("d");
                                }
                                    
                            }
                        }
                        //
                        ChiTietDenNgaySinhNhatList.Add(obj);
                    }
                }
            }
            }
        }
    }

}
