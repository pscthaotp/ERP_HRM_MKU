using System;
using System.Globalization;
using System.Web.UI.WebControls;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Web.Editors.ASPx;
using DevExpress.ExpressApp.Model;
using DevExpress.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Web.ASPxHtmlEditor;
using ERP.Module.NonPersistentObjects.TuyenSinh;
using ERP.Module.CauHinhChungs;
using ERP.Module.Commons;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.NghiepVu.TuyenSinh;
using System.Drawing;
using ERP.Module.NghiepVu.TuyenSinh_TP;
using ERP.Module.NonPersistentObjects.TuyenSinh_TP;
using ERP.Module.DanhMuc.TuyenSinh_TP;
using ERP.Module.DanhMuc.TuyenSinh;
using ERP.Module.NghiepVu.XetTuyen;
using ERP.Module.NonPersistentObjects.XetTuyen;
using ERP.Module.BaoCao.TuyenSinh_TP;

//...
namespace ERP.Module.Web.Editors
{
    [PropertyEditor(typeof(String), false)]
    public class ComboBox_Khoi : ASPxPropertyEditor
    {
        ASPxComboBox _aspxComboBox = new ASPxComboBox();
        string _connect = DataProvider.GetConnectionString();
        //
        public ComboBox_Khoi(Type objectType, IModelMemberViewItem info) : base(objectType, info) { }

        protected override void SetupControl(WebControl control)
        {
            if (ViewEditMode == ViewEditMode.Edit)
            {

            }
        }
        protected override WebControl CreateEditModeControlCore()
        {
            _aspxComboBox = new ASPxComboBox();
            //Set null value
            _aspxComboBox.NullText = "Chưa chọn";
            //đổ dữ liệu vào combobox
            DataTable khoiList = KhoiList();
            //
            _aspxComboBox.DataSource = khoiList;
            _aspxComboBox.DataBind();
            _aspxComboBox.TextField = "TENKHOI";
            _aspxComboBox.ValueField = "TENKHOI";

            //hiện nút clear
            _aspxComboBox.ClearButton.DisplayMode = ClearButtonDisplayMode.Always;
            //
            _aspxComboBox.DropDownButton.Visible = this.AllowEdit;
            _aspxComboBox.SelectedIndexChanged += new EventHandler(_aspxComboBox_SelectedIndexChanged);
            return _aspxComboBox;
        }

        void _aspxComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.AllowEdit)
            {
                try
                {
                    if (View.Id.Equals("HoSoXetTuyen_DetailView"))
                    {
                        HoSoXetTuyen obj = View.CurrentObject as HoSoXetTuyen;
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        //else if (!string.IsNullOrEmpty(obj.ID_KHOI.ToString()))
                        //    PropertyValue = obj.ID_KHOI;
                        else
                            PropertyValue = obj.KhoiSIS;
                    }

                    if (View.Id.Equals("ToChucThi_DetailView"))
                    {
                        ToChucThi obj = View.CurrentObject as ToChucThi;
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = obj.KhoiSIS;
                    }

                    if (View.Id.Equals("DanhSachHocSinhChoNhapHoc_DetailView"))
                    {
                        DanhSachHocSinhChoNhapHoc obj = View.CurrentObject as DanhSachHocSinhChoNhapHoc;
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = obj.KhoiSIS;
                    }

                    if (View.Id.Equals("DanhSachTre_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            DanhSachTre obj = View.CurrentObject as DanhSachTre;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("HoSoTuyenSinh_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            HoSoTuyenSinh obj = View.CurrentObject as HoSoTuyenSinh;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("ChiTietKeHoach_Khoi_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            ChiTietKeHoach_Khoi obj = View.CurrentObject as ChiTietKeHoach_Khoi;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("HoSoXetTuyen_KetQuaCuoiNam_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            HoSoXetTuyen_KetQuaCuoiNam obj = View.CurrentObject as HoSoXetTuyen_KetQuaCuoiNam;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("DanhMucMonXetTuyen_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            DanhMucMonXetTuyen obj = View.CurrentObject as DanhMucMonXetTuyen;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("KetQuaCuoiNam_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            KetQuaCuoiNam obj = View.CurrentObject as KetQuaCuoiNam;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("DotXetTuyen_Khoi_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            DotXetTuyen_Khoi obj = View.CurrentObject as DotXetTuyen_Khoi;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("XetTuyen_MonHoc_Diem_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            XetTuyen_MonHoc_Diem obj = View.CurrentObject as XetTuyen_MonHoc_Diem;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("XetTuyen_HocLuc_HanhKiem_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            XetTuyen_HocLuc_HanhKiem obj = View.CurrentObject as XetTuyen_HocLuc_HanhKiem;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("XetTuyen_DotXetTuyen_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            XetTuyen_DotXetTuyen obj = View.CurrentObject as XetTuyen_DotXetTuyen;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("XetMienGiam_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            XetMienGiam obj = View.CurrentObject as XetMienGiam;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }
                    if (View.Id.Equals("XetMienGiam_Khoi_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            XetMienGiam_Khoi obj = View.CurrentObject as XetMienGiam_Khoi;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("DanhMucXetMienGiam_Khoi_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            DanhMucXetMienGiam_Khoi obj = View.CurrentObject as DanhMucXetMienGiam_Khoi;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    //if (View.Id.Equals("XetMienGiam_ToHopMon_DetailView"))
                    //{
                    //    if ((sender as ASPxComboBox).Value != null
                    //        && (sender as ASPxComboBox).Text != null)
                    //    {
                    //        XetMienGiam_ToHopMon obj = View.CurrentObject as XetMienGiam_ToHopMon;
                    //        if (obj != null)
                    //        {
                    //            //Lấy id
                    //            obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                    //        }
                    //    }
                    //    else
                    //        PropertyValue = (sender as ASPxComboBox).Value;
                    //}


                    // ========= Báo cáo =============== //
                    if (View.Id.Equals("BaoCao_TuyenSinh_DanhSachTrungTuyen_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            BaoCao_TuyenSinh_DanhSachTrungTuyen obj = View.CurrentObject as BaoCao_TuyenSinh_DanhSachTrungTuyen;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenLop_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenLop obj = View.CurrentObject as BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenLop;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }

                    if (View.Id.Equals("BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenTruong_DetailView"))
                    {
                        if ((sender as ASPxComboBox).Value != null
                            && (sender as ASPxComboBox).Text != null)
                        {
                            BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenTruong obj = View.CurrentObject as BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenTruong;
                            if (obj != null)
                            {
                                //Lấy id
                                obj.KhoiSIS = (sender as ASPxComboBox).Text.ToString();
                            }
                        }
                        else
                            PropertyValue = (sender as ASPxComboBox).Value;
                    }
                }
                catch (Exception ex)
                {
                }
                OnControlValueChanged();
            }
        }
        public override void BreakLinksToControl(bool unwireEventsOnly)
        {
            //
            base.BreakLinksToControl(unwireEventsOnly);
        }

        private DataTable KhoiList()
        {
            SecuritySystemUser_Custom user = Common.SecuritySystemUser_GetCurrentUser();
            var query = "";
            if (_connect.Contains(Config.KeyServerMamNon) || _connect.Contains(Config.KeyServerMamNonAzure))
                query = "select ID,TENKHOI,GIOIHANTUOIDUOI,GIOIHANTUOITREN,GIOIHANTUOIDUOINU,GIOIHANTUOITRENNU from " + Config.KeyLinkServer + "SIS.dbo.KHOI";
            else
            {
                if (user.CongTy.Oid.Equals(Config.KeyTanPhu))
                {
                    query = "select ID,TENKHOI,GIOIHANTUOIDUOI,GIOIHANTUOITREN,GIOIHANTUOIDUOINU,GIOIHANTUOITRENNU from SIS.dbo.KHOI WHERE ID_TRUONGSUDUNG = 1";
                }
                else if (user.CongTy.Oid.Equals(Config.KeyThaiBinhDuong))
                {
                    query = "select ID,TENKHOI,GIOIHANTUOIDUOI,GIOIHANTUOITREN,GIOIHANTUOIDUOINU,GIOIHANTUOITRENNU from SIS.dbo.KHOI WHERE ID_TRUONGSUDUNG = 19";
                }
                else
                    query = "select ID,TENKHOI,GIOIHANTUOIDUOI,GIOIHANTUOITREN,GIOIHANTUOIDUOINU,GIOIHANTUOITRENNU from SIS.dbo.KHOI";
            }

            using (DataTable dt = DataProvider.GetDataTable(query, CommandType.Text))
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (View.Id.Equals("HoSoXetTuyen_DetailView"))
                    {
                        //
                        HoSoXetTuyen current = View.CurrentObject as HoSoXetTuyen;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                    //nam
                                    if (!string.IsNullOrEmpty(item["GIOIHANTUOIDUOI"].ToString()))
                                        obj.TuTuoi = int.Parse(item["GIOIHANTUOIDUOI"].ToString());
                                    else
                                        obj.TuTuoi = int.Parse(item["TENKHOI"].ToString()) + 5;
                                    if (!string.IsNullOrEmpty(item["GIOIHANTUOITREN"].ToString()))
                                        obj.DenTuoi = int.Parse(item["GIOIHANTUOITREN"].ToString());
                                    else
                                        obj.DenTuoi = int.Parse(item["TENKHOI"].ToString()) + 6;
                                    // nữ
                                    if (!string.IsNullOrEmpty(item["GIOIHANTUOIDUOINU"].ToString()))
                                        obj.TuTuoiNu = int.Parse(item["GIOIHANTUOIDUOINU"].ToString());
                                    else
                                        obj.TuTuoiNu = int.Parse(item["TENKHOI"].ToString()) + 5;
                                    if (!string.IsNullOrEmpty(item["GIOIHANTUOITRENNU"].ToString()))
                                        obj.DenTuoiNu = int.Parse(item["GIOIHANTUOITRENNU"].ToString());
                                    else
                                        obj.DenTuoiNu = int.Parse(item["TENKHOI"].ToString()) + 7;
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("ChiTietKeHoach_Khoi_DetailView"))
                    {
                        //
                        ChiTietKeHoach_Khoi current = View.CurrentObject as ChiTietKeHoach_Khoi;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                    //
                                    if (!string.IsNullOrEmpty(item["GIOIHANTUOIDUOI"].ToString()))
                                        obj.TuTuoi = int.Parse(item["GIOIHANTUOIDUOI"].ToString());
                                    else
                                        obj.TuTuoi = int.Parse(item["TENKHOI"].ToString()) + 5;
                                    if (!string.IsNullOrEmpty(item["GIOIHANTUOITREN"].ToString()))
                                        obj.DenTuoi = int.Parse(item["GIOIHANTUOITREN"].ToString());
                                    else
                                        obj.DenTuoi = int.Parse(item["TENKHOI"].ToString()) + 7;
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("ToChucThi_DetailView"))
                    {
                        //
                        ToChucThi current = View.CurrentObject as ToChucThi;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                    //
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("DanhSachHocSinhChoNhapHoc_DetailView"))
                    {
                        //
                        DanhSachHocSinhChoNhapHoc current = View.CurrentObject as DanhSachHocSinhChoNhapHoc;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                    //
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("DanhSachTre_DetailView"))
                    {
                        //
                        DanhSachTre current = View.CurrentObject as DanhSachTre;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                    //
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("HoSoTuyenSinh_DetailView"))
                    {
                        //
                        HoSoTuyenSinh current = View.CurrentObject as HoSoTuyenSinh;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                    //
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("HoSoXetTuyen_KetQuaCuoiNam_DetailView"))
                    {
                        //
                        HoSoXetTuyen_KetQuaCuoiNam current = View.CurrentObject as HoSoXetTuyen_KetQuaCuoiNam;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        //
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }
                    //

                    if (View.Id.Equals("DanhMucMonXetTuyen_DetailView"))
                    {
                        //
                        DanhMucMonXetTuyen current = View.CurrentObject as DanhMucMonXetTuyen;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        //
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("KetQuaCuoiNam_DetailView"))
                    {
                        //
                        KetQuaCuoiNam current = View.CurrentObject as KetQuaCuoiNam;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        //
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                                current.ListDanhMucKhoiKey.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("DotXetTuyen_Khoi_DetailView"))
                    {
                        //
                        DotXetTuyen_Khoi current = View.CurrentObject as DotXetTuyen_Khoi;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        //
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("XetTuyen_MonHoc_Diem_DetailView"))
                    {
                        //
                        XetTuyen_MonHoc_Diem current = View.CurrentObject as XetTuyen_MonHoc_Diem;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        //
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("XetTuyen_HocLuc_HanhKiem_DetailView"))
                    {
                        //
                        XetTuyen_HocLuc_HanhKiem current = View.CurrentObject as XetTuyen_HocLuc_HanhKiem;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        //
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("XetTuyen_DotXetTuyen_DetailView"))
                    {
                        //
                        XetTuyen_DotXetTuyen current = View.CurrentObject as XetTuyen_DotXetTuyen;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        //
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("DanhMucXetMienGiam_DetailView"))
                    {
                        //
                        DanhMucXetMienGiam current = View.CurrentObject as DanhMucXetMienGiam;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("DanhMucXetMienGiam_Khoi_DetailView"))
                    {
                        //
                        DanhMucXetMienGiam_Khoi current = View.CurrentObject as DanhMucXetMienGiam_Khoi;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("XetMienGiam_DetailView"))
                    {
                        //
                        XetMienGiam current = View.CurrentObject as XetMienGiam;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("XetMienGiam_Khoi_DetailView"))
                    {
                        //
                        XetMienGiam_Khoi current = View.CurrentObject as XetMienGiam_Khoi;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    //if (View.Id.Equals("XetMienGiam_ToHopMon_DetailView"))
                    //{
                    //    //
                    //    XetMienGiam_ToHopMon current = View.CurrentObject as XetMienGiam_ToHopMon;
                    //    Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                    //    current.ListDanhMucKhoi = null;
                    //    current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                    //    //
                    //    foreach (DataRow item in dt.Rows)
                    //    {
                    //        if (current != null)
                    //        {
                    //            DanhMucKhoi obj = new DanhMucKhoi(session);
                    //            if (!item.IsNull("ID"))
                    //            {
                    //                obj.ID = int.Parse(item["ID"].ToString());
                    //                obj.TenKhoi = item["TENKHOI"].ToString();
                    //            }
                    //            current.ListDanhMucKhoi.Add(obj);
                    //        }
                    //    }
                    //}

                    // ========= Báo cáo =============== //
                    if (View.Id.Equals("BaoCao_TuyenSinh_DanhSachTrungTuyen_DetailView"))
                    {
                        //
                        BaoCao_TuyenSinh_DanhSachTrungTuyen current = View.CurrentObject as BaoCao_TuyenSinh_DanhSachTrungTuyen;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenLop_DetailView"))
                    {
                        //
                        BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenLop current = View.CurrentObject as BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenLop;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }

                    if (View.Id.Equals("BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenTruong_DetailView"))
                    {
                        //
                        BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenTruong current = View.CurrentObject as BaoCao_TuyenSinh_ThongKeSoLuongHocSinhChuyenTruong;
                        Session session = ((XPObjectSpace)View.ObjectSpace).Session;
                        current.ListDanhMucKhoi = null;
                        current.ListDanhMucKhoi = new XPCollection<DanhMucKhoi>(session, false);
                        //
                        foreach (DataRow item in dt.Rows)
                        {
                            if (current != null)
                            {
                                DanhMucKhoi obj = new DanhMucKhoi(session);
                                if (!item.IsNull("ID"))
                                {
                                    obj.ID = int.Parse(item["ID"].ToString());
                                    obj.TenKhoi = item["TENKHOI"].ToString();
                                }
                                current.ListDanhMucKhoi.Add(obj);
                            }
                        }
                    }
                }
                return dt;
            }
        }
    }
}