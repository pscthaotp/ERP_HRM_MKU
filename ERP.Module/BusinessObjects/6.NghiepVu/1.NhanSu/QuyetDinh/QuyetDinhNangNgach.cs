using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.ExpressApp.Model;
using ERP.Module.Commons;

//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định nâng ngạch lương")]
    public class QuyetDinhNangNgach : QuyetDinh
    {

        [Aggregated]
        [ModelDefault("Caption", "Danh sách cán bộ")]
        [Association("QuyetDinhNangNgach-ListChiTietQuyetDinhNangNgach")]
        public XPCollection<ChiTietQuyetDinhNangNgach> ListChiTietQuyetDinhNangNgach
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhNangNgach>("ListChiTietQuyetDinhNangNgach");
            }
        }

        public QuyetDinhNangNgach(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhNangNgach;
            //
            QuyetDinhMoi = true;
        }

        protected override void OnSaving()
        {
            base.OnSaving();
        }
    }

}
