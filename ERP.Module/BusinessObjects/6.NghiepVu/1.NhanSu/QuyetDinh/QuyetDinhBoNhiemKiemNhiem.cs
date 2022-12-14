using System;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using System.Data.SqlClient;
using System.Data;
//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [ModelDefault("Caption", "Quyết định bổ nhiệm kiêm nhiệm")]
    public class QuyetDinhBoNhiemKiemNhiem : QuyetDinhCaNhan
    {
        private ChucVu _ChucVuMoi;
        private decimal _HSPCChucVuMoi;
        private DateTime _NgayHuongHeSoMoi;
        private DateTime _NgayHetNhiemKy;

        [ImmediatePostData]
        [ModelDefault("Caption", "Chức vụ mới")]
        [RuleRequiredField(DefaultContexts.Save)]
        public ChucVu ChucVuMoi
        {
            get
            {
                return _ChucVuMoi;
            }
            set
            {
                SetPropertyValue("ChucVuMoi", ref _ChucVuMoi, value);
            }
        }

        [ModelDefault("Caption", "HSPC chức vụ mới")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal HSPCChucVuMoi
        {
            get
            {
                return _HSPCChucVuMoi;
            }
            set
            {
                SetPropertyValue("HSPCChucVuMoi", ref _HSPCChucVuMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hưởng HS chức vụ mới")]
        public DateTime NgayHuongHeSoMoi
        {
            get
            {
                return _NgayHuongHeSoMoi;
            }
            set
            {
                SetPropertyValue("NgayHuongHeSoMoi", ref _NgayHuongHeSoMoi, value);
            }
        }

        [ModelDefault("Caption", "Ngày hết nhiệm kỳ")]
        public DateTime NgayHetNhiemKy
        {
            get
            {
                return _NgayHetNhiemKy;
            }
            set
            {
                SetPropertyValue("NgayHetNhiemKy", ref _NgayHetNhiemKy, value);
            }
        }

        [Aggregated]
        [ModelDefault("Caption", "Bộ phận quản lý")]
        [Association("QuyetDinhBoNhiemKiemNhiem-ListChiTietBoNhiemKiemNhiem")]
        public XPCollection<ChiTietBoNhiemKiemNhiem> ListChiTietBoNhiemKiemNhiem
        {
            get
            {
                return GetCollection<ChiTietBoNhiemKiemNhiem>("ListChiTietBoNhiemKiemNhiem");
            }
        }

        public QuyetDinhBoNhiemKiemNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhBoNhiemKiemNhiem;
            //
            QuyetDinhMoi = true;
        }
        
        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {

                if (QuyetDinhMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Tạo chức vụ kiêm nhiệm mới
                    ChucVuKiemNhiem chucVuKiemNhiem = new ChucVuKiemNhiem(Session);
                    chucVuKiemNhiem.QuyetDinh = this;
                    chucVuKiemNhiem.BoPhan = BoPhan;
                    chucVuKiemNhiem.NhanVien = ThongTinNhanVien;
                    chucVuKiemNhiem.ChucVu = ChucVuMoi;
                    chucVuKiemNhiem.PhuCapKiemNhiem = HSPCChucVuMoi;
                    chucVuKiemNhiem.NgayBoNhiem = NgayHuongHeSoMoi;
                    chucVuKiemNhiem.NgayHetNhiemKy = NgayHetNhiemKy;

                    //Làm quyết định tất cả quyết định bổ nhiệm trước đó hết hiệu lực
                    CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=? And Oid!=? and QuyetDinhMoi", ThongTinNhanVien, this.Oid);
                    XPCollection<QuyetDinhBoNhiemKiemNhiem> quyetDinhCuList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session, filter);
                    foreach(var item  in quyetDinhCuList)
                    {
                        item.QuyetDinhMoi = false;
                    }

                    JobUpdated = true;
                }

                //Quá trình bổ nhiệm chức vụ
                ProcessesHelper.CreateQuaTrinhBoNhiemKiemNhiem(Session, this, ChucVuMoi, HSPCChucVuMoi, NgayHuongHeSoMoi);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving && QuyetDinhMoi)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=?", this.Oid);
                ChucVuKiemNhiem chucVuKiemNhiem = Session.FindObject<ChucVuKiemNhiem>(filter);
                if (chucVuKiemNhiem != null)
                    Session.Delete(chucVuKiemNhiem);

                //Làm quyết định bổ nhiệm trước đó có hiệu lực lại
                filter = CriteriaOperator.Parse("ThongTinNhanVien=? And Oid !=? and !QuyetDinhMoi", ThongTinNhanVien, this.Oid);
                SortProperty sort = new SortProperty("NgayHieuLuc", SortingDirection.Descending);
                XPCollection<QuyetDinhBoNhiem> quyetDinhCuList = new XPCollection<QuyetDinhBoNhiem>(Session, filter, sort);
                quyetDinhCuList.TopReturnedObjects = 1;
                if (quyetDinhCuList.Count == 1)
                    quyetDinhCuList[0].QuyetDinhMoi = true;
            }

            //Xóa quá trình bổ nhiệm
            ProcessesHelper.DeleteQuaTrinhNhanVien<QuaTrinhBoNhiemKiemNhiem>(Session, this.Oid,this.ThongTinNhanVien.Oid);

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@QuyetDinh", Oid);
            DataProvider.ExecuteNonQuery("dbo.spd_WebChamCong_DeleteWebUser_BoPhan", CommandType.StoredProcedure, param);

            base.OnDeleting();
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@QuyetDinh", Oid);
            DataProvider.ExecuteNonQuery("dbo.spd_WebChamCong_CreateWebUser_BoPhan", CommandType.StoredProcedure, param);
        }
    }
}
