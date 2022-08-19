using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.DanhMuc;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Enum.NhanSu;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.HeThong;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo.Metadata.Helpers;

namespace ERP.Module.Commons
{
    public static partial class Common
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="ftppath"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static byte[] WriteDataToByte(string filepath)
        {
            //
            FileStream MyFileStream = new FileStream(filepath, FileMode.Open);
            long FileSize = MyFileStream.Length;
            var buffer = new byte[(int)FileSize];
            MyFileStream.Read(buffer, 0, (int)MyFileStream.Length);
            MyFileStream.Close();
            //
            return buffer;
        }

        /// <summary>
        /// Ghi dữ liệu ra file txt
        /// </summary>
        /// <returns></returns>
        public static void WriteDataToFile(string path, string data)
        {
            try
            {
                StreamWriter writer = new StreamWriter(path);
                writer.WriteLine(data);
                writer.Flush();
                writer.Close();
                writer.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Tạo loại giấy tờ hồ sơ
        /// </summary>
        public static void CreateLoaiGiayToHoSo(Session session, ThongTinNhanVien nhanVien)
        {
            //Lấy danh sách loại giấy tờ
            XPCollection<LoaiGiayTo> loaiGiayToList = new XPCollection<LoaiGiayTo>(session);
            //
            foreach (var loaiGiayTo in loaiGiayToList)
            {
                GiayToHoSo giayTo = new GiayToHoSo(session);
                giayTo.HoSo = nhanVien;
                giayTo.STT = loaiGiayTo.STT;
                giayTo.TenGiayTo = loaiGiayTo.TenLoaiGiayTo;
                giayTo.GhiChu = loaiGiayTo.MaQuanLy;
                //
                nhanVien.ListGiayToHoSo.Add(giayTo);
            }
        }

        public static void ShowSuccessMessage(string message)
        {
            XtraMessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowWarningMessage(string message)
        {
            XtraMessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void ShowErrorMessage(string message)
        {
            XtraMessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult ShowMessage(string message, MessageBoxButtons button)
        {
            return XtraMessageBox.Show(message, "Thông báo", button, MessageBoxIcon.Information);
        }

        public static void SaveOutsideTransaction<T>(this UnitOfWork uow, ref T obj)
        {
            Session session = new Session(uow.DataLayer);
            object clone;
            XPClassInfo sourceClassInfo = uow.GetClassInfo<T>();
            XPClassInfo cloneClassInfo = session.GetClassInfo<T>();
            if (uow.IsNewObject(obj)) clone = cloneClassInfo.CreateNewObject(session);
            else clone = session.GetObjectByKey(cloneClassInfo, sourceClassInfo.KeyProperty.GetValue(obj));
            foreach (XPMemberInfo property in sourceClassInfo.PersistentProperties)
            {
                if (property is ServiceField) continue;
                cloneClassInfo.GetMember(property.Name).SetValue(clone, property.GetValue(obj));
            }
            session.Save(clone);
            if (uow.IsNewObject(obj)) uow.Delete(obj);
            else uow.Reload(obj);
        }
    }
}
