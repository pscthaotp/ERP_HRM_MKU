using System;
using System.Collections.Generic;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.Data.Filtering;
using ERP.Module.Commons;
using ERP.Module.NghiepVu.TienLuong.ChungTus;
using ERP.Module.BaoCao.Custom;
using ERP.Module.Report.TienLuong;
using ERP.Module.NghiepVu.NhanSu.BoPhans;
using System.Text;
using ERP.Module.DanhMuc.TienLuong;
using DevExpress.ExpressApp.DC;
using DevExpress.XtraReports.UI;
using ERP.Module.Extends;
using DevExpress.Spreadsheet;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
//
namespace ERP.Module.Win.Controller.BaoCao
{
    public partial class BaoCao_ExportToWorkBookBaoCaoTienLuongController : ViewController
    {
        private IObjectSpace _obs;
        private ChungTu _chungTu;        

        public BaoCao_ExportToWorkBookBaoCaoTienLuongController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        private void BaoCao_ExportToWorkBookBaoCaoTienLuongController_Activated(object sender, EventArgs e)
        {
            //simpleAction1.Active["TruyCap"] = Common.IsWriteGranted<ChungTu>(); ;           
            simpleAction1.Active["TruyCap"] = false;
        }

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Lưu dữ liệu 
            View.ObjectSpace.CommitChanges();
            //
            _chungTu = View.CurrentObject as ChungTu;
            if (_chungTu != null && _chungTu.KyTinhLuong != null)
            {
                _obs = Application.CreateObjectSpace();         

                List<string> listBP = new List<string>();
                //               
                listBP = Common.Department_GetRoledDepartmentList_ByCurrentUser();
                //
                StringBuilder roled = new StringBuilder();
                foreach (string item in listBP)
                {
                    roled.Append(String.Format("{0};", item));
                }

                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.Filter = "Excel files (*.xls)|*.xls";
                    dialog.DefaultExt = "Excel files (*.xls)|*.xls";
                    dialog.Multiselect = false;                    

                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        string exf_BangChamCongThang = dialog.FileName;

                        string folderDirectory = dialog.FileName.Replace(dialog.SafeFileName,string.Empty);

                        if (string.IsNullOrEmpty(folderDirectory))
                            folderDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Downloads\";

                        using (DialogUtil.AutoWait("Hệ thống đang truy xuất dữ liệu. \nVui lòng chờ!"))
                        {
                            //In report danh sách kèm theo
                            string exf_TongHopThanhToanTienLuong = string.Empty;
                            XtraReport xrpt_TongHopThanhToanTienLuong = null;
                            ReportData_Custom report_TongHopThanhToanTienLuongPhanTich = _obs.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", "1332"));
                            ITypeInfo type_BaoCao_Luong_BangTongHopThanhToanTienLuong = _obs.TypesInfo.FindTypeInfo("ERP.Module.Report.TienLuong.BaoCao_Luong_BangTongHopThanhToanTienLuong");
                            if (type_BaoCao_Luong_BangTongHopThanhToanTienLuong != null)
                            {
                                StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type_BaoCao_Luong_BangTongHopThanhToanTienLuong.Type);
                                if (rpt != null)
                                {
                                    if (report_TongHopThanhToanTienLuongPhanTich != null)
                                    {
                                        //Truyền parameter
                                        ((BaoCao_Luong_BangTongHopThanhToanTienLuong)rpt).CongTy = _obs.GetObjectByKey<CongTy>(_chungTu.CongTy.Oid);
                                        ((BaoCao_Luong_BangTongHopThanhToanTienLuong)rpt).KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_chungTu.KyTinhLuong.Oid);
                                        ((BaoCao_Luong_BangTongHopThanhToanTienLuong)rpt).TatCa = true;
                                        ((BaoCao_Luong_BangTongHopThanhToanTienLuong)rpt).BoPhan = null;
                                        //     
                                        StoreProcedureReport.Param = rpt;
                                        //                                                       
                                        xrpt_TongHopThanhToanTienLuong = report_TongHopThanhToanTienLuongPhanTich.LoadReport(_obs);
                                        exf_TongHopThanhToanTienLuong = folderDirectory + report_TongHopThanhToanTienLuongPhanTich.ReportName + "-" + _chungTu.KyTinhLuong.TenKy + ".xlsx";
                                        xrpt_TongHopThanhToanTienLuong.ExportToXlsx(exf_TongHopThanhToanTienLuong, new DevExpress.XtraPrinting.XlsxExportOptions() { SheetName = "BangLuong" });
                                    }
                                }
                            }

                            string exf_TongHopCacKhoanNgoaiKhoa = string.Empty;
                            XtraReport xrpt_TongHopCacKhoanNgoaiKhoa = null;
                            ReportData_Custom report_TongHopCacKhoanNgoaiKhoa = _obs.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", "1333"));
                            ITypeInfo type_BaoCao_Luong_BangChiTietNgoaiKhoa = _obs.TypesInfo.FindTypeInfo("ERP.Module.Report.TienLuong.BaoCao_Luong_BangChiTietThuNhapKhac");
                            if (type_BaoCao_Luong_BangChiTietNgoaiKhoa != null)
                            {
                                StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type_BaoCao_Luong_BangChiTietNgoaiKhoa.Type);
                                if (rpt != null)
                                {
                                    if (report_TongHopCacKhoanNgoaiKhoa != null)
                                    {
                                        //Truyền parameter
                                        ((BaoCao_Luong_BangChiTietThuNhapKhac)rpt).CongTy = _obs.GetObjectByKey<CongTy>(_chungTu.CongTy.Oid);
                                        ((BaoCao_Luong_BangChiTietThuNhapKhac)rpt).KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_chungTu.KyTinhLuong.Oid);
                                        ((BaoCao_Luong_BangChiTietThuNhapKhac)rpt).TatCa = true;
                                        ((BaoCao_Luong_BangChiTietThuNhapKhac)rpt).BoPhan = null;
                                        //                             
                                        StoreProcedureReport.Param = rpt;
                                        //   
                                        xrpt_TongHopCacKhoanNgoaiKhoa = report_TongHopCacKhoanNgoaiKhoa.LoadReport(_obs);
                                        exf_TongHopCacKhoanNgoaiKhoa = folderDirectory + report_TongHopCacKhoanNgoaiKhoa.ReportName + "-" + _chungTu.KyTinhLuong.TenKy + ".xlsx";
                                        xrpt_TongHopCacKhoanNgoaiKhoa.ExportToXlsx(exf_TongHopCacKhoanNgoaiKhoa, new DevExpress.XtraPrinting.XlsxExportOptions() { SheetName = "BangNgoaiKhoa" });
                                    }
                                }
                            }

                            string exf_TongHopCacKhoanThuNhapKhac = string.Empty;
                            XtraReport xrpt_TongHopCacKhoanThuNhapKhac = null;
                            ReportData_Custom report_TongHopCacKhoanThuNhapKhac = _obs.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", "1334"));
                            ITypeInfo type_BaoCao_Luong_BangChiTietThuNhapKhac = _obs.TypesInfo.FindTypeInfo("ERP.Module.Report.TienLuong.BaoCao_Luong_BangChiTietThuNhapKhac");
                            if (type_BaoCao_Luong_BangChiTietThuNhapKhac != null)
                            {
                                StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type_BaoCao_Luong_BangChiTietNgoaiKhoa.Type);
                                if (rpt != null)
                                {
                                    if (report_TongHopCacKhoanThuNhapKhac != null)
                                    {
                                        //Truyền parameter
                                        ((BaoCao_Luong_BangChiTietThuNhapKhac)rpt).CongTy = _obs.GetObjectByKey<CongTy>(_chungTu.CongTy.Oid);
                                        ((BaoCao_Luong_BangChiTietThuNhapKhac)rpt).KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_chungTu.KyTinhLuong.Oid);
                                        ((BaoCao_Luong_BangChiTietThuNhapKhac)rpt).TatCa = true;
                                        ((BaoCao_Luong_BangChiTietThuNhapKhac)rpt).BoPhan = null;
                                        //                             
                                        StoreProcedureReport.Param = rpt;
                                        //    
                                        xrpt_TongHopCacKhoanThuNhapKhac = report_TongHopCacKhoanThuNhapKhac.LoadReport(_obs);
                                        exf_TongHopCacKhoanThuNhapKhac = folderDirectory + report_TongHopCacKhoanThuNhapKhac.ReportName + " " + _chungTu.KyTinhLuong.TenKy + ".xlsx";
                                        xrpt_TongHopCacKhoanThuNhapKhac.ExportToXlsx(exf_TongHopCacKhoanThuNhapKhac, new DevExpress.XtraPrinting.XlsxExportOptions() { SheetName = "BangThuNhapKhac" });
                                    }
                                }
                            }

                            string exf_BangTrichNopBaoHiem = string.Empty;
                            XtraReport xrpt_BangTrichNopBaoHiem = null;
                            ReportData_Custom report_BangTrichNopBaoHiem = _obs.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", "1111"));
                            ITypeInfo type_BaoCao_Luong_BangTrichNopBaoHiem = _obs.TypesInfo.FindTypeInfo("ERP.Module.Report.TienLuong.BaoCao_Luong_BangTrichNopBaoHiem");
                            if (type_BaoCao_Luong_BangTrichNopBaoHiem != null)
                            {
                                StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type_BaoCao_Luong_BangTrichNopBaoHiem.Type);
                                if (rpt != null)
                                {
                                    if (report_BangTrichNopBaoHiem != null)
                                    {
                                        //Truyền parameter
                                        ((BaoCao_Luong_BangTrichNopBaoHiem)rpt).CongTy = _obs.GetObjectByKey<CongTy>(_chungTu.CongTy.Oid);
                                        ((BaoCao_Luong_BangTrichNopBaoHiem)rpt).KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_chungTu.KyTinhLuong.Oid);
                                        //                             
                                        StoreProcedureReport.Param = rpt;
                                        //  
                                        xrpt_BangTrichNopBaoHiem = report_BangTrichNopBaoHiem.LoadReport(_obs);
                                        exf_BangTrichNopBaoHiem = folderDirectory + report_BangTrichNopBaoHiem.ReportName + "-" + _chungTu.KyTinhLuong.TenKy + ".xlsx";
                                        xrpt_BangTrichNopBaoHiem.ExportToXlsx(exf_BangTrichNopBaoHiem, new DevExpress.XtraPrinting.XlsxExportOptions() { SheetName = "BangTrichNopBaoHiem" });
                                    }
                                }
                            }

                            string exf_BangTrichNopCongDoan = string.Empty;
                            XtraReport xrpt_BangTrichNopCongDoan = null;
                            ReportData_Custom report_BangTrichNopCongDoan = _obs.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", "1219"));
                            ITypeInfo type_BaoCao_Luong_BangTrichNopCongDoan = _obs.TypesInfo.FindTypeInfo("ERP.Module.Report.TienLuong.BaoCao_Luong_BangTrichNopCongDoan");
                            if (type_BaoCao_Luong_BangTrichNopCongDoan != null)
                            {
                                StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type_BaoCao_Luong_BangTrichNopCongDoan.Type);
                                if (rpt != null)
                                {
                                    if (report_BangTrichNopCongDoan != null)
                                    {
                                        //Truyền parameter
                                        ((BaoCao_Luong_BangTrichNopCongDoan)rpt).CongTy = _obs.GetObjectByKey<CongTy>(_chungTu.CongTy.Oid);
                                        ((BaoCao_Luong_BangTrichNopCongDoan)rpt).KyTinhLuong = _obs.GetObjectByKey<KyTinhLuong>(_chungTu.KyTinhLuong.Oid);
                                        //                             
                                        StoreProcedureReport.Param = rpt;
                                        // 
                                        xrpt_BangTrichNopCongDoan = report_BangTrichNopCongDoan.LoadReport(_obs);
                                        exf_BangTrichNopCongDoan = folderDirectory + report_BangTrichNopCongDoan.ReportName + "-" + _chungTu.KyTinhLuong.TenKy + ".xlsx";
                                        xrpt_BangTrichNopCongDoan.ExportToXlsx(exf_BangTrichNopCongDoan, new DevExpress.XtraPrinting.XlsxExportOptions() { SheetName = "BangTrichNopCongDoan" });
                                    }
                                }
                            }

                            string exf_BangTrichNopThueTNCN = string.Empty;
                            XtraReport xrpt_BangTrichNopThueTNCN = null;
                            ReportData_Custom report_BangTrichNopThueTNCN = _obs.FindObject<ReportData_Custom>(CriteriaOperator.Parse("Oid =?", "1187"));
                            ITypeInfo type_BaoCao_Luong_BangTrichNopThueTNCN = _obs.TypesInfo.FindTypeInfo("ERP.Module.Report.TienLuong.BaoCao_Luong_BangTrichNopThueTNCN");
                            if (type_BaoCao_Luong_BangTrichNopThueTNCN != null)
                            {
                                StoreProcedureReport rpt = (StoreProcedureReport)_obs.CreateObject(type_BaoCao_Luong_BangTrichNopThueTNCN.Type);
                                if (rpt != null)
                                {
                                    if (report_BangTrichNopThueTNCN != null)
                                    {
                                        //Truyền parameter
                                        ((BaoCao_Luong_BangTrichNopThueTNCN)rpt).CongTy = _obs.GetObjectByKey<CongTy>(_chungTu.CongTy.Oid);
                                        ((BaoCao_Luong_BangTrichNopThueTNCN)rpt).ChungTu = _obs.GetObjectByKey<ChungTu>(_chungTu.Oid);
                                        //                             
                                        StoreProcedureReport.Param = rpt;
                                        //  
                                        xrpt_BangTrichNopThueTNCN = report_BangTrichNopThueTNCN.LoadReport(_obs);
                                        exf_BangTrichNopThueTNCN = folderDirectory + report_BangTrichNopThueTNCN.ReportName + "-" + _chungTu.KyTinhLuong.TenKy + ".xlsx";
                                        xrpt_BangTrichNopThueTNCN.ExportToXlsx(exf_BangTrichNopThueTNCN, new DevExpress.XtraPrinting.XlsxExportOptions() { SheetName = "BangTrichNopThueTNCN" });
                                    }
                                }
                            }

                            int sheetNumber = 0;
                            if (!string.IsNullOrEmpty(exf_TongHopThanhToanTienLuong))
                            {
                                Workbook wb_TongHopThanhToanTienLuong = new Workbook();
                                wb_TongHopThanhToanTienLuong.LoadDocument(exf_TongHopThanhToanTienLuong);
                                File.Delete(exf_TongHopThanhToanTienLuong);

                                if (!string.IsNullOrEmpty(exf_TongHopCacKhoanNgoaiKhoa))
                                {
                                    sheetNumber = sheetNumber + 1;
                                    Workbook wb_TongHopCacKhoanNgoaiKhoa = new Workbook();
                                    wb_TongHopCacKhoanNgoaiKhoa.LoadDocument(exf_TongHopCacKhoanNgoaiKhoa);
                                    wb_TongHopThanhToanTienLuong.Worksheets.Insert(sheetNumber, "NgoaiKhoa");
                                    wb_TongHopThanhToanTienLuong.Worksheets[sheetNumber].CopyFrom(wb_TongHopCacKhoanNgoaiKhoa.Worksheets[0]);
                                    File.Delete(exf_TongHopCacKhoanNgoaiKhoa);
                                }

                                if (!string.IsNullOrEmpty(exf_TongHopCacKhoanThuNhapKhac))
                                {
                                    sheetNumber = sheetNumber + 1;
                                    Workbook wb_TongHopCacKhoanThuNhapKhac = new Workbook();
                                    wb_TongHopCacKhoanThuNhapKhac.LoadDocument(exf_TongHopCacKhoanThuNhapKhac);
                                    wb_TongHopThanhToanTienLuong.Worksheets.Insert(sheetNumber, "ThuNhapKhac");
                                    wb_TongHopThanhToanTienLuong.Worksheets[sheetNumber].CopyFrom(wb_TongHopCacKhoanThuNhapKhac.Worksheets[0]);
                                    File.Delete(exf_TongHopCacKhoanThuNhapKhac);
                                }

                                if (!string.IsNullOrEmpty(exf_BangChamCongThang))
                                {
                                    sheetNumber = sheetNumber + 1;
                                    Workbook wb_BangChamCongThang = new Workbook();
                                    wb_BangChamCongThang.LoadDocument(exf_BangChamCongThang);
                                    wb_TongHopThanhToanTienLuong.Worksheets.Insert(sheetNumber, "BangChamCong");
                                    wb_TongHopThanhToanTienLuong.Worksheets[sheetNumber].CopyFrom(wb_BangChamCongThang.Worksheets[0]);
                                }

                                if (!string.IsNullOrEmpty(exf_BangTrichNopBaoHiem))
                                {
                                    sheetNumber = sheetNumber + 1;
                                    Workbook wb_BangTrichNopBaoHiem = new Workbook();
                                    wb_BangTrichNopBaoHiem.LoadDocument(exf_BangTrichNopBaoHiem);
                                    wb_TongHopThanhToanTienLuong.Worksheets.Insert(sheetNumber, "BaoHiem");
                                    wb_TongHopThanhToanTienLuong.Worksheets[sheetNumber].CopyFrom(wb_BangTrichNopBaoHiem.Worksheets[0]);
                                    File.Delete(exf_BangTrichNopBaoHiem);
                                }

                                if (!string.IsNullOrEmpty(exf_BangTrichNopCongDoan))
                                {
                                    sheetNumber = sheetNumber + 1;
                                    Workbook wb_BangTrichNopCongDoan = new Workbook();
                                    wb_BangTrichNopCongDoan.LoadDocument(exf_BangTrichNopCongDoan);
                                    wb_TongHopThanhToanTienLuong.Worksheets.Insert(sheetNumber, "CongDoan");
                                    wb_TongHopThanhToanTienLuong.Worksheets[sheetNumber].CopyFrom(wb_BangTrichNopCongDoan.Worksheets[0]);
                                    File.Delete(exf_BangTrichNopCongDoan);
                                }

                                if (!string.IsNullOrEmpty(exf_BangTrichNopThueTNCN))
                                {
                                    sheetNumber = sheetNumber + 1;
                                    Workbook wb_BangTrichNopThueTNCN = new Workbook();
                                    wb_BangTrichNopThueTNCN.LoadDocument(exf_BangTrichNopThueTNCN);
                                    wb_TongHopThanhToanTienLuong.Worksheets.Insert(sheetNumber, "ThueTNCN");
                                    wb_TongHopThanhToanTienLuong.Worksheets[sheetNumber].CopyFrom(wb_BangTrichNopThueTNCN.Worksheets[0]);
                                    File.Delete(exf_BangTrichNopThueTNCN);
                                }

                                wb_TongHopThanhToanTienLuong.SaveDocument("Hồ sơ lương " +  _chungTu.KyTinhLuong.TenKy + ".xlsx");
                                Process.Start("Hồ sơ lương " + _chungTu.KyTinhLuong.TenKy + ".xlsx");
                            }
                        }
                    }
                }
            }
        }
    }
}
