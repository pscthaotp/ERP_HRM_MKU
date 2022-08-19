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
using DevExpress.ExpressApp.Web;
using System.Data;
//
namespace ERP.Module.Extends
{
    public static class MailHelpers
    {
        public static bool CreateMailManager(Session session, string tieude, string filename, string emailgui, string passgui, string emailnhan, string noidung, TrangThaiGuiMailEnum trangthaiguimail, Guid keymask)
        {
            //try
            //{
            // Lấy Port và Server
            CauHinhChung cauHinhChung = Common.CauHinhChung_GetCauHinhChung;
            if (cauHinhChung == null) return false;
            //
            int port = cauHinhChung.CauHinhMail.Port;
            string server = cauHinhChung.CauHinhMail.Server;
            //
            if (port <= 0 || string.IsNullOrEmpty(server) || string.IsNullOrEmpty(emailgui) || string.IsNullOrEmpty(passgui) || string.IsNullOrEmpty(emailnhan) || string.IsNullOrEmpty(noidung))
            {
                return false;
            }

            // Tiến hành gửi mail
            if (SendMail(port, server, tieude, noidung, filename, emailgui, passgui, emailnhan))
            {
                // Lưu lại lịch sử gửi mail
                MailManager mail = new MailManager(session);
                mail.Title = tieude;
                mail.Contents = noidung;
                mail.SendEmail = emailgui;
                mail.SendPass = passgui;
                mail.ReceiverEmail = emailnhan;
                mail.TrangThaiGuiMail = trangthaiguimail;
                mail.KeyMask = keymask;
                //
                session.Save(mail);
                //
                return true;
            }
            //}
            //catch (Exception ex)
            //{
            //    //
            //    return false;
            //}
            //
            return false;
        }

        public static bool CreateMailManager(Session session, string tieude, string filename, string emailgui, string passgui, string emailnhan, string noidung, string server, int port)
        {       
            // Tiến hành gửi mail
            if (SendMail_App(port, server, tieude, noidung, filename, emailgui, passgui, emailnhan))
            {
                return true;
            }
            return false;
        }

        public static bool SendMail_App(int port, string smtpServer, string tieude, string noidung, string filename, string emailgui, string passgui, string emailnhan)
        {
            bool sucess = false;
            //
            if (!KiemTraDinhDangMail(emailnhan))
            {
                return false;
            }
            //
            var loginInfo = new NetworkCredential(emailgui, passgui);
            var msg = new System.Net.Mail.MailMessage();
            var smtpClient = new SmtpClient(smtpServer, port);
            //
            msg.To.Add(new MailAddress(emailnhan));
            //
            msg.From = new MailAddress(emailgui);
            msg.Subject = tieude;
            msg.Body = noidung;
            msg.IsBodyHtml = true;

            //Đính kèm file
            if (!string.IsNullOrEmpty(filename))
            {
                //
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(filename);
                if (System.IO.File.Exists(filename))
                {
                    attachment.Name = tieude;
                    msg.Attachments.Add(attachment);
                }
            }
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            try
            {
                smtpClient.Send(msg);
                sucess = true;
            }

            catch (Exception ex)
            {//
                sucess = false;
            }
            //
            return sucess;
        }

        public static bool SendMail(int port, string smtpServer, string tieude, string noidung, string filename, string emailgui, string passgui, string emailnhan)
        {
            bool sucess = false;
            //
            if (!KiemTraDinhDangMail(emailnhan) || !KiemTraEmailTonTai(emailnhan))
            {
                return false;
            }
            //
            var loginInfo = new NetworkCredential(emailgui, passgui);
            var msg = new System.Net.Mail.MailMessage();
            var smtpClient = new SmtpClient(smtpServer, port);
            //
            msg.To.Add(new MailAddress(emailnhan));
            //
            msg.From = new MailAddress(emailgui);
            msg.Subject = tieude;
            msg.Body = noidung;
            msg.IsBodyHtml = true;

            //Đính kèm file
            if (!string.IsNullOrEmpty(filename))
            {
                //
                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(filename);
                if (System.IO.File.Exists(filename))
                {
                    attachment.Name = tieude;
                    msg.Attachments.Add(attachment);
                }
            }
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            try
            {
                smtpClient.Send(msg);
                sucess = true;
            }

            catch (Exception ex)
            {//
              
            }
            //
            return sucess;
        }

        public static bool KiemTraDinhDangMail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
                return (true);
            else
                return (false);
        }

        private static bool KiemTraEmailTonTai(string email)
        {
            bool ok = true;
            //
            TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
            string CRLF = "\r\n";
            byte[] dataBuffer;
            string ResponseString;
            NetworkStream netStream = tClient.GetStream();
            StreamReader reader = new StreamReader(netStream);
            ResponseString = reader.ReadLine();
            /* Perform HELO to SMTP Server and get Response */
            dataBuffer = BytesFromString("HELO KirtanHere" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            dataBuffer = BytesFromString("MAIL FROM:<pscerp@gmail.com>" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            /* Read Response of the RCPT TO Message to know from google if it exist or not */
            dataBuffer = BytesFromString("RCPT TO:<" + email + ">" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            ResponseString = reader.ReadLine();
            if (GetResponseCode(ResponseString) == 550)
            {
                ok = false;
            }
            /* QUITE CONNECTION */
            dataBuffer = BytesFromString("QUITE" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            tClient.Close();

            return ok;
        }
        private static byte[] BytesFromString(string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
        private static int GetResponseCode(string ResponseString)
        {
            return int.Parse(ResponseString.Substring(0, 3));
        }

        public static bool DownloadEmailsUsingPOP3(Session session, string host, string user, string pass)
        {

            try
            {
                using (Pop3 pop3 = new Pop3())
                {
                    pop3.Connect("pop.gmail.com");
                    pop3.Login(user, pass);

                    // Receive all messages and display the subject 
                    MailBuilder builder = new MailBuilder();
                    foreach (string uid in pop3.GetAll())
                    {

                        IMail email = builder.CreateFromEml(pop3.GetMessageByUID(uid));
                        //
                        if (email != null)
                        {
                            MailManager receiverMail = new MailManager(session);
                            receiverMail.Title = email.Subject;
                            receiverMail.Contents = email.Text;
                            //
                            if (email.Attachments != null)
                            {

                            }
                            //
                            session.Save(receiverMail);
                        }
                    }
                    pop3.Close();
                    //
                    return true;
                }
            }
            catch (Exception ex)
            {
                //
                return false;
            }
        }
    }
}
