using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using ERP.Module.HeThong;
using ERP.Module.Enum.Systems;
using System.Net.Sockets;
using System.IO;
using ERP.Module.Commons;
using ERP.Module.CauHinhChungs;
using Limilabs.Mail;
using Limilabs.Client.POP3;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
//
namespace ERP.Module.Commons
{
    public static class UploadImage
    {
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        public static byte[] ImageToByteArray(System.Drawing.Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public static byte[] GetBytesFromImage(String imageFile)
        {
            MemoryStream ms = new MemoryStream();
            Image img = Image.FromFile(imageFile);
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            return ms.ToArray();
        }

        public static Image ResizeImage(Image img, int width = -1, int height = -1)
        {
            Image returnImage = null;
            try
            {
                int X = img.Width;
                int Y = img.Height;
                if (width != -1 && height == -1)
                {
                    height = (int)((width * Y) / X);
                }
                else if (width == -1 && height != -1)
                {
                    width = (int)((height * X) / Y);
                }

                if (width != -1 && height != -1)
                { returnImage = img.GetThumbnailImage(width, height, () => false, IntPtr.Zero); }
                else
                { returnImage = img; }
            }
            catch { }
            //
            return returnImage;
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;
            try
            {
                MemoryStream ms = new MemoryStream(byteArrayIn, 0, byteArrayIn.Length);
                ms.Write(byteArrayIn, 0, byteArrayIn.Length);
                returnImage = Image.FromStream(ms, true);//Exception occurs here
            }
            catch { }
            //
            return returnImage;
        }

        public static bool UploadImageToServer(Image img,string name, int loai, int width = -1, int height = -1)
        {      
            //
            try
            {
                CauHinhChung cauHinhChung = Commons.Common.CauHinhChung_GetCauHinhChung;
                if (cauHinhChung == null)
                    return false;
                //
                string url = string.Empty;
                if (loai == 1) //Nhân sự
                {
                    url = cauHinhChung.CauHinhSoHoa.URL_NhanVien;
                    width = cauHinhChung.CauHinhSoHoa.Width_NhanVien;
                    height = cauHinhChung.CauHinhSoHoa.Height_NhanVien;
                }   
                if(loai == 2) //Học sinh
                {
                    url = cauHinhChung.CauHinhSoHoa.URL_HocSinh;
                    width = cauHinhChung.CauHinhSoHoa.Width_HocSinh;
                    height = cauHinhChung.CauHinhSoHoa.Height_HocSinh;
                }
                if (loai == 3) //Người đưa đón
                {
                    url = cauHinhChung.CauHinhSoHoa.URL_NguoiDuaDon;
                    width = cauHinhChung.CauHinhSoHoa.Width_NguoiDuaDon;
                    height = cauHinhChung.CauHinhSoHoa.Height_NguoiDuaDon;
                }
                if (loai == 4) //ParentDoParentAsk
                {
                    url = cauHinhChung.CauHinhSoHoa.URL_ParentDoParentAsk;
                    width = cauHinhChung.CauHinhSoHoa.Width_ParentDoParentAsk;
                    height = cauHinhChung.CauHinhSoHoa.Height_ParentDoParentAsk;
                }
                if (loai == 5) //logo công ty
                {
                    url = cauHinhChung.CauHinhSoHoa.URL_CongTy;
                    width = cauHinhChung.CauHinhSoHoa.Width_CongTy;
                    height = cauHinhChung.CauHinhSoHoa.Height_CongTy;
                }
                if (loai == 6) //Tiếng anh
                {
                    url = cauHinhChung.CauHinhSoHoa.URL_TiengAnh;
                    width = cauHinhChung.CauHinhSoHoa.Width_TiengAnh;
                    height = cauHinhChung.CauHinhSoHoa.Height_TiengAnh;
                }
                if (loai == 7) //Bếp
                {
                    url = cauHinhChung.CauHinhSoHoa.URL_Bep;
                    width = cauHinhChung.CauHinhSoHoa.Width_Bep;
                    height = cauHinhChung.CauHinhSoHoa.Height_Bep;
                }
                if (loai == 8) //DanThuoc
                {
                    url = cauHinhChung.CauHinhSoHoa.URL_DanThuoc;
                    width = cauHinhChung.CauHinhSoHoa.Width_DanThuoc;
                    height = cauHinhChung.CauHinhSoHoa.Height_DanThuoc;
                }

                //
                string username = cauHinhChung.CauHinhSoHoa.Username;
                string password = cauHinhChung.CauHinhSoHoa.Password;
                //
                if (width != -1 && height != -1)
                { img = ResizeImage(img, new Size(width, height)); }
                byte[] buffer = ImageToByteArray(img);
                //
                UploadFile(url + "/", username, password, buffer,name);
                //
                return true;
            }
            catch (Exception ex)
            {
                //throw new Exception("Đã xảy ra lỗi trong quá trình ghi tập tin.");
            }
            //
            return false;
        }
        public static Image DownLoadImageFromServer(string name,int loai)
        {
            Image image = null;
            //
            if (string.IsNullOrEmpty(name))
                return image;
            //
            //Đọc dữ liệu
            try
            {
                CauHinhChung cauHinhChung = Commons.Common.CauHinhChung_GetCauHinhChung;
                if (cauHinhChung == null)
                    return image;
                ////
                string url = string.Empty;
                if (loai == 1) //Nhân sự
                    url = cauHinhChung.CauHinhSoHoa.URL_NhanVien;
                if (loai == 2) //Học sinh
                    url = cauHinhChung.CauHinhSoHoa.URL_HocSinh;
                if (loai == 3) //Người đưa đón
                    url = cauHinhChung.CauHinhSoHoa.URL_NguoiDuaDon;
                if (loai == 4) //ParentDoParentAsk
                    url = cauHinhChung.CauHinhSoHoa.URL_ParentDoParentAsk;
                if (loai == 5) //Tiếng anh
                    url = cauHinhChung.CauHinhSoHoa.URL_TiengAnh;
                if (loai == 6) //Bếp
                    url = cauHinhChung.CauHinhSoHoa.URL_Bep;
                if (loai == 8) //Bếp
                    url = cauHinhChung.CauHinhSoHoa.URL_DanThuoc;

                url = url + "/" + name;
                url = url.Replace("ftp://images.igc.edu.vn/", "https://image.igc.edu.vn/");           

                #region Cách cũ
                //string username = cauHinhChung.CauHinhSoHoa.Username;
                //string password = cauHinhChung.CauHinhSoHoa.Password;
                //byte[] imgByte = DownloadFile(url, username, password);
                //image = ByteArrayToImage(imgByte);
                #endregion
                
                var request = WebRequest.Create(url);
                using (var response = request.GetResponse())
                using (var stream = response.GetResponseStream())
                {
                    image = Bitmap.FromStream(stream);
                }
                

            }
            catch (Exception ex)
            {

            }
            //
            return image;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ftppath"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static byte[] DownloadFile(string ftppath, string username, string password)
        {
            try
            {
                WebClient request = new WebClient();
                request.Credentials = new NetworkCredential(username, password);
                byte[] newFileData = request.DownloadData(ftppath);
                return newFileData;
            }
            catch (Exception ex)
            {
                throw new Exception("Đã xảy ra lỗi trong quá trình đọc tập tin: \r\n" + ex.Message);
            }
        }


        /// <summary>
        /// Upload file to server using FTP
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <param name="filename">file name</param>
        public static void UploadFile(string ftppath, string username, string password, byte[] buffer, string fileName)
        {
            try
            {
                {//Tiến hành upload file lên máy chủ

                    FtpWebRequest ftp = CreateFTP(ftppath, username, password, fileName, WebRequestMethods.Ftp.UploadFile);
                    //
                    Stream requestStream = ftp.GetRequestStream();
                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Close();
                    requestStream.Flush();
                }
            }
            catch (WebException ex)
            {
                String status = ((FtpWebResponse)ex.Response).StatusDescription;
                throw new Exception("Đã xảy ra lỗi trong quá trình ghi tập tin: \r\n" + ex.Message);
            }
        }


        /// <summary>
        /// Create FTP
        /// </summary>
        /// <param name="ftppath">ftp path</param>
        /// <param name="username">ftp username</param>
        /// <param name="password">ftp password</param>
        /// <param name="filepath">file path</param>
        /// <param name="method">ftp method</param>
        /// <returns></returns>
        private static FtpWebRequest CreateFTP(string ftppath, string username, string password, string filepath, string method)
        {
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(ftppath + filepath);
            ftp.Credentials = new NetworkCredential(username, password);
            ftp.KeepAlive = false;
            ftp.UseBinary = true;
            ftp.Method = method;

            return ftp;
        }
    }
}
