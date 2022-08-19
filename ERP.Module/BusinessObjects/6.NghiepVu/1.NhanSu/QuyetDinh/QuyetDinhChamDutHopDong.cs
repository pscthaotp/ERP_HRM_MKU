using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.HopDongs;
using ERP.Module.Enum.NhanSu;
using DevExpress.ExpressApp.Editors;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Utils;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định chấm dứt hợp đồng")]
    //[Appearance("QuyetDinhChamDutHopDong.AnNoiDungLyDo", TargetItems = "NoiDungLyDo", Visibility = ViewItemVisibility.Hide, Criteria = "LyDo != 2")]
    [Appearance("QuyetDinhChamDutHopDong.AnNoiDungLyDo", TargetItems = "NoiDungLyDo", Enabled = false, Criteria = "LyDo != 2")]
    public class QuyetDinhChamDutHopDong : QuyetDinhCaNhan
    {
        private HopDong _HopDong;
        private LyDoChamDutHopDongEnum _LyDo;
        private string _NoiDungLyDo;
        private DateTime _NgayKyThoaThuan;
        
        [ModelDefault("Caption", "Hợp đồng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("HDList", DataSourcePropertyIsNullMode.SelectAll)]
        public HopDong HopDong
        {
            get
            {
                return _HopDong;
            }
            set
            {
                SetPropertyValue("HopDong", ref _HopDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Lý do")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LyDoChamDutHopDongEnum LyDo
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

        [Size(200)]
        [ModelDefault("Caption", "Nội dung lý do")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "LyDo = 2")]
        public string NoiDungLyDo
        {
            get
            {
                return _NoiDungLyDo;
            }
            set
            {
                SetPropertyValue("NoiDungLyDo", ref _NoiDungLyDo, value);
            }
        }

        [ModelDefault("Caption", "Ngày ký thỏa thuận")]
        public DateTime NgayKyThoaThuan
        {
            get
            {
                return _NgayKyThoaThuan;
            }
            set
            {
                SetPropertyValue("NgayKyThoaThuan", ref _NgayKyThoaThuan, value);
            }
        }

        public QuyetDinhChamDutHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            QuyetDinhMoi = true;
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhChamDutHopDong;
            //            
            LyDo = LyDoChamDutHopDongEnum.TheoNguyenVong;
            UpdateHDList();
        }

        protected override void AfterNhanVienChanged()
        {
            BoPhan = ThongTinNhanVien.BoPhan;
            NgayKyThoaThuan = ThongTinNhanVien.NgayVaoCongTy;
            //
            UpdateHDList();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted)
            {           
                //Update tình trạng
                if (LyDo != LyDoChamDutHopDongEnum.LuanChuyen)
                {
                    if (NgayHieuLuc <= Common.GetServerCurrentTime())
                    {
                        TinhTrang tinhTrang = Common.GetTinhTrang_ByTenTinhTrang(Session, "%nghỉ việc%");
                        if (tinhTrang == null)
                        {
                            tinhTrang = new TinhTrang(Session);
                            tinhTrang.TenTinhTrang = "Nghỉ việc";
                            tinhTrang.MaQuanLy = "NV";
                        }
                        ThongTinNhanVien.TinhTrang = tinhTrang;
                        ThongTinNhanVien.NgayNghiViec = NgayHieuLuc;

                        JobUpdated = true;
                    }

                    //Lịch sử bản thân
                    ProcessesHelper.CreateLichSuBanThan(Session, this.ThongTinNhanVien, this, "Nghỉ việc");
                }
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                if (LyDo != LyDoChamDutHopDongEnum.LuanChuyen)
                {
                    TinhTrang tinhTrang = Common.GetTinhTrang_ByTenTinhTrang(Session, "%đang làm việc%");
                    if (tinhTrang == null)
                    {
                        tinhTrang = new TinhTrang(Session);
                        tinhTrang.TenTinhTrang = "Đang làm việc";
                        tinhTrang.MaQuanLy = "DLV";
                    }
                    ThongTinNhanVien.TinhTrang = tinhTrang;
                    ThongTinNhanVien.NgayNghiViec = DateTime.MinValue;

                    //Xóa lịch sử bản thân
                    ProcessesHelper.DeleteQuaTrinhNhanVien<LichSuBanThan>(Session, this.Oid, this.ThongTinNhanVien.Oid);
                }
            }

            base.OnDeleting();
        }


        [Browsable(false)]
        public XPCollection<HopDong> HDList { get; set; }

        private void UpdateHDList()
        {
            //
            if (HDList == null)
                HDList = new XPCollection<HopDong>(Session);
            //
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien.Oid=?",ThongTinNhanVien != null ? ThongTinNhanVien.Oid : Guid.Empty);
            HDList.Criteria = filter;
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (!IsDeleted)
            {
                if (QuyetDinhMoi)
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
                if (QuyetDinhMoi)
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
