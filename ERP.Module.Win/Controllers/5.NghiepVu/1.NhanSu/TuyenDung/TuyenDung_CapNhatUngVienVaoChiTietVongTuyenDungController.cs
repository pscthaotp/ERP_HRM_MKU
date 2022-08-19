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
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    public partial class TuyenDung_CapNhatUngVienVaoChiTietVongTuyenDungController : ViewController
    {
        public TuyenDung_CapNhatUngVienVaoChiTietVongTuyenDungController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TuyenDung_CapNhatHinhThucTuyenDungController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<VongTuyenDung>() &&
                Common.IsWriteGranted<ChiTietVongTuyenDung>();
        }

        public void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            IObjectSpace obs = View.ObjectSpace;
            ChiTietVongTuyenDung ctVongTuyenDung;
            VongTuyenDung vongTuyenDung = View.CurrentObject as VongTuyenDung;

            if (vongTuyenDung.BuocTuyenDung != null)
            {
                CriteriaOperator filter;
                if (vongTuyenDung.BuocTuyenDung.ThuTu == 1)
                {
                    filter = CriteriaOperator.Parse("QuanLyTuyenDung=? and NhuCauTuyenDung=?"
                        , vongTuyenDung.ChiTietTuyenDung.QuanLyTuyenDung.Oid, vongTuyenDung.ChiTietTuyenDung.NhuCauTuyenDung.Oid);
                    using (XPCollection<UngVien> ungVienList = new XPCollection<UngVien>(((XPObjectSpace)obs).Session, filter))
                    {
                        foreach (UngVien ungVien in ungVienList)
                        {
                            ctVongTuyenDung = obs.FindObject<ChiTietVongTuyenDung>(CriteriaOperator.Parse("UngVien=?", ungVien));
                            if (ctVongTuyenDung == null)
                            {
                                ctVongTuyenDung = obs.CreateObject<ChiTietVongTuyenDung>();
                                ctVongTuyenDung.VongTuyenDung = vongTuyenDung;
                                ctVongTuyenDung.NhuCauTuyenDung = vongTuyenDung.ChiTietTuyenDung.NhuCauTuyenDung;
                                vongTuyenDung.ListChiTietVongTuyenDung.Add(ctVongTuyenDung);
                                ctVongTuyenDung.UngVien = ungVien;
                            }
                        }
                    }
                }
                else
                {
                    int thuTu = vongTuyenDung.BuocTuyenDung.ThuTu - 1;
                    filter = CriteriaOperator.Parse("VongTuyenDung.BuocTuyenDung.ThuTu=? and VongTuyenDung.ChiTietTuyenDung=? and DuocChuyenQuaVongSau",
                        thuTu, vongTuyenDung.ChiTietTuyenDung.Oid);
                    using (XPCollection<ChiTietVongTuyenDung> ctList = new XPCollection<ChiTietVongTuyenDung>(((XPObjectSpace)obs).Session, filter))
                    {
                        foreach (ChiTietVongTuyenDung item in ctList)
                        {
                            filter = CriteriaOperator.Parse("VongTuyenDung=? and UngVien=?", vongTuyenDung.Oid, item.UngVien.Oid);
                            ctVongTuyenDung = obs.FindObject<ChiTietVongTuyenDung>(filter);
                            if (ctVongTuyenDung == null)
                            {
                                ctVongTuyenDung = new ChiTietVongTuyenDung(((XPObjectSpace)obs).Session);
                                vongTuyenDung.ListChiTietVongTuyenDung.Add(ctVongTuyenDung);
                                ctVongTuyenDung.UngVien = item.UngVien;
                            }
                        }
                    }
                }
            }
        }
    }
}
