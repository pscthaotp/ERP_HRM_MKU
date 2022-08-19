using System;
using System.Collections.Generic;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.ExpressApp.Model;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.BaoCao.Custom
{
    [NonPersistent]
    [ModelDefault("Caption", "Chi tiết xuất")]
    public class DetailExport : BaseObject
    {
        // 
        private CongTy _CongTy;
        private GroupReport _GroupReport;
        private ReportData_Custom _Report;
        private bool _Chon;

        [ModelDefault("Caption", "Chọn")]
        public bool Chon
        {
            get
            {
                return _Chon;
            }
            set
            {
                SetPropertyValue("Chon", ref _Chon, value);
            }
        }

        [ModelDefault("Caption", "Trường")]
        public CongTy CongTy
        {
            get
            {
                return _CongTy;
            }
            set
            {
                SetPropertyValue("CongTy", ref _CongTy, value);
            }
        }

        [ImmediatePostData]
        [ModelDefault("Caption", "Báo cáo")]
        public ReportData_Custom Report
        {
            get
            {
                return _Report;
            }
            set
            {
                SetPropertyValue("Report", ref _Report, value);
                if (!IsLoading && value != null)
                    GroupReport = value.GroupReport;
            }
        }

        [ModelDefault("Caption", "Nhóm báo cáo")]
        public GroupReport GroupReport
        {
            get
            {
                return _GroupReport;
            }
            set
            {
                SetPropertyValue("GroupReport", ref _GroupReport, value);
            }
        }

        public DetailExport(Session session) : base(session) { }
    }
}
