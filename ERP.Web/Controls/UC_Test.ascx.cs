using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Web;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Web;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ERP.Web.Controls
{
    public partial class UC_Test : System.Web.UI.UserControl
    {
        // 
        public void UpdateDataSource(DevExpress.Xpo.Session session)
        {

        }
        protected override void OnInit(EventArgs e)
        {
            int number = 4;
            int left = 0;
            int top = 180;
            int addtop = 180;
            int addleft = 300;
            for (int i = 0; i <= number; i++)
            {
                if (i % 2 == 0)
                {
                    left += addleft;
                    top = 180;
                    //
                    ASPxDockPanel dockPanel = new ASPxDockPanel();
                    dockPanel.HeaderText = "THÔNG BÁO";
                    dockPanel.Text = "Có 1 kế hoạch tuyển sinh cần duyệt.";
                    dockPanel.ForeColor = System.Drawing.Color.DarkGreen;
                    dockPanel.BackColor = System.Drawing.Color.OrangeRed;
                    dockPanel.Left = left;
                    dockPanel.Top = top;
                    dockPanel.Width = 250;
                    dockPanel.Height = 100;
                    dockPanel.ID = "dockPanel_" + i.ToString();
                    dockPanel.Theme = "Office2010Blue";
                    dockPanel.ShowFooter = true;
                    dockPanel.FooterText = "";
                    this.Controls.Add(dockPanel);
                }
                else
                {
                    top += addtop;
                    //
                    ASPxDockPanel dockPanel = new ASPxDockPanel();
                    dockPanel.HeaderText = "THÔNG BÁO";
                    dockPanel.Text = "Có 2 kế hoạch tuyển sinh cần duyệt.";
                    dockPanel.ForeColor = System.Drawing.Color.DarkGreen;
                    dockPanel.BackColor = System.Drawing.Color.OrangeRed;
                    dockPanel.Left = left;
                    dockPanel.Top = top;
                    dockPanel.Width = 250;
                    dockPanel.Height = 100;
                    dockPanel.ID = "dockPanel_" + i.ToString();
                    dockPanel.Theme = "Office2010Blue";
                    dockPanel.ShowFooter = true;
                    dockPanel.FooterText = "";
                    this.Controls.Add(dockPanel);
                }
            }
        }
    }
}