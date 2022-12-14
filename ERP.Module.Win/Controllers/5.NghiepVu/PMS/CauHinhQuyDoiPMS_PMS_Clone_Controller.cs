using System;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Templates;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using ERP.Module.Commons;
using DevExpress.XtraEditors;
using System.Data;
using ERP.Module.NghiepVu.PMS.CauHinh;
using ERP.Module.NonPersistentObjects;
using ERP.Module.NghiepVu.NhanSu.BoPhans;

namespace ERP.Module.Win.Controllers.PMS
{
    public partial class CauHinhQuyDoiPMS_PMS_Clone_Controller : ViewController
    {
        IObjectSpace _obs = null;
        CauHinhQuyDoiPMS clone;
        CollectionSource collectionSource;
        ThongTinClone _source;
        public CauHinhQuyDoiPMS_PMS_Clone_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "CauHinhQuyDoiPMS_DetailView";
        }
        //Bấm thực thi
        private void btnpop_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            clone = View.CurrentObject as CauHinhQuyDoiPMS;
            if (clone != null)
            {
                View.ObjectSpace.CommitChanges();
                SqlParameter[] pDongBo = new SqlParameter[4];
                pDongBo[0] = new SqlParameter("@CongTy", _source.CongTy.Oid);
                pDongBo[1] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                pDongBo[2] = new SqlParameter("@HocKy", _source.HocKy.Oid);
                pDongBo[3] = new SqlParameter("@Oid", clone.Oid);
                object kq = DataProvider.GetValueFromDatabase("spd_PMS_Clone_CauHinh", CommandType.StoredProcedure, pDongBo);
                if (kq != null)
                    XtraMessageBox.Show(kq.ToString(), "Thông báo!");
            }
        }
        //Show thông tin 
        private void btnpop_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            clone = View.CurrentObject as CauHinhQuyDoiPMS;
            if (clone != null)
            {
                _obs = Application.CreateObjectSpace();
                Session ses = ((XPObjectSpace)_obs).Session;
                collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));

                collectionSource = new CollectionSource(_obs, typeof(ThongTinClone));
                _source = new ThongTinClone(ses);
                _source.CongTy = ses.FindObject<CongTy>(CriteriaOperator.Parse("Oid = ?",  clone.ThongTinTruong.Oid));
                //_source.NamHoc = clone.NamHoc;
                e.View = Application.CreateDetailView(_obs, _source);
            }
        }
       
    }
}
