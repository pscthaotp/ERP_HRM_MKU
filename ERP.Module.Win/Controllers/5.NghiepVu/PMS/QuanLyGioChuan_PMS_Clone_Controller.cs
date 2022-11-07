using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Actions;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using System.Data.SqlClient;
using ERP.Module.Commons;
using DevExpress.XtraEditors;
using System.Data;
using ERP.Module.NonPersistentObjects;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using ERP.Module.NghiepVu.PMS.QuanLyGioChuan;

namespace ERP.Module.Win.Controllers.PMS
{
    public partial class QuanLyGioChuan_PMS_Clone_Controller : ViewController
    {
        IObjectSpace _obs = null;
        QuanLyGioChuan clone;
        CollectionSource collectionSource;
        ThongTinClone _source;
        public QuanLyGioChuan_PMS_Clone_Controller()
        {
            InitializeComponent();
            RegisterActions(components);
            TargetViewId = "QuanLyGioChuan_DetailView";
        }
        //Bấm thực thi
        private void btnpop_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            clone = View.CurrentObject as QuanLyGioChuan;
            if (clone != null)
            {
                View.ObjectSpace.CommitChanges();
                SqlParameter[] pDongBo = new SqlParameter[4];
                pDongBo[0] = new SqlParameter("@CongTy", _source.CongTy.Oid);
                pDongBo[1] = new SqlParameter("@NamHoc", _source.NamHoc.Oid);
                pDongBo[2] = new SqlParameter("@HocKy", _source.HocKy.Oid);
                pDongBo[3] = new SqlParameter("@Oid", clone.Oid);
                object kq = DataProvider.GetValueFromDatabase("spd_PMS_Clone_QuanLyGioChuan", CommandType.StoredProcedure, pDongBo);
                if (kq != null)
                    XtraMessageBox.Show(kq.ToString(), "Thông báo!");
            }
        }
        //Show thông tin 
        private void btnpop_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            clone = View.CurrentObject as QuanLyGioChuan;
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
