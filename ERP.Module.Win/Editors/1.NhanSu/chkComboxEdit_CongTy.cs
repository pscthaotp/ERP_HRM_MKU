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
using ERP.Module.Commons;

namespace ERP.Module.Win.Editors.NhanSu
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_CongTy : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        object _obj;

        public chkComboxEdit_CongTy(Type objectType, IModelMemberViewItem model)
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

                //Lấy
                if (View != null)
                {
                    _obj = View.CurrentObject;
                    //
                    if (_obj != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }

                //Check các item
                //foreach (CheckedListBoxItem item in checkedListBoxItemList)
                //{
                //    if (_obj != null && !string.IsNullOrEmpty(_obj..CongTy) && (_obj as HocPhi_DanhSachChuaDongPhi).LoaiPhi.Contains(string.Format("{0}", item.Value)))
                //    {
                //        item.CheckState = CheckState.Checked;
                //    }
                //    else
                //    {
                //        item.CheckState = CheckState.Unchecked;
                //    }
                //}
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
            string CongTyPhanQuyen = Common.System_GetDeparment_Role_ByUser();//Oid công ty dc phân quyền
            System.Data.DataTable dt = DataProvider.GetDataTable("select * from func_SplitStringConvertToTable('" + CongTyPhanQuyen.ToString() + "',';')", System.Data.CommandType.Text);

            ERP.Module.NghiepVu.NhanSu.BoPhans.CongTy cty = null;
            foreach (System.Data.DataRow r in dt.Rows)
            {
                cty = ((XPObjectSpace)View.ObjectSpace).Session.FindObject<ERP.Module.NghiepVu.NhanSu.BoPhans.CongTy>(CriteriaOperator.Parse("Oid =? and LoaiTruong =?", r["Oid"], ERP.Module.Enum.TuyenSinh_PT.LoaiTruongEnum.MN));
                if (cty != null)
                    checkedListBoxItemList.Add(new CheckedListBoxItem(cty.Oid, cty.TenBoPhan, CheckState.Unchecked, true));
            }
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            var key = _obj;
            if (checkedListBox != null)
            {             
                //    if (key.ToString().Contains("NgoaiKhoa_ThongKeNgoaiKhoa"))
                //{
                //    (_obj as NgoaiKhoa_ThongKeNgoaiKhoa).CongTy = checkedListBox.EditValue.ToString().Trim().Replace(" ", "");
                //}
            }
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
