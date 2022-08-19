﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.UI;

namespace ERP.Module.Extends
{
    public static class WebMsgBox
    {
        public static Hashtable handlerPages = new Hashtable();
        
        public static void Show(string Message)
        {
            if (!(handlerPages.Contains(HttpContext.Current.Handler)))
            {
                var currentPage = (Page)HttpContext.Current.Handler;
                if (!((currentPage == null)))
                {
                    var messageQueue = new Queue();
                    messageQueue.Enqueue(Message);
                    handlerPages.Add(HttpContext.Current.Handler, messageQueue);
                    currentPage.Unload += new EventHandler(CurrentPageUnload);
                }
            }
            else
            {
                var queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
                queue.Enqueue(Message);
            }
        }
        private static void CurrentPageUnload(object sender, EventArgs e)
        {
            var queue = ((Queue)(handlerPages[HttpContext.Current.Handler]));
            if (queue != null)
            {
                var builder = new StringBuilder();
                var iMsgCount = queue.Count;
                builder.Append("<script language='javascript'>");
                string sMsg;
                while ((iMsgCount > 0))
                {
                    iMsgCount = iMsgCount - 1;
                    sMsg = Convert.ToString(queue.Dequeue());
                    sMsg = sMsg.Replace("\"", "'");
                    builder.Append("alert( \"" + sMsg + "\" );");
                }
                builder.Append("</script>");
                handlerPages.Remove(HttpContext.Current.Handler);
                HttpContext.Current.Response.Write(builder.ToString());
            }
        }
    }
}
