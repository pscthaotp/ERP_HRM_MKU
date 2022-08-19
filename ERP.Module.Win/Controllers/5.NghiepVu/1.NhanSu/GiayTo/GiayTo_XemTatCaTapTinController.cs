using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using System.Text;
using System.IO;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.Commons;
using ERP.Module.Extends;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.GiayTo
{
    public partial class GiayTo_XemTatCaTapTinController : ViewController
    {
        //
        public GiayTo_XemTatCaTapTinController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            GiayToHoSo obj = View.CurrentObject as GiayToHoSo;
            if (obj != null)
            {
                //
                IObjectSpace obs = Application.CreateObjectSpace();

               
                    SaveFileDialog savefile = new SaveFileDialog();
                    savefile.FileName = "All";
                    savefile.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    if (savefile.ShowDialog() == DialogResult.OK)
                    {
                        string[] arrListStr = savefile.FileName.Split('\\');
                        string savePath = savefile.FileName.Replace(arrListStr[arrListStr.Length - 1],"");

                        //Lấy danh sách tất cả tập tin
                        CriteriaOperator filter = CriteriaOperator.Parse("HoSo=? and LoaiGiayTo IS NOT NULL", obj.HoSo.Oid);
                        XPCollection<GiayToHoSo> fileList = new XPCollection<GiayToHoSo>(((XPObjectSpace)obs).Session, filter);
                        //
                        var mainLog = new StringBuilder();
                        foreach (var item in fileList)
                        {
                            var errorLog = new StringBuilder();
                            //
                             //             
                            try
                            {
                                byte[] data = FptProvider.DownloadFile(item.DuongDanFile, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Username, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Password);
                                if (data != null)
                                {
                                    string fullPath = string.Format("{0}{1}", savePath, item.TenGiayTo);

                                    //Lưu file vào thư mục bin\Debug
                                    FptProvider.SaveFilePDF(data, fullPath);
                                }
                                else
                                {
                                    errorLog.AppendLine(string.Format(" + {0} không tồn tại trên máy chủ.", item.TenGiayTo));
                                }
                            }
                            catch
                            {
                                errorLog.AppendLine(string.Format(" + {0} không tồn tại trên máy chủ.", item.TenGiayTo));
                            }

                            #region Ghi lỗi nếu không down được
                            {
                                //Đưa thông tin bị lỗi vào blog
                                if (errorLog.Length > 0)
                                {
                                    mainLog.AppendLine("- Một số giấy tờ hồ sơ không thể tải: ");
                                    mainLog.AppendLine(errorLog.ToString());
                                    //
                                }
                            }
                            #endregion
                        }
                        //
                        if (mainLog.Length > 0)
                        {
                            string fullPath = string.Format("{0}{1}", savePath, "Erorr.txt");
                            StreamWriter writer = new StreamWriter(fullPath);
                            writer.WriteLine(mainLog.ToString());
                            writer.Flush();
                            writer.Close();
                            writer.Dispose();
                            Common.WriteDataToFile(fullPath, mainLog.ToString());
                            Process.Start(fullPath);
                        }
                        else
                        {
                            //
                            DialogUtil.ShowInfo("Tải tất cả hồ sơ thành công.");
                        }
                    }
                
            }
        }

        private void GiayTo_XemTatCaTapTinController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<GiayToHoSo>();
        }
    }
}
