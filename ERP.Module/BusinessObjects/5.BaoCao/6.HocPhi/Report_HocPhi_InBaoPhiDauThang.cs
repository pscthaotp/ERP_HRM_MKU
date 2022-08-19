using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Data.SqlClient;
using DevExpress.ExpressApp.Model;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocPhi.BienLai;
using ERP.Module.NghiepVu.HocPhi.BangCongNos;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using ERP.Module.NonPersistentObjects.HocPhis;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.HocPhi;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.HocPhi.DinhPhi;
using System.Data;
using ERP.Module.NghiepVu.HocSinh.Lops;
using ERP.Module.Enum.HocSinh;
using ERP.Module.Extends;

namespace ERP.Module.Report.HocPhi
{
    [NonPersistent()]
    [ImageName("BO_Report")]
    [ModelDefault("Caption", "Báo cáo - In phiếu báo - Học phí")]
    public class Report_HocPhi_InBaoPhiDauThang : StoreProcedureReport, ICongTy
    {
        #region KhaiBao
        private CongTy _CongTy;
        private Khoi _Khoi;
        private Lop _Lop;
        private HeDaoTao _HeDaoTao;
        //private NamHoc _NamHoc;
        private BangCongNo _BangCongNo;
        private KyTinhHocPhi _KyTinhHocPhi;
        private DateTime _KyTinh;
        private DateTime _NgayTao;
        private DateTime _HanApDung;
        private bool _KhongHoanPhi; 
        private bool _ApDungThangHienTai;   
        #endregion

        private string _GhiChu;

        #region CongTy
        [ModelDefault("Caption", "Công ty")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        [DataSourceCriteria("LoaiTruong = 1")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading )
                {
                    if (CongTy != null)
                    {
                        HeDaoTao = CongTy.HeDaoTao;
                        //UpdateKyTinhHocPhiList();
                        if (KyTinh != DateTime.MinValue)
                            KyTinhHocPhi = Session.FindObject<KyTinhHocPhi>(CriteriaOperator.Parse("Thang =? and Nam =? and CongTy =?", KyTinh.Month, KyTinh.Year, CongTy.Oid));
                         LoadPhi();
                    }
                    else
                        Khoi = null;
                }
            }
        }
        #endregion

        #region Khoi
        [ModelDefault("Caption", "Khối")]
        [DataSourceProperty("KhoiList")]
        [ImmediatePostData]
        public Khoi Khoi
        {
            get
            {
                return _Khoi;
            }
            set
            {
                SetPropertyValue("Khoi", ref _Khoi, value);
                if (!IsLoading)
                {
                    if (Khoi != null)
                    {
                        UpdateLopList();
                    }
                    else
                        Lop = null;

                }
            }
        }

        [Browsable(false)]
        public XPCollection<Khoi> KhoiList { get; set; }
        protected void UpdateKhoiList()
        {

            KhoiList = new XPCollection<Khoi>(Session);
            if (HeDaoTao != null)
                KhoiList.Criteria = CriteriaOperator.Parse("CongTy =? and LoaiLop =? and HeDaoTao =?", CongTy.Oid, LoaiLopEnum.Khoi, HeDaoTao.Oid);
            OnChanged("Khoi");
        }
        #endregion

        #region Lop
        [ModelDefault("Caption", "Lớp")]
        [DataSourceProperty("LopList")]
        [ImmediatePostData]
        public Lop Lop
        {
            get
            {
                return _Lop;
            }
            set
            {
                SetPropertyValue("Lop", ref _Lop, value);
            }
        }
        [Browsable(false)]
        public XPCollection<Lop> LopList { get; set; }
        protected void UpdateLopList()
        {
            if (LopList == null)
                LopList = new XPCollection<Lop>(Session);
            using (DialogUtil.Wait("Đang lấy danh sách lớp!", "Thông báo!"))
            {
                LopList.Criteria = CriteriaOperator.Parse("CongTy =? and LopCha =?", CongTy.Oid, Khoi.Oid);
            }
        }
        #endregion

        #region HeDaoTao
        [ModelDefault("Caption", "Hệ đào tạo")]
        [ImmediatePostData] 
        public HeDaoTao HeDaoTao
        {
            get
            {
                return _HeDaoTao;
            }
            set
            {
                SetPropertyValue("HeDaoTao", ref _HeDaoTao, value);
                if (!IsLoading)
                {
                    if (value != null)
                        UpdateKhoiList();
                    else
                    {
                        Khoi = null;
                        KhoiList = null;
                    }
                }
            }
        }
        #endregion

        #region BangCongNo
        [ModelDefault("Caption", "Bảng công nợ")]
        [RuleRequiredField(DefaultContexts.Save)]
        [Browsable(false)]
        public BangCongNo BangCongNo
        {
            get
            {
                return _BangCongNo;
            }
            set
            {
                SetPropertyValue("BangCongNo", ref _BangCongNo, value);
                if (!IsLoading)
                    if (BangCongNo != null)
                        LoadPhi();
                    else
                        ListInCongNoChiTiet.Reload();
            }
        }
        #endregion

        #region NamHoc - KyTinh       

        [ModelDefault("Caption", "Kỳ tính học phí")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public KyTinhHocPhi KyTinhHocPhi
        {
            get
            {
                return _KyTinhHocPhi;
            }
            set
            {
                SetPropertyValue("KyTinhHocPhi", ref _KyTinhHocPhi, value);
                if (!IsLoading)
                {
                    if (KyTinhHocPhi != null)
                    {
                        BangCongNo = Session.FindObject<BangCongNo>(CriteriaOperator.Parse("CongTy =? and NamHoc =?", CongTy.Oid, KyTinhHocPhi.NamHoc.Oid));
                        HanApDung = KyTinhHocPhi.TuNgay;
                        //GhiChu = "Đóng HP từ ngày " + KyTinhHocPhi.ThuTuNgay.ToShortDateString() + " đến ngày " + KyTinhHocPhi.ThuDenNgay.ToShortDateString();
                    }
                }
            }
        }
      
        [ModelDefault("Caption", "Kỳ tính")]
        [ModelDefault("Editmask", "MM/yyyy")]
        [ModelDefault("DisplayFormat", "MM/yyyy")]
        [ImmediatePostData]
        public DateTime KyTinh
        {
            get { return _KyTinh; }
            set
            {
                SetPropertyValue("KyTinh", ref _KyTinh, value);
                if (!IsLoading)
                {
                    if (KyTinh != null && KyTinh != DateTime.MinValue && CongTy != null)
                        KyTinhHocPhi = Session.FindObject<KyTinhHocPhi>(CriteriaOperator.Parse("CongTy=? and Thang =? and Nam =?", CongTy.Oid, KyTinh.Month, KyTinh.Year));
                }
            }
        }
        [ModelDefault("Caption", "Ngày tạo")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "False")]
        public DateTime NgayTao
        {
            get
            {
                return _NgayTao;
            }
            set
            {
                SetPropertyValue("NgayTao", ref _NgayTao, value);
            }
        }

        [ModelDefault("Caption", "Hạn áp dụng")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [RuleRequiredField(DefaultContexts.Save)]
        public DateTime HanApDung
        {
            get
            {
                return _HanApDung;
            }
            set
            {
                SetPropertyValue("HanApDung", ref _HanApDung, value);
            }
        }
        #endregion

         [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        [ModelDefault("Caption", "Không hoàn phí")]
        public bool KhongHoanPhi
        {
            get
            {
                return _KhongHoanPhi;
            }
            set
            {
                SetPropertyValue("KhongHoanPhi", ref _KhongHoanPhi, value);
            }
        }
        [ModelDefault("Caption", "Áp dụng tháng hiện tại")]
        public bool ApDungThangHienTai
        {
            get
            {
                return _ApDungThangHienTai;
            }
            set
            {
                SetPropertyValue("ApDungThangHienTai", ref _ApDungThangHienTai, value);
            }
        }
        private bool _HocSinhMoi;
        [ModelDefault("Caption", "Học sinh mới")]
        public bool HocSinhMoi
        {
            get { return _HocSinhMoi; }
            set { SetPropertyValue("HocSinhMoi", ref _HocSinhMoi, value); }
        }

        [ModelDefault("Caption", "Danh sách phí")]
        public XPCollection<BangCongNo_InCongNoChiTiet> ListInCongNoChiTiet { get; set; }

        public Report_HocPhi_InBaoPhiDauThang(Session session) : base(session) { }
        public void LoadPhi()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách thu phí - "+CongTy.TenBoPhan))
            {
                #region
                ListInCongNoChiTiet.Reload();
                if (CongTy != null && KyTinhHocPhi != null)
                {
                    HinhThucDong HinhThucDong = Session.FindObject<HinhThucDong>(CriteriaOperator.Parse("MacDinh = ?", true));
                    DinhPhi dinhPhi = Session.FindObject<DinhPhi>(CriteriaOperator.Parse("NamHoc = ?  and CongTy = ? and NguoiDungTao =?", KyTinhHocPhi.NamHoc.Oid, CongTy.Oid, true));
                    if (dinhPhi != null)
                    {
                        DataTable dt = new DataTable();

                        XPCollection<LoaiPhi> listLoaiPhi = new XPCollection<LoaiPhi>(Session, false);
                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@DinhPhi", dinhPhi.Oid);

                        SqlCommand cmd = DataProvider.GetCommand("spd_HocPhi_ListDanhSachPhi", System.Data.CommandType.StoredProcedure, param);
                        cmd.Connection = DataProvider.GetConnection();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                            LoaiPhi lp;
                            foreach (DataRow item in dt.Rows)
                            {
                                lp = Session.GetObjectByKey<LoaiPhi>(item["LoaiPhi"]);
                                listLoaiPhi.Add(lp);
                            }
                        }
                        foreach (LoaiPhi loaiPhi in listLoaiPhi)
                        {
                            if (loaiPhi.NhomDinhPhi == Enum.HocPhi.NhomDinhPhiEnum.HocPhi)
                            {
                                BangCongNo_InCongNoChiTiet chiTiet = new BangCongNo_InCongNoChiTiet(Session);
                                chiTiet.LoaiPhi = loaiPhi;
                                if (loaiPhi.TuyChinhHinhThucDong == true)
                                    chiTiet.HinhThucDong = HinhThucDong;
                                else
                                    chiTiet.HinhThucDong = Session.FindObject<DinhPhiHocPhi>(CriteriaOperator.Parse("DinhPhi = ? and LoaiPhi = ?", dinhPhi.Oid, loaiPhi.Oid)).HinhThucDong;
                                if (chiTiet.HinhThucDong.MaQuanLy.Contains("Nam") || chiTiet.HinhThucDong.MaQuanLy.Contains("1"))
                                    chiTiet.Chon = false;
                                ListInCongNoChiTiet.Add(chiTiet);
                            }
                            else if (loaiPhi.NhomDinhPhi == Enum.HocPhi.NhomDinhPhiEnum.NgoaiGio)
                            {
                                DinhPhiNgoaiGio dinhPhiNgoaiGio = Session.FindObject<DinhPhiNgoaiGio>(CriteriaOperator.Parse("DinhPhi = ? and LoaiPhi = ?", dinhPhi.Oid, loaiPhi.Oid));
                                if (dinhPhiNgoaiGio != null)
                                {
                                    BangCongNo_InCongNoChiTiet chiTiet = new BangCongNo_InCongNoChiTiet(Session);
                                    chiTiet.LoaiPhi = loaiPhi;
                                    chiTiet.HinhThucDong = dinhPhiNgoaiGio.HinhThucDong;
                                    ListInCongNoChiTiet.Add(chiTiet);
                                }
                            }
                            else if (loaiPhi.NhomDinhPhi == Enum.HocPhi.NhomDinhPhiEnum.NgoaiKhoa)
                            {
                                DinhPhiNgoaiKhoa dinhPhiNgoaiKhoa = Session.FindObject<DinhPhiNgoaiKhoa>(CriteriaOperator.Parse("DinhPhi = ? and LoaiPhi = ?", dinhPhi.Oid, loaiPhi.Oid));
                                if (dinhPhiNgoaiKhoa != null)
                                {
                                    BangCongNo_InCongNoChiTiet chiTiet = new BangCongNo_InCongNoChiTiet(Session);
                                    chiTiet.LoaiPhi = loaiPhi;
                                    chiTiet.HinhThucDong = dinhPhiNgoaiKhoa.HinhThucDong;
                                    ListInCongNoChiTiet.Add(chiTiet);
                                }
                            }
                            else if (loaiPhi.NhomDinhPhi == Enum.HocPhi.NhomDinhPhiEnum.DichVu)
                            {
                                DinhPhiBUS dinhPhiBus = Session.FindObject<DinhPhiBUS>(CriteriaOperator.Parse("DinhPhi = ? and LoaiPhi = ?", dinhPhi.Oid, loaiPhi.Oid));
                                if (dinhPhiBus != null)
                                {
                                    BangCongNo_InCongNoChiTiet chiTiet = new BangCongNo_InCongNoChiTiet(Session);
                                    chiTiet.LoaiPhi = loaiPhi;
                                    chiTiet.HinhThucDong = dinhPhiBus.HinhThucDong;
                                    ListInCongNoChiTiet.Add(chiTiet);
                                }
                            }
                        }
                    }
                }
                #endregion
            }
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ListInCongNoChiTiet = new XPCollection<BangCongNo_InCongNoChiTiet>(Session, false);
            CongTy = Common.CongTy(Session);
            NgayTao = DateTime.Now;
            LoadPhi();
        }
        public override SqlCommand CreateCommand()
        {
            string query = "";
            foreach (BangCongNo_InCongNoChiTiet chiTiet in ListInCongNoChiTiet)
            {
                if (chiTiet.Chon == true)
                {

                    string bCongNo = BangCongNo != null ? "'" + BangCongNo.Oid.ToString() + "'" : "null";
                    query += " INSERT INTO dbo.CongNoLoaiPhi (Oid, LoaiPhi, HinhThucDong, SoLuong, NgayTao, BangCongNo) VALUES";
                    query += " (NEWID(), '" + chiTiet.LoaiPhi.Oid.ToString()
                            + "', '" + chiTiet.HinhThucDong.Oid.ToString() + "', '" + chiTiet.SoLuong + "', GETDATE(), " + bCongNo + ")";
                }
            }

            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@BangCongNo", BangCongNo.Oid);
            param[1] = new SqlParameter("@KyTinhHocPhi", KyTinhHocPhi.Oid);
            param[2] = new SqlParameter("@NgayTao", NgayTao.Date);
            param[3] = new SqlParameter("@HanApDung", HanApDung.Date);
            param[4] = new SqlParameter("@query", query);
            param[5] = new SqlParameter("@Khoi", Khoi != null ? Khoi.Oid : Guid.Empty);
            param[6] = new SqlParameter("@Lop", Lop != null ? Lop.Oid : Guid.Empty);
            param[7] = new SqlParameter("@KhongHoanPhi", KhongHoanPhi);
            param[8] = new SqlParameter("@HeDaoTao", HeDaoTao != null ? HeDaoTao.Oid : Guid.Empty);
            param[9] = new SqlParameter("@HocSinhMoi", HocSinhMoi == true ? 1 : 0);
            param[10] = new SqlParameter("@ApDungThangHienTai", ApDungThangHienTai == true ? 1 : 0);
            SqlCommand cmd = DataProvider.GetCommand("spd_Report_HocPhi_InCongNoHocSinh_DuThu", System.Data.CommandType.StoredProcedure, param);
            cmd.CommandTimeout = 180;
            return cmd;
        }
    }
}
