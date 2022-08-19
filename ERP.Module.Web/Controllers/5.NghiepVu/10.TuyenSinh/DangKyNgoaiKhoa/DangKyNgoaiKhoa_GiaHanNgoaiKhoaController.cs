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
    public partial class DangKyNgoaiKhoa_GiaHanNgoaiKhoaController : ViewController
    {
        DangKyNgoaiKhoa_GiaHan _GiaHan;
        IObjectSpace _obs;
        public DangKyNgoaiKhoa_GiaHanNgoaiKhoaController()
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
                _GiaHan = _obs.CreateObject<DangKyNgoaiKhoa_GiaHan>();
                view = Application.CreateDetailView(_obs, _GiaHan);

                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
            else
                //DialogUtil.ShowInfo("Vui lòng chọn học sinh.");
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Vui lòng chọn học sinh.')");
        }
        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_GiaHan != null)
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
                            SqlParameter[] param = new SqlParameter[2];
                            param[0] = new SqlParameter("@ThangGiaHan", _GiaHan.GiaHanTu.SetTime(Enum.Systems.SetTimeEnum.StartMonth));
                            param[1] = new SqlParameter("@OidNgoaiKhoa", dangKyNgoaiKhoa.Oid);
                            object obj = DataProvider.GetValueFromDatabase("spd_DangKyNgoaiKhoa_GiaHanNgoaiKhoa", CommandType.StoredProcedure, param);
                            if (Convert.ToInt32(obj.ToString()) == 0)
                            {
                                //DialogUtil.ShowInfo("Gia hạn không thành công !!!");
                                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Gia hạn KHÔNG thành công !!!')");

                            }
                            else
                            {
                                //DialogUtil.ShowInfo("Gia hạn thành công !!!");
                                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Gia hạn thành công !!!')");

                            }
                            //View.Refresh();

                            //nếu là listview thì update lại lưới
                            ListView view = View as ListView;
                            if (view != null)
                                ObjectSpace.Refresh();
                        }
                        catch (Exception ex)
                        {
                            //DialogUtil.ShowInfo("Lỗi: " + ex.Message);
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Lỗi: " + ex.Message+"')");

                        }

                    }
                }
                    #endregion
            }
        }

        private void DangKyNgoaiKhoa_GiaHanNgoaiKhoaController_Activated(object sender, EventArgs e)
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
