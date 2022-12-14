using System;
using System.Globalization;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Model;
using DevExpress.Web;
using DevExpress.ExpressApp.Xpo;
using System.Drawing;
//...
namespace ERP.Module.Web.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class TextBox_Font : ASPxPropertyEditor
    {
        ASPxTextBox textControl = null;
        //
        public TextBox_Font( Type objectType, IModelMemberViewItem info) : base(objectType, info) { }
       
        protected override void SetupControl(WebControl control)
        {
            
        }
        protected override WebControl CreateEditModeControlCore()
        {
            textControl = RenderHelper.CreateASPxTextBox();
            //
            textControl.BackColor = Color.Yellow;
            textControl.ForeColor = Color.Red;
            textControl.Font.Bold = true;
            //
            return textControl;
        }

    }
}