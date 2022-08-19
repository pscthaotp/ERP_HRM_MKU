using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System.Windows.Forms;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Win.NormalForm.MailMerge;
using ERP.Module.MailMerge;
using ERP.Module.Extends;

namespace ERP.Module.Win.NormalForm.MailMerge
{
    public partial class frmMailMergeManager : XtraForm
    {
        public event EventHandler<TemplateEventArgs> _defaultTemplateChanged;
        private MailMergeTemplate _template;
        private string _templateName = string.Empty;

        public frmMailMergeManager(IObjectSpace obs, string templateName)
        {
            InitializeComponent();
            //
            unitOfWork1 = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer);
            if (string.IsNullOrEmpty(templateName))
                Close();
            //
            _templateName = templateName;
            // Lấy dữ liệu
            GetData();
        }

        void GetData()
        {
            //
            listTemplate.Session = unitOfWork1;
            listTemplate.Criteria = CriteriaOperator.Parse("MaQuanLy=?", _templateName);
            listTemplate.Reload();
            gridView1.RefreshData();
        }
        private void frmMailMergeManager_Load(object sender, EventArgs e)
        {
            //Cấu hình cơ bản
            GridUtil.InitGridView(gridView1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MailMergeTemplate template = gridView1.GetFocusedRow() as MailMergeTemplate;
            if (template != null)
            {
                frmCreateMailMerge frm = new frmCreateMailMerge(unitOfWork1, template);
                frm._templateSaved += frm_TemplateSaved;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    // Lấy dữ liệu
                    GetData();
                }
            }
        }

        void frm_TemplateSaved(object sender, TemplateEventArgs e)
        {
            _template = e.Template;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount > 0
                && gridView1.GetSelectedRows().Length > 0)
            {
                if (DialogUtil.ShowYesNo("Xóa dòng đang chọn?") == DialogResult.Yes)
                {
                    gridView1.DeleteSelectedRows();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            gridView1.FocusedRowHandle = -1;
            gridView1.UpdateCurrentRow();
            BindingContext[listTemplate].EndCurrentEdit();
            unitOfWork1.CommitChanges();

            // Lấy dữ liệu
            GetData();
            OnDefaultTemplateChanged();
            //
            DialogUtil.ShowInfo("Lưu biểu mẫu thành công!!!");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            //
            Close();
        }

        private void OnDefaultTemplateChanged()
        {
            if (_defaultTemplateChanged != null)
            {
                //
                if (_template == null)
                {
                    _template = gridView1.GetFocusedRow() as MailMergeTemplate;
                    if (_template == null)
                    {
                        DialogUtil.ShowWarning("Vui lòng chọn dòng để lưu.");
                    }
                }

                _defaultTemplateChanged(this, new TemplateEventArgs(_template));
            }
        }

        private void btnMacDinh_Click(object sender, EventArgs e)
        {
            MailMergeTemplate template = gridView1.GetFocusedRow() as MailMergeTemplate;
            if (template != null)
            {
                foreach (MailMergeTemplate item in listTemplate)
                {
                    item.SuDungMacDinh = false;
                }
                template.SuDungMacDinh = true;
                _template = template;
            }
        }
    }
}