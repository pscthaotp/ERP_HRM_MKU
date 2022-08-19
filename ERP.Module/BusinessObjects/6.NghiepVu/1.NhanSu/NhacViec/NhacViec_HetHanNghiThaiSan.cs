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

namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nhắc việc - Hết hạn nghỉ thai sản")]
    public class NhacViec_HetHanNghiThaiSan : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ hết hạn nghỉ thai sản")]
        public XPCollection<NhacViec_ChiTietHetHanNghiThaiSan> ChiTietHetHanNghiThaiSanList
        { get; set; }


        public NhacViec_HetHanNghiThaiSan(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Common.CauHinhChung_GetCauHinhChung != null && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec != null
             && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.TheoDoiHetHanNghiBHXH)
            {
                ChiTietHetHanNghiThaiSanList = new XPCollection<NhacViec_ChiTietHetHanNghiThaiSan>(Session, false);
                //
                DateTime tuNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndDay).AddDays(Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.SoThangTruocKhiHetHanNghiThaiSan);
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                param[2] = new SqlParameter("@BoPhanPhanQuyen", Common.System_GetDeparment_Role_ByUser());
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_NhanSu_DanhSachHetHanNghiThaiSan", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietHetHanNghiThaiSan obj = new NhacViec_ChiTietHetHanNghiThaiSan(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.TinhTrang = nhanVien.TinhTrang;
                                    obj.NgayNghiThaiSan = Convert.ToDateTime(item["NgayNghiThaiSan"].ToString());
                                    obj.NgayHetHanNghiThaiSan = Convert.ToDateTime(item["NgayHetHanNghiThaiSan"].ToString());
                                    obj.GhiChu = "Hết hạn nghỉ thai sản vào ngày " + obj.NgayHetHanNghiThaiSan.ToString("d");
                                }
                            }
                            //
                            ChiTietHetHanNghiThaiSanList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
