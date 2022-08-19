using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.NghiepVu.NhanSu.QuaTrinh
{
    //[ImageName("BO_QuaTrinh")]
    [ModelDefault("Caption", "Quá trình khen thưởng")]
    public class QuaTrinhKhenThuong : BaseObject
    {
        private QuyetDinh _QuyetDinh;
        private string _SoQuyetDinh;
        private DateTime _NgayQuyetDinh;
        private ThongTinNhanVien _ThongTinNhanVien;
        private DateTime _NgayKhenThuong;
        private DanhHieuKhenThuong _DanhHieuKhenThuong;
        private string _LyDo;

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListQuaTrinhKhenThuong")]
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

        [ModelDefault("Caption", "Quyết định")]
        [Browsable(false)]
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
                if (!IsLoading && value != null)
                {
                    SoQuyetDinh = value.SoQuyetDinh;
                    NgayQuyetDinh = value.NgayQuyetDinh;
                }
            }
        }

        [ModelDefault("Caption", "Số quyết định")]
        public string SoQuyetDinh
        {
            get
            {
                return _SoQuyetDinh;
            }
            set
            {
                SetPropertyValue("SoQuyetDinh", ref _SoQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày quyết định")]
        public DateTime NgayQuyetDinh
        {
            get
            {
                return _NgayQuyetDinh;
            }
            set
            {
                SetPropertyValue("NgayQuyetDinh", ref _NgayQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày khen thưởng")]
        public DateTime NgayKhenThuong
        {
            get
            {
                return _NgayKhenThuong;
            }
            set
            {
                SetPropertyValue("NgayKhenThuong", ref _NgayKhenThuong, value);
            }
        }

        [ModelDefault("Caption", "Danh hiệu khen thưởng")]
        public DanhHieuKhenThuong DanhHieuKhenThuong
        {
            get
            {
                return _DanhHieuKhenThuong;
            }
            set
            {
                SetPropertyValue("DanhHieuKhenThuong", ref _DanhHieuKhenThuong, value);
            }
        }

        [Size(-1)]
        [ModelDefault("Caption", "Lý Do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        public QuaTrinhKhenThuong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }

}
