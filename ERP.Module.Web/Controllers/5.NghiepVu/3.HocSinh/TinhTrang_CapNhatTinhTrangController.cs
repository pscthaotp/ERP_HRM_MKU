using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using ERP.Module.Commons;
using ERP.Module.Extends;
using ERP.Module.NghiepVu.HocPhi.BienLai;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Data;
using ERP.Module.NghiepVu.HocPhi.BaoLuu;
using ERP.Module.NonPersistentObjects.HocSinh;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.DanhMuc.HocSinh;
using ERP.Module.NghiepVu.HocSinh.QuaTrinh;
using DevExpress.ExpressApp.Web;

namespace ERP.Module.Web.Controllers.NghiepVu.HocSinh
{
    public partial class TinhTrang_CapNhatTinhTrangController : ViewController
    {
        IObjectSpace _obs = null;

        public TinhTrang_CapNhatTinhTrangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void TinhTrang_CapNhatTinhTrangController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ThuPhi>()
                                                || Common.IsWriteGranted<ThuPhiLanDau>();
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //View.ObjectSpace.CommitChanges();
            _obs = View.ObjectSpace;
            TinhTrang_CapNhatTinhTrang obj = View.CurrentObject as TinhTrang_CapNhatTinhTrang;
            TinhTrang_CapNhatTinhTrangChiTiet itemChon = null;
            HoSoBaoLuu hsBaoLuu = null;
            if (obj != null)
            {
                if (obj.ListCapNhatChiTiet.Count == 0)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Không có Hồ sơ.')");
                }
                else if (obj.HuyBaoLuu_NhapHocLai == false && obj.HuyBaoLuu_ThoiHoc == false && obj.HuyHoSo == false
                        && obj.NhapHocLai_BaoLuu == false && obj.NhapHocLai_ThoiHoc == false)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Chưa chọn nghiệp vụ thực thi.')");
                }
                else
                { 
                    string message = string.Empty;
                    TinhTrangHS ttDangHoc = _obs.FindObject<TinhTrangHS>(CriteriaOperator.Parse("TenTinhTrang like ?", "Đang học"));
                    TinhTrangHS ttNghiHoc = _obs.FindObject<TinhTrangHS>(CriteriaOperator.Parse("TenTinhTrang like ?", "Nghỉ học"));
                    try
                    {
                            #region Lấy dòng được chọn đầu tiên
                            foreach (TinhTrang_CapNhatTinhTrangChiTiet item in obj.ListCapNhatChiTiet)
                            {
                                if (item.Chon)
                                {
                                    itemChon = item;
                                    if (item.HoSoBaoLuu != null)
                                        hsBaoLuu = _obs.GetObjectByKey<HoSoBaoLuu>(item.HoSoBaoLuu.Oid);
                                    break;
                                }
                            }
                        #endregion

                        if (obj.HuyBaoLuu_NhapHocLai && hsBaoLuu != null)
                        {
                            #region Hủy hồ sơ bảo lưu - Nhập học lại
                            if (hsBaoLuu.TinhTrangHS != ttDangHoc)
                            {
                                try
                                {
                                    hsBaoLuu.LoaiHuy = Enum.TuyenSinh.HuyBaoLuuEnum.Huy_NhapHocLai;
                                    hsBaoLuu.NgayHuy = DateTime.Now;
                                    hsBaoLuu.TinhTrangHS = ttDangHoc;
                                    hsBaoLuu.LyDoHuy = obj.LyDo;
                                    hsBaoLuu.HocSinh.TinhTrangHS = ttDangHoc;
                                    hsBaoLuu.HocSinh.GhiChu = string.Empty;

                                    BaoLuu baoLuu = _obs.FindObject<BaoLuu>(CriteriaOperator.Parse("HoSoBaoLuu = ?", hsBaoLuu.Oid));
                                    if (baoLuu != null)
                                        baoLuu.HuyBaoLuu = true;

                                    View.ObjectSpace.CommitChanges();

                                    SqlParameter[] param = new SqlParameter[1];
                                    param[0] = new SqlParameter("@Oid", hsBaoLuu.Oid);
                                    DataProvider.ExecuteNonQuery("spd_BaoLuu_HuyHoSoBaoLuu", CommandType.StoredProcedure, param);
                                    
                                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Hủy Hồ sơ bảo lưu thành công!')");
                                }
                                catch (Exception ex)
                                {
                                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Lỗi: " + message + "')");
                                }
                            }
                            #endregion
                        }
                        else if (obj.HuyBaoLuu_ThoiHoc && hsBaoLuu != null)
                        {
                            #region Hủy hồ sơ bảo lưu - Thôi học
                            if (hsBaoLuu.TinhTrangHS != ttDangHoc)
                            {
                                try
                                {
                                    hsBaoLuu.LoaiHuy = Enum.TuyenSinh.HuyBaoLuuEnum.Huy_NhapHocLai;
                                    hsBaoLuu.NgayHuy = obj.Ngay;
                                    hsBaoLuu.TinhTrangHS = ttDangHoc;
                                    hsBaoLuu.LyDoHuy = obj.LyDo;
                                    hsBaoLuu.HocSinh.TinhTrangHS = ttNghiHoc;
                                    hsBaoLuu.HocSinh.GhiChu = string.Empty;

                                    BaoLuu baoLuu = _obs.FindObject<BaoLuu>(CriteriaOperator.Parse("HoSoBaoLuu = ?", hsBaoLuu.Oid));
                                    if (baoLuu != null)
                                        baoLuu.HuyBaoLuu = true;

                                    View.ObjectSpace.CommitChanges();

                                    SqlParameter[] param = new SqlParameter[1];
                                    param[0] = new SqlParameter("@Oid", hsBaoLuu.Oid);
                                    DataProvider.ExecuteNonQuery("spd_BaoLuu_HuyHoSoBaoLuu", CommandType.StoredProcedure, param);
                                    
                                    SqlParameter[] param1 = new SqlParameter[4];
                                    param1[0] = new SqlParameter("@HoSoBaoLuu", hsBaoLuu.Oid);
                                    param1[1] = new SqlParameter("@NguoiTao", Common.SecuritySystemUser_GetCurrentUser().Oid);
                                    param1[2] = new SqlParameter("@Ngay", obj.Ngay);
                                    param1[3] = new SqlParameter("@GhiChu", obj.LyDo);
                                    DataProvider.ExecuteNonQuery("spd_ThoiHoc_TaoHoSoThoiHocTuHoSoBaoLuu", CommandType.StoredProcedure, param);

                                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Hủy Hồ sơ bảo lưu - Thôi học thành công!')");
                                }
                                catch (Exception ex)
                                {
                                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Lỗi: " + message + "')");
                                }
                            }
                            #endregion
                        }
                        else if (obj.NhapHocLai_BaoLuu && hsBaoLuu != null)
                        {
                            #region Nhập học lại cho hồ sơ bảo lưu
                            if (hsBaoLuu != null && hsBaoLuu.TinhTrangHS.DaNghiHoc == false && hsBaoLuu.TinhTrangHS.TenTinhTrang.Contains("Bảo lưu"))
                            // cập nhật ngày vào học
                            {
                                try
                                {
                                    hsBaoLuu.NgayKetThuc = obj.Ngay;
                                    if (ttDangHoc != null)
                                    {
                                        hsBaoLuu.TinhTrangHS = ttDangHoc;
                                        hsBaoLuu.HocSinh.TinhTrangHS = ttDangHoc;
                                        hsBaoLuu.HocSinh.TuNgay = DateTime.MinValue;
                                    }

                                    View.ObjectSpace.CommitChanges();

                                    SqlParameter[] param = new SqlParameter[1];
                                    param[0] = new SqlParameter("@HoSoBaoLuu", hsBaoLuu.Oid);
                                    DataProvider.ExecuteNonQuery("dbo.spd_BaoLuu_TinhLaiPhiKhiHocLai", System.Data.CommandType.StoredProcedure, param);
                                    
                                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Thực hiện nhập học lại thành công!')");
                                }
                                catch (Exception ex)
                                {
                                    WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Lỗi: " + message + "')");
                                }
                            }
                            #endregion
                        }
                        else if (obj.NhapHocLai_ThoiHoc)
                        {
                            #region Nhập học lại cho hồ sơ đã nghỉ học trước đó
                            itemChon.HocSinh.TinhTrangHS = ttDangHoc;
                            itemChon.HocSinh.TuNgay = DateTime.MinValue;
                            itemChon.HocSinh.GhiChu = string.Empty;

                            NhapHocLai nhapHocLai = new NhapHocLai(((XPObjectSpace)_obs).Session);
                            nhapHocLai.CongTy = itemChon.HocSinh.CongTy;
                            nhapHocLai.HocSinh = itemChon.HocSinh;
                            nhapHocLai.NgayNhapHoc = obj.Ngay;

                            View.ObjectSpace.CommitChanges();
                            
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Thực hiện nhập học lại thành công!')");
                            #endregion
                        }
                        else if (obj.HuyHoSo)
                        {
                            #region Hủy hồ sơ học sinh - trường hợp nhập học thử, chưa đóng tiền
                            itemChon.HocSinh.TinhTrangHS = _obs.FindObject<TinhTrangHS>(CriteriaOperator.Parse("TenTinhTrang like ?", "Hủy hồ sơ"));
                            itemChon.HocSinh.TuNgay = obj.Ngay;
                            itemChon.HocSinh.GhiChu = obj.LyDo;

                            View.ObjectSpace.CommitChanges();
                            
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Hủy hồ sơ học sinh thành công!')");
                            #endregion
                        }
                        else
                            WebWindow.CurrentRequestWindow.RegisterStartupScript("Thông báo", "alert('Chưa chọn Loại nghiệp vụ!')");

                        obj.Load_TimKiem();
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
