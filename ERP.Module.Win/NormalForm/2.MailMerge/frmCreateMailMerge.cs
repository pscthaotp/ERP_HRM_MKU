using System;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System.Windows.Forms;
using ERP.Module.MailMerge;
using ERP.Module.Commons;

namespace ERP.Module.Win.NormalForm.MailMerge
{
    public partial class frmCreateMailMerge : XtraForm
    {
        public event EventHandler<TemplateEventArgs> _templateSaved;
        private MailMergeTemplate _template;
        private MailMergeTemplate _newTemplate;

        public frmCreateMailMerge(UnitOfWork uow, MailMergeTemplate template)
        {
            InitializeComponent();
            //
            unitOfWork1 = uow;
            _template = template;
            _newTemplate = Common.Copy<MailMergeTemplate>(uow, template);
            //
            if (_newTemplate == null)
                Close();

            obj.Session = uow;
            obj.Criteria = CriteriaOperator.Parse("Oid=?", Guid.Empty);
            if (obj.Count == 0)
            {
                _newTemplate.NgayLap = Common.GetServerCurrentTime();
                obj.Add(_newTemplate);
            }
            layoutControl1.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            unitOfWork1.CommitChanges();
            OnTemplateSaved();
            //
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OnTemplateSaved()
        {
            if (_templateSaved != null)
                _templateSaved(this, new TemplateEventArgs(_newTemplate));
        }
    }

    public class TemplateEventArgs : EventArgs
    {
        public MailMergeTemplate Template { get; private set; }

        public TemplateEventArgs(MailMergeTemplate template)
        {
            Template = template;
        }
    }
}