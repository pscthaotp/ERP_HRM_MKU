using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Security;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.DB;
using ERP.Module.NghiepVu.NhanSu.GiayTo;
using ERP.Module.Extends;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.NhanSu.NhanViens;

namespace ERP.Module.Win.Controllers.NghiepVu.NhanSu.GiayTo
{
    public partial class GiayTo_TaiTapTinController : ViewController
    {

        public GiayTo_TaiTapTinController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            GiayToHoSo obj = View.CurrentObject as GiayToHoSo;
            if (obj != null)
            {
                if (obj.LoaiGiayTo != null)
                {
                    DialogUtil.ShowWarning("Chọn loại giấy tờ !!!");
                    return;
                }
                //
                IObjectSpace obs = Application.CreateObjectSpace();
                GiayToHoSo fileNew = null;

                //Lấy đường dẫn máy chủ theo tên loại giấy tờ
                string dinhDangTenLoaiGiayTo = StringHelpers.ReplaceVietnameseChar(StringHelpers.ToTitleCase(obj.TenGiayTo)).Replace(" ", String.Empty);
                string filePath = string.Format("{0}/{1}/", Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.URL_GiayToHoSoNhanSu, dinhDangTenLoaiGiayTo);
                //
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Pdf files (*.pdf)|*.pdf";
                    dialog.DefaultExt = "Pdf files (*.pdf)|*.pdf";
                    dialog.Multiselect = false;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        //
                        string fileName = dialog.SafeFileName;
                        try
                        {
                            using (DialogUtil.AutoWait())
                            {

                                {// Tạo thư mục theo loại giấy tờ trên máy chủ
                                    FptProvider.CreateForder(filePath, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Username, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Password);
                                }

                                {//Tiến hành upload dữ liệu
                                    FptProvider.UploadFile(filePath, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Username, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Password, dialog.FileName, fileName);
                                }

                                {//Nếu upload thành công
                                    fileNew = obs.CreateObject<GiayToHoSo>();
                                    fileNew.STT = GetMaxSTT(obj);
                                    fileNew.HoSo = obs.GetObjectByKey<HoSo>(obj.HoSo.Oid);
                                    fileNew.LoaiGiayTo = obs.GetObjectByKey<GiayToHoSo>(obj.Oid);
                                    fileNew.TenGiayTo = fileName;
                                    fileNew.DuongDanFile = string.Format("{0}/{1}", filePath, fileName);
                                    fileNew.DuongDanFileWeb = string.Format("{0}/{1}", filePath, fileName);
                                    fileNew.NgayLap = Common.GetServerCurrentTime().Date;
                                    //fileNew.GhiChu = String.Concat(obj.GhiChu.Substring(1, 2), "_", obj.HoSo.MaTapDoan, "_", obj.HoSo.HoTen.ToUpper(), "_", fileName);
                                    //
                                    obs.CommitChanges();
                                    //
                                    View.ObjectSpace.Refresh();
                                }
                            }
                            DialogUtil.ShowInfo("Tải tập tin thành công.");
                        }
                        catch (Exception ex)
                        {
                            DialogUtil.ShowError("Tải tập tin không thành công: " + ex.Message);
                            //
                            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(fileName))
                            {
                                //
                                string fullPath = string.Format("{0}/{1}", filePath, fileName);
                                //
                                bool deleteSucess = FptProvider.DeleteFileOnServerNew(fullPath, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Username, Commons.Common.CauHinhChung_GetCauHinhChung.CauHinhSoHoa.Password);
                            }
                        }
                    }
                }
            }
        }

        private void GiayTo_TaiTapTinController_Activated(object sender, EventArgs e)
        {
            simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<GiayToHoSo>();
        }

        decimal GetMaxSTT(GiayToHoSo loaiGiayTo)
        {
            decimal maxSTT = 0;
            //
            CriteriaOperator filter = CriteriaOperator.Parse("LoaiGiayTo = ?", loaiGiayTo.Oid);
            XPCollection<GiayToHoSo> fileList = new XPCollection<GiayToHoSo>(((XPObjectSpace)View.ObjectSpace).Session, filter);
            fileList.Sorting.Add(new SortProperty("STT", SortingDirection.Descending));
            fileList.TopReturnedObjects = 1;
            if (fileList != null && fileList.Count > 0)
            {
                decimal stt = fileList[0].STT;
                //
                maxSTT = stt + 0.1m;
            }
            else
                maxSTT = loaiGiayTo.STT + 0.1m;
            //
            return maxSTT;
        }
    }
}
