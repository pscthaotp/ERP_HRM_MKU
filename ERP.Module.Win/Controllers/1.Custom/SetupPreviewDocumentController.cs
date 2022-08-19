using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System.IO;
using System.Windows;
using ERP.Module.Win.NormalForm.System;
using ERP.Module.Commons;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class SetupPreviewDocumentController : ViewController
    {
        public SetupPreviewDocumentController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void SetupPreviewDocumentController_Activated(object sender, EventArgs e)
        {
            if (View.ObjectTypeInfo != null)
            {
                string type = View.ObjectTypeInfo.FullName;
                if (!string.IsNullOrEmpty(type))
                {
                    int index = type.LastIndexOf('.') + 1;
                    type = type.Substring(index);
                    //
                    FileInfo file = new FileInfo(String.Format(@"{0}\Help\{1}.mht", Config.StartupPath, type));
                    simpleAction1.Active["TruyCap"] = file.Exists;
                }
                else
                    simpleAction1.Active["TruyCap"] = false;
            }
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            frmHelper help = new frmHelper();
            help.XuLy(View.ObjectTypeInfo.Type, View.Caption);
        }
    }
}
