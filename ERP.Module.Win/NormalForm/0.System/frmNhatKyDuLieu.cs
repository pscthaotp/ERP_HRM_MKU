using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ERP.Module.Win.Editors.Commons;
using ERP.Module.Extends;
using DevExpress.Xpo;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using ERP.Module.NonPersistentObjects.HeThong;
using System.Data.SqlClient;

namespace ERP.Module.Win.NormalForm.System
{
    public partial class frmNhatKyDuLieu : XtraForm
    {
        private Session _session;

        public frmNhatKyDuLieu(Session session)
        {
            InitializeComponent();
            //
            _session = session;

        }

        private void frmNhatKyDuLieu_Load(object sender, EventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                //
                dateTuNgay.EditValue = DateTime.Now.Date;
                dateDenNgay.EditValue = DateTime.Now.Date;
                //
                GridUtil.InitGridView(grid_NhatKyDuLieu);
                //
                List<AuditDataItemNonPer> dataSource = GetDataSource();
                //
                gridCtrl_NhatKyDuLieu.DataSource = dataSource;
            }
        }

        public List<AuditDataItemNonPer> GetDataSource()
        {

            DateTime tuNgay = Convert.ToDateTime(dateTuNgay.EditValue).SetTime(Enum.Systems.SetTimeEnum.StartDay);
            DateTime denNgay = Convert.ToDateTime(dateDenNgay.EditValue).SetTime(Enum.Systems.SetTimeEnum.EndDay);
            //
            DataTable dt = new DataTable();
            XPCollection<AuditDataItemNonPer> auditList = new XPCollection<AuditDataItemNonPer>(_session,false);
            //
            string query = @"SELECT UserName,
	                                   ModifiedOn,
	                                   OldValue,
	                                   NewValue,
	                                   PropertyName,
	                                   Description
                                FROM dbo.AuditDataItemPersistent
                                WHERE ModifiedOn BETWEEN @TuNgay AND @DenNgay
                                    AND UserName NOT LIKE N'%psc%'";

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@TuNgay", tuNgay);
            param[1] = new SqlParameter("@DenNgay", denNgay);

            SqlCommand cmd = DataProvider.GetCommand(query, CommandType.Text, param);
            cmd.Connection = DataProvider.GetConnection();
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    AuditDataItemNonPer chitiet = new AuditDataItemNonPer(_session);
                    chitiet.UserName = item["UserName"].ToString();
                    chitiet.ModifiedOn = Convert.ToDateTime(item["ModifiedOn"]);
                    chitiet.PropertyName = item["PropertyName"].ToString();
                    chitiet.OldValue = item["OldValue"].ToString();
                    chitiet.NewValue = item["NewValue"].ToString();
                    chitiet.Description = item["Description"].ToString();
                    auditList.Add(chitiet);
                }
            }
            //
            return auditList.ToList();
        }

        private void btnTim_Click(Object sender, EventArgs e)
        {
            using (DialogUtil.AutoWait())
            {
                List<AuditDataItemNonPer> dataSource = GetDataSource();
                //
                gridCtrl_NhatKyDuLieu.DataSource = dataSource;
                grid_NhatKyDuLieu.RefreshData();
            }
        }
    }
}