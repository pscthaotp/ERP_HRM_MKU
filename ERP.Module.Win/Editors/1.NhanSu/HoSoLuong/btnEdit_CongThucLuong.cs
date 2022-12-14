
using System;

using DevExpress.Xpo;

using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.ExpressApp.Model;
using ERP.Module.Win.NormalForm.NhanSu;

namespace ERP.Module.Win.Editors.NhanSu.HoSoLuong
{
    [PropertyEditor(typeof(String), false)]
    public class btnEdit_CongThucLuong : DXPropertyEditor
    {
        public btnEdit_CongThucLuong(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Text";
        }

        protected override object CreateControlCore()
        {
            ButtonEdit ctrl = new ButtonEdit();
            ctrl.Properties.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Plus;
            ctrl.Properties.AutoHeight = false;
            ctrl.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;            
            ctrl.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            ctrl.ButtonClick += ctrl_ButtonClick2;
            //
            return ctrl;
        }


        void ctrl_ButtonClick2(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if ((sender as ButtonEdit).Properties.ReadOnly) 
                return;

            frmCongThucLuong formula = new frmCongThucLuong();
            formula.BONode = View.Model.Application.BOModel;
            formula.ObjectType = (CurrentObject as XPBaseObject).GetMemberValue("ExpressionType").ToString();
            //
            ButtonEdit edit = (ButtonEdit)sender;
            int len = edit.SelectionLength;
            int pos = edit.SelectionStart;
            string txt = edit.SelectedText;
            if (formula.ShowDialog() == DialogResult.OK)
            {
                string s = formula.GetCurrentField();
                if (string.IsNullOrEmpty(s)) 
                    return;
                if (len > 0)
                    edit.Text = edit.Text.Replace(txt, s);
                else
                {
                    edit.Text = edit.Text.Insert(pos, s);
                    edit.SelectionStart = pos + s.Length;
                }
            }
        }

        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            UpdateControlEnabled();
        }

        protected override void OnAllowEditChanged()
        {
            base.OnAllowEditChanged();
            UpdateControlEnabled();
        }

        private void UpdateControlEnabled()
        {
            if (Control != null)
            {
                if (AllowEdit)
                {
                    Control.Enabled = true;
                    Control.Properties.ReadOnly = false;
                }
                else
                {
                    Control.Enabled = false;
                    Control.Properties.ReadOnly = true;
                }
            }
        }
    }

}
