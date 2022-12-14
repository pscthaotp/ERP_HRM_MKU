using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    //[ModelDefault("IsCloneable", "True")]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thành lập hội đồng tuyển dụng")]  
    public class QuyetDinhThanhLapHoiDongTuyenDung : QuyetDinh
    {
        private QuanLyTuyenDung _QuanLyTuyenDung;       

        [ModelDefault("Caption", "Quản lý tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        public QuanLyTuyenDung QuanLyTuyenDung
        {
            get
            {
                return _QuanLyTuyenDung;
            }
            set
            {
                SetPropertyValue("QuanLyTuyenDung", ref _QuanLyTuyenDung, value);
            }
        }       

        [Aggregated]
        [ModelDefault("Caption", "Danh sách hội đồng")]
        [Association("QuyetDinhThanhLapHoiDongTuyenDung-ListHoiDongTuyenDung")]
        public XPCollection<HoiDongTuyenDung> ListHoiDongTuyenDung
        {
            get
            {
                return GetCollection<HoiDongTuyenDung>("ListHoiDongTuyenDung");
            }
        }

        [NonCloneable]
        [Browsable(false)]
        [NonPersistent]
        public bool IsSave { get; set; }

        public QuyetDinhThanhLapHoiDongTuyenDung(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            //if (string.IsNullOrWhiteSpace(NoiDung))
            //    NoiDung = HamDungChung.CauHinhChung.CauHinhQuyetDinh.QuyetDinhThanhLapHoiDongTuyenDung;
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {
                IsSave = true;

                foreach (HoiDongTuyenDung item in ListHoiDongTuyenDung)
                {
                    item.QuanLyTuyenDung = QuanLyTuyenDung;
                }
             
            }
        }
    }
}
