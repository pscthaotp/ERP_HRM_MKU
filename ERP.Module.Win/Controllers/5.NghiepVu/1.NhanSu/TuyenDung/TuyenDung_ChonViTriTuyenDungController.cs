using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Commons;
using System.Windows.Forms;
using ERP.Module.NonPersistentObjects.NhanSu;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Extends;
using System.Collections.Generic;
using ERP.Module.NghiepVu.NhanSu.DinhBien;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    public partial class TuyenDung_ChonViTriTuyenDungController : ViewController
    {
        IObjectSpace _obs = null;
        TuyenDung_ChonViTriTuyenDung _chonViTriTuyenDung = null;
        QuanLyTuyenDung _qlTuyenDungCurrent = null;

        public TuyenDung_ChonViTriTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChuyenHoSoTuyenDungAction_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<ViTriTuyenDung>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            _obs = Application.CreateObjectSpace();
            _qlTuyenDungCurrent = View.CurrentObject as QuanLyTuyenDung;
            Session session = ((XPObjectSpace)_obs).Session;
            if (_qlTuyenDungCurrent != null)
            {
                CriteriaOperator filter = CriteriaOperator.Parse("QuanLyTuyenDung=?", _qlTuyenDungCurrent.Oid);
                List<string> maQuanLy = new List<string>();
                using (XPCollection<ViTriTuyenDung> vtList = new XPCollection<ViTriTuyenDung>(session, filter))
                {
                    foreach (ViTriTuyenDung item in vtList)
                    {
                        maQuanLy.Add(item.MaQuanLy);
                    }
                }
                _chonViTriTuyenDung = _obs.CreateObject<TuyenDung_ChonViTriTuyenDung>();
                _chonViTriTuyenDung.GetDanhSachViTriTuyenDungAll(_qlTuyenDungCurrent, maQuanLy);
                e.View = Application.CreateDetailView(_obs, _chonViTriTuyenDung);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_chonViTriTuyenDung != null && _chonViTriTuyenDung.ChonViTriTuyenDungList.Count > 0)
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)_obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    //
                    try
                    {
                        using (DialogUtil.AutoWait())
                        {
                            foreach (var item in _chonViTriTuyenDung.ChonViTriTuyenDungList)
                            {
                                if (item.Chon)
                                {
                                    DinhBienChucDanh dinhBienSelect = uow.GetObjectByKey<DinhBienChucDanh>(item.DinhBienChucDanh.Oid);
                                    QuanLyTuyenDung quanLyTuyenDungCopy = uow.GetObjectByKey<QuanLyTuyenDung>(_qlTuyenDungCurrent.Oid);
                                    //
                                    ViTriTuyenDung viTri = new ViTriTuyenDung(uow);
                                    viTri.MaQuanLy = dinhBienSelect.ChucDanh.MaQuanLy;
                                    viTri.QuanLyTuyenDung = quanLyTuyenDungCopy;
                                    viTri.BoPhan = dinhBienSelect.BoPhan;
                                    viTri.ChucVu = dinhBienSelect.ChucVu;
                                    viTri.ChucDanh = dinhBienSelect.ChucDanh;
                                    viTri.TenViTriTuyenDung = dinhBienSelect.ChucDanh.TenChucDanh;
                                    // Add vào list đăng ký được duyệt
                                    quanLyTuyenDungCopy.ListViTriTuyenDung.Add(viTri);
                                }
                            }
                        }

                        //Lưu dữ liệu nếu thành công
                        uow.CommitChanges();
                        View.ObjectSpace.Refresh();
                        //
                        DialogUtil.ShowInfo("Chọn vị trí tuyển dụng thành công.");
                    }
                    catch (Exception ex) { DialogUtil.ShowError("Xảy ra lỗi trong quá trình copy dữ liệu: " + ex.Message); return; }
                }
            }
        }
    }
}
