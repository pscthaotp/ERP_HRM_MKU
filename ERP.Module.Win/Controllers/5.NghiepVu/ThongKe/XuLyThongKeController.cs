using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Commons;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Data.Filtering;
using System.Drawing;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.ThongKe
{
    public partial class XuLyThongKeController : ViewController
    {
        private PanelControl panel;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private ThongKeBaseController control;
        private System.Windows.Forms.CheckBox checkBox;
        private System.Windows.Forms.ComboBox cb;
        private System.Windows.Forms.Button button;
        private XPCollection<CongTy> congTyList;
        private bool laQuanTri = false;

        public XuLyThongKeController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }

        private void XuLyThongKeController_ViewControlsCreated(object sender, EventArgs e)
        {
            DashboardView view = View as DashboardView;            
            if (view != null && view.Id == "ThongKe_DashboardView")
            {
                ControlViewItem itemThongKe = ((DashboardView)View).FindItem("CustomControl") as ControlViewItem;
                //
                if (itemThongKe!=null)
                {
                    panel = itemThongKe.Control as PanelControl;
                    //
                    IObjectSpace obs = Application.CreateObjectSpace();

                    if (panel != null)
                    {                    
                        #region Chart
                        control = ThongKeFactory.CreateControl(Application, View.ObjectSpace);
                        view.Caption = control.Caption;

                        int left = CalculatorLocation(panel.Width, control.Width);
                        int top = CalculatorLocation(panel.Height, control.Height);
                        control.SetLocation(50, left + 50);
                        //
                        panel.Controls.Clear();
                        panel.Controls.Add(control);
                        panel.Dock = DockStyle.Fill;
                        control.PerformLayout();
                        #endregion

                        #region Control
                        //Button
                        button = new System.Windows.Forms.Button();
                        button.Text = "Lọc";
                        button.Location = new System.Drawing.Point(700, 0);
                        button.Click += button_Click;
                        panel.Controls.Add(button);

                        //Check box
                        checkBox = new System.Windows.Forms.CheckBox();
                        checkBox.Text = "Tất cả";
                        checkBox.Location = new System.Drawing.Point(50, 0);
                        checkBox.Checked = true;
                        checkBox.CheckedChanged += checkBox_CheckedChanged;
                        panel.Controls.Add(checkBox);

                        //Label
                        System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                        label.Text = "Tính đến ngày";
                        label.TextAlign = ContentAlignment.MiddleCenter;;
                        label.Location = new System.Drawing.Point(150, 0);
                        //label.Size = new System.Drawing.Size(50, 10);
                        panel.Controls.Add(label);

                        //DateTimePicker
                        dateTimePicker = new System.Windows.Forms.DateTimePicker();
                        dateTimePicker.Location = new System.Drawing.Point(250, 0);
                        dateTimePicker.Size = new System.Drawing.Size(150, 10);
                        dateTimePicker.Value = DateTime.Now;
                        dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
                        dateTimePicker.CustomFormat = "dd/MM/yyyy";                      
                        panel.Controls.Add(dateTimePicker);

                        //Combobox   
                        cb = new System.Windows.Forms.ComboBox();
                        congTyList = new XPCollection<CongTy>(((XPObjectSpace)View.ObjectSpace).Session);

                        if (Common.QuanTriKhoi())
                        {
                            laQuanTri = true;                           
                            checkBox.Visible = true;
                            cb.Visible = false;

                            List<string> lstBP = Common.Department_GetRoledDepartmentList_ByDepartment(null);
                            StringBuilder sb = new StringBuilder();
                            foreach (string item in lstBP)
                            {
                                sb.Append(String.Format("{0},", item));
                            }
                            List<Guid> resultList = DataProvider.GetGuidList("spd_HeThong_DanhSachCongTyDuocPhanQuyen", CommandType.StoredProcedure, new SqlParameter("@BoPhanPhanQuyen", sb.ToString()));
                            congTyList.Criteria = new InOperator("Oid", resultList);
                        }
                        else if (Common.QuanTriToanCongTy() || Common.TaiKhoanBinhThuong())
                        {                           
                            checkBox.Visible = false;
                            cb.Visible = false;
                            laQuanTri = false;
                        }
                        else
                        {
                            laQuanTri = true;                            
                            checkBox.Visible = true;
                            cb.Visible = false;
                        }
                        
                        cb.DataSource = congTyList;
                        cb.DisplayMember = "TenBoPhan";
                        cb.ValueMember = "Oid";                        
                        //                                        
                        cb.Size = new System.Drawing.Size(300, 10);
                        cb.Location = new System.Drawing.Point(400, 0);
                        //
                        cb.SelectedValueChanged += cb_SelectedValueChanged;
                        panel.Controls.Add(cb);
                        #endregion

                        panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                        panel.Dock = DockStyle.Fill;
                        control.PerformLayout();
                    }
                }
            }
        }

        private void cb_SelectedValueChanged(object sender, EventArgs e)
        {
            if (panel.Controls[0] is ThongKeBaseController)
            {
                ((ThongKeBaseController)panel.Controls[0]).LoadData((Guid)((System.Windows.Forms.ComboBox)sender).SelectedValue, dateTimePicker.Value, laQuanTri);
            }
        }

        private void checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (panel.Controls[0] is ThongKeBaseController)
            {
                if (checkBox.Checked)
                {
                    cb.Visible = false;
                    ((ThongKeBaseController)panel.Controls[0]).LoadData(Guid.Empty, dateTimePicker.Value, laQuanTri);
                }
                else 
                {
                    cb.Visible = true;
                }
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (panel.Controls[0] is ThongKeBaseController)
            { 
                if (!checkBox.Checked && dateTimePicker.Value != DateTime.MinValue)
                    ((ThongKeBaseController)panel.Controls[0]).LoadData((Guid)(cb.SelectedValue), dateTimePicker.Value, laQuanTri);
                else
                    ((ThongKeBaseController)panel.Controls[0]).LoadData(Guid.Empty, dateTimePicker.Value, laQuanTri);
            }
        }

        private int CalculatorLocation(int width1, int width2)
        {
            int location = 0;
            if (width1 > width2)
                location = (width1 - width2) / 2;

            return location;
        }   
    }
}
