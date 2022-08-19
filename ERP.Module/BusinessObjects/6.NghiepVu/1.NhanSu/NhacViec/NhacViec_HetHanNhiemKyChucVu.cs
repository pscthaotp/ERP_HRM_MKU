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
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

//
namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nhắc việc - Hết hạn nhiệm kỳ chức vụ")]
    public class NhacViec_HetHanNhiemKyChucVu : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ hết nhiệm kỳ chức vụ")]
        public XPCollection<NhacViec_ChiTietHetHanNhiemKyChucVu> ChiTietHetHanNhiemKyChucVuList
        { get; set; }


        public NhacViec_HetHanNhiemKyChucVu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Common.CauHinhChung_GetCauHinhChung != null && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec != null
               && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.TheoDoiHetNhiemKyChucVu)
            {
                ChiTietHetHanNhiemKyChucVuList = new XPCollection<NhacViec_ChiTietHetHanNhiemKyChucVu>(Session, false);
                //
                DateTime tuNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndDay).AddDays(Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.SoThangTruocKhiNangLuong);
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                param[2] = new SqlParameter("@BoPhanPhanQuyen", Common.System_GetDeparment_Role_ByUser());
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_NhanSu_DanhSachHetHanNhiemKyChucVu", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietHetHanNhiemKyChucVu obj = new NhacViec_ChiTietHetHanNhiemKyChucVu(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    QuyetDinh quyetdinh = Session.FindObject<QuyetDinh>(CriteriaOperator.Parse("Oid=? ", new Guid(item["QuyetDinh"].ToString())));
                                    if (quyetdinh != null)
                                        obj.QuyetDinh = quyetdinh;
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.NgayBoNhiem = Convert.ToDateTime(item["NgayBoNhiemChucVu"].ToString());
                                    obj.NgayHetNhiemKy = Convert.ToDateTime(item["NgayHetHanNhiemKy"].ToString());
                                    obj.GhiChu = "Hết nhiệm kỳ vào ngày " + obj.NgayHetNhiemKy.ToString("d"); ;
                                }
                            }

                            ChiTietHetHanNhiemKyChucVuList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
