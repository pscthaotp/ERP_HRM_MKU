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
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.NhacViec
{
    [NonPersistent]
    [DefaultClassOptions]
    [ImageName("BO_Money2")]
    [ModelDefault("Caption", "Nhắc việc - Hết hạn hợp đồng")]
    public class NhacViec_HetHanHopDong : BaseObject
    {
        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ hết hạn hợp đồng")]
        public XPCollection<NhacViec_ChiTietHetHanHopDong> ChiTietHetHanHopDongList
        { get; set; }


        public NhacViec_HetHanHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (Common.CauHinhChung_GetCauHinhChung != null && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec != null
             && Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.TheoDoiHetHanHopDong)
            {
                ChiTietHetHanHopDongList = new XPCollection<NhacViec_ChiTietHetHanHopDong>(Session, false);
                //
                DateTime tuNgay = Common.GetServerCurrentTime().SetTime(SetTimeEnum.StartDay);
                DateTime denNgay = tuNgay.SetTime(SetTimeEnum.EndDay).AddDays(Common.CauHinhChung_GetCauHinhChung.CauHinhNhacViec.SoThangTruocKhiHetHanHopDong);
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@TuNgay", tuNgay);
                param[1] = new SqlParameter("@DenNgay", denNgay);
                param[2] = new SqlParameter("@BoPhanPhanQuyen", Common.System_GetDeparment_Role_ByUser());
                //
                using (DataTable dt = DataProvider.GetDataTable("spd_NhacViec_NhanSu_DanhSachHetHanHopDong", CommandType.StoredProcedure, param))
                {
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            NhacViec_ChiTietHetHanHopDong obj = new NhacViec_ChiTietHetHanHopDong(Session);
                            if (!item.IsNull("ThongTinNhanVien"))
                            {
                                ThongTinNhanVien nhanVien = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid=?", new Guid(item["ThongTinNhanVien"].ToString())));
                                if (nhanVien != null)
                                {
                                    obj.ThongTinNhanVien = nhanVien;
                                    obj.BoPhan = nhanVien.BoPhan;
                                    obj.HopDongHienTai = nhanVien.HopDongHienTai;
                                    obj.NgayHetHan = Convert.ToDateTime(item["NgayHetHan"].ToString());
                                    LoaiHopDong loaiHopDong = Session.FindObject<LoaiHopDong>(CriteriaOperator.Parse("Oid=?", new Guid(item["LoaiHopDong"].ToString())));
                                    if (loaiHopDong != null)
                                        obj.LoaiHopDong = loaiHopDong;
                                    obj.GhiChu = "Hết hạn hợp đồng ngày " + obj.NgayHetHan.ToString("d");
                                }
                            }
                            //
                            ChiTietHetHanHopDongList.Add(obj);
                        }
                    }
                }
            }
        }
    }

}
