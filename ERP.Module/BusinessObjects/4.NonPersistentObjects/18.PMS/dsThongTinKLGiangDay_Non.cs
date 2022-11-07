using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.NonPersistentObjects
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách môn học theo tkb")]
    [DefaultProperty("LopHocPhan")]
    public class dsThongTinKLGiangDay_Non : BaseObject
    {

        private string _Oid_TTKL;//Oid này chỉ lưu khi tạo mới 1 dòng        
        private bool _Chon; //Check vào những môn học... nào được tính thù lao 
        private string _TenBoPhanGiangDay;
        private string _MaNV;
        private string _HoTen;
        private string _TenBoPhan;
        private string _TenBacDaoTao;
        private string _TenHeDaoTao;
        private string _LoaiGiangVien;
        private string _TenLoaiHocPhan;
        private string _MaHocPhan;
        private string _TenHocPhan;
        private string _MaLopHocPhan;
        private string _TenLopSV;     
        private decimal _TongSoTietKeHoach;
        private decimal _TongSoTietThucDay;
        private decimal _TongGioQuyDoi;

        [Browsable(false)]
        public string Oid_TTKL
        {
            get { return _Oid_TTKL; }
            set { SetPropertyValue("Oid_TTKL", ref _Oid_TTKL, value); }
        }
        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get { return _Chon; }
            set { SetPropertyValue("Chon", ref _Chon, value); }
        }

        [ModelDefault("Caption", "Bộ phận giảng dạy")]
        [ModelDefault("AllowEdit", "False")]
        public string TenBoPhanGiangDay
        {
            get { return _TenBoPhanGiangDay; }
            set { SetPropertyValue("TenBoPhanGiangDay", ref _TenBoPhanGiangDay, value); }
        }


        [ModelDefault("Caption", "Mã nhân viên")]
        [ModelDefault("AllowEdit", "False")]
        public string MaNV
        {
            get { return _MaNV; }
            set { SetPropertyValue("MaNV", ref _MaNV, value); }
        }

        [ModelDefault("Caption", "Họ tên")]
        [ModelDefault("AllowEdit", "False")]
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }

        [ModelDefault("Caption", "Bộ phận")]
        [ModelDefault("AllowEdit", "False")]
        public string TenBoPhan
        {
            get { return _TenBoPhan; }
            set { SetPropertyValue("TenBoPhan", ref _TenBoPhan, value); }
        }

        [ModelDefault("Caption", "Bậc đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public string TenBacDaoTao
        {
            get { return _TenBacDaoTao; }
            set { SetPropertyValue("TenBoPhan", ref _TenBacDaoTao, value); }
        }
        [ModelDefault("Caption", "Hệ đào tạo")]
        [ModelDefault("AllowEdit", "False")]
        public string TenHeDaoTao
        {
            get { return _TenHeDaoTao; }
            set { SetPropertyValue("TenHeDaoTao", ref _TenHeDaoTao, value); }
        }     

        [ModelDefault("Caption", "Loại học phần")]
        [ModelDefault("AllowEdit", "False")]
        public string TenLoaiHocPhan
        {
            get { return _TenLoaiHocPhan; }
            set { SetPropertyValue("TenLoaiHocPhan", ref _TenLoaiHocPhan, value); }
        }

        [ModelDefault("Caption", "Mã học phần")]
        [ModelDefault("AllowEdit", "False")]
        public string MaHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("MaHocPhan", ref _MaHocPhan, value); }
        }
        [ModelDefault("Caption", "Tên học phần")]
        [ModelDefault("AllowEdit", "False")]
        public string TenHocPhan
        {
            get { return _MaHocPhan; }
            set { SetPropertyValue("TenHocPhan", ref _TenHocPhan, value); }
        }

        [ModelDefault("Caption", "Mã lớp học phần")]
        [ModelDefault("AllowEdit", "False")]
        public string MaLopHocPhan
        {
            get { return _MaLopHocPhan; }
            set { SetPropertyValue("MaLopHocPhan", ref _MaLopHocPhan, value); }
        }

        [ModelDefault("Caption", "Tên lớp sinh viên")]
        [ModelDefault("AllowEdit", "False")]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }
      

        [ModelDefault("Caption", "Tổng số tiết kế hoạch")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoTietKeHoach
        {
            get { return _TongSoTietKeHoach; }
            set { SetPropertyValue("TongSoTietKeHoach", ref _TongSoTietKeHoach, value); }
        }

        [ModelDefault("Caption", "Tổng số tiết thực dạy")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoTietThucDay
        {
            get { return _TongSoTietThucDay; }
            set { SetPropertyValue("TongSoTietThucDay", ref _TongSoTietThucDay, value); }
        }

        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioQuyDoi
        {
            get { return _TongGioQuyDoi; }
            set { SetPropertyValue("TongGioQuyDoi", ref _TongGioQuyDoi, value); }
        }

        public dsThongTinKLGiangDay_Non(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
