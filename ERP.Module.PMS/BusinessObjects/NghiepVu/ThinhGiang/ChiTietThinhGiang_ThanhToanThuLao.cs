using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.PMS.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [NonPersistent]
    [ModelDefault("Caption", "Danh sách thanh toán thù lao thỉnh giảng")]
    public class ChiTietThinhGiang_ThanhToanThuLao : BaseObject
    {
        private Guid _Oid;//Oid này chỉ lưu khi tạo mới 1 dòng 
        private string _OidChiTietHoatDong_String;//Oid này truyền xuống phân tách dữ liệu tính thù lao cho thỉnh giảng 
        private string _OidBangChotThuLao;
        private string _OidNhanVien;
        private string _OidBoPhan;
        //
        //private bool _Chon; //Check vào những học phần, đồ án,... nào được tính thù lao 
        //
        private string _MaNV;
        private string _HoTen;
        private string _TenBoPhan;
        private string _TenBacDaoTao;
        private string _TenHeDaoTao;
        private LoaiHoatDongEnum? _LoaiHoatDong;
        private string _NoiDungHoatDong;
        private string _TenLopSV;
        private decimal _TongSoTietKeHoach; //Thu Hương mới thêm
        private decimal _TongSoTietThucDay; //Thu Huong mới thêm 
        private decimal _TongGio;
        private int _SoLuongDe;
        //
        [ModelDefault("Caption", "Oid")]
        [Browsable(false)]
        public Guid Oid
        {
            get { return _Oid; }
            set { SetPropertyValue("Oid", ref _Oid, value); }
        }
        [ModelDefault("Caption", "OidChiTietHoatDong_String")]
        [Browsable(false)]
        public string OidChiTietHoatDong_String
        {
            get { return _OidChiTietHoatDong_String; }
            set { SetPropertyValue("OidChiTietHoatDong_String", ref _OidChiTietHoatDong_String, value); }
        }
        [ModelDefault("Caption", "OidBangChotThuLao")]
        [Browsable(false)]
        public string OidBangChotThuLao
        {
            get { return _OidBangChotThuLao; }
            set { SetPropertyValue("OidBangChotThuLao", ref _OidBangChotThuLao, value); }
        }
        [ModelDefault("Caption", "OidNhanVien")]
        [Browsable(false)]
        public string OidNhanVien
        {
            get { return _OidNhanVien; }
            set { SetPropertyValue("OidNhanVien", ref _OidNhanVien, value); }
        }
        [ModelDefault("Caption", "OidBoPhan")]
        [Browsable(false)]
        public string OidBoPhan
        {
            get { return _OidBoPhan; }
            set { SetPropertyValue("OidBoPhan", ref _OidBoPhan, value); }
        }
        //
        //[ModelDefault("Caption", "Chọn")]
        //public bool Chon
        //{
        //    get { return _Chon; }
        //    set { SetPropertyValue("Chon", ref _Chon, value); }
        //}
        //
        [ModelDefault("Caption", "Mã quản lý")]       
        public string MaNV
        {
            get { return _MaNV; }
            set { SetPropertyValue("MaNV", ref _MaNV, value); }
        }
        [ModelDefault("Caption", "Họ tên")]     
        public string HoTen
        {
            get { return _HoTen; }
            set { SetPropertyValue("HoTen", ref _HoTen, value); }
        }
        [ModelDefault("Caption", "Bộ phận")]     
        public string TenBoPhan
        {
            get { return _TenBoPhan; }
            set { SetPropertyValue("TenBoPhan", ref _TenBoPhan, value); }
        }
        [ModelDefault("Caption", "Bậc đào tạo")]     
        public string TenBacDaoTao
        {
            get { return _TenBacDaoTao; }
            set { SetPropertyValue("TenBoPhan", ref _TenBacDaoTao, value); }
        }
        [ModelDefault("Caption", "Hệ đào tạo")]      
        public string TenHeDaoTao
        {
            get { return _TenHeDaoTao; }
            set { SetPropertyValue("TenHeDaoTao", ref _TenHeDaoTao, value); }
        }
        [ModelDefault("Caption", "Ghi chú")]       
        public LoaiHoatDongEnum? LoaiHoatDong
        {
            get { return _LoaiHoatDong; }
            set { SetPropertyValue("LoaiHoatDong", ref _LoaiHoatDong, value); }
        }
        [ModelDefault("Caption", "Lớp học phần")]      
        public string NoiDungHoatDong
        {
            get { return _NoiDungHoatDong; }
            set { SetPropertyValue("NoiDungHoatDong", ref _NoiDungHoatDong, value); }
        }
        [ModelDefault("Caption", "Tên lớp SV")]
        public string TenLopSV
        {
            get { return _TenLopSV; }
            set { SetPropertyValue("TenLopSV", ref _TenLopSV, value); }
        }

        [ModelDefault("Caption", "Tổng số tiết kế hoạch")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoTietKeHoach
        {
            get { return _TongSoTietKeHoach; }
            set { SetPropertyValue("TongSoTietKeHoach", ref _TongSoTietKeHoach, value); }
        }

        [ModelDefault("Caption", "Tổng số tiết thực dạy")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongSoTietThucDay
        {
            get { return _TongSoTietThucDay; }
            set { SetPropertyValue("TongSoTietThucDay", ref _TongSoTietThucDay, value); }
        }

        [ModelDefault("Caption", "Tổng giờ quy đổi")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal TongGioQuyDoi
        {
            get { return _TongGio; }
            set { SetPropertyValue("TongGio", ref _TongGio, value); }
        }

        [ModelDefault("Caption", "Số lượng đề")]
        public int SoLuongDe
        {
            get { return _SoLuongDe; }
            set { SetPropertyValue("SoLuongDe", ref _SoLuongDe, value); }
        }

        public ChiTietThinhGiang_ThanhToanThuLao(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
