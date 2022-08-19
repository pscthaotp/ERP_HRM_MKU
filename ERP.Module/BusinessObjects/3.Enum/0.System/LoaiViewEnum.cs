using System;

namespace ERP.Module.Enum.Systems
{
    public enum LoaiViewEnum : byte
    {
        [DevExpress.ExpressApp.DC.XafDisplayName("ListView")]
        ListView = 0,
        [DevExpress.ExpressApp.DC.XafDisplayName("DetailView")]
        DetailView = 1,
        [DevExpress.ExpressApp.DC.XafDisplayName("DashboardView")]
        DashboardView = 2,
        [DevExpress.ExpressApp.DC.XafDisplayName("CategorizedListEditor")]
        CustomCategorizedListEditor = 3,
        [DevExpress.ExpressApp.DC.XafDisplayName("ListViewCustom")]
        ListViewCustom = 4
    }
}
