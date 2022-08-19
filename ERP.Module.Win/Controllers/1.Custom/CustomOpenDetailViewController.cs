using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Windows.Forms;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace ERP.Module.Win.Controllers.Custom
{
    public partial class CustomOpenDetailViewController : ViewController
    {

        public CustomOpenDetailViewController()
        {
            InitializeComponent();
            TargetViewId = "TimKiem_NgoaiKhoa_ListDangKyNgoaiKhoa_ListView;"
                            + "QuanLyCTKhung_CTKhungList_ListView;"
                            + "QuanLyCTKhung_CTKhung_NDCSList_ListView;"
                            + "CTKhung_DuyetCTKhung_ChitTietDuyetCTKhungList_ListView;"
                            + "HocSinhCustomView_HocSinhList_ListView;"
                            + "NhanSuCustomView_NhanSuList_ListView;"
                            + "ThinhGiangCustomView_ThinhGiangList_ListView;"                          
                            + "QuanLyDichVuBus_ListChiTietDangKyBus_ListView;"
                            + "DangKyNgoaiKhoa_QuanLyNon_ListDangKyNgoaiKhoa_ListView;"
                            + "PhieuThu_QuanLyPhieuThu_ListDanhSachPhieuThu_ListView;"
                            + "QuanLyLopNgoaiKhoa_ListLopNgoaiKhoa_ListView;"
                            + "QuanLyHocSinhChinhSach_listChinhSach_ListView;"
                            + "QuanLyHocSinh_NghiHoc_HocSinhBaoLuuList_ListView;"
                            + "QuanLyHocSinh_NghiHoc_HocSinhRaTruong_HuyHoSoList_ListView;"
                            + "CongNoMax_HuyChiTiet_listChiTietCongNo_HuyCongNoHangLoat_ListView;"
                            + "QuanLySoNhanThuocNon_ListDanhSach_ListView;"
                            + "TaiKhoan_TimWeb_MamNon_WebUsers_ListWeb_MamNon_WebUsers_ListView";
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            //Hàm này dùng để khi double click vào lưới (nói nôm na là từ ListView gọi sang DetailView)
            ListViewProcessCurrentObjectController processCurrentObjectController = Frame.GetController<ListViewProcessCurrentObjectController>();
            if (processCurrentObjectController != null)
            {
                processCurrentObjectController.ProcessCurrentObjectAction.Execute += ProcessCurrentObjectAction_Execute;
            }
        }

        void ProcessCurrentObjectAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Kiểm tra các trường hợp đặc biệt chia ra 2 trường hợp
            //1. Mở detailview theo đúng class của list theo OID
            //2. Mở detailview class khác không giống với list đang hiển thị -> ngoài OID còn phải có cột ClassName

            IObjectSpace obs = Application.CreateObjectSpace();
            Session _ses = ((XPObjectSpace)obs).Session;
            
            if (View.Id == "TimKiem_NgoaiKhoa_ListDangKyNgoaiKhoa_ListView"
                    || View.Id == "HocSinhCustomView_HocSinhList_ListView"
                    || View.Id == "NhanSuCustomView_NhanSuList_ListView"
                    || View.Id == "ThinhGiangCustomView_ThinhGiangList_ListView"
                    || View.Id == "CTKhung_DuyetCTKhung_ChitTietDuyetCTKhungList_ListView"
                    || View.Id == "PhieuThu_QuanLyPhieuThu_ListDanhSachPhieuThu_ListView"
                    || View.Id == "DangKyNgoaiKhoa_QuanLyNon_ListDangKyNgoaiKhoa_ListView"
                    || View.Id == "QuanLyLopNgoaiKhoa_ListLopNgoaiKhoa_ListView"
                    || View.Id == "QuanLyHocSinhChinhSach_listChinhSach_ListView"
                    || View.Id == "QuanLyHocSinh_NghiHoc_HocSinhBaoLuuList_ListView"
                    || View.Id == "QuanLyHocSinh_NghiHoc_HocSinhRaTruong_HuyHoSoList_ListView"
                    || View.Id == "CongNoMax_HuyChiTiet_listChiTietCongNo_HuyCongNoHangLoat_ListView"
                    || View.Id == "QuanLySoNhanThuocNon_ListDanhSach_ListView"
                    || View.Id == "TaiKhoan_TimWeb_MamNon_WebUsers_ListWeb_MamNon_WebUsers_ListView")
            {
                //Lấy kiểu đổi tượng (type) của detailview muốn mở dựa vào cột ClassName
                var i = e.CurrentObject.GetType().GetProperty("ClassName").GetValue(e.CurrentObject);
                Type objecttype = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).FirstOrDefault(x => x.Name == i.ToString());

                //Lấy object theo kiểu đối tượng ở code trên và OID
                object _object = _ses.GetObjectByKey(objecttype, obs.GetKeyValue(e.CurrentObject));

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, _object);
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
            else
            {
                object _object = _ses.GetObjectByKey(e.CurrentObject.GetType(), obs.GetKeyValue(e.CurrentObject));

                e.ShowViewParameters.CreatedView = Application.CreateDetailView(obs, _object);
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
            }
        }
    }
}