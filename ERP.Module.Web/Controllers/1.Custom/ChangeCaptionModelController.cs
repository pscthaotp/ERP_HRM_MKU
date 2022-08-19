using System;
using DevExpress.ExpressApp;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Extends;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Web.Layout;
using DevExpress.Web;
using System.Web.UI;
using DevExpress.ExpressApp.Web;

namespace ERP.Module.Web.Controllers.Custom
{
    public partial class ChangeCaptionModelController : ViewController
    {
        public ChangeCaptionModelController()
        {
            InitializeComponent();
            //
            RegisterActions(components);
        }

        private void ChangeCaptionModelController_Activated(object sender, EventArgs e)
        {
            //Cài đặt detailview ở đây
            View.ControlsCreated += View_ControlsCreated;
            //if (View.Id.Equals("HoSoXetTuyen_ListView"))
            //ObjectSpace.Committed += new EventHandler(ObjectSpace_Committed);
        }

        private void View_ControlsCreated(object sender, EventArgs e)
        {
            string idObjectSpace = View.ObjectSpace.ToString();
            //
            if (View == null || idObjectSpace.Equals("DevExpress.ExpressApp.NonPersistentObjectSpace"))
                return;
            //
            XPObjectSpace objectSpace = (XPObjectSpace)View.ObjectSpace;
            if (objectSpace == null)
                return;
            //
            CongTy congTyHienTai = Common.CongTy(objectSpace.Session);
            //
            if (congTyHienTai != null)
            {
                if (congTyHienTai.Oid.Equals(Config.KeyTanPhu))
                {
                    //Trường phổ thông
                    PhoThong();
                }
                else
                {
                    //Trường mần non
                    ManNon();
                }
            }
        }

        void PhoThong()
        {
            if (View.Id.Contains("ThongTinKhachHang"))
            {
                if (!View.Id.Contains("ThongTinKhachHang_ListTre"))
                {
                    //Chưa cần - 
                    //Thay đổi caption of property
                    //ChangePropertyCaption("HoTen", "Họ tên mẹ");
                }
                //
                if (View.Id.Contains("ThongTinKhachHang_ListTre"))
                {
                    //Thay đổi caption of property
                    ChangePropertyCaption("NhomTuoi", "Khối dự kiến");
                }
                //Caption view
                ChangeGroupCaption("ThongTinKhachHang", "Thông tin phụ huynh");

                //  Thay đổi property caption
                ChangePropertyCaption("ListTre", "Danh sách học sinh");

                //Thay đổi caption của group
                ChangeGroupCaption("ListTre_Group", "Danh sách học sinh");

                View.Caption = "Thông tin phụ huynh";
            }
            //
            if (View.Id.Contains("DanhSachTre"))
            {
                //Thay đổi caption của group
                ChangeGroupCaption("DanhSachTre", "Danh sách học sinh");
                //Caption view
                View.Caption = "Danh sách học sinh";
            }
            if (View.Id.Contains("KeHoachTuyenSinh"))
            {
                if (View.Id.Contains("ChiTietKeHoachTuyenSinh_DetailView"))
                    ChangePropertyCaption("Thang", "Từ tháng");
            }
        }
        void ManNon()
        {
            if (View.Id.Contains("ThongTinKhachHang"))
            {
                //
                if (!View.Id.Contains("ThongTinKhachHang_ListTre"))
                {
                    ChangePropertyCaption("HoTen", "Họ tên");
                }
                //
                if (View.Id.Contains("ThongTinKhachHang_ListTre"))
                {
                    //Thay đổi caption of property
                    ChangePropertyCaption("NhomTuoi", "Nhóm tuổi");
                }
                //Thay đổi caption của group
                ChangeGroupCaption("ListTre_Group", "Danh sách trẻ");
                //Caption view
                ChangeGroupCaption("ThongTinKhachHang", "Thông tin khách hàng");

                //  Thay đổi property caption
                ChangePropertyCaption("ListTre", "Danh sách trẻ");
                //
                View.Caption = "Thông tin khách hàng";

                //Custom "ké" ẩn 1 group trong ThongTinKhachHang_DetailView có id = ThongTinLLK
                // Visible Group layout Detail_view
                if (View.Id.Contains("ThongTinKhachHang_DetailView"))
                    VisibleGroupDetailView("ThongTinLLK");
            }
            //
            if (View.Id.Contains("DanhSachTre"))
            {
                //Thay đổi caption của group
                ChangeGroupCaption("DanhSachTre", "Danh sách trẻ");
                //Caption view
                View.Caption = "Danh sách trẻ";
            }
            //
        }

        void ChangePropertyCaption(string propertyName, string propertyCaption)
        {
            /* Lưu ý chỉ có tác dụng đổi với Detailview */

            //
            var property = View.Model.AsObjectView.ModelClass.FindMember(propertyName);
            if (property != null)
            {
                property.Caption = propertyCaption;
            }
        }

        void ChangeGroupCaption(string groupName, string groupCaption)
        {
            //
            DetailView detailView = View as DetailView;
            if (detailView == null)
                return;
            //
            foreach (var group in ((WebLayoutManager)((DetailView)View).LayoutManager).Items)
            {
                //
                if (group.Value.Model.Id.Equals(groupName))
                {
                    ((IModelLayoutGroup)((LayoutGroupTemplateContainer)group.Value).Model).Caption = groupCaption;
                }
            }
        }

        void VisibleGroupDetailView(string groupName)
        {
            //
            DetailView detailView = View as DetailView;
            if (detailView == null)
                return;
            //
            foreach (var group in ((WebLayoutManager)((DetailView)View).LayoutManager).Items)
            {
                //
                if (group.Value.Model.Id.Equals("Main"))
                {
                    foreach (var item in group.Value.LayoutManager.Items)
                    {
                        if (item.Value.Model.Id.Equals(groupName))
                        {
                            item.Value.Visible = false;
                        }
                    }
                }
            }
        }

        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            ASPxPopupControl popup = new ASPxPopupControl();
            popup.ID = popup.ClientInstanceName = popup.HeaderText = "Nhắc nhở";
            popup.Modal = true;
            popup.AllowDragging = true;
            ((Control)View.Control).Controls.Add(popup);
            WebWindow.CurrentRequestWindow.RegisterStartupScript("showPopup", "popup.Show();");
        }
    }
}
