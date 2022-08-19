using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.Win.Controllers.NghiepVu.TienLuong.ExecuteClass;

namespace ERP.Module.Win.Controllers.NghiepVu.TienLuong.ChungTus
{
    public partial class ChungTu_TinhChuyenKhoanController : ViewController
    {
        public ChungTu_TinhChuyenKhoanController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChungTu chungTu = View.CurrentObject as ChungTu;
            if (chungTu != null)
            {
                if (chungTu != null && chungTu.ListChungTuChuyenKhoan.Count == 0)
                {
                    //Lưu thông tin trên form lại
                    View.ObjectSpace.CommitChanges();
                    //
                }

                #region Bắt lỗi trước khi tính
                if (chungTu.KyTinhLuong.KhoaSo)
                {
                    DialogUtil.ShowWarning(String.Format("Kỳ tính lương '[{0}]' đã khóa sổ.", chungTu.KyTinhLuong.TenKy));
                    return;
                }
                if (KiemTraLoaiChuyenKhoan(chungTu))
                {
                    DialogUtil.ShowError(string.Format("Loại chuyển khoản {0} đã tồn tại.", chungTu.LoaiChi));
                    return;
                }
                #endregion

                #region Gọi hàm xử lý tính chuyển khoản
                //
                string message = string.Empty;
                //
                using (DialogUtil.AutoWait())
                {
                    message = ChungTu_TinhChuyenKhoan.XuLy(View.ObjectSpace, chungTu);
                }
                #endregion

                //
                if (string.IsNullOrEmpty(message))
                {
                    //
                    View.ObjectSpace.ReloadObject(chungTu);
                    //
                    chungTu.SoTienBangChu = Common.DocTien(Math.Round(chungTu.TongTienChungTu, 0));
                    View.ObjectSpace.CommitChanges();
                    View.ObjectSpace.Refresh();
                    DialogUtil.ShowInfo("Tính chuyển khoản thành công!!!");
                    //
                }
                else
                {
                    DialogUtil.ShowError(message);
                }
            }
        }

        private void ChungTu_TinhChuyenKhoanController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ChungTu>();
        }

        private bool KiemTraLoaiChuyenKhoan(ChungTu chungTu)
        {
            bool result = false;

            if (chungTu.LoaiChi != null)
            {
                string LuongVaPhuCap = string.Empty;
                /*
                string TruyLinh = string.Empty;
                string TruyThu = string.Empty;
                string KhenThuong = string.Empty;*/
                string ThuNhapKhac = string.Empty;
                string KhauTruLuong = string.Empty;

                //Xét loại chuyển khoản
                if (chungTu.LoaiChi.Contains("LuongVaPhuCap"))
                {
                    LuongVaPhuCap = "LuongVaPhuCap";
                }
                /*
                if (chungTu.LoaiChi.Contains("TruyLinh"))
                {
                    TruyLinh = "TruyLinh";
                }
                if (chungTu.LoaiChi.Contains("TruyLinh"))
                {
                    TruyLinh = "TruyLinh";
                }
                if (chungTu.LoaiChi.Contains("TruyThu"))
                {
                    TruyThu = "TruyThu";
                }
                if (chungTu.LoaiChi.Contains("KhenThuong"))
                {
                    KhenThuong = "KhenThuong";
                }
                */
                if (chungTu.LoaiChi.Contains("ThuNhapKhac"))
                {
                    ThuNhapKhac = "ThuNhapKhac";
                }
                if (chungTu.LoaiChi.Contains("KhauTruLuong"))
                {
                    KhauTruLuong = "KhauTruLuong";
                }

                CriteriaOperator filter = CriteriaOperator.Parse("KyTinhLuong=? And Oid != ?", chungTu.KyTinhLuong.Oid, chungTu.Oid);
                XPCollection<ChungTu> chungTuList = new XPCollection<ChungTu>(((XPObjectSpace)View.ObjectSpace).Session, filter);

                if (chungTuList != null && chungTuList.Count > 0)
                {
                    foreach (ChungTu item in chungTuList)
                    {
                        if (!String.IsNullOrEmpty(item.LoaiChi))
                        {
                            //
                            if ((item.LoaiChi.Trim().Equals(string.Format("{0}", LuongVaPhuCap)) && !string.IsNullOrEmpty(LuongVaPhuCap))
                                /*
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", KhenThuong)) && !string.IsNullOrEmpty(KhenThuong))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", TruyThu)) && !string.IsNullOrEmpty(TruyThu))
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", TruyLinh)) && !string.IsNullOrEmpty(TruyLinh))
                                */
                                || (item.LoaiChi.Trim().Contains(string.Format("{0}", KhauTruLuong)) && !string.IsNullOrEmpty(KhauTruLuong))
                                 || (item.LoaiChi.Trim().Contains(string.Format("{0}", ThuNhapKhac)) && !string.IsNullOrEmpty(ThuNhapKhac))
                               )
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            else
                DialogUtil.ShowError("Chọn loại chuyển khoản !!!");
            //
            return result;
        }
    }
}
