using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using ERP.Module.PMS.BusinessObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Module.PMS.NghiepVu
{
    [ModelDefault("Caption", "Quản lý khảo thí")]
    [Appearance("QuanLyKhaoThi_Khoa", TargetItems = "*", Enabled = false, Criteria = "BangChotThuLao is not null")] 
    public class QuanLyKhaoThi : ThongTinChungPMS
    {
        private bool _Khoa;
        private BangChotThuLao _BangChotThuLao;
      
        [ModelDefault("Caption", "Khóa")]
        [ImmediatePostData]
        [Browsable(false)]
        public bool Khoa
        {
            get { return _Khoa; }
            set { SetPropertyValue("Khoa", ref _Khoa, value); }
        }
        [ModelDefault("Caption", "Bảng chốt")]
        [ModelDefault("AllowEdit", "False")]
        [Browsable(false)]
        [VisibleInListView(false)]
        public BangChotThuLao BangChotThuLao
        {
            get { return _BangChotThuLao; }
            set
            {
                SetPropertyValue("BangChotThuLao", ref _BangChotThuLao, value);
                if (BangChotThuLao != null)
                    Khoa = true;
                else
                    Khoa = false;
            }
        }
      
        [Aggregated]
        [Association("QuanLyKhaoThi-ListChiTietCoiThi")]
        [ModelDefault("Caption", "Chi tiết Coi thi")]
        public XPCollection<ChiTietCoiThi> ListChiTietCoiThi
        {
            get
            {
                return GetCollection<ChiTietCoiThi>("ListChiTietCoiThi");
            }
        }
        [Aggregated]
        [Association("QuanLyKhaoThi-ListChiTietRaDe")]
        [ModelDefault("Caption", "Chi tiết Ra đề")]
        public XPCollection<ChiTietRaDe> ListChiTietRaDe
        {
            get
            {
                return GetCollection<ChiTietRaDe>("ListChiTietRaDe");
            }
        }
        [Aggregated]
        [Association("QuanLyKhaoThi-ListChiTietChamBai")]
        [ModelDefault("Caption", "Chi tiết Chấm bài")]
        public XPCollection<ChiTietChamBai> ListChiTietChamBai
        {
            get
            {
                return GetCollection<ChiTietChamBai>("ListChiTietChamBai");
            }
        }
        public QuanLyKhaoThi(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction(); 
        }
    }
}
