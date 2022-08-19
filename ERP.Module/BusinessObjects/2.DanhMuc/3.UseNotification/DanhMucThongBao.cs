using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using System.ComponentModel;

namespace ERP.Module.DanhMuc.UseNotification
{
    [ModelDefault("Caption", "Danh mục thông báo")]
    [DefaultProperty("TenDanhMuc")]
    public class DanhMucThongBao : BaseObject
    {
        private string _TenDanhMuc;
        public string TenDanhMuc
        {
            get { return _TenDanhMuc; }
            set { SetPropertyValue("TenDanhMuc", ref _TenDanhMuc, value); }
        }
        public DanhMucThongBao(Session session)
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