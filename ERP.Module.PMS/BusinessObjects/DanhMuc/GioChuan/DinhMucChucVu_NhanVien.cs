using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;

using DevExpress.ExpressApp.Editors;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Commons;
using ERP.Module.CauHinhChungs;

namespace ERP.Module.PMS.GioChuan
{
    [ImageName("BO_ChuyenNgach")]
    //[DefaultProperty("TenNhomMonHoc")]
    [ModelDefault("Caption", "Định mức chức vụ")]
    [Appearance("SoGioDinhMuc", TargetItems = "SoGioDinhMuc", BackColor = "Aquamarine", FontColor = "Red")]

    [RuleCombinationOfPropertiesIsUnique("", DefaultContexts.Save, "QuanLyGioChuan;NhanVien", "Chức vụ đã tồn tại")]


    public class DinhMucChucVu_NhanVien : BaseObject
    {
        private QuanLyGioChuan _QuanLyGioChuan;

        private NhanVien _NhanVien;
        private string _DinhMucChucVu;
        //
        private decimal _SoGioDinhMuc_ChucVu;
        private decimal _SoGioDinhMuc_CheDoXaHoi;
        //private decimal _SoGioDinhMuc_CVHT;
        private decimal _SoGioDinhMuc_DiHoc;
        //
        private decimal _SoGioDinhMuc_GiangDay;
        private decimal _SoGioDinhMuc_NCKH;
        private decimal _SoGioDinhMuc_Khac;
        //
        private decimal _GioChuanDinhMuc;
        private bool _ChiTinhGioChuan;
        private string _GhiChu;

        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyGioChuan-ListDinhMucChucVuNhanVien")]
        [Browsable(false)]
        public QuanLyGioChuan QuanLyGioChuan
        {
            get
            {
                return _QuanLyGioChuan;
            }
            set
            {
                SetPropertyValue("QuanLyGioChuan", ref _QuanLyGioChuan, value);
            }
        }

        [ModelDefault("Caption", "Nhân viên")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set { SetPropertyValue("NhanVien", ref _NhanVien, value); }
        }

        [ModelDefault("Caption", "Định mức")]
        [ModelDefault("PropertyEditorType", "ERP.Module.Win.Editors.PMS.chkComboxEdit_DinhMucChucVu")]
        [ImmediatePostData]
        [VisibleInListView(false)]
        public string DinhMucChucVu
        {
            get { return _DinhMucChucVu; }
            set
            {
                SetPropertyValue("DinhMucChucVu", ref _DinhMucChucVu, value);

                if (!IsLoading && DinhMucChucVu != string.Empty)
                {
                    if (DinhMucChucVu != null)
                    {
                        if (DinhMucChucVu != null)
                        {
                            try
                            {
                                string sql = "SELECT TOP 1 DinhMuc.SoGioChuan";
                                sql += " FROM dbo.DinhMucChucVu DinhMuc";
                                sql += " JOIN dbo.func_SplitString('" + DinhMucChucVu + "',';') ds ON ds.VALUE=DinhMuc.Oid";
                                sql += " ORDER BY DinhMuc.SoGioChuan ASC";
                                object SoGio = DataProvider.GetValueFromDatabase(sql, System.Data.CommandType.Text);
                                if (SoGio != null)
                                {
                                    SoGioDinhMuc_GiangDay = Convert.ToDecimal(SoGio);
                                }
                            }
                            catch (Exception)
                            {
                                //throw;
                            }

                        }
                    }
                }
            }
        }
        [ModelDefault("Caption", "Bộ phận")]
        //[NonPersistent]
        public string DonVi
        {
            get
            {
                if (NhanVien != null)
                    return NhanVien.BoPhan != null ? NhanVien.BoPhan.TenBoPhan : "";
                else return "";
            }

        }
        [ModelDefault("Caption", "Chức vụ")]
        //[NonPersistent]
        public string ChucVu
        {
            get
            {
                if (NhanVien != null)
                {
                    ThongTinNhanVien ttnv = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid =?", NhanVien.Oid));
                    if (ttnv != null)
                        return ttnv.ChucVu != null ? ttnv.ChucVu.TenChucVu : "";
                    else
                        return "";
                }
                else
                    return "";
            }
        }
        [ModelDefault("Caption", "Chức danh")]
        //[NonPersistent]
        public string ChucDanh
        {
            get
            {
                if (NhanVien != null)
                {
                    ThongTinNhanVien ttnv = Session.FindObject<ThongTinNhanVien>(CriteriaOperator.Parse("Oid =?", NhanVien.Oid));
                    if (ttnv != null)
                        return ttnv.ChucDanh != null ? ttnv.ChucDanh.TenChucDanh : "";
                    else
                        return "";
                }
                else
                    return "";
            }
        }

        [ModelDefault("Caption", "Số giờ giảm trừ chức vụ")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_ChucVu
        {
            get { return _SoGioDinhMuc_ChucVu; }
            set
            {
                SetPropertyValue("SoGioDinhMuc_ChucVu", ref _SoGioDinhMuc_ChucVu, value);
            }
        }

        //[ModelDefault("Caption", "Số giờ giảm trừ CVHT")]
        //[ModelDefault("DisplayFormat", "N2")]
        //[ModelDefault("EditMask", "N2")]
        //public decimal SoGioDinhMuc_CVHT
        //{
        //    get { return _SoGioDinhMuc_CVHT; }
        //    set
        //    {
        //        SetPropertyValue("SoGioDinhMuc_CVHT", ref _SoGioDinhMuc_CVHT, value);
        //    }
        //}

        [ModelDefault("Caption", "Số giờ định mức đi học")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_DiHoc
        {
            get { return _SoGioDinhMuc_DiHoc; }
            set
            {
                SetPropertyValue("SoGioDinhMuc_DiHoc", ref _SoGioDinhMuc_DiHoc, value);
            }
        }

        [ModelDefault("Caption", "Số giờ giảm trừ chế độ xã hội")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_CheDoXaHoi
        {
            get { return _SoGioDinhMuc_CheDoXaHoi; }
            set
            {
                SetPropertyValue("SoGioDinhMuc_CheDoXaHoi", ref _SoGioDinhMuc_CheDoXaHoi, value);
            }
        }

        //

        [ModelDefault("Caption", "Định mức giảng dạy(sau cùng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [ImmediatePostData]
        public decimal SoGioDinhMuc_GiangDay
        {
            get { return _SoGioDinhMuc_GiangDay; }
            set
            {
                SetPropertyValue("SoGioDinhMuc_GiangDay", ref _SoGioDinhMuc_GiangDay, value);
            }
        }

        [ModelDefault("Caption", "Định mức NCKH(sau cùng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_NCHK
        {
            get { return _SoGioDinhMuc_NCKH; }
            set { SetPropertyValue("SoGioDinhMuc_NCHK", ref _SoGioDinhMuc_NCKH, value); }
        }

        [ModelDefault("Caption", "Định mức khác(sau cùng)")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioDinhMuc_Khac
        {
            get { return _SoGioDinhMuc_Khac; }
            set { SetPropertyValue("SoGioDinhMuc_Khac", ref _SoGioDinhMuc_Khac, value); }
        }

        //

        [ModelDefault("Caption", "Giờ chuẩn định mức")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        //[NonPersistent]
        public decimal GioChuanDinhMuc
        {
            get { return _GioChuanDinhMuc; }
            set
            {
                SetPropertyValue("GioChuanDinhMuc", ref _GioChuanDinhMuc, value);
            }
        }

        [ModelDefault("Caption", "Chỉ tính giờ chuẩn")]
        [Browsable(false)]
        public bool ChiTinhGioChuan
        {
            get { return _ChiTinhGioChuan; }
            set
            {
                SetPropertyValue("ChiTinhGioChuan", ref _ChiTinhGioChuan, value);
            }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(500)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set { SetPropertyValue("GhiChu", ref _GhiChu, value); }
        }
        public DinhMucChucVu_NhanVien(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
         
        }
    }
}