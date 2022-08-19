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
    [ModelDefault("Caption", "Nhắc việc - Đến tuổi nghỉ hưu")]
    public class NhacViec_DenTuoiNghiHuu : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ đến tuổi nghỉ hưu")]
        public XPCollection<NhacViec_ChiTietDenTuoiNghiHuu> ChiTietDenTuoiNghiHuuList
        { get; set; }


        public NhacViec_DenTuoiNghiHuu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Common.CauHinhChung_GetCauHinhChung != null && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec != null
              && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.TheoDoiDenTuoiNghiHuu)
            {
                ChiTietDenTuoiNghiHuuList = new XPCollection<NhacViec_ChiTietDenTuoiNghiHuu>(Session, false);
                //
                DateTime tuNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndDay).AddDays(Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.SoThangTruocKhiNghiHuu);
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                param[2] = new SqlParameter("@BoPhanPhanQuyen", Common.System_GetDeparment_Role_ByUser());
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_NhanSu_DanhSachDenHanNghiHuu", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietDenTuoiNghiHuu obj = new NhacViec_ChiTietDenTuoiNghiHuu(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.GioiTinh = nhanVien.GioiTinh;
                                    obj.NgaySinh = nhanVien.NgaySinh;
                                    obj.NgayNghiHuu = Convert.ToDateTime(item["NgayNghiHuu"].ToString());
                                    obj.GhiChu = "Sẽ nghỉ hưu vào ngày " + obj.NgayNghiHuu.ToString("d");
                                }
                            }
                            //
                            ChiTietDenTuoiNghiHuuList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
