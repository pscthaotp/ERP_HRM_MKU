namespace ERP.Module.Extends
{
    partial class Form_NgonNgu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btTiengViet = new System.Windows.Forms.Button();
            this.btTiengAnh = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btTiengAnh);
            this.panel1.Controls.Add(this.btTiengViet);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 99);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(434, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vui lòng chọn ngôn ngữ hiển thị (Please select a display language)";
            // 
            // btTiengViet
            // 
            this.btTiengViet.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTiengViet.Location = new System.Drawing.Point(16, 49);
            this.btTiengViet.Name = "btTiengViet";
            this.btTiengViet.Size = new System.Drawing.Size(115, 28);
            this.btTiengViet.TabIndex = 2;
            this.btTiengViet.Text = "Tiếng việt";
            this.btTiengViet.UseVisualStyleBackColor = true;
            this.btTiengViet.Click += new System.EventHandler(this.btTiengViet_Click);
            // 
            // btTiengAnh
            // 
            this.btTiengAnh.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btTiengAnh.Location = new System.Drawing.Point(310, 49);
            this.btTiengAnh.Name = "btTiengAnh";
            this.btTiengAnh.Size = new System.Drawing.Size(115, 28);
            this.btTiengAnh.TabIndex = 3;
            this.btTiengAnh.Text = "English";
            this.btTiengAnh.UseVisualStyleBackColor = true;
            this.btTiengAnh.Click += new System.EventHandler(this.btTiengAnh_Click);
            // 
            // Form_NgonNgu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 99);
            this.Controls.Add(this.panel1);
            this.Name = "Form_NgonNgu";
            this.Text = "Thông báo/ Information";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btTiengAnh;
        private System.Windows.Forms.Button btTiengViet;
        private System.Windows.Forms.Label label1;

    }
}