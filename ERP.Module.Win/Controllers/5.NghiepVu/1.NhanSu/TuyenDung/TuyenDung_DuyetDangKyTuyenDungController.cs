using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Commons;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using ERP.Module.NonPersistentObjects.NhanSu;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Extends;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    public partial class TuyenDung_DuyetDangKyTuyenDungController : ViewController
    {
        TuyenDung_DuyetDangKyTuyenDung _chonDangKyTuyenDung = null;
        IObjectSpace _obs = null;
        QuanLyTuyenDung _quanLyTuyenDungCurrent = null;

        public TuyenDung_DuyetDangKyTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void ChuyenHoSoTuyenDungAction_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<QuanLyTuyenDung>() && Common.IsWriteGranted<NhuCauTuyenDung>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //
            _quanLyTuyenDungCurrent = View.CurrentObject as QuanLyTuyenDung;
            //
            if (_quanLyTuyenDungCurrent != null)
            {
                //View.ObjectSpace.CommitChanges();
                _obs = Application.CreateObjectSpace();
                //
                _chonDangKyTuyenDung = _obs.CreateObject<TuyenDung_DuyetDangKyTuyenDung>();
                _chonDangKyTuyenDung.GetDangKyTuyenDungAll(_quanLyTuyenDungCurrent);
                e.View = Application.CreateDetailView(_obs, _chonDangKyTuyenDung);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (_chonDangKyTuyenDung != null && _chonDangKyTuyenDung.DuyetDangKyTuyenDungList.Count > 0)
            {
                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)_obs).Session.DataLayer))
                {
                    uow.BeginTransaction();
                    //
                    try
                    {
                        using (DialogUtil.AutoWait())
                        {
                            foreach (var item in _chonDangKyTuyenDung.DuyetDangKyTuyenDungList)
                            {
                                if (item.Chon)
                                {
                                    DangKyTuyenDung dangKyTuyenDung = uow.GetObjectByKey<DangKyTuyenDung>(item.DangKyTuyenDung.Oid);
                                    //
                                    QuanLyTuyenDung quanLyTuyenDungCopy = uow.GetObjectByKey<QuanLyTuyenDung>(item.QuanLyTuyenDung.Oid);
                                    //
                                    NhuCauTuyenDung nhuCau = new NhuCauTuyenDung(uow);
                                    nhuCau.QuanLyTuyenDung = dangKyTuyenDung.QuanLyTuyenDung;
                                    nhuCau.ViTriTuyenDung = dangKyTuyenDung.ViTriTuyenDung;
                                    nhuCau.BoPhan = dangKyTuyenDung.BoPhan;
                                    nhuCau.SoLuongTuyen = dangKyTuyenDung.SoLuongTuyen;
                                    // Add vào list đăng ký được duyệt
                                    quanLyTuyenDungCopy.ListNhuCauTuyenDung.Add(nhuCau);
                                    // Cập nhật trạng thái của đăng ký tuyển dụng
                                    dangKyTuyenDung.Duyet = true;
                                }
                            }
                        }

                        //Lưu dữ liệu nếu thành công
                        uow.CommitChanges();
                        View.ObjectSpace.Refresh();
                        //
                        DialogUtil.ShowInfo("Duyệt đăng ký tuyển dụng thành công.");
                    }
                    catch (Exception ex) { DialogUtil.ShowError("Xảy ra lỗi trong quá trình copy dữ liệu: " + ex.Message); return; }
                }
            }
        }
    }
}
