using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using ERP.Module.NonPersistentObjects.HocSinh;
using DevExpress.Web;
using DevExpress.ExpressApp.Web;

namespace ERP.Module.Web.Controllers.NghiepVu.HocSinh
{
    public partial class TinhTrang_TimKiemHocSinhController : ViewController
    {
        public TinhTrang_TimKiemHocSinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TinhTrang_TimKiemHocSinhController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;
            TinhTrang_CapNhatTinhTrang timKiem = View.CurrentObject as TinhTrang_CapNhatTinhTrang;
            //
            if (view != null)
            {
                ControlViewItem item = ((DetailView)View).FindItem("btnSearch") as ControlViewItem;
                //
                if (item != null)
                {
                    //
                    if (item.Control == null)
                    {
                        item.ControlCreated += TinhTrang_TimKiemHocSinhController_ViewControlsCreated;
                    }

                    ASPxButton btnSearch = item.Control as ASPxButton;
                    if (btnSearch != null)
                    {
                        btnSearch.Text = "Tìm kiếm";
                        btnSearch.Width = 80;
                        btnSearch.Click += (obj, ea) =>
                        {
                            using (DialogUtil.AutoWait())
                            {
                                if (view != null)
                                {
                                    if (!timKiem.HuyBaoLuu_NhapHocLai && !timKiem.HuyBaoLuu_ThoiHoc 
                                        && !timKiem.NhapHocLai_BaoLuu && !timKiem.NhapHocLai_ThoiHoc 
                                        && !timKiem.HuyHoSo)
                                        WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Vui lòng chọn loại tìm kiếm!')");
                                    else
                                        timKiem.Load_TimKiem();
                                }
                                View.Refresh();
                            }
                        };
                    }
                }
            }
        }
    }
}
