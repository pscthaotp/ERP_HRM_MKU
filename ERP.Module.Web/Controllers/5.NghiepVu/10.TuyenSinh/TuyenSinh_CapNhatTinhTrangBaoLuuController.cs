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
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_CapNhatTinhTrangBaoLuuController : ViewController
    {
        HoSoBaoLuu_NhapHocLai _nhapHocLai;
        IObjectSpace _obs;
        public TuyenSinh_CapNhatTinhTrangBaoLuuController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {

            _obs = Application.CreateObjectSpace();
            DetailView view = null;
            if (View.SelectedObjects.Count > 0)
            {
                _nhapHocLai = _obs.CreateObject<HoSoBaoLuu_NhapHocLai>();
                view = Application.CreateDetailView(_obs, _nhapHocLai);

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
            if (_nhapHocLai != null)
            {
                if (View.SelectedObjects != null)
                {
                    #region Thông tin khách hàng
                    foreach (var item in View.SelectedObjects)
                    {
                        var oid = item as HoSoBaoLuu;
                        HoSoBaoLuu hoSo = View.ObjectSpace.GetObjectByKey<HoSoBaoLuu>(oid.Oid);

                        if (hoSo != null && hoSo.TinhTrangHS.DaNghiHoc == false && hoSo.TinhTrangHS.TenTinhTrang.Contains("Bảo lưu"))
                        // cập nhật ngày vào học
                        {
                            hoSo.NgayKetThuc = _nhapHocLai.NgayKetThuc;
                            TinhTrangHS tt = View.ObjectSpace.FindObject<TinhTrangHS>(CriteriaOperator.Parse("TenTinhTrang like ? and DaNghiHoc = 0", "Đang học"));
                            if (tt != null)
                            {
                                hoSo.TinhTrangHS = tt;
                                hoSo.HocSinh.TinhTrangHS = tt;
                                hoSo.HocSinh.TuNgay = DateTime.MinValue;
                            }
                        }
                        View.ObjectSpace.CommitChanges();

                        SqlParameter[] param = new SqlParameter[1];
                        param[0] = new SqlParameter("@HoSoBaoLuu", hoSo.Oid);
                        DataProvider.ExecuteNonQuery("dbo.spd_BaoLuu_TinhLaiPhiKhiHocLai", System.Data.CommandType.StoredProcedure, param);
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
