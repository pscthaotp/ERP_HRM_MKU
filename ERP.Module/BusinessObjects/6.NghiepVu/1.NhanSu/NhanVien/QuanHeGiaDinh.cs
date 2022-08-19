using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Commons;
//
namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [ImageName("BO_GiaDinh")]
    [DefaultProperty("HoTenNguoiThan")]
    [ModelDefault("Caption", "Quan hệ gia đình")]
    [Appearance("QuanHeGiaDinh", TargetItems = "NuocCuTru;NamDiCu", Enabled = false, Criteria = "PhanLoai=1")]
    public class QuanHeGiaDinh : BaseObject
    {
        private LoaiQuocGiaEnum _PhanLoai;
        private int _NamDiCu;
        private QuocGia _QuocTich;
        private NhanVien _NhanVien;
        private string _HoTenNguoiThan;
        private GioiTinhEnum _GioiTinh;
        private QuanHe _QuanHe;
        private DateTime _NgaySinh;
        private TinhTrangEnum _TinhTrang;
        private string _NgheNghiepHienTai;
        private string _NoiLamViec;
        private QuocGia _NuocCuTru;
        private TonGiao _TonGiao;
        private DanToc _DanToc;
        private TinhThanh _QueQuan;
        private string _DienThoaiDiDong;
        private string _NoiOHienNay;

        [Browsable(false)]
        [ModelDefault("Caption", "Cán bộ")]
        [Association("NhanVien-ListQuanHeGiaDinh")]
        public NhanVien NhanVien
        {
            get
            {
                return _NhanVien;
            }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Phân loại")]
        public LoaiQuocGiaEnum PhanLoai
        {
            get
            {
                return _PhanLoai;
            }
            set
            {
                SetPropertyValue("PhanLoai", ref _PhanLoai, value);
                if (!IsLoading)
                {
                    if (PhanLoai == LoaiQuocGiaEnum.NgoaiNuoc)
                        QuocTich = null;
                    else
                        QuocTich = Common.GetQuocGia(Session,"việt nam");
                }

            }
        }

        [ModelDefault("Caption", "Tên người thân")]
        [RuleRequiredField(DefaultContexts.Save)]
        public string HoTenNguoiThan
        {
            get
            {
                return _HoTenNguoiThan;
            }
            set
            {
                SetPropertyValue("HoTenNguoiThan", ref _HoTenNguoiThan, value);
            }
        }

        [ModelDefault("Caption", "Giới tính")]
        public GioiTinhEnum GioiTinh
        {
            get
            {
                return _GioiTinh;
            }
            set
            {
                SetPropertyValue("GioiTinh", ref _GioiTinh, value);
            }
        }

        [ModelDefault("Caption", "Ngày sinh")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime NgaySinh
        {
            get
            {
                return _NgaySinh;
            }
            set
            {
                SetPropertyValue("NgaySinh", ref _NgaySinh, value);
            }
        }

        [ModelDefault("Caption", "Quan hệ")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuanHe QuanHe
        {
            get
            {
                return _QuanHe;
            }
            set
            {
                SetPropertyValue("QuanHe", ref _QuanHe, value);
            }
        }

        [ModelDefault("Caption", "Quê quán")]
        public TinhThanh QueQuan
        {
            get
            {
                return _QueQuan;
            }
            set
            {
                SetPropertyValue("QueQuan", ref _QueQuan, value);
            }
        }

        [ModelDefault("Caption", "Nơi ở hiện nay")]
        public string NoiOHienNay
        {
            get
            {
                return _NoiOHienNay;
            }
            set
            {
                SetPropertyValue("NoiOHienNay", ref _NoiOHienNay, value);
            }
        }



        [ModelDefault("Caption", "Dân tộc")]
        public DanToc DanToc
        {
            get
            {
                return _DanToc;
            }
            set
            {
                SetPropertyValue("DanToc", ref _DanToc, value);
            }
        }

        [ModelDefault("Caption", "Tôn giáo")]
        public TonGiao TonGiao
        {
            get
            {
                return _TonGiao;
            }
            set
            {
                SetPropertyValue("TonGiao", ref _TonGiao, value);
            }
        }

        [ModelDefault("Caption", "Quốc tịch")]
        public QuocGia QuocTich
        {
            get
            {
                return _QuocTich;
            }
            set
            {
                SetPropertyValue("QuocTich", ref _QuocTich, value);
            }
        }

        [ModelDefault("Caption", "Nước cư trú")]
        public QuocGia NuocCuTru
        {
            get
            {
                return _NuocCuTru;
            }
            set
            {
                SetPropertyValue("NuocCuTru", ref _NuocCuTru, value);
            }
        }

        [ModelDefault("Caption", "Năm di cư")]
        public int NamDiCu
        {
            get
            {
                return _NamDiCu;
            }
            set
            {
                SetPropertyValue("NamDiCu", ref _NamDiCu, value);
            }
        }

        [ModelDefault("Caption", "Nghề nghiệp")]
        public string NgheNghiepHienTai
        {
            get
            {
                return _NgheNghiepHienTai;
            }
            set
            {
                SetPropertyValue("NgheNghiepHienTai", ref _NgheNghiepHienTai, value);
            }
        }

        [ModelDefault("Caption", "Nơi làm việc")]
        public string NoiLamViec
        {
            get
            {
                return _NoiLamViec;
            }
            set
            {
                SetPropertyValue("NoiLamViec", ref _NoiLamViec, value);
            }
        }

        [ModelDefault("Caption", "Tình trạng")]
        public TinhTrangEnum TinhTrang
        {
            get
            {
                return _TinhTrang;
            }
            set
            {
                SetPropertyValue("TinhTrang", ref _TinhTrang, value);
            }
        }

        [ModelDefault("Caption", "Điện thoại di động")]
        public string DienThoaiDiDong
        {
            get
            {
                return _DienThoaiDiDong;
            }
            set
            {
                SetPropertyValue("DienThoaiDiDong", ref _DienThoaiDiDong, value);
            }
        }

        public QuanHeGiaDinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            PhanLoai = LoaiQuocGiaEnum.TrongNuoc;
        }
    }
}
