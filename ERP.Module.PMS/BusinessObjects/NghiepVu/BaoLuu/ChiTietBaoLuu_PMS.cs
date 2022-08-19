using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Chi tiết bảo lưu")]
    public class ChiTietBaoLuu_PMS : BaseObject
    {
        private QuanLyBaoLuu _QuanLyBaoLuu;

        private NhanVien _NhanVien;
        private decimal _SoGioBaoLuuGiangDay;
        private decimal _SoGioBaoLuuNCKH;
        private decimal _SoGioBaoLuuHDQL;
        private string _GhiChu;
        private decimal _SoGioBaoLuuNCKHConLai;


        [ModelDefault("Caption", "Quản lý giờ chuẩn")]
        [Association("QuanLyBaoLuu-ListChiTietBaoLuu")]
        [Browsable(false)]
        public QuanLyBaoLuu QuanLyBaoLuu
        {
            get
            {
                return _QuanLyBaoLuu;
            }
            set
            {
                SetPropertyValue("QuanLyBaoLuu", ref _QuanLyBaoLuu, value);
            }
        }
        [ModelDefault("Caption", "Cán bộ")]
        public NhanVien NhanVien
        {
            get { return _NhanVien; }
            set
            {
                SetPropertyValue("NhanVien", ref _NhanVien, value);
            }
        }
        [ModelDefault("Caption", "Bảo lưu giảng dạy")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioBaoLuuGiangDay
        {
            get { return _SoGioBaoLuuGiangDay; }
            set { SetPropertyValue("SoGioBaoLuuGiangDay", ref _SoGioBaoLuuGiangDay, value); }
        }
        [ModelDefault("Caption", "Bảo lưu NCKH")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioBaoLuuNCKH
        {
            get { return _SoGioBaoLuuNCKH; }
            set { SetPropertyValue("SoGioBaoLuuNCKH", ref _SoGioBaoLuuNCKH, value); }
        }
        [ModelDefault("Caption", "Bảo lưu TGQL")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        public decimal SoGioBaoLuuHDQL
        {
            get { return _SoGioBaoLuuHDQL; }
            set { SetPropertyValue("SoGioBaoLuuHDQL", ref _SoGioBaoLuuHDQL, value); }
        }

        [ModelDefault("Caption", "Bảo lưu NCKH còn lại")]
        [ModelDefault("DisplayFormat", "N2")]
        [ModelDefault("EditMask", "N2")]
        [Browsable(false)]
        public decimal SoGioBaoLuuNCKHConLai
        {
            get { return _SoGioBaoLuuNCKHConLai; }
            set { SetPropertyValue("SoGioBaoLuuNCKHConLai", ref _SoGioBaoLuuNCKHConLai, value); }
        }

        [ModelDefault("Caption", "Ghi chú")]
        [Size(-1)]
        public string GhiChu
        {
            get { return _GhiChu; }
            set
            {
                SetPropertyValue("GhiChu", ref _GhiChu, value);
            }
        }
        public ChiTietBaoLuu_PMS(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
    }
}
