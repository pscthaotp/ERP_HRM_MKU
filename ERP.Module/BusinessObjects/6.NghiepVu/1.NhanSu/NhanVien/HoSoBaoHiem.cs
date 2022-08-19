using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.Enum.NhanSu;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.QuaTrinh;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.NhanSu.NhanViens
{
    [DefaultClassOptions]
    [ImageName("BO_BaoHiem")]
    [DefaultProperty("SoSoBHXH")]
    [ModelDefault("Caption", "Hồ sơ bảo hiểm")]
    public class HoSoBaoHiem : BaseObject, IBoPhan, ICongTy
    {
        //
        private TrangThaiThamGiaBaoHiemEnum _TrangThai;
        private bool _KhongThamGiaBHTN;
        private QuyenLoiHuongBHYT _QuyenLoiHuongBHYT;
        private BenhVien _NoiDangKyKhamChuaBenh;
        private DateTime _TuNgay;
        private DateTime _DenNgay;
        private string _SoTheBHYT;
        private DateTime _NgayThamGiaBHXH;
        private string _SoSoBHXH;
        private ThongTinNhanVien _ThongTinNhanVien;
        private BoPhan _BoPhan;
        private CongTy _CongTy;

        [Browsable(false)]
        [ModelDefault("Caption", "Đơn vị")]
        [RuleRequiredField("", DefaultContexts.Save)]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
                if (!IsLoading)
                {
                    UpdateNhanVienList();
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thông tin Trường")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
        [Association("ThongTinNhanVien-ListHoSoBaoHiem")]
        public ThongTinNhanVien ThongTinNhanVien
        {
            get
            {
                return _ThongTinNhanVien;
            }
            set
            {
                SetPropertyValue("ThongTinNhanVien", ref _ThongTinNhanVien, value);
                if (!IsLoading && value != null)
                {
                    if (BoPhan == null || value.BoPhan.Oid != BoPhan.Oid)
                    {
                        BoPhan = value.BoPhan;                       
                    }
                    CongTy = value.CongTy;
                }
            }
        }

        [ModelDefault("Caption", "Số sổ BHXH")]
        public string SoSoBHXH
        {
            get
            {
                return _SoSoBHXH;
            }
            set
            {
                SetPropertyValue("SoSoBHXH", ref _SoSoBHXH, value);
            }
        }

        [ModelDefault("Caption", "Ngày tham gia BHXH")]
        public DateTime NgayThamGiaBHXH
        {
            get
            {
                return _NgayThamGiaBHXH;
            }
            set
            {
                SetPropertyValue("NgayThamGiaBHXH", ref _NgayThamGiaBHXH, value);
            }
        }

        [ModelDefault("Caption", "Số thẻ BHYT")]
        public string SoTheBHYT
        {
            get
            {
                return _SoTheBHYT;
            }
            set
            {
                SetPropertyValue("SoTheBHYT", ref _SoTheBHYT, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Từ ngày")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Đến ngày")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [ModelDefault("Caption", "Nơi đăng ký khám chữa bệnh")]
        public BenhVien NoiDangKyKhamChuaBenh
        {
            get
            {
                return _NoiDangKyKhamChuaBenh;
            }
            set
            {
                SetPropertyValue("NoiDangKyKhamChuaBenh", ref _NoiDangKyKhamChuaBenh, value);
            }
        }

        [ModelDefault("Caption", "Quyền lợi hưởng BHYT")]
        public QuyenLoiHuongBHYT QuyenLoiHuongBHYT
        {
            get
            {
                return _QuyenLoiHuongBHYT;
            }
            set
            {
                SetPropertyValue("QuyenLoiHuongBHYT", ref _QuyenLoiHuongBHYT, value);
            }
        }

        [ModelDefault("Caption", "Không tham gia BHTN")]
        public bool KhongThamGiaBHTN
        {
            get
            {
                return _KhongThamGiaBHTN;
            }
            set
            {
                SetPropertyValue("KhongThamGiaBHTN", ref _KhongThamGiaBHTN, value);
            }
        }

        [ModelDefault("Caption", "Trạng thái")]
        public TrangThaiThamGiaBaoHiemEnum TrangThai
        {
            get
            {
                return _TrangThai;
            }
            set
            {
                SetPropertyValue("TrangThai", ref _TrangThai, value);
            }
        }
        public HoSoBaoHiem(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<ThongTinNhanVien> NVList { get; set; }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            CongTy = Common.CongTy(Session);
        }

        /// <summary>
        /// Cập nhật danh sách nhân viên
        /// </summary>
        private void UpdateNhanVienList()
        {
            //
            if (NVList == null)
                NVList = new XPCollection<ThongTinNhanVien>(Session);
            //
            if (BoPhan == null)
                NVList.Criteria = new InOperator("Oid", Common.NhanVien_DanhSachNhanVienDuocPhanQuyen());
            else
                NVList.Criteria = Common.Criteria_NhanVien_DanhSachNhanVienTheoBoPhan(BoPhan);
        }     
    }

}
