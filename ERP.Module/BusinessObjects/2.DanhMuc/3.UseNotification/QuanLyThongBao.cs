using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.ComponentModel;
using ERP.Module.Enum.ThongBao;
using DevExpress.Persistent.Validation;

namespace ERP.Module.DanhMuc.UseNotification
{
    [ModelDefault("Caption", "Quản lý thông báo")]
    [DefaultProperty("DanhMucThongBao")]

    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "CongTy;DanhMucThongBao", "Đã tồn tại.")]
    public class QuanLyThongBao : BaseObject, ICongTy
    {
        private DanhMucThongBaoEnum _DanhMucThongBao;
        private CongTy _CongTy;
        private string _GhiChu;

        [ModelDefault("Caption", "Thông báo")]
        public DanhMucThongBaoEnum DanhMucThongBao
        {
            get { return _DanhMucThongBao; }
            set { SetPropertyValue("DanhMucThongBao", ref _DanhMucThongBao, value); }
        }
        [ModelDefault("Caption", "Trường")]
        public CongTy CongTy
        {
            get { return _CongTy; }
            set { SetPropertyValue("CongTy", ref _CongTy, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }

        [Aggregated]
        [ModelDefault("Caption", "Chi tiết")]
        [Association("QuanLyThongBao-ListChiTietUserThongBao")]
        public XPCollection<ChiTietUserThongBao> ListChiTietUserThongBao
        {
            get
            {
                return GetCollection<ChiTietUserThongBao>("ListChiTietUserThongBao");
            }
        }
        public QuanLyThongBao(Session session)
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