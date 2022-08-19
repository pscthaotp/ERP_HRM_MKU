using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NonPersistentObjects.NhanSu;
using ERP.Module.NghiepVu.NhanSu.QuyetDinhs;

namespace ERP.Module.NghiepVu.NhanSu.DaoTao
{
    [ImageName("BO_QuanLyDaoTao")]
    [DefaultProperty("DangKyDaoTao")]
    [ModelDefault("Caption", "Lịch đào tạo")]    
    public class LichDaoTao : BaseObject
    {
        private QuyetDinhDaoTao _QuyetDinhDaoTao;
        private DateTime _ThuHaiBatDau;
        private DateTime _ThuHaiKetThuc;
        private DateTime _ThuBaBatDau;
        private DateTime _ThuBaKetThuc;   
        private DateTime _ThuTuBatDau;
        private DateTime _ThuTuKetThuc;   
        private DateTime _ThuNamBatDau;
        private DateTime _ThuNamKetThuc;  
        private DateTime _ThuSauBatDau;
        private DateTime _ThuSauKetThuc; 
        private DateTime _ThuBayBatDau;
        private DateTime _ThuBayKetThuc;
        private DateTime _ChuNhatBatDau;
        private DateTime _ChuNhatKetThuc; 
        private DateTime _TuNgay;
        private DateTime _DenNgay;  
        private string _GhiChu;

        [Browsable(false)]
        [Association("QuyetDinhDaoTao-ListLichDaoTao")]
        public QuyetDinhDaoTao QuyetDinhDaoTao
        {
            get
            {
                return _QuyetDinhDaoTao;
            }
            set
            {
                SetPropertyValue("QuyetDinhDaoTao", ref _QuyetDinhDaoTao, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thứ hai từ giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuHaiBatDau
        {
            get
            {
                return _ThuHaiBatDau;
            }
            set
            {
                SetPropertyValue("ThuHaiBatDau", ref _ThuHaiBatDau, value);               
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Thứ hai đến giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuHaiKetThuc
        {
            get
            {
                return _ThuHaiKetThuc;
            }
            set
            {
                SetPropertyValue("ThuHaiKetThuc", ref _ThuHaiKetThuc, value);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thứ hai")]      
        public string ThuHai
        {
            get
            {
                if (ThuHaiBatDau != DateTime.MinValue && ThuHaiKetThuc != DateTime.MinValue)
                    return String.Concat("Từ ", ThuHaiBatDau.ToString("HH:mm"), " Đến ", ThuHaiKetThuc.ToString("HH:mm"));
                else return null;
            }            
        } 


        [ModelDefault("Caption", "Thứ ba từ giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuBaBatDau
        {
            get
            {
                return _ThuBaBatDau;
            }
            set
            {
                SetPropertyValue("ThuBa", ref _ThuBaBatDau, value);               
            }
        }

        [ModelDefault("Caption", "Thứ ba đến giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuBaKetThuc
        {
            get
            {
                return _ThuBaKetThuc;
            }
            set
            {
                SetPropertyValue("ThuBaKetThuc", ref _ThuBaKetThuc, value);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thứ ba")]
        public string ThuBa
        {
            get
            {
                if (ThuBaBatDau != DateTime.MinValue && ThuBaKetThuc != DateTime.MinValue)
                    return String.Concat("Từ ", ThuBaBatDau.ToString("HH:mm"), " Đến ", ThuBaKetThuc.ToString("HH:mm"));
                else return null;
            }
        } 

        [ModelDefault("Caption", "Thứ tư từ giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuTuBatDau
        {
            get
            {
                return _ThuTuBatDau;
            }
            set
            {
                SetPropertyValue("ThuTuBatDau", ref _ThuTuBatDau, value);               
            }
        }

        [ModelDefault("Caption", "Thứ tư đến giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuTuKetThuc
        {
            get
            {
                return _ThuTuKetThuc;
            }
            set
            {
                SetPropertyValue("ThuTuKetThuc", ref _ThuTuKetThuc, value);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thứ tư")]
        public string ThuTu
        {
            get
            {
                if (ThuTuBatDau != DateTime.MinValue && ThuTuKetThuc != DateTime.MinValue)
                    return String.Concat("Từ ", ThuTuBatDau.ToString("HH:mm"), " Đến ", ThuTuKetThuc.ToString("HH:mm"));
                else return null;
            }
        } 

        [ModelDefault("Caption", "Thứ năm từ giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuNamBatDau
        {
            get
            {
                return _ThuNamBatDau;
            }
            set
            {
                SetPropertyValue("ThuNamBatDau", ref _ThuNamBatDau, value);               
            }
        }

        [ModelDefault("Caption", "Thứ năm đến giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuNamKetThuc
        {
            get
            {
                return _ThuNamKetThuc;
            }
            set
            {
                SetPropertyValue("ThuNamKetThuc", ref _ThuNamKetThuc, value);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thứ năm")]
        public string ThuNam
        {
            get
            {
                if (ThuNamBatDau != DateTime.MinValue && ThuNamKetThuc != DateTime.MinValue)
                    return String.Concat("Từ ", ThuNamBatDau.ToString("HH:mm"), " Đến ", ThuNamKetThuc.ToString("HH:mm"));
                else return null;
            }
        } 

        [ModelDefault("Caption", "Thứ sáu từ giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuSauBatDau
        {
            get
            {
                return _ThuSauBatDau;
            }
            set
            {
                SetPropertyValue("ThuSauBatDau", ref _ThuSauBatDau, value);               
            }
        }

        [ModelDefault("Caption", "Thứ sáu đến giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuSauKetThuc
        {
            get
            {
                return _ThuSauKetThuc;
            }
            set
            {
                SetPropertyValue("ThuSauKetThuc", ref _ThuSauKetThuc, value);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thứ sáu")]
        public string ThuSau
        {
            get
            {
                if (ThuSauBatDau != DateTime.MinValue && ThuSauKetThuc != DateTime.MinValue)
                    return String.Concat("Từ ", ThuSauBatDau.ToString("HH:mm"), " Đến ", ThuSauKetThuc.ToString("HH:mm"));
                else return null;
            }
        } 

        [ModelDefault("Caption", "Thứ bảy từ giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuBayBatDau
        {
            get
            {
                return _ThuBayBatDau;
            }
            set
            {
                SetPropertyValue("ThuBayBatDau", ref _ThuBayBatDau, value);               
            }
        }

        [ModelDefault("Caption", "Thứ bảy đến giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ThuBayKetThuc
        {
            get
            {
                return _ThuBayKetThuc;
            }
            set
            {
                SetPropertyValue("ThuBayKetThuc", ref _ThuBayKetThuc, value);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Thứ bảy")]
        public string ThuBay
        {
            get
            {
                if (ThuBayBatDau != DateTime.MinValue && ThuBayKetThuc != DateTime.MinValue)
                    return String.Concat("Từ ", ThuBayBatDau.ToString("HH:mm"), " Đến ", ThuBayKetThuc.ToString("HH:mm"));
                else return null;
            }
        } 

        [ModelDefault("Caption", "Chủ nhật từ giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ChuNhatBatDau
        {
            get
            {
                return _ChuNhatBatDau;
            }
            set
            {
                SetPropertyValue("ChuNhatBatDau", ref _ChuNhatBatDau, value);
            }
        }

        [ModelDefault("Caption", "Chủ nhật đến giờ")]
        [ModelDefault("EditMask", "HH:mm")]
        [ModelDefault("DisplayFormat", "HH:mm")]
        public DateTime ChuNhatKetThuc
        {
            get
            {
                return _ChuNhatKetThuc;
            }
            set
            {
                SetPropertyValue("ChuNhatKetThuc", ref _ChuNhatKetThuc, value);
            }
        }

        [NonPersistent]
        [ImmediatePostData]
        [ModelDefault("Caption", "Chủ nhật")]
        public string ChuNhat
        {
            get
            {
                if (ChuNhatBatDau != DateTime.MinValue && ChuNhatKetThuc != DateTime.MinValue)
                    return String.Concat("Từ ", ChuNhatBatDau.ToString("HH:mm"), " Đến ", ChuNhatKetThuc.ToString("HH:mm"));
                else return null;
            }
        } 

        [ImmediatePostData]
        [ModelDefault("Caption", "Áp dụng từ ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime TuNgay
        {
            get
            {
                return _TuNgay;
            }
            set
            {
                SetPropertyValue("TuNgay", ref _TuNgay, value);             
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Áp dụng đến ngày")]
        [ModelDefault("EditMask", "dd/MM/yyyy")]
        [ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        public DateTime DenNgay
        {
            get
            {
                return _DenNgay;
            }
            set
            {
                SetPropertyValue("DenNgay", ref _DenNgay, value);
            }
        }

        [Size(300)]
        [ModelDefault("Caption", "Ghi chú")]
        public string GhiChu
        {
            get
            {
                return _GhiChu;
            }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }       

        public LichDaoTao(Session session) : base(session) { }

        protected override void OnLoaded()
        {
            base.OnLoaded();                   
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //            
        }
            
    }

}
