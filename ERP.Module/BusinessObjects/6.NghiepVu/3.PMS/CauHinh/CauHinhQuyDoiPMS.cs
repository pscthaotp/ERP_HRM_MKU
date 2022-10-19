using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Enum.PMS;
using ERP.Module.NghiepVu.PMS.DanhMuc;
using ERP.Module.Commons;

namespace ERP.Module.NghiepVu.PMS.CauHinh
{
    [DefaultClassOptions]
    [DefaultProperty("Caption")]
    [ModelDefault("Caption", "Cấu hình quy đổi PMS")]
    public class CauHinhQuyDoiPMS : BaseObject
    {
        private BoPhan _ThongTinTruong;
        private NamHoc _NamHoc;
        private HocKy _HocKy;
        private HeDaoTao _HeDaoTao;
        private LoaiGiangVienEnum? _LoaiGiangVien;
        private decimal _DonGiaVuotChuan;
        private decimal _DonGiaTrongChuan;
        private bool _NgungApDung;
        private string _CongThucTongHeSo;
        private string _CongThucTinhHeSoVuotGio;
        [ModelDefault("Caption", "Trường")]
        [VisibleInDetailView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        [ModelDefault("AllowEdit", "false")]
        public BoPhan ThongTinTruong
        {
            get { return _ThongTinTruong; }
            set { SetPropertyValue("ThongTinTruong", ref _ThongTinTruong, value); }
        }


        [ModelDefault("Caption", "Năm học")]
        [ImmediatePostData]
        //[VisibleInListView(false)]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Học kỳ")]
        [DataSourceProperty("NamHoc.ListHocKy")]
        //[RuleRequiredField(DefaultContexts.Save)]
        //[VisibleInListView(false)]
        public HocKy HocKy
        {
            get { return _HocKy; }
            set { SetPropertyValue("HocKy", ref _HocKy, value); }
        }
        [ModelDefault("Caption", "Ngừng áp dụng")]
        [ImmediatePostData]
        public bool NgungApDung
        {
            get { return _NgungApDung; }
            set { SetPropertyValue("NgungApDung", ref _NgungApDung, value); }
        }

        [ModelDefault("Caption", "Hệ đào tạo")]
        //[VisibleInListView(false)]
        public HeDaoTao HeDaoTao
        {
            get { return _HeDaoTao; }
            set { _HeDaoTao = value; }
        }

        [ModelDefault("Caption", "Loại giảng viên")]
        public LoaiGiangVienEnum? LoaiGiangVien
        {
            get { return _LoaiGiangVien; }
            set { _LoaiGiangVien = value; }
        }


        [ModelDefault("Caption", "Đơn giá (vượt chuẩn)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [VisibleInListView(false)]
        public decimal DonGiaVuotChuan
        {
            get { return _DonGiaVuotChuan; }
            set { SetPropertyValue("DonGiaVuotChuan", ref _DonGiaVuotChuan, value); }
        }

        [ModelDefault("Caption", "Đơn giá (trong chuẩn)")]
        [ModelDefault("DisplayFormat", "N0")]
        [ModelDefault("EditMask", "N0")]
        [VisibleInListView(false)]
        public decimal DonGiaTrongChuan
        {
            get { return _DonGiaTrongChuan; }
            set { SetPropertyValue("DonGiaTrongChuan", ref _DonGiaTrongChuan, value); }
        }


        private string ExpressionType
        {
            get
            {
                return "ERP.Module.NghiepVu.PMS.CauHinh.ChonGiaTriLapCongThucPMS";
            }
        }


        [ModelDefault("Caption", "Tính hệ số lương")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.btnEdit_CongThucPMS")]
        [VisibleInListView(false)]
        public string CongThucTongHeSo
        {
            get
            {
                return _CongThucTongHeSo;
            }
            set
            {
                SetPropertyValue("CongThucTongHeSo", ref _CongThucTongHeSo, value);
            }
        }


        [ModelDefault("Caption", "Tính vượt giờ")]
        //[RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.btnEdit_CongThucPMS")]
        [VisibleInListView(false)]
        public string CongThucTinhHeSoVuotGio
        {
            get
            {
                return _CongThucTinhHeSoVuotGio;
            }
            set
            {
                SetPropertyValue("CongThucTinhHeSoVuotGio", ref _CongThucTinhHeSoVuotGio, value);
            }
        }
        
        public CauHinhQuyDoiPMS(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThongTinTruong = Common.CongTy(Session);
        }

    }
}