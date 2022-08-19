using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.SystemModule;

namespace ERP.Module.Win.Diagrams
{
    public partial class TruyCapQuyTrinhController : WindowController
    {
        private ShowNavigationItemController nav;

        public TruyCapQuyTrinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        
        protected override void OnFrameAssigned()
        {
            base.OnFrameAssigned();
            nav = Frame.GetController<ShowNavigationItemController>();
            if (nav != null)
            {
                nav.CustomShowNavigationItem += nav_CustomShowNavigationItem;
            }
        }

        protected override void OnDeactivated()
        {
            if (nav != null)
                nav.CustomShowNavigationItem -= nav_CustomShowNavigationItem;

            base.OnDeactivated();
        }

        void nav_CustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhNghiViec_DashboardView")
            {
                QuyTrinhFactory.Type = LoaiQuyTrinhEnum.QuyTrinhNghiViec;
            }
            if (e.ActionArguments.SelectedChoiceActionItem.Id == "QuyTrinhNangLuong_DashboardView")
            {
                QuyTrinhFactory.Type = LoaiQuyTrinhEnum.QuyTrinhNangLuong;
            }           
        }
    }
}
