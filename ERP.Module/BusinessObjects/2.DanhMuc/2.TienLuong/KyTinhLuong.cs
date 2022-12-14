using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.TienLuong.ChamCong;
using ERP.Module.Commons;
using System.Data.SqlClient;
using System.Data;
//
namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [DefaultProperty("TenKy")]
    [ImageName("BO_KyTinhLuong")]
    [ModelDefault("Caption", "Kỳ tính lương")]
    //[Appearance("KyTinhLuong.KhoaTinhLuong", TargetItems = "Thang;Nam;TuNgay;DenNgay;SoNgay;ThongTinChung;MocTinhThueTNCN;QuanLyChamCong;QuanLyCongNgoaiGio;QuanLyCongKhac", Enabled = false,Criteria = "KhoaKyTinhLuong")]
    [Appearance("KyTinhLuong.KhoaSo", TargetItems = "*", Enabled = false, Criteria = "KhoaSo")]
    [RuleCombinationOfPropertiesIsUnique("Kỳ tính lương đã tồn tại.", DefaultContexts.Save, "Thang;Nam;CongTy")]
    public class KyTinhLuong : BaseObject,ICongTy
    {
        private int _Thang;
        private int _Nam;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private decimal _SoNgay;
        private bool _KhoaSo;
        private ThongTinChung _ThongTinChung;
        private MocTinhThueTNCN _MocTinhThueTNCN;
        private MocQuyDoiThuNhapKhongThue _MocQuyDoiThuNhapKhongThue;
        private CC_QuanLyChamCong _QuanLyChamCong;
        private CC_QuanLyCongNgoaiGio _QuanLyCongNgoaiGio;
        private CC_KyChamCong _QuanLyCongKhac;
        private CongTy _CongTy;
        private bool _KhoaKyTinhLuong;
        //
        private DateTime _MocDongBaoHiem;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tháng")]
        [RuleRange("", DefaultContexts.Save, 1, 12)]
        [RuleRequiredField(DefaultContexts.Save)]
        public int Thang
        {
            get
            {
                return _Thang;
            }
            set
            {
                SetPropertyValue("Thang", ref _Thang, value);
                if (!IsLoading && Nam > 0 && Thang > 0)
                {
                    if (Thang > 1)
                        TuNgay = new DateTime(Nam, Thang - 1, 26);
                    else
                    {
                        TuNgay = new DateTime(Nam - 1, 12, 26);
                    }
                    DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                    MocDongBaoHiem = new DateTime(Nam, Thang, 15);
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public int Nam
        {
            get
            {
                return _Nam;
            }
            set
            {
                SetPropertyValue("Nam", ref _Nam, value);
                if (!IsLoading && Nam > 0 && Thang > 0)
                {
                    if(Thang > 1)
                        TuNgay = new DateTime(Nam, Thang - 1, 26);
                    else
                    {
                        TuNgay = new DateTime(Nam - 1, 12, 26);
                    }
                    //
                    DenNgay = TuNgay.AddMonths(1).AddDays(-1);
                    MocDongBaoHiem = new DateTime(Nam, Thang, 15);
                }
            }
        }

        [ModelDefault("Caption", "Tên kỳ lương")]
        public string TenKy
        {
            get
            {
                return String.Format("Tháng {0:0#} năm {1:####}", Thang, Nam);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
                if (!IsLoading)
                {
                    TinhSoNgayCongChuan();
                }
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
                if (!IsLoading)
                {
                    TinhSoNgayCongChuan();
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Số ngày")]
        [ModelDefault("EditMask", "n1")]
        [ModelDefault("DisplayFormat", "n1")]
        public decimal SoNgay
        {
            get
            {
                return _SoNgay;
            }
            set
            {
                SetPropertyValue("SoNgay", ref _SoNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Khóa sổ")]
        public bool KhoaSo
        {
            get
            {
                return _KhoaSo;
            }
            set
            {
                SetPropertyValue("KhoaSo", ref _KhoaSo, value);
            }
        }

        [Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("Caption", "Thông tin chung")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public ThongTinChung ThongTinChung
        {
            get
            {
                return _ThongTinChung;
            }
            set
            {
                SetPropertyValue("ThongTinChung", ref _ThongTinChung, value);
            }
        }

        [Aggregated]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("Caption", "Mốc tính thuế TNCN")]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public MocTinhThueTNCN MocTinhThueTNCN
        {
            get
            {
                return _MocTinhThueTNCN;
            }
            set
            {
                SetPropertyValue("MocTinhThueTNCN", ref _MocTinhThueTNCN, value);
            }
        }

        [Aggregated]
        [VisibleInListView(false)]
        [ModelDefault("Caption", "Mốc quy đổi TNKT")]
        [ExpandObjectMembers(ExpandObjectMembers.Never)]
        [ModelDefault("PropertyEditorType", "DevExpress.ExpressApp.Win.Editors.ObjectPropertyEditor")]
        public MocQuyDoiThuNhapKhongThue MocQuyDoiThuNhapKhongThue
        {
            get
            {
                return _MocQuyDoiThuNhapKhongThue;
            }
            set
            {
                SetPropertyValue("MocQuyDoiThuNhapKhongThue", ref _MocQuyDoiThuNhapKhongThue, value);
            }
        }

        [ModelDefault("Caption", "Bảng công nhân viên")]
        [DataSourceProperty("QLChamCongList")]
        public CC_QuanLyChamCong QuanLyChamCong
        {
            get
            {
                return _QuanLyChamCong;
            }
            set
            {
                if (IsLoading)
                    SetPropertyValue("QuanLyChamCong", ref _QuanLyChamCong, value);
                else
                {
                    if (value == null)
                        QuanLyChamCong.KhoaChamCong = false;
                    SetPropertyValue("QuanLyChamCong", ref _QuanLyChamCong, value);
                }
            }
        }

        [ModelDefault("Caption", "Bảng công ngoài giờ")]
        [DataSourceProperty("QLChamCongNgoaiGioList")]
        public CC_QuanLyCongNgoaiGio QuanLyCongNgoaiGio
        {
            get
            {
                return _QuanLyCongNgoaiGio;
            }
            set
            {
                if (IsLoading)
                    SetPropertyValue("QuanLyCongNgoaiGio", ref _QuanLyCongNgoaiGio, value);
                else
                {
                    if (value == null)
                        QuanLyCongNgoaiGio.KhoaChamCong = false;
                    SetPropertyValue("QuanLyCongNgoaiGio", ref _QuanLyCongNgoaiGio, value);
                }
            }
        }

        [ModelDefault("Caption", "Bảng công khác")]
        [DataSourceProperty("QLCongKhacList")]
        public CC_KyChamCong QuanLyCongKhac
        {
            get
            {
                return _QuanLyCongKhac;
            }
            set
            {
                if (IsLoading)
                    SetPropertyValue("QuanLyCongKhac", ref _QuanLyCongKhac, value);
                else
                {
                    if (value == null)
                    {
                        XPCollection<CC_QuanLyCongKhac> listQuanLyCongKhac = new XPCollection<CC_QuanLyCongKhac>(Session);
                        listQuanLyCongKhac.Criteria = CriteriaOperator.Parse("KyChamCong.Oid = ?", QuanLyCongKhac.Oid);
                        foreach (var item in listQuanLyCongKhac)
                            item.KhoaChamCong = false;
                    }
                    SetPropertyValue("QuanLyCongKhac", ref _QuanLyCongKhac, value);
                }
            }
        }

        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [ImmediatePostData]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
                if (!IsLoading)
                {
                    TinhSoNgayCongChuan();
                    UpdateQLChamCongList();
                    UpdateQLChamCongNgoaiGioList();
                    UpdateQLCongKhacList();
                    //
                   // ThongTinChung = value.CongTy.ThongTinChung;//7-15-2021
                }
            }
        }

        [ModelDefault("Caption", "Mốc đóng bảo hiểm")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime MocDongBaoHiem
        {
            get
            {
                return _MocDongBaoHiem;
            }
            set
            {
                SetPropertyValue("MocDongBaoHiem", ref _MocDongBaoHiem, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public bool KhoaKyTinhLuong
        {
            get
            {
                return _KhoaKyTinhLuong;
            }
            set
            {
                SetPropertyValue("KhoaKyTinhLuong", ref _KhoaKyTinhLuong, value);
            }
        }

        public KyTinhLuong(Session session) :base(session) { }

        [Browsable(false)]
        public XPCollection<CC_QuanLyChamCong> QLChamCongList { get; set; }

        [Browsable(false)]
        public XPCollection<CC_QuanLyCongNgoaiGio> QLChamCongNgoaiGioList { get; set; }

        [Browsable(false)]
        public XPCollection<CC_KyChamCong> QLCongKhacList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            if (CongTy != null)
            {
                if (CongTy.ThongTinChung != null)
                    ThongTinChung = Common.Copy<ThongTinChung>(Session, CongTy.ThongTinChung);
                else
                    ThongTinChung = new ThongTinChung(Session);

                if (CongTy.MocTinhThueTNCN != null)
                    MocTinhThueTNCN = Common.Copy<MocTinhThueTNCN>(Session, CongTy.MocTinhThueTNCN);
                else
                    MocTinhThueTNCN = new MocTinhThueTNCN(Session);

                if (CongTy.MocQuyDoiThuNhapKhongThue != null)
                    MocQuyDoiThuNhapKhongThue = Common.Copy<MocQuyDoiThuNhapKhongThue>(Session, CongTy.MocQuyDoiThuNhapKhongThue);
                else
                    MocQuyDoiThuNhapKhongThue = new MocQuyDoiThuNhapKhongThue(Session);
            }
            else
            {
                ThongTinChung = new ThongTinChung(Session);
                MocTinhThueTNCN = new MocTinhThueTNCN(Session);
                MocQuyDoiThuNhapKhongThue = new MocQuyDoiThuNhapKhongThue(Session);
            }
            //
            DateTime currentDate = Common.GetServerCurrentTime();
            Thang = currentDate.Month;
            Nam = currentDate.Year;
            //
            UpdateQLChamCongList();
            UpdateQLChamCongNgoaiGioList();
            UpdateQLCongKhacList();
        }

        protected override void OnSaving()
        {
            base.OnSaving();
            //
            if (!IsDeleted)
            {
                if(QuanLyChamCong != null)
                {
                    QuanLyChamCong.KhoaChamCong = true;
                }
                if (QuanLyCongNgoaiGio != null)
                {
                    QuanLyCongNgoaiGio.KhoaChamCong = true;
                }
                if (QuanLyCongKhac != null)
                {
                    XPCollection<CC_QuanLyCongKhac> listQuanLyCongKhac = new XPCollection<CC_QuanLyCongKhac>(Session);
                    listQuanLyCongKhac.Criteria = CriteriaOperator.Parse("KyChamCong = ?", QuanLyCongKhac.Oid);
                    foreach (var item in listQuanLyCongKhac)
                        item.KhoaChamCong = true;
                }
            }
        }
        protected override void OnDeleting()
        {
            //
            if (!IsSaving)
            {
                if (QuanLyChamCong != null)
                {
                    QuanLyChamCong.KhoaChamCong = false;
                }
                if (QuanLyCongNgoaiGio != null)
                {
                    QuanLyCongNgoaiGio.KhoaChamCong = false;
                }

                if (QuanLyCongKhac != null)
                {
                    XPCollection<CC_QuanLyCongKhac> listQuanLyCongKhac = new XPCollection<CC_QuanLyCongKhac>(Session);
                    listQuanLyCongKhac.Criteria = CriteriaOperator.Parse("KyChamCong.Oid = ?", QuanLyCongKhac.Oid);
                    foreach (var item in listQuanLyCongKhac)
                        item.KhoaChamCong = false;
                }
            }
            //
            base.OnDeleting();
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateQLChamCongList();
            //
            UpdateQLChamCongNgoaiGioList();
            //
            UpdateQLCongKhacList();
        }

        /// <summary>
        /// Danh sách quản lý chấm công theo Trường
        /// </summary>
        private void UpdateQLChamCongList()
        {
            //
            if (QLChamCongList == null)
                QLChamCongList = new XPCollection<CC_QuanLyChamCong>(Session);
            //
            if (CongTy != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("CongTy=? and (!KyChamCong.KhoaSo or KyChamCong.KhoaSo is null)", CongTy.Oid);
                QLChamCongList.Criteria = filter;
            }
        }

        /// <summary>
        /// Danh sách quản lý chấm công ngoài giờ Theo trường
        /// </summary>
        private void UpdateQLChamCongNgoaiGioList()
        {
            //
            if (QLChamCongNgoaiGioList == null)
                QLChamCongNgoaiGioList = new XPCollection<CC_QuanLyCongNgoaiGio>(Session);
            //
            if (CongTy != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("CongTy=? and (!KyChamCong.KhoaSo or KyChamCong.KhoaSo is null)", CongTy.Oid);
                QLChamCongNgoaiGioList.Criteria = filter;
            }
        }

        /// <summary>
        /// Danh sách quản lý chấm công khác Theo trường
        /// </summary>
        private void UpdateQLCongKhacList()
        {
            //
            if (QLCongKhacList == null)
                QLCongKhacList = new XPCollection<CC_KyChamCong>(Session);
            //
            if (CongTy != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("CongTy=? and (!KhoaSo or KhoaSo is null)", CongTy.Oid);
                QLCongKhacList.Criteria = filter;
            }
        }

        void TinhSoNgayCongChuan()
        {
            if (CongTy != null && TuNgay != DateTime.MinValue && DenNgay != DateTime.MinValue)
            {
                //Nếu mần non và phổ thông thì tính thứ 7
                if (CongTy.BoPhanCha != null && !CongTy.BoPhanCha.Oid.Equals(Config.KeyDaiHoc_CaoDang) || CongTy.Equals(Config.KeyTTCEdu))
                {
                    SoNgay = Common.GetDayNumberSubtrackWeekend_ManNonAndPhoThong(TuNgay, DenNgay, Session);
                }
                else
                {
                    SoNgay = Common.GetDayNumberSubtrackWeekend_DaiHoc(TuNgay, DenNgay, Session);
                }
            }
            else
            {
                SoNgay = 0;
            }
        }

        protected override void OnSaved()
        {
            base.OnSaved();

            if (QuanLyChamCong != null)
            {
                int loai = 0;
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@QuanLyChamCong", QuanLyChamCong.Oid);
                param[1] = new SqlParameter("@Loai", loai); // 0: Thêm chi tiết -> tính công trước - sau điều chỉnh
                DataProvider.ExecuteNonQuery("spd_HoSoLuong_CapNhatCongTruocVaSauDieuChinh", CommandType.StoredProcedure, param);
            }
        }
    }

}
