using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.BaseImpl;
using System.Data;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Extends;

namespace ERP.Module.NonPersistentObjects
{
    [ModelDefault("Caption", "Danh sách thông tin khối lượng giảng dạy")]
    [NonPersistent]
    public class QuanLyThongTinKLGiangDay_Non : BaseObject
    {
        private BoPhan _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;

        private Guid _BangChotThuLao_ThinhGiang;


        [Browsable(false)]
        public Guid BangChotThuLao_ThinhGiang
        {
            get { return _BangChotThuLao_ThinhGiang; }
            set { SetPropertyValue("BangChotThuLao_ThinhGiang", ref _BangChotThuLao_ThinhGiang, value); }
        }


        [ModelDefault("Caption", "Trường")]      
        [ModelDefault("AllowEdit", "false")]
        public BoPhan ThongTinTruong
        {
            get;
            set;
        }

        [RuleRequiredField("", DefaultContexts.Save)]
        [ImmediatePostData]
        public NamHoc NamHoc
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Học kỳ)")]      
        public HocKy HocKy
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Bộ phận giảng dạy")]
        public BoPhan BoPhanGiangDay
        {
            get;
            set;
        }

        [ModelDefault("Caption", "Danh sách thông tin khối lượng giảng dạy")]
        public XPCollection<dsThongTinKLGiangDay_Non> listTTKL
        {
            get;
            set;
        }

        public QuanLyThongTinKLGiangDay_Non(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            listTTKL = new XPCollection<dsThongTinKLGiangDay_Non>(Session, false);
        }

        //Thực hiện việc Load toàn bộ dữ liệu 
        public void LoadData()
        {
            using (DialogUtil.AutoWait("Đang lấy danh sách thông tin khối lượng giảng dạy"))
            {
                if (NamHoc != null)
                {
                    listTTKL.Reload();
                    if (listTTKL == null)
                        listTTKL = new XPCollection<dsThongTinKLGiangDay_Non>(Session, false);
                    else
                        listTTKL.Reload();
                    //
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@BangChotThuLao_ThinhGiang", BangChotThuLao_ThinhGiang);
                    param[1] = new SqlParameter("@BoPhanGiangDay", BoPhanGiangDay != null ? BoPhanGiangDay.Oid : Guid.Empty);
                    using (DataTable dt = DataProvider.GetDataTable("spd_PMS_BangChotThuLao_LayThongTinKhoiLuong", CommandType.StoredProcedure, param))
                    {                       
                        if (dt != null)
                        {
                            dsThongTinKLGiangDay_Non ttkl;
                            foreach (DataRow item in dt.Rows)
                            {
                                ttkl = new dsThongTinKLGiangDay_Non(Session);
                                ttkl.Oid_TTKL = item["Oid_TTKL"].ToString();
                                ttkl.TenBoPhanGiangDay = item["BoPhanGiangDay"].ToString();
                                ttkl.MaNV = item["MaNV"].ToString();
                                ttkl.HoTen = item["HoTen"].ToString();
                                ttkl.TenBacDaoTao = item["TenBacDaoTao"].ToString();
                                ttkl.TenHeDaoTao = item["TenHeDaoTao"].ToString();
                                ttkl.TenBoPhan = item["TenBoPhan"].ToString();
                                
                                ttkl.TenLoaiHocPhan = item["TenLoaiHocPhan"].ToString();
                                ttkl.MaHocPhan = item["MaHocPhan"].ToString();
                                ttkl.TenHocPhan = item["TenHocPhan"].ToString();
                                ttkl.MaLopHocPhan = item["MaLopHocPhan"].ToString();
                                ttkl.TenLopSV = item["TenLopSV"].ToString();
                                try
                                {                                
                                    ttkl.TongSoTietThucDay = Convert.ToDecimal(item["TongGio_SoTietThucDay"].ToString());
                                    ttkl.TongSoTietKeHoach = Convert.ToDecimal(item["TongGio_SoTietKeHoach"].ToString());
                                    ttkl.TongGioQuyDoi = Convert.ToDecimal(item["TongGio_QuyDoi"].ToString());
                                }
                                catch (Exception ex)
                                {                                  
                                    ttkl.TongSoTietThucDay = 0;
                                    ttkl.TongSoTietKeHoach = 0;
                                    ttkl.TongGioQuyDoi = 0;
                                }

                                if (item["TichChon"].ToString() == "1")
                                {
                                    ttkl.Chon = true;
                                }
                                else
                                {
                                    ttkl.Chon = false;
                                }
                                listTTKL.Add(ttkl);
                            }

                        }
                    }
                }

            }
        }
    }
}
