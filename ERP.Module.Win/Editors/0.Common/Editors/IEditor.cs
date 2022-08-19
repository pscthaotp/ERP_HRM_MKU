using DevExpress.XtraEditors.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Module.Win.Editors.Commons
{
    public interface IEditor
    {
        Control Control { get; }
        RepositoryItem RepositoryItem { get; }
    }
}
