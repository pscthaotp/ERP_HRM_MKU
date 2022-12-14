using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    //[ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình tham gia lực lượng vũ trang")]
    public class QuaTrinhThamGiaLucLuongVuTrang : BaseObject
    {
        private int _STT;
        private ThongTinNhanVien _ThongTinNhanVien;
        private string _NgayNhapNgu;
        private string _NgayXuatNgu;
        private QuanHam _QuanHam;
        private string _NoiDung;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListQuaTrinhThamGiaLucLuongVuTrang")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
            }
        }

        [ModelDefault("Caption", "Số thứ tự")]
        public int STT
        {
            get
            {
                return _STT;
            }
            set
            {
                SetPropertyValue("STT", ref _STT, value);
            }
        }

        [ModelDefault("Caption", "Ngày nhập ngũ")]
        public string NgayNhapNgu
        {
            get
            {
                return _NgayNhapNgu;
            }
            set
            {
                SetPropertyValue("NgayNhapNgu", ref _NgayNhapNgu, value);
            }
        }

        [ModelDefault("Caption", "Ngày xuất ngũ")]
        public string NgayXuatNgu
        {
            get
            {
                return _NgayXuatNgu;
            }
            set
            {
                SetPropertyValue("NgayXuatNgu", ref _NgayXuatNgu, value);
            }
        }

        [ModelDefault("Caption", "Quân hàm")]
        public QuanHam QuanHam
        {
            get
            {
                return _QuanHam;
            }
            set
            {
                SetPropertyValue("QuanHam", ref _QuanHam, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Nội dung")]
        public string NoiDung
        {
            get
            {
                return _NoiDung;
            }
            set
            {
                SetPropertyValue("NoiDung", ref _NoiDung, value);
            }
        }

        public QuaTrinhThamGiaLucLuongVuTrang(Session session) : base(session) { }

    }

}
