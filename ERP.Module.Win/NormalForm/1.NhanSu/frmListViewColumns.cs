using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ERP.Module.Win.Editors.Commons;
using ERP.Module.Extends;

namespace ERP.Module.Win.NormalForm.NhanSu
{
    public partial class frmListViewColumns : DevExpress.XtraEditors.XtraForm
    {
        private Type _type;
        private List<ObjectProperty> _dataSource;

        public frmListViewColumns(Type type)
        {
            InitializeComponent();
            //
            _type = type;
        }

        private void frmListViewColumns_Load(object sender, EventArgs e)
        {
            GridUtil.InitGridView(gridView1);
            //
            _dataSource = ObjectPropertyHelper.GetDataSource(_type);
            gridControl1.DataSource = _dataSource;
        }

        private void ckChonTatCa_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ObjectProperty item in _dataSource)
            {
                if (item.Chon != ckChonTatCa.Checked)
                    item.Chon = ckChonTatCa.Checked;
            }
            gridControl1.RefreshDataSource();
        }

        public List<ObjectProperty> GetData()
        {
            var o = (from i in _dataSource
                    where i.Chon == true
                    select i).ToList<ObjectProperty>();

            return o;
        }
    }
}