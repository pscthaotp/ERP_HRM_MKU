using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.Registers;
using System.Data.SqlClient;
using System.Data;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thôi việc")]
    public class QuyetDinhThoiViec : QuyetDinhCaNhan
    {
        private TinhTrang _TinhTrangCu;
        private DateTime _ThoiHanBanGiao;
        private DateTime _NghiViecTuNgay;
        private string _LyDo;
        private HinhThucThoiViec _HinhThucThoiViec;

        [ModelDefault("Caption", "Hình thức thôi việc")]
        public HinhThucThoiViec HinhThucThoiViec
        {
            get
            {
                return _HinhThucThoiViec;
            }
            set
            {
                SetPropertyValue("HinhThucThoiViec", ref _HinhThucThoiViec, value);
            }
        }

        [ModelDefault("Caption", "Nghỉ việc từ ngày")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime NghiViecTuNgay
        {
            get
            {
                return _NghiViecTuNgay;
            }
            set
            {
                SetPropertyValue("NghiViecTuNgay", ref _NghiViecTuNgay, value);
            }
        }

        [ModelDefault("Caption", "Thời hạn bàn giao")]
        public DateTime ThoiHanBanGiao
        {
            get
            {
                return _ThoiHanBanGiao;
            }
            set
            {
                SetPropertyValue("ThoiHanBanGiao", ref _ThoiHanBanGiao, value);
            }
        }     

        [Size(500)]
        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        //Lưu vết
        [Browsable(false)]
        public TinhTrang TinhTrangCu
        {
            get
            {
                return _TinhTrangCu;
            }
            set
            {
                SetPropertyValue("TinhTrangCu", ref _TinhTrangCu, value);
            }
        }

        public QuyetDinhThoiViec(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhThoiViec;
        }

        protected override void AfterNhanVienChanged()
        {
            TinhTrangCu = ThongTinNhanVien.TinhTrang;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                // Gọi hàm xử lý
                Register.Func_QuyetDinhThoiViec.Save(Session, this);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                // Gọi hàm xử lý
                Register.Func_QuyetDinhThoiViec.Delete(Session, this);
            }
            //
            base.OnDeleting();
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (!IsDeleted)
            {
                if (ThongTinNhanVien.TinhTrang.DaNghiViec == true && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@NgayHieuLuc", NgayHieuLuc);
                    param[1] = new SqlParameter("@IDHinhThucNghi", "00000000-0000-0000-0000-000000000022");
                    param[2] = new SqlParameter("@IDNhanVien", ThongTinNhanVien.Oid);
                    param[3] = new SqlParameter("@CongTy", ThongTinNhanVien.CongTy.Oid);
                    param[4] = new SqlParameter("@TrangThaiXoa", false);
                    DataProvider.ExecuteNonQuery("spd_WebChamCong_CapNhatHinhThucNghiTheoQuyetDinh", CommandType.StoredProcedure, param);
                }
            }
            else
            {
                if (ThongTinNhanVien.TinhTrang.DaNghiViec == false)
                {
                    SqlParameter[] param = new SqlParameter[5];
                    param[0] = new SqlParameter("@NgayHieuLuc", NgayHieuLuc);
                    param[1] = new SqlParameter("@IDHinhThucNghi", "00000000-0000-0000-0000-000000000001");
                    param[2] = new SqlParameter("@IDNhanVien", ThongTinNhanVien.Oid);
                    param[3] = new SqlParameter("@CongTy", ThongTinNhanVien.CongTy.Oid);
                    param[4] = new SqlParameter("@TrangThaiXoa", true);
                    DataProvider.ExecuteNonQuery("spd_WebChamCong_CapNhatHinhThucNghiTheoQuyetDinh", CommandType.StoredProcedure, param);
                }
            }
        }
    }
}