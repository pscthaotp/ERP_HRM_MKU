using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.CauHinhChungs;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.Enum.NhanSu;
using ERP.Module.HeThong;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.DanhMuc.TienLuong;
using ERP.Module.MailMerge;
using ERP.Module.NonPersistentObjects.HeThong;
using ERP.Module.Extends;
using ERP.Module.Enum.TuyenSinh_PT;

namespace ERP.Module.Commons
{
    public static partial class Common
    {
        /// <summary>
        /// Lấy Trường hiện tại của người sử dụng
        /// </summary>
        /// <returns></returns>
        public static CongTy CongTy(Session session)
        {
            //SecuritySystemUser_Custom user = SecuritySystemUser_GetCurrentUser();
            //if (user != null)
            //{
            //    if (user.CongTy != null)
            //        return session.GetObjectByKey<CongTy>(user.CongTy.Oid);
            //    return null;
            //}
            //else
            //    return null;
            return session.FindObject<CongTy>(CriteriaOperator.Parse("Oid = '3448404C-3FC2-4135-B74F-1D08428BD383'"));
        }


        /// <summary>
        /// Get current user
        /// </summary>
        /// <returns></returns>
        public static SecuritySystemUser_Custom SecuritySystemUser_GetCurrentUser()
        {
            if (SecuritySystem.CurrentUser != null)
                return SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            return null;
        }

        /// <summary>
        /// Get current user
        /// </summary>
        /// <returns></returns>
        public static SecuritySystemUser_Custom SecuritySystemUser_GetCurrentUser(Session session)
        {
            SecuritySystemUser_Custom userCurrent = SecuritySystem.CurrentUser as SecuritySystemUser_Custom;
            if (userCurrent != null)
                return session.GetObjectByKey<SecuritySystemUser_Custom>(userCurrent.Oid);
            return null;
        }


        /// <summary>
        /// Lấy sức khỏe
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static SucKhoe GetSucKhoe(Session session, string tensuckhoe)
        {
            SucKhoe sucKhoe = session.FindObject<SucKhoe>(CriteriaOperator.Parse("TenSucKhoe like ?", tensuckhoe));
            //
            return sucKhoe;
        }


        /// <summary>
        /// Lấy quốc gia việt nam
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static QuocGia GetQuocGia(Session session, string tenquocgia)
        {
            QuocGia quocGia = session.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia like ?", tenquocgia));
            //
            return quocGia;
        }


        /// <summary>
        /// Lấy quốc gia việt nam
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static TonGiao GetTonGiao(Session session, string tenquocgia)
        {
            TonGiao tonGiao = session.FindObject<TonGiao>(CriteriaOperator.Parse("TenTonGiao like ?", tenquocgia));
            //
            return tonGiao;
        }

        /// <summary>
        /// Lấy cấu hình chung
        /// </summary>
        public static CauHinhChung CauHinhChung_GetCauHinhChung
        {
            get
            {
                //
                if (SecuritySystem.CurrentUser != null &&
                    (SecuritySystem.CurrentUser is SecuritySystemUser_Custom) &&
                    (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).CongTy != null)
                {
                    return (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).CongTy.CauHinhChung;
                }
                return null;
            }
        }

        /// <summary>
        /// Lấy cấu hình chung công ty
        /// </summary>       
        public static CauHinhChung CauHinhChung_GetCauHinhChung_CongTy(CongTy congTy)
        {
            CauHinhChung cauHinhChung = null;
            if (congTy != null)
            {
                cauHinhChung = congTy.CauHinhChung;
            }
            return cauHinhChung;
        }

        /// <summary>
        /// Danh sách quyền chức năng phụ of User
        /// </summary>
        /// <returns></returns>
        public static XPCollection<SecuritySystemRole_MenuNonPersistent> NonPersistentRole_GetListByCurrentUser(Session sesion)
        {
            XPCollection<SecuritySystemRole_MenuNonPersistent> result = null;
            //
            SecuritySystemUser_Custom currentUser = sesion.GetObjectByKey<SecuritySystemUser_Custom>((SecuritySystem.CurrentUser as SecuritySystemUser_Custom).Oid);
            if (currentUser != null)
            {
                InOperator filter1 = new InOperator("Oid", currentUser.Roles);
                //
                //
                XPCollection<SecuritySystemRole_Custom> roleList = new XPCollection<SecuritySystemRole_Custom>(sesion, filter1);
                //
                if (roleList.Count > 0)
                {
                    InOperator filter3 = new InOperator("SecuritySystemRole.Oid", roleList);
                    result = new XPCollection<SecuritySystemRole_MenuNonPersistent>(sesion, filter3);
                    //
                }
            }
            //
            return result;
        }

        /// <summary>
        /// Lấy hiện phần quyền báo cáo of User
        /// </summary>
        /// <returns></returns>
        public static SecuritySystemRole_Report SecuritySystemRole_ReportOfUser()
        {
            SecuritySystemRole_Report result = null; ;
            //
            if (SecuritySystem.CurrentUser != null && (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Report != null)
                result = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Report;
            return result;
        }

        /// <summary>
        /// Lấy phân quyền bộ phận of User
        /// </summary>
        /// <returns></returns>
        public static SecuritySystemRole_Department SecuritySystemRole_DepartmentOfUser()
        {
            SecuritySystemRole_Department result = null; ;
            //
            if (SecuritySystem.CurrentUser != null && (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department != null)
                result = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department;
            return result;
        }

        /// <summary>
        /// Sử dụng dùng để copy các property của 1 object persistent, hiện tại không copy property là 1 collection
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="session">Session của object sau khi copy</param>
        /// <param name="source">object dữ liệu copy</param>
        /// <returns></returns>
        public static T Copy<T>(Session session, T source) where T : IXPSimpleObject
        {
            T copy = (T)source.ClassInfo.CreateNewObject(session);
            foreach (XPMemberInfo m in source.ClassInfo.PersistentProperties)
            {
                if (m is DevExpress.Xpo.Metadata.Helpers.ServiceField || m.IsKey)
                    continue;
                if (m.ReferenceType != null)
                {
                    if (m.GetValue(source) != null)
                        m.SetValue(copy, session.GetObjectByKey(m.ReferenceType, source.Session.GetKeyValue(m.GetValue(source))));
                }
                else
                    m.SetValue(copy, m.GetValue(source));
            }
            return copy;
        }

        /// <summary>
        /// Sử dụng dùng để copy các property của 1 object persistent, hiện tại copy property là 1 collection
        /// </summary>
        /// <typeparam name="T">Kiểu dữ liệu</typeparam>
        /// <param name="session">Session của object sau khi copy</param>
        /// <param name="source">object dữ liệu copy</param>
        /// <returns></returns>
        public static object CopyAll(Session session, object source)
        {
            object copy = session.GetClassInfo(source).CreateNewObject(session);

            foreach (XPMemberInfo m in session.GetClassInfo(source).Members)
            {
                if (m is DevExpress.Xpo.Metadata.Helpers.ServiceField || m.IsKey)
                    continue;

                if (m.IsPublic)
                {
                    if (m.ReferenceType != null)
                    {
                        if (m.GetValue(source) != null)
                            m.SetValue(copy, session.GetObjectByKey(m.ReferenceType, session.GetKeyValue(m.GetValue(source))));
                    }
                    else
                        m.SetValue(copy, m.GetValue(source));
                }
            }

            return copy;
        }

        /// <summary>
        /// Get default template
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MailMergeTemplate GetTemplate(IObjectSpace obs, string name, Guid congTy)
        {
            //
            MailMergeTemplate template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ? and SuDungMacDinh = ? and CongTy.Oid=?", name, "True", congTy));
            if (template == null)
                template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ?", name));
            return template;
        }

        /// <summary>
        /// Get default template with valid date
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static MailMergeTemplate GetTemplateWithValidDate(IObjectSpace obs, string name, Guid congTy, DateTime ngayHieuLuc)
        {
            //
            MailMergeTemplate template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ? and CongTy.Oid=? and NgayLap <= ? and HieuLucDenNgay >= ?", name, congTy, ngayHieuLuc.Date, ngayHieuLuc.Date));
            if (template == null)
                //template = obs.FindObject<MailMergeTemplate>(CriteriaOperator.Parse("MaQuanLy like ? and CongTy.Oid = ?", name, congTy));
                return null;
            return template;
        }

        /// <summary>
        /// Lấy tình trạng theo tên
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TinhTrang GetTinhTrang_ByTenTinhTrang(Session session, string name)
        {
            TinhTrang tinhTrang = session.FindObject<TinhTrang>(CriteriaOperator.Parse("TenTinhTrang like ?", name));
            //
            return tinhTrang;
        }

        /// <summary>
        /// Lấy kỳ tính lương theo ngày
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static KyTinhLuong GetKyTinhLuong_ByDate(Session session, DateTime date)
        {
            KyTinhLuong kyTinhLuong = session.FindObject<KyTinhLuong>(CriteriaOperator.Parse("CongTy=? and Thang=? and Nam=? and !KhoaSo", Common.CongTy(session), date.Month, date.Year));
            //
            return kyTinhLuong;
        }



        /// <summary>
        /// Lấy kỳ chấm công theo ngày
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CC_KyChamCong GetKyChamCong_ByDate(Session session, DateTime date)
        {
            CC_KyChamCong kyChamCong = session.FindObject<CC_KyChamCong>(CriteriaOperator.Parse("CongTy=? and Thang=? and Nam=?", Common.CongTy(session), date.Month, date.Year));
            //
            return kyChamCong;
        }

        /// <summary>
        /// Lấy danh sách kỳ tính lương chưa khóa sổ theo Trường
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XPCollection<KyTinhLuong> GetKyTinhLuongList_ByCompanyInfo(Session session, CongTy congTy)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("CongTy=? and !KhoaSo", congTy.Oid);
            //
            XPCollection<KyTinhLuong> kyTinhLuongList = new XPCollection<KyTinhLuong>(session, filter);
            //
            return kyTinhLuongList;
        }

        /// <summary>
        /// Lấy danh sách kỳ tính lương theo Trường
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XPCollection<KyTinhLuong> GetKyTinhLuongNotBlockList_ByCompanyInfo(Session session, CongTy congTy)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("CongTy=?", congTy.Oid);
            //
            XPCollection<KyTinhLuong> kyTinhLuongList = new XPCollection<KyTinhLuong>(session, filter);
            //
            return kyTinhLuongList;
        }

        /// <summary>
        /// Lấy danh sách kỳ tính lương khóa sổ theo Trường
        /// </summary>
        /// <param name="obs"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static XPCollection<KyTinhLuong> GetKyTinhLuongBlockList_ByCompanyInfo(Session session, CongTy congTy)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("CongTy=? and KhoaSo", congTy.Oid);
            //
            XPCollection<KyTinhLuong> kyTinhLuongList = new XPCollection<KyTinhLuong>(session, filter);
            //
            return kyTinhLuongList;
        }

        public static XPCollection<CongTyView> getCongTy(Session session, LoaiBoPhanEnum loaiBoPhan, LoaiTruongEnum loaiTruong)
        {
            XPCollection<CongTyView> ListCongTy = new XPCollection<CongTyView>(session, false);
            using (DialogUtil.Wait("Đang lấy danh sách trường", "Vui lòng chờ...."))
            {
                SecuritySystemRole_Department permission = (SecuritySystem.CurrentUser as SecuritySystemUser_Custom).SecuritySystemRole_Department;
                string sql = "SELECT bp.Oid,bp.TenBoPhan";
                sql += " FROM dbo.func_SplitStringConvertToTable('" + permission.Quyen + "',';') ds";
                sql += " JOIN dbo.BoPhan bp ON bp.Oid = ds.Oid";
                sql += " JOIN dbo.CongTy ON CongTy.Oid = bp.Oid";
                sql += " WHERE bp.LoaiBoPhan=" + loaiBoPhan.GetHashCode();
                sql += " AND LoaiTruong=" + loaiTruong.GetHashCode();
                sql += " ORDER BY bp.TenBoPhan";
                DataTable dtCongTy = DataProvider.GetDataTable(sql, CommandType.Text);
                if (dtCongTy != null)
                {
                    foreach (DataRow item in dtCongTy.Rows)
                    {
                        CongTyView cty = new CongTyView(session);
                        cty.OidCongTy = new Guid(item["Oid"].ToString());
                        cty.TenCongTy = item["TenBoPhan"].ToString();
                        ListCongTy.Add(cty);
                    }
                }
            }
            return ListCongTy;
        }


        /// <summary>
        /// Lấy hồ sơ bảo hiểm
        /// </summary>
        /// <param name="session"></param>
        /// <param name="nhanVien"></param>
        /// <returns></returns>
        public static HoSoBaoHiem GetHoSoBaoHiem(Session session, ThongTinNhanVien nhanVien)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("ThongTinNhanVien=?", nhanVien.Oid);
            HoSoBaoHiem hoSoBaoHiem = session.FindObject<HoSoBaoHiem>(filter);
            return hoSoBaoHiem;
        }

        /// <summary>
        /// Get năm học hiện tại
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>NamHoc</returns>
        public static NamHoc GetCurrentNamHoc(Session session)
        {
            NamHoc namHoc = null;
            //if (CauHinhChung != null && CauHinhChung.NamHoc != null)
            //    namHoc = session.GetObjectByKey<NamHoc>(CauHinhChung.NamHoc.Oid);
            //else
            //{
            DateTime ngay = DateTime.Now.Date;
            namHoc = session.FindObject<NamHoc>(CriteriaOperator.Parse("NgayBatDau<=? and NgayKetThuc>=?", ngay, ngay));
            //}
            return namHoc;
        }

        /// <summary>
        /// Get niên độ hiện tại
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>NienDoTaiChinh</returns>
        public static NienDoTaiChinh GetCurrentNienDoTaiChinh(Session session, CongTy congTy)
        {
            NienDoTaiChinh nienDo = null;
            //if (CauHinhChung != null && CauHinhChung.NamHoc != null)
            //    namHoc = session.GetObjectByKey<NamHoc>(CauHinhChung.NamHoc.Oid);
            //else
            //{
            DateTime ngay = DateTime.Now.Date;
            nienDo = session.FindObject<NienDoTaiChinh>(CriteriaOperator.Parse("CongTy=? and TuNgay<=? and DenNgay>=?", congTy.Oid, ngay, ngay));
            //}
            return nienDo;
        }

        /// <summary>
        /// Get niên độ hiện tại
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>NienDoTaiChinh</returns>
        public static NienDoTaiChinh GetCurrentNienDoTaiChinhByDate(Session session, CongTy congTy, DateTime ngay)
        {
            NienDoTaiChinh nienDo = null;
            nienDo = session.FindObject<NienDoTaiChinh>(CriteriaOperator.Parse("CongTy=? and TuNgay<=? and DenNgay>=?", congTy.Oid, ngay, ngay));
            return nienDo;
        }

        /// <summary>
        /// Get năm học trong cấu hình
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>NamHoc</returns>
        public static NamHoc GetCurrentConfig()
        {
            var cauHinh = Common.CauHinhChung_GetCauHinhChung;
            if (cauHinh != null)
            {
                return cauHinh.CauHinhTuyenSinh.NamHoc;
            }
            else
                return null;
        }
        /// <summary>
        /// Get tuần học hiện tại
        /// </summary>
        /// <param name="session">session</param>
        /// <returns>NamHoc</returns>
        public static TuanHoc GetCurrentTuanHoc(Session session)
        {
            DateTime ngay = DateTime.Now.Date;

            TuanHoc tuanHoc = null;
            tuanHoc = session.FindObject<TuanHoc>(CriteriaOperator.Parse("TuNgay<=? and DenNgay>=?", ngay, ngay));

            return tuanHoc;
        }

        /// <summary>
        /// Đợt tính lương
        /// </summary>
        /// <param name="madot">Đợt</param>
        /// <returns></returns>
        public static DotTinhLuong GetDotTinhLuong(Session sesion, string maDot)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("MaQuanLy like ?", maDot);
            DotTinhLuong dotTinhLuong = sesion.FindObject<DotTinhLuong>(filter);
            //
            return dotTinhLuong;
        }


        /// <summary>
        /// Dân tộc kinh
        /// </summary>
        /// <param name="lop">Lớp</param>
        /// <returns></returns>
        public static DanToc GetDanToc(Session sesion, string tenDanToc)
        {
            CriteriaOperator filter = CriteriaOperator.Parse("TenDanToc like ?", tenDanToc);
            DanToc danToc = sesion.FindObject<DanToc>(filter);
            //
            return danToc;
        }

        /// <summary>
        /// Tạo hồ sơ học sinh
        /// </summary>

    }
}
