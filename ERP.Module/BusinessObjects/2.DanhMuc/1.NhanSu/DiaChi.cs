using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;

namespace ERP.Module.DanhMuc.NhanSu
{
    [ImageName("BO_Address")]
    [DefaultProperty("FullDiaChi")]
    [ModelDefault("Caption", "Địa chỉ")]
    public class DiaChi : BaseObject
    {
        private QuocGia _QuocGia;
        private TinhThanh _TinhThanh;
        private QuanHuyen _QuanHuyen;
        private XaPhuong _XaPhuong;
        private string _SoNha;
        private string _FullDiaChi;

        #region Thêm mới danh mục
        private KhuPho _KhuPho;
        private ToDanPho _TenToDanPho;
        private TenDuong _TenDuong;
        #endregion

        [ModelDefault("Caption", "Quốc gia")]
        [ImmediatePostData()]
        public QuocGia QuocGia
        {
            get
            {
                return _QuocGia;
            }
            set
            {
                SetPropertyValue("QuocGia", ref _QuocGia, value);
                OnChanged("FullDiaChi");
            }
        }

        [ModelDefault("Caption", "Tỉnh thành")]
        [DataSourceProperty("QuocGia.TinhThanhList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData()]
        public TinhThanh TinhThanh
        {
            get
            {
                return _TinhThanh;
            }
            set
            {
                SetPropertyValue("TinhThanh", ref _TinhThanh, value);
                OnChanged("FullDiaChi");
            }
        }

        [ModelDefault("Caption", "Quận huyện")]
        [DataSourceProperty("TinhThanh.QuanHuyenList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData()]
        public QuanHuyen QuanHuyen
        {
            get
            {
                return _QuanHuyen;
            }
            set
            {
                SetPropertyValue("QuanHuyen", ref _QuanHuyen, value);
                OnChanged("FullDiaChi");
            }
        }

        [ModelDefault("Caption", "Xã phường")]
        [DataSourceProperty("QuanHuyen.XaPhuongList", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData]
        public XaPhuong XaPhuong
        {
            get
            {
                return _XaPhuong;
            }
            set
            {
                SetPropertyValue("XaPhuong", ref _XaPhuong, value);
                OnChanged("FullDiaChi");
            }
        }

        #region Thêm mới danh mục
        [ModelDefault("Caption", "Khu phố")]
        [ImmediatePostData()]
        public KhuPho KhuPho
        {
            get
            {
                return _KhuPho;
            }
            set
            {
                SetPropertyValue("KhuPho", ref _KhuPho, value);
                OnChanged("FullDiaChi");
            }
        }
        [ModelDefault("Caption", "Tổ dân phố")]
        [ImmediatePostData()]
        public ToDanPho ToDanPho
        {
            get
            {
                return _TenToDanPho;
            }
            set
            {
                SetPropertyValue("ToDanPho", ref _TenToDanPho, value);
                OnChanged("FullDiaChi");
            }
        }
        [ModelDefault("Caption", "Đường")]
        [ImmediatePostData()]
        public TenDuong TenDuong
        {
            get
            {
                return _TenDuong;
            }
            set
            {
                SetPropertyValue("TenDuong", ref _TenDuong, value);
                OnChanged("FullDiaChi");
            }
        }
        #endregion

        [ModelDefault("Caption", "Số nhà")]
        [ImmediatePostData()]
        public string SoNha
        {
            get
            {
                return _SoNha;
            }
            set
            {
                SetPropertyValue("SoNha", ref _SoNha, value);
                OnChanged("FullDiaChi");
            }
        }

        [ModelDefault("Caption", "Địa chỉ")]
        public string FullDiaChi
        {
            get
            {
                //
                if (QuocGia != null && QuocGia.TenQuocGia != "Việt Nam")
                    _FullDiaChi = ObjectFormatter.Format("{SoNha},  {XaPhuong.TenXaPhuong}, {QuanHuyen.TenQuanHuyen}, {TinhThanh.TenTinhThanh}, {QuocGia.TenQuocGia}", this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
                else
                    _FullDiaChi = ObjectFormatter.Format("{SoNha},{TenDuong}, {ToDanPho}, {KhuPho}, {XaPhuong.TenXaPhuong}, {QuanHuyen.TenQuanHuyen}, {TinhThanh.TenTinhThanh}", this, EmptyEntriesMode.RemoveDelimeterWhenEntryIsEmpty);
                //
                return _FullDiaChi;
            }
            set
            {
                SetPropertyValue("FullDiaChi", ref _FullDiaChi, value);
            }
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //Tìm quốc gia vn mặc định
            QuocGia vietNam = Session.FindObject<QuocGia>(CriteriaOperator.Parse("TenQuocGia=?", "Việt Nam"));
            if (vietNam != null)
                QuocGia = vietNam;
        }
        public DiaChi(Session session) : base(session) { }
    }

}
