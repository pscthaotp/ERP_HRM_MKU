using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using ERP.Module.Enum.TienLuong;
using DevExpress.ExpressApp.Editors;

namespace ERP.Module.DanhMuc.TienLuong
{
    [DefaultClassOptions]
    [ImageName("BO_QuanLyChamCong")]
    [DefaultProperty("TenCa")]
    [ModelDefault("Caption", "Ca chấm công")]
    [Appearance("Hide_GiuaCa", TargetItems = "ThoiGianBDQuetBuoiSang_Non;ThoiGianKTQuetBuoiChieu_Non", Visibility = ViewItemVisibility.Hide, Criteria = "LoaiKhung <> 1")]
    public class CC_CaChamCong : BaseObject
    {
        //
        private string _TenCa;
        private LoaiKhungEnum _LoaiKhung;
        private CC_HinhThucNghi _HinhThucNghi;
        //
        private string _ThoiGianVaoSang;
        private string _ThoiGianRaSang;
        private TimeSpan _ThoiGianVaoSang_Non;
        private TimeSpan _ThoiGianRaSang_Non;
        private decimal _TongSoGioLamViecBuoiSang;
        //
        private string _ThoiGianVaoChieu;
        private string _ThoiGianRaChieu;
        private TimeSpan _ThoiGianVaoChieu_Non;
        private TimeSpan _ThoiGianRaChieu_Non;
        private decimal _TongSoGioLamViecBuoiChieu;
        //
        private string _ThoiGianBatDauNghiGiuaCa;
        private string _ThoiGianKetThucNghiGiuaCa;
        private TimeSpan _ThoiGianBDNghiGiuaCa_Non;
        private TimeSpan _ThoiGianKTNghiGiuaCa_Non;
        private decimal _TongSoGioNghiGiuaCa;
        //
        private string _ThoiGianBDQuetBuoiSang;
        private string _ThoiGianKTQuetBuoiChieu;
        private TimeSpan _ThoiGianBDQuetBuoiSang_Non;
        private TimeSpan _ThoiGianKTQuetBuoiChieu_Non;
        private int _SoPhutCong;
        private int _SoPhutTru;
        //
        private decimal _TongSoGioLamViecCaNgay;
        private bool _NgungSuDung;
        //
        private bool _TatOnLoaded = true;

        [RuleRequiredField("", DefaultContexts.Save)]
        [ModelDefault("Caption", "Tên ca")]
        public string TenCa
        {
            get
            {
                return _TenCa;
            }
            set
            {
                SetPropertyValue("TenCa", ref _TenCa, value);
            }
        }
        
        [ModelDefault("Caption", "Loại khung")]
        public LoaiKhungEnum LoaiKhung
        {
            get
            {
                return _LoaiKhung;
            }
            set
            {
                SetPropertyValue("LoaiKhung", ref _LoaiKhung, value);
            }
        }

        [ModelDefault("Caption", "Hình thức chấm công")]
        public CC_HinhThucNghi HinhThucNghi
        {
            get
            {
                return _HinhThucNghi;
            }
            set
            {
                SetPropertyValue("HinhThucNghi", ref _HinhThucNghi, value);
            }
        }

        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("Caption", "Thời gian vào sáng")]
        public TimeSpan ThoiGianVaoSang_Non
        {
            get
            {
                return _ThoiGianVaoSang_Non;
            }
            set
            {
                SetPropertyValue("ThoiGianVaoSang_Non", ref _ThoiGianVaoSang_Non, value);
                if (!IsLoading && ThoiGianVaoSang_Non != null && TatOnLoaded == false)
                {
                    TongSoGioLamViecBuoiSang = Convert.ToDecimal(ThoiGianRaSang_Non.TotalHours - ThoiGianVaoSang_Non.TotalHours);
                    ThoiGianVaoSang = ThoiGianVaoSang_Non.Hours.ToString("0#") + ":" 
                                        + ThoiGianVaoSang_Non.Minutes.ToString("0#") + ":" 
                                        + ThoiGianVaoSang_Non.Seconds.ToString("0#");
                }
            }
        }
        
        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian vào sáng")]
        public string ThoiGianVaoSang
        {
            get
            {
                return _ThoiGianVaoSang;
            }
            set
            {
                SetPropertyValue("ThoiGianVaoSang", ref _ThoiGianVaoSang, value);
                if (IsLoading && ThoiGianVaoSang != null)
                    ThoiGianVaoSang_Non = TimeSpan.Parse(ThoiGianVaoSang);
            }
        }

        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("Caption", "Thời gian ra sáng")]
        public TimeSpan ThoiGianRaSang_Non
        {
            get
            {
                return _ThoiGianRaSang_Non;
            }
            set
            {
                SetPropertyValue("ThoiGianRaSang_Non", ref _ThoiGianRaSang_Non, value);
                if (!IsLoading && ThoiGianRaSang_Non != null && TatOnLoaded == false)
                {
                    TongSoGioLamViecBuoiSang = Convert.ToDecimal(ThoiGianRaSang_Non.TotalHours - ThoiGianVaoSang_Non.TotalHours);
                    ThoiGianRaSang = ThoiGianRaSang_Non.Hours.ToString("0#") + ":" 
                                    + ThoiGianRaSang_Non.Minutes.ToString("0#") + ":" 
                                    + ThoiGianRaSang_Non.Seconds.ToString("0#");
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian ra sáng")]
        public string ThoiGianRaSang
        {
            get
            {
                return _ThoiGianRaSang;
            }
            set
            {
                SetPropertyValue("ThoiGianRaSang", ref _ThoiGianRaSang, value);
                if (IsLoading && ThoiGianRaSang != null)
                    ThoiGianRaSang_Non = TimeSpan.Parse(ThoiGianRaSang);
            }
        }

        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tổng số giờ làm sáng")]
        [ModelDefault("Editmask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal TongSoGioLamViecBuoiSang
        {
            get
            {
                return _TongSoGioLamViecBuoiSang;
            }
            set
            {
                SetPropertyValue("TongSoGioLamViecBuoiSang", ref _TongSoGioLamViecBuoiSang, value);
                if (!IsLoading)
                    TongSoGioLamViecCaNgay = TongSoGioLamViecBuoiSang + TongSoGioLamViecBuoiChieu;
            }
        }

        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("Caption", "Thời gian vào chiều")]
        public TimeSpan ThoiGianVaoChieu_Non
        {
            get
            {
                return _ThoiGianVaoChieu_Non;
            }
            set
            {
                SetPropertyValue("ThoiGianVaoChieu_Non", ref _ThoiGianVaoChieu_Non, value);
                if (!IsLoading && ThoiGianVaoChieu_Non != null && TatOnLoaded == false)
                {
                    TongSoGioLamViecBuoiChieu = Convert.ToDecimal(ThoiGianRaChieu_Non.TotalHours - ThoiGianVaoChieu_Non.TotalHours);
                    ThoiGianVaoChieu = ThoiGianVaoChieu_Non.Hours.ToString("0#") + ":" 
                                        + ThoiGianVaoChieu_Non.Minutes.ToString("0#") + ":" 
                                        + ThoiGianVaoChieu_Non.Seconds.ToString("0#");
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian vào chiều")]
        public string ThoiGianVaoChieu
        {
            get
            {
                return _ThoiGianVaoChieu;
            }
            set
            {
                SetPropertyValue("ThoiGianVaoChieu", ref _ThoiGianVaoChieu, value);
                if (IsLoading && ThoiGianVaoChieu != null)
                    ThoiGianVaoChieu_Non = TimeSpan.Parse(ThoiGianVaoChieu);
            }
        }

        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("Caption", "Thời gian ra chiều")]
        public TimeSpan ThoiGianRaChieu_Non
        {
            get
            {
                return _ThoiGianRaChieu_Non;
            }
            set
            {
                SetPropertyValue("ThoiGianRaChieu_Non", ref _ThoiGianRaChieu_Non, value);
                if (!IsLoading && ThoiGianRaChieu_Non != null && TatOnLoaded == false)
                {
                    TongSoGioLamViecBuoiChieu = Convert.ToDecimal(ThoiGianRaChieu_Non.TotalHours - ThoiGianVaoChieu_Non.TotalHours);
                    ThoiGianRaChieu = ThoiGianRaChieu_Non.Hours.ToString("0#") + ":" 
                                        + ThoiGianRaChieu_Non.Minutes.ToString("0#") + ":" 
                                        + ThoiGianRaChieu_Non.Seconds.ToString("0#");
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian ra chiều")]
        public string ThoiGianRaChieu
        {
            get
            {
                return _ThoiGianRaChieu;
            }
            set
            {
                SetPropertyValue("ThoiGianRaChieu", ref _ThoiGianRaChieu, value);
                if (IsLoading && ThoiGianRaChieu != null)
                    ThoiGianRaChieu_Non = TimeSpan.Parse(ThoiGianRaChieu);
            }
        }

        [ImmediatePostData]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tổng số giờ làm chiều")]
        [ModelDefault("Editmask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal TongSoGioLamViecBuoiChieu
        {
            get
            {
                return _TongSoGioLamViecBuoiChieu;
            }
            set
            {
                SetPropertyValue("TongSoGioLamViecBuoiChieu", ref _TongSoGioLamViecBuoiChieu, value);
                if (!IsLoading)
                    TongSoGioLamViecCaNgay = TongSoGioLamViecBuoiSang + TongSoGioLamViecBuoiChieu;
            }
        }

        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("Caption", "Thời gian BĐ nghỉ giữa ca")]
        public TimeSpan ThoiGianBDNghiGiuaCa_Non
        {
            get
            {
                return _ThoiGianBDNghiGiuaCa_Non;
            }
            set
            {
                SetPropertyValue("ThoiGianBDNghiGiuaCa_Non", ref _ThoiGianBDNghiGiuaCa_Non, value);
                if (!IsLoading && ThoiGianBDNghiGiuaCa_Non != null && TatOnLoaded == false)
                {
                    TongSoGioNghiGiuaCa = Convert.ToDecimal(ThoiGianKTNghiGiuaCa_Non.TotalHours - ThoiGianBDNghiGiuaCa_Non.TotalHours);
                    ThoiGianBatDauNghiGiuaCa = ThoiGianBDNghiGiuaCa_Non.Hours.ToString("0#") + ":" 
                                            + ThoiGianBDNghiGiuaCa_Non.Minutes.ToString("0#") + ":" 
                                            + ThoiGianBDNghiGiuaCa_Non.Seconds.ToString("0#");
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian BĐ nghỉ giữa ca")]
        public string ThoiGianBatDauNghiGiuaCa
        {
            get
            {
                return _ThoiGianBatDauNghiGiuaCa;
            }
            set
            {
                SetPropertyValue("ThoiGianBatDauNghiGiuaCa", ref _ThoiGianBatDauNghiGiuaCa, value);
                if (IsLoading && ThoiGianBatDauNghiGiuaCa != null)
                    ThoiGianBDNghiGiuaCa_Non = TimeSpan.Parse(ThoiGianBatDauNghiGiuaCa);
            }
        }

        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("Caption", "Thời gian KT nghỉ giữa ca")]
        public TimeSpan ThoiGianKTNghiGiuaCa_Non
        {
            get
            {
                return _ThoiGianKTNghiGiuaCa_Non;
            }
            set
            {
                SetPropertyValue("ThoiGianKTNghiGiuaCa_Non", ref _ThoiGianKTNghiGiuaCa_Non, value);
                if (!IsLoading && ThoiGianKTNghiGiuaCa_Non != null && TatOnLoaded == false)
                {
                    TongSoGioNghiGiuaCa = Convert.ToDecimal(ThoiGianKTNghiGiuaCa_Non.TotalHours - ThoiGianBDNghiGiuaCa_Non.TotalHours);
                    ThoiGianKetThucNghiGiuaCa = ThoiGianKTNghiGiuaCa_Non.Hours.ToString("0#") + ":" 
                                            + ThoiGianKTNghiGiuaCa_Non.Minutes.ToString("0#") + ":" 
                                            + ThoiGianKTNghiGiuaCa_Non.Seconds.ToString("0#");
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian KT nghỉ giữa ca")]
        public string ThoiGianKetThucNghiGiuaCa
        {
            get
            {
                return _ThoiGianKetThucNghiGiuaCa;
            }
            set
            {
                SetPropertyValue("ThoiGianKetThucNghiGiuaCa", ref _ThoiGianKetThucNghiGiuaCa, value);
                if (IsLoading && ThoiGianKetThucNghiGiuaCa != null)
                    ThoiGianKTNghiGiuaCa_Non = TimeSpan.Parse(ThoiGianKetThucNghiGiuaCa);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tổng số giờ nghỉ giữa ca")]
        [ModelDefault("Editmask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal TongSoGioNghiGiuaCa
        {
            get
            {
                return _TongSoGioNghiGiuaCa;
            }
            set
            {
                SetPropertyValue("TongSoGioNghiGiuaCa", ref _TongSoGioNghiGiuaCa, value);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thời gian BĐ quét buổi sáng")]
        public TimeSpan ThoiGianBDQuetBuoiSang_Non
        {
            get
            {
                return _ThoiGianBDQuetBuoiSang_Non;
            }
            set
            {
                SetPropertyValue("ThoiGianBDQuetBuoiSang_Non", ref _ThoiGianBDQuetBuoiSang_Non, value);
                if (!IsLoading && ThoiGianBDQuetBuoiSang_Non != null && TatOnLoaded == false)
                {
                    ThoiGianBDQuetBuoiSang = ThoiGianBDQuetBuoiSang_Non.Hours.ToString("0#") + ":" 
                                            + ThoiGianBDQuetBuoiSang_Non.Minutes.ToString("0#") + ":" 
                                            + ThoiGianBDQuetBuoiSang_Non.Seconds.ToString("0#");
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian BĐ quét buổi sáng")]
        public string ThoiGianBDQuetBuoiSang
        {
            get
            {
                return _ThoiGianBDQuetBuoiSang;
            }
            set
            {
                SetPropertyValue("ThoiGianBDQuetBuoiSang", ref _ThoiGianBDQuetBuoiSang, value);
                if (IsLoading && ThoiGianBDQuetBuoiSang != null)
                    ThoiGianBDQuetBuoiSang_Non = TimeSpan.Parse(ThoiGianBDQuetBuoiSang);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thời gian KT quét buổi chiều")]
        public TimeSpan ThoiGianKTQuetBuoiChieu_Non
        {
            get
            {
                return _ThoiGianKTQuetBuoiChieu_Non;
            }
            set
            {
                SetPropertyValue("ThoiGianKTQuetBuoiChieu_Non", ref _ThoiGianKTQuetBuoiChieu_Non, value);
                if (!IsLoading && ThoiGianKTQuetBuoiChieu_Non != null && TatOnLoaded == false)
                {
                    ThoiGianKTQuetBuoiChieu = ThoiGianKTQuetBuoiChieu_Non.Hours.ToString("0#") + ":" 
                                            + ThoiGianKTQuetBuoiChieu_Non.Minutes.ToString("0#") + ":" 
                                            + ThoiGianKTQuetBuoiChieu_Non.Seconds.ToString("0#");
                }
            }
        }

        [Browsable(false)]
        [ModelDefault("Caption", "Thời gian KT quét buổi chiều")]
        public string ThoiGianKTQuetBuoiChieu
        {
            get
            {
                return _ThoiGianKTQuetBuoiChieu;
            }
            set
            {
                SetPropertyValue("ThoiGianKTQuetBuoiChieu", ref _ThoiGianKTQuetBuoiChieu, value);
                if (IsLoading && ThoiGianKTQuetBuoiChieu != null)
                    ThoiGianKTQuetBuoiChieu_Non = TimeSpan.Parse(ThoiGianKTQuetBuoiChieu);
            }
        }

        [ModelDefault("Caption", "Số phút cộng")]
        public int SoPhutCong
        {
            get
            {
                return _SoPhutCong;
            }
            set
            {
                SetPropertyValue("SoPhutCong", ref _SoPhutCong, value);
            }
        }

        [ModelDefault("Caption", "Số phút trừ")]
        public int SoPhutTru
        {
            get
            {
                return _SoPhutTru;
            }
            set
            {
                SetPropertyValue("SoPhutTru", ref _SoPhutTru, value);
            }
        }

        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("Caption", "Tổng số giờ làm việc cả ngày")]
        [ModelDefault("Editmask", "N1")]
        [ModelDefault("DisplayFormat", "N1")]
        public decimal TongSoGioLamViecCaNgay
        {
            get
            {
                return _TongSoGioLamViecCaNgay;
            }
            set
            {
                SetPropertyValue("TongSoGioLamViecCaNgay", ref _TongSoGioLamViecCaNgay, value);
            }
        }

        [ModelDefault("Caption", "Ngừng sử dụng")]
        public bool NgungSuDung
        {
            get
            {
                return _NgungSuDung;
            }
            set
            {
                SetPropertyValue("NgungSuDung", ref _NgungSuDung, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        [ModelDefault("Caption", "Tắt OnLoaded")]
        public bool TatOnLoaded
        {
            get
            {
                return _TatOnLoaded;
            }
            set
            {
                SetPropertyValue("TatOnLoaded", ref _TatOnLoaded, value);
            }
        }

        public CC_CaChamCong(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ThoiGianVaoSang = ThoiGianRaSang = "00:00:00";
            ThoiGianVaoChieu = ThoiGianRaChieu = "00:00:00";
            ThoiGianBatDauNghiGiuaCa = ThoiGianKetThucNghiGiuaCa = "00:00:00";
            ThoiGianBDQuetBuoiSang = ThoiGianKTQuetBuoiChieu = "00:00:00";
            TatOnLoaded = false;
        }

        protected override void OnLoading()
        {
            base.OnLoaded();
            TatOnLoaded = true;
            //
            //ThoiGianVaoSang_Non = TimeSpan.Parse(ThoiGianVaoSang);
            //ThoiGianRaSang_Non = TimeSpan.Parse(ThoiGianRaSang);
            //ThoiGianVaoChieu_Non = TimeSpan.Parse(ThoiGianVaoChieu);
            //ThoiGianRaChieu_Non = TimeSpan.Parse(ThoiGianRaChieu);
            //ThoiGianBDNghiGiuaCa_Non = TimeSpan.Parse(ThoiGianBatDauNghiGiuaCa);
            //ThoiGianKTNghiGiuaCa_Non = TimeSpan.Parse(ThoiGianKetThucNghiGiuaCa);
            //ThoiGianBDQuetBuoiSang_Non = TimeSpan.Parse(ThoiGianBDQuetBuoiSang);
            //ThoiGianKTQuetBuoiChieu_Non = TimeSpan.Parse(ThoiGianKTQuetBuoiChieu);
            //
            TatOnLoaded = false;
        }
    }

}
