using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
using ERP.Module.Extends;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.Web;
//
namespace ERP.Module.Win.Controllers.NghiepVu.TuyenSinh
{
    public partial class DangKyNgoaiKhoa_ChuyenLopNgoaiKhoa : ViewController
    {
        DangKyNgoaiKhoa_LopNgoaiKhoa _Lop;
        IObjectSpace _obs;
        public DangKyNgoaiKhoa_ChuyenLopNgoaiKhoa()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            if (View.SelectedObjects.Count > 0)
            {
                DetailView view;
                _Lop = _obs.CreateObject<DangKyNgoaiKhoa_LopNgoaiKhoa>();
                _Lop.DangKyNgoaiKhoa = _obs.GetObjectByKey<DangKyNgoaiKhoa>(((DangKyNgoaiKhoa)View.SelectedObjects[0]).Oid);
                view = Application.CreateDetailView(_obs, _Lop);

                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }
        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_Lop != null)
            {
                if (View.SelectedObjects != null)
                {
                    #region Cập nhật gia hạn
                    foreach (var item in View.SelectedObjects)
                    {
                        var oid = item as DangKyNgoaiKhoa;
                        DangKyNgoaiKhoa dangKyNgoaiKhoa = _obs.GetObjectByKey<DangKyNgoaiKhoa>(oid.Oid);

                        try
                        {
                            dangKyNgoaiKhoa.LopNgoaiKhoa = _Lop.LopNgoaiKhoa;
                            _obs.CommitChanges();
                            View.Refresh();
                            //DialogUtil.ShowInfo("Chuyển lớp ngoại khóa thành công!");
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chuyển lớp ngoại khóa thành công!')");
                            //nếu là listview thì update lại lưới
                            ListView view = View as ListView;
                            if (view != null)
                                ObjectSpace.Refresh();
                        }
                        catch (Exception ex)
                        {
                            //DialogUtil.ShowInfo("Lỗi: " + ex.Message);
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Lỗi:'"+ex.Message+")");
                        }

                    }
                }
                #endregion
            }
        }

        private void DangKyNgoaiKhoa_ChuyenLopNgoaiKhoa_Activated(object sender, EventArgs e)
        {
            if (View is DetailView)
            {
                #region DetailView
                if (View.Id.Equals("DangKyNgoaiKhoa_DetailView"))

                {
                    popupWindowShowAction1.Active["TruyCap"] = true;
                }
                else
                {
                    popupWindowShowAction1.Active["TruyCap"] = false;
                }
                #endregion
            }
            else
            {
                #region ListView
                if (View.Id.Equals("DangKyNgoaiKhoa_ListView"))
                {
                    popupWindowShowAction1.Active["TruyCap"] = true;
                }
                else
                {
                    popupWindowShowAction1.Active["TruyCap"] = false;
                }
                #endregion
            }

        }
    }
}
