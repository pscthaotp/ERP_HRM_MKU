using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using ERP.Module.Win.Editors.Commons;
using DevExpress.Data.Filtering;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Xpo.DB;

namespace ERP.Module.Win.Editors.NhanSu
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_NamHoc : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        object _obj;

        public chkComboxEdit_NamHoc(Type objectType, IModelMemberViewItem model)
            : base(objectType, model)
        {
            ControlBindingProperty = "Value";
        }

        protected override object CreateControlCore()
        {


            checkedListBox = editor.Control as CheckedComboBoxEdit;
            //
            if (checkedListBox != null)
            {
                checkedListBox.Properties.SelectAllItemCaption = "Tất cả";
                checkedListBox.Properties.TextEditStyle = TextEditStyles.Standard;
                checkedListBox.Properties.Items.Clear();

                List<CheckedListBoxItem> checkedListBoxItemList = new List<CheckedListBoxItem>();
                

                //Check các item
                foreach (CheckedListBoxItem item in checkedListBoxItemList)
                {
                     item.CheckState = CheckState.Unchecked;
                }
                //
                checkedListBox.Properties.Items.AddRange(checkedListBoxItemList.ToArray());
                checkedListBox.Properties.SeparatorChar = ';';
                checkedListBox.EditValueChanged += SetValueOfComboBox;
                checkedListBox.Refresh();
            }
            return checkedListBox;
        }

        private void AddItemComboBoxEdit(List<CheckedListBoxItem> checkedListBoxItemList)
        {
            XPCollection<NamHoc> listNamHoc = new XPCollection<NamHoc>(((XPObjectSpace)View.ObjectSpace).Session, CriteriaOperator.Parse("NgayKetThuc >=?", DateTime.Now.Date));
            SortingCollection sortCollection = new SortingCollection();
            sortCollection.Add(new SortProperty("NgayBatDau", SortingDirection.Ascending));
            listNamHoc.Sorting = sortCollection;
            foreach (NamHoc namHoc in listNamHoc)
            {
                checkedListBoxItemList.Add(new CheckedListBoxItem(namHoc.Oid, namHoc.TenNamHoc, CheckState.Unchecked, true));
            }
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            
        }

        protected override DevExpress.XtraEditors.Repository.RepositoryItem CreateRepositoryItem()
        {
            return editor.RepositoryItem;
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
                Control.Enabled = true;
                Control.Properties.ReadOnly = false;
            }
        }
    }

}
