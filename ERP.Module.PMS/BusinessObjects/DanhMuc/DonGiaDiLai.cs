using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ERP.Module.PMS.DanhMuc
{
    [ModelDefault("Caption", "Đơn giá giá đi lại")]
    [DefaultProperty("Caption")]
    public class DonGiaDiLai : BaseObject
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;
        private NganhHoc _NganhHoc;
        private TrinhDoChuyenMon _TrinhDoChuyenMon;       
        private int _TuKhoan;
        private int _DenKhoan;
        //private decimal _HeSo; Hiện tại không sử dụng _ ThuHuong;
        private decimal _DonGia;
        private bool _MacDinh;

        //
        [ModelDefault("Caption", "Trường")]
        [RuleRequiredField(DefaultContexts.Save)]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { SetPropertyValue("CongTy", ref _CongTy, value); }
        }
        [ModelDefault("Caption", "Năm học")]
        [RuleRequiredField(DefaultContexts.Save)]
        public NamHoc NamHoc
        {
            get { return _NamHoc; }
            set { SetPropertyValue("NamHoc", ref _NamHoc, value); }
        }

        [ModelDefault("Caption", "Chuyên ngành")]
        public NganhHoc NganhHoc
        {
            get { return _NganhHoc; }
            set { SetPropertyValue("NganhHoc", ref _NganhHoc, value); }
        }

        [ModelDefault("Caption", "Học vị")]
        public TrinhDoChuyenMon TrinhDoChuyenMon
        {
            get { return _TrinhDoChuyenMon; }
            set { SetPropertyValue("TrinhDoChuyenMon", ref _TrinhDoChuyenMon, value); }
        }
        //
        [ModelDefault("Caption", "Từ khoản")]
        [ModelDefault("DisplayFormat", "N")]
        [ModelDefault("EditMask", "N")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int TuKhoan
        {
            get { return _TuKhoan; }
            set { SetPropertyValue("TuKhoan", ref _TuKhoan, value); }
        }
        [ModelDefault("Caption", "Đến khoản")]
        [ModelDefault("DisplayFormat", "N")]
        [ModelDefault("EditMask", "N")]
        [RuleRequiredField(DefaultContexts.Save)]
        public int DenKhoan
        {
            get { return _DenKhoan; }
            set { SetPropertyValue("DenKhoan", ref _DenKhoan, value); }
        }

        [Browsable(false)]
        public string Caption
        {
            get
            {
                return String.Format(DonGia.ToString());
            }
        }
        //[ModelDefault("Caption", "Hệ số")]
        //[ModelDefault("DisplayFormat", "N2")]
        //[ModelDefault("EditMask", "N2")]
        //[RuleRange("DonGiaDiLai", DefaultContexts.Save, 0.00, 10000, "Hệ số > 0")]
        //public decimal HeSo
        //{
        //    get { return _HeSo; }
        //    set { SetPropertyValue("HeSo", ref _HeSo, value); }
        //}
        [ModelDefault("Caption", "Đơn giá")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal DonGia
        {
            get { return _DonGia; }
            set { SetPropertyValue("DonGia", ref _DonGia, value); }
        }

        [ModelDefault("Caption", "Mặc định")]
        public bool MacDinh
        {
            get { return _MacDinh; }
            set { SetPropertyValue("MacDinh", ref _MacDinh, value); }
        }

        public DonGiaDiLai(Session session)
            : base(session)
        { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            MacDinh = true;
        }
    }
}
