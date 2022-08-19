using System;
using System.Collections.Generic;
using DevExpress.XtraRichEdit;
using System.IO;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.MailMerge;

namespace ERP.Module.Win.NormalForm.MailMerge
{
    public class CustomSaveDocumentCommand : SaveDocumentCommand
    {
        public MailMergeTemplate MailMerge { get; set; }
        public XPObjectSpace ObjectSpace { get; set; }

        public CustomSaveDocumentCommand(IRichEditControl control)
            : base(control)
        { }

        public override void Execute()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                Control.Document.SaveDocument(stream, DocumentFormat.Rtf);
                MailMerge.LuuTru = stream.ToArray();
                //
                ObjectSpace.CommitChanges();
            }
        }
    }
}
