using DevExpress.Web;
using ERP.Module.Commons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ERP.Module.Extends
{
    public partial class ASPxGridUtil
    {
        /// Enum Format Column
        public enum ColumnFormat
        {
            NULL,
            Date,
            DateTime,
            N0,
            N1,
            N2,
            N3,
            N4
        }

        /// Hàm dùng để khởi tạo cấu hình giao diện cho grid view.
        public static void InitGridView(ASPxGridView grv)
        {
            /// Filter Row
            grv.Settings.ShowFilterRow = true;

            grv.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            /// ScrollBar
            grv.Settings.ShowFooter = true;
            /// FocusedRow
            grv.SettingsBehavior.AllowFocusedRow = true;
            /// FocusedRow
            grv.SettingsBehavior.AllowSort = true;

            //  Id
            grv.KeyFieldName = "Oid";
        }

        /// Hàm dùng để khởi tạo cấu hình giao diện cho grid view
        public static void InitGridViewCustom(ASPxGridView grv)
        {
            /// Filter Row
            grv.Settings.ShowFilterRow = true;

            grv.Settings.AutoFilterCondition = AutoFilterCondition.Contains;
            /// ScrollBar
            grv.Settings.ShowFooter = true;
            /// FocusedRow
            grv.SettingsBehavior.AllowFocusedRow = true;
            /// FocusedRow
            grv.SettingsBehavior.AllowSort = true;
            //  Id
            grv.KeyFieldName = "OidCustom";
        }

        public static void InvisibleColumn(ASPxGridView grv, string[] fieldNames)
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
            foreach (var item in grv.DataColumns)
            {
                if (item.FieldName.Equals("STT,DiemTrungBinh_HK1,DiemTrungBinh_HK2,DiemTrungBinh_CaNam"))
                    grv.Columns[item.FieldName].Visible = false;
            }
        }
        public static void InvisibleAllColumn(ASPxGridView grv)
        {
           
            foreach (var item in grv.DataColumns)
            {
               grv.Columns[item.FieldName].Visible = false;
            }
        }


        public static void InShowColumn(ASPxGridView grv, string[] fieldNames)
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
                    grv.Columns[strFieldName].Visible = true;
            }

        }
        public static void InShowAllColumn(ASPxGridView grv)
        {
            /// Field Name

            foreach (var item in grv.DataColumns)
            {
                grv.Columns[item.FieldName].Visible = true;
            }
        }
        public static void RenameColumn(ASPxGridView grv, string fieldName, string newName)
        {
            /// Set caption mới cho các column
            if (grv.Columns[fieldName] != null)
                grv.Columns[fieldName].Caption = newName;
        }

        public static void CustomWithGridCell(ASPxGridView grv, string[] fieldNames, int width)
        {
            string strFieldName = string.Empty;

            for (int i = 0; i < fieldNames.Length; i++)
            {
                /// Lấy field Name
                strFieldName = fieldNames[i];
                if (grv.Columns[strFieldName] != null)
                {
                    grv.Columns[strFieldName].MinWidth = width;
                    grv.Columns[strFieldName].Width = width;
                }
            }
        }
    }
}
