using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Xpo;
using System.Collections;
using ERP.Module.DanhMuc.HocSinh;
using ERP.Module.NghiepVu.HocPhi.BaoLuu;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
using System.Data;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_NgungGiaHanDangKyNgoaiKhoaController : ViewController
    {
        DangKyNgoaiKhoa_NgungGiaHan _ngungGiaHan;
        IObjectSpace _obs;
        public TuyenSinh_NgungGiaHanDangKyNgoaiKhoaController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            DetailView view;
            _obs = Application.CreateObjectSpace();
            if (View.SelectedObjects.Count > 0)
            {
                _ngungGiaHan = _obs.CreateObject<DangKyNgoaiKhoa_NgungGiaHan>();
                view = Application.CreateDetailView(_obs, _ngungGiaHan);
            }
            else
            {
                XuatLoiNghiepVuTuyenSinh thongbaoloi = _obs.CreateObject<XuatLoiNghiepVuTuyenSinh>();
                thongbaoloi.ThongBao = "Vui lòng chọn học sinh.";
                view = Application.CreateDetailView(_obs, thongbaoloi);
            }

            view.ViewEditMode = ViewEditMode.Edit;
            e.View = view;            
        }
        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_ngungGiaHan != null)
            {
                if (View.SelectedObjects != null)
                {
                    #region Ngừng gia hạn
                    foreach (var item in View.SelectedObjects)
                    {
                        var oid = item as DangKyNgoaiKhoa;
                        DangKyNgoaiKhoa dangKyNgoaiKhoa = _obs.GetObjectByKey<DangKyNgoaiKhoa>(oid.Oid);

                        try
                        {
                            dangKyNgoaiKhoa.NgungGiaHanTu = _ngungGiaHan.NgungGiaHanTu.SetTime(Enum.Systems.SetTimeEnum.StartMonth);

                            HocSinhDichVu hocSinhDichVu = _obs.FindObject<HocSinhDichVu>(CriteriaOperator.Parse("HocSinh = ? and LoaiPhi = ? and Thang >= ?", 
                                                            dangKyNgoaiKhoa.HocSinh.Oid, dangKyNgoaiKhoa.LoaiPhi.Oid, 
                                                            _ngungGiaHan.NgungGiaHanTu.SetTime(Enum.Systems.SetTimeEnum.StartMonth)));
                            if (hocSinhDichVu != null)
                                hocSinhDichVu.NgungGiaHan = true;

                            //View.ObjectSpace.CommitChanges();
                            _obs.CommitChanges();
                            View.Refresh();

                            SqlParameter[] param = new SqlParameter[1];
                            param[0] = new SqlParameter("@DangKyNgoaiKhoa", dangKyNgoaiKhoa.Oid);
                            DataProvider.ExecuteNonQuery("spd_NgoaiKhoa_XoaCongNo", CommandType.StoredProcedure, param);

                            //nếu là listview thì update lại lưới
                            ListView view = View as ListView;
                            if (view != null)
                                ObjectSpace.Refresh();

                            WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Đã ngưng gia hạn thành công !!!')");
                        }
                        catch (Exception ex)
                        {
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Lỗi: " + ex.Message + "')");
                        }

                    }
                }
                #endregion
            }
        }

        private void TuyenSinh_NgungGiaHanDangKyNgoaiKhoaController_Activated(object sender, EventArgs e)
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
