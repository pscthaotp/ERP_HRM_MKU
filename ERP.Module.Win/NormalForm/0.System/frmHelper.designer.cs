using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.NormalForm.System
{
    partial class frmHelper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmHelper));
            this.webBrowser1 = new WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = DockStyle.Fill;
            this.webBrowser1.Location = new Point(0, 0);
            this.webBrowser1.MinimumSize = new Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new Size(747, 475);
            this.webBrowser1.TabIndex = 0;
            // 
            // frmHelper
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(747, 475);
            this.Controls.Add(this.webBrowser1);
            this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
            this.LookAndFeel.SkinName = "Office 2007 Blue";
            this.Name = "frmHelper";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Hướng dẫn sử dụng";
            this.ResumeLayout(false);

        }
        #endregion

        private WebBrowser webBrowser1;

    }
}