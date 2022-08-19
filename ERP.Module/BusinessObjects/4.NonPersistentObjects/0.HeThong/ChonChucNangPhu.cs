using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.Enum.Systems;
using ERP.Module.HeThong;
using DevExpress.Data.Filtering;

namespace ERP.Module.NonPersistentObjects.HeThong
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Chọn dữ liệu")]
    public class ChonChucNangPhu : BaseObject
    {
        //
        [ModelDefault("Caption", "Danh sách chức năng")]
        public XPCollection<ChucNangPhuItem> ChucNangList { get; set; }

        public ChonChucNangPhu(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //
            ChucNangList = new XPCollection<ChucNangPhuItem>(Session,false);
            //
            CriteriaOperator filter = CriteriaOperator.Parse("LoaiObject=1");
            XPCollection<AppMenu> appList = new XPCollection<AppMenu>(Session, filter);
            //
            foreach (var item in appList)
            {
                ChucNangList.Add(new ChucNangPhuItem(Session) { AppMenu = item,PhanHe = item.PhanHe.TenPhanHe });
            }
        }
    }

}
