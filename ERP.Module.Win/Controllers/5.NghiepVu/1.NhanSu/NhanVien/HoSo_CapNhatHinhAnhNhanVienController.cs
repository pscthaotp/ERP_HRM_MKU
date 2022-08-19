using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NonPersistentObjects.NhanSu;
using System.Text;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;
using DevExpress.Xpo;
using System.IO;
using System.Diagnostics;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class HoSo_CapNhatHinhAnhNhanVienController : ViewController
    {
        IObjectSpace _obs;
        HoSo_ChonHinhAnh _obj;

        public HoSo_CapNhatHinhAnhNhanVienController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "NhanVienDangLamViec_ListView;ThongTinNhanVien_DetailView;NhanSuCustomView_DetailView";
        }

        private void HoSo_CapNhatHinhAnhNhanVienController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = (Common.IsWriteGranted<ThongTinNhanVien>()
                                                        && (Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriCongTy
                                                           || Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriHeThong));
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _obj = _obs.CreateObject<HoSo_ChonHinhAnh>();
            e.View = Application.CreateDetailView(_obs, _obj);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_obj != null)
            {                                    
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Images files (*.png; *jpg)|*.png;*.jpg";
                    dialog.DefaultExt = "Images files (*.png; *jpg)|*.png;*.jpg";
                    dialog.Multiselect = true;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        StringBuilder mainLog = new StringBuilder();
                        StringBuilder detailLog;
                        bool sucessImport = true;
                        int sucessNumber = 0;
                        int erorrNumber = 0;
                        using (var uow = new UnitOfWork(((XPObjectSpace)_obs).Session.DataLayer))
                        {
                            uow.BeginTransaction();

                            using (DialogUtil.AutoWait())
                            {
                                foreach (string fullFileName in dialog.FileNames)
                                {
                                    detailLog = new StringBuilder();
                                    string fileName = fullFileName.Substring(fullFileName.LastIndexOf(@"\") + 1);                                   
                                    string maNhanVien = fileName.Replace(".png", String.Empty).Replace(".PNG", String.Empty).Replace(".JPG", String.Empty).Replace(".jpg", String.Empty);
                                    //Lấy đường dẫn máy chủ theo tên loại giấy tờ
                                    string filePath = string.Format("{0}/", Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.URL_NhanVien);

                                    if (!string.IsNullOrEmpty(maNhanVien))
                                    {//Tiến hành upload dữ liệu
                                        NhanVien nhanVien = uow.FindObject<NhanVien>(CriteriaOperator.Parse("CongTy = ? and (MaNhanVien like ? or MaTapDoan like ?)", _obj.CongTy.Oid, maNhanVien, maNhanVien));
                                        if (nhanVien != null)
                                        {
                                            try
                                            {
                                                FptProvider.UploadImageFile(filePath, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Username, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Password, fullFileName, fileName);

                                                nhanVien.URLHinh = fileName;
                                            }
                                            catch (Exception ex)
                                            {
                                                detailLog.Append("+ Lỗi: " + ex);
                                            }
                                        }
                                        else
                                            detailLog.AppendLine(string.Format("- Nhân viên có mã: {0} không thuộc {1} quản lý ", maNhanVien, _obj.CongTy.TenBoPhan));

                                        //Đưa thông tin bị lỗi vào blog
                                        if (detailLog.Length > 0)
                                        {
                                            mainLog.AppendLine(string.Format("- Không upload hình [{0}] vào hệ thống được: ", fullFileName));
                                            mainLog.AppendLine(detailLog.ToString());

                                            sucessImport = false;
                                        }

                                        ///////////////////////////NẾU THÀNH CÔNG THÌ SAVE/////////////////////////////////
                                        if (sucessImport)
                                        {                                                
                                            uow.CommitChanges();
                                            //
                                            sucessNumber++;
                                        }
                                        else
                                        {
                                            erorrNumber++;
                                            //
                                            sucessImport = true;
                                        }
                                    }
                                }

                                string s = (erorrNumber > 0 ? "Mời bạn xem file log" : "");
                                DialogUtil.ShowInfo("Upload Thành Công " + sucessNumber + " file ảnh - Số file ảnh upload không thành công " + erorrNumber + " " + s + "!");

                                //Mở file log lỗi lên
                                if (erorrNumber > 0)
                                {
                                    string tenFile = "Import_Log.txt";
                                    StreamWriter writer = new StreamWriter(tenFile);
                                    writer.WriteLine(mainLog.ToString());
                                    writer.Flush();
                                    writer.Close();
                                    writer.Dispose();
                                    Common.WriteDataToFile(tenFile, mainLog.ToString());
                                    Process.Start(tenFile);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
