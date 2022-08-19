using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.PMS;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NonPersistentObjects;
using ERP.Module.NghiepVu.PMS.DanhMuc;
using System.Data.SqlClient;
using System.Data;

namespace ERP.Module.NghiepVu.PMS.DonGiaGiangVienTG
{
    [DefaultClassOptions]
    [ImageName("BO_List")]
    [ModelDefault("Caption", "Đơn giá giảng viên đặc biệt")]
    public class DonGiaGiangVienThinhGiangDacBiet : BaseObject
    {
        private QuanLyDonGiaDacBiet _QuanLyDonGiaDacBiet;
        private NhanVien _NhanVien;
        private dsMonHocTheoTKB _dsMonHocTheoTKB;
        private string _LopHocPhan;
        private decimal _DonGia;
        private LoaiTien _LoaiTien;
        private decimal _TyLe;

        [Association("QuanLyDonGiaDacBiet-ListDonGiaGiangVienThinhGiangDacBiet")]
        [Browsable(false)]
        [ModelDefault("Caption", "Quản lý đơn giá đặc biệt")]
        public QuanLyDonGiaDacBiet QuanLyDonGiaDacBiet
        {
            get
            {
                return _QuanLyDonGiaDacBiet;
            }
            set
            {
                SetPropertyValue("QuanLyDonGiaDacBiet", ref _QuanLyDonGiaDacBiet, value);               
            }
        }

        [ModelDefault("Caption", "Giảng viên")]
        [ImmediatePostData]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
                if (value != null)
                {
                    LoadData();
                }
            }
        }

        [ModelDefault("Caption", "Môn học")]
        [NonPersistent]
        [DataSourceProperty("ListdsMonHocTheoTKB")]
        [ImmediatePostData]
        public dsMonHocTheoTKB dsMonHocTheoTKB
        {
            get { return _dsMonHocTheoTKB; }
            set
            {
                SetPropertyValue("dsMonHocTheoTKB", ref _dsMonHocTheoTKB, value);
                if(!IsLoading && value != null)
                {
                    LopHocPhan = value.LopHocPhan;
                }
            }
        }


        [ModelDefault("Caption", "Lớp học phần")]
        [ModelDefault("AllowEdit", "false")]
        public string LopHocPhan
        {
            get { return _LopHocPhan; }
            set { SetPropertyValue("LopHocPhan", ref _LopHocPhan, value); }
        }     

        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "Loại tiền")]
        public LoaiTien LoaiTien
        {
            get { return _LoaiTien; }
            set { SetPropertyValue("LoaiTien", ref _LoaiTien, value); }
        }

        [ModelDefault("Caption", "Tỷ lệ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TyLe
        {
            get { return _TyLe; }
            set { SetPropertyValue("TyLe", ref _TyLe, value); }
        }

        public DonGiaGiangVienThinhGiangDacBiet(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [Browsable(false)]
        public XPCollection<dsMonHocTheoTKB> ListdsMonHocTheoTKB
        {
            get; set;
        }

        public void LoadData()
        {
            if(ListdsMonHocTheoTKB != null)
            {
                ListdsMonHocTheoTKB.Reload();
            }
            else
            {
                ListdsMonHocTheoTKB = new XPCollection<dsMonHocTheoTKB>(Session, false);
            }

            SqlParameter[] param = new SqlParameter[2]; /*Số parameter trên Store Procedure*/
            param[0] = new SqlParameter("@QuanLyDonGiaDacBiet", QuanLyDonGiaDacBiet.Oid);
            param[1] = new SqlParameter("@NhanVien", NhanVien.Oid);
            DataTable dt = DataProvider.GetDataTable("spd_PMS_LayDuLieuMonHocTheoTKB", System.Data.CommandType.StoredProcedure, param);
            if (dt != null)
            {
                foreach (DataRow item in dt.Rows)
                {
                    dsMonHocTheoTKB ds = new dsMonHocTheoTKB(Session);
                    ds.MaHocPhan = item["MaHocPhan"].ToString();
                    ds.TenHocPhan = item["TenHocPhan"].ToString();
                    ds.LopHocPhan = item["LopHocPhan"].ToString();
                    ListdsMonHocTheoTKB.Add(ds);
                }
            }
        }

    }
}
