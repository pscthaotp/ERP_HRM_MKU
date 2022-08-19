using ERP.Module.Commons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Module.Extends
{
    public partial class Form_NgonNgu : Form
    {
        public Form_NgonNgu()
        {
            InitializeComponent();
        }

        private void btTiengViet_Click(object sender, EventArgs e)
        {
            Config.NgonNguSuDung = "vi-VN";
            Config.SetNgonNgu = true;

            Config.Start = true;

        }

        private void btTiengAnh_Click(object sender, EventArgs e)
        {
            Config.NgonNguSuDung = "en";
            Config.SetNgonNgu = true;

            Config.Start = true;
        }
    }
}
