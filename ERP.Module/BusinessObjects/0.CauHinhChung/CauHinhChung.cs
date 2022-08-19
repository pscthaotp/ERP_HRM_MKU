using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.ComponentModel;
using ERP.Module.Enum.NhanSu;
using ERP.Module.Enum.Systems;
using DevExpress.Persistent.Validation;
using ERP.Module.Commons;
using ERP.Module.HeThong;

namespace ERP.Module.CauHinhChungs
{
    [DefaultClassOptions]
    [ImageName("BO_TienIch")]
    [ModelDefault("Caption", "Cấu hình chung")]
    public class CauHinhChung : BaseObject, ICongTy
    {
        //
        private CongTy _CongTy;
        private LoaiPhanMenEnum _LoaiPhanMen;
        private CauHinhNhacViec _CauHinhNhacViec;
        private CauHinhHopDong _CauHinhHopDong;
        private CauHinhHoSo _CauHinhHoSo;
        private CauHinhQuyetDinh _CauHinhQuyetDinh;
        private CauHinhTuyenSinh _CauHinhTuyenSinh;
        private CauHinhSoHoa _CauHinhSoHoa;
        private CauHinhMail _CauHinhMail;
        private CauHinhXacThuc _CauHinhXacThuc;
        private CauHinhKho _CauHinhKho;
        private CauHinhTaiSan _CauHinhTaiSan;
        private CauHinhCongCu _CauHinhCongCu;
        private CauHinhHocSinh _CauHinhHocSinh; 
        //Moi Them
        private int _SoGioChuan;
        private int _SoGioChuan_NCHK;
        private int _SoGioChuan_Khac;
        //
        [Browsable(false)]
        [ImmediatePostData]
        [ModelDefault("Caption", "Loại phần mềm")]
        [RuleRequiredField(DefaultContexts.Save)]
        public LoaiPhanMenEnum LoaiPhanMen
        {
            get
            {
                return _LoaiPhanMen;
            }
            set
            {
                SetPropertyValue("LoaiPhanMen", ref _LoaiPhanMen, value);
            }
        }

        [VisibleInDetailView(false)]
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        [RuleUniqueValue(DefaultContexts.Save)]
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

        [ModelDefault("Caption", "Cấu hình nhắc việc")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhNhacViec CauHinhNhacViec
        {
            get
            {
                return _CauHinhNhacViec;
            }
            set
            {
                SetPropertyValue("CauHinhNhacViec", ref _CauHinhNhacViec, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình hồ sơ")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhHoSo CauHinhHoSo
        {
            get
            {
                return _CauHinhHoSo;
            }
            set
            {
                SetPropertyValue("CauHinhHoSo", ref _CauHinhHoSo, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình hợp đồng")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhHopDong CauHinhHopDong
        {
            get
            {
                return _CauHinhHopDong;
            }
            set
            {
                SetPropertyValue("CauHinhHopDong", ref _CauHinhHopDong, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình quyết định")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhQuyetDinh CauHinhQuyetDinh
        {
            get
            {
                return _CauHinhQuyetDinh;
            }
            set
            {
                SetPropertyValue("CauHinhQuyetDinh", ref _CauHinhQuyetDinh, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình tuyển sinh")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhTuyenSinh CauHinhTuyenSinh
        {
            get
            {
                return _CauHinhTuyenSinh;
            }
            set
            {
                SetPropertyValue("CauHinhTuyenSinh", ref _CauHinhTuyenSinh, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình số hóa")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhSoHoa CauHinhSoHoa
        {
            get
            {
                return _CauHinhSoHoa;
            }
            set
            {
                SetPropertyValue("CauHinhSoHoa", ref _CauHinhSoHoa, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình mail")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhMail CauHinhMail
        {
            get
            {
                return _CauHinhMail;
            }
            set
            {
                SetPropertyValue("CauHinhMail", ref _CauHinhMail, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình xác thực")]
        [ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        public CauHinhXacThuc CauHinhXacThuc
        {
            get
            {
                return _CauHinhXacThuc;
            }
            set
            {
                SetPropertyValue("CauHinhXacThuc", ref _CauHinhXacThuc, value);
            }
        }

        //[ModelDefault("Caption", "Cấu hình học phí")]
        //[ExpandObjectMembers(ExpandObjectMembers.InDetailView)]
        //public CauHinhHocPhi CauHinhHocPhi
        //{
        //    get
        //    {
        //        return _CauHinhHocPhi;
        //    }
        //    set
        //    {
        //        SetPropertyValue("CauHinhHocPhi", ref _CauHinhHocPhi, value);
        //    }
        //}

        [ModelDefault("Caption", "Cấu hình kho")]
        [ExpandObjectMembers(ExpandObjectMembers.InListView)]
        public CauHinhKho CauHinhKho
        {
            get
            {
                return _CauHinhKho;
            }
            set
            {
                SetPropertyValue("CauHinhKho", ref _CauHinhKho, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình tài sản")]
        [ExpandObjectMembers(ExpandObjectMembers.InListView)]
        public CauHinhTaiSan CauHinhTaiSan
        {
            get
            {
                return _CauHinhTaiSan;
            }
            set
            {
                SetPropertyValue("CauHinhTaiSan", ref _CauHinhTaiSan, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình công cụ")]
        [ExpandObjectMembers(ExpandObjectMembers.InListView)]
        public CauHinhCongCu CauHinhCongCu
        {
            get
            {
                return _CauHinhCongCu;
            }
            set
            {
                SetPropertyValue("CauHinhCongCu", ref _CauHinhCongCu, value);
            }
        }

        [ModelDefault("Caption", "Cấu hình học sinh")]
        [ExpandObjectMembers(ExpandObjectMembers.InListView)]
        public CauHinhHocSinh CauHinhHocSinh
        {
            get
            {
                return _CauHinhHocSinh;
            }
            set
            {
                SetPropertyValue("CauHinhHocSinh", ref _CauHinhHocSinh, value);
            }
        }

        [ModelDefault("Caption", "Số giờ chuẩn")]
        [RuleRange("CHC_SoGioChuan", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        public int SoGioChuan
        {
            get { return _SoGioChuan; }
            set { SetPropertyValue("SoGioChuan", ref _SoGioChuan, value); }
        }
        [ModelDefault("Caption", "Số giờ chuẩn (NCKH)")]
        [RuleRange("CHC_SoGioChuan_NCHK", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        public int SoGioChuan_NCHK
        {
            get { return _SoGioChuan_NCHK; }
            set { SetPropertyValue("SoGioChuan_NCHK", ref _SoGioChuan_NCHK, value); }
        }
        [ModelDefault("Caption", "Số giờ chuẩn(Khác)")]
        [RuleRange("CHC_SoGioChuan_Khac", DefaultContexts.Save, 0.00, 10000, "Số giờ chuẩn > 0")]
        [VisibleInListView(false)]
        public int SoGioChuan_Khac
        {
            get { return _SoGioChuan_Khac; }
            set { SetPropertyValue("SoGioChuan_Khac", ref _SoGioChuan_Khac, value); }
        }

        [Aggregated]
        [Association("CauHinhChung-ListCauHinhHocPhi")]
        [ModelDefault("Caption", "Cấu hình học phí")]
        public XPCollection<CauHinhHocPhi> ListCauHinhHocPhi
        {
            get
            {
                return GetCollection<CauHinhHocPhi>("ListCauHinhHocPhi");
            }
        }

        public CauHinhChung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            CongTy = Common.CongTy(Session);
            //
            KhoiTaoCauHinh();
            //
            if (Config.TypeApplication.Equals("WebForm"))
            {
                LoaiPhanMen = LoaiPhanMenEnum.Web;
            }
            else
            {
                LoaiPhanMen = LoaiPhanMenEnum.Win;
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            KhoiTaoCauHinh();
        }

        private void KhoiTaoCauHinh()
        {
            //
            if (CauHinhNhacViec == null)
            CauHinhNhacViec = new CauHinhNhacViec(Session);
            if (CauHinhHoSo==null)
            CauHinhHoSo = new CauHinhHoSo(Session);
            if (CauHinhHopDong == null)
            CauHinhHopDong = new CauHinhHopDong(Session);
            if (CauHinhQuyetDinh == null)
            CauHinhQuyetDinh = new CauHinhQuyetDinh(Session);
            if (CauHinhTuyenSinh == null)
                CauHinhTuyenSinh = new CauHinhTuyenSinh(Session);
            if (CauHinhSoHoa == null)
                CauHinhSoHoa = new CauHinhSoHoa(Session);
            if (CauHinhMail == null)
                CauHinhMail = new CauHinhMail(Session);
            if (CauHinhXacThuc == null)
                CauHinhXacThuc = new CauHinhXacThuc(Session);
            if (CauHinhKho == null)
                CauHinhKho = new CauHinhKho(Session);
            if (CauHinhCongCu == null)
                CauHinhCongCu = new CauHinhCongCu(Session);
            if (CauHinhTaiSan == null)
                CauHinhTaiSan = new CauHinhTaiSan(Session);
            if (CauHinhHocSinh == null)
                CauHinhHocSinh = new CauHinhHocSinh(Session);
        }
    }
}
