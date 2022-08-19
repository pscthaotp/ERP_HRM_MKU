using DevExpress.ExpressApp.Win.Editors;
using DevExpress.Utils;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Module.Extends
{
    public partial class GridUtil
    {
        /// Enum Format Column
        public enum ColumnFormat { NULL, Date, DateTime, N0, N1, N2, N3, N4 }

        #region Process
        /// Hàm dùng để khởi tạo cấu hình giao diện cho grid view.
        public static void InitGridView(GridView grv)
        {

            #region Step 1: Set các thuộc tính cơ bản
            /// Filter Row
            //grv.OptionsView.ShowAutoFilterRow = true;

            grv.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            grv.IndicatorWidth = 500;
            /// Wrap Header
            grv.Appearance.HeaderPanel.TextOptions.WordWrap = WordWrap.Default;

            /// HorzAlignment Text
            grv.Appearance.HeaderPanel.TextOptions.HAlignment = HorzAlignment.Center;

            /// Tự động ẩn Group Panel 
            grv.OptionsView.ShowGroupPanel = false;

            /// Tắt mở hệ thông Panel Filter
            grv.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;

            /// Show footer của lưới
            grv.OptionsView.ShowFooter = true;

            foreach (GridColumn item in grv.Columns)
            {
                    //
                    if (item.ColumnType == typeof(decimal))
                    {
                        item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    }
                    else if (item.ColumnType == typeof(DateTime) || item.ColumnType == typeof(bool) || item.ColumnType == typeof(int))
                    {
                        item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    }
                    else
                    {
                        item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                    }
                    //
                    item.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
                    item.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                    item.BestFit();
            }
            //
            #endregion

            #region Step 2: Bổ sung các sự kiện trên lưới
            /// Sự kiện cho việc đánh số thứ tự
            ShowRowNumber(grv);
            #endregion
        }

        //Cấu hình lại các lưới đặt biệt
        public static void SetupHorzAlignmentGridView(GridView grv)
        {
            #region Step 1: Set các thuộc tính cơ bản
            foreach (GridColumn item in grv.Columns)
            {
                //
                item.OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
                item.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                //
                if (item.ColumnType == typeof(decimal))
                {
                    item.Width = 120;
                    item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Far;
                    
                }
                else if (item.ColumnType == typeof(DateTime))
                {
                    item.Width = 100;
                    item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }
                else if (item.ColumnType == typeof(bool))
                {
                    item.Width = 100;
                    item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                }
                else if (item.ColumnType == typeof(string))
                {
                    item.Width = 200;
                    item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                }
                else
                {
                    item.Width = 150;
                    item.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Near;
                }
            }
            #endregion
        }

        //Không cho chọn nhiều dòng trên lưới
        public static void SetupMultiSelectGridView(GridView grv, bool value)
        {
            grv.OptionsSelection.MultiSelect = value;
        }

        /// Hàm dùng để tạo summaries của lưới
        public static void ShowSummaries(GridView gridView, bool count = false, bool average = false, bool sum = false, bool max = false, bool min = false, params string[] fieldNames)
        {
            //Cho phép chỉnh vị trí của footer
            gridView.Appearance.FooterPanel.Options.UseTextOptions = true;
            gridView.Appearance.FooterPanel.TextOptions.HAlignment = HorzAlignment.Near;

            
            for (int i = 0; i < fieldNames.Length; i++)
            {
                //Xóa cấu hình cũ
                gridView.Columns[fieldNames[i]].Summary.Clear();
                //
                if (count)//Count
                {
                    gridView.Columns[fieldNames[i]].Summary.Add(DevExpress.Data.SummaryItemType.Count, fieldNames[i], "Count={0:N0}");
                }
                if (average)//Trung bình cộng
                {
                    gridView.Columns[fieldNames[i]].Summary.Add(DevExpress.Data.SummaryItemType.Average, fieldNames[i], "Avg={0:n2}");
                }
                if (sum)//Tổng cộng
                {
                    gridView.Columns[fieldNames[i]].Summary.Add(DevExpress.Data.SummaryItemType.Sum, fieldNames[i], "Sum={0:N0}");
                }
                if (max)// Lớn nhất
                {
                    GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Max, fieldNames[i], "Max={0:N0}");
                    gridView.Columns[fieldNames[i]].Summary.Add(item);
                }
                if (min)// Nhỏ nhất
                {
                    GridColumnSummaryItem item = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Min, fieldNames[i], "Min={0}");
                    gridView.Columns[fieldNames[i]].Summary.Add(item);
                }
            }
        }

        /// Hàm dùng để tạo summaries của group
        public static void ShowGroupSummaries(GridView gridView, bool count = false, bool average = false, bool sum = false, bool max = false, bool min = false, params string[] fieldNames)
        {
            gridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DefaultBoolean.True;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                if (count)//Count
                {
                    gridView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, fieldNames[i], gridView.Columns[fieldNames[i]], "Count={0:N0}");
                }
                if (average)//Trung bình cộng
                {
                    gridView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Average, fieldNames[i], gridView.Columns[fieldNames[i]], "Avg={0:N0}");
                }
                if (sum)//Tổng cộng
                {
                    gridView.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, fieldNames[i], gridView.Columns[fieldNames[i]], "Sum={0:N0}");
                }
                if (max)// Lớn nhất
                {
                    GridGroupSummaryItem item = new GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Max, fieldNames[i], gridView.Columns[fieldNames[i]], "Max={0:N0}");
                    gridView.GroupSummary.Add(item);
                }
                if (min)// Nhỏ nhất
                {
                    GridGroupSummaryItem item = new GridGroupSummaryItem(DevExpress.Data.SummaryItemType.Min, fieldNames[i], gridView.Columns[fieldNames[i]], "Min={0}");
                    gridView.GroupSummary.Add(item);
                }
            }

            #region vidu
            /*
             gridView.OptionsBehavior.AlignGroupSummaryInGroupRow = DefaultBoolean.True;
            // group summaries 
            GridGroupSummaryItem item = new GridGroupSummaryItem() {
                FieldName = "Length",
                SummaryType = SummaryItemType.Sum,
                ShowInGroupColumnFooter = gridView.Columns["Length"]
            };
            gridView.GroupSummary.Add(item);

            item = new GridGroupSummaryItem() {
                FieldName = "Mark",
                SummaryType = SummaryItemType.Count,
                ShowInGroupColumnFooter = gridView.Columns["Mark"]
            };
            gridView.GroupSummary.Add(item);
            gridView.Columns["ID"].Group();
            // Handle this event to paint group row cells manually 
            gridView.CustomDrawGroupRowCell += (s, e) => {
                e.Appearance.BackColor = Color.BlanchedAlmond;
                e.Appearance.FillRectangle(e.Cache, e.Bounds);
                e.Appearance.ForeColor = Color.DimGray;
                e.Appearance.DrawString(e.Cache, e.DisplayText, e.Bounds);
                e.Handled = true;
            };
             */
            #endregion vidu
        }

        /// Bật cột số thứ tự trên GridView
        public static void ShowRowNumber(GridView grv)
        {
            grv.IndicatorWidth = 40;
            grv.CustomDrawRowIndicator -= grv_CustomDrawRowIndicator;
            grv.CustomDrawRowIndicator += grv_CustomDrawRowIndicator;
        }
       
        /// Tạo dòng thêm mới trên lưới
         public static void NewItemRow(GridView grv, NewItemRowPosition location = NewItemRowPosition.None, string strText = "")
        {
            /// Set vị trí 
            grv.OptionsView.NewItemRowPosition = location;

            /// Set text hiển thị nếu có
            if (strText != string.Empty)
                grv.NewItemRowText = strText;
        }
        
        /// Hàm đánh số thứ tự của từng dòng bằng sự kiện vẽ cột Indication
        public static void CreateNumberRow(DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            /// Lấy thông tin từng dòng đang vẽ
            DevExpress.Utils.Drawing.IndicatorObjectInfoArgs args = default(DevExpress.Utils.Drawing.IndicatorObjectInfoArgs);
            args = (DevExpress.Utils.Drawing.IndicatorObjectInfoArgs)e.Info;

            /// Nếu dòng đang thao tác lớn hơn hoặc bằng 0 thì đánh số 
            if (e.RowHandle >= 0)
                args.DisplayText = (e.RowHandle + 1).ToString();
        }

        /// Hàm Fix thứ tự các các cột về bên trai hoặc bên tay phải
        public static void FixColumn(GridView grv, string[] fieldNames, DevExpress.XtraGrid.Columns.FixedStyle fixType = DevExpress.XtraGrid.Columns.FixedStyle.Left)
        {
            /// Field Name
            string strFieldName = string.Empty;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                /// Lấy field Name
                strFieldName = fieldNames[i];

                /// Set thuộc tính Fix cho các column
                grv.Columns[strFieldName].Fixed = fixType;
            }
        }

        /// Hàm cho phép Edit hoặc không cho edit một số cột
        public static void AllowEditColumn(GridView grv, string[] fieldNames = null, bool bEditColum = false)
        {
            /// Field Name
            string strFieldName = string.Empty;

            /// Nếu danh sách truyền vào là Null thì áp lên toàn bộ các cột
            if (fieldNames == null)
            {
                foreach (GridColumn col in grv.Columns)
                {
                    col.OptionsColumn.AllowEdit = bEditColum;
                }
            }
            else
            {
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    /// Lấy field Name
                    strFieldName = fieldNames[i];

                    /// Set thuộc tính Fix cho các column
                    grv.Columns[strFieldName].OptionsColumn.AllowEdit = bEditColum;
                }
            }
        }

        /// Hàm cho phép Sort hoặc không cho Sort một số cột
        public static void AllowSortColumn(GridView grv, string[] fieldNames = null, DefaultBoolean bSortColum = DefaultBoolean.False)
        {
            /// Field Name
            string strFieldName = string.Empty;

            /// Nếu danh sách truyền vào là Null thì áp lên toàn bộ các cột
            if (fieldNames == null)
            {
                foreach (GridColumn col in grv.Columns)
                {
                    col.OptionsColumn.AllowSort = bSortColum;
                }
            }
            else
            {
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    /// Lấy field Name
                    strFieldName = fieldNames[i];

                    /// Set thuộc tính Fix cho các column
                    grv.Columns[strFieldName].OptionsColumn.AllowSort = bSortColum;
                }
            }
        }

        /// Hàm cho phép Ẩn một số cột khi đang chạy
        public static void InvisibleColumn(GridView grv, string[] fieldNames)
        {
            /// Field Name
            string strFieldName = string.Empty;

            /// Nếu danh sách truyền vào là Null thì áp lên toàn bộ các cột
            for (int i = 0; i < fieldNames.Length; i++)
            {
                /// Lấy field Name
                strFieldName = fieldNames[i];

                /// Set thuộc tính Fix cho các column
                if (grv.Columns[strFieldName] != null)
                    grv.Columns[strFieldName].Visible = false;
            }
        }

        public static void RenameColumn(GridView grv, string fieldName, string newName)
        {
            /// Set caption mới cho các column
            if (grv.Columns[fieldName] != null)
                grv.Columns[fieldName].Caption = newName;
        }

        /// Hàm Set trạng thái Read Only của lưới
        public static void SetReadOnly(GridView grv, bool bEdit = false)
        {
            grv.OptionsBehavior.Editable = bEdit;
        }

        /// Hàm dùng để nhóm lưới (một hoặc nhiều cột)
        public static void GroupingByColumn(GridView grv, string[] fieldNames)
        {
            /// Field Name
            string strFieldName = string.Empty;

            ///Bắt đầu group
            grv.BeginSort();
           
            try
            {
                //Xóa tất cả các group
                grv.ClearGrouping();
                //
                int index = 0;

                for (int i = 0; i < fieldNames.Length; i++)
                {
                   
                    /// Lấy field Name
                    strFieldName = fieldNames[i];

                   
                    grv.Columns[strFieldName].GroupIndex = index;
                    //
                    index++;
                }
            }
            finally
            {
                grv.EndSort();
            }         
        }

        /// Hàm cho phép xuống nhiều dòng khi đang chạy
        public static void ShowMultilineGridCell(GridView grv, string[] fieldNames, string[] Booleanfield = null)
        {
            grv.OptionsView.RowAutoHeight = true;
            RepositoryItemMemoEdit memoEdit = new RepositoryItemMemoEdit();
            memoEdit.ReadOnly = true;
            memoEdit.AutoHeight = true;
            memoEdit.Appearance.TextOptions.WordWrap = WordWrap.Wrap;
            memoEdit.WordWrap = true;

            /// Nếu danh sách truyền vào là Null thì áp lên toàn bộ các cột
            if (fieldNames == null)
            {
                foreach (GridColumn columna in grv.Columns)
                {
                    columna.ColumnEdit = memoEdit;
                }
            }
            else
            {
                /// Field Name
                string strFieldName = string.Empty;

                for (int i = 0; i < fieldNames.Length; i++)
                {
                    /// Lấy field Name
                    strFieldName = fieldNames[i];

                    /// Set thuộc tính Fix cho các column
                    if (grv.Columns[strFieldName] != null)
                        grv.Columns[strFieldName].ColumnEdit = memoEdit;
                }
            }

            if (Booleanfield != null)
            {
                string strFieldName = string.Empty;

                for (int i = 0; i < Booleanfield.Length; i++)
                {
                    /// Lấy field Name
                    strFieldName = Booleanfield[i];

                    /// Set thuộc tính Fix cho các column
                    if (grv.Columns[strFieldName] != null)
                    {
                        RepositoryItemBooleanEdit booleanEdit = new RepositoryItemBooleanEdit();
                        grv.Columns[strFieldName].ColumnEdit = booleanEdit;
                    }
                }
            }
        }

        /// Hàm cố định kích thước ảnh trong gridview
        public static void ShowImageGridCell(GridView grv, string[] fieldNames, int auto = -1)
        {
            grv.OptionsView.RowAutoHeight = true; 
            if(auto != -1)
            {
                RepositoryItemPictureEdit pictureEdit = new RepositoryItemPictureEdit();
                pictureEdit.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                pictureEdit.CustomHeight = auto;
                pictureEdit.NullText = " ";
                
                /// Nếu danh sách truyền vào là Null thì áp lên toàn bộ các cột
                if (fieldNames == null)
                {
                    foreach (GridColumn columna in grv.Columns)
                    {
                        columna.ColumnEdit = pictureEdit;
                    }
                }
                else
                {
                    /// Field Name
                    string strFieldName = string.Empty;

                    for (int i = 0; i < fieldNames.Length; i++)
                    {
                        /// Lấy field Name
                        strFieldName = fieldNames[i];

                        /// Set thuộc tính Fix cho các column
                        if (grv.Columns[strFieldName] != null)
                        {
                            grv.Columns[strFieldName].ColumnEdit = pictureEdit;
                            grv.Columns[strFieldName].MaxWidth = auto;
                            grv.Columns[strFieldName].Width = auto;
                        }
                    }
                }
            }
        }

        public static void SetupCellMerge(GridView grv)
        {
            grv.OptionsView.AllowCellMerge = true;

            foreach (GridColumn item in grv.Columns)
            {
                item.SortIndex = -1;

                if (item.FieldName != "GuiGroup"
                    && item.FieldName != "HangHoaMonAn!"
                    && item.FieldName != "DonViTinhMonAn!"
                    && item.FieldName != "SoLuongMonAn")
                {
                    item.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
                }

                if (item.FieldName == "GuiGroup")
                {
                    item.SortIndex = 0;
                    item.Visible = false;
                }
            }
            grv.CellMerge += gridView_CellMerge;
        }

        #endregion

        #region Event
        /// Sự kiện vẽ lại cột Indicator
        static void grv_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            /// Đánh số thứ tự
            CreateNumberRow(e);
        }

        static void gridView_CellMerge(object sender, CellMergeEventArgs e)
        {
            GridView gridView = (GridView)sender;
            GridColumn col = gridView.Columns.ColumnByFieldName("GuiGroup");
            string text1 = gridView.GetRowCellDisplayText(e.RowHandle1, col);
            string text2 = gridView.GetRowCellDisplayText(e.RowHandle2, col);
            e.Merge = (text1 == text2);
            e.Handled = true;

        }
        #endregion
    }
}
