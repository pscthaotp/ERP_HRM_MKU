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
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_ChonHocSinhController : ViewController
    {
        private ThongBaoNhapHoc_Mau _thongBao;
        private TuyenSinh_ChonHocSinh _chonHocSinh;

        public TuyenSinh_ChonHocSinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            #region 1. Hồ sơ nhập học
            if (View.Id.Equals("ThongBaoNhapHoc_Mau_DetailView"))
            {
                _thongBao = View.CurrentObject as ThongBaoNhapHoc_Mau;
                if (_thongBao != null)
                {
                    //
                    IObjectSpace obs = Application.CreateObjectSpace();
                    //
                    _chonHocSinh = obs.CreateObject<TuyenSinh_ChonHocSinh>();
                    DetailView view = Application.CreateDetailView(obs, _chonHocSinh);
                    view.ViewEditMode = ViewEditMode.Edit;
                    e.View = view;
                }
            }
            #endregion
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            #region 1. Hồ sơ nhập học
            if (View.Id.Equals("ThongBaoNhapHoc_Mau_DetailView"))
            //
            {
                if (_chonHocSinh != null)
                {
                    if (_thongBao != null)
                    {
                        Module.NghiepVu.HocSinh.HocSinhs.HocSinh hs = View.ObjectSpace.GetObjectByKey<Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(_chonHocSinh.HocSinh.Oid);
                        if(hs != null)
                        {
                            _thongBao.HocSinh = hs;
                            _thongBao.Email = hs.EmailCha != string.Empty ? hs.EmailCha : hs.EmailMe;
                            _thongBao.NamHoc = View.ObjectSpace.GetObjectByKey<NamHoc>(hs.NamHoc.Oid);
                            _thongBao.NoiDung = ReplaceThongTin(_thongBao.NoiDung,hs.HoTen,hs.NgaySinh.ToString("dd/MM/yyyy"),hs.NgayVaoHoc.ToString("dd/MM/yyyy"),hs.Lop != null ? hs.Lop.TenLop : string.Empty);
                            //
                            View.Refresh();
                        }
                    }
                }
            }
            #endregion

        }

        string ReplaceThongTin(string noiDung,string hoTen,string ngaySinh,string ngayVaoHoc,string lop)
        {
            string ketqua = "";
            if (!string.IsNullOrEmpty(noiDung))
            {
                ketqua = noiDung.Replace("[HoTenHocSinh]", hoTen);
                ketqua = ketqua.Replace("[NgaySinh]", ngaySinh);
                ketqua = ketqua.Replace("[NgayNhapHoc]", ngayVaoHoc);
                ketqua = ketqua.Replace("[LopHoc]", lop);
            }
            //
            return ketqua;
        }

        private void TuyenSinh_ChonHocSinhController_Activated(object sender, EventArgs e)
        {      
                #region DetailView
                if (View.Id.Equals("ThongBaoNhapHoc_Mau_DetailView"))
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
