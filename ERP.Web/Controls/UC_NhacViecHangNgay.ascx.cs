using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Xpo;
using ERP.Module.Commons;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.HeThong;
using ERP.Module.NghiepVu.HocSinh.HocSinhs;
using ERP.Module.NghiepVu.TuyenSinh;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Web.Controls
{
    public partial class UC_NhacViecHangNgay : System.Web.UI.UserControl
    {
        IObjectSpace _obs = null;
        XafApplication _application = WebApplication.Instance;
        string _dieuKien = string.Empty;
        NamHoc _namHoc = null;
        // 
        public void UpdateDataSource(DevExpress.Xpo.Session session)
        {

        }
        protected override void OnInit(EventArgs e)
        {

            //
            bool quyenTruyCap = false;
            _obs = _application.CreateObjectSpace();
            DataSet dataSource = new DataSet();
            //
            {
                _namHoc = Common.GetCurrentNamHoc(((XPObjectSpace)_obs).Session);
                //Guid a = Guid.Parse("BE994E9B-B5D0-4D9D-B56E-EA1202919876");
                //_namHoc = ((XPObjectSpace)_obs).Session.GetObjectByKey<NamHoc>(a);
                if (_namHoc != null)
                {
                    _dieuKien = "NamHoc.TenNamHoc Like N'" + _namHoc.TenNamHoc.Trim() + "'";
                    //
                    bool duyet = false;
                    SecuritySystemUser_Custom currentUser = Common.SecuritySystemUser_GetCurrentUser();
                    SecuritySystemRole_Accept accept = ((XPObjectSpace)_obs).Session.FindObject<SecuritySystemRole_Accept>(CriteriaOperator.Parse("SecuritySystemUser = ? and GCRecord is null", currentUser.Oid));
                    if (currentUser == null) return;
                    //
                    if (accept != null)
                    {
                        duyet = true;
                    }
                    //
                    SqlParameter[] param = new SqlParameter[4];
                    param[0] = new SqlParameter("@Criteria", _dieuKien);
                    param[1] = new SqlParameter("@Duyet", duyet);
                    param[2] = new SqlParameter("@CongTy", currentUser.CongTy.Oid.ToString());
                    if (currentUser.ThongTinNhanVien != null)
                        param[3] = new SqlParameter("@ThongTinNhanVien", currentUser.ThongTinNhanVien.Oid);
                    else
                        param[3] = new SqlParameter("@ThongTinNhanVien", Guid.Empty);
                    //
                    SqlCommand sqlCommand = DataProvider.GetCommand("spd_NhacViec_TuyenSinh_NhacNhoCongViecHangNgay", CommandType.StoredProcedure, param);
                    //
                    dataSource = DataProvider.GetDataSet(sqlCommand);
                }
            }

            int i = 0;
            int left = 0;
            int top = 180;
            int addtop = 150;
            int addleft = 320;
            foreach (DataTable item in dataSource.Tables)
            {
                if (item.Rows.Count > 0)
                {

                    //List<string> ListObject = 

                    //Common.IsAccessGranted

                    //
                    string message = (from DataRow dr in item.Rows
                                      select dr["Message"].ToString()).FirstOrDefault();
                    string type = (from DataRow dr in item.Rows
                                   select dr["Type"].ToString()).FirstOrDefault();
                    string header = (from DataRow dr in item.Rows
                                     select dr["Header"].ToString()).FirstOrDefault();
                    string idbutton = (from DataRow dr in item.Rows
                                       select dr["IDButton"].ToString()).FirstOrDefault();

                    if (idbutton.Replace("btn", "").Equals("KeHoachTuyenSinh"))
                    {
                        if (Common.IsAccessGranted(typeof(KeHoachTuyenSinh)))
                            quyenTruyCap = true;
                        else
                            quyenTruyCap = false;
                    }
                    if (idbutton.Replace("btn", "").Equals("TuVanTuyenSinh"))
                    {
                        if (Common.IsAccessGranted(typeof(TuVanTuyenSinh)))
                            quyenTruyCap = true;
                        else
                            quyenTruyCap = false;
                    }
                    if (idbutton.Replace("btn", "").Equals("ThongBaoTuyenSinh"))
                    {
                        if (Common.IsAccessGranted(typeof(ThongBaoTuyenSinh)))
                            quyenTruyCap = true;
                        else
                            quyenTruyCap = false;
                    }
                    if (idbutton.Replace("btn", "").Equals("ThongBaoNhapHoc"))
                    {
                        if (Common.IsAccessGranted(typeof(ThongBaoNhapHoc)))
                            quyenTruyCap = true;
                        else
                            quyenTruyCap = false;
                    }
                    if (idbutton.Replace("btn", "").Equals("YKienChuaXuLy"))
                    {
                        if (Common.IsAccessGranted(typeof(YKienKhachHang)))
                            quyenTruyCap = true;
                        else
                            quyenTruyCap = false;
                    }
                    if (idbutton.Replace("btn", "").Equals("YKienDangXuLy"))
                    {
                        if (Common.IsAccessGranted(typeof(YKienKhachHang)))
                            quyenTruyCap = true;
                        else
                            quyenTruyCap = false;
                    }
                    if (idbutton.Replace("btn", "").Equals("YKienChuyenTiep"))
                    {
                        if (Common.IsAccessGranted(typeof(YKienKhachHang)))
                            quyenTruyCap = true;
                        else
                            quyenTruyCap = false;
                    }
                    if (idbutton.Replace("btn", "").Equals("ToChucSuKien"))
                    {
                        if (Common.IsAccessGranted(typeof(ToChucSuKien)))
                            quyenTruyCap = true;
                        else
                            quyenTruyCap = false;
                    }

                    if (quyenTruyCap)
                    {
                        //
                        if (i % 2 == 0)
                        {
                            left += addleft;
                            top = 180;
                            //
                            ASPxDockPanel dockPanel = new ASPxDockPanel();
                            dockPanel.HeaderText = header;
                            //dockPanel.Text = string.Format("Có {0} {1} cần {2}", item.Rows.Count.ToString(), type, message);
                            dockPanel.ForeColor = System.Drawing.Color.Red;
                            dockPanel.BackColor = System.Drawing.Color.GhostWhite;
                            dockPanel.Left = left;
                            dockPanel.Top = top;
                            dockPanel.Width = 300;
                            dockPanel.Height = 100;
                            dockPanel.ID = "dockPanel_" + i.ToString();
                            dockPanel.Theme = "PlasticBlue";
                            dockPanel.ShowFooter = true;
                            dockPanel.FooterText = "";
                            //
                            LinkButton linkButton = new LinkButton();
                            linkButton.Text = string.Format("Có {0} {1} cần {2}", item.Rows.Count.ToString(), type, message);
                            linkButton.Click += linkButton_Click;
                            linkButton.ID = idbutton;
                            linkButton.BackColor = System.Drawing.Color.GhostWhite;
                            linkButton.ForeColor = System.Drawing.Color.Red;
                            dockPanel.Controls.Add(linkButton);
                            //
                            this.Controls.Add(dockPanel);
                        }
                        else
                        {
                            top += addtop;
                            //
                            ASPxDockPanel dockPanel = new ASPxDockPanel();
                            dockPanel.HeaderText = header;
                            //dockPanel.Text = string.Format("Có {0} {1} cần {2}", item.Rows.Count.ToString(), type, message);
                            dockPanel.ForeColor = System.Drawing.Color.Red;
                            dockPanel.BackColor = System.Drawing.Color.GhostWhite;
                            dockPanel.Left = left;
                            dockPanel.Top = top;
                            dockPanel.Width = 300;
                            dockPanel.Height = 100;
                            dockPanel.ID = "dockPanel_" + i.ToString();
                            dockPanel.Theme = "PlasticBlue";
                            dockPanel.ShowFooter = true;
                            dockPanel.FooterText = "";
                            //
                            LinkButton linkButton = new LinkButton();
                            linkButton.Text = string.Format("Có {0} {1} cần {2}", item.Rows.Count.ToString(), type, message);
                            linkButton.Click += linkButton_Click;
                            linkButton.ID = idbutton;
                            linkButton.BackColor = System.Drawing.Color.GhostWhite;
                            linkButton.ForeColor = System.Drawing.Color.Red;
                            dockPanel.Controls.Add(linkButton);
                            //
                            this.Controls.Add(dockPanel);
                        }

                        //Tăng lên để thông báo
                        i++;
                    }
                    //
                }
            }
        }
        void linkButton_Click(object sender, EventArgs e)
        {
            string idButton = (sender as LinkButton).ID;

            #region 1. Kế hoạch tuyển sinh
            if (idButton.Equals("btnKeHoachTuyenSinh"))
            {
                //
                SqlParameter[] param = new SqlParameter[2];
                param[0] = new SqlParameter("@Criteria", _dieuKien);
                param[1] = new SqlParameter("@CongTy", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.ToString());
                //
                List<Guid> listGuid = DataProvider.GetGuidList("spd_TuyenSinh_TimKeHoachTuyenSinhCanDuyet", CommandType.StoredProcedure, param);
                //
                ChiTietKeHoachTuyenSinh chiTiet = _obs.GetObjectByKey<ChiTietKeHoachTuyenSinh>(listGuid[0]);
                if (chiTiet != null)
                {
                    KeHoachTuyenSinh keHoach = _obs.GetObjectByKey<KeHoachTuyenSinh>(chiTiet.KeHoachTuyenSinh.Oid);
                    //
                    DetailView detailView = _application.CreateDetailView(_obs, keHoach);
                    detailView.ViewEditMode = ViewEditMode.Edit;
                    ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                    showViewParameters.TargetWindow = TargetWindow.Current;
                    ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);

                    //Show cửa sổ mới
                    _application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
                }
            }
            #endregion

            #region 2. Thông báo tuyển sinh
            if (idButton.Equals("btnThongBaoTuyenSinh"))
            {
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Criteria", _dieuKien);
                param[1] = new SqlParameter("@Type", "2");
                param[2] = new SqlParameter("@CongTy", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.ToString());
                //
                List<Guid> listGuid = DataProvider.GetGuidList("spd_TuyenSinh_TimThongBaoTuyenSinhCanDuyet", CommandType.StoredProcedure, param);
                //
                DuyetThongBaoTuyenSinh duyet = _obs.CreateObject<DuyetThongBaoTuyenSinh>();
                //
                foreach (var item in listGuid)
                {
                    ThongBaoTuyenSinh thongBao = _obs.GetObjectByKey<ThongBaoTuyenSinh>(item);
                    //
                    if (thongBao != null)
                    {
                        ChiTietDuyetThongBaoTuyenSinh chiTiet = new ChiTietDuyetThongBaoTuyenSinh(((XPObjectSpace)_obs).Session);
                        chiTiet.Oid = thongBao.Oid;
                        chiTiet.NamHoc = _obs.GetObjectByKey<NamHoc>(thongBao.NamHoc.Oid);
                        chiTiet.ChiTietKeHoachTuyenSinh = _obs.GetObjectByKey<ChiTietKeHoachTuyenSinh>(thongBao.ChiTietKeHoachTuyenSinh.Oid);
                        chiTiet.NgayBatDau = thongBao.NgayBatDau;
                        chiTiet.NgayKetThuc = thongBao.NgayKetThuc;
                        chiTiet.DaDuyet = thongBao.DaDuyet;
                        //
                        duyet.ListChiTietDuyetThongBaoTuyenSinh.Add(chiTiet);
                    }
                }
                //
                DetailView detailView = _application.CreateDetailView(_obs, duyet);
                detailView.ViewEditMode = ViewEditMode.Edit;
                ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                showViewParameters.TargetWindow = TargetWindow.Current;
                ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);

                //Show cửa sổ mới
                _application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
            }
            #endregion

            #region 3. Tư vấn tuyển sinh
            if (idButton.Equals("btnTuVanTuyenSinh"))
            {
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Criteria", _dieuKien);
                param[1] = new SqlParameter("@Type", "4");
                param[2] = new SqlParameter("@CongTy", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.ToString());
                //
                List<Guid> listGuid = DataProvider.GetGuidList("spd_TuyenSinh_TimDanhSachKhachHangCanTuVan", CommandType.StoredProcedure, param);
                //
                TuVanTuyenSinh_TongHop tuVan = _obs.CreateObject<TuVanTuyenSinh_TongHop>();
                tuVan.HinhThuc = Module.Enum.TuyenSinh.HinhThucTuVanEnum.DienThoai;
                //
                foreach (var item in listGuid)
                {
                    ThongTinKhachHang khacHang = _obs.GetObjectByKey<ThongTinKhachHang>(item);
                    //
                    if (khacHang != null)
                    {
                        ChiTietTuVanTuyenSinh_TongHop chiTiet = new ChiTietTuVanTuyenSinh_TongHop(((XPObjectSpace)_obs).Session);
                        chiTiet.Oid = khacHang.Oid;
                        chiTiet.MaKhachHang = khacHang.MaKhachHang;
                        chiTiet.HoTen = khacHang.HoTen;
                        chiTiet.GioiTinh = khacHang.GioiTinh;
                        chiTiet.DienThoai = khacHang.DienThoaiDiDong;
                        chiTiet.Email = khacHang.Email;
                        //
                        tuVan.ListChiTietTuVanTuyenSinh.Add(chiTiet);
                    }
                }
                //
                DetailView detailView = _application.CreateDetailView(_obs, tuVan);
                detailView.ViewEditMode = ViewEditMode.Edit;
                ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                showViewParameters.TargetWindow = TargetWindow.Current;
                ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);

                //Show cửa sổ mới
                _application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
            }
            #endregion

            #region 4. Thông báo nhập học
            if (idButton.Equals("btnThongBaoNhapHoc"))
            {
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Criteria", _dieuKien);
                param[1] = new SqlParameter("@Type", "4");
                param[2] = new SqlParameter("@CongTy", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.ToString());
                //
                List<Guid> listGuid = DataProvider.GetGuidList("spd_TuyenSinh_TimDanhSachHocSinhCanThongBao", CommandType.StoredProcedure, param);
                //
                ThongBaoNhapHoc_TongHop thongBao = _obs.CreateObject<ThongBaoNhapHoc_TongHop>();
                thongBao.HinhThuc = Module.Enum.TuyenSinh.HinhThucThongBaoNhapHocEnum.TrucTiep;
                //
                foreach (var item in listGuid)
                {
                    HocSinh hocSinh = _obs.GetObjectByKey<HocSinh>(item);
                    //
                    if (hocSinh != null)
                    {
                        ChiTietThongBaoNhapHoc_TongHop chiTiet = new ChiTietThongBaoNhapHoc_TongHop(((XPObjectSpace)_obs).Session);
                        chiTiet.Oid = hocSinh.Oid;
                        chiTiet.MaHocSinh = hocSinh.MaQuanLy;
                        chiTiet.HoTen = hocSinh.HoTen;
                        chiTiet.GioiTinh = hocSinh.GioiTinh;
                        chiTiet.NgaySinh = hocSinh.NgaySinh;
                        chiTiet.DienThoaiDiDong = !string.IsNullOrEmpty(hocSinh.DienThoaiCha) ? hocSinh.DienThoaiCha : hocSinh.DienThoaiMe;
                        chiTiet.Email = !string.IsNullOrEmpty(hocSinh.EmailCha) ? hocSinh.EmailCha : hocSinh.EmailCha;
                        //
                        thongBao.ListChiTietThongBaoNhapHoc.Add(chiTiet);
                    }
                }
                //
                DetailView detailView = _application.CreateDetailView(_obs, thongBao);
                detailView.ViewEditMode = ViewEditMode.Edit;
                ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                showViewParameters.TargetWindow = TargetWindow.Current;
                ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);

                //Show cửa sổ mới
                _application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
            }
            #endregion

            #region 5. Ý kiến chứa xử lý
            if (idButton.Equals("btnYKienChuaXuLy"))
            {
                //
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Criteria", _dieuKien);
                param[1] = new SqlParameter("@Type", "2");
                param[2] = new SqlParameter("@CongTy", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.ToString());
                param[3] = new SqlParameter("@ThongTinNhanVien", Common.SecuritySystemUser_GetCurrentUser().ThongTinNhanVien != null ? Common.SecuritySystemUser_GetCurrentUser().ThongTinNhanVien.Oid : Guid.Empty);
                //
                List<Guid> listGuid = DataProvider.GetGuidList("spd_TuyenSinh_TimDanhSachYKienCanXuLy", CommandType.StoredProcedure, param);
                //
                XyLyYKien xyLy = _obs.CreateObject<XyLyYKien>();
                xyLy.DieuKienTimKiem = "[NamHoc.TenNamHoc] Like '" + _namHoc.TenNamHoc + "' and [TrangThai] = ##Enum#ERP.Module.Enum.TuyenSinh.TrangThaiYKienEnum,ChuaXuLy#";
                //
                foreach (var item in listGuid)
                {
                    YKienKhachHang yKien = _obs.GetObjectByKey<YKienKhachHang>(item);
                    //
                    if (yKien != null)
                    {
                        ChiTietXyLyYKien chiTiet = new ChiTietXyLyYKien(((XPObjectSpace)_obs).Session);
                        chiTiet.Oid = yKien.Oid;
                        if (yKien.ThongTinKhachHang != null)
                            chiTiet.ThongTinKhachHang = _obs.GetObjectByKey<ThongTinKhachHang>(yKien.ThongTinKhachHang.Oid);
                        if (yKien.HocSinh != null)
                            chiTiet.HocSinh = _obs.GetObjectByKey<HocSinh>(yKien.HocSinh.Oid);
                        chiTiet.NoiDung = yKien.NoiDung;
                        chiTiet.NgayTiepNhan = yKien.NgayTiepNhan;
                        chiTiet.TrangThai = yKien.TrangThai;
                        chiTiet.LoaiYKien = yKien.LoaiYKien;
                        //
                        xyLy.ListChiTietXyLyYKien.Add(chiTiet);
                    }
                }
                //
                DetailView detailView = _application.CreateDetailView(_obs, xyLy);
                detailView.ViewEditMode = ViewEditMode.Edit;
                ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                showViewParameters.TargetWindow = TargetWindow.Current;
                ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);

                //Show cửa sổ mới
                _application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
            }
            #endregion

            #region 6. Ý kiến đang xử lý

            if (idButton.Equals("btnYKienDangXuLy"))
            {
                //
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Criteria", _dieuKien);
                param[1] = new SqlParameter("@Type", "3");
                param[2] = new SqlParameter("@CongTy", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.ToString());
                param[3] = new SqlParameter("@ThongTinNhanVien", Common.SecuritySystemUser_GetCurrentUser().ThongTinNhanVien != null ? Common.SecuritySystemUser_GetCurrentUser().ThongTinNhanVien.Oid : Guid.Empty);

                //
                List<Guid> listGuid = DataProvider.GetGuidList("spd_TuyenSinh_TimDanhSachYKienCanXuLy", CommandType.StoredProcedure, param);
                //
                XyLyYKien xyLy = _obs.CreateObject<XyLyYKien>();
                xyLy.DieuKienTimKiem = "[NamHoc.TenNamHoc] Like '" + _namHoc.TenNamHoc + "' and [TrangThai] = ##Enum#ERP.Module.Enum.TuyenSinh.TrangThaiYKienEnum,DangXuLy#";
                //
                foreach (var item in listGuid)
                {
                    YKienKhachHang yKien = _obs.GetObjectByKey<YKienKhachHang>(item);
                    //
                    if (yKien != null)
                    {
                        ChiTietXyLyYKien chiTiet = new ChiTietXyLyYKien(((XPObjectSpace)_obs).Session);
                        chiTiet.Oid = yKien.Oid;
                        chiTiet.ThongTinKhachHang = _obs.GetObjectByKey<ThongTinKhachHang>(yKien.ThongTinKhachHang.Oid);
                        chiTiet.NoiDung = yKien.NoiDung;
                        chiTiet.NgayTiepNhan = yKien.NgayTiepNhan;
                        chiTiet.TrangThai = yKien.TrangThai;
                        chiTiet.LoaiYKien = yKien.LoaiYKien;
                        //
                        xyLy.ListChiTietXyLyYKien.Add(chiTiet);
                    }
                }
                //
                DetailView detailView = _application.CreateDetailView(_obs, xyLy);
                detailView.ViewEditMode = ViewEditMode.Edit;
                ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                showViewParameters.TargetWindow = TargetWindow.Current;
                ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);

                //Show cửa sổ mới
                _application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
            }
            #endregion


            #region 6.1. Ý kiến chuyển tiếp

            if (idButton.Equals("btnYKienChuyenTiep"))
            {
                //
                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@Criteria", _dieuKien);
                param[1] = new SqlParameter("@Type", "5");
                param[2] = new SqlParameter("@CongTy", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.ToString());
                param[3] = new SqlParameter("@ThongTinNhanVien", Common.SecuritySystemUser_GetCurrentUser().ThongTinNhanVien != null ? Common.SecuritySystemUser_GetCurrentUser().ThongTinNhanVien.Oid : Guid.Empty);
                //
                List<Guid> listGuid = DataProvider.GetGuidList("spd_TuyenSinh_TimDanhSachYKienCanXuLy", CommandType.StoredProcedure, param);
                //
                YKienKhachHang chiTiet = _obs.GetObjectByKey<YKienKhachHang>(listGuid[0]);
                if (chiTiet != null)
                //
                {
                    YKienKhachHang ct = _obs.GetObjectByKey<YKienKhachHang>(chiTiet.Oid);
                    //
                    DetailView detailView = _application.CreateDetailView(_obs, ct);
                    detailView.ViewEditMode = ViewEditMode.Edit;
                    ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                    showViewParameters.TargetWindow = TargetWindow.Current;
                    ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);

                    //Show cửa sổ mới
                    _application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
                }
                //
            }
            #endregion

            #region 7. Tổ chức sự kiện
            if (idButton.Equals("btnToChucSuKien"))
            {
                //
                SqlParameter[] param = new SqlParameter[3];
                param[0] = new SqlParameter("@Criteria", _dieuKien);
                param[1] = new SqlParameter("@Type", "2");
                param[2] = new SqlParameter("@CongTy", Common.SecuritySystemUser_GetCurrentUser().CongTy.Oid.ToString());
                //
                List<Guid> listGuid = DataProvider.GetGuidList("spd_TuyenSinh_ToChucSuKienCanDuyet", CommandType.StoredProcedure, param);
                //
                DuyetToChucSuKien duyet = _obs.CreateObject<DuyetToChucSuKien>();
                //
                foreach (var item in listGuid)
                {
                    ToChucSuKien toChuc = _obs.GetObjectByKey<ToChucSuKien>(item);
                    //
                    if (toChuc != null)
                    {
                        ChiTietDuyetToChucSuKien chiTiet = new ChiTietDuyetToChucSuKien(((XPObjectSpace)_obs).Session);
                        chiTiet.Oid = toChuc.Oid;
                        chiTiet.ToChucSuKien = toChuc;
                        chiTiet.NgayThucHien = toChuc.NgayThucHien;
                        chiTiet.KinhPhi = toChuc.KinhPhi;
                        chiTiet.DaDuyet = toChuc.DaDuyet;
                        //
                        duyet.ListChiTietDuyetToChucSuKien.Add(chiTiet);
                    }
                }
                //
                DetailView detailView = _application.CreateDetailView(_obs, duyet);
                detailView.ViewEditMode = ViewEditMode.Edit;
                ShowViewParameters showViewParameters = new ShowViewParameters(detailView);
                showViewParameters.TargetWindow = TargetWindow.Current;
                ShowViewSource viewSource = new ShowViewSource(WebWindow.CurrentRequestWindow, null);

                //Show cửa sổ mới
                _application.ShowViewStrategy.ShowView(showViewParameters, viewSource);
            }
            #endregion
        }
    }
}