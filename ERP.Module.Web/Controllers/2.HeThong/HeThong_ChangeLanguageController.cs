using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System.Globalization;
using DevExpress.ExpressApp.Xpo;
using ERP.Module.HeThong;
using ERP.Module.Commons;
using ERP.Module.Enum.Systems;

namespace ERP.Module.Web.Controllers.HeThong
{
    public partial class HeThong_ChangeLanguageController : WindowController
    {
        //
        private string _defaultCulture;
        private string _defaultFormattingCulture;
        SecuritySystemUser_Custom _userCurrent = null;

        public HeThong_ChangeLanguageController()
        {
            InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            //Set ngôn ngữ
            GetDefaultCulture();
        }
        private void GetDefaultCulture()
        {
            //
            _userCurrent = Common.SecuritySystemUser_GetCurrentUser();
            if (_userCurrent == null) return;
            //
            if (_userCurrent.LoaiNgonNgu == LoaiNgonNguEnum.VietNammese)
            {
                _defaultCulture = "vi-VN";
                _defaultFormattingCulture = "vi-VN";

                ChooseLanguage.Items.Add(new ChoiceActionItem(string.Format("Default ({0})", _defaultCulture), _defaultCulture));
                ChooseLanguage.Items.Add(new ChoiceActionItem("English (en)", "en-EN"));

                ChooseFormattingCulture.Items.Add(new ChoiceActionItem(string.Format("Default ({0})", _defaultFormattingCulture), _defaultFormattingCulture));
                ChooseFormattingCulture.Items.Add(new ChoiceActionItem("English (en)", "en-EN"));
            }
            else
            {
                _defaultCulture = "en-EN";
                _defaultFormattingCulture = "en-EN";

                ChooseLanguage.Items.Add(new ChoiceActionItem(string.Format("Default ({0})", _defaultCulture), _defaultCulture));
                ChooseLanguage.Items.Add(new ChoiceActionItem("Vietnamese (vi)", "vi-VN"));

                ChooseFormattingCulture.Items.Add(new ChoiceActionItem(string.Format("Default ({0})", _defaultFormattingCulture), _defaultFormattingCulture));
                ChooseFormattingCulture.Items.Add(new ChoiceActionItem("Vietnamese (vi)", "vi-VN"));
            }
        }

        private void ChooseLanguage_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            //
            Application.SetLanguage(e.SelectedChoiceActionItem.Data as string);
            Application.Model.PreferredLanguage = e.SelectedChoiceActionItem.Data as string;
            //
            IObjectSpace obs = Application.CreateObjectSpace();
            //
            if ((e.SelectedChoiceActionItem.Data as string) == "en-EN" && _userCurrent.LoaiNgonNgu != LoaiNgonNguEnum.English)
            {
                _userCurrent.LoaiNgonNgu = LoaiNgonNguEnum.English;
                //
                AuthenticationStandard_Custom._LoaiNgonNgu = LoaiNgonNguEnum.English;
            }
            else if ((e.SelectedChoiceActionItem.Data as string) == "vi-VN" && _userCurrent.LoaiNgonNgu != LoaiNgonNguEnum.VietNammese)
            {
                _userCurrent.LoaiNgonNgu = LoaiNgonNguEnum.VietNammese;
                //
                AuthenticationStandard_Custom._LoaiNgonNgu = LoaiNgonNguEnum.VietNammese;
            }

            //
            obs.CommitChanges();
        }

        private void ChooseFormattingCulture_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            Application.SetFormattingCulture(e.SelectedChoiceActionItem.Data as string);
            Application.Model.PreferredLanguage = e.SelectedChoiceActionItem.Data as string;
        }
    }
}
