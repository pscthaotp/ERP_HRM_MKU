﻿using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.Commons;
using DevExpress.ExpressApp.Web;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using DevExpress.Data.Filtering;
using ERP.Module.HeThong;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Extends;
using DevExpress.Xpo;
using ERP.Module.Enum.Systems;
using System.Web;
using System.IO;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
//
namespace ERP.Module.Web.Controllers.NghiepVu.TuyenSinh
{
    public partial class TuyenSinh_GuiMailThongBaoNhapHocController : ViewController<DetailView>
    {
        public TuyenSinh_GuiMailThongBaoNhapHocController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            List<Guid> hocSinhList = Common.OidCustomList;
            //
            int sucessNumber = 0;
            int erorrNumber = 0;
            string erorrEmail = "";
            //
            ThongBaoNhapHoc_TongHop obj = View.CurrentObject as ThongBaoNhapHoc_TongHop;
            //
            if (obj == null)
            {
                return;
            }
            else
            {
                if (obj.HinhThuc != Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.Email)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn hình thức Email!!!')");
                    return;
                }
                if (string.IsNullOrEmpty(obj.TieuDe))
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Nhập tiêu đề!!!')");
                    return;
                }
                if (string.IsNullOrEmpty(obj.NoiDung))
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Nhập nội dung!!!')");
                    return;
                }
                if (hocSinhList.Count == 0)
                {
                    WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Chọn học sinh!!!')");
                    return;
                }
            }
            //
            SecuritySystemUser_Custom currentUser = Common.SecuritySystemUser_GetCurrentUser();
            if (currentUser == null) return;

            try
            {
                if (hocSinhList.Count > 0)
                {
                    //
                    Session session = ((XPObjectSpace)View.ObjectSpace).Session;

                    //Tư vấn tuyển sinh
                    CriteriaOperator filter = CriteriaOperator.Parse("NamHoc=? and CongTy=?", obj.NamHoc, currentUser.CongTy.Oid);
                    ThongBaoNhapHoc thongBaoNhapHoc = session.FindObject<ThongBaoNhapHoc>(filter);
                    if (thongBaoNhapHoc == null)
                    {
                        thongBaoNhapHoc = new ThongBaoNhapHoc(session);
                        thongBaoNhapHoc.NamHoc = session.GetObjectByKey<NamHoc>(obj.NamHoc.Oid);
                        thongBaoNhapHoc.GhiChu = "THÔNG BÁO NHẬP HỌC NĂM HỌC " + obj.NamHoc.TenNamHoc.ToUpper();
                        //
                    }

                    DateTime ngayThongBao = DateTime.Now;
                    foreach (var item in hocSinhList)
                    {
                        Module.NghiepVu.HocSinh.HocSinhs.HocSinh hocSinh = session.GetObjectByKey<Module.NghiepVu.HocSinh.HocSinhs.HocSinh>(item);
                        if (hocSinh != null && (!string.IsNullOrEmpty(hocSinh.EmailCha) || !string.IsNullOrEmpty(hocSinh.EmailMe)))
                        {
                            //
                            bool daGuiMail = false;

                            //Chi tiết tư vấn
                            filter = CriteriaOperator.Parse("ThongBaoNhapHoc=? and HocSinh=? and NgayThongBao =?  and HinhThucThongBao = 2", thongBaoNhapHoc.Oid, item, ngayThongBao);
                            ChiTietThongBaoNhapHoc chiTiet = session.FindObject<ChiTietThongBaoNhapHoc>(filter);
                            if (chiTiet == null)
                            {
                                chiTiet = new ChiTietThongBaoNhapHoc(session);
                                chiTiet.HocSinh = hocSinh;
                                chiTiet.NgayThongBao = ngayThongBao;
                                chiTiet.NoiDung = obj.NoiDung;
                                chiTiet.Khoi = hocSinh.Lop.LopCha;
                                chiTiet.Lop = hocSinh.Lop;
                                chiTiet.HinhThucThongBao = Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.Email;
                                chiTiet.ThongBaoNhapHoc = thongBaoNhapHoc;

                                //Tránh gửi 1 người 1 ngày
                                filter = CriteriaOperator.Parse("SecuritySystemUser=? and  KeyMask=?", currentUser.Oid, chiTiet.Oid);
                                MailManager mail = session.FindObject<MailManager>(filter);
                                if (mail == null)
                                {
                                    //
                                    string emailgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Email;
                                    string passgui = Common.CauHinhChung_GetCauHinhChung.CauHinhMail.Password;
                                    string noidung = obj.NoiDung;
                                    string tieude = obj.TieuDe.ToLower();
                                    string emailnhan = hocSinh.EmailCha != null ? hocSinh.EmailCha : hocSinh.EmailMe;

                                    //Lưu tập tin đính kèm lại để gửi
                                    string fullPath = string.Empty;
                                    if (obj.TapTinDinhKem != null)
                                    {
                                        fullPath = HttpContext.Current.Server.MapPath("~/Downloads/" + Guid.NewGuid().ToString() + ".xsl");
                                        //
                                        File.WriteAllBytes(fullPath, obj.TapTinDinhKem.Content);
                                    }

                                    //Gửi mail thông báo nhập học
                                    daGuiMail = MailHelpers.CreateMailManager(session, tieude, fullPath, emailgui, passgui, emailnhan, noidung, TrangThaiGuiMailEnum.DaGuiMailThongBaoNhapHoc, chiTiet.Oid);

                                }
                            }
                            //
                            if (daGuiMail)
                            {
                                //
                                View.ObjectSpace.CommitChanges();
                                //
                                sucessNumber++;
                            }
                            else
                            {
                                erorrNumber++;
                            }
                        }
                    }
                }
                //
                string message = "alert('Gửi thành công: " + sucessNumber + " dòng. Số dòng thất bại: " + erorrNumber + "" + erorrEmail + "')";
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", message);
            }
            catch (Exception ex)
            {
                WebWindow.CurrentRequestWindow.RegisterStartupScript("", "alert('Thất bại!!!')");
            }
        }

        private void TuyenSinh_GuiMailThongBaoNhapHocController_Activated(object sender, EventArgs e)
        {
            bool truyCap = Commons.Common.IsWriteGranted<ThongBaoNhapHoc_TongHop>();
            simpleAction1.Active["TruyCap"] = truyCap;
            //
            if (truyCap && Common.OidCustomList.Count > 0)
            {
                Common.OidCustomList = new List<Guid>();
            }
        }
    }
}
