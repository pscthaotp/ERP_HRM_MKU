using System;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.HocPhi.BienLai;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Data.Filtering;
using ERP.Module.NghiepVu.HocPhi.BangCongNos;
using System.Data;
using DevExpress.ExpressApp.Web;

namespace ERP.Module.Web.Controllers.NghiepVu.HocPhi
{
    public partial class HocPhi_TaoHoSoHocSinhController : ViewController
    {
        IObjectSpace _obs = null;
        public HocPhi_TaoHoSoHocSinhController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void HocPhi_TaoHoSoHocSinhController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<TaoHoSoHocSinh>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            View.ObjectSpace.CommitChanges();
            _obs = View.ObjectSpace;
            
            TaoHoSoHocSinh obj = View.CurrentObject as TaoHoSoHocSinh;
            if (obj != null)
            {
                if (obj.ListChiTietTaoHoSoHocSinh.Count == 0)
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Không có loại phí.')");
                else
                {
                    string message = string.Empty;
                    decimal sotien = 0;
                    try
                    {
                        ERP.Module.NghiepVu.HocPhi.BangCongNos.BangCongNo bangCongNo;
                        HocSinhCongNo hocSinhCongNo;
                        CongNo congNo = new CongNo(((XPObjectSpace)_obs).Session);

                        //using (DevExpress.Utils.WaitDialogForm dialog = new DevExpress.Utils.WaitDialogForm("Chương trình đang xử lý.", "Vui lòng chờ..."))
                        //{
                            #region Tạo phát sinh công nợ và tạo hồ sơ học sinh
                            int lanIn = 1;
                            
                            bangCongNo = _obs.FindObject<ERP.Module.NghiepVu.HocPhi.BangCongNos.BangCongNo>(CriteriaOperator.Parse("NamHoc = ? and CongTy = ?", obj.NamHoc.Oid, obj.CongTy.Oid));
                        if (bangCongNo != null)
                        {
                            hocSinhCongNo = _obs.FindObject<HocSinhCongNo>(CriteriaOperator.Parse("HoSoNhapHoc = ? and BangCongNo = ?",
                                                                            obj.HoSoNhapHoc != null ? obj.HoSoNhapHoc.Oid : Guid.Empty,
                                                                            bangCongNo.Oid));
                            if (hocSinhCongNo != null)
                            {
                                congNo.NgayTao = obj.NgayLap;
                                congNo.HanApDung = obj.NgayLap;
                                congNo.ApDungThu = true;
                                congNo.KyTinhHocPhi = obj.KyTinhHocPhi;
                                hocSinhCongNo.ListCongNo.Add(congNo);
                            }
                            else
                            {
                                hocSinhCongNo = new HocSinhCongNo(((XPObjectSpace)_obs).Session);
                                hocSinhCongNo.HoSoNhapHoc = obj.HoSoNhapHoc;
                                //Tạo hồ sơ học sinh
                                hocSinhCongNo.HocSinh = Common.CreateHoSoHocSinh(((XPObjectSpace)_obs).Session, obj.HoSoNhapHoc, obj.NamHoc, obj.NgayVaoHoc);
                                hocSinhCongNo.HocSinh.NgayVaoHocHienTai = obj.NgayVaoHoc;
                                //
                                hocSinhCongNo.Lop = obj.Lop;
                                bangCongNo.ListHocSinhCongNo.Add(hocSinhCongNo);

                                congNo.NgayTao = obj.NgayLap;
                                congNo.HanApDung = obj.NgayLap;
                                congNo.KyTinhHocPhi = obj.KyTinhHocPhi;
                                congNo.ApDungThu = true;
                                hocSinhCongNo.ListCongNo.Add(congNo);
                            }
                        }
                        else
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Chưa có Bảng công nợ cho năm học '" + obj.NamHoc.TenNamHoc + "')");
                        //

                        foreach (ChiTietTaoHoSoHocSinh item in obj.ListChiTietTaoHoSoHocSinh)
                        {
                            #region Tạo công nợ áp dụng
                            ChiTietCongNo chiTietCongNo = new ChiTietCongNo(((XPObjectSpace)_obs).Session);
                            chiTietCongNo.DienGiai = item.DienGiai;
                            chiTietCongNo.SoLuong = item.SoLuong;
                            chiTietCongNo.LoaiPhi = item.LoaiPhi;
                            chiTietCongNo.ChinhSachKhuyenMai = item.ChinhSachKhuyenMai;
                            chiTietCongNo.HinhThucDong = item.HinhThucDong;
                            chiTietCongNo.ThuTuUuTien = item.ThuTuUuTien;
                            chiTietCongNo.PhamTramMienGiam = item.PhanTramMienGiam;
                            chiTietCongNo.SoTienMienGiam = item.SoTienMienGiam;
                            chiTietCongNo.KyTinhHocPhi = item.KyTinhHocPhi;
                            chiTietCongNo.CongTru = item.CongTru;
                            chiTietCongNo.DonViTinh = item.DonViTinh;
                            if (item.CongTru == Enum.NhanSu.CongTruEnum.Tru)
                            {
                                chiTietCongNo.DonGia = item.DonGia * (-1);
                                chiTietCongNo.SoTien = item.SoTienThu * (-1);
                                chiTietCongNo.SoTienThu = item.SoTienThu * (-1);
                                chiTietCongNo.DaThu = 0;
                                chiTietCongNo.ConLai = item.SoTienThu * (-1);
                            }
                            else
                            {
                                chiTietCongNo.DonGia = item.DonGia;
                                chiTietCongNo.SoTien = item.SoTienThu;
                                chiTietCongNo.SoTienThu = item.SoTienThu;
                                chiTietCongNo.DaThu = 0;
                                chiTietCongNo.ConLai = item.SoTienThu;
                            }
                            lanIn = item.LanIn;
                            congNo.ListChiTietCongNo.Add(chiTietCongNo);
                            sotien += chiTietCongNo.SoTienThu;
                            #endregion
                        }

                            congNo.TongTien = sotien;
                            congNo.LanIn = lanIn;
                            #endregion
                        //}

                        View.ObjectSpace.CommitChanges();
                        message = "Tạo hồ sơ thành công";
                        WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('" + message + "')");
                        obj.HoSoNhapHoc = null;

                    }
                    catch (Exception ex)
                    {
                        message = "Lỗi: " + ex.Message;
                        WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('" + message + "')");
                    }
                }
            }
        }
    }
}
