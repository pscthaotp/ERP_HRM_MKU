using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Layout;
using DevExpress.XtraEditors;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using System.Data;
using DevExpress.Utils;
using System.Data.SqlClient;
using ERP.Module.NghiepVu.NhanSu.NhanViens;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;
using System.Text.RegularExpressions;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.NhanViens
{
    public partial class NhanVien_TimKiemThinhGiangController : ViewController
    {
        public NhanVien_TimKiemThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void NhanVien_TimKiemThinhGiangController_ViewControlsCreated(object sender, EventArgs e)
        {
            DetailView view = View as DetailView;

            if (view != null)
            {
                foreach (ControlViewItem item in view.GetItems<ControlViewItem>())
                {
                    if (item.Id == "btnSearch")
                    {
                        SimpleButton btnSearch = item.Control as SimpleButton;

                        if (btnSearch != null)
                        {
                            btnSearch.Text = "Tìm kiếm";
                            btnSearch.Width = 80;
                            btnSearch.Click += (obj, ea) =>
                            {
                                TimKiemThinhGiang search = view.CurrentObject as TimKiemThinhGiang;
                                if (search != null)
                                {
                                    XPCollection<GiangVienThinhGiang> nvList;
                                    CriteriaOperator criteria;
                                    ChiTietTimKiemThinhGiang data;
                                    if (!String.IsNullOrWhiteSpace(search.DieuKienTimKiem))
                                    {
                                        string temp = search.DieuKienTimKiem;
                                        temp = ThayDoiDieuKienBoPhan(temp);
                                        criteria = CriteriaEditorHelper.GetCriteriaOperator(temp, typeof(GiangVienThinhGiang), View.ObjectSpace);
                                        nvList = new XPCollection<GiangVienThinhGiang>(((XPObjectSpace)View.ObjectSpace).Session, criteria);
                                    }
                                    else
                                    {
                                        criteria = new InOperator("BoPhan.Oid", Common.Department_GetRoledDepartmentList_BySession(((XPObjectSpace)View.ObjectSpace).Session));
                                        nvList = new XPCollection<GiangVienThinhGiang>(((XPObjectSpace)View.ObjectSpace).Session, criteria);
                                    }
                                    if (search.ListChiTietTimKiemThinhGiang == null)
                                        search.ListChiTietTimKiemThinhGiang = new XPCollection<ChiTietTimKiemThinhGiang>(((XPObjectSpace)View.ObjectSpace).Session, false);
                                    else
                                        search.ListChiTietTimKiemThinhGiang.Reload();
                                    foreach (GiangVienThinhGiang nvItem in nvList)
                                    {
                                        data = new ChiTietTimKiemThinhGiang(((XPObjectSpace)View.ObjectSpace).Session)
                                        {
                                            ThinhGiang = nvItem
                                        };
                                        search.ListChiTietTimKiemThinhGiang.Add(data);
                                    }
                                    View.Refresh();
                                }
                            };
                        }
                    }
                }
            }
        }

        private string ThayDoiDieuKienBoPhan(string criteria)
        {
            IObjectSpace obs = Application.CreateObjectSpace();

            if (criteria.Contains("[BoPhan]"))
            {
                criteria = criteria.Replace("[BoPhan]", "BoPhan");

                // in, not in bo phan operatorERP.Module.NghiepVu.NhanSu.BoPhans.BoPhan
                criteria = Regex.Replace(criteria, "BoPhan In ([(]##XpoObject#ERP.Module.NghiepVu.NhanSu.BoPhans.BoPhan[(][{]\\w{8}(-\\w{4}){3}-\\w{12}[}][)]#)+(, ##XpoObject#ERP.Module.NghiepVu.NhanSu.BoPhans.BoPhan[(][{]\\w{8}(-\\w{4}){3}-\\w{12}[}][)]#)*[)]", match =>
                {
                    string temp = match.Value;
                    MatchCollection matches = Regex.Matches(temp, "\\w{8}(-\\w{4}){3}-\\w{12}");
                    List<string> oid = new List<string>();
                    if (matches.Count > 0)
                    {
                        Guid boPhanID;
                        BoPhan bp;

                        foreach (Match matche in matches)
                        {
                            if (Guid.TryParse(matche.Value, out boPhanID))
                            {
                                bp = obs.GetObjectByKey<BoPhan>(boPhanID);
                                List<string> oidTemp = Common.Department_GetRoledDepartmentList_ByDepartment(bp);
                                if (oidTemp.Count > 0)
                                    oid.AddRange(oidTemp);
                            }
                        }
                    }

                    StringBuilder sb = new StringBuilder("[BoPhan.Oid] In (");
                    bool state = false;
                    foreach (string item in oid)
                    {
                        if (state)
                            sb.Append(", ");
                        else
                            state = true;
                        sb.Append("##XpoObject#ERP.Module.NghiepVu.NhanSu.BoPhans.BoPhan({");
                        sb.Append(item);
                        sb.Append("})#");
                    }
                    sb.Append(")");
                    return sb.ToString();
                });

                // =, <> bo phan operator
                criteria = Regex.Replace(criteria, "BoPhan [<>=]+ ##XpoObject#ERP.Module.NghiepVu.NhanSu.BoPhans.BoPhan[(][{]\\w{8}(-\\w{4}){3}-\\w{12}[}][)]#", match =>
                {
                    string temp = match.Value;
                    Match match1 = Regex.Match(temp, "[<>=]+");
                    if (match1.Success)
                    {
                        StringBuilder sb;
                        if (match1.Value == "=")
                            sb = new StringBuilder("[BoPhan.Oid] In (");
                        else
                            sb = new StringBuilder("Not [BoPhan.Oid] In (");

                        Match matche = Regex.Match(temp, "\\w{8}(-\\w{4}){3}-\\w{12}");
                        List<string> oid = new List<string>();
                        if (matche.Success)
                        {
                            Guid boPhanID;
                            BoPhan bp;
                            if (Guid.TryParse(matche.Value, out boPhanID))
                            {
                                bp = obs.GetObjectByKey<BoPhan>(boPhanID);
                                List<string> oidTemp = Common.Department_GetRoledDepartmentList_ByDepartment(bp);
                                if (oidTemp.Count > 0)
                                    oid.AddRange(oidTemp);
                            }
                        }

                        bool state = false;
                        foreach (string item in oid)
                        {
                            if (state)
                                sb.Append(", ");
                            else
                                state = true;
                            sb.Append("##XpoObject#ERP.Module.NghiepVu.NhanSu.BoPhans.BoPhan({");
                            sb.Append(item);
                            sb.Append("})#");
                        }
                        sb.Append(")");

                        return sb.ToString();
                    }

                    return temp;
                });
            }
            else
            {
                List<string> oidTemp = Common.Department_GetRoledDepartmentList_BySession(((XPObjectSpace)View.ObjectSpace).Session);

                StringBuilder sb = new StringBuilder(" AND [BoPhan.Oid] In (");
                bool state = false;
                foreach (string item in oidTemp)
                {
                    if (state)
                        sb.Append(", ");
                    else
                        state = true;
                    sb.Append("##XpoObject#ERP.Module.NghiepVu.NhanSu.BoPhans.BoPhan({");
                    sb.Append(item);
                    sb.Append("})#");
                }
                sb.Append(")");

                criteria += sb.ToString();
            }

            return criteria;
        }
    }
}
