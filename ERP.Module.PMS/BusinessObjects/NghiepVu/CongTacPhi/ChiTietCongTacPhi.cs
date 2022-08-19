using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.DanhMuc.PMS;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.DanhMuc;
using ERP.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết công tác phí")]
    [Appearance("EnableTinhThuLao", TargetItems = "DonGiaDiLai;SoLuotDiLai", Enabled = false, Criteria = "TinhThuLao")]
    public class ChiTietCongTacPhi : BaseObject
    {
        private QuanLyCongTacPhi _QuanLyCongTacPhi;
        private HocKy _HocKy;
        private NhanVien _NhanVien;
        //private LopHocPhan _LopHocPhan; Hiện tại không sử dụng _ ThuHuong tắt 
        private string _MaHocPhan; 
        private string _LopHocPhan;
        private string _MaKhoaQuanLyMonHoc;
        private string _TenKhoaQuanLyMonHoc; 
        private string _MaLopSV;
        private string _TenLopSV;
        private NhomMon _NhomMon;
        private NganhHoc _NganhHoc;
        private KhoiNganh _KhoiNganh; 
        private decimal _SoLuotDiLai;
        private DonGiaDiLai _DonGiaDiLai;
        private bool _TinhThuLao;
        private bool _Sua;
        private string _GhiChu;
        private BangChotThuLao_ThinhGiang _BangChotThuLao_ThinhGiang;
        //private decimal _ChiPhiTienXe; Hiện tại không sử dụng _ ThuHuong tắt  
        //private decimal _ChiPhiTienAn; Hiện tại không sử dụng _ ThuHuong tắt 

        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý công tác phí")]
        [Association("QuanLyCongTacPhi-ListChiTietCongTacPhi")]
        public QuanLyCongTacPhi QuanLyCongTacPhi
        {
            get
            {
                return _QuanLyCongTacPhi;
            }
            set
            {
                SetPropertyValue("QuanLyCongTacPhi", ref _QuanLyCongTacPhi, value);
            }
        }


        [ModelDefault("Caption", "Học kỳ")]
        [ModelDefault("AllowEdit", "false")]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        [ModelDefault("AllowEdit", "false")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        //[ModelDefault("Caption", "Lớp học phần")]
        //public LopHocPhan LopHocPhan
        //{
        //    get { return _LopHocPhan; }
        //    set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        //}

        [ModelDefault("Caption", "Mã học phần")]
        [ModelDefault("AllowEdit", "false")]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }

        [ModelDefault("Caption", "Lớp học phần")]
        [ModelDefault("AllowEdit", "false")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }

        [ModelDefault("Caption","Mã khoa QL môn học")]
        [ModelDefault("AllowEdit", "false")]
        public string MaKhoaQuanLyMonHoc
        {
            get { return _MaKhoaQuanLyMonHoc; }
            set { SetPropertyValue("MaKhoaQuanLyMonHoc", ref _MaKhoaQuanLyMonHoc, value); }
        }

        [ModelDefault("Caption", "Tên khoa QL môn học")]
        [ModelDefault("AllowEdit", "false")]
        public string TenKhoaQuanLyMonHoc
        {
            get { return _TenKhoaQuanLyMonHoc; }
            set { SetPropertyValue("TenKhoaQuanLyMonHoc", ref _TenKhoaQuanLyMonHoc, value); }
        }

        [ModelDefault("Caption", "Mã lớp SV")]
        [ModelDefault("AllowEdit", "false")]
        public string MaLopSV
        {
            get { return _MaLopSV; }
            set { SetPropertyValue("MaLopSV", ref _MaLopSV, value); }
        }

        [ModelDefault("Caption", "Tên lớp SV")]
        [ModelDefault("AllowEdit", "false")]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }

        [ModelDefault("Caption", "Nhóm môn")]
        [ModelDefault("AllowEdit", "false")]
        public NhomMon NhomMon
        {
            get { return _NhomMon; }
            set { SetPropertyValue("NhomMon", ref _NhomMon, value); }
        }

        [ModelDefault("Caption", "Ngành học")]
        [ModelDefault("AllowEdit", "false")]
        public NganhHoc NganhHoc
        {
            get { return _NganhHoc; }
            set { SetPropertyValue("NganhHoc", ref _NganhHoc, value); }
        }

        [ModelDefault("Caption", "Khối ngành")]
        [ModelDefault("AllowEdit", "false")]
        public KhoiNganh KhoiNganh
        {
            get { return _KhoiNganh; }
            set { SetPropertyValue("KhoiNganh", ref _KhoiNganh, value); }
        }

        [ModelDefault("Caption", "Số lượt đi lại")]
        [ModelDefault("EditMask", "N0")]
        [ModelDefault("DisplayFormat", "N0")]
        public decimal SoLuotDiLai
        {
            get
            {
                return _SoLuotDiLai;
            }
            set
            {
                SetPropertyValue("SoLuotDiLai", ref _SoLuotDiLai, value);
            }
        }

        [ModelDefault("Caption", "Đơn giá đi lại")]
        [DataSourceProperty("DonGiaList", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        public DonGiaDiLai DonGiaDiLai
        {
            get
            {
                return _DonGiaDiLai;
            }
            set
            {
                SetPropertyValue("DonGiaDiLai", ref _DonGiaDiLai, value);
                if (!IsLoading)
                {
                    UpdateDonGiaList();
                }
            }
        }
        [ModelDefault("Caption", "Tính thù lao")]
        [ModelDefault("AllowEdit", "false")]
        public bool TinhThuLao
        {
            get { return _TinhThuLao; }
            set { SetPropertyValue("TinhThuLao", ref _TinhThuLao, value); }
        }


        [ModelDefault("Caption", "Sửa")]
        [ModelDefault("AllowEdit", "false")]
        public bool Sua
        {
            get { return _Sua; }
            set { SetPropertyValue("Sua", ref _Sua, value); }
        }

        [ModelDefault("Caption", "Lý do điều chỉnh/Ghi chú")]     
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "false")]
        public BangChotThuLao_ThinhGiang BangChotThuLao_ThinhGiang
        {
            get
            {
                return _BangChotThuLao_ThinhGiang;
            }
            set
            {
                SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value);
            }
        }

        //[ModelDefault("Caption", "Chi phí tiền xe")]
        //[ModelDefault("EditMask", "N2")]
        //[ModelDefault("DisplayFormat", "N2")]
        //public decimal ChiPhiTienXe
        //{
        //    get
        //    {
        //        return _ChiPhiTienXe;
        //    }
        //    set
        //    {
        //        SetPropertyValue("ChiPhiTienXe", ref _ChiPhiTienXe, value);
        //    }
        //}

        //[ModelDefault("Caption", "Chi phí tiền ăn")]
        //[ModelDefault("EditMask", "N2")]
        //[ModelDefault("DisplayFormat", "N2")]
        //public decimal ChiPhiTienAn
        //{
        //    get
        //    {
        //        return _ChiPhiTienAn;
        //    }
        //    set
        //    {
        //        SetPropertyValue("ChiPhiTienAn", ref _ChiPhiTienAn, value);
        //    }
        //}

        [Browsable(false)]
        public XPCollection<DonGiaDiLai>DonGiaList { get; set; }


        protected override void OnSaving()
        {
            base.OnSaving();
            Sua = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            UpdateDonGiaList();
        }

        public ChiTietCongTacPhi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            UpdateDonGiaList();
        }

        private void UpdateDonGiaList()
        {
            //
            if (DonGiaList == null)
                DonGiaList = new XPCollection<DonGiaDiLai>(Session);
            if (QuanLyCongTacPhi != null)
            {
                DonGiaList.Criteria = CriteriaOperator.Parse("NamHoc=?", QuanLyCongTacPhi.NamHoc.Oid);
            }
          
        }
    }
}
