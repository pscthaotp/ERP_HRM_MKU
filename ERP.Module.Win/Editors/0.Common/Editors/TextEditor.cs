using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Module.Win.Editors.Commons
{
    public class TextEditor : IEditor
    {
        public Control Control
        {
            get
            {
                return new TextEdit();
            }
        }

        public RepositoryItem RepositoryItem
        {
            get
            {
                return new RepositoryItemTextEdit();
            }
        }
    }
}
