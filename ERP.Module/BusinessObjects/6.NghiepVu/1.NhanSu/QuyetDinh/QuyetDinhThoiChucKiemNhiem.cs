using System;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo.DB;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using System.Collections.Generic;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.NghiepVu.NhanSu.Helper;

//
namespace ERP.Module.NghiepVu.NhanSu.QuyetDinhs
{
    [DefaultClassOptions]
    [ImageName("BO_QuyetDinh")]
    [DefaultProperty("SoQuyetDinh")]
    [ModelDefault("Caption", "Quyết định thôi chức kiêm nhiệm")]
    public class QuyetDinhThoiChucKiemNhiem : QuyetDinhCaNhan
    {
        //
        private QuyetDinhBoNhiemKiemNhiem _QuyetDinhBoNhiemKiemNhiem;
        private string _LyDo;

        [ImmediatePostData]
        [ModelDefault("Caption", "Quyết định bổ nhiệm")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("QuyetDinhList")]
        public QuyetDinhBoNhiemKiemNhiem QuyetDinhBoNhiemKiemNhiem
        {
            get
            {
                return _QuyetDinhBoNhiemKiemNhiem;
            }
            set
            {
                SetPropertyValue("QuyetDinhBoNhiemKiemNhiem", ref _QuyetDinhBoNhiemKiemNhiem, value);
            }
        }

        [ModelDefault("Caption", "Lý do")]
        public string LyDo
        {
            get
            {
                return _LyDo;
            }
            set
            {
                SetPropertyValue("LyDo", ref _LyDo, value);
            }
        }

        [Browsable(false)]
        public XPCollection<QuyetDinhBoNhiemKiemNhiem> QuyetDinhList { get; set; }

        public QuyetDinhThoiChucKiemNhiem(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            if (string.IsNullOrWhiteSpace(NoiDung))
                NoiDung = Common.CauHinhChung_GetCauHinhChung.CauHinhQuyetDinh.QuyetDinhThoiChucKiemNhiem;
            //
            QuyetDinhMoi = true;
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            //
            UpdateQuyetDinhList();
        }

        protected override void AfterNhanVienChanged()
        {
            //
            QuyetDinhBoNhiemKiemNhiem = null;
            //
            UpdateQuyetDinhList();
       }

        private void UpdateQuyetDinhList()
        {
            if (QuyetDinhList == null)
                QuyetDinhList = new XPCollection<QuyetDinhBoNhiemKiemNhiem>(Session);
            if (ThongTinNhanVien != null)
                QuyetDinhList.Criteria = CriteriaOperator.Parse("ThongTinNhanVien=?", ThongTinNhanVien.Oid);
        }

        protected override void OnSaving()
        {
            base.OnSaving();

            if (!IsDeleted)
            {

                if (QuyetDinhMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Vô hiệu hóa quyết định bổ nhiệm
                    QuyetDinhBoNhiemKiemNhiem.QuyetDinhMoi = false;

                    //Cập nhật thông tin lương
                    CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=?",this.QuyetDinhBoNhiemKiemNhiem.Oid);
                    ChucVuKiemNhiem chucVukiemNhiem = Session.FindObject<ChucVuKiemNhiem>(filter);
                    if (chucVukiemNhiem != null)
                        chucVukiemNhiem.DaMienNhiem = true;

                    JobUpdated = true;
                }

                if (NgayHieuLuc != DateTime.MinValue)
                    //Cập nhật đến ngày của quá trình bổ nhiệm
                    ProcessesHelper.UpdateQuaTrinhBoNhiem(Session, QuyetDinhBoNhiemKiemNhiem, NgayHieuLuc, true);
            }
        }

        protected override void OnDeleting()
        {
            if (!IsSaving)
            {
                //
                if (QuyetDinhMoi && NgayHieuLuc <= Common.GetServerCurrentTime())
                {
                    //Vô hiệu hóa quyết định bổ nhiệm
                    QuyetDinhBoNhiemKiemNhiem.QuyetDinhMoi = false;

                    //Cập nhật thông tin lương
                    CriteriaOperator filter = CriteriaOperator.Parse("QuyetDinh=?", this.QuyetDinhBoNhiemKiemNhiem.Oid);
                    ChucVuKiemNhiem chucVukiemNhiem = Session.FindObject<ChucVuKiemNhiem>(filter);
                    if (chucVukiemNhiem != null)
                        chucVukiemNhiem.DaMienNhiem = false;
                }

                if (NgayHieuLuc != DateTime.MinValue)
                    //Cập nhật đến ngày của quá trình bổ nhiệm
                    ProcessesHelper.UpdateQuaTrinhBoNhiem(Session, QuyetDinhBoNhiemKiemNhiem, NgayHieuLuc, false);
            }

            base.OnDeleting();
        }
    }
}
