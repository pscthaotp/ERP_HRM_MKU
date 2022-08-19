using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
//using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.BusinessObjects._6.NghiepVu.NghiPhep
{
    [DefaultClassOptions]
    [DefaultProperty("ThongTinNhanVien")]
    [ModelDefault("Caption", "Chi tiết nghỉ phép")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    public class CC_ChiTietNghiPhep : BaseObject, IBoPhan
    {
        private CC_QuanLyNghiPhep _QuanLyNghiPhep;
        private BoPhan _BoPhan;
        private ThongTinNhanVien _ThongTinNhanVien;
        private decimal _TongSoNgayPhep;
        private decimal _SoNgayPhepDaNghi;
        private decimal _SoNgayPhepConLai;
        private decimal _SoNgayPhepCongThem;
        private decimal _SoNgayPhepNamHienTai;
        private decimal _SoNgayPhepNamTruoc;
        private decimal _SoNgayTamUngNamTruoc;
        private decimal _SoNgayTamUngHienTai;
        private decimal _SoNgayPhepDaNghi_QuiI;
        private decimal _SoNgayPhepNamTruoc_BK;
        private decimal _TongSoNgayBu;
        private decimal _SoNgayBuDaNghi;
        private decimal _SoNgayBuConLai;
        private decimal _SoNgayBuNamHienTai;
        private decimal _SoNgayBuNamTruoc;
        private decimal _SoNgayBuNamTruoc_BK;
        //private bool _Check_ChotPhepTon;
        //private bool _Check_ClearPhep;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quản lý nghỉ phép")]
        [Association("QuanLyNghiPhep-ListChiTietNghiPhep")]
        public CC_QuanLyNghiPhep QuanLyNghiPhep
        {
            get
            {
                return _QuanLyNghiPhep;
            }
            set
            {
                SetPropertyValue("QuanLyNghiPhep", ref _QuanLyNghiPhep, value);
            }
        }

        [ModelDefault("Caption", "Đơn vị")]
        public BoPhan BoPhan
        {
            get
            {
                return _BoPhan;
            }
            set
            {
                SetPropertyValue("BoPhan", ref _BoPhan, value);
            }
        }

        [ModelDefault("Caption", "Cán bộ")]
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

        //Phân bổ đều 12 tháng (cộng tăng thêm)
        [ImmediatePostData]
        //[ModelDefault("Caption", "Tổng số ngày nghỉ phép")]
        [ModelDefault("Caption", "Số phép hiện tại")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongSoNgayPhep
        {
            get
            {
                return _TongSoNgayPhep;
            }
            set
            {
                SetPropertyValue("TongSoNgayPhep", ref _TongSoNgayPhep, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày đã nghỉ")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayPhepDaNghi
        {
            get
            {
                return _SoNgayPhepDaNghi;
            }
            set
            {
                SetPropertyValue("SoNgayPhepDaNghi", ref _SoNgayPhepDaNghi, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày còn lại")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayPhepConLai
        {
            get
            {
                return _SoNgayPhepConLai;
            }
            set
            {
                SetPropertyValue("SoNgayPhepConLai", ref _SoNgayPhepConLai, value);
            }
        }

        [ImmediatePostData]
        //[ModelDefault("Caption", "Số ngày phép cộng thêm")]
        [ModelDefault("Caption", "Phép thâm niên")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayPhepCongThem
        {
            get
            {
                return _SoNgayPhepCongThem;
            }
            set
            {
                SetPropertyValue("SoNgayPhepCongThem", ref _SoNgayPhepCongThem, value);
            }
        }

        [ImmediatePostData]
        //[ModelDefault("Caption", "Số ngày phép năm hiện tại")]
        [ModelDefault("Caption", "Phép năm quy định")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayPhepNamHienTai
        {
            get
            {
                return _SoNgayPhepNamHienTai;
            }
            set
            {
                SetPropertyValue("SoNgayPhepNamHienTai", ref _SoNgayPhepNamHienTai, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày phép năm trước")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayPhepNamTruoc
        {
            get
            {
                return _SoNgayPhepNamTruoc;
            }
            set
            {
                SetPropertyValue("SoNgayPhepNamTruoc", ref _SoNgayPhepNamTruoc, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày phép nghỉ bù Quý 1")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayPhepDaNghi_QuiI
        {
            get
            {
                return _SoNgayPhepDaNghi_QuiI;
            }
            set
            {
                SetPropertyValue("SoNgayPhepDaNghi_QuiI", ref _SoNgayPhepDaNghi_QuiI, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày phép tạm ứng năm trước")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayTamUngNamTruoc
        {
            get
            {
                return _SoNgayTamUngNamTruoc;
            }
            set
            {
                SetPropertyValue("SoNgayTamUngNamTruoc", ref _SoNgayTamUngNamTruoc, value);
            }
        }

        [ImmediatePostData]
        //[ModelDefault("Caption", "Số ngày phép tạm ứng năm hiện tại")]
        [ModelDefault("Caption", "Số ngày ứng")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayTamUngHienTai
        {
            get
            {
                return _SoNgayTamUngHienTai;
            }
            set
            {
                SetPropertyValue("SoNgayTamUngHienTai", ref _SoNgayTamUngHienTai, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày phép năm trước_BK")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayPhepNamTruoc_BK
        {
            get
            {
                return _SoNgayPhepNamTruoc_BK;
            }
            set
            {
                SetPropertyValue("SoNgayPhepNamTruoc_BK", ref _SoNgayPhepNamTruoc_BK, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Tổng số ngày nghỉ bù")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal TongSoNgayBu
        {
            get
            {
                return _TongSoNgayBu;
            }
            set
            {
                SetPropertyValue("TongSoNgayBu", ref _TongSoNgayBu, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày bù đã nghỉ")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayBuDaNghi
        {
            get
            {
                return _SoNgayBuDaNghi;
            }
            set
            {
                SetPropertyValue("SoNgayBuDaNghi", ref _SoNgayBuDaNghi, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày bù còn lại")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayBuConLai
        {
            get
            {
                return _SoNgayBuConLai;
            }
            set
            {
                SetPropertyValue("SoNgayBuConLai", ref _SoNgayBuConLai, value);
            }
        }

        [ImmediatePostData]
        //[ModelDefault("Caption", "Số ngày bù của năm hiện tại")]
        [ModelDefault("Caption", "Số ngày bù")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayBuNamHienTai
        {
            get
            {
                return _SoNgayBuNamHienTai;
            }
            set
            {
                SetPropertyValue("SoNgayBuNamHienTai", ref _SoNgayBuNamHienTai, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày bù của năm trước")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayBuNamTruoc
        {
            get
            {
                return _SoNgayBuNamTruoc;
            }
            set
            {
                SetPropertyValue("SoNgayBuNamTruoc", ref _SoNgayBuNamTruoc, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Số ngày bù của năm trước_BK")]
        [ModelDefault("DisplayFormat", "N1")]
        [ModelDefault("EditMask", "N1")]
        public decimal SoNgayBuNamTruoc_BK
        {
            get
            {
                return _SoNgayBuNamTruoc_BK;
            }
            set
            {
                SetPropertyValue("SoNgayBuNamTruoc_BK", ref _SoNgayBuNamTruoc_BK, value);
            }
        }

        //[ModelDefault("Caption", "Kiểm tra phép tồn")]
        //[Browsable(false)]
        //public bool Check_ChotPhepTon
        //{
        //    get
        //    {
        //        return _Check_ChotPhepTon;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Check_ChotPhepTon", ref _Check_ChotPhepTon, value);
        //    }
        //}

        //[ModelDefault("Caption", "Kiểm tra reset phép")]
        //[Browsable(false)]
        //public bool Check_ClearPhep
        //{
        //    get
        //    {
        //        return _Check_ClearPhep;
        //    }
        //    set
        //    {
        //        SetPropertyValue("Check_ClearPhep", ref _Check_ClearPhep, value);
        //    }
        //}

        public CC_ChiTietNghiPhep(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }
    }
}