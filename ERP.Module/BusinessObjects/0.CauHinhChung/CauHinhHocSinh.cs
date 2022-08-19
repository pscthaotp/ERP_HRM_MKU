using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình học sinh")]
    public class CauHinhHocSinh : BaseObject
    {
        // Fields...
        private int _SoBatDauMaQuanLy;
        private string _MauMaQuanLy;
        private bool _TuDongTaoMaQuanLy;
        private bool _KhongHienHocSinhKhiChonTruong;
        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo mã Học sinh")]
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

        [ModelDefault("Caption", "Số bắt đầu mã Học sinh")]
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

        [ModelDefault("Caption", "Mẫu mã Học sinh")]
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
        [ModelDefault("Caption", "Không hiện học sinh khi chọn Trường")]
        public bool KhongHienHocSinhKhiChonTruong
        {
            get
            {
                return _KhongHienHocSinhKhiChonTruong;
            }
            set
            {
                SetPropertyValue("KhongHienHocSinhKhiChonTruong", ref _KhongHienHocSinhKhiChonTruong, value);
            }
        }
        public CauHinhHocSinh(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            TuDongTaoMaQuanLy = true;
            SoBatDauMaQuanLy = 1;
            MauMaQuanLy = "ABI-NienKhoa-{000#}";
        }
    }

}
