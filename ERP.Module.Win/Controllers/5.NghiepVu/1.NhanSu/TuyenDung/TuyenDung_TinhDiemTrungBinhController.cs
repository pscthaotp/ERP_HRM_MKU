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
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using System.Windows.Forms;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.TuyenDung
{
    public partial class TuyenDung_TinhDiemTrungBinhController : ViewController
    {
        public TuyenDung_TinhDiemTrungBinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TuyenDung_TinhDiemTrungBinhController_Activated(object sender, EventArgs e)
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
                    IObjectSpace obs = View.ObjectSpace;
                    CriteriaOperator filter;
                    if (vongTuyenDung.BuocTuyenDung.CoToChucThiTuyen)
                    {
                        foreach (ChiTietVongTuyenDung ctItem in vongTuyenDung.ListChiTietVongTuyenDung)
                        {
                            filter = CriteriaOperator.Parse("DanhSachThi.BuocTuyenDung=? and UngVien=?",
                                vongTuyenDung.BuocTuyenDung.Oid, ctItem.UngVien.Oid);
                            using (XPCollection<ThiSinh> thiSinhList = new XPCollection<ThiSinh>(((XPObjectSpace)obs).Session, filter))
                            {
                                int diem = 0, heSo = 0;
                                bool dau = true, mienThi = true, xetTuyen = true;

                                foreach (ThiSinh tsItem in thiSinhList)
                                {
                                    if (!tsItem.MienThi && !tsItem.XetTuyen)
                                    {
                                        mienThi = false;
                                        xetTuyen = false;
                                        diem += tsItem.DiemSo * tsItem.DanhSachThi.MonThi.HeSo;
                                        heSo += tsItem.DanhSachThi.MonThi.HeSo;

                                        if (tsItem.VangThi || tsItem.DiemSo < tsItem.DanhSachThi.MonThi.DiemSan)
                                            dau = false;
                                    }
                                }

                                if (mienThi)
                                {
                                    ctItem.DuocMien = true;
                                    ctItem.DuocChuyenQuaVongSau = true;
                                }
                                if (xetTuyen)
                                {
                                    ctItem.XetTuyen = true;
                                    ctItem.DuocChuyenQuaVongSau = true;
                                }
                                else
                                {
                                    ctItem.DuocChuyenQuaVongSau = dau;
                                    if (heSo != 0)
                                        ctItem.Diem = Math.Round((decimal)diem / heSo, 1, MidpointRounding.AwayFromZero);
                                    else
                                        XtraMessageBox.Show("Chưa nhập hệ số cho môn thi");
                                }
                            }
                        }
                        obs.CommitChanges();
                        XtraMessageBox.Show("Tính điểm trung bình thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                    }
                    else
                        XtraMessageBox.Show("Vòng tuyển dụng này không tổ chức thi tuyển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);               
                }
            }
        }
    }
}
