using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Extends;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class ShowTemplateOnToolbarController : ViewController
    {
        private XPCollection<ExcelTemplate> excelTemplateList;
        private IObjectSpace _obs;

        public ShowTemplateOnToolbarController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ShowReportOnToolbarController_Activated(object sender, EventArgs e)
        {
            if (!(View is DashboardView))
            {
                Type type = View.ObjectTypeInfo.Type;
                _obs = Application.CreateObjectSpace();

                if (type != null)
                {
                    CriteriaOperator filter = CriteriaOperator.Parse("TargetTypeName=?", type.FullName);
                    SortProperty sort = new SortProperty("TenBieuMau", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    excelTemplateList = new XPCollection<ExcelTemplate>(((XPObjectSpace)_obs).Session, filter, sort);
                    //
                    if (excelTemplateList.Count > 0)
                        singleChoiceAction1.Active["ByMainForm"] = true;
                    else
                        singleChoiceAction1.Active["ByMainForm"] = false;
                }
                else
                {
                    singleChoiceAction1.Active["ByMainForm"] = false;
                }
            }
            else
            {
                singleChoiceAction1.Active["ByMainForm"] = false;
            }
        }

        private void ShowReportOnToolbarController_ViewControlsCreated(object sender, EventArgs e)
        {
            if (excelTemplateList != null && excelTemplateList.Count > 0)
            {
                ChoiceActionItem subItem;
                singleChoiceAction1.Items.Clear();
                foreach (ExcelTemplate item in excelTemplateList)
                {
                    subItem = new ChoiceActionItem();
                    subItem.Id = item.Oid.ToString();
                    subItem.Caption = item.TenBieuMau;
                    subItem.ImageName = "ChiTietLuong";
                    singleChoiceAction1.Items.Add(subItem);
                }
            }
        }

        private void singleChoiceAction1_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            foreach (ExcelTemplate item in excelTemplateList)
            {
                if (item.Oid.ToString() == e.SelectedChoiceActionItem.Id)
                {
                    _obs = Application.CreateObjectSpace();
                    //
                    using (SaveFileDialog dialog = new SaveFileDialog())
                    {
                        dialog.AddExtension = true;
                        dialog.Filter = "Excel|*.xls|All file|*.*";
                        if (item.File != null)
                        {
                            dialog.FileName = item.File.FileName;
                            if (dialog.ShowDialog() == DialogResult.OK)
                            {
                                FileStream stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                                item.File.SaveToStream(stream);
                                stream.Flush();
                                stream.Close();
                                stream.Dispose();
                                //
                                Process.Start(new ProcessStartInfo(dialog.FileName));
                            }
                        }
                        else
                        {
                            DialogUtil.ShowError("File biểu mẫu không tồn tại trong cơ sở dữ liệu.");
                        }
                    }

                }
            }
        }

       
    }
}
