using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý bảo lưu(PMS)")]
    public class QuanLyBaoLuu : BaseObject
    {
        private CongTy _CongTy;
        private NamHoc _NamHoc;

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


        [Aggregated]
        [Association("QuanLyBaoLuu-ListChiTietBaoLuu")]
        [ModelDefault("Caption", "Chi tiết bảo lưu")]
        public XPCollection<ChiTietBaoLuu_PMS> ListChiTietBaoLuu
        {
            get
            {
                return GetCollection<ChiTietBaoLuu_PMS>("ListChiTietBaoLuu");
            }
        }
        public QuanLyBaoLuu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
    }
}
