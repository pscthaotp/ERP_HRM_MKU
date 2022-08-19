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
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_HuyHoSoBaoLuuController : ViewController
    {
        HoSoBaoLuu_HuyHoSo _huyHoSo;
        IObjectSpace _obs;
        public TuyenSinh_HuyHoSoBaoLuuController()
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
                _huyHoSo = _obs.CreateObject<HoSoBaoLuu_HuyHoSo>();
                view = Application.CreateDetailView(_obs, _huyHoSo);
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
            if (_huyHoSo != null)
            {
                if (View.SelectedObjects != null)
                {
                    #region Thông tin khách hàng
                    foreach (var item in View.SelectedObjects)
                    {
                        var oid = item as HoSoBaoLuu;
                        HoSoBaoLuu hoSo = _obs.GetObjectByKey<HoSoBaoLuu>(oid.Oid);
                        TinhTrangHS ttDangHoc = _obs.FindObject<TinhTrangHS>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang học"));

                        //if (hoSo.TinhTrangHS != ttDangHoc)
                        //{
                            try
                            {
                                hoSo.LoaiHuy = Enum.TuyenSinh.HuyBaoLuuEnum.Huy_NhapHocLai;
                                hoSo.NgayHuy = DateTime.Now;
                                hoSo.TinhTrangHS = ttDangHoc;
                                hoSo.LyDoHuy = _huyHoSo.LyDoHuy;
                                hoSo.HocSinh.TinhTrangHS = ttDangHoc;
                                hoSo.HocSinh.GhiChu = string.Empty;

                                BaoLuu baoLuu = _obs.FindObject<BaoLuu>(CriteriaOperator.Parse("HoSoBaoLuu = ?", hoSo.Oid));
                                if (baoLuu != null)
                                    baoLuu.HuyBaoLuu = true;

                                SqlParameter[] param = new SqlParameter[1];
                                param[0] = new SqlParameter("@Oid", hoSo.Oid);
                                DataProvider.ExecuteNonQuery("spd_BaoLuu_HuyHoSoBaoLuu", System.Data.CommandType.StoredProcedure, param);

                                //View.ObjectSpace.CommitChanges();
                                _obs.CommitChanges();
                                View.Refresh();

                                //nếu là listview thì update lại lưới
                                ListView view = View as ListView;
                                if (view != null)
                                    ObjectSpace.Refresh();

                                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Đã hủy Hồ sơ bảo lưu thành công !!!')");
                            }
                            catch (Exception ex)
                            {
                                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Lỗi: " + ex.Message + "')");
                            }
                        //}
                    }
                }
                #endregion
            }
        }

        private void TuyenSinh_ChonTreController_Activated(object sender, EventArgs e)
        {
            if (View is DetailView)
            {
                #region DetailView
                if (View.Id.Equals("HoSoBaoLuu_DetailView"))

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
                if (View.Id.Equals("HoSoBaoLuu_ListView"))
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
