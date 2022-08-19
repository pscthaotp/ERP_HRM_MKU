using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Extends;
using DevExpress.XtraBars;
using DevExpress.ExpressApp.Win.Controls;
using DevExpress.ExpressApp.Templates;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class HideToolbarController : ViewController
    {
        protected override void OnActivated()
        {
            base.OnActivated();
            Application.CustomizeTemplate += Application_CustomizeTemplate;
        }

        void Application_CustomizeTemplate(object sender, CustomizeTemplateEventArgs e)
        {
            ISupportActionsToolbarVisibility template = e.Template as ISupportActionsToolbarVisibility;
            if (e.Context == TemplateContext.NestedFrame &&
                template != null &&
                ( View.Id.Contains("ThuPhi_ListChiTietThuPhi_ListView")
                 || View.Id.Contains("ThuPhi_ListDetailDongPhucHocPham_ListView")
                 || View.Id.Contains("ThuPhi_ListDetailHocPhi_ListView")
                 || View.Id.Contains("ThuPhiLanDau_ListChiTietThuPhiLanDau_ListView")
                 || View.Id.Contains("ThuPhiLanDau_ListDetailDongPhucHocPham_ListView")
                 || View.Id.Contains("ThuPhiLanDau_ListDetailHocPhi_ListView")
                 //
                 || View.Id.Contains("HocSinhCustomView_LopList_ListView")
                 || View.Id.Contains("HocSinhCustomView_HocSinhList_ListView")
                 || View.Id.Contains("NhanSuCustomView_BoPhanList_ListView")
                 || View.Id.Contains("NhanSuCustomView_NhanSuList_ListView")

                 || View.Id.Contains("QuanLyHocSinh_NghiHoc_LopList_ListView")
                 || View.Id.Contains("QuanLyHocSinh_NghiHoc_HocSinhBaoLuuList_ListView")
                 || View.Id.Contains("QuanLyHocSinh_NghiHoc_HocSinhRaTruong_HuyHoSoList_ListView")

                 || View.Id.Contains("ThinhGiangCustomView_BoPhanList_ListView")
                 || View.Id.Contains("ThinhGiangCustomView_ThinhGiangList_ListView"))

                || View.Id.Contains("ReportCustomView_ReportViewList_ListView")
                || View.Id.Contains("HocPhi_DuThuPhi_HoSoNhapHoc_listHocSinh_ListView")
                || View.Id.Contains("HocPhi_DuThuPhi_HoSoNhapHoc_listDanhSachPhi_ListView")
                )
            {
                if (template != null) template.SetVisible(false);
            }
            else
            {
                if (template != null) template.SetVisible(true);
            }
        }

        protected override void OnDeactivated()
        {
            Application.CustomizeTemplate -= Application_CustomizeTemplate;
            base.OnDeactivated();
        }
    }

}
