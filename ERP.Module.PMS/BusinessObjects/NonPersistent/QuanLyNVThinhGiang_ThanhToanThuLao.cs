using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.DanhMuc;
using ERP.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách thực hiện thanh toán tính thù lao")]
    public class QuanLyNVThinhGiang_ThanhToanThuLao : BaseObject
    {
        private Guid _BangChotThuLao;
        //private HocKy _HocKy;
        private NhanVien _NhanVien;

        [Browsable(false)]
        [ModelDefault("Caption", "Bảng chốt thù lao")]
        public Guid BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set { SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value); }
        }
        [Browsable(false)]
        [ModelDefault("Caption", "User")]
        public string User
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Công ty")]
        [ModelDefault("AllowEdit", "False")]
        public CongTy CongTy
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Năm học")]
        [ModelDefault("AllowEdit", "False")]
        public NamHoc NamHoc
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Học kỳ")]
        [ModelDefault("AllowEdit", "False")]
        public HocKy HocKy
        {
            get;
            set;
        } 
        [ModelDefault("Caption", "Đợt tính")]
        [ModelDefault("AllowEdit", "False")]
        public DotTinhPMS DotTinh
        {
            get;
            set;
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
        //
        [Browsable(false)]
        [ModelDefault("Caption", "Hoc kỳ List")]
        public XPCollection<NhanVien> listNV
        {
            get;
            set;
        }
        [ModelDefault("Caption", "Danh sách bảng chốt thù lao thỉnh giảng")]
        public XPCollection<dsQuanLyNVThinhGiang_ThanhToanThuLao> listBangChot
        {
            get;
            set;
        }
        //
        public QuanLyNVThinhGiang_ThanhToanThuLao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listBangChot = new XPCollection<dsQuanLyNVThinhGiang_ThanhToanThuLao>(Session, false);
            
        }
        //Thực hiện Load danh sách NV thuộc trường Yerisin
        public void UpdateNV()
        {
            if (NamHoc != null)
            {
                XPCollection<NhanVien> ls = new XPCollection<NhanVien>(Session, CriteriaOperator.Parse("CongTy.TenBoPhan like ?", "Trường Đại học Yersin Đà Lạt"));
                if (listNV != null)
                {
                    foreach (NhanVien item in ls)
                    {
                        listNV.Add(item);
                    }
                }
                else
                    listNV = new XPCollection<NhanVien>(Session, false);
            }
           
        }

        //Thực hiện việc Load toàn bộ dữ liệu 
        public void LoadData()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách chi tiết khối lượng thanh toán"))
            {
                if (NamHoc != null)
                {
                    listBangChot.Reload();
                    if (listBangChot == null)
                        listBangChot = new XPCollection<dsQuanLyNVThinhGiang_ThanhToanThuLao>(Session, false);
                    else
                        listBangChot.Reload();
                    //
                    SqlParameter[] param = new SqlParameter[3]; /*Số parameter trên Store Procedure*/
                    param[0] = new SqlParameter("@BangChotThuLao_ThinhGiang", BangChotThuLao);
                    param[1] = new SqlParameter("@User", User);
                    param[2] = new SqlParameter("@NhanVien", NhanVien != null ? NhanVien.Oid : Guid.Empty);
                    DataTable dt = DataProvider.GetDataTable("spd_PMS_BangChotThuLao_LayKhoiLuong_ThinhGiang", System.Data.CommandType.StoredProcedure, param);
                    if (dt != null)
                    {
                        dsQuanLyNVThinhGiang_ThanhToanThuLao ctbangchot;
                        foreach (DataRow item in dt.Rows)
                        {
                            ctbangchot = new dsQuanLyNVThinhGiang_ThanhToanThuLao(Session);
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
                            if (item["TichChon"].ToString() == "1")
                            {
                                ctbangchot.Chon = true;
                            }
                            else
                            {
                                ctbangchot.Chon = false;
                            }
                                                  
                            listBangChot.Add(ctbangchot);                           
                            ctbangchot.Reload();
                        }
                    }
                }
            }
        }
    }
}
