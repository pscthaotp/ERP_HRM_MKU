using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.Commons;
using System.Data.SqlClient;
using System.Data;
using ERP.Module.NghiepVu.PMS.BangChotThuLao;
using System.Windows.Forms;
using ERP.Module.NonPersistentObjects;
using ERP.Module.Extends;
using System.Collections.Generic;
using System.Linq;
using DevExpress.XtraEditors;

namespace ERP.Module.Win.Controllers.NghiepVu.PMS
{
    public partial class BangChotThuLaoThinhGiangController : ViewController
    {
        BangChotThuLao_ThinhGiang qly;
        IObjectSpace _obs = null;
        Session _ses;
        CollectionSource collectionSource;
        QuanLyThongTinKLGiangDay_Non _source;
        public BangChotThuLaoThinhGiangController()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "BangChotThuLao_ThinhGiang_DetailView";
        }       

        private void DongBoBangChotThuLaoThinhGiangController_Activated(object sender, EventArgs e)
        {
            //simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ChungTu>();
        }

        private void simpleAction2_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            qly = View.CurrentObject as BangChotThuLao_ThinhGiang;
            if (qly.Khoa)
            {
                SqlParameter[] param1 = new SqlParameter[1];
                param1[0] = new SqlParameter("@BangChotThuLao", qly.Oid);
                //
                DataProvider.ExecuteNonQuery("spd_PMS_XoaBangChotThuLao_ThinhGiang", CommandType.StoredProcedure, param1);

                View.ObjectSpace.Refresh();
                MessageBox.Show("Mở khóa bảng chốt dữ liệu thành công!");
            }
            else
            {
                MessageBox.Show("Chưa có bảng chốt, vui lòng kiểm tra lại!");
            }
        }

        private void popupTinhThuLao_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            using (DialogUtil.AutoWait("Đang thực hiện thanh toán"))
            {
                if (_source != null)
                {
                    string listOidChiTiet = "";
                    if (_source != null)
                    {

                        List<dsThongTinKLGiangDay_Non> ds = (from d in _source.listTTKL
                                                             where d.Chon
                                                             select d).ToList();
                        foreach (dsThongTinKLGiangDay_Non item in ds)
                        {
                            listOidChiTiet += item.Oid_TTKL + ";";
                        }
                        if (ds.Count() <= 0)
                        {
                            XtraMessageBox.Show("Bạn chưa chọn bất kỳ dòng nào để tính thù lao!");
                            return;
                        }

                    }


                    #region
                    SqlCommand cmd = new SqlCommand("spd_PMS_BangChotThuLao_TinhThuLao", DataProvider.GetConnection());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BangChotThuLao_ThinhGiang", qly.Oid);
                    cmd.Parameters.AddWithValue("@Oid_TTKluong", listOidChiTiet);
                    //cmd.Parameters.AddWithValue("@User", Common.SecuritySystemUser_GetCurrentUser().UserName.ToString());
                    cmd.ExecuteNonQuery();
                    View.ObjectSpace.Refresh();
                    #endregion
                }
            }
        }

        private void popupTinhThuLao_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            qly = View.CurrentObject as BangChotThuLao_ThinhGiang;
            if (qly != null)
            {
                _obs = Application.CreateObjectSpace();
                _ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(dsThongTinKLGiangDay_Non));
                _source = new QuanLyThongTinKLGiangDay_Non(_ses);
                _source.ThongTinTruong = qly.ThongTinTruong;
                _source.NamHoc = qly.NamHoc;
                //_source.DotTinh = _bangchot.DotTinhPMS;
               _source.HocKy = qly.HocKy;
                _source.BangChotThuLao_ThinhGiang = qly.Oid;
                e.View = Application.CreateDetailView(_obs, _source);

            }
        }
    }
}
