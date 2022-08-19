using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.PMS.Enum;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.PMS.NghiepVu;

namespace ERP.Module.PMS.NonPersistent
{
    [NonPersistent]
    [ModelDefault("Caption","Danh sách chi tiết")]
    public class DanhSachChiTietThongTinBangChotTheoNhanVien : BaseObject
    {
        private ThongTinBangChot _ThongTinBangChot;
        private BangChotThuLao _BangChotThuLao;

        [Browsable(false)]
        public ThongTinBangChot ThongTinBangChot
        {
            set { _ThongTinBangChot = value; }
            get { return _ThongTinBangChot; }
        }

        [Browsable(false)]
        public BangChotThuLao BangChotThuLao
        {
            set { _BangChotThuLao = value; }
            get { return _BangChotThuLao ; }
        }

        [ModelDefault("Caption", "Danh sách chi tiết được update")]
        public XPCollection<DanhSachChiTietThongTinBangChotNhanVien> listBangChot
        {
            get;
            set;
        }


        public DanhSachChiTietThongTinBangChotTheoNhanVien(Session session,ThongTinBangChot ThongTinBangChot)
            : base(session)
        {
            this.ThongTinBangChot = ThongTinBangChot;
        }


        public DanhSachChiTietThongTinBangChotTheoNhanVien(Session session,ThongTinBangChot ThongTinBangChot,BangChotThuLao BangChotThuLao)
            : base(session)
        {
            this.ThongTinBangChot = ThongTinBangChot;
            this.BangChotThuLao = BangChotThuLao;
        }

        public DanhSachChiTietThongTinBangChotTheoNhanVien(Session session, ThongTinBangChot ThongTinBangChot, BangChotThuLao BangChotThuLao, XPCollection<DanhSachChiTietThongTinBangChotNhanVien> listBangChot)
            : base(session)
        {
            this.ThongTinBangChot = ThongTinBangChot;
            this.BangChotThuLao = BangChotThuLao;
            this.listBangChot = listBangChot;
        }


    }
}