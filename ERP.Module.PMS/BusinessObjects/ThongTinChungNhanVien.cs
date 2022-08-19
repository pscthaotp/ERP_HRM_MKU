using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System.ComponentModel;

namespace ERP.Module.PMS
{
    [ModelDefault ("Caption", "Thông tin chung (Nhân viên)")]
    public class ThongTinChungNhanVien : BaseObject
    {
     #region KB Thông tin nhân viên
        private CongTy _CongTy;
        private BoPhan _BoPhan;
        private NhanVien _NhanVien;

        private ChucVu _ChucVu;
        private ChucDanh _ChucDanh;
        private HocHam _HocHam;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;//Học vị
        #endregion

        #region Thông tin nhân viên
        [ModelDefault("Caption", "Trường")]
        //[Browsable(false)]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { SetPropertyValue("CongTy", ref _CongTy, value); }
        }
        [ModelDefault("Caption", "Đơn vị")]
        //[Browsable(false)]
        public BoPhan BoPhan
        {
            get { return _BoPhan; }
            set { SetPropertyValue("BoPhan", ref _BoPhan, value); }
        }
        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }
        [ModelDefault("Caption", "Chức vụ")]
        [VisibleInListView(false)]
        public ChucVu ChucVu
        {
            get { return _ChucVu; }
            set { SetPropertyValue("ChucVu", ref _ChucVu, value); }
        }
        [ModelDefault("Caption", "Chức danh")]
        [VisibleInListView(false)]
        public ChucDanh ChucDanh
        {
            get { return _ChucDanh; }
            set { SetPropertyValue("ChucDanh", ref _ChucDanh, value); }
        }

        [ModelDefault("Caption", "Học hàm")]
        [VisibleInListView(false)]
        public HocHam HocHam
        {
            get { return _HocHam; }
            set { SetPropertyValue("HocHam", ref _HocHam, value); }
        }

        [ModelDefault("Caption", "Trình độ chuyên môn")]
        [VisibleInListView(false)]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }
        #endregion
        public ThongTinChungNhanVien(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }

}