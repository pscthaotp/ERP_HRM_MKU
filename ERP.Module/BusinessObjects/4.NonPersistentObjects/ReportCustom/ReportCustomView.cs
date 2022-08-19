using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.Model;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Xpo.DB;
using ERP.Module.BaoCao.Custom;
using DevExpress.Xpo.Metadata;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using DevExpress.ExpressApp.ConditionalAppearance;

namespace ERP.Module.NonPersistentObjects.ReportCustom
{
    [NonPersistent]
    [ImageName("Action_New")]
    [ModelDefault("Caption", "Báo cáo")]
    [Appearance("ToMau_TenBaoCao", TargetItems = "TenBaoCao", BackColor = "chartreuse", FontColor = "Red")]
    public class ReportCustomView : BaseObject
    {
        private string _TenBaoCao;
        private int _OidReport;
        private Guid _Oid;
        private Guid _PhanHe;

        [ModelDefault("Caption", "Báo cáo")]
        [ModelDefault("AllowEdit", "False")]
        public string TenBaoCao
        {
            get { return _TenBaoCao; }
            set { SetPropertyValue("TenBaoCao", ref _TenBaoCao, value); }
        }
        [Delayed]
        [ModelDefault("Caption", "Hình ảnh")]
        [Size(SizeAttribute.Unlimited)]
        [ValueConverter(typeof(ImageValueConverter))]
        [VisibleInListView(false)]
        public Image HinhAnh
        {
            get
            {
                return GetDelayedPropertyValue<Image>("HinhAnh");
            }
            set
            {
                SetDelayedPropertyValue<Image>("HinhAnh", value);
            }
        }

        [Browsable(false)]
        public int OidReport
        {
            get
            {
                return _OidReport;
            }
            set
            {
                SetPropertyValue("OidReport", ref _OidReport, value);
            }
        }

        [Browsable(false)]
        public Guid Oid
        {
            get
            {
                return _Oid;
            }
            set
            {
                SetPropertyValue("Oid", ref _Oid, value);
            }
        }

        [Browsable(false)]
        [ImmediatePostData]
        public Guid PhanHe
        {
            get
            {
                return _PhanHe;
            }
            set
            {
                SetPropertyValue("PhanHe", ref _PhanHe, value);
                if (!IsLoading)
                    LoadPhanHe(PhanHe);
            }
        }

        [ModelDefault("Caption", "Danh sách báo cáo")]
        public XPCollection<ReportViewGroup> ReportViewList { get; set; }
        public ReportCustomView(Session session) : base(session) { }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
            ReportViewList = new XPCollection<ReportViewGroup>(Session, false);

        }
        public void LoadPhanHe(Guid PhanHe)
        {
            var user = Common.SecuritySystemUser_GetCurrentUser(Session).Oid.ToString();
            SqlParameter[] p = new SqlParameter[2];
            p[0] = new SqlParameter("@User", user);
            p[1] = new SqlParameter("@PhanHe", PhanHe);
            SqlCommand cmd = DataProvider.GetCommand("spd_HeThong_GetMenuReport", CommandType.StoredProcedure, p);
            DataSet dts = DataProvider.GetDataSet(cmd);
            DataTable dt = dts.Tables[1];
            foreach (DataRow r in dt.Rows)
            {
                ReportViewGroup rptView = new ReportViewGroup(Session);
                if (r["TenNhom"].ToString() != string.Empty)
                    rptView.TenNhom = r["TenNhom"].ToString();
                if (r["TenReport"].ToString() != string.Empty)
                    rptView.TenReport = r["TenReport"].ToString();
                if (r["OidReport"].ToString() != string.Empty)
                    rptView.ID = Convert.ToInt32(r["OidReport"].ToString());
                rptView.ReportCustomView = this;
                rptView.Group = new Guid(r["GroupReport"].ToString());
                rptView.ListGroup = new XPCollection<ReportViewGroup>(Session, false);
                //
                ReportViewList.Add(rptView);
            }


            foreach (var item in ReportViewList)
            {
                var i = item.Group.ToString();
                if (item.Group != null && item.TenNhom == null)
                {
                    int indexCha = ReportViewList.FindIndex<ReportViewGroup>(s => s.Group.ToString() == item.Group.ToString() && item.ID.ToString() != string.Empty);
                    //int index = ReportViewList.FindIndex<ReportViewGroup>(s => s.Oid.ToString() == item.Oid.ToString() && item.TenNhom == null);
                    //
                    if (indexCha >= 0)
                    {
                        item.ReportCha = ReportViewList[indexCha];
                        ReportViewList[indexCha].ListGroup.Add(item);
                    }
                }
            }
        }
    }
}
