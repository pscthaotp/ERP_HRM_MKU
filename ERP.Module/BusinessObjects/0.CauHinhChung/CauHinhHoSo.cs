using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.DanhMuc.NhanSu;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình hồ sơ")]
    public class CauHinhHoSo : BaseObject
    {
        // Fields...
        private bool _KhongHienNhanVienKhiChonCongTy;
        private int _SoBatDauMaQuanLy;
        private string _MauMaQuanLy;
        private bool _TuDongTaoMaQuanLy;
        private LoaiGioLamViec _LoaiGioLamViec;

        [ModelDefault("Caption", "Không hiện nhân viên khi chọn Trường")]
        public bool KhongHienNhanVienKhiChonCongTy
        {
            get
            {
                return _KhongHienNhanVienKhiChonCongTy;
            }
            set
            {
                SetPropertyValue("KhongHienNhanVienKhiChonCongTy", ref _KhongHienNhanVienKhiChonCongTy, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo mã nhân viên")]
        public bool TuDongTaoMaQuanLy
        {
            get
            {
                return _TuDongTaoMaQuanLy;
            }
            set
            {
                SetPropertyValue("TuDongTaoMaQuanLy", ref _TuDongTaoMaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu mã nhân viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaQuanLy")]
        public int SoBatDauMaQuanLy
        {
            get
            {
                return _SoBatDauMaQuanLy;
            }
            set
            {
                SetPropertyValue("SoBatDauMaQuanLy", ref _SoBatDauMaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Mẫu mã nhân viên")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoMaQuanLy")]
        public string MauMaQuanLy
        {
            get
            {
                return _MauMaQuanLy;
            }
            set
            {
                SetPropertyValue("MauMaQuanLy", ref _MauMaQuanLy, value);
            }
        }

        [ModelDefault("Caption", "Loại giờ làm việc mặc định")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiGioLamViec LoaiGioLamViec
        {
            get
            {
                return _LoaiGioLamViec;
            }
            set
            {
                SetPropertyValue("LoaiGioLamViec", ref _LoaiGioLamViec, value);
            }
        }

        public CauHinhHoSo(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            TuDongTaoMaQuanLy = true;
            SoBatDauMaQuanLy = 1;
            MauMaQuanLy = "{00#}";
        }
    }

}
