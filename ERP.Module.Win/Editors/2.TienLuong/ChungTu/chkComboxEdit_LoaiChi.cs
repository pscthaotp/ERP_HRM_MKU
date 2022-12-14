
using System;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Win.Editors;
using DevExpress.XtraEditors;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraCharts.Native;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.Win.Editors.Commons;
using ERP.Module.Enum.TienLuong;
//
namespace ERP.Module.Win.Editors.TienLuong.ChungTus
{
    [PropertyEditor(typeof(String), false)]
    public class chkComboxEdit_LoaiChi : DXPropertyEditor
    {
        private readonly IEditor editor = EditorFactory.GetEditor(EditorTypeEnum.CheckedComboBoxEdit);
        CheckedComboBoxEdit checkedListBox;
        ChungTu _chungTu;

        public chkComboxEdit_LoaiChi(Type objectType, IModelMemberViewItem model)
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

                //Lấy chứng từ hiện tại
                if (View != null && View.Id.Contains("ChungTu_DetailView"))
                {
                    _chungTu = View.CurrentObject as ChungTu;
                    //
                    if (_chungTu != null)
                    {
                        //Thêm các item vào
                        AddItemComboBoxEdit(checkedListBoxItemList);
                    }
                }

                //Check các item
                foreach (CheckedListBoxItem item in checkedListBoxItemList)
                {
                    if ((_chungTu != null && !string.IsNullOrEmpty(_chungTu.LoaiChi) && _chungTu.LoaiChi.Contains(string.Format("{0}", item.Value)))
                       )
                    {
                        item.CheckState = CheckState.Checked;
                    }
                    else
                    { item.CheckState = CheckState.Unchecked; }
                }
                //
                checkedListBox.Properties.Items.AddRange(checkedListBoxItemList.ToArray());
                checkedListBox.Properties.SeparatorChar = ';';
                checkedListBox.EditValueChanged += SetValueOfComboBox;
                checkedListBox.Refresh();

            }
            return checkedListBox;
        }

        private static void AddItemComboBoxEdit(List<CheckedListBoxItem> checkedListBoxItemList)
        {
            //Thêm các item 
            checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.LuongVaPhuCap, "Lương - phụ cấp", CheckState.Unchecked, true));
            checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.NgoaiGio, "Ngoài giờ", CheckState.Unchecked, true));
            checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.KhauTruLuong, "Khấu trừ lương", CheckState.Unchecked, true));
            checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.ThuNhapKhac, "Thu nhập khác", CheckState.Unchecked, true));
            checkedListBoxItemList.Add(new CheckedListBoxItem(LoaiChiEnum.KhenThuong, "Khen thưởng - phúc lợi", CheckState.Unchecked, true));
        }

        private void SetValueOfComboBox(object sender, EventArgs e)
        {
            //Lấy chứng từ hiện tại
            if (View != null && View.Id.Contains("ChungTu_DetailView") && checkedListBox != null)
            {
                _chungTu = View.CurrentObject as ChungTu;
                _chungTu.LoaiChi = (string)checkedListBox.EditValue.ToString().Trim();
                _chungTu.TinhThueTNCN = true;
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
