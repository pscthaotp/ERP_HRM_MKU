using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ERP.Module.Win.Editors.Commons
{
    public class CheckedComboBoxEditEditor : IEditor
    {
        public Control Control
        {
            get
            {
                return new CheckedComboBoxEdit();
            }
        }

        public RepositoryItem RepositoryItem
        {
            get
            {
                return new RepositoryItemComboBox();
            }
        }
    }
}
