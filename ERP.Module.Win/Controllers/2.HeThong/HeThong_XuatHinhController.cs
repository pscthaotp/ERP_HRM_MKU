using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.CauHinhChungs;
using System.Data;
using System.IO;
using ERP.Module.Extends;

namespace ERP.Module.Win.Controllers.HeThong
{
    public partial class HeThong_XuatHinhController : ViewController
    {
        private ChonDuLieuXuatHinh _chonDuLieu;
        private IObjectSpace _obs;
        //
        public HeThong_XuatHinhController()
        {
            InitializeComponent();
            RegisterActions(components);
            //
        }

        private void HeThong_XuatHinhController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = false;// Common.IsWriteGranted<CauHinhChung>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //
            _obs = Application.CreateObjectSpace();
            _chonDuLieu = _obs.CreateObject<ChonDuLieuXuatHinh>();
            e.View = Application.CreateDetailView(_obs, _chonDuLieu);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //
            if (_chonDuLieu != null)
            {
                string text = _chonDuLieu.Query;
                DataTable data = DataProvider.GetDataTable(text, CommandType.Text);
                //
                try
                {
                    using (DialogUtil.AutoWait())
                    {
                        foreach (DataRow item in data.Rows)
                        {

                            string nameFile = item["FileName"].ToString();
                            string url = _chonDuLieu.DuongDan + "\\" + nameFile + ".png";
                            byte[] imgBytes = (byte[])item["Value"];
                            //
                            if (imgBytes.Length > 0)
                            {
                                File.WriteAllBytes(url, imgBytes);

                            }
                        }
                    }
                    //
                    DialogUtil.ShowInfo("Xuất tập tin hình thành công !!!");
                }
                catch (Exception ex)
                {

                }
            }
        }
        //
    }
}
