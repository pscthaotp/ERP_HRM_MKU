using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.Enum.NhanSu;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    public partial class TuyenDung_CapNhatHinhThucTuyenDungController : ViewController
    {
        public TuyenDung_CapNhatHinhThucTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TuyenDung_CapNhatHinhThucTuyenDungController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<VongTuyenDung>() &&
                Common.IsWriteGranted<ChiTietVongTuyenDung>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            using (WaitDialogForm dialog = new WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
            {
                VongTuyenDung vongTuyenDung = View.CurrentObject as VongTuyenDung;
                if (vongTuyenDung != null)
                {
                    //IObjectSpace obs = Application.CreateObjectSpace();
                    IObjectSpace obs = View.ObjectSpace;
                    
                    using (UnitOfWork uow = new UnitOfWork(((XPObjectSpace)obs).Session.DataLayer))
                    {
                        uow.BeginTransaction();

                        try
                        {
                            using (DialogUtil.AutoWait())
                            {
                                CriteriaOperator filter;
                                foreach (ChiTietVongTuyenDung ctItem in vongTuyenDung.ListChiTietVongTuyenDung)
                                {
                                    filter = CriteriaOperator.Parse("DanhSachThi.BuocTuyenDung=? and UngVien=?",
                                        vongTuyenDung.BuocTuyenDung.Oid, ctItem.UngVien.Oid);
                                    using (XPCollection<ThiSinh> thiSinhList = new XPCollection<ThiSinh>(((XPObjectSpace)obs).Session, filter))
                                    {
                                        if (ctItem.UngVien.HinhThucTuyenDung == HinhThucTuyenDungEnum.XetTuyen)
                                        {
                                            ctItem.XetTuyen = true;
                                            foreach (ThiSinh tsItem in thiSinhList)
                                            {
                                                tsItem.XetTuyen = true;
                                            }
                                        }
                                        else
                                        {
                                            ctItem.XetTuyen = false;
                                            foreach (ThiSinh tsItem in thiSinhList)
                                            {
                                                tsItem.XetTuyen = false;
                                            }
                                        }
                                    }
                                }
                            }

                            uow.CommitChanges();
                            //Refesh lại dữ liệu DB
                            View.ObjectSpace.ReloadObject(vongTuyenDung);
                            (View as DetailView).Refresh();

                            //Refesh lại dữ liệu trên giao diện
                            View.ObjectSpace.CommitChanges();

                            //Thông báo kết quả
                            DialogUtil.ShowInfo("Cập nhật thành công.");
                        }
                        catch (Exception ex)
                        {
                            //Trả lại dữ liệu ban đầu
                            uow.RollbackTransaction();
                            //Thông báo lỗi
                            DialogUtil.ShowError("Cập nhật không thành công." + ex.Message);
                        }
                    }
                }
            }
        }
    }
}
