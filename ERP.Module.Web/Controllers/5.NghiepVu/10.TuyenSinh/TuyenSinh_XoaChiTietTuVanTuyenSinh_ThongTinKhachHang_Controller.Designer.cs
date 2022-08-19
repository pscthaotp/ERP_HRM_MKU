namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    partial class TuyenSinh_XoaChiTietTuVanTuyenSinh_ThongTinKhachHang_Controller
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = "Xóa";
            this.simpleAction1.ConfirmationMessage = "Bạn có xác nhận xóa hay không ?";
            this.simpleAction1.Id = "Xoa_ChiTietTuVanTuyenSinh_ThongTinKhacHang";
            this.simpleAction1.ImageName = "BO_HinhThucKyLuat";
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // TuyenSinh_XoaChiTietTuVanTuyenSinh_ThongTinKhachHang_Controller
            // 
            this.Actions.Add(this.simpleAction1);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;

    }
}
