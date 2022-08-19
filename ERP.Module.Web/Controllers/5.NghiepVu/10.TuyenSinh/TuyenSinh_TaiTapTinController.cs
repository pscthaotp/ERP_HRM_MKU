using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.IO;
using DevExpress.ExpressApp.Web;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;

namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_TaiTapTinController : ViewController
    {
        IObjectSpace _obs;
        HoSoNhapHoc _hoSoNhapHoc;
        TuyenSinh_ChonTapTin _obj;
        //
        public TuyenSinh_TaiTapTinController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            //
            _hoSoNhapHoc = View.CurrentObject as HoSoNhapHoc;
            if (_hoSoNhapHoc != null)
            {
                //
                _obs = Application.CreateObjectSpace();
                _obj = _obs.CreateObject<TuyenSinh_ChonTapTin>();
                //
                DetailView detailView = Application.CreateDetailView(_obs, _obj);
                detailView.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                e.Context = TemplateContext.PopupWindow;
                e.View = detailView;
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            bool sucess = false;

            //1. Hồ sơ nhập học
            if (_hoSoNhapHoc != null)
            {
                if (_obj != null && _obj.File != null && _obj.File.Content != null)
                {
                    //
                    string filepath = string.Format("{0}/{1}{2}", Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.URL_HoSoNhapHoc, Guid.NewGuid(), Path.GetExtension(_obj.File.FileName));
                    //
                    if (UploadFileLargeSize(filepath, _obj.File.Content))
                    {
                        // Nếu upload thành công thì lưu dữ liệu lại
                        HoSoNhapHoc_TapTin tapTin = View.ObjectSpace.CreateObject<HoSoNhapHoc_TapTin>();
                        tapTin.HoSoNhapHoc = View.ObjectSpace.GetObjectByKey<HoSoNhapHoc>(_hoSoNhapHoc.Oid);
                        tapTin.TenTapTin = _obj.File.FileName;
                        tapTin.NgayCapNhat = DateTime.Now;
                        tapTin.DuongDanTapTin = filepath;
                        _hoSoNhapHoc.TapTinList.Add(tapTin);
                        //
                        sucess = true;
                    }
                }
            }

            //Thông báo
            if (sucess)
            {
                //
                View.ObjectSpace.CommitChanges();
                View.Refresh();

                //
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Tải tập tin thành công !!!')");
            }
            else
            {
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Tải tập tin thất bại !!!')");
            }
        }

        private void TuyenSinh_TaiTapTinController_Activated(object sender, EventArgs e)
        {
            //
            if (View.Id.Equals("HoSoNhapHoc_DetailView")
                )
            {
                popupWindowShowAction1.Active["TruyCap"] = true;
            }
            else
                popupWindowShowAction1.Active["TruyCap"] = false;
        }

        public bool UploadFileLargeSize(string filepath, byte[] buffer)
        {
            try
            {
                File.Create(filepath).Close();
                //
                using (FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
                {
                    fs.Write(buffer, 0, buffer.Length);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
