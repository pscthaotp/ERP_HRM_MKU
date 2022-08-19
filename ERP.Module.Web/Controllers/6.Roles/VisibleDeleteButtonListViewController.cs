using System;
using System.Collections.Generic;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Web.SystemModule;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NghiepVu.HocSinh.HoSoNhapHocs;
using ERP.Module.NghiepVu.TuyenSinh_TP;

namespace ERP.Module.Web.Controllers.Roles
{
    public partial class VisibleDeleteButtonListViewController : WebDeleteObjectsViewController
    {
        public VisibleDeleteButtonListViewController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void UpdateActionState()
        {
            //
            DeleteAction.BeginUpdate();

            try
            {
                base.UpdateActionState();
                string objectSpace = View.ObjectSpace.ToString();
                //
                if (View == null || objectSpace.Equals("DevExpress.ExpressApp.NonPersistentObjectSpace"))
                    return;

                bool enable = true;
                Session session = ((XPObjectSpace)View.ObjectSpace).Session;

                #region Tuyển sinh
                if (View.ObjectTypeInfo.FullName.Contains("TuyenSinh"))
                {
                    #region 0. Kế hoạch tuyển sinh
                    if (View.ObjectTypeInfo.Name == "KeHoachTuyenSinh")
                    {
                        KeHoachTuyenSinh keHoach = View.CurrentObject as KeHoachTuyenSinh;
                        if (keHoach != null)
                        {
                            CriteriaOperator filter = CriteriaOperator.Parse("KeHoachTuyenSinh=? and TrangThai!=1 and TrangThai!=5", keHoach.Oid);
                            XPCollection<ChiTietKeHoachTuyenSinh> chiTietList = new XPCollection<ChiTietKeHoachTuyenSinh>(session, filter);
                            if (chiTietList.Count > 0)
                                enable = false;
                        }
                        //
                    }
                    if (View.ObjectTypeInfo.Name == "ChiTietKeHoachTuyenSinh")
                    {
                        ChiTietKeHoachTuyenSinh chiTiet = View.CurrentObject as ChiTietKeHoachTuyenSinh;
                        if (chiTiet != null && chiTiet.TrangThai != Enum.TuyenSinh.TrangThaiKeHoachEnum.ChuaDuyet && chiTiet.TrangThai != Enum.TuyenSinh.TrangThaiKeHoachEnum.KhongXacDinh)
                        {
                            enable = false;
                        }
                        //
                    }
                    #endregion

                    #region 1. Thông báo tuyển sinh
                    if (View.ObjectTypeInfo.Name == "ThongBaoTuyenSinh")
                    {
                        ThongBaoTuyenSinh thongBao = View.CurrentObject as ThongBaoTuyenSinh;
                        if (thongBao != null && thongBao.DaDuyet)
                        {
                            enable = false;
                        }
                        //
                    }
                    #endregion

                    #region 2. Kiểm tra IQ
                    if (View.ObjectTypeInfo.Name == "KiemTraIQ")
                    {
                        KiemTraIQ kiemTraIQ = View.CurrentObject as KiemTraIQ;
                        if (kiemTraIQ != null && kiemTraIQ.DaCoHoSoNhapHoc)
                            enable = false;
                        //
                    }
                    #endregion

                    #region 3. Hồ sơ nhập học
                    if (View.ObjectTypeInfo.Name == "HoSoNhapHoc")
                    {
                        HoSoNhapHoc hoSoNhapHoc = View.CurrentObject as HoSoNhapHoc;
                        if (hoSoNhapHoc != null && (hoSoNhapHoc.DaDongHocPhi))
                            enable = false;
                        //
                    }
                    #endregion

                    #region 4. Tổ chức sự kiện
                    if (View.ObjectTypeInfo.Name == "ToChucSuKien")
                    {
                        ToChucSuKien toChucSuKien = View.CurrentObject as ToChucSuKien;
                        if (toChucSuKien != null && (toChucSuKien.DaDuyet))
                            enable = false;
                        //
                    }
                    #endregion

                    #region 5. Hồ sơ xét tuyển - tân phú
                    if (View.ObjectTypeInfo.Name == "HoSoXetTuyen")
                    {
                        HoSoXetTuyen hoSoXetTuyen = View.CurrentObject as HoSoXetTuyen;
                        if (hoSoXetTuyen != null && (hoSoXetTuyen.DaDongHocPhi && hoSoXetTuyen.DaNhapHoc))
                            enable = false;
                        //
                    }
                    #endregion

                    //Vô hiệu hóa
                    DeleteAction.Active["ViewAllowDelete"] = enable;
                }
                #endregion
            }
            finally
            {
                DeleteAction.EndUpdate();
            }
        }
    }
}
