using DevExpress.ExpressApp;
using ERP.Module.MailMerge;
using ERP.Module.Win.NormalForm.MailMerge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ERP.Module.Win.MailMerge.Prosess.ShowMaiMerge
{
    public class Prosess_Show
    {
        public static void ShowEditor<T>(List<T> obj, IObjectSpace obs, params MailMergeTemplate[] args) where T : IMailMergeBase
        {
            frmShowMailMerge<T> editor = new frmShowMailMerge<T>();
            editor.Show();
            //
            if (args != null)
                editor.LoadData(obj, obs, args);
        }

        public static void ShowEditorWithValidDate<T>(List<T> obj, IObjectSpace obs, DateTime validDate, params MailMergeTemplate[] args) where T : IMailMergeBase
        {
            frmShowMailMerge<T> editor = new frmShowMailMerge<T>();
            editor.Show();
            //
            if (args != null)
                editor.LoadDataWithValidDate(obj, obs, validDate, args);
        }
    }
}
