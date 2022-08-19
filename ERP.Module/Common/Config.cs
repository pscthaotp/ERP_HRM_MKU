using ERP.Module.Extends;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ERP.Module.Commons
{
    public static class Config
    {

        public static bool SetNgonNgu = false;
        public static bool Start = false;
        public static string NgonNguSuDung = "";
        /// <summary>
        /// Lưu trữ lại trong control xaf không hỗ trợ lấy
        /// </summary>
        public static string StartupPath
        {
            get;
            set;
        }

        /// <summary>
        /// Get CompanyKey
        /// </summary>
        public static string DecryptKey
        {
            get
            {
                return "56dsFkj3kj23asdf83ks32gLJkj43A58MfKj"; // Key này khi tạo chuổi kết nối bằng phần mền đã gán
            }
        }

        /// <summary>
        /// Get CompanyKey
        /// </summary>
        public static string CompanyKey
        {
            get
            {
                return ConfigurationManager.AppSettings["CompanyKey"];
            }
        }

        /// <summary>
        /// Get SkinName
        /// </summary>
        public static string SkinName
        {
            get
            {
                return ConfigurationManager.AppSettings["SkinName"];
            }
        }

        /// <summary>
        /// Get UploadFile
        /// </summary>
        public static string UploadFile
        {
            get
            {
                return ConfigurationManager.AppSettings["UploadFile"];
            }
        }

        /// <summary>
        /// Get Language
        /// </summary>
        public static string Languages
        {
            get
            {
                if (ConfigurationManager.AppSettings["CompanyKey"] != "MKU")
                {
                    
                    if (NgonNguSuDung == "")
                    {
                        
                        if (!SetNgonNgu)
                        {
                            //if (!Start)
                            //{
                            //    Form_NgonNgu form = new Form_NgonNgu();
                            //    form.Show();
                            //    Start = true;
                            //}
                            DialogResult dialogResult = MessageBox.Show("Sử dụng Tiếng Việt, bấm 'Yes' \nIn English, press 'No'", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                            if (dialogResult == DialogResult.Yes)
                            {
                                NgonNguSuDung = "vi-VN";
                                SetNgonNgu = true;
                            }
                            else if (dialogResult == DialogResult.No)
                            {
                                //do something else
                                NgonNguSuDung = "en";
                                SetNgonNgu = true;
                            }
                        }
                    }
                    return NgonNguSuDung;
                }
                else
                {
                    return ConfigurationManager.AppSettings["Languages"];
                }
            }
        }

        /// <summary>
        /// Get Modules
        /// </summary>
        public static string TypeApplication
        {
            get
            {
                return ConfigurationManager.AppSettings["TypeApplication"];
            }
        }


        /// <summary>
        /// Đường dẫn đến tập tin lỗi khi import excel
        /// </summary>
        public static string URLErorrImportExcel
        {
            get
            {
                return "~/Downloads/Commons/Erorr_Import.txt";
            }
        }

        /// <summary>
        /// Lấy cấu hình xác thực
        /// </summary>
        public static string Accuracy
        {
            get
            {
                return ConfigurationManager.AppSettings["Accuracy"];
            }
        }
        
        /// <summary>
        /// Lấy Oid Công ty
        /// </summary>
        public static Guid KeyTTCEdu
        {
            get
            {
                return new Guid("1B238BAD-2616-4FDD-B958-BA47F3111751");//13AB28F5-AE88-44DE-8A2F-A0CA7AFD8E36
            }
        }

        /// <summary>
        /// Lấy Oid Estudy
        /// </summary>
        public static Guid KeyEStudy
        {
            get
            {
                return new Guid("C94FA671-B3D7-425A-994D-8983DFE182ED");
            }
        }
       

        /// <summary>
        /// Lấy Oid Phân hệ bàn làm việc
        /// </summary>
        public static Guid KeyBanLamViec
        {
            get
            {
                return new Guid("00000000-0000-0000-0000-000000000006");
            }
        }
        /// <summary>
        /// Lấy ID từ SIS
        /// </summary>
        public static string IDKhoi_6
        {
            get
            {
                return "6";
            }
        }

        /// <summary>
        /// Lấy ID từ SIS
        /// </summary>
        public static string IDKhoi_10
        {
            get
            {
                return "10";
            }
        }

        /// <summary>
        /// Lấy Oid Trường Thái Bình Dương - 
        /// </summary>
        public static Guid KeyThaiBinhDuong
        {
            get
            {
                return new Guid("73038965-E348-484F-AC41-ED0E2A210AD3");
            }
        }

        /// <summary>
        /// Lấy Oid Trường tân phú
        /// </summary>
        public static Guid KeyTanPhu
        {
            get
            {
                return new Guid("5156ECFC-E6FC-47E3-8F2F-52EA6A4ACDCA");
            }
        }
        /// <summary>
        /// Lấy id link ser Trường tân phú
        /// </summary>
        public static string KeyLinkServer
        {
            get
            {
                if (DataProvider.GetConnectionString().Contains(KeyServerMamNonAzure))
                    return "";
                else if (DataProvider.GetConnectionString().Contains(KeyServerMamNon))
                    return "[ERP.TTCEDU.VN,4016].";
                else
                    return "[ERP.TTCEDU.VN,4016]";
                //return "[203.205.63.142,4016]";
                //return "[14.161.48.67,4016]";
            }
        }

        /// <summary>
        /// Lấy ID server mầm non
        /// </summary>
        public static string KeyConnectServer
        {
            get
            {
                if (DataProvider.GetConnectionString().Contains(KeyServerMamNonAzure))
                    return "52.163.82.89";
                else if (DataProvider.GetConnectionString().Contains(KeyServerMamNon))
                    return "erp.ttcedu.vn,4015";
                //return "14.161.48.67,4015";
                //return "203.205.63.142,4015";
                return "erp.ttcedu.vn,4015";
            }
        }
        public static string KeyServerMamNon
        {
            get
            {
                return "erp.ttcedu.vn,4015";
            }
        }
        /// <summary>
        /// Lấy ID server mầm non
        /// </summary>
        public static string KeyServerMamNonAzure
        {
            get
            {
                //return "14.161.48.67,4015";
                //return "203.205.63.142,4015";
                return "52.163.82.89";
            }
        }
        /// <summary>
        /// Lấy Oid Khối đại học
        /// </summary>
        public static Guid KeyDaiHoc_CaoDang
        {
            get
            {
                //Key
                return new Guid("670BCCC9-0BAE-4B7B-AA41-3D211FE8D24E");
            }
        }

        /// <summary>
        /// Lấy Oid Khối phổ thông
        /// </summary>
        public static Guid KeyPhoThong
        {
            get
            {
                //Key

                return new Guid("AB48FB62-6B5C-4655-AB26-C219E326AA86");
            }
        }



        /// <summary>
        /// Lấy Oid Khối mần non
        /// </summary>
        public static Guid KeyManNon
        {
            get
            {
                //Key
                return new Guid("DC05AAE8-DC38-4BCE-B62B-AEE5DE300D67");
            }
        }
    }
}
