using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.XtraEditors;
using DevExpress.ExpressApp.Security;
using DevExpress.Utils;
using System.Windows.Forms;
using System.Data;
using DevExpress.Data.Filtering;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NghiepVu.NhanSu.HoSoLuong;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.HoSoLuong
{
    public partial class HoSoLuong_CopyCongThucLuongController : ViewController
    {
        HoSoLuong_CopyCongThucLuong _chonCongThucTinhLuong = null;
        IObjectSpace _obs = null;
        CongThucTinhLuong _congThucTinhLuongCurrent = null;

        public HoSoLuong_CopyCongThucLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HoSoLuong_CopyCongThucLuongController_Activated(object sender, EventArgs e)
        {
            popupWindowShowAction1.Active["TruyCap"] = Common.IsWriteGranted<ChiTietPhanQuyenTinhLuong>();
        }

        private void popupWindowShowAction1_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            //
            _congThucTinhLuongCurrent = View.CurrentObject as CongThucTinhLuong;
            //
            if (_congThucTinhLuongCurrent != null)
            {
                _obs = Application.CreateObjectSpace();
                //
                _chonCongThucTinhLuong = _obs.CreateObject<HoSoLuong_CopyCongThucLuong>();
                _chonCongThucTinhLuong.GetCongThucTinhLuongAll(_congThucTinhLuongCurrent.CongTy);
                e.View = Application.CreateDetailView(_obs, _chonCongThucTinhLuong);
            }
        }

        private void popupWindowShowAction1_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            if (_chonCongThucTinhLuong != null && _chonCongThucTinhLuong.CongThucTinhLuongList.Count > 0)
            {

                if (_chonCongThucTinhLuong.CongTy == null)
                {
                    DialogUtil.ShowError("Chưa chọn công ty để copy.");
                    return;
                }

                using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)_obs).Session.DataLayer))
                {

                    uow.BeginTransaction();
                    //
                    try
                    {
                        using (DialogUtil.AutoWait())
                        {
                            foreach (var item in _chonCongThucTinhLuong.CongThucTinhLuongList)
                            {
                                if (item.Chon)
                                {

                                    //
                                    CongThucTinhLuong congThucDaChon = uow.GetObjectByKey<CongThucTinhLuong>(item.CongThucTinhLuong.Oid);
                                    //
                                    CongThucTinhLuong congThucLuongCopy = new CongThucTinhLuong(uow);
                                    congThucLuongCopy.CongTy = uow.GetObjectByKey<CongTy>(_chonCongThucTinhLuong.CongTy.Oid);
                                    congThucLuongCopy.NgungSuDung = congThucDaChon.NgungSuDung;
                                    congThucLuongCopy.DienGiai = congThucDaChon.DienGiai;
                                    congThucLuongCopy.DieuKienNhanVien = congThucDaChon.DieuKienNhanVien;
                                    congThucLuongCopy.LoaiCongThucLuong = congThucDaChon.LoaiCongThucLuong;
                                    //
                                    foreach (ChiTietCongThucTinhLuong itemChiTietCongThucLuong in congThucDaChon.ListChiTietCongThucTinhLuong)
                                    {
                                        ChiTietCongThucTinhLuong chiTietCopy = new ChiTietCongThucTinhLuong(uow);
                                        chiTietCopy.MaChiTiet = itemChiTietCongThucLuong.MaChiTiet;
                                        chiTietCopy.NgungSuDung = itemChiTietCongThucLuong.NgungSuDung;
                                        chiTietCopy.CongThucTinhLuong = congThucLuongCopy;
                                        chiTietCopy.CongTru = itemChiTietCongThucLuong.CongTru;
                                        chiTietCopy.LoaiNgayCongTinhLuong = itemChiTietCongThucLuong.LoaiNgayCongTinhLuong;
                                        chiTietCopy.TruNgayCong = itemChiTietCongThucLuong.TruNgayCong;
                                        chiTietCopy.TinhTheoCongThucTe = itemChiTietCongThucLuong.TinhTheoCongThucTe;
                                        chiTietCopy.DienGiai = itemChiTietCongThucLuong.DienGiai;
                                        chiTietCopy.CongThucTinhSoTien = itemChiTietCongThucLuong.CongThucTinhSoTien;
                                        chiTietCopy.TinhTNCT = itemChiTietCongThucLuong.TinhTNCT;
                                        chiTietCopy.CongThucTinhTNCT = itemChiTietCongThucLuong.CongThucTinhTNCT;
                                        chiTietCopy.CongThucTinhBangChu = itemChiTietCongThucLuong.CongThucTinhBangChu;
                                        //
                                        congThucLuongCopy.ListChiTietCongThucTinhLuong.Add(chiTietCopy);
                                    }

                                }
                            }
                        }

                        //Lưu dữ liệu nếu thành công
                        uow.CommitChanges();
                        //
                        DialogUtil.ShowInfo("Copy công thức lương thành công.");
                    }
                    catch (Exception ex) { DialogUtil.ShowError("Xảy ra lỗi trong quá trình copy dữ liệu: " + ex.Message); return; }
                }
            }
        }
    }
}
