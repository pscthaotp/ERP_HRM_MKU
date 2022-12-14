using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.ComponentModel;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using ERP.Module.DanhMuc.NhanSu;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NonPersistentObjects.NhanSu;

namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{    
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định công nhận đào tạo")]
    public class QuyetDinhCongNhanDaoTao : QuyetDinh
    {
        private QuyetDinhDaoTao _QuyetDinhDaoTao;        

        [ModelDefault("Caption", "Quyết định đào tạo")]
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

        [Aggregated]
        [ImmediatePostData]
        [ModelDefault("Caption", "Danh sách nhân viên")]
        [Association("QuyetDinhCongNhanDaoTao-ListChiTietQuyetDinhCongNhanDaoTao")]
        public XPCollection<ChiTietQuyetDinhCongNhanDaoTao> ListChiTietQuyetDinhCongNhanDaoTao
        {
            get
            {
                return GetCollection<ChiTietQuyetDinhCongNhanDaoTao>("ListChiTietQuyetDinhCongNhanDaoTao");
            }
        }

        public QuyetDinhCongNhanDaoTao(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhDaoTao;
            //
            QuyetDinhMoi = true;
        }       
     
        protected override void OnSaving()
        {
            base.OnSaving();

            IsDirty = true;
        }

        public void CreateListChiTietCongNhanDaoTao(DaoTao_ChonNhanVien item)
        {
            ChiTietQuyetDinhCongNhanDaoTao chiTiet = new ChiTietQuyetDinhCongNhanDaoTao(Session);
            chiTiet.BoPhan = Session.GetObjectByKey<BoPhan>(item.BoPhan.Oid);
            chiTiet.ThongTinNhanVien = Session.GetObjectByKey<ThongTinNhanVien>(item.ThongTinNhanVien.Oid);
            this.ListChiTietQuyetDinhCongNhanDaoTao.Add(chiTiet);
            //this.ListChiTietDuyetDangKyDaoTao.Reload();
        }
    }
}
