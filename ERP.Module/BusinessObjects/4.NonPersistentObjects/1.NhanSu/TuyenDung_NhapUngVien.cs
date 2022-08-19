using System;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using System.Windows.Forms;
using System.Data;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.TuyenDung;
using ERP.Module.NonPersistentObjects.HeThong;
using DevExpress.Persistent.Base;

namespace ERP.Module.NonPersistentObjects.NhanSu
{
    [NonPersistent]
    [ModelDefault("Caption", "Import ứng viên từ file excel")]
    public class TuyenDung_NhapUngVien : OfficeBaseObject
    {
        // Fields...
        private NhuCauTuyenDung _NhuCauTuyenDung;

        [ModelDefault("Caption", "Vị trí tuyển dụng")]
        [RuleRequiredField(DefaultContexts.Save)]
        [DataSourceProperty("NhuCauTuyenDungList")]
        public NhuCauTuyenDung NhuCauTuyenDung
        {
            get
            {
                return _NhuCauTuyenDung;
            }
            set
            {
                SetPropertyValue("NhuCauTuyenDung", ref _NhuCauTuyenDung, value);
            }
        }

        public TuyenDung_NhapUngVien(Session session) : base(session) { }

        [Browsable(false)]
        public XPCollection<NhuCauTuyenDung> NhuCauTuyenDungList { get; set; }

        public void SetNhuCauTuyenDungList(QuanLyTuyenDung quanLyTuyenDung)
        {
            NhuCauTuyenDungList = new XPCollection<NhuCauTuyenDung>(Session,
                CriteriaOperator.Parse("QuanLyTuyenDung=?", quanLyTuyenDung.Oid));
        }

       
    }

}
