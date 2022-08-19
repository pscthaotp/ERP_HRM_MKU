using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using DevExpress.XtraRichEdit;
using DevExpress.XtraEditors.Controls;
using DevExpress.ExpressApp;
using DevExpress.XtraBars.Ribbon;
using DevExpress.ExpressApp.Xpo;
using DevExpress.XtraBars;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Services;
using DevExpress.XtraRichEdit.Commands;
using DevExpress.Office;
using System.Windows.Forms;
using ERP.Module.MailMerge;
using ERP.Module.Commons;
using System.Net;
using System.Drawing;

namespace ERP.Module.Win.NormalForm.MailMerge
{
    public partial class frmShowMailMerge<T> : RibbonForm where T : IMailMergeBase
    {
        private XPObjectSpace _obs;
        private List<T> _dataSource;
        private RichEditControl _currentRichEdit;
        private MailMergeTemplate[] _mailMerge;
        private Guid _congTy;
        private DateTime _validDate = DateTime.MinValue;

        public frmShowMailMerge()
        {
            InitializeComponent();
            this.lookupBieuMau.ButtonClick += repositoryItemLookUpEdit1_ButtonClick;
        }

        private void frmShowMailMerge_Load(object sender, EventArgs e)
        {
            //
            SetRichEdit(richTemplate);
        }

        public void LoadData(List<T> obj, IObjectSpace obs, params MailMergeTemplate[] mailMerge)
        {
            _congTy = Common.CongTy(((XPObjectSpace)obs).Session).Oid;
            //
            Text = String.Format("{0} - {1}", mailMerge[0].TenTaiLieu, Text);
            _mailMerge = mailMerge;
            //
            HideTabs(mailMerge);
            //
            _currentRichEdit = richTemplate;
            //
            this._obs = (XPObjectSpace)obs;
            _dataSource = obj;
            richTemplate.Options.MailMerge.DataSource = obj;
            //
            LoadTemplate(mailMerge);
            LoadTemplates();

            Zoom();
        }

        public void LoadDataWithValidDate(List<T> obj, IObjectSpace obs, DateTime validDate, params MailMergeTemplate[] mailMerge)
        {
            _congTy = Common.CongTy(((XPObjectSpace)obs).Session).Oid;
            _validDate = validDate;
            //
            Text = String.Format("{0} - {1}", mailMerge[0].TenTaiLieu, Text);
            _mailMerge = mailMerge;
            //
            HideTabs(mailMerge);
            //
            _currentRichEdit = richTemplate;
            //
            this._obs = (XPObjectSpace)obs;
            _dataSource = obj;
            richTemplate.Options.MailMerge.DataSource = obj;
            //
            LoadTemplate(mailMerge);
            LoadTemplates();

            Zoom();
        }

        public void Execute()
        {
            MergeToNewDocument();
        }

        private void MergeToNewDocument()
        {
            xtraTabControl.SelectedTabPage = tabResult;
            richEditBarController1.RichEditControl = richResult;
            MailMergeOptions options = richTemplate.Document.CreateMailMergeOptions();
            options.MergeMode = MergeMode.NewSection;
            richTemplate.Document.MailMerge(options, richResult.Document);
        }

        private void richResult_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e)
        {
            if (e.Arguments.Count > 0)
            {
                var oid = e.Arguments[0].Value;
                if (string.IsNullOrWhiteSpace(oid))
                    return;

                var data = (from m in _dataSource
                            where m.Oid == oid
                            select m).SingleOrDefault();

                if (data == null)
                    return;

                if (e.VariableName == "Master")
                {
                    richMaster.Options.MailMerge.DataSource = data.Master;

                    IRichEditDocumentServer result = richMaster.CreateDocumentServer();
                    result.CalculateDocumentVariable += result_CalculateDocumentVariable;
                    richMaster.MailMerge(result);
                    result.CalculateDocumentVariable -= result_CalculateDocumentVariable;

                    e.Value = result;
                    e.Handled = true;
                }
                else if (e.VariableName == "Master1")
                {
                    richMaster1.Options.MailMerge.DataSource = data.Master1;

                    IRichEditDocumentServer result = richMaster1.CreateDocumentServer();
                    result.CalculateDocumentVariable += result_CalculateDocumentVariable1;
                    richMaster1.MailMerge(result);
                    result.CalculateDocumentVariable -= result_CalculateDocumentVariable1;

                    e.Value = result;
                    e.Handled = true;
                }
            }
        }

        private void result_CalculateDocumentVariable(object sender, CalculateDocumentVariableEventArgs e)
        {
            if (e.Arguments.Count > 0)
            {
                var oid = e.Arguments[0].Value;
                if (string.IsNullOrWhiteSpace(oid))
                    return;

                var data = (from m in _dataSource
                            where m.Oid == oid
                            select m).SingleOrDefault();

                if (data == null)
                    return;

                if (e.VariableName == "Detail")
                {
                    richDetail.Options.MailMerge.DataSource = data.Detail;

                    IRichEditDocumentServer result = richDetail.CreateDocumentServer();

                    MailMergeOptions options = richDetail.CreateMailMergeOptions();
                    options.MergeMode = MergeMode.JoinTables;
                    richDetail.MailMerge(options, result);

                    e.Value = result;
                    e.Handled = true;
                }
            }
        }

        private void result_CalculateDocumentVariable1(object sender, CalculateDocumentVariableEventArgs e)
        {
            if (e.Arguments.Count > 0)
            {
                var oid = e.Arguments[0].Value;
                if (string.IsNullOrWhiteSpace(oid))
                    return;

                var data = (from m in _dataSource
                            where m.Oid == oid
                            select m).SingleOrDefault();

                if (data == null)
                    return;

                if (e.VariableName == "Detail1")
                {
                    richDetail1.Options.MailMerge.DataSource = data.Detail1;

                    IRichEditDocumentServer result = richDetail1.CreateDocumentServer();

                    MailMergeOptions options = richDetail1.CreateMailMergeOptions();
                    options.MergeMode = MergeMode.JoinTables;
                    richDetail1.MailMerge(options, result);

                    e.Value = result;
                    e.Handled = true;
                }
            }
        }

        private void LoadTemplates()
        {
            XPCollection<MailMergeTemplate> templates;
            Text = String.Format("{0}", _mailMerge[0].TenTaiLieu);
            if (_validDate != DateTime.MinValue)
                templates = new XPCollection<MailMergeTemplate>(_obs.Session, CriteriaOperator.Parse("MaQuanLy=? and CongTy=? and NgayLap <= ? and HieuLucDenNgay >= ?", _mailMerge[0].MaQuanLy, _mailMerge[0].CongTy, _validDate.Date, _validDate.Date));
            else
                templates = new XPCollection<MailMergeTemplate>(_obs.Session, CriteriaOperator.Parse("MaQuanLy=? and CongTy=? and NgayLap <= ? and HieuLucDenNgay >= ?", _mailMerge[0].MaQuanLy, _mailMerge[0].CongTy, _mailMerge[0].NgayLap.Date, _mailMerge[0].HieuLucDenNgay.Date));
            lookupBieuMau.DataSource = templates;
            barBieuMau.EditValue = _mailMerge[0];
            SetService(_mailMerge);
            MergeToNewDocument();            
        }

        private void barBieuMau_EditValueChanged(object sender, EventArgs e)
        {
            MailMergeTemplate template = barBieuMau.EditValue as MailMergeTemplate;
            if (template != null)
            {
                _mailMerge[0] = template;
                LoadTemplate(_mailMerge[0]);
                LoadTemplates();
            }
        }

        private void HideTabs(params MailMergeTemplate[] args)
        {
            if (args.Length == 1)
            {
                tabMaster.PageVisible = false;
                tabDetail.PageVisible = false;
                tabMaster1.PageVisible = false;
                tabDetail1.PageVisible = false;
            }
            else if (args.Length == 3)
            {
                if (_mailMerge[1].MaQuanLy.Contains("QuyetDinhKhenThuongMaster1.rtf")
                    && _mailMerge[2].MaQuanLy.Contains("QuyetDinhKhenThuongDetail1.rtf"))
                {
                    tabMaster.PageVisible = false;
                    tabDetail.PageVisible = false;
                }
                else
                {
                    tabMaster1.PageVisible = false;
                    tabDetail1.PageVisible = false;
                }
            }
        }

        private void mergeToNewDocumentItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            MergeToNewDocument();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (e.Page.Name == "tabTemplate")
                SetRichEdit(richTemplate);
            else if (e.Page.Name == "tabMaster")
                SetRichEdit(richMaster);
            else if (e.Page.Name == "tabDetail")
                SetRichEdit(richDetail);
            else if (e.Page.Name == "tabMaster1")
                SetRichEdit(richMaster1);
            else if (e.Page.Name == "tabDetail1")
                SetRichEdit(richDetail1);
            else
                SetRichEdit(richResult);
        }

        private void SetService(params MailMergeTemplate[] mailMerge)
        {
            if (mailMerge.Length == 5)
            {
                //master1
                ReplaceRichEditCommandFactoryService(richMaster1, mailMerge[3], (XPObjectSpace)_obs);

                //detail1
                ReplaceRichEditCommandFactoryService(richDetail1, mailMerge[4], (XPObjectSpace)_obs);
            }

            if (mailMerge.Length >= 3)
            {
                if (mailMerge[1].MaQuanLy.Contains("QuyetDinhKhenThuongMaster1.rtf")
                    && mailMerge[2].MaQuanLy.Contains("QuyetDinhKhenThuongDetail1.rtf"))
                {
                    //master1
                    ReplaceRichEditCommandFactoryService(richMaster1, mailMerge[1], (XPObjectSpace)_obs);

                    //detail1
                    ReplaceRichEditCommandFactoryService(richDetail1, mailMerge[2], (XPObjectSpace)_obs);
                }
                else
                {
                    //master
                    ReplaceRichEditCommandFactoryService(richMaster, mailMerge[1], (XPObjectSpace)_obs);

                    //detail
                    ReplaceRichEditCommandFactoryService(richDetail, mailMerge[2], (XPObjectSpace)_obs);
                }
            }

            if (mailMerge.Length > 0)
            {
                //template
                ReplaceRichEditCommandFactoryService(richTemplate, mailMerge[0], (XPObjectSpace)_obs);
            }
        }

        private static void ReplaceRichEditCommandFactoryService(RichEditControl control, MailMergeTemplate mailMerge, XPObjectSpace obs)
        {
            IRichEditCommandFactoryService service = control.GetService<IRichEditCommandFactoryService>();
            if (service == null)
                return;
            control.RemoveService(typeof(IRichEditCommandFactoryService));
            CustomCommandFactoryService newService = new CustomCommandFactoryService(control, service);
            newService.MailMerge = mailMerge;
            newService.ObjectSpace = obs;
            control.AddService(typeof(IRichEditCommandFactoryService), newService);
        }

        private void LoadTemplate(params MailMergeTemplate[] mailMerge)
        {
            if (mailMerge.Length == 5)
            {
                using (MemoryStream stream = new MemoryStream(mailMerge[3].LuuTru))
                {
                    richMaster1.LoadDocument(stream, DocumentFormat.Rtf);
                }
                using (MemoryStream stream = new MemoryStream(mailMerge[4].LuuTru))
                {
                    richDetail1.LoadDocument(stream, DocumentFormat.Rtf);
                }
            }

            if (mailMerge.Length >= 3)
            {
                if (mailMerge[1].MaQuanLy.Contains("1")
                    && mailMerge[2].MaQuanLy.Contains("1"))
                {
                    using (MemoryStream stream = new MemoryStream(mailMerge[1].LuuTru))
                    {
                        richMaster1.LoadDocument(stream, DocumentFormat.Rtf);
                    }
                    using (MemoryStream stream = new MemoryStream(mailMerge[2].LuuTru))
                    {
                        richDetail1.LoadDocument(stream, DocumentFormat.Rtf);
                    }
                }
                else
                {
                    using (MemoryStream stream = new MemoryStream(mailMerge[1].LuuTru))
                    {
                        richMaster.LoadDocument(stream, DocumentFormat.Rtf);
                    }
                    using (MemoryStream stream = new MemoryStream(mailMerge[2].LuuTru))
                    {
                        richDetail.LoadDocument(stream, DocumentFormat.Rtf);
                    }
                }
            }

            if (mailMerge.Length > 0)
            {
                if (mailMerge[0].LuuTru != null)
                {
                    using (MemoryStream stream = new MemoryStream(mailMerge[0].LuuTru))
                    {
                        richTemplate.LoadDocument(stream, DocumentFormat.Rtf);
                    }
                }
            }
        }

        private void SetRichEdit(RichEditControl richEditControl)
        {
            _currentRichEdit = richEditControl;
            richEditBarController1.RichEditControl = richEditControl;
        }

        private void Zoom()
        {
            //zoom
            float zoomFactor = _currentRichEdit.ActiveView.ZoomFactor;
            UpdateZoomTrackbarCore(zoomFactor);
            UpdateZoomCaption(zoomFactor);
            SubscribeZoomChangedEvent();
            SubscribeZoomTrackbarEvents();
        }

        #region Xử lý giao diện
        void SubscribeZoomTrackbarEvents()
        {
            repositoryItemZoomTrackBar1.EditValueChanging += OnZoomTrackBarEditValueChanging;
        }

        void UnsubscribeZoomTrackbarEvents()
        {
            repositoryItemZoomTrackBar1.EditValueChanging -= OnZoomTrackBarEditValueChanging;
        }

        void SubscribeZoomChangedEvent()
        {
            _currentRichEdit.ZoomChanged += OnZoomChanged;
        }

        void UnsubcribeZoomChangedEvent()
        {
            _currentRichEdit.ZoomChanged -= OnZoomChanged;
        }

        void OnZoomTrackBarEditValueChanging(object sender, ChangingEventArgs e)
        {
            OnZoomTrackBarEditValueChangedCore((int)e.NewValue);
        }

        void OnZoomTrackBarEditValueChangedCore(int value)
        {
            UnsubcribeZoomChangedEvent();
            _currentRichEdit.ActiveView.ZoomFactor = (int)value / 100.0f;
            SubscribeZoomChangedEvent();
        }

        void OnZoomChanged(object sender, EventArgs e)
        {
            float zoomFactor = _currentRichEdit.ActiveView.ZoomFactor;
            UnsubscribeZoomTrackbarEvents();
            UpdateZoomTrackbarCore(zoomFactor);
            SubscribeZoomTrackbarEvents();
        }

        void UpdateZoomTrackbarCore(float zoomFactor)
        {
            barEditItem1.EditValue = (int)Math.Round(zoomFactor * 100);
        }

        void UpdateZoomCaption(float zoomFactor)
        {
            barEditItem1.Caption = String.Format("{0}%", (int)Math.Round(zoomFactor * 100));
        }

        private void richEditControl_ZoomChanged(object sender, EventArgs e)
        {
            UpdateZoomCaption(_currentRichEdit.ActiveView.ZoomFactor);
        }
        #endregion

        private void btnSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            CustomSaveDocumentCommand cmd = new CustomSaveDocumentCommand(richTemplate);
            cmd.MailMerge = _mailMerge[0];
            cmd.ObjectSpace = _obs;
            cmd.Execute();
            if (tabMaster.PageVisible)
            {
                cmd = new CustomSaveDocumentCommand(richMaster);
                cmd.MailMerge = _mailMerge[1];
                cmd.ObjectSpace = _obs;
                cmd.Execute();
            }
            if (tabDetail.PageVisible)
            {
                cmd = new CustomSaveDocumentCommand(richDetail);
                cmd.MailMerge = _mailMerge[2];
                cmd.ObjectSpace = _obs;
                cmd.Execute();
            }
            if (tabMaster1.PageVisible)
            {
                cmd = new CustomSaveDocumentCommand(richMaster1);
                cmd.MailMerge = _mailMerge[3];
                cmd.ObjectSpace = _obs;
                cmd.Execute();
            }
            if (tabDetail1.PageVisible)
            {
                cmd = new CustomSaveDocumentCommand(richDetail1);
                cmd.MailMerge = _mailMerge[4];
                cmd.ObjectSpace = _obs;
                cmd.Execute();
            }
        }

        private void btnInsertMergeField_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowInsertMergeFieldFormCommand cmd = new ShowInsertMergeFieldFormCommand(richEditBarController1.RichEditControl);
            cmd.Execute();
        }

        private void repositoryItemLookUpEdit1_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == ButtonPredefines.Ellipsis)
            {
                frmMailMergeManager frm = new frmMailMergeManager(_obs, _mailMerge[0].MaQuanLy);
                frm._defaultTemplateChanged += frm_DefaultTemplateChanged;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    //                    
                    LoadTemplates();
                }
            }
        }

        void frm_DefaultTemplateChanged(object sender, TemplateEventArgs e)
        {
            _mailMerge[0] = e.Template;
            LoadTemplate(_mailMerge);
            LoadTemplates();
        }

        private void barEditItem2_EditValueChanged(object sender, EventArgs e)
        {
            string donViTinh = barDonViTinh.EditValue.ToString();
            if (donViTinh == "Centimet")
                Settings(DevExpress.Office.DocumentUnit.Centimeter);
            else if (donViTinh == "Millimet")
                Settings(DevExpress.Office.DocumentUnit.Millimeter);
            else if (donViTinh == "Document")
                Settings(DevExpress.Office.DocumentUnit.Document);
            else if (donViTinh == "Inch")
                Settings(DevExpress.Office.DocumentUnit.Inch);
            else if (donViTinh == "Point")
                Settings(DevExpress.Office.DocumentUnit.Point);
        }

        private void Settings(DocumentUnit unit)
        {
            richDetail.Unit = unit;
            richDetail1.Unit = unit;
            richMaster.Unit = unit;
            richMaster1.Unit = unit;
            richTemplate.Unit = unit;
            richResult.Unit = unit;
        }
    }
}