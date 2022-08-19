using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.CauHinhChungs
{
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình hợp đồng")]
    [Appearance("CauHinhHopDong.LaoDong", TargetItems = "SoBatDauHopDongLamViec;MauSoHopDongLamViec", Enabled = false, Criteria = "!TuDongTaoSoHopDongLamViec")]
    [Appearance("CauHinhHopDong.ThuViec", TargetItems = "SoBatDauHopDongThuViec;MauSoHopDongThuViec", Enabled = false, Criteria = "!TuDongTaoSoHopDongThuViec")]
    [Appearance("CauHinhHopDong.PhuLuc", TargetItems = "SoBatDauPhuLucHopDong;MauSoPhuLucHopDong", Enabled = false, Criteria = "!TuDongTaoSoPhuLucHopDong")]
    [Appearance("CauHinhHopDong.Khoan", TargetItems = "SoBatDauHopDongKhoan;MauSoHopDongKhoan", Enabled = false, Criteria = "!TuDongTaoSoHopDongKhoan")]
    [Appearance("CauHinhHopDong.ThinhGiang", TargetItems = "SoBatDauHopDongTG;MauSoHopDongTG", Enabled = false, Criteria = "!TuDongTaoSoHopDongTG")]
    public class CauHinhHopDong : BaseObject
    {
        //
        private int _SoBatDauHopDongLamViec;
        private string _MauSoHopDongLamViec;
        private bool _TuDongTaoSoHopDongLamViec = true;
        //
        private int _SoBatDauHopDongThuViec;
        private string _MauSoHopDongThuViec;
        private bool _TuDongTaoSoHopDongThuViec = true;
        //
        private int _SoBatDauPhuLucHopDong;
        private string _MauSoPhuLucHopDong;
        private bool _TuDongTaoSoPhuLucHopDong = true;
        //
        private int _SoBatDauHopDongKhoan;
        private string _MauSoHopDongKhoan;
        private bool _TuDongTaoSoHopDongKhoan = true;
        //
        private int _SoBatDauHopDongTG;
        private string _MauSoHopDongTG;
        private bool _TuDongTaoSoHopDongTG = true;

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số HĐ lao động")]
        public bool TuDongTaoSoHopDongLamViec
        {
            get
            {
                return _TuDongTaoSoHopDongLamViec;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHopDongLamViec", ref _TuDongTaoSoHopDongLamViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Số bắt đầu HĐ lao động")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongLamViec")]
        public int SoBatDauHopDongLamViec
        {
            get
            {
                return _SoBatDauHopDongLamViec;
            }
            set
            {
                SetPropertyValue("SoBatDauHopDongLamViec", ref _SoBatDauHopDongLamViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Mẫu số HĐ lao động")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongLamViec")]
        public string MauSoHopDongLamViec
        {
            get
            {
                return _MauSoHopDongLamViec;
            }
            set
            {
                SetPropertyValue("MauSoHopDongLamViec", ref _MauSoHopDongLamViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số PLHĐ lao động")]
        public bool TuDongTaoSoPhuLucHopDong
        {
            get
            {
                return _TuDongTaoSoPhuLucHopDong;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoPhuLucHopDong", ref _TuDongTaoSoPhuLucHopDong, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu PLHĐ lao động")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoPhuLucHopDong")]
        public int SoBatDauPhuLucHopDong
        {
            get
            {
                return _SoBatDauPhuLucHopDong;
            }
            set
            {
                SetPropertyValue("SoBatDauPhuLucHopDong", ref _SoBatDauPhuLucHopDong, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số PLHĐ lao động")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoPhuLucHopDong")]
        public string MauSoPhuLucHopDong
        {
            get
            {
                return _MauSoPhuLucHopDong;
            }
            set
            {
                SetPropertyValue("MauSoPhuLucHopDong", ref _MauSoPhuLucHopDong, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số HĐ thử việc")]
        public bool TuDongTaoSoHopDongThuViec
        {
            get
            {
                return _TuDongTaoSoHopDongThuViec;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHopDongThuViec", ref _TuDongTaoSoHopDongThuViec, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu HĐ thử việc")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongThuViec")]
        public int SoBatDauHopDongThuViec
        {
            get
            {
                return _SoBatDauHopDongThuViec;
            }
            set
            {
                SetPropertyValue("SoBatDauHopDongThuViec", ref _SoBatDauHopDongThuViec, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số HĐ thử việc")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongThuViec")]
        public string MauSoHopDongThuViec
        {
            get
            {
                return _MauSoHopDongThuViec;
            }
            set
            {
                SetPropertyValue("MauSoHopDongThuViec", ref _MauSoHopDongThuViec, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số HĐ khoán")]
        public bool TuDongTaoSoHopDongKhoan
        {
            get
            {
                return _TuDongTaoSoHopDongKhoan;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHopDongKhoan", ref _TuDongTaoSoHopDongKhoan, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu HĐ khoán")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongKhoan")]
        public int SoBatDauHopDongKhoan
        {
            get
            {
                return _SoBatDauHopDongKhoan;
            }
            set
            {
                SetPropertyValue("SoBatDauHopDongKhoan", ref _SoBatDauHopDongKhoan, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số HĐ khoán")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongKhoan")]
        public string MauSoHopDongKhoan
        {
            get
            {
                return _MauSoHopDongKhoan;
            }
            set
            {
                SetPropertyValue("MauSoHopDongKhoan", ref _MauSoHopDongKhoan, value);
            }
        }


        [ImmediatePostData]
        [ModelDefault("Caption", "Tự động tạo số HĐTG")]
        public bool TuDongTaoSoHopDongTG
        {
            get
            {
                return _TuDongTaoSoHopDongTG;
            }
            set
            {
                SetPropertyValue("TuDongTaoSoHopDongTG", ref _TuDongTaoSoHopDongTG, value);
            }
        }

        [ModelDefault("Caption", "Số bắt đầu HĐTG")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongTG")]
        public int SoBatDauHopDongTG
        {
            get
            {
                return _SoBatDauHopDongTG;
            }
            set
            {
                SetPropertyValue("SoBatDauHopDongTG", ref _SoBatDauHopDongTG, value);
            }
        }

        [ModelDefault("Caption", "Mẫu số HĐTG")]
        [RuleRequiredField(DefaultContexts.Save, TargetCriteria = "TuDongTaoSoHopDongTG")]
        public string MauSoHopDongTG
        {
            get
            {
                return _MauSoHopDongTG;
            }
            set
            {
                SetPropertyValue("MauSoHopDongTG", ref _MauSoHopDongTG, value);
            }
        }

        public CauHinhHopDong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            SoBatDauHopDongLamViec = 1;
            MauSoHopDongLamViec = "{00#}/HĐLV";
            //
            SoBatDauHopDongKhoan = 1;
            MauSoHopDongKhoan = "{00#}/HĐK";
            //
            SoBatDauHopDongTG = 1;
            MauSoHopDongTG = "{00#}/HĐTG";
        }
    }

}
