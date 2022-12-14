using System;
using System.ComponentModel;

namespace ERP.Module.Win.Controllers.Custom
{
    partial class CustomNumberColumnController
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();

            this.Activated += new EventHandler(this.CustomNumberColumnController_Activated);
            this.Deactivated += new EventHandler(this.CustomNumberColumnController_Deactivated);
        }

        #endregion
    }
}
