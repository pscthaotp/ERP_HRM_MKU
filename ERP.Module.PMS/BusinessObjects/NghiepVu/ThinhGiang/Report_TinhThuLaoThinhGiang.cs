using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Data.Filtering;
using DevExpress.Xpo.DB;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Commons;
using ERP.Module.PMS.DanhMuc;
using System.Collections.Generic;
using System.Data.SqlClient;
using ERP.Module.Extends;
using System.Data;
using ERP.Module.PMS.Enum;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý tính thù lao thỉnh giảng")]
    [DefaultProperty("Caption")]
    [NonPersistent]
    [Appearance("Khoa", TargetItems = "*", Enabled = false, Criteria = "Khoa = 1 and DaTinhThuLao = 1")]
    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "CongTy;NamHoc;DotTinh", "Bảng chốt thông tin giảng dạy đã tồn tại")]
    public class Report_TinhThuLaoThinhGiang : BaseObject
    {
        private BangChotThuLao_ThinhGiang _BangChotThuLao_ThinhGiang;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private CongTy _CongTy;
        private bool _Khoa;
        private bool _DaTinhThuLao;
        private DateTime _NgayChot;
        private DotTinhPMS _DotTinh;
        private NhanVien _NhanVien;

        [ModelDefault("Caption", "Bảng chốt thù lao (thỉnh giảng)")]
        [RuleRequiredField(DefaultContexts.Save)]
        public BangChotThuLao_ThinhGiang BangChotThuLao_ThinhGiang
        {
            get { return _BangChotThuLao_ThinhGiang; }
            set { SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value); }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "User")]
        public string User
        {
            get;
            set;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        //[ModelDefault("AllowEdit","False")]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { SetPropertyValue("CongTy", ref _CongTy, value); }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Năm học")]
        [VisibleInListView(false)]
        [ImmediatePostData]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set
            {
                SetPropertyValue("NamHoc", ref _NamHoc, value);
                if (!IsLoading && value != null)
                {
                    HocKy = null;
                    //DotTinh = null; //ThuHuong tắt code không dùng
                    UpdateHocKy();
                    DotTinh = null;
                    UpdateDotTinh();
                }
            }
        }
        [Browsable(false)]
          [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NamHoc.ListHocKy")]
        [ModelDefault("Caption", "Học kỳ")]
        //[DataSourceProperty("listHocKy")]  Đông tắt
        [VisibleInListView(false)]
        [ImmediatePostData]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set
            {
                SetPropertyValue("HocKy", ref _HocKy, value);
                if (!IsLoading && value != null)
                {
                    DotTinh = null;
                    UpdateDotTinh();
                }
            }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [ModelDefault("Caption", "Đã tính thù lao")]
        [ModelDefault("AllowEdit","false")]
        [Browsable(false)]
        public bool DaTinhThuLao
        {
            get { return _DaTinhThuLao; }
            set { SetPropertyValue("DaTinhThuLao", ref _DaTinhThuLao, value); }
        }
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("Caption", "Đợt tính PMS")]
        [DataSourceProperty("listDotTinhPMS", DataSourcePropertyIsNullMode.SelectAll)]
        [ImmediatePostData]
        [VisibleInListView(false)]
        [Browsable(false)]
        public DotTinhPMS DotTinh
        {
            get { return _DotTinh; }
            set { SetPropertyValue("DotTinh", ref _DotTinh, value); }
        }

        [ModelDefault("Caption", "Ngày chốt")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        public DateTime NgayChot
        {
            get { return _NgayChot; }
            set { SetPropertyValue("NgayChot", ref _NgayChot, value); }
        }

        [ModelDefault("Caption", "Nhân viên")]
        //[DataSourceProperty("listNV", DataSourcePropertyIsNullMode.SelectAll)]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ModelDefault("Caption", "Danh sách thanh toán")]
        public XPCollection<ChiTietThinhGiang_ThanhToanThuLao> ListChiTietThinhGiang_ThanhToanThuLao
        {
            get;
            set;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        public XPCollection<HocKy> listHocKy
        {
            get;
            set;
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Đợt tính List")]
        public XPCollection<DotTinhPMS> listDotTinhPMS
        {
            get;
            set;
        }

        [VisibleInDetailView(false)]
        [NonPersistent]
        [ModelDefault("Caption", "Thông tin")]
        public string Caption
        {
            get
            {
                //return String.Format("{0} - Năm học  {1} {2}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "", HocKy != null ? " - " + HocKy.TenHocKy : "");
                return String.Format("{0} - Năm học  {1} {2}", CongTy != null ? CongTy.TenBoPhan : "", NamHoc != null ? NamHoc.TenNamHoc : "", DotTinh != null ? " - " + DotTinh.TenKy : "");      
            
            }
        }
        


        public Report_TinhThuLaoThinhGiang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CongTy = Common.CongTy(Session);
            NamHoc = Common.GetCurrentNamHoc(Session);
            NgayChot = DateTime.Now;
        }

        public void UpdateHocKy()
        {
            CriteriaOperator filter = CriteriaOperator.Parse("NamHoc = ?", NamHoc.Oid);
            XPCollection<HocKy> DS_HocKy = new XPCollection<HocKy>(Session, filter);
            if (listHocKy != null)
            {
                listHocKy.Reload();
            }
            else
            {
                listHocKy = new XPCollection<HocKy>(Session, false);
            }
            foreach (HocKy item in DS_HocKy)
            {
                listHocKy.Add(item);
            }
            OnChanged("listHocKy");
        }

        public void UpdateDotTinh()
        {         
            if (NamHoc != null)
            {
                if (listDotTinhPMS != null)
                {
                    listDotTinhPMS.Reload();
                }
                else
                {
                    listDotTinhPMS = new XPCollection<DotTinhPMS>(Session, false);
                }
                CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=?", NamHoc.Oid); //Lấy toàn bộ đợt tinh theo NamHoc 
                XPCollection<DotTinhPMS> DS_List = new XPCollection<DotTinhPMS>(Session, filter);

                foreach (DotTinhPMS item in DS_List)
                {
                    listDotTinhPMS.Add(item);
                }

            }
        }

        //Thực hiện việc Load toàn bộ dữ liệu 
        public void LoadData()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách chi tiết khối lượng thanh toán"))
            {
                if (NamHoc != null)
                {                    
                    if (ListChiTietThinhGiang_ThanhToanThuLao == null)
                        ListChiTietThinhGiang_ThanhToanThuLao = new XPCollection<ChiTietThinhGiang_ThanhToanThuLao>(Session, false);
                    else
                        ListChiTietThinhGiang_ThanhToanThuLao.Reload();
                    //
                    SqlParameter[] param = new SqlParameter[3]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@BangChotThuLao_ThinhGiang",BangChotThuLao_ThinhGiang!=null? BangChotThuLao_ThinhGiang.Oid:Guid.Empty);
                    param[1] = new SqlParameter("@User", User);
                    param[2] = new SqlParameter("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
                    DataTable dt = DataProvider.GetDataTable("spd_PMS_BangChotThuLao_LayKhoiLuong_ThinhGiang", System.Data.CommandType.StoredProcedure, param);
                    if (dt != null)
                    {
                        ChiTietThinhGiang_ThanhToanThuLao ctbangchot;
                        foreach (DataRow item in dt.Rows)
                        {
                            ctbangchot = new ChiTietThinhGiang_ThanhToanThuLao(Session);
                            ctbangchot.Oid = new Guid();
                            ctbangchot.OidChiTietHoatDong_String = item["OidChiTietHoatDong_String"].ToString();
                            ctbangchot.OidNhanVien = item["NhanVien"].ToString();
                            ctbangchot.OidBoPhan = item["BoPhan"].ToString();
                            //Kiểm tra từng loại hoạt động
                            if (item["LoaiHoatDong"].ToString() == "0")
                                ctbangchot.LoaiHoatDong = LoaiHoatDongEnum.DaoTaoChinhQuy;
                            else if (item["LoaiHoatDong"].ToString() == "8")
                                ctbangchot.LoaiHoatDong = LoaiHoatDongEnum.GiangDay;
                            else if (item["LoaiHoatDong"].ToString() == "10")
                                ctbangchot.LoaiHoatDong = LoaiHoatDongEnum.RaDe;
                            else if (item["LoaiHoatDong"].ToString() == "2")
                                ctbangchot.LoaiHoatDong = LoaiHoatDongEnum.ChamBai;
                            else if (item["LoaiHoatDong"].ToString() == "11")
                                ctbangchot.LoaiHoatDong = LoaiHoatDongEnum.CoiThi;
                            //
                            ctbangchot.MaNV = item["MaNV"].ToString();
                            ctbangchot.HoTen = item["HoTen"].ToString();
                            ctbangchot.TenBacDaoTao = item["TenBacDaoTao"].ToString();
                            ctbangchot.TenHeDaoTao = item["TenHeDaoTao"].ToString();
                            ctbangchot.TenBoPhan = item["TenBoPhan"].ToString();
                            ctbangchot.NoiDungHoatDong = item["NoiDungHoatDong"].ToString();
                            ctbangchot.TenLopSV = item["TenLopSV"].ToString();
                            string soluongde = item["SoLuongDe"].ToString();
                            try
                            {
                                ctbangchot.TongSoTietThucDay = Convert.ToDecimal(item["TongGio_SoTietThucDay"].ToString());
                                ctbangchot.TongSoTietKeHoach = Convert.ToDecimal(item["TongGio_SoTietKeHoach"].ToString());
                                ctbangchot.TongGioQuyDoi = Convert.ToDecimal(item["TongGio_QuyDoi"].ToString());
                                if (soluongde != "")
                                {
                                    ctbangchot.SoLuongDe = Convert.ToInt32(soluongde);
                                }         

                            }
                            catch (Exception ex)
                            {
                                ctbangchot.TongSoTietThucDay = 0;
                                ctbangchot.TongSoTietKeHoach = 0;
                                ctbangchot.TongGioQuyDoi = 0;
                                ctbangchot.SoLuongDe = 0;
                            }

                            //ctbangchot.Chon = true;
                            //
                            ListChiTietThinhGiang_ThanhToanThuLao.Add(ctbangchot);
                            //
                            ctbangchot.Reload();
                        }
                    }
                }
            }
        }

    }
}
