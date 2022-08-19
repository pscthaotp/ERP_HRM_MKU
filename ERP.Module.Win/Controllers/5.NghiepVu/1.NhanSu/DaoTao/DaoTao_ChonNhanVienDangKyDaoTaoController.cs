using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Utils;
using ERP.Module.NghiepVu.NhanSu.DaoTao;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using DevExpress.Data.Filtering;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NghiepVu.NhanSu.Helper;
using ERP.Module.Win.NormalForm.NhanSu;
using DevExpress.ExpressApp.Xpo;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.DaoTao
{
    public partial class DaoTao_ChonNhanVienDangKyDaoTaoController : ViewController
    {      
        private DangKyDaoTao dangKy;
        private ChiTietDangKyDaoTao ctDangKyDaoTao;

        public DaoTao_ChonNhanVienDangKyDaoTaoController()
        {
            InitializeComponent();
            RegisterActions(components);
            //Common.DebugTrace("DaoTao_LapQuyetDinhDaoTaoController");
        }

        private void DaoTao_ChonNhanVienDangKyDaoTaoController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsCreateGranted<DangKyDaoTao>(); ;
        }

        private void simpleAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();           
            dangKy = View.CurrentObject as DangKyDaoTao;
            if (dangKy != null && dangKy.QuanLyDaoTao != null)
            {
                using (frmChonNhanVien chonCanBo = new frmChonNhanVien(((XPObjectSpace)View.ObjectSpace).Session, dangKy.QuanLyDaoTao.CongTy))
                {
                    if (chonCanBo.ShowDialog() == DialogResult.OK)
                    {
                        foreach (ThongTinNhanVien item in chonCanBo.GetNhanVienListList())
                        {
                            if(!dangKy.IsExists(item))
                            {
                                ctDangKyDaoTao = new ChiTietDangKyDaoTao(((XPObjectSpace)View.ObjectSpace).Session);
                                ctDangKyDaoTao.DangKyDaoTao = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<DangKyDaoTao>(dangKy.Oid);
                                ctDangKyDaoTao.ThongTinNhanVien = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<ThongTinNhanVien>(item.Oid);
                                ctDangKyDaoTao.BoPhan = ((XPObjectSpace)View.ObjectSpace).Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);

                                dangKy.ListChiTietDangKyDaoTao.Add(ctDangKyDaoTao);
                            }
                        }
                        (View as DetailView).Refresh();
                    }                    
                }              
                //View.ObjectSpace.CommitChanges();
                //View.ObjectSpace.Refresh();
            }            
        }       
    }
}
