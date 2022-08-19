using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using System.Data.SqlClient;
using DevExpress.Utils;
using System.Data;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.NonPersistentObjects.DanhMuc;
using System.Text;

namespace ERP.Module.Win.Controllers.DanhMuc
{
    public partial class DanhMuc_CopyNgayNghiTrongNamController : ViewController
    {
        IObjectSpace _obs;
        DanhMuc_ChonLoaiNgayNghi _source;

        public DanhMuc_CopyNgayNghiTrongNamController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void DanhMuc_CopyNgayNghiTrongNamController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = (Common.IsWriteGranted<CC_NgayNghiTrongNam>()
                                                        && (Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriCongTy
                                                            || Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriHeThong
                                                            || Common.SecuritySystemUser_GetCurrentUser().LoaiTaiKhoan == Enum.Systems.LoaiTaiKhoanEnum.QuanTriKhoi));
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _source = _obs.CreateObject<DanhMuc_ChonLoaiNgayNghi>();
            e.View = Application.CreateDetailView(_obs, _source);
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_source != null)
            {
                using (DialogUtil.AutoWait())
                {
                    List<string> listBP = new List<string>();
                    //
                    if (_source.TatCa)
                        listBP = Common.Department_GetRoledDepartmentList_ByCurrentUser();
                    else
                        listBP = Common.Department_GetRoledDepartmentList_ByDepartment(_source.BoPhan);
                    //
                    StringBuilder roled = new StringBuilder();
                    foreach (string item in listBP)
                    {
                        roled.Append(String.Format("{0};", item));
                    }

                    int loaiNgayNghi = 0;
                    if (_source.LoaiNgayNghi == Enum.NhanSu.LoaiNgayNghiEnum.NghiBu)
                        loaiNgayNghi = 1;
                    else if (_source.LoaiNgayNghi == Enum.NhanSu.LoaiNgayNghiEnum.NghiCheDo)
                        loaiNgayNghi = 2;
                    else if (_source.LoaiNgayNghi == Enum.NhanSu.LoaiNgayNghiEnum.NghiCuoiTuan)
                        loaiNgayNghi = 3;
                    else if (_source.LoaiNgayNghi == Enum.NhanSu.LoaiNgayNghiEnum.NgayThuong)
                        loaiNgayNghi = 4;

                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@CongTy", _source.CongTy.Oid);
                    param[1] = new SqlParameter("@BoPhan", roled.ToString());
                    param[2] = new SqlParameter("@LoaiNgayNghi", loaiNgayNghi);
                    param[3] = new SqlParameter("@NguoiTao", Common.SecuritySystemUser_GetCurrentUser().Oid);
                    //
                    int sucess = DataProvider.ExecuteNonQuery("spd_DanhMuc_CopyNgayNghiTrongNam", CommandType.StoredProcedure, param);
                    //
                    if (sucess != 1000)
                    {
                        View.ObjectSpace.Refresh();
                        View.Refresh();
                        //
                        DialogUtil.ShowInfo("Copy thành công.");
                    }
                    else
                        DialogUtil.ShowError("Copy thất bại.");
                    //
                }              
            }
        }
    }
}
