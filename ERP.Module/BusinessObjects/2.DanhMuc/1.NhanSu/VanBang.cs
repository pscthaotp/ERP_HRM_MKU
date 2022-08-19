using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.DanhMuc.NhanSu
{
    [ImageName("BO_VanBang")]
    [DefaultProperty("TrinhDoChuyenMon")]
    [ModelDefault("Caption", "Văn bằng")]
    public class VanBang : BaseObject
    {
        private XepLoaiChungChiEnum _XepLoai = XepLoaiChungChiEnum.KhongChon;
        private decimal _DiemTrungBinh;
        private HoSo _HoSo;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;
        private HinhThucDaoTao _HinhThucDaoTao;
        private ChuyenNganhDaoTao _ChuyenNganhDaoTao;
        private TruongDaoTao _TruongDaoTao;
        private int _NamTotNghiep;
        private DateTime _NgayCapBang;
        private string _GhiChu;
        private QuyetDinh _QuyetDinh;

        [Browsable(false)]
        [ImmediatePostData]
        [Association("HoSo-ListVanBang")]
        [ModelDefault("Caption", "Nhân viên trình độ")]
        public HoSo HoSo
        {
            get
            {
                return _HoSo;
            }
            set
            {
                SetPropertyValue("HoSo", ref _HoSo, value);
            }
        }

        [Browsable(false)]     
        [ModelDefault("Caption", "Quyết định")]
        public QuyetDinh QuyetDinh
        {
            get
            {
                return _QuyetDinh;
            }
            set
            {
                SetPropertyValue("QuyetDinh", ref _QuyetDinh, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Học vị")]
        [RuleRequiredField(DefaultContexts.Save)]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get
            {
                return _TrinhDoChuyenMon;
            }
            set
            {
                SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Chuyên ngành đào tạo")]
        public ChuyenNganhDaoTao ChuyenNganhDaoTao
        {
            get
            {
                return _ChuyenNganhDaoTao;
            }
            set
            {
                SetPropertyValue("ChuyenNganhDaoTao", ref _ChuyenNganhDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Trường đào tạo")]
        public TruongDaoTao TruongDaoTao
        {
            get
            {
                return _TruongDaoTao;
            }
            set
            {
                SetPropertyValue("TruongDaoTao", ref _TruongDaoTao, value);
            }
        }

        [ModelDefault("Caption", "Hình thức đào tạo")]
        public HinhThucDaoTao HinhThucDaoTao
        {
            get
            {
                return _HinhThucDaoTao;
            }
            set
            {
                SetPropertyValue("HinhThucDaoTao", ref _HinhThucDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Năm tốt nghiệp")]
        [ModelDefault("DisplayFormat", "####")]
        [ModelDefault("EditMask", "####")]
        public int NamTotNghiep
        {
            get
            {
                return _NamTotNghiep;
            }
            set
            {
                SetPropertyValue("NamTotNghiep", ref _NamTotNghiep, value);
            }
        }

        [ModelDefault("Caption", "Điểm trung bình")]
        [ModelDefault("EditMask", "N2")]
        [ModelDefault("DisplayFormat", "N2")]
        public decimal DiemTrungBinh
        {
            get
            {
                return _DiemTrungBinh;
            }
            set
            {
                SetPropertyValue("DiemTrungBinh", ref _DiemTrungBinh, value);
            }
        }

        [ModelDefault("Caption", "Xếp loại")]
        public XepLoaiChungChiEnum XepLoai
        {
            get
            {
                return _XepLoai;
            }
            set
            {
                SetPropertyValue("XepLoai", ref _XepLoai, value);
            }
        }      

        [ModelDefault("Caption", "Ngày cấp bằng")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        public DateTime NgayCapBang
        {
            get
            {
                return _NgayCapBang;
            }
            set
            {
                SetPropertyValue("NgayCapBang", ref _NgayCapBang, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }

        public VanBang(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        protected override void OnSaving()
        {
            base.OnSaving();            
        }

        protected override void OnDeleting()
        {
            VanBang vanBang = Session.FindObject<VanBang>(CriteriaOperator.Parse("Oid=?", this.Oid));
            if (vanBang != null)
            {
                NhanVien nhanVien = Session.FindObject<NhanVien>(CriteriaOperator.Parse("Oid=?", vanBang.HoSo.Oid));
                if (nhanVien != null)
                {
                    //Nếu văn bằng vừa xóa là văn bằng cao nhất thì cập nhật lại trình độ chuyên môn cao nhất
                    if (vanBang.TrinhDoChuyenMon != null && vanBang.TruongDaoTao != null && vanBang.ChuyenNganhDaoTao != null)
                    {

                        if (nhanVien.NhanVienTrinhDo != null
                             && nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon != null
                             && nhanVien.NhanVienTrinhDo.TruongDaoTao != null
                             && nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao != null
                             && nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon.Oid == vanBang.TrinhDoChuyenMon.Oid
                             && nhanVien.NhanVienTrinhDo.TruongDaoTao.Oid == vanBang.TruongDaoTao.Oid
                             && nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao.Oid == vanBang.ChuyenNganhDaoTao.Oid
                             && nhanVien.NhanVienTrinhDo.NamTotNghiep == vanBang.NamTotNghiep)
                        {
                            nhanVien.NhanVienTrinhDo.TrinhDoChuyenMon = null;
                            nhanVien.NhanVienTrinhDo.HinhThucDaoTao = null;
                            nhanVien.NhanVienTrinhDo.TruongDaoTao = null;
                            nhanVien.NhanVienTrinhDo.ChuyenNganhDaoTao = null;
                            nhanVien.NhanVienTrinhDo.NamTotNghiep = 0;
                            nhanVien.NhanVienTrinhDo.NgayCapBang = DateTime.MinValue;
                        }
                    }
                }
            }
            base.OnDeleting();
        }
    }

}
