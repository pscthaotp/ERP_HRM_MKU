using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using ERP.Module.NonPersistentObjects.TKB;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.ExpressApp.Win.Editors;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using DevExpress.XtraGrid;
using ERP.Module.DanhMuc.TKB;
using ERP.Module.NghiepVu.TKB.ChuongTrinhGiaoDuc;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraEditors.Repository;
using ERP.Module.Commons;
using DevExpress.XtraEditors.Controls;
using ERP.Module.NghiepVu.TKB;
using ERP.Module.NonPersistentObjects.NhanSu;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.BoPhans
{
    public partial class BoPhan_CustomThemBoPhanController : ViewController<DetailView>
    {

        public BoPhan_CustomThemBoPhanController()
        {
            InitializeComponent();
            TargetObjectType = typeof(BoPhan_ChonLoaiBoPhan);
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            
            //Kiểm tra view hiện tại có được edit hay không
            if (((DetailView)View).ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit
                  && View.Id == "BoPhan_ChonLoaiBoPhan_DetailView")
            {
                //Tìm trong view những cột nào kiểu là enum và duyệt qua nếu k có quyền thì remove giá trị enum đó
                foreach (EnumPropertyEditor propertyEditor in ((DetailView)View).GetItems<EnumPropertyEditor>())
                {
                    if (((PropertyEditor)propertyEditor).Id == "LoaiBoPhan")
                    {
                        int stt = 0;
                        while (stt < propertyEditor.Control.Properties.Items.Count)
                        {
                            var item = propertyEditor.Control.Properties.Items[stt];
                            if (((ImageComboBoxItem)item).Value.ToString() == "CongTy")
                            {
                                propertyEditor.Control.Properties.Items.Remove(item);
                            }
                            else
                                stt++;
                        }

                    }
                }
            }
        }
    }
}
