﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ERP.Module.Win.NormalForm.MailMerge
{
    partial class frmShowMailMerge<T>
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup1 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.XtraBars.Ribbon.GalleryItemGroup galleryItemGroup2 = new DevExpress.XtraBars.Ribbon.GalleryItemGroup();
            DevExpress.XtraRichEdit.Model.BorderInfo borderInfo1 = new DevExpress.XtraRichEdit.Model.BorderInfo();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraBars.Ribbon.ReduceOperation reduceOperation1 = new DevExpress.XtraBars.Ribbon.ReduceOperation();
            this.stylesRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.StylesRibbonPageGroup();
            this.galleryChangeStyleItem1 = new DevExpress.XtraRichEdit.UI.GalleryChangeStyleItem();
            this.repositoryItemFontEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.repositoryItemRichEditFontSizeEdit1 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit();
            this.richTemplate = new DevExpress.XtraRichEdit.RichEditControl();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.applicationMenu1 = new DevExpress.XtraBars.Ribbon.ApplicationMenu(this.components);
            this.fileNewItem1 = new DevExpress.XtraRichEdit.UI.FileNewItem();
            this.fileOpenItem1 = new DevExpress.XtraRichEdit.UI.FileOpenItem();
            this.fileSaveItem1 = new DevExpress.XtraRichEdit.UI.FileSaveItem();
            this.fileSaveAsItem1 = new DevExpress.XtraRichEdit.UI.FileSaveAsItem();
            this.quickPrintItem1 = new DevExpress.XtraRichEdit.UI.QuickPrintItem();
            this.printItem1 = new DevExpress.XtraRichEdit.UI.PrintItem();
            this.printPreviewItem1 = new DevExpress.XtraRichEdit.UI.PrintPreviewItem();
            this.mergeToNewDocumentItem = new DevExpress.XtraBars.BarButtonItem();
            this.barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemZoomTrackBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar();
            this.barButtonGroup1 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup2 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup3 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup4 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup5 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup6 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup7 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup8 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup9 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup10 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup11 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup12 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup13 = new DevExpress.XtraBars.BarButtonGroup();
            this.barButtonGroup14 = new DevExpress.XtraBars.BarButtonGroup();
            this.undoItem1 = new DevExpress.XtraRichEdit.UI.UndoItem();
            this.redoItem1 = new DevExpress.XtraRichEdit.UI.RedoItem();
            this.pasteItem1 = new DevExpress.XtraRichEdit.UI.PasteItem();
            this.cutItem1 = new DevExpress.XtraRichEdit.UI.CutItem();
            this.copyItem1 = new DevExpress.XtraRichEdit.UI.CopyItem();
            this.pasteSpecialItem1 = new DevExpress.XtraRichEdit.UI.PasteSpecialItem();
            this.barButtonGroup15 = new DevExpress.XtraBars.BarButtonGroup();
            this.changeFontNameItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontNameItem();
            this.repositoryItemFontEdit3 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.changeFontSizeItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontSizeItem();
            this.repositoryItemRichEditFontSizeEdit3 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit();
            this.fontSizeIncreaseItem1 = new DevExpress.XtraRichEdit.UI.FontSizeIncreaseItem();
            this.fontSizeDecreaseItem1 = new DevExpress.XtraRichEdit.UI.FontSizeDecreaseItem();
            this.barButtonGroup16 = new DevExpress.XtraBars.BarButtonGroup();
            this.toggleFontBoldItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontBoldItem();
            this.toggleFontItalicItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontItalicItem();
            this.toggleFontUnderlineItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem();
            this.toggleFontDoubleUnderlineItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontDoubleUnderlineItem();
            this.toggleFontStrikeoutItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontStrikeoutItem();
            this.toggleFontDoubleStrikeoutItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontDoubleStrikeoutItem();
            this.toggleFontSuperscriptItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontSuperscriptItem();
            this.toggleFontSubscriptItem1 = new DevExpress.XtraRichEdit.UI.ToggleFontSubscriptItem();
            this.barButtonGroup17 = new DevExpress.XtraBars.BarButtonGroup();
            this.changeFontColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontColorItem();
            this.changeFontBackColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeFontBackColorItem();
            this.changeTextCaseItem1 = new DevExpress.XtraRichEdit.UI.ChangeTextCaseItem();
            this.makeTextUpperCaseItem1 = new DevExpress.XtraRichEdit.UI.MakeTextUpperCaseItem();
            this.makeTextLowerCaseItem1 = new DevExpress.XtraRichEdit.UI.MakeTextLowerCaseItem();
            this.toggleTextCaseItem1 = new DevExpress.XtraRichEdit.UI.ToggleTextCaseItem();
            this.clearFormattingItem1 = new DevExpress.XtraRichEdit.UI.ClearFormattingItem();
            this.barButtonGroup18 = new DevExpress.XtraBars.BarButtonGroup();
            this.toggleBulletedListItem1 = new DevExpress.XtraRichEdit.UI.ToggleBulletedListItem();
            this.toggleNumberingListItem1 = new DevExpress.XtraRichEdit.UI.ToggleNumberingListItem();
            this.toggleMultiLevelListItem1 = new DevExpress.XtraRichEdit.UI.ToggleMultiLevelListItem();
            this.barButtonGroup19 = new DevExpress.XtraBars.BarButtonGroup();
            this.decreaseIndentItem1 = new DevExpress.XtraRichEdit.UI.DecreaseIndentItem();
            this.increaseIndentItem1 = new DevExpress.XtraRichEdit.UI.IncreaseIndentItem();
            this.toggleShowWhitespaceItem1 = new DevExpress.XtraRichEdit.UI.ToggleShowWhitespaceItem();
            this.barButtonGroup20 = new DevExpress.XtraBars.BarButtonGroup();
            this.toggleParagraphAlignmentLeftItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentLeftItem();
            this.toggleParagraphAlignmentCenterItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentCenterItem();
            this.toggleParagraphAlignmentRightItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentRightItem();
            this.toggleParagraphAlignmentJustifyItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentJustifyItem();
            this.barButtonGroup21 = new DevExpress.XtraBars.BarButtonGroup();
            this.changeParagraphLineSpacingItem1 = new DevExpress.XtraRichEdit.UI.ChangeParagraphLineSpacingItem();
            this.setSingleParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem();
            this.setSesquialteralParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem();
            this.setDoubleParagraphSpacingItem1 = new DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem();
            this.showLineSpacingFormItem1 = new DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem();
            this.addSpacingBeforeParagraphItem1 = new DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem();
            this.removeSpacingBeforeParagraphItem1 = new DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem();
            this.addSpacingAfterParagraphItem1 = new DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem();
            this.removeSpacingAfterParagraphItem1 = new DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem();
            this.changeParagraphBackColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeParagraphBackColorItem();
            this.findItem1 = new DevExpress.XtraRichEdit.UI.FindItem();
            this.replaceItem1 = new DevExpress.XtraRichEdit.UI.ReplaceItem();
            this.insertPageBreakItem1 = new DevExpress.XtraRichEdit.UI.InsertPageBreakItem();
            this.insertTableItem1 = new DevExpress.XtraRichEdit.UI.InsertTableItem();
            this.insertPictureItem1 = new DevExpress.XtraRichEdit.UI.InsertPictureItem();
            this.insertFloatingPictureItem1 = new DevExpress.XtraRichEdit.UI.InsertFloatingPictureItem();
            this.insertBookmarkItem1 = new DevExpress.XtraRichEdit.UI.InsertBookmarkItem();
            this.insertHyperlinkItem1 = new DevExpress.XtraRichEdit.UI.InsertHyperlinkItem();
            this.editPageHeaderItem1 = new DevExpress.XtraRichEdit.UI.EditPageHeaderItem();
            this.editPageFooterItem1 = new DevExpress.XtraRichEdit.UI.EditPageFooterItem();
            this.insertPageNumberItem1 = new DevExpress.XtraRichEdit.UI.InsertPageNumberItem();
            this.insertPageCountItem1 = new DevExpress.XtraRichEdit.UI.InsertPageCountItem();
            this.insertTextBoxItem1 = new DevExpress.XtraRichEdit.UI.InsertTextBoxItem();
            this.insertSymbolItem1 = new DevExpress.XtraRichEdit.UI.InsertSymbolItem();
            this.changeSectionPageMarginsItem1 = new DevExpress.XtraRichEdit.UI.ChangeSectionPageMarginsItem();
            this.setNormalSectionPageMarginsItem1 = new DevExpress.XtraRichEdit.UI.SetNormalSectionPageMarginsItem();
            this.setNarrowSectionPageMarginsItem1 = new DevExpress.XtraRichEdit.UI.SetNarrowSectionPageMarginsItem();
            this.setModerateSectionPageMarginsItem1 = new DevExpress.XtraRichEdit.UI.SetModerateSectionPageMarginsItem();
            this.setWideSectionPageMarginsItem1 = new DevExpress.XtraRichEdit.UI.SetWideSectionPageMarginsItem();
            this.showPageMarginsSetupFormItem1 = new DevExpress.XtraRichEdit.UI.ShowPageMarginsSetupFormItem();
            this.changeSectionPageOrientationItem1 = new DevExpress.XtraRichEdit.UI.ChangeSectionPageOrientationItem();
            this.setPortraitPageOrientationItem1 = new DevExpress.XtraRichEdit.UI.SetPortraitPageOrientationItem();
            this.setLandscapePageOrientationItem1 = new DevExpress.XtraRichEdit.UI.SetLandscapePageOrientationItem();
            this.changeSectionPaperKindItem1 = new DevExpress.XtraRichEdit.UI.ChangeSectionPaperKindItem();
            this.changeSectionColumnsItem1 = new DevExpress.XtraRichEdit.UI.ChangeSectionColumnsItem();
            this.setSectionOneColumnItem1 = new DevExpress.XtraRichEdit.UI.SetSectionOneColumnItem();
            this.setSectionTwoColumnsItem1 = new DevExpress.XtraRichEdit.UI.SetSectionTwoColumnsItem();
            this.setSectionThreeColumnsItem1 = new DevExpress.XtraRichEdit.UI.SetSectionThreeColumnsItem();
            this.showColumnsSetupFormItem1 = new DevExpress.XtraRichEdit.UI.ShowColumnsSetupFormItem();
            this.insertBreakItem1 = new DevExpress.XtraRichEdit.UI.InsertBreakItem();
            this.insertColumnBreakItem1 = new DevExpress.XtraRichEdit.UI.InsertColumnBreakItem();
            this.insertSectionBreakNextPageItem1 = new DevExpress.XtraRichEdit.UI.InsertSectionBreakNextPageItem();
            this.insertSectionBreakEvenPageItem1 = new DevExpress.XtraRichEdit.UI.InsertSectionBreakEvenPageItem();
            this.insertSectionBreakOddPageItem1 = new DevExpress.XtraRichEdit.UI.InsertSectionBreakOddPageItem();
            this.changeSectionLineNumberingItem1 = new DevExpress.XtraRichEdit.UI.ChangeSectionLineNumberingItem();
            this.setSectionLineNumberingNoneItem1 = new DevExpress.XtraRichEdit.UI.SetSectionLineNumberingNoneItem();
            this.setSectionLineNumberingContinuousItem1 = new DevExpress.XtraRichEdit.UI.SetSectionLineNumberingContinuousItem();
            this.setSectionLineNumberingRestartNewPageItem1 = new DevExpress.XtraRichEdit.UI.SetSectionLineNumberingRestartNewPageItem();
            this.setSectionLineNumberingRestartNewSectionItem1 = new DevExpress.XtraRichEdit.UI.SetSectionLineNumberingRestartNewSectionItem();
            this.toggleParagraphSuppressLineNumbersItem1 = new DevExpress.XtraRichEdit.UI.ToggleParagraphSuppressLineNumbersItem();
            this.showLineNumberingFormItem1 = new DevExpress.XtraRichEdit.UI.ShowLineNumberingFormItem();
            this.changePageColorItem1 = new DevExpress.XtraRichEdit.UI.ChangePageColorItem();
            this.insertTableOfContentsItem1 = new DevExpress.XtraRichEdit.UI.InsertTableOfContentsItem();
            this.updateTableOfContentsItem1 = new DevExpress.XtraRichEdit.UI.UpdateTableOfContentsItem();
            this.addParagraphsToTableOfContentItem1 = new DevExpress.XtraRichEdit.UI.AddParagraphsToTableOfContentItem();
            this.setParagraphHeadingLevelItem1 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem2 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem3 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem4 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem5 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem6 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem7 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem8 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem9 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.setParagraphHeadingLevelItem10 = new DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem();
            this.insertCaptionPlaceholderItem1 = new DevExpress.XtraRichEdit.UI.InsertCaptionPlaceholderItem();
            this.insertFiguresCaptionItems1 = new DevExpress.XtraRichEdit.UI.InsertFiguresCaptionItems();
            this.insertTablesCaptionItems1 = new DevExpress.XtraRichEdit.UI.InsertTablesCaptionItems();
            this.insertEquationsCaptionItems1 = new DevExpress.XtraRichEdit.UI.InsertEquationsCaptionItems();
            this.insertTableOfFiguresPlaceholderItem1 = new DevExpress.XtraRichEdit.UI.InsertTableOfFiguresPlaceholderItem();
            this.insertTableOfFiguresItems1 = new DevExpress.XtraRichEdit.UI.InsertTableOfFiguresItems();
            this.insertTableOfTablesItems1 = new DevExpress.XtraRichEdit.UI.InsertTableOfTablesItems();
            this.insertTableOfEquationsItems1 = new DevExpress.XtraRichEdit.UI.InsertTableOfEquationsItems();
            this.insertMergeFieldItem1 = new DevExpress.XtraRichEdit.UI.InsertMergeFieldItem();
            this.showAllFieldCodesItem1 = new DevExpress.XtraRichEdit.UI.ShowAllFieldCodesItem();
            this.showAllFieldResultsItem1 = new DevExpress.XtraRichEdit.UI.ShowAllFieldResultsItem();
            this.toggleViewMergedDataItem1 = new DevExpress.XtraRichEdit.UI.ToggleViewMergedDataItem();
            this.checkSpellingItem1 = new DevExpress.XtraRichEdit.UI.CheckSpellingItem();
            this.protectDocumentItem1 = new DevExpress.XtraRichEdit.UI.ProtectDocumentItem();
            this.changeRangeEditingPermissionsItem1 = new DevExpress.XtraRichEdit.UI.ChangeRangeEditingPermissionsItem();
            this.unprotectDocumentItem1 = new DevExpress.XtraRichEdit.UI.UnprotectDocumentItem();
            this.switchToSimpleViewItem1 = new DevExpress.XtraRichEdit.UI.SwitchToSimpleViewItem();
            this.switchToDraftViewItem1 = new DevExpress.XtraRichEdit.UI.SwitchToDraftViewItem();
            this.switchToPrintLayoutViewItem1 = new DevExpress.XtraRichEdit.UI.SwitchToPrintLayoutViewItem();
            this.toggleShowHorizontalRulerItem1 = new DevExpress.XtraRichEdit.UI.ToggleShowHorizontalRulerItem();
            this.toggleShowVerticalRulerItem1 = new DevExpress.XtraRichEdit.UI.ToggleShowVerticalRulerItem();
            this.zoomOutItem1 = new DevExpress.XtraRichEdit.UI.ZoomOutItem();
            this.zoomInItem1 = new DevExpress.XtraRichEdit.UI.ZoomInItem();
            this.goToPageHeaderItem1 = new DevExpress.XtraRichEdit.UI.GoToPageHeaderItem();
            this.goToPageFooterItem1 = new DevExpress.XtraRichEdit.UI.GoToPageFooterItem();
            this.goToNextHeaderFooterItem1 = new DevExpress.XtraRichEdit.UI.GoToNextHeaderFooterItem();
            this.goToPreviousHeaderFooterItem1 = new DevExpress.XtraRichEdit.UI.GoToPreviousHeaderFooterItem();
            this.toggleLinkToPreviousItem1 = new DevExpress.XtraRichEdit.UI.ToggleLinkToPreviousItem();
            this.toggleDifferentFirstPageItem1 = new DevExpress.XtraRichEdit.UI.ToggleDifferentFirstPageItem();
            this.toggleDifferentOddAndEvenPagesItem1 = new DevExpress.XtraRichEdit.UI.ToggleDifferentOddAndEvenPagesItem();
            this.closePageHeaderFooterItem1 = new DevExpress.XtraRichEdit.UI.ClosePageHeaderFooterItem();
            this.toggleFirstRowItem1 = new DevExpress.XtraRichEdit.UI.ToggleFirstRowItem();
            this.toggleLastRowItem1 = new DevExpress.XtraRichEdit.UI.ToggleLastRowItem();
            this.toggleBandedRowsItem1 = new DevExpress.XtraRichEdit.UI.ToggleBandedRowsItem();
            this.toggleFirstColumnItem1 = new DevExpress.XtraRichEdit.UI.ToggleFirstColumnItem();
            this.toggleLastColumnItem1 = new DevExpress.XtraRichEdit.UI.ToggleLastColumnItem();
            this.toggleBandedColumnItem1 = new DevExpress.XtraRichEdit.UI.ToggleBandedColumnItem();
            this.galleryChangeTableStyleItem1 = new DevExpress.XtraRichEdit.UI.GalleryChangeTableStyleItem();
            this.changeTableBorderLineStyleItem1 = new DevExpress.XtraRichEdit.UI.ChangeTableBorderLineStyleItem();
            this.repositoryItemBorderLineStyle4 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle();
            this.changeTableBorderLineWeightItem1 = new DevExpress.XtraRichEdit.UI.ChangeTableBorderLineWeightItem();
            this.repositoryItemBorderLineWeight4 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight();
            this.changeTableBorderColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeTableBorderColorItem();
            this.changeTableBordersItem1 = new DevExpress.XtraRichEdit.UI.ChangeTableBordersItem();
            this.toggleTableCellsBottomBorderItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomBorderItem();
            this.toggleTableCellsTopBorderItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsTopBorderItem();
            this.toggleTableCellsLeftBorderItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsLeftBorderItem();
            this.toggleTableCellsRightBorderItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsRightBorderItem();
            this.resetTableCellsAllBordersItem1 = new DevExpress.XtraRichEdit.UI.ResetTableCellsAllBordersItem();
            this.toggleTableCellsAllBordersItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsAllBordersItem();
            this.toggleTableCellsOutsideBorderItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsOutsideBorderItem();
            this.toggleTableCellsInsideBorderItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideBorderItem();
            this.toggleTableCellsInsideHorizontalBorderItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideHorizontalBorderItem();
            this.toggleTableCellsInsideVerticalBorderItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideVerticalBorderItem();
            this.toggleShowTableGridLinesItem1 = new DevExpress.XtraRichEdit.UI.ToggleShowTableGridLinesItem();
            this.changeTableCellsShadingItem1 = new DevExpress.XtraRichEdit.UI.ChangeTableCellsShadingItem();
            this.selectTableElementsItem1 = new DevExpress.XtraRichEdit.UI.SelectTableElementsItem();
            this.selectTableCellItem1 = new DevExpress.XtraRichEdit.UI.SelectTableCellItem();
            this.selectTableColumnItem1 = new DevExpress.XtraRichEdit.UI.SelectTableColumnItem();
            this.selectTableRowItem1 = new DevExpress.XtraRichEdit.UI.SelectTableRowItem();
            this.selectTableItem1 = new DevExpress.XtraRichEdit.UI.SelectTableItem();
            this.showTablePropertiesFormItem1 = new DevExpress.XtraRichEdit.UI.ShowTablePropertiesFormItem();
            this.deleteTableElementsItem1 = new DevExpress.XtraRichEdit.UI.DeleteTableElementsItem();
            this.showDeleteTableCellsFormItem1 = new DevExpress.XtraRichEdit.UI.ShowDeleteTableCellsFormItem();
            this.deleteTableColumnsItem1 = new DevExpress.XtraRichEdit.UI.DeleteTableColumnsItem();
            this.deleteTableRowsItem1 = new DevExpress.XtraRichEdit.UI.DeleteTableRowsItem();
            this.deleteTableItem1 = new DevExpress.XtraRichEdit.UI.DeleteTableItem();
            this.insertTableRowAboveItem1 = new DevExpress.XtraRichEdit.UI.InsertTableRowAboveItem();
            this.insertTableRowBelowItem1 = new DevExpress.XtraRichEdit.UI.InsertTableRowBelowItem();
            this.insertTableColumnToLeftItem1 = new DevExpress.XtraRichEdit.UI.InsertTableColumnToLeftItem();
            this.insertTableColumnToRightItem1 = new DevExpress.XtraRichEdit.UI.InsertTableColumnToRightItem();
            this.mergeTableCellsItem1 = new DevExpress.XtraRichEdit.UI.MergeTableCellsItem();
            this.showSplitTableCellsForm1 = new DevExpress.XtraRichEdit.UI.ShowSplitTableCellsForm();
            this.splitTableItem1 = new DevExpress.XtraRichEdit.UI.SplitTableItem();
            this.toggleTableAutoFitItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableAutoFitItem();
            this.toggleTableAutoFitContentsItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableAutoFitContentsItem();
            this.toggleTableAutoFitWindowItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableAutoFitWindowItem();
            this.toggleTableFixedColumnWidthItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableFixedColumnWidthItem();
            this.toggleTableCellsTopLeftAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsTopLeftAlignmentItem();
            this.toggleTableCellsMiddleLeftAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleLeftAlignmentItem();
            this.toggleTableCellsBottomLeftAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomLeftAlignmentItem();
            this.toggleTableCellsTopCenterAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsTopCenterAlignmentItem();
            this.toggleTableCellsMiddleCenterAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleCenterAlignmentItem();
            this.toggleTableCellsBottomCenterAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomCenterAlignmentItem();
            this.toggleTableCellsTopRightAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsTopRightAlignmentItem();
            this.toggleTableCellsMiddleRightAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleRightAlignmentItem();
            this.toggleTableCellsBottomRightAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomRightAlignmentItem();
            this.showTableOptionsFormItem1 = new DevExpress.XtraRichEdit.UI.ShowTableOptionsFormItem();
            this.changeFloatingObjectFillColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeFloatingObjectFillColorItem();
            this.changeFloatingObjectOutlineColorItem1 = new DevExpress.XtraRichEdit.UI.ChangeFloatingObjectOutlineColorItem();
            this.changeFloatingObjectOutlineWeightItem1 = new DevExpress.XtraRichEdit.UI.ChangeFloatingObjectOutlineWeightItem();
            this.repositoryItemFloatingObjectOutlineWeight3 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemFloatingObjectOutlineWeight();
            this.changeFloatingObjectTextWrapTypeItem1 = new DevExpress.XtraRichEdit.UI.ChangeFloatingObjectTextWrapTypeItem();
            this.setFloatingObjectSquareTextWrapTypeItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectSquareTextWrapTypeItem();
            this.setFloatingObjectTightTextWrapTypeItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectTightTextWrapTypeItem();
            this.setFloatingObjectThroughTextWrapTypeItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectThroughTextWrapTypeItem();
            this.setFloatingObjectTopAndBottomTextWrapTypeItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectTopAndBottomTextWrapTypeItem();
            this.setFloatingObjectBehindTextWrapTypeItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectBehindTextWrapTypeItem();
            this.setFloatingObjectInFrontOfTextWrapTypeItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectInFrontOfTextWrapTypeItem();
            this.changeFloatingObjectAlignmentItem1 = new DevExpress.XtraRichEdit.UI.ChangeFloatingObjectAlignmentItem();
            this.setFloatingObjectTopLeftAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectTopLeftAlignmentItem();
            this.setFloatingObjectTopCenterAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectTopCenterAlignmentItem();
            this.setFloatingObjectTopRightAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectTopRightAlignmentItem();
            this.setFloatingObjectMiddleLeftAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleLeftAlignmentItem();
            this.setFloatingObjectMiddleCenterAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleCenterAlignmentItem();
            this.setFloatingObjectMiddleRightAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleRightAlignmentItem();
            this.setFloatingObjectBottomLeftAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomLeftAlignmentItem();
            this.setFloatingObjectBottomCenterAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomCenterAlignmentItem();
            this.setFloatingObjectBottomRightAlignmentItem1 = new DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomRightAlignmentItem();
            this.floatingObjectBringForwardSubItem1 = new DevExpress.XtraRichEdit.UI.FloatingObjectBringForwardSubItem();
            this.floatingObjectBringForwardItem1 = new DevExpress.XtraRichEdit.UI.FloatingObjectBringForwardItem();
            this.floatingObjectBringToFrontItem1 = new DevExpress.XtraRichEdit.UI.FloatingObjectBringToFrontItem();
            this.floatingObjectBringInFrontOfTextItem1 = new DevExpress.XtraRichEdit.UI.FloatingObjectBringInFrontOfTextItem();
            this.floatingObjectSendBackwardSubItem1 = new DevExpress.XtraRichEdit.UI.FloatingObjectSendBackwardSubItem();
            this.floatingObjectSendBackwardItem1 = new DevExpress.XtraRichEdit.UI.FloatingObjectSendBackwardItem();
            this.floatingObjectSendToBackItem1 = new DevExpress.XtraRichEdit.UI.FloatingObjectSendToBackItem();
            this.floatingObjectSendBehindTextItem1 = new DevExpress.XtraRichEdit.UI.FloatingObjectSendBehindTextItem();
            this.btnMergeDocuments = new DevExpress.XtraBars.BarButtonItem();
            this.btnSave = new DevExpress.XtraBars.BarButtonItem();
            this.btnInsertMergeField = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.barBieuMau = new DevExpress.XtraBars.BarEditItem();
            this.lookupBieuMau = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.barDonViTinh = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.headerFooterToolsRibbonPageCategory1 = new DevExpress.XtraRichEdit.UI.HeaderFooterToolsRibbonPageCategory();
            this.headerFooterToolsDesignRibbonPage1 = new DevExpress.XtraRichEdit.UI.HeaderFooterToolsDesignRibbonPage();
            this.headerFooterToolsDesignNavigationRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.HeaderFooterToolsDesignNavigationRibbonPageGroup();
            this.headerFooterToolsDesignOptionsRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.HeaderFooterToolsDesignOptionsRibbonPageGroup();
            this.headerFooterToolsDesignCloseRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.HeaderFooterToolsDesignCloseRibbonPageGroup();
            this.tableToolsRibbonPageCategory1 = new DevExpress.XtraRichEdit.UI.TableToolsRibbonPageCategory();
            this.tableDesignRibbonPage1 = new DevExpress.XtraRichEdit.UI.TableDesignRibbonPage();
            this.tableStyleOptionsRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableStyleOptionsRibbonPageGroup();
            this.tableStylesRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableStylesRibbonPageGroup();
            this.tableDrawBordersRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableDrawBordersRibbonPageGroup();
            this.tableLayoutRibbonPage1 = new DevExpress.XtraRichEdit.UI.TableLayoutRibbonPage();
            this.tableTableRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableTableRibbonPageGroup();
            this.tableRowsAndColumnsRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableRowsAndColumnsRibbonPageGroup();
            this.tableMergeRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableMergeRibbonPageGroup();
            this.tableCellSizeRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableCellSizeRibbonPageGroup();
            this.tableAlignmentRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableAlignmentRibbonPageGroup();
            this.floatingPictureToolsRibbonPageCategory1 = new DevExpress.XtraRichEdit.UI.FloatingPictureToolsRibbonPageCategory();
            this.floatingPictureToolsFormatPage1 = new DevExpress.XtraRichEdit.UI.FloatingPictureToolsFormatPage();
            this.floatingPictureToolsShapeStylesPageGroup1 = new DevExpress.XtraRichEdit.UI.FloatingPictureToolsShapeStylesPageGroup();
            this.floatingPictureToolsArrangePageGroup1 = new DevExpress.XtraRichEdit.UI.FloatingPictureToolsArrangePageGroup();
            this.homeRibbonPage2 = new DevExpress.XtraRichEdit.UI.HomeRibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.clipboardRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.ClipboardRibbonPageGroup();
            this.fontRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.FontRibbonPageGroup();
            this.paragraphRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.ParagraphRibbonPageGroup();
            this.editingRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.EditingRibbonPageGroup();
            this.insertRibbonPage2 = new DevExpress.XtraRichEdit.UI.InsertRibbonPage();
            this.pagesRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.PagesRibbonPageGroup();
            this.tablesRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.TablesRibbonPageGroup();
            this.illustrationsRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.IllustrationsRibbonPageGroup();
            this.linksRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.LinksRibbonPageGroup();
            this.headerFooterRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.HeaderFooterRibbonPageGroup();
            this.textRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.TextRibbonPageGroup();
            this.symbolsRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.SymbolsRibbonPageGroup();
            this.pageLayoutRibbonPage2 = new DevExpress.XtraRichEdit.UI.PageLayoutRibbonPage();
            this.pageSetupRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.PageSetupRibbonPageGroup();
            this.pageBackgroundRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.PageBackgroundRibbonPageGroup();
            this.referencesRibbonPage2 = new DevExpress.XtraRichEdit.UI.ReferencesRibbonPage();
            this.tableOfContentsRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.TableOfContentsRibbonPageGroup();
            this.captionsRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.CaptionsRibbonPageGroup();
            this.mailingsRibbonPage2 = new DevExpress.XtraRichEdit.UI.MailingsRibbonPage();
            this.mailMergeRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.MailMergeRibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.reviewRibbonPage2 = new DevExpress.XtraRichEdit.UI.ReviewRibbonPage();
            this.documentProofingRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.DocumentProofingRibbonPageGroup();
            this.documentProtectionRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.DocumentProtectionRibbonPageGroup();
            this.viewRibbonPage2 = new DevExpress.XtraRichEdit.UI.ViewRibbonPage();
            this.documentViewsRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.DocumentViewsRibbonPageGroup();
            this.showRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.ShowRibbonPageGroup();
            this.zoomRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.ZoomRibbonPageGroup();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemBorderLineStyle1 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle();
            this.richResult = new DevExpress.XtraRichEdit.RichEditControl();
            this.repositoryItemBorderLineWeight1 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight();
            this.repositoryItemRichEditStyleEdit1 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit();
            this.repositoryItemBorderLineStyle2 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle();
            this.repositoryItemBorderLineWeight2 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight();
            this.repositoryItemFloatingObjectOutlineWeight1 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemFloatingObjectOutlineWeight();
            this.repositoryItemFontEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemFontEdit();
            this.repositoryItemRichEditFontSizeEdit2 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit();
            this.repositoryItemRichEditStyleEdit2 = new DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit();
            this.repositoryItemBorderLineStyle3 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle();
            this.repositoryItemBorderLineWeight3 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight();
            this.repositoryItemFloatingObjectOutlineWeight2 = new DevExpress.XtraRichEdit.Forms.Design.RepositoryItemFloatingObjectOutlineWeight();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.backstageViewControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewControl();
            this.backstageViewClientControl1 = new DevExpress.XtraBars.Ribbon.BackstageViewClientControl();
            this.backstageViewTabItem1 = new DevExpress.XtraBars.Ribbon.BackstageViewTabItem();
            this.homeRibbonPage1 = new DevExpress.XtraRichEdit.UI.HomeRibbonPage();
            this.clipboardRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.ClipboardRibbonPageGroup();
            this.fontRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.FontRibbonPageGroup();
            this.paragraphRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.ParagraphRibbonPageGroup();
            this.stylesRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.StylesRibbonPageGroup();
            this.editingRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.EditingRibbonPageGroup();
            this.fileRibbonPage1 = new DevExpress.XtraRichEdit.UI.FileRibbonPage();
            this.commonRibbonPageGroup2 = new DevExpress.XtraRichEdit.UI.CommonRibbonPageGroup();
            this.insertRibbonPage1 = new DevExpress.XtraRichEdit.UI.InsertRibbonPage();
            this.pagesRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.PagesRibbonPageGroup();
            this.tablesRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TablesRibbonPageGroup();
            this.illustrationsRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.IllustrationsRibbonPageGroup();
            this.linksRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.LinksRibbonPageGroup();
            this.headerFooterRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.HeaderFooterRibbonPageGroup();
            this.textRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TextRibbonPageGroup();
            this.symbolsRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.SymbolsRibbonPageGroup();
            this.pageLayoutRibbonPage1 = new DevExpress.XtraRichEdit.UI.PageLayoutRibbonPage();
            this.pageSetupRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.PageSetupRibbonPageGroup();
            this.mailingsRibbonPage1 = new DevExpress.XtraRichEdit.UI.MailingsRibbonPage();
            this.mailMergeRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.MailMergeRibbonPageGroup();
            this.viewRibbonPage1 = new DevExpress.XtraRichEdit.UI.ViewRibbonPage();
            this.documentViewsRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.DocumentViewsRibbonPageGroup();
            this.showRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.ShowRibbonPageGroup();
            this.zoomRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.ZoomRibbonPageGroup();
            this.reviewRibbonPage1 = new DevExpress.XtraRichEdit.UI.ReviewRibbonPage();
            this.documentProofingRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.DocumentProofingRibbonPageGroup();
            this.documentProtectionRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.DocumentProtectionRibbonPageGroup();
            this.referencesRibbonPage1 = new DevExpress.XtraRichEdit.UI.ReferencesRibbonPage();
            this.tableOfContentsRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.TableOfContentsRibbonPageGroup();
            this.captionsRibbonPageGroup1 = new DevExpress.XtraRichEdit.UI.CaptionsRibbonPageGroup();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabDetail = new DevExpress.XtraTab.XtraTabPage();
            this.richDetail = new DevExpress.XtraRichEdit.RichEditControl();
            this.tabTemplate = new DevExpress.XtraTab.XtraTabPage();
            this.tabMaster = new DevExpress.XtraTab.XtraTabPage();
            this.richMaster = new DevExpress.XtraRichEdit.RichEditControl();
            this.tabMaster1 = new DevExpress.XtraTab.XtraTabPage();
            this.richMaster1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.tabDetail1 = new DevExpress.XtraTab.XtraTabPage();
            this.richDetail1 = new DevExpress.XtraRichEdit.RichEditControl();
            this.tabResult = new DevExpress.XtraTab.XtraTabPage();
            this.richEditBarController1 = new DevExpress.XtraRichEdit.UI.RichEditBarController();
            this.insertPageBreakItem2 = new DevExpress.XtraRichEdit.UI.InsertPageBreakItem();
            ((ISupportInitialize)(this.repositoryItemFontEdit1)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit1)).BeginInit();
            ((ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((ISupportInitialize)(this.applicationMenu1)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemZoomTrackBar1)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemFontEdit3)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit3)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineStyle4)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineWeight4)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemFloatingObjectOutlineWeight3)).BeginInit();
            ((ISupportInitialize)(this.lookupBieuMau)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineStyle1)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineWeight1)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemRichEditStyleEdit1)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineStyle2)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineWeight2)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemFloatingObjectOutlineWeight1)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemFontEdit2)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit2)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemRichEditStyleEdit2)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineStyle3)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineWeight3)).BeginInit();
            ((ISupportInitialize)(this.repositoryItemFloatingObjectOutlineWeight2)).BeginInit();
            this.backstageViewControl1.SuspendLayout();
            ((ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.tabDetail.SuspendLayout();
            this.tabTemplate.SuspendLayout();
            this.tabMaster.SuspendLayout();
            this.tabMaster1.SuspendLayout();
            this.tabDetail1.SuspendLayout();
            this.tabResult.SuspendLayout();
            ((ISupportInitialize)(this.richEditBarController1)).BeginInit();
            this.SuspendLayout();
            // 
            // stylesRibbonPageGroup2
            // 
            this.stylesRibbonPageGroup2.ItemLinks.Add(this.galleryChangeStyleItem1);
            this.stylesRibbonPageGroup2.Name = "stylesRibbonPageGroup2";
            // 
            // galleryChangeStyleItem1
            // 
            // 
            // 
            // 
            this.galleryChangeStyleItem1.Gallery.ColumnCount = 10;
            this.galleryChangeStyleItem1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup1});
            this.galleryChangeStyleItem1.Gallery.ImageSize = new Size(65, 46);
            this.galleryChangeStyleItem1.Id = 486;
            this.galleryChangeStyleItem1.Name = "galleryChangeStyleItem1";
            // 
            // repositoryItemFontEdit1
            // 
            this.repositoryItemFontEdit1.AutoHeight = false;
            this.repositoryItemFontEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit1.Name = "repositoryItemFontEdit1";
            // 
            // repositoryItemRichEditFontSizeEdit1
            // 
            this.repositoryItemRichEditFontSizeEdit1.AutoHeight = false;
            this.repositoryItemRichEditFontSizeEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditFontSizeEdit1.Control = this.richTemplate;
            this.repositoryItemRichEditFontSizeEdit1.Name = "repositoryItemRichEditFontSizeEdit1";
            // 
            // richTemplate
            // 
            this.richTemplate.Dock = DockStyle.Fill;
            this.richTemplate.Location = new Point(0, 0);
            this.richTemplate.LookAndFeel.SkinName = "Office 2007 Blue";
            this.richTemplate.MenuManager = this.ribbonControl1;
            this.richTemplate.Name = "richTemplate";
            this.richTemplate.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richTemplate.Options.MailMerge.KeepLastParagraph = false;
            this.richTemplate.Size = new Size(1038, 360);
            this.richTemplate.TabIndex = 0;
            this.richTemplate.Unit = DevExpress.Office.DocumentUnit.Centimeter;
            this.richTemplate.ZoomChanged += new EventHandler(this.richEditControl_ZoomChanged);
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ApplicationButtonDropDownControl = this.applicationMenu1;
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.mergeToNewDocumentItem,
            this.barEditItem1,
            this.barButtonGroup1,
            this.barButtonGroup2,
            this.barButtonGroup3,
            this.barButtonGroup4,
            this.barButtonGroup5,
            this.barButtonGroup6,
            this.barButtonGroup7,
            this.barButtonGroup8,
            this.barButtonGroup9,
            this.barButtonGroup10,
            this.barButtonGroup11,
            this.barButtonGroup12,
            this.barButtonGroup13,
            this.barButtonGroup14,
            this.fileNewItem1,
            this.fileOpenItem1,
            this.fileSaveItem1,
            this.fileSaveAsItem1,
            this.quickPrintItem1,
            this.printItem1,
            this.printPreviewItem1,
            this.undoItem1,
            this.redoItem1,
            this.pasteItem1,
            this.cutItem1,
            this.copyItem1,
            this.pasteSpecialItem1,
            this.barButtonGroup15,
            this.changeFontNameItem1,
            this.changeFontSizeItem1,
            this.fontSizeIncreaseItem1,
            this.fontSizeDecreaseItem1,
            this.barButtonGroup16,
            this.toggleFontBoldItem1,
            this.toggleFontItalicItem1,
            this.toggleFontUnderlineItem1,
            this.toggleFontDoubleUnderlineItem1,
            this.toggleFontStrikeoutItem1,
            this.toggleFontDoubleStrikeoutItem1,
            this.toggleFontSuperscriptItem1,
            this.toggleFontSubscriptItem1,
            this.barButtonGroup17,
            this.changeFontColorItem1,
            this.changeFontBackColorItem1,
            this.changeTextCaseItem1,
            this.makeTextUpperCaseItem1,
            this.makeTextLowerCaseItem1,
            this.toggleTextCaseItem1,
            this.clearFormattingItem1,
            this.barButtonGroup18,
            this.toggleBulletedListItem1,
            this.toggleNumberingListItem1,
            this.toggleMultiLevelListItem1,
            this.barButtonGroup19,
            this.decreaseIndentItem1,
            this.increaseIndentItem1,
            this.barButtonGroup20,
            this.toggleParagraphAlignmentLeftItem1,
            this.toggleParagraphAlignmentCenterItem1,
            this.toggleParagraphAlignmentRightItem1,
            this.toggleParagraphAlignmentJustifyItem1,
            this.toggleShowWhitespaceItem1,
            this.barButtonGroup21,
            this.changeParagraphLineSpacingItem1,
            this.setSingleParagraphSpacingItem1,
            this.setSesquialteralParagraphSpacingItem1,
            this.setDoubleParagraphSpacingItem1,
            this.showLineSpacingFormItem1,
            this.addSpacingBeforeParagraphItem1,
            this.removeSpacingBeforeParagraphItem1,
            this.addSpacingAfterParagraphItem1,
            this.removeSpacingAfterParagraphItem1,
            this.changeParagraphBackColorItem1,
            this.galleryChangeStyleItem1,
            this.findItem1,
            this.replaceItem1,
            this.insertPageBreakItem1,
            this.insertTableItem1,
            this.insertPictureItem1,
            this.insertFloatingPictureItem1,
            this.insertBookmarkItem1,
            this.insertHyperlinkItem1,
            this.editPageHeaderItem1,
            this.editPageFooterItem1,
            this.insertPageNumberItem1,
            this.insertPageCountItem1,
            this.insertTextBoxItem1,
            this.insertSymbolItem1,
            this.changeSectionPageMarginsItem1,
            this.setNormalSectionPageMarginsItem1,
            this.setNarrowSectionPageMarginsItem1,
            this.setModerateSectionPageMarginsItem1,
            this.setWideSectionPageMarginsItem1,
            this.showPageMarginsSetupFormItem1,
            this.changeSectionPageOrientationItem1,
            this.setPortraitPageOrientationItem1,
            this.setLandscapePageOrientationItem1,
            this.changeSectionPaperKindItem1,
            this.changeSectionColumnsItem1,
            this.setSectionOneColumnItem1,
            this.setSectionTwoColumnsItem1,
            this.setSectionThreeColumnsItem1,
            this.showColumnsSetupFormItem1,
            this.insertBreakItem1,
            this.insertColumnBreakItem1,
            this.insertSectionBreakNextPageItem1,
            this.insertSectionBreakEvenPageItem1,
            this.insertSectionBreakOddPageItem1,
            this.changeSectionLineNumberingItem1,
            this.setSectionLineNumberingNoneItem1,
            this.setSectionLineNumberingContinuousItem1,
            this.setSectionLineNumberingRestartNewPageItem1,
            this.setSectionLineNumberingRestartNewSectionItem1,
            this.toggleParagraphSuppressLineNumbersItem1,
            this.showLineNumberingFormItem1,
            this.changePageColorItem1,
            this.insertTableOfContentsItem1,
            this.updateTableOfContentsItem1,
            this.addParagraphsToTableOfContentItem1,
            this.setParagraphHeadingLevelItem1,
            this.setParagraphHeadingLevelItem2,
            this.setParagraphHeadingLevelItem3,
            this.setParagraphHeadingLevelItem4,
            this.setParagraphHeadingLevelItem5,
            this.setParagraphHeadingLevelItem6,
            this.setParagraphHeadingLevelItem7,
            this.setParagraphHeadingLevelItem8,
            this.setParagraphHeadingLevelItem9,
            this.setParagraphHeadingLevelItem10,
            this.insertCaptionPlaceholderItem1,
            this.insertFiguresCaptionItems1,
            this.insertTablesCaptionItems1,
            this.insertEquationsCaptionItems1,
            this.insertTableOfFiguresPlaceholderItem1,
            this.insertTableOfFiguresItems1,
            this.insertTableOfTablesItems1,
            this.insertTableOfEquationsItems1,
            this.insertMergeFieldItem1,
            this.showAllFieldCodesItem1,
            this.showAllFieldResultsItem1,
            this.toggleViewMergedDataItem1,
            this.checkSpellingItem1,
            this.protectDocumentItem1,
            this.changeRangeEditingPermissionsItem1,
            this.unprotectDocumentItem1,
            this.switchToSimpleViewItem1,
            this.switchToDraftViewItem1,
            this.switchToPrintLayoutViewItem1,
            this.toggleShowHorizontalRulerItem1,
            this.toggleShowVerticalRulerItem1,
            this.zoomOutItem1,
            this.zoomInItem1,
            this.goToPageHeaderItem1,
            this.goToPageFooterItem1,
            this.goToNextHeaderFooterItem1,
            this.goToPreviousHeaderFooterItem1,
            this.toggleLinkToPreviousItem1,
            this.toggleDifferentFirstPageItem1,
            this.toggleDifferentOddAndEvenPagesItem1,
            this.closePageHeaderFooterItem1,
            this.toggleFirstRowItem1,
            this.toggleLastRowItem1,
            this.toggleBandedRowsItem1,
            this.toggleFirstColumnItem1,
            this.toggleLastColumnItem1,
            this.toggleBandedColumnItem1,
            this.galleryChangeTableStyleItem1,
            this.changeTableBorderLineStyleItem1,
            this.changeTableBorderLineWeightItem1,
            this.changeTableBorderColorItem1,
            this.changeTableBordersItem1,
            this.toggleTableCellsBottomBorderItem1,
            this.toggleTableCellsTopBorderItem1,
            this.toggleTableCellsLeftBorderItem1,
            this.toggleTableCellsRightBorderItem1,
            this.resetTableCellsAllBordersItem1,
            this.toggleTableCellsAllBordersItem1,
            this.toggleTableCellsOutsideBorderItem1,
            this.toggleTableCellsInsideBorderItem1,
            this.toggleTableCellsInsideHorizontalBorderItem1,
            this.toggleTableCellsInsideVerticalBorderItem1,
            this.toggleShowTableGridLinesItem1,
            this.changeTableCellsShadingItem1,
            this.selectTableElementsItem1,
            this.selectTableCellItem1,
            this.selectTableColumnItem1,
            this.selectTableRowItem1,
            this.selectTableItem1,
            this.showTablePropertiesFormItem1,
            this.deleteTableElementsItem1,
            this.showDeleteTableCellsFormItem1,
            this.deleteTableColumnsItem1,
            this.deleteTableRowsItem1,
            this.deleteTableItem1,
            this.insertTableRowAboveItem1,
            this.insertTableRowBelowItem1,
            this.insertTableColumnToLeftItem1,
            this.insertTableColumnToRightItem1,
            this.mergeTableCellsItem1,
            this.showSplitTableCellsForm1,
            this.splitTableItem1,
            this.toggleTableAutoFitItem1,
            this.toggleTableAutoFitContentsItem1,
            this.toggleTableAutoFitWindowItem1,
            this.toggleTableFixedColumnWidthItem1,
            this.toggleTableCellsTopLeftAlignmentItem1,
            this.toggleTableCellsMiddleLeftAlignmentItem1,
            this.toggleTableCellsBottomLeftAlignmentItem1,
            this.toggleTableCellsTopCenterAlignmentItem1,
            this.toggleTableCellsMiddleCenterAlignmentItem1,
            this.toggleTableCellsBottomCenterAlignmentItem1,
            this.toggleTableCellsTopRightAlignmentItem1,
            this.toggleTableCellsMiddleRightAlignmentItem1,
            this.toggleTableCellsBottomRightAlignmentItem1,
            this.showTableOptionsFormItem1,
            this.changeFloatingObjectFillColorItem1,
            this.changeFloatingObjectOutlineColorItem1,
            this.changeFloatingObjectOutlineWeightItem1,
            this.changeFloatingObjectTextWrapTypeItem1,
            this.setFloatingObjectSquareTextWrapTypeItem1,
            this.setFloatingObjectTightTextWrapTypeItem1,
            this.setFloatingObjectThroughTextWrapTypeItem1,
            this.setFloatingObjectTopAndBottomTextWrapTypeItem1,
            this.setFloatingObjectBehindTextWrapTypeItem1,
            this.setFloatingObjectInFrontOfTextWrapTypeItem1,
            this.changeFloatingObjectAlignmentItem1,
            this.setFloatingObjectTopLeftAlignmentItem1,
            this.setFloatingObjectTopCenterAlignmentItem1,
            this.setFloatingObjectTopRightAlignmentItem1,
            this.setFloatingObjectMiddleLeftAlignmentItem1,
            this.setFloatingObjectMiddleCenterAlignmentItem1,
            this.setFloatingObjectMiddleRightAlignmentItem1,
            this.setFloatingObjectBottomLeftAlignmentItem1,
            this.setFloatingObjectBottomCenterAlignmentItem1,
            this.setFloatingObjectBottomRightAlignmentItem1,
            this.floatingObjectBringForwardSubItem1,
            this.floatingObjectBringForwardItem1,
            this.floatingObjectBringToFrontItem1,
            this.floatingObjectBringInFrontOfTextItem1,
            this.floatingObjectSendBackwardSubItem1,
            this.floatingObjectSendBackwardItem1,
            this.floatingObjectSendToBackItem1,
            this.floatingObjectSendBehindTextItem1,
            this.btnMergeDocuments,
            this.btnSave,
            this.btnInsertMergeField,
            this.barButtonItem3,
            this.barBieuMau,
            this.barDonViTinh});
            this.ribbonControl1.Location = new Point(0, 0);
            this.ribbonControl1.MaxItemId = 2;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.PageCategories.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageCategory[] {
            this.headerFooterToolsRibbonPageCategory1,
            this.tableToolsRibbonPageCategory1,
            this.floatingPictureToolsRibbonPageCategory1});
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.homeRibbonPage2,
            this.insertRibbonPage2,
            this.pageLayoutRibbonPage2,
            this.referencesRibbonPage2,
            this.mailingsRibbonPage2,
            this.reviewRibbonPage2,
            this.viewRibbonPage2});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemFontEdit1,
            this.repositoryItemRichEditFontSizeEdit1,
            this.repositoryItemBorderLineStyle1,
            this.repositoryItemBorderLineWeight1,
            this.repositoryItemZoomTrackBar1,
            this.repositoryItemRichEditStyleEdit1,
            this.repositoryItemBorderLineStyle2,
            this.repositoryItemBorderLineWeight2,
            this.repositoryItemFloatingObjectOutlineWeight1,
            this.repositoryItemFontEdit2,
            this.repositoryItemRichEditFontSizeEdit2,
            this.repositoryItemRichEditStyleEdit2,
            this.repositoryItemBorderLineStyle3,
            this.repositoryItemBorderLineWeight3,
            this.repositoryItemFloatingObjectOutlineWeight2,
            this.repositoryItemFontEdit3,
            this.repositoryItemRichEditFontSizeEdit3,
            this.repositoryItemBorderLineStyle4,
            this.repositoryItemBorderLineWeight4,
            this.repositoryItemFloatingObjectOutlineWeight3,
            this.lookupBieuMau,
            this.repositoryItemComboBox1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.Office2013;
            this.ribbonControl1.Size = new Size(1046, 144);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl1.Toolbar.ItemLinks.Add(this.btnSave);
            this.ribbonControl1.Toolbar.ItemLinks.Add(this.btnInsertMergeField);
            this.ribbonControl1.Toolbar.ItemLinks.Add(this.barButtonItem3);
            this.ribbonControl1.Toolbar.ItemLinks.Add(this.barBieuMau);
            // 
            // applicationMenu1
            // 
            this.applicationMenu1.ItemLinks.Add(this.fileNewItem1);
            this.applicationMenu1.ItemLinks.Add(this.fileOpenItem1);
            this.applicationMenu1.ItemLinks.Add(this.fileSaveItem1);
            this.applicationMenu1.ItemLinks.Add(this.fileSaveAsItem1);
            this.applicationMenu1.ItemLinks.Add(this.quickPrintItem1);
            this.applicationMenu1.ItemLinks.Add(this.printItem1);
            this.applicationMenu1.ItemLinks.Add(this.printPreviewItem1);
            this.applicationMenu1.Name = "applicationMenu1";
            this.applicationMenu1.Ribbon = this.ribbonControl1;
            // 
            // fileNewItem1
            // 
            this.fileNewItem1.Id = 434;
            this.fileNewItem1.Name = "fileNewItem1";
            // 
            // fileOpenItem1
            // 
            this.fileOpenItem1.Id = 435;
            this.fileOpenItem1.Name = "fileOpenItem1";
            this.fileOpenItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // fileSaveItem1
            // 
            this.fileSaveItem1.Id = 436;
            this.fileSaveItem1.Name = "fileSaveItem1";
            this.fileSaveItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // fileSaveAsItem1
            // 
            this.fileSaveAsItem1.Id = 437;
            this.fileSaveAsItem1.Name = "fileSaveAsItem1";
            this.fileSaveAsItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // quickPrintItem1
            // 
            this.quickPrintItem1.Id = 438;
            this.quickPrintItem1.Name = "quickPrintItem1";
            this.quickPrintItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // printItem1
            // 
            this.printItem1.Id = 439;
            this.printItem1.Name = "printItem1";
            this.printItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // printPreviewItem1
            // 
            this.printPreviewItem1.Id = 440;
            this.printPreviewItem1.Name = "printPreviewItem1";
            this.printPreviewItem1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.SmallWithText;
            // 
            // mergeToNewDocumentItem
            // 
            this.mergeToNewDocumentItem.Caption = "Merge to New Document";
            this.mergeToNewDocumentItem.Glyph = global::ERP.Module.Properties.Resources.mergeToNewDocumentItem_Glyph;
            this.mergeToNewDocumentItem.Id = 56;
            this.mergeToNewDocumentItem.LargeGlyph = global::ERP.Module.Properties.Resources.mergeToNewDocumentItem_LargeGlyph;
            this.mergeToNewDocumentItem.Name = "mergeToNewDocumentItem";
            toolTipTitleItem1.Text = "Merge";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Merge to New Document";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.mergeToNewDocumentItem.SuperTip = superToolTip1;
            this.mergeToNewDocumentItem.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mergeToNewDocumentItem_ItemClick);
            // 
            // barEditItem1
            // 
            this.barEditItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barEditItem1.Caption = "100%";
            this.barEditItem1.Edit = this.repositoryItemZoomTrackBar1;
            this.barEditItem1.EditValue = ((short)(100));
            this.barEditItem1.Id = 1;
            this.barEditItem1.Name = "barEditItem1";
            this.barEditItem1.Width = 200;
            // 
            // repositoryItemZoomTrackBar1
            // 
            this.repositoryItemZoomTrackBar1.LargeChange = 10;
            this.repositoryItemZoomTrackBar1.Maximum = 200;
            this.repositoryItemZoomTrackBar1.Middle = 5;
            this.repositoryItemZoomTrackBar1.Name = "repositoryItemZoomTrackBar1";
            this.repositoryItemZoomTrackBar1.ScrollThumbStyle = DevExpress.XtraEditors.Repository.ScrollThumbStyle.ArrowDownRight;
            this.repositoryItemZoomTrackBar1.SmallChange = 5;
            // 
            // barButtonGroup1
            // 
            this.barButtonGroup1.Id = 2;
            this.barButtonGroup1.Name = "barButtonGroup1";
            this.barButtonGroup1.Tag = "{97BBE334-159B-44d9-A168-0411957565E8}";
            // 
            // barButtonGroup2
            // 
            this.barButtonGroup2.Id = 3;
            this.barButtonGroup2.Name = "barButtonGroup2";
            this.barButtonGroup2.Tag = "{433DA7F0-03E2-4650-9DB5-66DD92D16E39}";
            // 
            // barButtonGroup3
            // 
            this.barButtonGroup3.Id = 4;
            this.barButtonGroup3.Name = "barButtonGroup3";
            this.barButtonGroup3.Tag = "{DF8C5334-EDE3-47c9-A42C-FE9A9247E180}";
            // 
            // barButtonGroup4
            // 
            this.barButtonGroup4.Id = 5;
            this.barButtonGroup4.Name = "barButtonGroup4";
            this.barButtonGroup4.Tag = "{0B3A7A43-3079-4ce0-83A8-3789F5F6DC9F}";
            // 
            // barButtonGroup5
            // 
            this.barButtonGroup5.Id = 6;
            this.barButtonGroup5.Name = "barButtonGroup5";
            this.barButtonGroup5.Tag = "{4747D5AB-2BEB-4ea6-9A1D-8E4FB36F1B40}";
            // 
            // barButtonGroup6
            // 
            this.barButtonGroup6.Id = 7;
            this.barButtonGroup6.Name = "barButtonGroup6";
            this.barButtonGroup6.Tag = "{8E89E775-996E-49a0-AADA-DE338E34732E}";
            // 
            // barButtonGroup7
            // 
            this.barButtonGroup7.Id = 8;
            this.barButtonGroup7.Name = "barButtonGroup7";
            this.barButtonGroup7.Tag = "{9A8DEAD8-3890-4857-A395-EC625FD02217}";
            // 
            // barButtonGroup8
            // 
            this.barButtonGroup8.Id = 189;
            this.barButtonGroup8.Name = "barButtonGroup8";
            this.barButtonGroup8.Tag = "{97BBE334-159B-44d9-A168-0411957565E8}";
            // 
            // barButtonGroup9
            // 
            this.barButtonGroup9.Id = 190;
            this.barButtonGroup9.Name = "barButtonGroup9";
            this.barButtonGroup9.Tag = "{433DA7F0-03E2-4650-9DB5-66DD92D16E39}";
            // 
            // barButtonGroup10
            // 
            this.barButtonGroup10.Id = 191;
            this.barButtonGroup10.Name = "barButtonGroup10";
            this.barButtonGroup10.Tag = "{DF8C5334-EDE3-47c9-A42C-FE9A9247E180}";
            // 
            // barButtonGroup11
            // 
            this.barButtonGroup11.Id = 192;
            this.barButtonGroup11.Name = "barButtonGroup11";
            this.barButtonGroup11.Tag = "{0B3A7A43-3079-4ce0-83A8-3789F5F6DC9F}";
            // 
            // barButtonGroup12
            // 
            this.barButtonGroup12.Id = 193;
            this.barButtonGroup12.Name = "barButtonGroup12";
            this.barButtonGroup12.Tag = "{4747D5AB-2BEB-4ea6-9A1D-8E4FB36F1B40}";
            // 
            // barButtonGroup13
            // 
            this.barButtonGroup13.Id = 194;
            this.barButtonGroup13.Name = "barButtonGroup13";
            this.barButtonGroup13.Tag = "{8E89E775-996E-49a0-AADA-DE338E34732E}";
            // 
            // barButtonGroup14
            // 
            this.barButtonGroup14.Id = 195;
            this.barButtonGroup14.Name = "barButtonGroup14";
            this.barButtonGroup14.Tag = "{9A8DEAD8-3890-4857-A395-EC625FD02217}";
            // 
            // undoItem1
            // 
            this.undoItem1.Id = 441;
            this.undoItem1.Name = "undoItem1";
            // 
            // redoItem1
            // 
            this.redoItem1.Id = 442;
            this.redoItem1.Name = "redoItem1";
            // 
            // pasteItem1
            // 
            this.pasteItem1.Id = 443;
            this.pasteItem1.Name = "pasteItem1";
            // 
            // cutItem1
            // 
            this.cutItem1.Id = 444;
            this.cutItem1.Name = "cutItem1";
            // 
            // copyItem1
            // 
            this.copyItem1.Id = 445;
            this.copyItem1.Name = "copyItem1";
            // 
            // pasteSpecialItem1
            // 
            this.pasteSpecialItem1.Id = 446;
            this.pasteSpecialItem1.Name = "pasteSpecialItem1";
            // 
            // barButtonGroup15
            // 
            this.barButtonGroup15.Id = 427;
            this.barButtonGroup15.ItemLinks.Add(this.changeFontNameItem1);
            this.barButtonGroup15.ItemLinks.Add(this.changeFontSizeItem1);
            this.barButtonGroup15.ItemLinks.Add(this.fontSizeIncreaseItem1);
            this.barButtonGroup15.ItemLinks.Add(this.fontSizeDecreaseItem1);
            this.barButtonGroup15.Name = "barButtonGroup15";
            this.barButtonGroup15.Tag = "{97BBE334-159B-44d9-A168-0411957565E8}";
            // 
            // changeFontNameItem1
            // 
            this.changeFontNameItem1.Edit = this.repositoryItemFontEdit3;
            this.changeFontNameItem1.Id = 447;
            this.changeFontNameItem1.Name = "changeFontNameItem1";
            // 
            // repositoryItemFontEdit3
            // 
            this.repositoryItemFontEdit3.AutoHeight = false;
            this.repositoryItemFontEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit3.Name = "repositoryItemFontEdit3";
            // 
            // changeFontSizeItem1
            // 
            this.changeFontSizeItem1.Edit = this.repositoryItemRichEditFontSizeEdit3;
            this.changeFontSizeItem1.Id = 448;
            this.changeFontSizeItem1.Name = "changeFontSizeItem1";
            // 
            // repositoryItemRichEditFontSizeEdit3
            // 
            this.repositoryItemRichEditFontSizeEdit3.AutoHeight = false;
            this.repositoryItemRichEditFontSizeEdit3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditFontSizeEdit3.Control = this.richTemplate;
            this.repositoryItemRichEditFontSizeEdit3.Name = "repositoryItemRichEditFontSizeEdit3";
            // 
            // fontSizeIncreaseItem1
            // 
            this.fontSizeIncreaseItem1.Id = 449;
            this.fontSizeIncreaseItem1.Name = "fontSizeIncreaseItem1";
            // 
            // fontSizeDecreaseItem1
            // 
            this.fontSizeDecreaseItem1.Id = 450;
            this.fontSizeDecreaseItem1.Name = "fontSizeDecreaseItem1";
            // 
            // barButtonGroup16
            // 
            this.barButtonGroup16.Id = 428;
            this.barButtonGroup16.ItemLinks.Add(this.toggleFontBoldItem1);
            this.barButtonGroup16.ItemLinks.Add(this.toggleFontItalicItem1);
            this.barButtonGroup16.ItemLinks.Add(this.toggleFontUnderlineItem1);
            this.barButtonGroup16.ItemLinks.Add(this.toggleFontDoubleUnderlineItem1);
            this.barButtonGroup16.ItemLinks.Add(this.toggleFontStrikeoutItem1);
            this.barButtonGroup16.ItemLinks.Add(this.toggleFontDoubleStrikeoutItem1);
            this.barButtonGroup16.ItemLinks.Add(this.toggleFontSuperscriptItem1);
            this.barButtonGroup16.ItemLinks.Add(this.toggleFontSubscriptItem1);
            this.barButtonGroup16.Name = "barButtonGroup16";
            this.barButtonGroup16.Tag = "{433DA7F0-03E2-4650-9DB5-66DD92D16E39}";
            // 
            // toggleFontBoldItem1
            // 
            this.toggleFontBoldItem1.Id = 451;
            this.toggleFontBoldItem1.Name = "toggleFontBoldItem1";
            // 
            // toggleFontItalicItem1
            // 
            this.toggleFontItalicItem1.Id = 452;
            this.toggleFontItalicItem1.Name = "toggleFontItalicItem1";
            // 
            // toggleFontUnderlineItem1
            // 
            this.toggleFontUnderlineItem1.Id = 453;
            this.toggleFontUnderlineItem1.Name = "toggleFontUnderlineItem1";
            // 
            // toggleFontDoubleUnderlineItem1
            // 
            this.toggleFontDoubleUnderlineItem1.Id = 454;
            this.toggleFontDoubleUnderlineItem1.Name = "toggleFontDoubleUnderlineItem1";
            // 
            // toggleFontStrikeoutItem1
            // 
            this.toggleFontStrikeoutItem1.Id = 455;
            this.toggleFontStrikeoutItem1.Name = "toggleFontStrikeoutItem1";
            // 
            // toggleFontDoubleStrikeoutItem1
            // 
            this.toggleFontDoubleStrikeoutItem1.Id = 456;
            this.toggleFontDoubleStrikeoutItem1.Name = "toggleFontDoubleStrikeoutItem1";
            // 
            // toggleFontSuperscriptItem1
            // 
            this.toggleFontSuperscriptItem1.Id = 457;
            this.toggleFontSuperscriptItem1.Name = "toggleFontSuperscriptItem1";
            // 
            // toggleFontSubscriptItem1
            // 
            this.toggleFontSubscriptItem1.Id = 458;
            this.toggleFontSubscriptItem1.Name = "toggleFontSubscriptItem1";
            // 
            // barButtonGroup17
            // 
            this.barButtonGroup17.Id = 429;
            this.barButtonGroup17.ItemLinks.Add(this.changeFontColorItem1);
            this.barButtonGroup17.ItemLinks.Add(this.changeFontBackColorItem1);
            this.barButtonGroup17.Name = "barButtonGroup17";
            this.barButtonGroup17.Tag = "{DF8C5334-EDE3-47c9-A42C-FE9A9247E180}";
            // 
            // changeFontColorItem1
            // 
            this.changeFontColorItem1.Id = 459;
            this.changeFontColorItem1.Name = "changeFontColorItem1";
            // 
            // changeFontBackColorItem1
            // 
            this.changeFontBackColorItem1.Id = 460;
            this.changeFontBackColorItem1.Name = "changeFontBackColorItem1";
            // 
            // changeTextCaseItem1
            // 
            this.changeTextCaseItem1.Id = 461;
            this.changeTextCaseItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.makeTextUpperCaseItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.makeTextLowerCaseItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTextCaseItem1)});
            this.changeTextCaseItem1.Name = "changeTextCaseItem1";
            // 
            // makeTextUpperCaseItem1
            // 
            this.makeTextUpperCaseItem1.Id = 462;
            this.makeTextUpperCaseItem1.Name = "makeTextUpperCaseItem1";
            // 
            // makeTextLowerCaseItem1
            // 
            this.makeTextLowerCaseItem1.Id = 463;
            this.makeTextLowerCaseItem1.Name = "makeTextLowerCaseItem1";
            // 
            // toggleTextCaseItem1
            // 
            this.toggleTextCaseItem1.Id = 464;
            this.toggleTextCaseItem1.Name = "toggleTextCaseItem1";
            // 
            // clearFormattingItem1
            // 
            this.clearFormattingItem1.Id = 465;
            this.clearFormattingItem1.Name = "clearFormattingItem1";
            // 
            // barButtonGroup18
            // 
            this.barButtonGroup18.Id = 430;
            this.barButtonGroup18.ItemLinks.Add(this.toggleBulletedListItem1);
            this.barButtonGroup18.ItemLinks.Add(this.toggleNumberingListItem1);
            this.barButtonGroup18.ItemLinks.Add(this.toggleMultiLevelListItem1);
            this.barButtonGroup18.Name = "barButtonGroup18";
            this.barButtonGroup18.Tag = "{0B3A7A43-3079-4ce0-83A8-3789F5F6DC9F}";
            // 
            // toggleBulletedListItem1
            // 
            this.toggleBulletedListItem1.Id = 466;
            this.toggleBulletedListItem1.Name = "toggleBulletedListItem1";
            // 
            // toggleNumberingListItem1
            // 
            this.toggleNumberingListItem1.Id = 467;
            this.toggleNumberingListItem1.Name = "toggleNumberingListItem1";
            // 
            // toggleMultiLevelListItem1
            // 
            this.toggleMultiLevelListItem1.Id = 468;
            this.toggleMultiLevelListItem1.Name = "toggleMultiLevelListItem1";
            // 
            // barButtonGroup19
            // 
            this.barButtonGroup19.Id = 431;
            this.barButtonGroup19.ItemLinks.Add(this.decreaseIndentItem1);
            this.barButtonGroup19.ItemLinks.Add(this.increaseIndentItem1);
            this.barButtonGroup19.ItemLinks.Add(this.toggleShowWhitespaceItem1);
            this.barButtonGroup19.Name = "barButtonGroup19";
            this.barButtonGroup19.Tag = "{4747D5AB-2BEB-4ea6-9A1D-8E4FB36F1B40}";
            // 
            // decreaseIndentItem1
            // 
            this.decreaseIndentItem1.Id = 469;
            this.decreaseIndentItem1.Name = "decreaseIndentItem1";
            // 
            // increaseIndentItem1
            // 
            this.increaseIndentItem1.Id = 470;
            this.increaseIndentItem1.Name = "increaseIndentItem1";
            // 
            // toggleShowWhitespaceItem1
            // 
            this.toggleShowWhitespaceItem1.Id = 471;
            this.toggleShowWhitespaceItem1.Name = "toggleShowWhitespaceItem1";
            // 
            // barButtonGroup20
            // 
            this.barButtonGroup20.Id = 432;
            this.barButtonGroup20.ItemLinks.Add(this.toggleParagraphAlignmentLeftItem1);
            this.barButtonGroup20.ItemLinks.Add(this.toggleParagraphAlignmentCenterItem1);
            this.barButtonGroup20.ItemLinks.Add(this.toggleParagraphAlignmentRightItem1);
            this.barButtonGroup20.ItemLinks.Add(this.toggleParagraphAlignmentJustifyItem1);
            this.barButtonGroup20.Name = "barButtonGroup20";
            this.barButtonGroup20.Tag = "{8E89E775-996E-49a0-AADA-DE338E34732E}";
            // 
            // toggleParagraphAlignmentLeftItem1
            // 
            this.toggleParagraphAlignmentLeftItem1.Id = 472;
            this.toggleParagraphAlignmentLeftItem1.Name = "toggleParagraphAlignmentLeftItem1";
            // 
            // toggleParagraphAlignmentCenterItem1
            // 
            this.toggleParagraphAlignmentCenterItem1.Id = 473;
            this.toggleParagraphAlignmentCenterItem1.Name = "toggleParagraphAlignmentCenterItem1";
            // 
            // toggleParagraphAlignmentRightItem1
            // 
            this.toggleParagraphAlignmentRightItem1.Id = 474;
            this.toggleParagraphAlignmentRightItem1.Name = "toggleParagraphAlignmentRightItem1";
            // 
            // toggleParagraphAlignmentJustifyItem1
            // 
            this.toggleParagraphAlignmentJustifyItem1.Id = 475;
            this.toggleParagraphAlignmentJustifyItem1.Name = "toggleParagraphAlignmentJustifyItem1";
            // 
            // barButtonGroup21
            // 
            this.barButtonGroup21.Id = 433;
            this.barButtonGroup21.ItemLinks.Add(this.changeParagraphLineSpacingItem1);
            this.barButtonGroup21.ItemLinks.Add(this.changeParagraphBackColorItem1);
            this.barButtonGroup21.Name = "barButtonGroup21";
            this.barButtonGroup21.Tag = "{9A8DEAD8-3890-4857-A395-EC625FD02217}";
            // 
            // changeParagraphLineSpacingItem1
            // 
            this.changeParagraphLineSpacingItem1.Id = 476;
            this.changeParagraphLineSpacingItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setSingleParagraphSpacingItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setSesquialteralParagraphSpacingItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setDoubleParagraphSpacingItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.showLineSpacingFormItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.addSpacingBeforeParagraphItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.removeSpacingBeforeParagraphItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.addSpacingAfterParagraphItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.removeSpacingAfterParagraphItem1)});
            this.changeParagraphLineSpacingItem1.Name = "changeParagraphLineSpacingItem1";
            // 
            // setSingleParagraphSpacingItem1
            // 
            this.setSingleParagraphSpacingItem1.Id = 477;
            this.setSingleParagraphSpacingItem1.Name = "setSingleParagraphSpacingItem1";
            // 
            // setSesquialteralParagraphSpacingItem1
            // 
            this.setSesquialteralParagraphSpacingItem1.Id = 478;
            this.setSesquialteralParagraphSpacingItem1.Name = "setSesquialteralParagraphSpacingItem1";
            // 
            // setDoubleParagraphSpacingItem1
            // 
            this.setDoubleParagraphSpacingItem1.Id = 479;
            this.setDoubleParagraphSpacingItem1.Name = "setDoubleParagraphSpacingItem1";
            // 
            // showLineSpacingFormItem1
            // 
            this.showLineSpacingFormItem1.Id = 480;
            this.showLineSpacingFormItem1.Name = "showLineSpacingFormItem1";
            // 
            // addSpacingBeforeParagraphItem1
            // 
            this.addSpacingBeforeParagraphItem1.Id = 481;
            this.addSpacingBeforeParagraphItem1.Name = "addSpacingBeforeParagraphItem1";
            // 
            // removeSpacingBeforeParagraphItem1
            // 
            this.removeSpacingBeforeParagraphItem1.Id = 482;
            this.removeSpacingBeforeParagraphItem1.Name = "removeSpacingBeforeParagraphItem1";
            // 
            // addSpacingAfterParagraphItem1
            // 
            this.addSpacingAfterParagraphItem1.Id = 483;
            this.addSpacingAfterParagraphItem1.Name = "addSpacingAfterParagraphItem1";
            // 
            // removeSpacingAfterParagraphItem1
            // 
            this.removeSpacingAfterParagraphItem1.Id = 484;
            this.removeSpacingAfterParagraphItem1.Name = "removeSpacingAfterParagraphItem1";
            // 
            // changeParagraphBackColorItem1
            // 
            this.changeParagraphBackColorItem1.Id = 485;
            this.changeParagraphBackColorItem1.Name = "changeParagraphBackColorItem1";
            // 
            // findItem1
            // 
            this.findItem1.Id = 487;
            this.findItem1.Name = "findItem1";
            // 
            // replaceItem1
            // 
            this.replaceItem1.Id = 488;
            this.replaceItem1.Name = "replaceItem1";
            // 
            // insertPageBreakItem1
            // 
            this.insertPageBreakItem1.Id = 489;
            this.insertPageBreakItem1.Name = "insertPageBreakItem1";
            // 
            // insertTableItem1
            // 
            this.insertTableItem1.Id = 490;
            this.insertTableItem1.Name = "insertTableItem1";
            // 
            // insertPictureItem1
            // 
            this.insertPictureItem1.Id = 491;
            this.insertPictureItem1.Name = "insertPictureItem1";
            // 
            // insertFloatingPictureItem1
            // 
            this.insertFloatingPictureItem1.Id = 492;
            this.insertFloatingPictureItem1.Name = "insertFloatingPictureItem1";
            // 
            // insertBookmarkItem1
            // 
            this.insertBookmarkItem1.Id = 493;
            this.insertBookmarkItem1.Name = "insertBookmarkItem1";
            // 
            // insertHyperlinkItem1
            // 
            this.insertHyperlinkItem1.Id = 494;
            this.insertHyperlinkItem1.Name = "insertHyperlinkItem1";
            // 
            // editPageHeaderItem1
            // 
            this.editPageHeaderItem1.Id = 495;
            this.editPageHeaderItem1.Name = "editPageHeaderItem1";
            // 
            // editPageFooterItem1
            // 
            this.editPageFooterItem1.Id = 496;
            this.editPageFooterItem1.Name = "editPageFooterItem1";
            // 
            // insertPageNumberItem1
            // 
            this.insertPageNumberItem1.Id = 497;
            this.insertPageNumberItem1.Name = "insertPageNumberItem1";
            // 
            // insertPageCountItem1
            // 
            this.insertPageCountItem1.Id = 498;
            this.insertPageCountItem1.Name = "insertPageCountItem1";
            // 
            // insertTextBoxItem1
            // 
            this.insertTextBoxItem1.Id = 499;
            this.insertTextBoxItem1.Name = "insertTextBoxItem1";
            // 
            // insertSymbolItem1
            // 
            this.insertSymbolItem1.Id = 500;
            this.insertSymbolItem1.Name = "insertSymbolItem1";
            // 
            // changeSectionPageMarginsItem1
            // 
            this.changeSectionPageMarginsItem1.Id = 501;
            this.changeSectionPageMarginsItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setNormalSectionPageMarginsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setNarrowSectionPageMarginsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setModerateSectionPageMarginsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setWideSectionPageMarginsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.showPageMarginsSetupFormItem1)});
            this.changeSectionPageMarginsItem1.Name = "changeSectionPageMarginsItem1";
            // 
            // setNormalSectionPageMarginsItem1
            // 
            this.setNormalSectionPageMarginsItem1.Id = 502;
            this.setNormalSectionPageMarginsItem1.Name = "setNormalSectionPageMarginsItem1";
            // 
            // setNarrowSectionPageMarginsItem1
            // 
            this.setNarrowSectionPageMarginsItem1.Id = 503;
            this.setNarrowSectionPageMarginsItem1.Name = "setNarrowSectionPageMarginsItem1";
            // 
            // setModerateSectionPageMarginsItem1
            // 
            this.setModerateSectionPageMarginsItem1.Id = 504;
            this.setModerateSectionPageMarginsItem1.Name = "setModerateSectionPageMarginsItem1";
            // 
            // setWideSectionPageMarginsItem1
            // 
            this.setWideSectionPageMarginsItem1.Id = 505;
            this.setWideSectionPageMarginsItem1.Name = "setWideSectionPageMarginsItem1";
            // 
            // showPageMarginsSetupFormItem1
            // 
            this.showPageMarginsSetupFormItem1.Id = 506;
            this.showPageMarginsSetupFormItem1.Name = "showPageMarginsSetupFormItem1";
            // 
            // changeSectionPageOrientationItem1
            // 
            this.changeSectionPageOrientationItem1.Id = 507;
            this.changeSectionPageOrientationItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setPortraitPageOrientationItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setLandscapePageOrientationItem1)});
            this.changeSectionPageOrientationItem1.Name = "changeSectionPageOrientationItem1";
            // 
            // setPortraitPageOrientationItem1
            // 
            this.setPortraitPageOrientationItem1.Id = 508;
            this.setPortraitPageOrientationItem1.Name = "setPortraitPageOrientationItem1";
            // 
            // setLandscapePageOrientationItem1
            // 
            this.setLandscapePageOrientationItem1.Id = 509;
            this.setLandscapePageOrientationItem1.Name = "setLandscapePageOrientationItem1";
            // 
            // changeSectionPaperKindItem1
            // 
            this.changeSectionPaperKindItem1.Id = 510;
            this.changeSectionPaperKindItem1.Name = "changeSectionPaperKindItem1";
            // 
            // changeSectionColumnsItem1
            // 
            this.changeSectionColumnsItem1.Id = 511;
            this.changeSectionColumnsItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setSectionOneColumnItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setSectionTwoColumnsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setSectionThreeColumnsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.showColumnsSetupFormItem1)});
            this.changeSectionColumnsItem1.Name = "changeSectionColumnsItem1";
            // 
            // setSectionOneColumnItem1
            // 
            this.setSectionOneColumnItem1.Id = 512;
            this.setSectionOneColumnItem1.Name = "setSectionOneColumnItem1";
            // 
            // setSectionTwoColumnsItem1
            // 
            this.setSectionTwoColumnsItem1.Id = 513;
            this.setSectionTwoColumnsItem1.Name = "setSectionTwoColumnsItem1";
            // 
            // setSectionThreeColumnsItem1
            // 
            this.setSectionThreeColumnsItem1.Id = 514;
            this.setSectionThreeColumnsItem1.Name = "setSectionThreeColumnsItem1";
            // 
            // showColumnsSetupFormItem1
            // 
            this.showColumnsSetupFormItem1.Id = 515;
            this.showColumnsSetupFormItem1.Name = "showColumnsSetupFormItem1";
            // 
            // insertBreakItem1
            // 
            this.insertBreakItem1.Id = 516;
            this.insertBreakItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.insertPageBreakItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.insertColumnBreakItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.insertSectionBreakNextPageItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.insertSectionBreakEvenPageItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.insertSectionBreakOddPageItem1)});
            this.insertBreakItem1.Name = "insertBreakItem1";
            // 
            // insertColumnBreakItem1
            // 
            this.insertColumnBreakItem1.Id = 517;
            this.insertColumnBreakItem1.Name = "insertColumnBreakItem1";
            // 
            // insertSectionBreakNextPageItem1
            // 
            this.insertSectionBreakNextPageItem1.Id = 518;
            this.insertSectionBreakNextPageItem1.Name = "insertSectionBreakNextPageItem1";
            // 
            // insertSectionBreakEvenPageItem1
            // 
            this.insertSectionBreakEvenPageItem1.Id = 519;
            this.insertSectionBreakEvenPageItem1.Name = "insertSectionBreakEvenPageItem1";
            // 
            // insertSectionBreakOddPageItem1
            // 
            this.insertSectionBreakOddPageItem1.Id = 520;
            this.insertSectionBreakOddPageItem1.Name = "insertSectionBreakOddPageItem1";
            // 
            // changeSectionLineNumberingItem1
            // 
            this.changeSectionLineNumberingItem1.Id = 521;
            this.changeSectionLineNumberingItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setSectionLineNumberingNoneItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setSectionLineNumberingContinuousItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setSectionLineNumberingRestartNewPageItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setSectionLineNumberingRestartNewSectionItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleParagraphSuppressLineNumbersItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.showLineNumberingFormItem1)});
            this.changeSectionLineNumberingItem1.Name = "changeSectionLineNumberingItem1";
            // 
            // setSectionLineNumberingNoneItem1
            // 
            this.setSectionLineNumberingNoneItem1.Id = 522;
            this.setSectionLineNumberingNoneItem1.Name = "setSectionLineNumberingNoneItem1";
            // 
            // setSectionLineNumberingContinuousItem1
            // 
            this.setSectionLineNumberingContinuousItem1.Id = 523;
            this.setSectionLineNumberingContinuousItem1.Name = "setSectionLineNumberingContinuousItem1";
            // 
            // setSectionLineNumberingRestartNewPageItem1
            // 
            this.setSectionLineNumberingRestartNewPageItem1.Id = 524;
            this.setSectionLineNumberingRestartNewPageItem1.Name = "setSectionLineNumberingRestartNewPageItem1";
            // 
            // setSectionLineNumberingRestartNewSectionItem1
            // 
            this.setSectionLineNumberingRestartNewSectionItem1.Id = 525;
            this.setSectionLineNumberingRestartNewSectionItem1.Name = "setSectionLineNumberingRestartNewSectionItem1";
            // 
            // toggleParagraphSuppressLineNumbersItem1
            // 
            this.toggleParagraphSuppressLineNumbersItem1.Id = 526;
            this.toggleParagraphSuppressLineNumbersItem1.Name = "toggleParagraphSuppressLineNumbersItem1";
            // 
            // showLineNumberingFormItem1
            // 
            this.showLineNumberingFormItem1.Id = 527;
            this.showLineNumberingFormItem1.Name = "showLineNumberingFormItem1";
            // 
            // changePageColorItem1
            // 
            this.changePageColorItem1.Id = 528;
            this.changePageColorItem1.Name = "changePageColorItem1";
            // 
            // insertTableOfContentsItem1
            // 
            this.insertTableOfContentsItem1.Id = 529;
            this.insertTableOfContentsItem1.Name = "insertTableOfContentsItem1";
            // 
            // updateTableOfContentsItem1
            // 
            this.updateTableOfContentsItem1.Id = 530;
            this.updateTableOfContentsItem1.Name = "updateTableOfContentsItem1";
            // 
            // addParagraphsToTableOfContentItem1
            // 
            this.addParagraphsToTableOfContentItem1.Id = 531;
            this.addParagraphsToTableOfContentItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem2),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem3),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem4),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem5),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem6),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem7),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem8),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem9),
            new DevExpress.XtraBars.LinkPersistInfo(this.setParagraphHeadingLevelItem10)});
            this.addParagraphsToTableOfContentItem1.Name = "addParagraphsToTableOfContentItem1";
            // 
            // setParagraphHeadingLevelItem1
            // 
            this.setParagraphHeadingLevelItem1.Id = 532;
            this.setParagraphHeadingLevelItem1.Name = "setParagraphHeadingLevelItem1";
            this.setParagraphHeadingLevelItem1.OutlineLevel = 0;
            // 
            // setParagraphHeadingLevelItem2
            // 
            this.setParagraphHeadingLevelItem2.Id = 533;
            this.setParagraphHeadingLevelItem2.Name = "setParagraphHeadingLevelItem2";
            this.setParagraphHeadingLevelItem2.OutlineLevel = 1;
            // 
            // setParagraphHeadingLevelItem3
            // 
            this.setParagraphHeadingLevelItem3.Id = 534;
            this.setParagraphHeadingLevelItem3.Name = "setParagraphHeadingLevelItem3";
            this.setParagraphHeadingLevelItem3.OutlineLevel = 2;
            // 
            // setParagraphHeadingLevelItem4
            // 
            this.setParagraphHeadingLevelItem4.Id = 535;
            this.setParagraphHeadingLevelItem4.Name = "setParagraphHeadingLevelItem4";
            this.setParagraphHeadingLevelItem4.OutlineLevel = 3;
            // 
            // setParagraphHeadingLevelItem5
            // 
            this.setParagraphHeadingLevelItem5.Id = 536;
            this.setParagraphHeadingLevelItem5.Name = "setParagraphHeadingLevelItem5";
            this.setParagraphHeadingLevelItem5.OutlineLevel = 4;
            // 
            // setParagraphHeadingLevelItem6
            // 
            this.setParagraphHeadingLevelItem6.Id = 537;
            this.setParagraphHeadingLevelItem6.Name = "setParagraphHeadingLevelItem6";
            this.setParagraphHeadingLevelItem6.OutlineLevel = 5;
            // 
            // setParagraphHeadingLevelItem7
            // 
            this.setParagraphHeadingLevelItem7.Id = 538;
            this.setParagraphHeadingLevelItem7.Name = "setParagraphHeadingLevelItem7";
            this.setParagraphHeadingLevelItem7.OutlineLevel = 6;
            // 
            // setParagraphHeadingLevelItem8
            // 
            this.setParagraphHeadingLevelItem8.Id = 539;
            this.setParagraphHeadingLevelItem8.Name = "setParagraphHeadingLevelItem8";
            this.setParagraphHeadingLevelItem8.OutlineLevel = 7;
            // 
            // setParagraphHeadingLevelItem9
            // 
            this.setParagraphHeadingLevelItem9.Id = 540;
            this.setParagraphHeadingLevelItem9.Name = "setParagraphHeadingLevelItem9";
            this.setParagraphHeadingLevelItem9.OutlineLevel = 8;
            // 
            // setParagraphHeadingLevelItem10
            // 
            this.setParagraphHeadingLevelItem10.Id = 541;
            this.setParagraphHeadingLevelItem10.Name = "setParagraphHeadingLevelItem10";
            this.setParagraphHeadingLevelItem10.OutlineLevel = 9;
            // 
            // insertCaptionPlaceholderItem1
            // 
            this.insertCaptionPlaceholderItem1.Id = 542;
            this.insertCaptionPlaceholderItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.insertFiguresCaptionItems1),
            new DevExpress.XtraBars.LinkPersistInfo(this.insertTablesCaptionItems1),
            new DevExpress.XtraBars.LinkPersistInfo(this.insertEquationsCaptionItems1)});
            this.insertCaptionPlaceholderItem1.Name = "insertCaptionPlaceholderItem1";
            // 
            // insertFiguresCaptionItems1
            // 
            this.insertFiguresCaptionItems1.Id = 543;
            this.insertFiguresCaptionItems1.Name = "insertFiguresCaptionItems1";
            // 
            // insertTablesCaptionItems1
            // 
            this.insertTablesCaptionItems1.Id = 544;
            this.insertTablesCaptionItems1.Name = "insertTablesCaptionItems1";
            // 
            // insertEquationsCaptionItems1
            // 
            this.insertEquationsCaptionItems1.Id = 545;
            this.insertEquationsCaptionItems1.Name = "insertEquationsCaptionItems1";
            // 
            // insertTableOfFiguresPlaceholderItem1
            // 
            this.insertTableOfFiguresPlaceholderItem1.Id = 546;
            this.insertTableOfFiguresPlaceholderItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.insertTableOfFiguresItems1),
            new DevExpress.XtraBars.LinkPersistInfo(this.insertTableOfTablesItems1),
            new DevExpress.XtraBars.LinkPersistInfo(this.insertTableOfEquationsItems1)});
            this.insertTableOfFiguresPlaceholderItem1.Name = "insertTableOfFiguresPlaceholderItem1";
            // 
            // insertTableOfFiguresItems1
            // 
            this.insertTableOfFiguresItems1.Id = 547;
            this.insertTableOfFiguresItems1.Name = "insertTableOfFiguresItems1";
            // 
            // insertTableOfTablesItems1
            // 
            this.insertTableOfTablesItems1.Id = 548;
            this.insertTableOfTablesItems1.Name = "insertTableOfTablesItems1";
            // 
            // insertTableOfEquationsItems1
            // 
            this.insertTableOfEquationsItems1.Id = 549;
            this.insertTableOfEquationsItems1.Name = "insertTableOfEquationsItems1";
            // 
            // insertMergeFieldItem1
            // 
            this.insertMergeFieldItem1.Id = 550;
            this.insertMergeFieldItem1.Name = "insertMergeFieldItem1";
            // 
            // showAllFieldCodesItem1
            // 
            this.showAllFieldCodesItem1.Id = 551;
            this.showAllFieldCodesItem1.Name = "showAllFieldCodesItem1";
            // 
            // showAllFieldResultsItem1
            // 
            this.showAllFieldResultsItem1.Id = 552;
            this.showAllFieldResultsItem1.Name = "showAllFieldResultsItem1";
            // 
            // toggleViewMergedDataItem1
            // 
            this.toggleViewMergedDataItem1.Id = 553;
            this.toggleViewMergedDataItem1.Name = "toggleViewMergedDataItem1";
            // 
            // checkSpellingItem1
            // 
            this.checkSpellingItem1.Id = 554;
            this.checkSpellingItem1.Name = "checkSpellingItem1";
            // 
            // protectDocumentItem1
            // 
            this.protectDocumentItem1.Id = 555;
            this.protectDocumentItem1.Name = "protectDocumentItem1";
            // 
            // changeRangeEditingPermissionsItem1
            // 
            this.changeRangeEditingPermissionsItem1.Id = 556;
            this.changeRangeEditingPermissionsItem1.Name = "changeRangeEditingPermissionsItem1";
            // 
            // unprotectDocumentItem1
            // 
            this.unprotectDocumentItem1.Id = 557;
            this.unprotectDocumentItem1.Name = "unprotectDocumentItem1";
            // 
            // switchToSimpleViewItem1
            // 
            this.switchToSimpleViewItem1.Id = 558;
            this.switchToSimpleViewItem1.Name = "switchToSimpleViewItem1";
            // 
            // switchToDraftViewItem1
            // 
            this.switchToDraftViewItem1.Id = 559;
            this.switchToDraftViewItem1.Name = "switchToDraftViewItem1";
            // 
            // switchToPrintLayoutViewItem1
            // 
            this.switchToPrintLayoutViewItem1.Id = 560;
            this.switchToPrintLayoutViewItem1.Name = "switchToPrintLayoutViewItem1";
            // 
            // toggleShowHorizontalRulerItem1
            // 
            this.toggleShowHorizontalRulerItem1.Id = 561;
            this.toggleShowHorizontalRulerItem1.Name = "toggleShowHorizontalRulerItem1";
            // 
            // toggleShowVerticalRulerItem1
            // 
            this.toggleShowVerticalRulerItem1.Id = 562;
            this.toggleShowVerticalRulerItem1.Name = "toggleShowVerticalRulerItem1";
            // 
            // zoomOutItem1
            // 
            this.zoomOutItem1.Id = 563;
            this.zoomOutItem1.Name = "zoomOutItem1";
            // 
            // zoomInItem1
            // 
            this.zoomInItem1.Id = 564;
            this.zoomInItem1.Name = "zoomInItem1";
            // 
            // goToPageHeaderItem1
            // 
            this.goToPageHeaderItem1.Id = 565;
            this.goToPageHeaderItem1.Name = "goToPageHeaderItem1";
            // 
            // goToPageFooterItem1
            // 
            this.goToPageFooterItem1.Id = 566;
            this.goToPageFooterItem1.Name = "goToPageFooterItem1";
            // 
            // goToNextHeaderFooterItem1
            // 
            this.goToNextHeaderFooterItem1.Id = 567;
            this.goToNextHeaderFooterItem1.Name = "goToNextHeaderFooterItem1";
            // 
            // goToPreviousHeaderFooterItem1
            // 
            this.goToPreviousHeaderFooterItem1.Id = 568;
            this.goToPreviousHeaderFooterItem1.Name = "goToPreviousHeaderFooterItem1";
            // 
            // toggleLinkToPreviousItem1
            // 
            this.toggleLinkToPreviousItem1.Id = 569;
            this.toggleLinkToPreviousItem1.Name = "toggleLinkToPreviousItem1";
            // 
            // toggleDifferentFirstPageItem1
            // 
            this.toggleDifferentFirstPageItem1.Id = 570;
            this.toggleDifferentFirstPageItem1.Name = "toggleDifferentFirstPageItem1";
            // 
            // toggleDifferentOddAndEvenPagesItem1
            // 
            this.toggleDifferentOddAndEvenPagesItem1.Id = 571;
            this.toggleDifferentOddAndEvenPagesItem1.Name = "toggleDifferentOddAndEvenPagesItem1";
            // 
            // closePageHeaderFooterItem1
            // 
            this.closePageHeaderFooterItem1.Id = 572;
            this.closePageHeaderFooterItem1.Name = "closePageHeaderFooterItem1";
            // 
            // toggleFirstRowItem1
            // 
            this.toggleFirstRowItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.toggleFirstRowItem1.Id = 573;
            this.toggleFirstRowItem1.Name = "toggleFirstRowItem1";
            // 
            // toggleLastRowItem1
            // 
            this.toggleLastRowItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.toggleLastRowItem1.Id = 574;
            this.toggleLastRowItem1.Name = "toggleLastRowItem1";
            // 
            // toggleBandedRowsItem1
            // 
            this.toggleBandedRowsItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.toggleBandedRowsItem1.Id = 575;
            this.toggleBandedRowsItem1.Name = "toggleBandedRowsItem1";
            // 
            // toggleFirstColumnItem1
            // 
            this.toggleFirstColumnItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.toggleFirstColumnItem1.Id = 576;
            this.toggleFirstColumnItem1.Name = "toggleFirstColumnItem1";
            // 
            // toggleLastColumnItem1
            // 
            this.toggleLastColumnItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.toggleLastColumnItem1.Id = 577;
            this.toggleLastColumnItem1.Name = "toggleLastColumnItem1";
            // 
            // toggleBandedColumnItem1
            // 
            this.toggleBandedColumnItem1.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.toggleBandedColumnItem1.Id = 578;
            this.toggleBandedColumnItem1.Name = "toggleBandedColumnItem1";
            // 
            // galleryChangeTableStyleItem1
            // 
            this.galleryChangeTableStyleItem1.CurrentItem = null;
            this.galleryChangeTableStyleItem1.CurrentItemStyle = null;
            this.galleryChangeTableStyleItem1.CurrentStyle = null;
            this.galleryChangeTableStyleItem1.DeleteItemLink = null;
            // 
            // 
            // 
            this.galleryChangeTableStyleItem1.Gallery.ColumnCount = 3;
            this.galleryChangeTableStyleItem1.Gallery.Groups.AddRange(new DevExpress.XtraBars.Ribbon.GalleryItemGroup[] {
            galleryItemGroup2});
            this.galleryChangeTableStyleItem1.Gallery.ImageSize = new Size(65, 46);
            this.galleryChangeTableStyleItem1.Id = 579;
            this.galleryChangeTableStyleItem1.ModifyItemLink = null;
            this.galleryChangeTableStyleItem1.Name = "galleryChangeTableStyleItem1";
            this.galleryChangeTableStyleItem1.NewItemLink = null;
            this.galleryChangeTableStyleItem1.PopupGallery = null;
            // 
            // changeTableBorderLineStyleItem1
            // 
            this.changeTableBorderLineStyleItem1.Edit = this.repositoryItemBorderLineStyle4;
            borderInfo1.Color = Color.Black;
            borderInfo1.Frame = false;
            borderInfo1.Offset = 0;
            borderInfo1.Shadow = false;
            borderInfo1.Style = DevExpress.XtraRichEdit.Model.BorderLineStyle.Single;
            borderInfo1.Width = 10;
            this.changeTableBorderLineStyleItem1.EditValue = borderInfo1;
            this.changeTableBorderLineStyleItem1.Id = 580;
            this.changeTableBorderLineStyleItem1.Name = "changeTableBorderLineStyleItem1";
            // 
            // repositoryItemBorderLineStyle4
            // 
            this.repositoryItemBorderLineStyle4.AutoHeight = false;
            this.repositoryItemBorderLineStyle4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBorderLineStyle4.Control = this.richTemplate;
            this.repositoryItemBorderLineStyle4.Name = "repositoryItemBorderLineStyle4";
            // 
            // changeTableBorderLineWeightItem1
            // 
            this.changeTableBorderLineWeightItem1.Edit = this.repositoryItemBorderLineWeight4;
            this.changeTableBorderLineWeightItem1.EditValue = 20;
            this.changeTableBorderLineWeightItem1.Id = 581;
            this.changeTableBorderLineWeightItem1.Name = "changeTableBorderLineWeightItem1";
            // 
            // repositoryItemBorderLineWeight4
            // 
            this.repositoryItemBorderLineWeight4.AutoHeight = false;
            this.repositoryItemBorderLineWeight4.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBorderLineWeight4.Control = this.richTemplate;
            this.repositoryItemBorderLineWeight4.Name = "repositoryItemBorderLineWeight4";
            // 
            // changeTableBorderColorItem1
            // 
            this.changeTableBorderColorItem1.Id = 582;
            this.changeTableBorderColorItem1.Name = "changeTableBorderColorItem1";
            // 
            // changeTableBordersItem1
            // 
            this.changeTableBordersItem1.Id = 583;
            this.changeTableBordersItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsBottomBorderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsTopBorderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsLeftBorderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsRightBorderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.resetTableCellsAllBordersItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsAllBordersItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsOutsideBorderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsInsideBorderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsInsideHorizontalBorderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableCellsInsideVerticalBorderItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleShowTableGridLinesItem1)});
            this.changeTableBordersItem1.Name = "changeTableBordersItem1";
            // 
            // toggleTableCellsBottomBorderItem1
            // 
            this.toggleTableCellsBottomBorderItem1.Id = 584;
            this.toggleTableCellsBottomBorderItem1.Name = "toggleTableCellsBottomBorderItem1";
            // 
            // toggleTableCellsTopBorderItem1
            // 
            this.toggleTableCellsTopBorderItem1.Id = 585;
            this.toggleTableCellsTopBorderItem1.Name = "toggleTableCellsTopBorderItem1";
            // 
            // toggleTableCellsLeftBorderItem1
            // 
            this.toggleTableCellsLeftBorderItem1.Id = 586;
            this.toggleTableCellsLeftBorderItem1.Name = "toggleTableCellsLeftBorderItem1";
            // 
            // toggleTableCellsRightBorderItem1
            // 
            this.toggleTableCellsRightBorderItem1.Id = 587;
            this.toggleTableCellsRightBorderItem1.Name = "toggleTableCellsRightBorderItem1";
            // 
            // resetTableCellsAllBordersItem1
            // 
            this.resetTableCellsAllBordersItem1.Id = 588;
            this.resetTableCellsAllBordersItem1.Name = "resetTableCellsAllBordersItem1";
            // 
            // toggleTableCellsAllBordersItem1
            // 
            this.toggleTableCellsAllBordersItem1.Id = 589;
            this.toggleTableCellsAllBordersItem1.Name = "toggleTableCellsAllBordersItem1";
            // 
            // toggleTableCellsOutsideBorderItem1
            // 
            this.toggleTableCellsOutsideBorderItem1.Id = 590;
            this.toggleTableCellsOutsideBorderItem1.Name = "toggleTableCellsOutsideBorderItem1";
            // 
            // toggleTableCellsInsideBorderItem1
            // 
            this.toggleTableCellsInsideBorderItem1.Id = 591;
            this.toggleTableCellsInsideBorderItem1.Name = "toggleTableCellsInsideBorderItem1";
            // 
            // toggleTableCellsInsideHorizontalBorderItem1
            // 
            this.toggleTableCellsInsideHorizontalBorderItem1.Id = 592;
            this.toggleTableCellsInsideHorizontalBorderItem1.Name = "toggleTableCellsInsideHorizontalBorderItem1";
            // 
            // toggleTableCellsInsideVerticalBorderItem1
            // 
            this.toggleTableCellsInsideVerticalBorderItem1.Id = 593;
            this.toggleTableCellsInsideVerticalBorderItem1.Name = "toggleTableCellsInsideVerticalBorderItem1";
            // 
            // toggleShowTableGridLinesItem1
            // 
            this.toggleShowTableGridLinesItem1.Id = 594;
            this.toggleShowTableGridLinesItem1.Name = "toggleShowTableGridLinesItem1";
            // 
            // changeTableCellsShadingItem1
            // 
            this.changeTableCellsShadingItem1.Id = 595;
            this.changeTableCellsShadingItem1.Name = "changeTableCellsShadingItem1";
            // 
            // selectTableElementsItem1
            // 
            this.selectTableElementsItem1.Id = 596;
            this.selectTableElementsItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.selectTableCellItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.selectTableColumnItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.selectTableRowItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.selectTableItem1)});
            this.selectTableElementsItem1.Name = "selectTableElementsItem1";
            // 
            // selectTableCellItem1
            // 
            this.selectTableCellItem1.Id = 597;
            this.selectTableCellItem1.Name = "selectTableCellItem1";
            // 
            // selectTableColumnItem1
            // 
            this.selectTableColumnItem1.Id = 598;
            this.selectTableColumnItem1.Name = "selectTableColumnItem1";
            // 
            // selectTableRowItem1
            // 
            this.selectTableRowItem1.Id = 599;
            this.selectTableRowItem1.Name = "selectTableRowItem1";
            // 
            // selectTableItem1
            // 
            this.selectTableItem1.Id = 600;
            this.selectTableItem1.Name = "selectTableItem1";
            // 
            // showTablePropertiesFormItem1
            // 
            this.showTablePropertiesFormItem1.Id = 601;
            this.showTablePropertiesFormItem1.Name = "showTablePropertiesFormItem1";
            // 
            // deleteTableElementsItem1
            // 
            this.deleteTableElementsItem1.Id = 602;
            this.deleteTableElementsItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.showDeleteTableCellsFormItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.deleteTableColumnsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.deleteTableRowsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.deleteTableItem1)});
            this.deleteTableElementsItem1.Name = "deleteTableElementsItem1";
            // 
            // showDeleteTableCellsFormItem1
            // 
            this.showDeleteTableCellsFormItem1.Id = 603;
            this.showDeleteTableCellsFormItem1.Name = "showDeleteTableCellsFormItem1";
            // 
            // deleteTableColumnsItem1
            // 
            this.deleteTableColumnsItem1.Id = 604;
            this.deleteTableColumnsItem1.Name = "deleteTableColumnsItem1";
            // 
            // deleteTableRowsItem1
            // 
            this.deleteTableRowsItem1.Id = 605;
            this.deleteTableRowsItem1.Name = "deleteTableRowsItem1";
            // 
            // deleteTableItem1
            // 
            this.deleteTableItem1.Id = 606;
            this.deleteTableItem1.Name = "deleteTableItem1";
            // 
            // insertTableRowAboveItem1
            // 
            this.insertTableRowAboveItem1.Id = 607;
            this.insertTableRowAboveItem1.Name = "insertTableRowAboveItem1";
            // 
            // insertTableRowBelowItem1
            // 
            this.insertTableRowBelowItem1.Id = 608;
            this.insertTableRowBelowItem1.Name = "insertTableRowBelowItem1";
            // 
            // insertTableColumnToLeftItem1
            // 
            this.insertTableColumnToLeftItem1.Id = 609;
            this.insertTableColumnToLeftItem1.Name = "insertTableColumnToLeftItem1";
            // 
            // insertTableColumnToRightItem1
            // 
            this.insertTableColumnToRightItem1.Id = 610;
            this.insertTableColumnToRightItem1.Name = "insertTableColumnToRightItem1";
            // 
            // mergeTableCellsItem1
            // 
            this.mergeTableCellsItem1.Id = 611;
            this.mergeTableCellsItem1.Name = "mergeTableCellsItem1";
            // 
            // showSplitTableCellsForm1
            // 
            this.showSplitTableCellsForm1.Id = 612;
            this.showSplitTableCellsForm1.Name = "showSplitTableCellsForm1";
            // 
            // splitTableItem1
            // 
            this.splitTableItem1.Id = 613;
            this.splitTableItem1.Name = "splitTableItem1";
            // 
            // toggleTableAutoFitItem1
            // 
            this.toggleTableAutoFitItem1.Id = 614;
            this.toggleTableAutoFitItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableAutoFitContentsItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableAutoFitWindowItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.toggleTableFixedColumnWidthItem1)});
            this.toggleTableAutoFitItem1.Name = "toggleTableAutoFitItem1";
            // 
            // toggleTableAutoFitContentsItem1
            // 
            this.toggleTableAutoFitContentsItem1.Id = 615;
            this.toggleTableAutoFitContentsItem1.Name = "toggleTableAutoFitContentsItem1";
            // 
            // toggleTableAutoFitWindowItem1
            // 
            this.toggleTableAutoFitWindowItem1.Id = 616;
            this.toggleTableAutoFitWindowItem1.Name = "toggleTableAutoFitWindowItem1";
            // 
            // toggleTableFixedColumnWidthItem1
            // 
            this.toggleTableFixedColumnWidthItem1.Id = 617;
            this.toggleTableFixedColumnWidthItem1.Name = "toggleTableFixedColumnWidthItem1";
            // 
            // toggleTableCellsTopLeftAlignmentItem1
            // 
            this.toggleTableCellsTopLeftAlignmentItem1.Id = 618;
            this.toggleTableCellsTopLeftAlignmentItem1.Name = "toggleTableCellsTopLeftAlignmentItem1";
            // 
            // toggleTableCellsMiddleLeftAlignmentItem1
            // 
            this.toggleTableCellsMiddleLeftAlignmentItem1.Id = 619;
            this.toggleTableCellsMiddleLeftAlignmentItem1.Name = "toggleTableCellsMiddleLeftAlignmentItem1";
            // 
            // toggleTableCellsBottomLeftAlignmentItem1
            // 
            this.toggleTableCellsBottomLeftAlignmentItem1.Id = 620;
            this.toggleTableCellsBottomLeftAlignmentItem1.Name = "toggleTableCellsBottomLeftAlignmentItem1";
            // 
            // toggleTableCellsTopCenterAlignmentItem1
            // 
            this.toggleTableCellsTopCenterAlignmentItem1.Id = 621;
            this.toggleTableCellsTopCenterAlignmentItem1.Name = "toggleTableCellsTopCenterAlignmentItem1";
            // 
            // toggleTableCellsMiddleCenterAlignmentItem1
            // 
            this.toggleTableCellsMiddleCenterAlignmentItem1.Id = 622;
            this.toggleTableCellsMiddleCenterAlignmentItem1.Name = "toggleTableCellsMiddleCenterAlignmentItem1";
            // 
            // toggleTableCellsBottomCenterAlignmentItem1
            // 
            this.toggleTableCellsBottomCenterAlignmentItem1.Id = 623;
            this.toggleTableCellsBottomCenterAlignmentItem1.Name = "toggleTableCellsBottomCenterAlignmentItem1";
            // 
            // toggleTableCellsTopRightAlignmentItem1
            // 
            this.toggleTableCellsTopRightAlignmentItem1.Id = 624;
            this.toggleTableCellsTopRightAlignmentItem1.Name = "toggleTableCellsTopRightAlignmentItem1";
            // 
            // toggleTableCellsMiddleRightAlignmentItem1
            // 
            this.toggleTableCellsMiddleRightAlignmentItem1.Id = 625;
            this.toggleTableCellsMiddleRightAlignmentItem1.Name = "toggleTableCellsMiddleRightAlignmentItem1";
            // 
            // toggleTableCellsBottomRightAlignmentItem1
            // 
            this.toggleTableCellsBottomRightAlignmentItem1.Id = 626;
            this.toggleTableCellsBottomRightAlignmentItem1.Name = "toggleTableCellsBottomRightAlignmentItem1";
            // 
            // showTableOptionsFormItem1
            // 
            this.showTableOptionsFormItem1.Id = 627;
            this.showTableOptionsFormItem1.Name = "showTableOptionsFormItem1";
            // 
            // changeFloatingObjectFillColorItem1
            // 
            this.changeFloatingObjectFillColorItem1.Id = 628;
            this.changeFloatingObjectFillColorItem1.Name = "changeFloatingObjectFillColorItem1";
            // 
            // changeFloatingObjectOutlineColorItem1
            // 
            this.changeFloatingObjectOutlineColorItem1.Id = 629;
            this.changeFloatingObjectOutlineColorItem1.Name = "changeFloatingObjectOutlineColorItem1";
            // 
            // changeFloatingObjectOutlineWeightItem1
            // 
            this.changeFloatingObjectOutlineWeightItem1.Edit = this.repositoryItemFloatingObjectOutlineWeight3;
            this.changeFloatingObjectOutlineWeightItem1.EditValue = 20;
            this.changeFloatingObjectOutlineWeightItem1.Id = 630;
            this.changeFloatingObjectOutlineWeightItem1.Name = "changeFloatingObjectOutlineWeightItem1";
            // 
            // repositoryItemFloatingObjectOutlineWeight3
            // 
            this.repositoryItemFloatingObjectOutlineWeight3.AutoHeight = false;
            this.repositoryItemFloatingObjectOutlineWeight3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFloatingObjectOutlineWeight3.Control = this.richTemplate;
            this.repositoryItemFloatingObjectOutlineWeight3.Name = "repositoryItemFloatingObjectOutlineWeight3";
            // 
            // changeFloatingObjectTextWrapTypeItem1
            // 
            this.changeFloatingObjectTextWrapTypeItem1.Id = 631;
            this.changeFloatingObjectTextWrapTypeItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectSquareTextWrapTypeItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectTightTextWrapTypeItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectThroughTextWrapTypeItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectTopAndBottomTextWrapTypeItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectBehindTextWrapTypeItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectInFrontOfTextWrapTypeItem1)});
            this.changeFloatingObjectTextWrapTypeItem1.Name = "changeFloatingObjectTextWrapTypeItem1";
            // 
            // setFloatingObjectSquareTextWrapTypeItem1
            // 
            this.setFloatingObjectSquareTextWrapTypeItem1.Id = 632;
            this.setFloatingObjectSquareTextWrapTypeItem1.Name = "setFloatingObjectSquareTextWrapTypeItem1";
            // 
            // setFloatingObjectTightTextWrapTypeItem1
            // 
            this.setFloatingObjectTightTextWrapTypeItem1.Id = 633;
            this.setFloatingObjectTightTextWrapTypeItem1.Name = "setFloatingObjectTightTextWrapTypeItem1";
            // 
            // setFloatingObjectThroughTextWrapTypeItem1
            // 
            this.setFloatingObjectThroughTextWrapTypeItem1.Id = 634;
            this.setFloatingObjectThroughTextWrapTypeItem1.Name = "setFloatingObjectThroughTextWrapTypeItem1";
            // 
            // setFloatingObjectTopAndBottomTextWrapTypeItem1
            // 
            this.setFloatingObjectTopAndBottomTextWrapTypeItem1.Id = 635;
            this.setFloatingObjectTopAndBottomTextWrapTypeItem1.Name = "setFloatingObjectTopAndBottomTextWrapTypeItem1";
            // 
            // setFloatingObjectBehindTextWrapTypeItem1
            // 
            this.setFloatingObjectBehindTextWrapTypeItem1.Id = 636;
            this.setFloatingObjectBehindTextWrapTypeItem1.Name = "setFloatingObjectBehindTextWrapTypeItem1";
            // 
            // setFloatingObjectInFrontOfTextWrapTypeItem1
            // 
            this.setFloatingObjectInFrontOfTextWrapTypeItem1.Id = 637;
            this.setFloatingObjectInFrontOfTextWrapTypeItem1.Name = "setFloatingObjectInFrontOfTextWrapTypeItem1";
            // 
            // changeFloatingObjectAlignmentItem1
            // 
            this.changeFloatingObjectAlignmentItem1.Id = 638;
            this.changeFloatingObjectAlignmentItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectTopLeftAlignmentItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectTopCenterAlignmentItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectTopRightAlignmentItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectMiddleLeftAlignmentItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectMiddleCenterAlignmentItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectMiddleRightAlignmentItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectBottomLeftAlignmentItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectBottomCenterAlignmentItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.setFloatingObjectBottomRightAlignmentItem1)});
            this.changeFloatingObjectAlignmentItem1.Name = "changeFloatingObjectAlignmentItem1";
            // 
            // setFloatingObjectTopLeftAlignmentItem1
            // 
            this.setFloatingObjectTopLeftAlignmentItem1.Id = 639;
            this.setFloatingObjectTopLeftAlignmentItem1.Name = "setFloatingObjectTopLeftAlignmentItem1";
            // 
            // setFloatingObjectTopCenterAlignmentItem1
            // 
            this.setFloatingObjectTopCenterAlignmentItem1.Id = 640;
            this.setFloatingObjectTopCenterAlignmentItem1.Name = "setFloatingObjectTopCenterAlignmentItem1";
            // 
            // setFloatingObjectTopRightAlignmentItem1
            // 
            this.setFloatingObjectTopRightAlignmentItem1.Id = 641;
            this.setFloatingObjectTopRightAlignmentItem1.Name = "setFloatingObjectTopRightAlignmentItem1";
            // 
            // setFloatingObjectMiddleLeftAlignmentItem1
            // 
            this.setFloatingObjectMiddleLeftAlignmentItem1.Id = 642;
            this.setFloatingObjectMiddleLeftAlignmentItem1.Name = "setFloatingObjectMiddleLeftAlignmentItem1";
            // 
            // setFloatingObjectMiddleCenterAlignmentItem1
            // 
            this.setFloatingObjectMiddleCenterAlignmentItem1.Id = 643;
            this.setFloatingObjectMiddleCenterAlignmentItem1.Name = "setFloatingObjectMiddleCenterAlignmentItem1";
            // 
            // setFloatingObjectMiddleRightAlignmentItem1
            // 
            this.setFloatingObjectMiddleRightAlignmentItem1.Id = 644;
            this.setFloatingObjectMiddleRightAlignmentItem1.Name = "setFloatingObjectMiddleRightAlignmentItem1";
            // 
            // setFloatingObjectBottomLeftAlignmentItem1
            // 
            this.setFloatingObjectBottomLeftAlignmentItem1.Id = 645;
            this.setFloatingObjectBottomLeftAlignmentItem1.Name = "setFloatingObjectBottomLeftAlignmentItem1";
            // 
            // setFloatingObjectBottomCenterAlignmentItem1
            // 
            this.setFloatingObjectBottomCenterAlignmentItem1.Id = 646;
            this.setFloatingObjectBottomCenterAlignmentItem1.Name = "setFloatingObjectBottomCenterAlignmentItem1";
            // 
            // setFloatingObjectBottomRightAlignmentItem1
            // 
            this.setFloatingObjectBottomRightAlignmentItem1.Id = 647;
            this.setFloatingObjectBottomRightAlignmentItem1.Name = "setFloatingObjectBottomRightAlignmentItem1";
            // 
            // floatingObjectBringForwardSubItem1
            // 
            this.floatingObjectBringForwardSubItem1.Id = 648;
            this.floatingObjectBringForwardSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.floatingObjectBringForwardItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.floatingObjectBringToFrontItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.floatingObjectBringInFrontOfTextItem1)});
            this.floatingObjectBringForwardSubItem1.Name = "floatingObjectBringForwardSubItem1";
            // 
            // floatingObjectBringForwardItem1
            // 
            this.floatingObjectBringForwardItem1.Id = 649;
            this.floatingObjectBringForwardItem1.Name = "floatingObjectBringForwardItem1";
            // 
            // floatingObjectBringToFrontItem1
            // 
            this.floatingObjectBringToFrontItem1.Id = 650;
            this.floatingObjectBringToFrontItem1.Name = "floatingObjectBringToFrontItem1";
            // 
            // floatingObjectBringInFrontOfTextItem1
            // 
            this.floatingObjectBringInFrontOfTextItem1.Id = 651;
            this.floatingObjectBringInFrontOfTextItem1.Name = "floatingObjectBringInFrontOfTextItem1";
            // 
            // floatingObjectSendBackwardSubItem1
            // 
            this.floatingObjectSendBackwardSubItem1.Id = 652;
            this.floatingObjectSendBackwardSubItem1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.floatingObjectSendBackwardItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.floatingObjectSendToBackItem1),
            new DevExpress.XtraBars.LinkPersistInfo(this.floatingObjectSendBehindTextItem1)});
            this.floatingObjectSendBackwardSubItem1.Name = "floatingObjectSendBackwardSubItem1";
            // 
            // floatingObjectSendBackwardItem1
            // 
            this.floatingObjectSendBackwardItem1.Id = 653;
            this.floatingObjectSendBackwardItem1.Name = "floatingObjectSendBackwardItem1";
            // 
            // floatingObjectSendToBackItem1
            // 
            this.floatingObjectSendToBackItem1.Id = 654;
            this.floatingObjectSendToBackItem1.Name = "floatingObjectSendToBackItem1";
            // 
            // floatingObjectSendBehindTextItem1
            // 
            this.floatingObjectSendBehindTextItem1.Id = 655;
            this.floatingObjectSendBehindTextItem1.Name = "floatingObjectSendBehindTextItem1";
            // 
            // btnMergeDocuments
            // 
            this.btnMergeDocuments.Caption = "Merge Documents";
            this.btnMergeDocuments.Glyph = global::ERP.Module.Properties.Resources.mergeToNewDocumentItem_Glyph;
            this.btnMergeDocuments.Id = 657;
            this.btnMergeDocuments.LargeGlyph = global::ERP.Module.Properties.Resources.mergeToNewDocumentItem_LargeGlyph;
            this.btnMergeDocuments.Name = "btnMergeDocuments";
            this.btnMergeDocuments.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mergeToNewDocumentItem_ItemClick);
            // 
            // btnSave
            // 
            this.btnSave.Caption = "Save";
            this.btnSave.Glyph = global::ERP.Module.Properties.Resources.Save_16x16;
            this.btnSave.Id = 658;
            this.btnSave.Name = "btnSave";
            this.btnSave.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnSave_ItemClick);
            // 
            // btnInsertMergeField
            // 
            this.btnInsertMergeField.Caption = "Insert Merge Field";
            this.btnInsertMergeField.Glyph = global::ERP.Module.Properties.Resources.InsertMergeField_16x16;
            this.btnInsertMergeField.Id = 659;
            this.btnInsertMergeField.Name = "btnInsertMergeField";
            this.btnInsertMergeField.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btnInsertMergeField_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Caption = "Merge Documents";
            this.barButtonItem3.Glyph = global::ERP.Module.Properties.Resources.mergeToNewDocumentItem_Glyph;
            this.barButtonItem3.Id = 660;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.mergeToNewDocumentItem_ItemClick);
            // 
            // barBieuMau
            // 
            this.barBieuMau.Caption = "Biểu mẫu:";
            this.barBieuMau.Edit = this.lookupBieuMau;
            this.barBieuMau.Id = 661;
            this.barBieuMau.Name = "barBieuMau";
            this.barBieuMau.Width = 200;
            this.barBieuMau.EditValueChanged += new EventHandler(this.barBieuMau_EditValueChanged);
            // 
            // lookupBieuMau
            // 
            this.lookupBieuMau.AutoHeight = false;
            this.lookupBieuMau.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(Keys.None), serializableAppearanceObject1, "Chọn biểu mẫu", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(Keys.None), serializableAppearanceObject2, "Cấu hình biểu mẫu", null, null, true)});
            this.lookupBieuMau.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenTaiLieu", "Biểu mẫu", 200, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.Ascending, DevExpress.Utils.DefaultBoolean.True)});
            this.lookupBieuMau.Name = "lookupBieuMau";
            this.lookupBieuMau.NullText = "";
            // 
            // barDonViTinh
            // 
            this.barDonViTinh.Caption = "Đơn vị đo";
            this.barDonViTinh.Edit = this.repositoryItemComboBox1;
            this.barDonViTinh.EditValue = "Centimet";
            this.barDonViTinh.Id = 1;
            this.barDonViTinh.Name = "barDonViTinh";
            this.barDonViTinh.Width = 100;
            this.barDonViTinh.EditValueChanged += new EventHandler(this.barEditItem2_EditValueChanged);
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Items.AddRange(new object[] {
            "Centimet",
            "Millimet",
            "Inch",
            "Document",
            "Point"});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // headerFooterToolsRibbonPageCategory1
            // 
            this.headerFooterToolsRibbonPageCategory1.Color = Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(176)))), ((int)(((byte)(35)))));
            this.headerFooterToolsRibbonPageCategory1.Control = this.richTemplate;
            this.headerFooterToolsRibbonPageCategory1.Name = "headerFooterToolsRibbonPageCategory1";
            this.headerFooterToolsRibbonPageCategory1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.headerFooterToolsDesignRibbonPage1});
            // 
            // headerFooterToolsDesignRibbonPage1
            // 
            this.headerFooterToolsDesignRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.headerFooterToolsDesignNavigationRibbonPageGroup1,
            this.headerFooterToolsDesignOptionsRibbonPageGroup1,
            this.headerFooterToolsDesignCloseRibbonPageGroup1});
            this.headerFooterToolsDesignRibbonPage1.Name = "headerFooterToolsDesignRibbonPage1";
            // 
            // headerFooterToolsDesignNavigationRibbonPageGroup1
            // 
            this.headerFooterToolsDesignNavigationRibbonPageGroup1.ItemLinks.Add(this.goToPageHeaderItem1);
            this.headerFooterToolsDesignNavigationRibbonPageGroup1.ItemLinks.Add(this.goToPageFooterItem1);
            this.headerFooterToolsDesignNavigationRibbonPageGroup1.ItemLinks.Add(this.goToNextHeaderFooterItem1);
            this.headerFooterToolsDesignNavigationRibbonPageGroup1.ItemLinks.Add(this.goToPreviousHeaderFooterItem1);
            this.headerFooterToolsDesignNavigationRibbonPageGroup1.ItemLinks.Add(this.toggleLinkToPreviousItem1);
            this.headerFooterToolsDesignNavigationRibbonPageGroup1.Name = "headerFooterToolsDesignNavigationRibbonPageGroup1";
            // 
            // headerFooterToolsDesignOptionsRibbonPageGroup1
            // 
            this.headerFooterToolsDesignOptionsRibbonPageGroup1.ItemLinks.Add(this.toggleDifferentFirstPageItem1);
            this.headerFooterToolsDesignOptionsRibbonPageGroup1.ItemLinks.Add(this.toggleDifferentOddAndEvenPagesItem1);
            this.headerFooterToolsDesignOptionsRibbonPageGroup1.Name = "headerFooterToolsDesignOptionsRibbonPageGroup1";
            // 
            // headerFooterToolsDesignCloseRibbonPageGroup1
            // 
            this.headerFooterToolsDesignCloseRibbonPageGroup1.ItemLinks.Add(this.closePageHeaderFooterItem1);
            this.headerFooterToolsDesignCloseRibbonPageGroup1.Name = "headerFooterToolsDesignCloseRibbonPageGroup1";
            // 
            // tableToolsRibbonPageCategory1
            // 
            this.tableToolsRibbonPageCategory1.Color = Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(233)))), ((int)(((byte)(20)))));
            this.tableToolsRibbonPageCategory1.Control = this.richTemplate;
            this.tableToolsRibbonPageCategory1.Name = "tableToolsRibbonPageCategory1";
            this.tableToolsRibbonPageCategory1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.tableDesignRibbonPage1,
            this.tableLayoutRibbonPage1});
            // 
            // tableDesignRibbonPage1
            // 
            this.tableDesignRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.tableStyleOptionsRibbonPageGroup1,
            this.tableStylesRibbonPageGroup1,
            this.tableDrawBordersRibbonPageGroup1});
            this.tableDesignRibbonPage1.Name = "tableDesignRibbonPage1";
            // 
            // tableStyleOptionsRibbonPageGroup1
            // 
            this.tableStyleOptionsRibbonPageGroup1.ItemLinks.Add(this.toggleFirstRowItem1);
            this.tableStyleOptionsRibbonPageGroup1.ItemLinks.Add(this.toggleLastRowItem1);
            this.tableStyleOptionsRibbonPageGroup1.ItemLinks.Add(this.toggleBandedRowsItem1);
            this.tableStyleOptionsRibbonPageGroup1.ItemLinks.Add(this.toggleFirstColumnItem1);
            this.tableStyleOptionsRibbonPageGroup1.ItemLinks.Add(this.toggleLastColumnItem1);
            this.tableStyleOptionsRibbonPageGroup1.ItemLinks.Add(this.toggleBandedColumnItem1);
            this.tableStyleOptionsRibbonPageGroup1.Name = "tableStyleOptionsRibbonPageGroup1";
            // 
            // tableStylesRibbonPageGroup1
            // 
            this.tableStylesRibbonPageGroup1.ItemLinks.Add(this.galleryChangeTableStyleItem1);
            this.tableStylesRibbonPageGroup1.Name = "tableStylesRibbonPageGroup1";
            // 
            // tableDrawBordersRibbonPageGroup1
            // 
            this.tableDrawBordersRibbonPageGroup1.ItemLinks.Add(this.changeTableBorderLineStyleItem1);
            this.tableDrawBordersRibbonPageGroup1.ItemLinks.Add(this.changeTableBorderLineWeightItem1);
            this.tableDrawBordersRibbonPageGroup1.ItemLinks.Add(this.changeTableBorderColorItem1);
            this.tableDrawBordersRibbonPageGroup1.ItemLinks.Add(this.changeTableBordersItem1);
            this.tableDrawBordersRibbonPageGroup1.ItemLinks.Add(this.changeTableCellsShadingItem1);
            this.tableDrawBordersRibbonPageGroup1.Name = "tableDrawBordersRibbonPageGroup1";
            // 
            // tableLayoutRibbonPage1
            // 
            this.tableLayoutRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.tableTableRibbonPageGroup1,
            this.tableRowsAndColumnsRibbonPageGroup1,
            this.tableMergeRibbonPageGroup1,
            this.tableCellSizeRibbonPageGroup1,
            this.tableAlignmentRibbonPageGroup1});
            this.tableLayoutRibbonPage1.Name = "tableLayoutRibbonPage1";
            // 
            // tableTableRibbonPageGroup1
            // 
            this.tableTableRibbonPageGroup1.ItemLinks.Add(this.selectTableElementsItem1);
            this.tableTableRibbonPageGroup1.ItemLinks.Add(this.toggleShowTableGridLinesItem1);
            this.tableTableRibbonPageGroup1.ItemLinks.Add(this.showTablePropertiesFormItem1);
            this.tableTableRibbonPageGroup1.Name = "tableTableRibbonPageGroup1";
            // 
            // tableRowsAndColumnsRibbonPageGroup1
            // 
            this.tableRowsAndColumnsRibbonPageGroup1.ItemLinks.Add(this.deleteTableElementsItem1);
            this.tableRowsAndColumnsRibbonPageGroup1.ItemLinks.Add(this.insertTableRowAboveItem1);
            this.tableRowsAndColumnsRibbonPageGroup1.ItemLinks.Add(this.insertTableRowBelowItem1);
            this.tableRowsAndColumnsRibbonPageGroup1.ItemLinks.Add(this.insertTableColumnToLeftItem1);
            this.tableRowsAndColumnsRibbonPageGroup1.ItemLinks.Add(this.insertTableColumnToRightItem1);
            this.tableRowsAndColumnsRibbonPageGroup1.Name = "tableRowsAndColumnsRibbonPageGroup1";
            // 
            // tableMergeRibbonPageGroup1
            // 
            this.tableMergeRibbonPageGroup1.ItemLinks.Add(this.mergeTableCellsItem1);
            this.tableMergeRibbonPageGroup1.ItemLinks.Add(this.showSplitTableCellsForm1);
            this.tableMergeRibbonPageGroup1.ItemLinks.Add(this.splitTableItem1);
            this.tableMergeRibbonPageGroup1.Name = "tableMergeRibbonPageGroup1";
            // 
            // tableCellSizeRibbonPageGroup1
            // 
            this.tableCellSizeRibbonPageGroup1.ItemLinks.Add(this.toggleTableAutoFitItem1);
            this.tableCellSizeRibbonPageGroup1.Name = "tableCellSizeRibbonPageGroup1";
            // 
            // tableAlignmentRibbonPageGroup1
            // 
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsTopLeftAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsMiddleLeftAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsBottomLeftAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsTopCenterAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsMiddleCenterAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsBottomCenterAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsTopRightAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsMiddleRightAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.toggleTableCellsBottomRightAlignmentItem1);
            this.tableAlignmentRibbonPageGroup1.ItemLinks.Add(this.showTableOptionsFormItem1);
            this.tableAlignmentRibbonPageGroup1.Name = "tableAlignmentRibbonPageGroup1";
            // 
            // floatingPictureToolsRibbonPageCategory1
            // 
            this.floatingPictureToolsRibbonPageCategory1.Color = Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(0)))), ((int)(((byte)(119)))));
            this.floatingPictureToolsRibbonPageCategory1.Control = this.richTemplate;
            this.floatingPictureToolsRibbonPageCategory1.Name = "floatingPictureToolsRibbonPageCategory1";
            this.floatingPictureToolsRibbonPageCategory1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.floatingPictureToolsFormatPage1});
            // 
            // floatingPictureToolsFormatPage1
            // 
            this.floatingPictureToolsFormatPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.floatingPictureToolsShapeStylesPageGroup1,
            this.floatingPictureToolsArrangePageGroup1});
            this.floatingPictureToolsFormatPage1.Name = "floatingPictureToolsFormatPage1";
            // 
            // floatingPictureToolsShapeStylesPageGroup1
            // 
            this.floatingPictureToolsShapeStylesPageGroup1.ItemLinks.Add(this.changeFloatingObjectFillColorItem1);
            this.floatingPictureToolsShapeStylesPageGroup1.ItemLinks.Add(this.changeFloatingObjectOutlineColorItem1);
            this.floatingPictureToolsShapeStylesPageGroup1.ItemLinks.Add(this.changeFloatingObjectOutlineWeightItem1);
            this.floatingPictureToolsShapeStylesPageGroup1.Name = "floatingPictureToolsShapeStylesPageGroup1";
            // 
            // floatingPictureToolsArrangePageGroup1
            // 
            this.floatingPictureToolsArrangePageGroup1.ItemLinks.Add(this.changeFloatingObjectTextWrapTypeItem1);
            this.floatingPictureToolsArrangePageGroup1.ItemLinks.Add(this.changeFloatingObjectAlignmentItem1);
            this.floatingPictureToolsArrangePageGroup1.ItemLinks.Add(this.floatingObjectBringForwardSubItem1);
            this.floatingPictureToolsArrangePageGroup1.ItemLinks.Add(this.floatingObjectSendBackwardSubItem1);
            this.floatingPictureToolsArrangePageGroup1.Name = "floatingPictureToolsArrangePageGroup1";
            // 
            // homeRibbonPage2
            // 
            this.homeRibbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup3,
            this.clipboardRibbonPageGroup2,
            this.fontRibbonPageGroup2,
            this.paragraphRibbonPageGroup2,
            this.stylesRibbonPageGroup2,
            this.editingRibbonPageGroup2});
            this.homeRibbonPage2.Name = "homeRibbonPage2";
            reduceOperation1.Behavior = DevExpress.XtraBars.Ribbon.ReduceOperationBehavior.UntilAvailable;
            reduceOperation1.Group = this.stylesRibbonPageGroup2;
            reduceOperation1.ItemLinkIndex = 0;
            reduceOperation1.ItemLinksCount = 0;
            reduceOperation1.Operation = DevExpress.XtraBars.Ribbon.ReduceOperationType.Gallery;
            this.homeRibbonPage2.ReduceOperations.Add(reduceOperation1);
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.fileOpenItem1);
            this.ribbonPageGroup2.ItemLinks.Add(this.fileSaveItem1);
            this.ribbonPageGroup2.ItemLinks.Add(this.fileSaveAsItem1);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "File";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.quickPrintItem1);
            this.ribbonPageGroup3.ItemLinks.Add(this.printItem1);
            this.ribbonPageGroup3.ItemLinks.Add(this.printPreviewItem1);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Print";
            // 
            // clipboardRibbonPageGroup2
            // 
            this.clipboardRibbonPageGroup2.ItemLinks.Add(this.pasteItem1);
            this.clipboardRibbonPageGroup2.ItemLinks.Add(this.cutItem1);
            this.clipboardRibbonPageGroup2.ItemLinks.Add(this.copyItem1);
            this.clipboardRibbonPageGroup2.ItemLinks.Add(this.pasteSpecialItem1);
            this.clipboardRibbonPageGroup2.Name = "clipboardRibbonPageGroup2";
            // 
            // fontRibbonPageGroup2
            // 
            this.fontRibbonPageGroup2.ItemLinks.Add(this.barButtonGroup15);
            this.fontRibbonPageGroup2.ItemLinks.Add(this.barButtonGroup16);
            this.fontRibbonPageGroup2.ItemLinks.Add(this.barButtonGroup17);
            this.fontRibbonPageGroup2.ItemLinks.Add(this.changeTextCaseItem1);
            this.fontRibbonPageGroup2.ItemLinks.Add(this.clearFormattingItem1);
            this.fontRibbonPageGroup2.Name = "fontRibbonPageGroup2";
            // 
            // paragraphRibbonPageGroup2
            // 
            this.paragraphRibbonPageGroup2.ItemLinks.Add(this.barButtonGroup18);
            this.paragraphRibbonPageGroup2.ItemLinks.Add(this.barButtonGroup19);
            this.paragraphRibbonPageGroup2.ItemLinks.Add(this.barButtonGroup20);
            this.paragraphRibbonPageGroup2.ItemLinks.Add(this.barButtonGroup21);
            this.paragraphRibbonPageGroup2.Name = "paragraphRibbonPageGroup2";
            // 
            // editingRibbonPageGroup2
            // 
            this.editingRibbonPageGroup2.ItemLinks.Add(this.findItem1);
            this.editingRibbonPageGroup2.ItemLinks.Add(this.replaceItem1);
            this.editingRibbonPageGroup2.Name = "editingRibbonPageGroup2";
            // 
            // insertRibbonPage2
            // 
            this.insertRibbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.pagesRibbonPageGroup2,
            this.tablesRibbonPageGroup2,
            this.illustrationsRibbonPageGroup2,
            this.linksRibbonPageGroup2,
            this.headerFooterRibbonPageGroup2,
            this.textRibbonPageGroup2,
            this.symbolsRibbonPageGroup2});
            this.insertRibbonPage2.Name = "insertRibbonPage2";
            // 
            // pagesRibbonPageGroup2
            // 
            this.pagesRibbonPageGroup2.ItemLinks.Add(this.insertPageBreakItem1);
            this.pagesRibbonPageGroup2.Name = "pagesRibbonPageGroup2";
            // 
            // tablesRibbonPageGroup2
            // 
            this.tablesRibbonPageGroup2.ItemLinks.Add(this.insertTableItem1);
            this.tablesRibbonPageGroup2.Name = "tablesRibbonPageGroup2";
            // 
            // illustrationsRibbonPageGroup2
            // 
            this.illustrationsRibbonPageGroup2.ItemLinks.Add(this.insertPictureItem1);
            this.illustrationsRibbonPageGroup2.ItemLinks.Add(this.insertFloatingPictureItem1);
            this.illustrationsRibbonPageGroup2.Name = "illustrationsRibbonPageGroup2";
            // 
            // linksRibbonPageGroup2
            // 
            this.linksRibbonPageGroup2.ItemLinks.Add(this.insertBookmarkItem1);
            this.linksRibbonPageGroup2.ItemLinks.Add(this.insertHyperlinkItem1);
            this.linksRibbonPageGroup2.Name = "linksRibbonPageGroup2";
            // 
            // headerFooterRibbonPageGroup2
            // 
            this.headerFooterRibbonPageGroup2.ItemLinks.Add(this.editPageHeaderItem1);
            this.headerFooterRibbonPageGroup2.ItemLinks.Add(this.editPageFooterItem1);
            this.headerFooterRibbonPageGroup2.ItemLinks.Add(this.insertPageNumberItem1);
            this.headerFooterRibbonPageGroup2.ItemLinks.Add(this.insertPageCountItem1);
            this.headerFooterRibbonPageGroup2.Name = "headerFooterRibbonPageGroup2";
            // 
            // textRibbonPageGroup2
            // 
            this.textRibbonPageGroup2.ItemLinks.Add(this.insertTextBoxItem1);
            this.textRibbonPageGroup2.Name = "textRibbonPageGroup2";
            // 
            // symbolsRibbonPageGroup2
            // 
            this.symbolsRibbonPageGroup2.ItemLinks.Add(this.insertSymbolItem1);
            this.symbolsRibbonPageGroup2.Name = "symbolsRibbonPageGroup2";
            // 
            // pageLayoutRibbonPage2
            // 
            this.pageLayoutRibbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.pageSetupRibbonPageGroup2,
            this.pageBackgroundRibbonPageGroup1});
            this.pageLayoutRibbonPage2.Name = "pageLayoutRibbonPage2";
            // 
            // pageSetupRibbonPageGroup2
            // 
            this.pageSetupRibbonPageGroup2.ItemLinks.Add(this.changeSectionPageMarginsItem1);
            this.pageSetupRibbonPageGroup2.ItemLinks.Add(this.changeSectionPageOrientationItem1);
            this.pageSetupRibbonPageGroup2.ItemLinks.Add(this.changeSectionPaperKindItem1);
            this.pageSetupRibbonPageGroup2.ItemLinks.Add(this.changeSectionColumnsItem1);
            this.pageSetupRibbonPageGroup2.ItemLinks.Add(this.insertBreakItem1);
            this.pageSetupRibbonPageGroup2.ItemLinks.Add(this.changeSectionLineNumberingItem1);
            this.pageSetupRibbonPageGroup2.Name = "pageSetupRibbonPageGroup2";
            // 
            // pageBackgroundRibbonPageGroup1
            // 
            this.pageBackgroundRibbonPageGroup1.ItemLinks.Add(this.changePageColorItem1);
            this.pageBackgroundRibbonPageGroup1.Name = "pageBackgroundRibbonPageGroup1";
            // 
            // referencesRibbonPage2
            // 
            this.referencesRibbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.tableOfContentsRibbonPageGroup2,
            this.captionsRibbonPageGroup2});
            this.referencesRibbonPage2.Name = "referencesRibbonPage2";
            // 
            // tableOfContentsRibbonPageGroup2
            // 
            this.tableOfContentsRibbonPageGroup2.ItemLinks.Add(this.insertTableOfContentsItem1);
            this.tableOfContentsRibbonPageGroup2.ItemLinks.Add(this.updateTableOfContentsItem1);
            this.tableOfContentsRibbonPageGroup2.ItemLinks.Add(this.addParagraphsToTableOfContentItem1);
            this.tableOfContentsRibbonPageGroup2.Name = "tableOfContentsRibbonPageGroup2";
            // 
            // captionsRibbonPageGroup2
            // 
            this.captionsRibbonPageGroup2.ItemLinks.Add(this.insertCaptionPlaceholderItem1);
            this.captionsRibbonPageGroup2.ItemLinks.Add(this.insertTableOfFiguresPlaceholderItem1);
            this.captionsRibbonPageGroup2.ItemLinks.Add(this.updateTableOfContentsItem1);
            this.captionsRibbonPageGroup2.Name = "captionsRibbonPageGroup2";
            // 
            // mailingsRibbonPage2
            // 
            this.mailingsRibbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.mailMergeRibbonPageGroup2,
            this.ribbonPageGroup1});
            this.mailingsRibbonPage2.Name = "mailingsRibbonPage2";
            // 
            // mailMergeRibbonPageGroup2
            // 
            this.mailMergeRibbonPageGroup2.ItemLinks.Add(this.insertMergeFieldItem1);
            this.mailMergeRibbonPageGroup2.ItemLinks.Add(this.showAllFieldCodesItem1);
            this.mailMergeRibbonPageGroup2.ItemLinks.Add(this.showAllFieldResultsItem1);
            this.mailMergeRibbonPageGroup2.ItemLinks.Add(this.toggleViewMergedDataItem1);
            this.mailMergeRibbonPageGroup2.Name = "mailMergeRibbonPageGroup2";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.btnMergeDocuments);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Merge";
            // 
            // reviewRibbonPage2
            // 
            this.reviewRibbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.documentProofingRibbonPageGroup2,
            this.documentProtectionRibbonPageGroup2});
            this.reviewRibbonPage2.Name = "reviewRibbonPage2";
            // 
            // documentProofingRibbonPageGroup2
            // 
            this.documentProofingRibbonPageGroup2.ItemLinks.Add(this.checkSpellingItem1);
            this.documentProofingRibbonPageGroup2.Name = "documentProofingRibbonPageGroup2";
            // 
            // documentProtectionRibbonPageGroup2
            // 
            this.documentProtectionRibbonPageGroup2.ItemLinks.Add(this.protectDocumentItem1);
            this.documentProtectionRibbonPageGroup2.ItemLinks.Add(this.changeRangeEditingPermissionsItem1);
            this.documentProtectionRibbonPageGroup2.ItemLinks.Add(this.unprotectDocumentItem1);
            this.documentProtectionRibbonPageGroup2.Name = "documentProtectionRibbonPageGroup2";
            // 
            // viewRibbonPage2
            // 
            this.viewRibbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.documentViewsRibbonPageGroup2,
            this.showRibbonPageGroup2,
            this.zoomRibbonPageGroup2,
            this.ribbonPageGroup4});
            this.viewRibbonPage2.Name = "viewRibbonPage2";
            // 
            // documentViewsRibbonPageGroup2
            // 
            this.documentViewsRibbonPageGroup2.ItemLinks.Add(this.switchToSimpleViewItem1);
            this.documentViewsRibbonPageGroup2.ItemLinks.Add(this.switchToDraftViewItem1);
            this.documentViewsRibbonPageGroup2.ItemLinks.Add(this.switchToPrintLayoutViewItem1);
            this.documentViewsRibbonPageGroup2.Name = "documentViewsRibbonPageGroup2";
            // 
            // showRibbonPageGroup2
            // 
            this.showRibbonPageGroup2.ItemLinks.Add(this.toggleShowHorizontalRulerItem1);
            this.showRibbonPageGroup2.ItemLinks.Add(this.toggleShowVerticalRulerItem1);
            this.showRibbonPageGroup2.Name = "showRibbonPageGroup2";
            // 
            // zoomRibbonPageGroup2
            // 
            this.zoomRibbonPageGroup2.ItemLinks.Add(this.zoomOutItem1);
            this.zoomRibbonPageGroup2.ItemLinks.Add(this.zoomInItem1);
            this.zoomRibbonPageGroup2.Name = "zoomRibbonPageGroup2";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.ItemLinks.Add(this.barDonViTinh);
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "Settings";
            // 
            // repositoryItemBorderLineStyle1
            // 
            this.repositoryItemBorderLineStyle1.AutoHeight = false;
            this.repositoryItemBorderLineStyle1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBorderLineStyle1.Control = this.richResult;
            this.repositoryItemBorderLineStyle1.Name = "repositoryItemBorderLineStyle1";
            // 
            // richResult
            // 
            this.richResult.Dock = DockStyle.Fill;
            this.richResult.Location = new Point(0, 0);
            this.richResult.MenuManager = this.ribbonControl1;
            this.richResult.Name = "richResult";
            this.richResult.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richResult.Options.MailMerge.KeepLastParagraph = false;
            this.richResult.Size = new Size(1038, 360);
            this.richResult.TabIndex = 1;
            this.richResult.Unit = DevExpress.Office.DocumentUnit.Centimeter;
            this.richResult.CalculateDocumentVariable += new DevExpress.XtraRichEdit.CalculateDocumentVariableEventHandler(this.richResult_CalculateDocumentVariable);
            this.richResult.ZoomChanged += new EventHandler(this.richEditControl_ZoomChanged);
            // 
            // repositoryItemBorderLineWeight1
            // 
            this.repositoryItemBorderLineWeight1.AutoHeight = false;
            this.repositoryItemBorderLineWeight1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBorderLineWeight1.Control = this.richResult;
            this.repositoryItemBorderLineWeight1.Name = "repositoryItemBorderLineWeight1";
            // 
            // repositoryItemRichEditStyleEdit1
            // 
            this.repositoryItemRichEditStyleEdit1.AutoHeight = false;
            this.repositoryItemRichEditStyleEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditStyleEdit1.Control = this.richTemplate;
            this.repositoryItemRichEditStyleEdit1.Name = "repositoryItemRichEditStyleEdit1";
            // 
            // repositoryItemBorderLineStyle2
            // 
            this.repositoryItemBorderLineStyle2.AutoHeight = false;
            this.repositoryItemBorderLineStyle2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBorderLineStyle2.Control = this.richTemplate;
            this.repositoryItemBorderLineStyle2.Name = "repositoryItemBorderLineStyle2";
            // 
            // repositoryItemBorderLineWeight2
            // 
            this.repositoryItemBorderLineWeight2.AutoHeight = false;
            this.repositoryItemBorderLineWeight2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBorderLineWeight2.Control = this.richTemplate;
            this.repositoryItemBorderLineWeight2.Name = "repositoryItemBorderLineWeight2";
            // 
            // repositoryItemFloatingObjectOutlineWeight1
            // 
            this.repositoryItemFloatingObjectOutlineWeight1.AutoHeight = false;
            this.repositoryItemFloatingObjectOutlineWeight1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFloatingObjectOutlineWeight1.Control = this.richTemplate;
            this.repositoryItemFloatingObjectOutlineWeight1.Name = "repositoryItemFloatingObjectOutlineWeight1";
            // 
            // repositoryItemFontEdit2
            // 
            this.repositoryItemFontEdit2.AutoHeight = false;
            this.repositoryItemFontEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFontEdit2.Name = "repositoryItemFontEdit2";
            // 
            // repositoryItemRichEditFontSizeEdit2
            // 
            this.repositoryItemRichEditFontSizeEdit2.AutoHeight = false;
            this.repositoryItemRichEditFontSizeEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditFontSizeEdit2.Control = this.richResult;
            this.repositoryItemRichEditFontSizeEdit2.Name = "repositoryItemRichEditFontSizeEdit2";
            // 
            // repositoryItemRichEditStyleEdit2
            // 
            this.repositoryItemRichEditStyleEdit2.AutoHeight = false;
            this.repositoryItemRichEditStyleEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemRichEditStyleEdit2.Control = this.richResult;
            this.repositoryItemRichEditStyleEdit2.Name = "repositoryItemRichEditStyleEdit2";
            // 
            // repositoryItemBorderLineStyle3
            // 
            this.repositoryItemBorderLineStyle3.AutoHeight = false;
            this.repositoryItemBorderLineStyle3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBorderLineStyle3.Control = this.richResult;
            this.repositoryItemBorderLineStyle3.Name = "repositoryItemBorderLineStyle3";
            // 
            // repositoryItemBorderLineWeight3
            // 
            this.repositoryItemBorderLineWeight3.AutoHeight = false;
            this.repositoryItemBorderLineWeight3.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemBorderLineWeight3.Control = this.richResult;
            this.repositoryItemBorderLineWeight3.Name = "repositoryItemBorderLineWeight3";
            // 
            // repositoryItemFloatingObjectOutlineWeight2
            // 
            this.repositoryItemFloatingObjectOutlineWeight2.AutoHeight = false;
            this.repositoryItemFloatingObjectOutlineWeight2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemFloatingObjectOutlineWeight2.Control = this.richResult;
            this.repositoryItemFloatingObjectOutlineWeight2.Name = "repositoryItemFloatingObjectOutlineWeight2";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.barEditItem1);
            this.ribbonStatusBar1.Location = new Point(0, 532);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new Size(1046, 31);
            // 
            // backstageViewControl1
            // 
            this.backstageViewControl1.ColorScheme = DevExpress.XtraBars.Ribbon.RibbonControlColorScheme.Yellow;
            this.backstageViewControl1.Controls.Add(this.backstageViewClientControl1);
            this.backstageViewControl1.Items.Add(this.backstageViewTabItem1);
            this.backstageViewControl1.Location = new Point(72, 216);
            this.backstageViewControl1.Name = "backstageViewControl1";
            this.backstageViewControl1.SelectedTab = null;
            this.backstageViewControl1.Size = new Size(480, 150);
            this.backstageViewControl1.TabIndex = 7;
            // 
            // backstageViewClientControl1
            // 
            this.backstageViewClientControl1.Location = new Point(188, 0);
            this.backstageViewClientControl1.Name = "backstageViewClientControl1";
            this.backstageViewClientControl1.Size = new Size(292, 150);
            this.backstageViewClientControl1.TabIndex = 0;
            // 
            // backstageViewTabItem1
            // 
            this.backstageViewTabItem1.Caption = "backstageViewTabItem1";
            this.backstageViewTabItem1.ContentControl = this.backstageViewClientControl1;
            this.backstageViewTabItem1.Name = "backstageViewTabItem1";
            this.backstageViewTabItem1.Selected = false;
            // 
            // homeRibbonPage1
            // 
            this.homeRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.clipboardRibbonPageGroup1,
            this.fontRibbonPageGroup1,
            this.paragraphRibbonPageGroup1,
            this.stylesRibbonPageGroup1,
            this.editingRibbonPageGroup1});
            this.homeRibbonPage1.Name = "homeRibbonPage1";
            // 
            // clipboardRibbonPageGroup1
            // 
            this.clipboardRibbonPageGroup1.Name = "clipboardRibbonPageGroup1";
            this.clipboardRibbonPageGroup1.Text = "";
            // 
            // fontRibbonPageGroup1
            // 
            this.fontRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup1);
            this.fontRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup2);
            this.fontRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup3);
            this.fontRibbonPageGroup1.Name = "fontRibbonPageGroup1";
            this.fontRibbonPageGroup1.Text = "";
            // 
            // paragraphRibbonPageGroup1
            // 
            this.paragraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup4);
            this.paragraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup5);
            this.paragraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup6);
            this.paragraphRibbonPageGroup1.ItemLinks.Add(this.barButtonGroup7);
            this.paragraphRibbonPageGroup1.Name = "paragraphRibbonPageGroup1";
            this.paragraphRibbonPageGroup1.Text = "";
            // 
            // stylesRibbonPageGroup1
            // 
            this.stylesRibbonPageGroup1.Name = "stylesRibbonPageGroup1";
            this.stylesRibbonPageGroup1.Text = "";
            // 
            // editingRibbonPageGroup1
            // 
            this.editingRibbonPageGroup1.Name = "editingRibbonPageGroup1";
            this.editingRibbonPageGroup1.Text = "";
            // 
            // fileRibbonPage1
            // 
            this.fileRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.commonRibbonPageGroup2});
            this.fileRibbonPage1.Name = "fileRibbonPage1";
            // 
            // commonRibbonPageGroup2
            // 
            this.commonRibbonPageGroup2.Name = "commonRibbonPageGroup2";
            this.commonRibbonPageGroup2.Text = "";
            // 
            // insertRibbonPage1
            // 
            this.insertRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.pagesRibbonPageGroup1,
            this.tablesRibbonPageGroup1,
            this.illustrationsRibbonPageGroup1,
            this.linksRibbonPageGroup1,
            this.headerFooterRibbonPageGroup1,
            this.textRibbonPageGroup1,
            this.symbolsRibbonPageGroup1});
            this.insertRibbonPage1.Name = "insertRibbonPage1";
            // 
            // pagesRibbonPageGroup1
            // 
            this.pagesRibbonPageGroup1.Name = "pagesRibbonPageGroup1";
            this.pagesRibbonPageGroup1.Text = "";
            // 
            // tablesRibbonPageGroup1
            // 
            this.tablesRibbonPageGroup1.Name = "tablesRibbonPageGroup1";
            this.tablesRibbonPageGroup1.Text = "";
            // 
            // illustrationsRibbonPageGroup1
            // 
            this.illustrationsRibbonPageGroup1.Name = "illustrationsRibbonPageGroup1";
            this.illustrationsRibbonPageGroup1.Text = "";
            // 
            // linksRibbonPageGroup1
            // 
            this.linksRibbonPageGroup1.Name = "linksRibbonPageGroup1";
            this.linksRibbonPageGroup1.Text = "";
            // 
            // headerFooterRibbonPageGroup1
            // 
            this.headerFooterRibbonPageGroup1.Name = "headerFooterRibbonPageGroup1";
            this.headerFooterRibbonPageGroup1.Text = "";
            // 
            // textRibbonPageGroup1
            // 
            this.textRibbonPageGroup1.Name = "textRibbonPageGroup1";
            this.textRibbonPageGroup1.Text = "";
            // 
            // symbolsRibbonPageGroup1
            // 
            this.symbolsRibbonPageGroup1.Name = "symbolsRibbonPageGroup1";
            this.symbolsRibbonPageGroup1.Text = "";
            // 
            // pageLayoutRibbonPage1
            // 
            this.pageLayoutRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.pageSetupRibbonPageGroup1});
            this.pageLayoutRibbonPage1.Name = "pageLayoutRibbonPage1";
            // 
            // pageSetupRibbonPageGroup1
            // 
            this.pageSetupRibbonPageGroup1.Name = "pageSetupRibbonPageGroup1";
            this.pageSetupRibbonPageGroup1.Text = "";
            // 
            // mailingsRibbonPage1
            // 
            this.mailingsRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.mailMergeRibbonPageGroup1});
            this.mailingsRibbonPage1.Name = "mailingsRibbonPage1";
            // 
            // mailMergeRibbonPageGroup1
            // 
            this.mailMergeRibbonPageGroup1.Name = "mailMergeRibbonPageGroup1";
            this.mailMergeRibbonPageGroup1.Text = "";
            // 
            // viewRibbonPage1
            // 
            this.viewRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.documentViewsRibbonPageGroup1,
            this.showRibbonPageGroup1,
            this.zoomRibbonPageGroup1});
            this.viewRibbonPage1.Name = "viewRibbonPage1";
            // 
            // documentViewsRibbonPageGroup1
            // 
            this.documentViewsRibbonPageGroup1.Name = "documentViewsRibbonPageGroup1";
            this.documentViewsRibbonPageGroup1.Text = "";
            // 
            // showRibbonPageGroup1
            // 
            this.showRibbonPageGroup1.Name = "showRibbonPageGroup1";
            this.showRibbonPageGroup1.Text = "";
            // 
            // zoomRibbonPageGroup1
            // 
            this.zoomRibbonPageGroup1.Name = "zoomRibbonPageGroup1";
            this.zoomRibbonPageGroup1.Text = "";
            // 
            // reviewRibbonPage1
            // 
            this.reviewRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.documentProofingRibbonPageGroup1,
            this.documentProtectionRibbonPageGroup1});
            this.reviewRibbonPage1.Name = "reviewRibbonPage1";
            // 
            // documentProofingRibbonPageGroup1
            // 
            this.documentProofingRibbonPageGroup1.Name = "documentProofingRibbonPageGroup1";
            this.documentProofingRibbonPageGroup1.Text = "";
            // 
            // documentProtectionRibbonPageGroup1
            // 
            this.documentProtectionRibbonPageGroup1.Name = "documentProtectionRibbonPageGroup1";
            this.documentProtectionRibbonPageGroup1.Text = "";
            // 
            // referencesRibbonPage1
            // 
            this.referencesRibbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.tableOfContentsRibbonPageGroup1,
            this.captionsRibbonPageGroup1});
            this.referencesRibbonPage1.Name = "referencesRibbonPage1";
            // 
            // tableOfContentsRibbonPageGroup1
            // 
            this.tableOfContentsRibbonPageGroup1.Name = "tableOfContentsRibbonPageGroup1";
            this.tableOfContentsRibbonPageGroup1.Text = "";
            // 
            // captionsRibbonPageGroup1
            // 
            this.captionsRibbonPageGroup1.Name = "captionsRibbonPageGroup1";
            this.captionsRibbonPageGroup1.Text = "";
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Dock = DockStyle.Fill;
            this.xtraTabControl.Location = new Point(0, 144);
            this.xtraTabControl.LookAndFeel.SkinName = "Office 2010 Blue";
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.tabDetail;
            this.xtraTabControl.Size = new Size(1046, 388);
            this.xtraTabControl.TabIndex = 4;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabTemplate,
            this.tabMaster,
            this.tabDetail,
            this.tabMaster1,
            this.tabDetail1,
            this.tabResult});
            this.xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl1_SelectedPageChanged);
            // 
            // tabDetail
            // 
            this.tabDetail.Controls.Add(this.richDetail);
            this.tabDetail.Name = "tabDetail";
            this.tabDetail.Size = new Size(1038, 360);
            this.tabDetail.Text = "Danh sách kèm theo";
            // 
            // richDetail
            // 
            this.richDetail.Dock = DockStyle.Fill;
            this.richDetail.Location = new Point(0, 0);
            this.richDetail.LookAndFeel.SkinName = "Office 2007 Blue";
            this.richDetail.MenuManager = this.ribbonControl1;
            this.richDetail.Name = "richDetail";
            this.richDetail.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richDetail.Options.MailMerge.KeepLastParagraph = false;
            this.richDetail.Size = new Size(1038, 360);
            this.richDetail.TabIndex = 1;
            this.richDetail.Unit = DevExpress.Office.DocumentUnit.Centimeter;
            this.richDetail.ZoomChanged += new EventHandler(this.richEditControl_ZoomChanged);
            // 
            // tabTemplate
            // 
            this.tabTemplate.Controls.Add(this.richTemplate);
            this.tabTemplate.Name = "tabTemplate";
            this.tabTemplate.Size = new Size(1038, 360);
            this.tabTemplate.Text = "Văn bản mẫu";
            // 
            // tabMaster
            // 
            this.tabMaster.Controls.Add(this.richMaster);
            this.tabMaster.Name = "tabMaster";
            this.tabMaster.Size = new Size(1038, 360);
            this.tabMaster.Text = "Tiêu đề";
            // 
            // richMaster
            // 
            this.richMaster.Dock = DockStyle.Fill;
            this.richMaster.Location = new Point(0, 0);
            this.richMaster.LookAndFeel.SkinName = "Office 2007 Blue";
            this.richMaster.MenuManager = this.ribbonControl1;
            this.richMaster.Name = "richMaster";
            this.richMaster.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richMaster.Options.MailMerge.KeepLastParagraph = false;
            this.richMaster.Size = new Size(1038, 360);
            this.richMaster.TabIndex = 1;
            this.richMaster.Unit = DevExpress.Office.DocumentUnit.Centimeter;
            // 
            // tabMaster1
            // 
            this.tabMaster1.Controls.Add(this.richMaster1);
            this.tabMaster1.Name = "tabMaster1";
            this.tabMaster1.Size = new Size(1038, 360);
            this.tabMaster1.Text = "Tiêu đề 1";
            // 
            // richMaster1
            // 
            this.richMaster1.Dock = DockStyle.Fill;
            this.richMaster1.Location = new Point(0, 0);
            this.richMaster1.LookAndFeel.SkinName = "Office 2007 Blue";
            this.richMaster1.MenuManager = this.ribbonControl1;
            this.richMaster1.Name = "richMaster1";
            this.richMaster1.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richMaster1.Options.MailMerge.KeepLastParagraph = false;
            this.richMaster1.Size = new Size(1038, 360);
            this.richMaster1.TabIndex = 2;
            this.richMaster1.Unit = DevExpress.Office.DocumentUnit.Centimeter;
            // 
            // tabDetail1
            // 
            this.tabDetail1.Controls.Add(this.richDetail1);
            this.tabDetail1.Name = "tabDetail1";
            this.tabDetail1.Size = new Size(1038, 360);
            this.tabDetail1.Text = "Danh sách kèm theo 1";
            // 
            // richDetail1
            // 
            this.richDetail1.Dock = DockStyle.Fill;
            this.richDetail1.Location = new Point(0, 0);
            this.richDetail1.LookAndFeel.SkinName = "Office 2007 Blue";
            this.richDetail1.MenuManager = this.ribbonControl1;
            this.richDetail1.Name = "richDetail1";
            this.richDetail1.Options.Fields.UseCurrentCultureDateTimeFormat = false;
            this.richDetail1.Options.MailMerge.KeepLastParagraph = false;
            this.richDetail1.Size = new Size(1038, 360);
            this.richDetail1.TabIndex = 2;
            this.richDetail1.Unit = DevExpress.Office.DocumentUnit.Centimeter;
            // 
            // tabResult
            // 
            this.tabResult.Controls.Add(this.richResult);
            this.tabResult.Name = "tabResult";
            this.tabResult.Size = new Size(1038, 360);
            this.tabResult.Text = "Kết quả";
            // 
            // richEditBarController1
            // 
            this.richEditBarController1.BarItems.Add(this.fileNewItem1);
            this.richEditBarController1.BarItems.Add(this.fileOpenItem1);
            this.richEditBarController1.BarItems.Add(this.fileSaveItem1);
            this.richEditBarController1.BarItems.Add(this.fileSaveAsItem1);
            this.richEditBarController1.BarItems.Add(this.quickPrintItem1);
            this.richEditBarController1.BarItems.Add(this.printItem1);
            this.richEditBarController1.BarItems.Add(this.printPreviewItem1);
            this.richEditBarController1.BarItems.Add(this.undoItem1);
            this.richEditBarController1.BarItems.Add(this.redoItem1);
            this.richEditBarController1.BarItems.Add(this.pasteItem1);
            this.richEditBarController1.BarItems.Add(this.cutItem1);
            this.richEditBarController1.BarItems.Add(this.copyItem1);
            this.richEditBarController1.BarItems.Add(this.pasteSpecialItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontNameItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontSizeItem1);
            this.richEditBarController1.BarItems.Add(this.fontSizeIncreaseItem1);
            this.richEditBarController1.BarItems.Add(this.fontSizeDecreaseItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontBoldItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontItalicItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontUnderlineItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontDoubleUnderlineItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontStrikeoutItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontDoubleStrikeoutItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontSuperscriptItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFontSubscriptItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontColorItem1);
            this.richEditBarController1.BarItems.Add(this.changeFontBackColorItem1);
            this.richEditBarController1.BarItems.Add(this.changeTextCaseItem1);
            this.richEditBarController1.BarItems.Add(this.makeTextUpperCaseItem1);
            this.richEditBarController1.BarItems.Add(this.makeTextLowerCaseItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTextCaseItem1);
            this.richEditBarController1.BarItems.Add(this.clearFormattingItem1);
            this.richEditBarController1.BarItems.Add(this.toggleBulletedListItem1);
            this.richEditBarController1.BarItems.Add(this.toggleNumberingListItem1);
            this.richEditBarController1.BarItems.Add(this.toggleMultiLevelListItem1);
            this.richEditBarController1.BarItems.Add(this.decreaseIndentItem1);
            this.richEditBarController1.BarItems.Add(this.increaseIndentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentLeftItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentCenterItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentRightItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphAlignmentJustifyItem1);
            this.richEditBarController1.BarItems.Add(this.toggleShowWhitespaceItem1);
            this.richEditBarController1.BarItems.Add(this.changeParagraphLineSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.setSingleParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.setSesquialteralParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.setDoubleParagraphSpacingItem1);
            this.richEditBarController1.BarItems.Add(this.showLineSpacingFormItem1);
            this.richEditBarController1.BarItems.Add(this.addSpacingBeforeParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.removeSpacingBeforeParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.addSpacingAfterParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.removeSpacingAfterParagraphItem1);
            this.richEditBarController1.BarItems.Add(this.changeParagraphBackColorItem1);
            this.richEditBarController1.BarItems.Add(this.galleryChangeStyleItem1);
            this.richEditBarController1.BarItems.Add(this.findItem1);
            this.richEditBarController1.BarItems.Add(this.replaceItem1);
            this.richEditBarController1.BarItems.Add(this.insertPageBreakItem1);
            this.richEditBarController1.BarItems.Add(this.insertTableItem1);
            this.richEditBarController1.BarItems.Add(this.insertPictureItem1);
            this.richEditBarController1.BarItems.Add(this.insertFloatingPictureItem1);
            this.richEditBarController1.BarItems.Add(this.insertBookmarkItem1);
            this.richEditBarController1.BarItems.Add(this.insertHyperlinkItem1);
            this.richEditBarController1.BarItems.Add(this.editPageHeaderItem1);
            this.richEditBarController1.BarItems.Add(this.editPageFooterItem1);
            this.richEditBarController1.BarItems.Add(this.insertPageNumberItem1);
            this.richEditBarController1.BarItems.Add(this.insertPageCountItem1);
            this.richEditBarController1.BarItems.Add(this.insertTextBoxItem1);
            this.richEditBarController1.BarItems.Add(this.insertSymbolItem1);
            this.richEditBarController1.BarItems.Add(this.changeSectionPageMarginsItem1);
            this.richEditBarController1.BarItems.Add(this.setNormalSectionPageMarginsItem1);
            this.richEditBarController1.BarItems.Add(this.setNarrowSectionPageMarginsItem1);
            this.richEditBarController1.BarItems.Add(this.setModerateSectionPageMarginsItem1);
            this.richEditBarController1.BarItems.Add(this.setWideSectionPageMarginsItem1);
            this.richEditBarController1.BarItems.Add(this.showPageMarginsSetupFormItem1);
            this.richEditBarController1.BarItems.Add(this.changeSectionPageOrientationItem1);
            this.richEditBarController1.BarItems.Add(this.setPortraitPageOrientationItem1);
            this.richEditBarController1.BarItems.Add(this.setLandscapePageOrientationItem1);
            this.richEditBarController1.BarItems.Add(this.changeSectionPaperKindItem1);
            this.richEditBarController1.BarItems.Add(this.changeSectionColumnsItem1);
            this.richEditBarController1.BarItems.Add(this.setSectionOneColumnItem1);
            this.richEditBarController1.BarItems.Add(this.setSectionTwoColumnsItem1);
            this.richEditBarController1.BarItems.Add(this.setSectionThreeColumnsItem1);
            this.richEditBarController1.BarItems.Add(this.showColumnsSetupFormItem1);
            this.richEditBarController1.BarItems.Add(this.insertBreakItem1);
            this.richEditBarController1.BarItems.Add(this.insertPageBreakItem2);
            this.richEditBarController1.BarItems.Add(this.insertColumnBreakItem1);
            this.richEditBarController1.BarItems.Add(this.insertSectionBreakNextPageItem1);
            this.richEditBarController1.BarItems.Add(this.insertSectionBreakEvenPageItem1);
            this.richEditBarController1.BarItems.Add(this.insertSectionBreakOddPageItem1);
            this.richEditBarController1.BarItems.Add(this.changeSectionLineNumberingItem1);
            this.richEditBarController1.BarItems.Add(this.setSectionLineNumberingNoneItem1);
            this.richEditBarController1.BarItems.Add(this.setSectionLineNumberingContinuousItem1);
            this.richEditBarController1.BarItems.Add(this.setSectionLineNumberingRestartNewPageItem1);
            this.richEditBarController1.BarItems.Add(this.setSectionLineNumberingRestartNewSectionItem1);
            this.richEditBarController1.BarItems.Add(this.toggleParagraphSuppressLineNumbersItem1);
            this.richEditBarController1.BarItems.Add(this.showLineNumberingFormItem1);
            this.richEditBarController1.BarItems.Add(this.changePageColorItem1);
            this.richEditBarController1.BarItems.Add(this.insertTableOfContentsItem1);
            this.richEditBarController1.BarItems.Add(this.updateTableOfContentsItem1);
            this.richEditBarController1.BarItems.Add(this.addParagraphsToTableOfContentItem1);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem1);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem2);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem3);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem4);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem5);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem6);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem7);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem8);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem9);
            this.richEditBarController1.BarItems.Add(this.setParagraphHeadingLevelItem10);
            this.richEditBarController1.BarItems.Add(this.insertCaptionPlaceholderItem1);
            this.richEditBarController1.BarItems.Add(this.insertFiguresCaptionItems1);
            this.richEditBarController1.BarItems.Add(this.insertTablesCaptionItems1);
            this.richEditBarController1.BarItems.Add(this.insertEquationsCaptionItems1);
            this.richEditBarController1.BarItems.Add(this.insertTableOfFiguresPlaceholderItem1);
            this.richEditBarController1.BarItems.Add(this.insertTableOfFiguresItems1);
            this.richEditBarController1.BarItems.Add(this.insertTableOfTablesItems1);
            this.richEditBarController1.BarItems.Add(this.insertTableOfEquationsItems1);
            this.richEditBarController1.BarItems.Add(this.insertMergeFieldItem1);
            this.richEditBarController1.BarItems.Add(this.showAllFieldCodesItem1);
            this.richEditBarController1.BarItems.Add(this.showAllFieldResultsItem1);
            this.richEditBarController1.BarItems.Add(this.toggleViewMergedDataItem1);
            this.richEditBarController1.BarItems.Add(this.checkSpellingItem1);
            this.richEditBarController1.BarItems.Add(this.protectDocumentItem1);
            this.richEditBarController1.BarItems.Add(this.changeRangeEditingPermissionsItem1);
            this.richEditBarController1.BarItems.Add(this.unprotectDocumentItem1);
            this.richEditBarController1.BarItems.Add(this.switchToSimpleViewItem1);
            this.richEditBarController1.BarItems.Add(this.switchToDraftViewItem1);
            this.richEditBarController1.BarItems.Add(this.switchToPrintLayoutViewItem1);
            this.richEditBarController1.BarItems.Add(this.toggleShowHorizontalRulerItem1);
            this.richEditBarController1.BarItems.Add(this.toggleShowVerticalRulerItem1);
            this.richEditBarController1.BarItems.Add(this.zoomOutItem1);
            this.richEditBarController1.BarItems.Add(this.zoomInItem1);
            this.richEditBarController1.BarItems.Add(this.goToPageHeaderItem1);
            this.richEditBarController1.BarItems.Add(this.goToPageFooterItem1);
            this.richEditBarController1.BarItems.Add(this.goToNextHeaderFooterItem1);
            this.richEditBarController1.BarItems.Add(this.goToPreviousHeaderFooterItem1);
            this.richEditBarController1.BarItems.Add(this.toggleLinkToPreviousItem1);
            this.richEditBarController1.BarItems.Add(this.toggleDifferentFirstPageItem1);
            this.richEditBarController1.BarItems.Add(this.toggleDifferentOddAndEvenPagesItem1);
            this.richEditBarController1.BarItems.Add(this.closePageHeaderFooterItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFirstRowItem1);
            this.richEditBarController1.BarItems.Add(this.toggleLastRowItem1);
            this.richEditBarController1.BarItems.Add(this.toggleBandedRowsItem1);
            this.richEditBarController1.BarItems.Add(this.toggleFirstColumnItem1);
            this.richEditBarController1.BarItems.Add(this.toggleLastColumnItem1);
            this.richEditBarController1.BarItems.Add(this.toggleBandedColumnItem1);
            this.richEditBarController1.BarItems.Add(this.galleryChangeTableStyleItem1);
            this.richEditBarController1.BarItems.Add(this.changeTableBorderLineStyleItem1);
            this.richEditBarController1.BarItems.Add(this.changeTableBorderLineWeightItem1);
            this.richEditBarController1.BarItems.Add(this.changeTableBorderColorItem1);
            this.richEditBarController1.BarItems.Add(this.changeTableBordersItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsBottomBorderItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsTopBorderItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsLeftBorderItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsRightBorderItem1);
            this.richEditBarController1.BarItems.Add(this.resetTableCellsAllBordersItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsAllBordersItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsOutsideBorderItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsInsideBorderItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsInsideHorizontalBorderItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsInsideVerticalBorderItem1);
            this.richEditBarController1.BarItems.Add(this.toggleShowTableGridLinesItem1);
            this.richEditBarController1.BarItems.Add(this.changeTableCellsShadingItem1);
            this.richEditBarController1.BarItems.Add(this.selectTableElementsItem1);
            this.richEditBarController1.BarItems.Add(this.selectTableCellItem1);
            this.richEditBarController1.BarItems.Add(this.selectTableColumnItem1);
            this.richEditBarController1.BarItems.Add(this.selectTableRowItem1);
            this.richEditBarController1.BarItems.Add(this.selectTableItem1);
            this.richEditBarController1.BarItems.Add(this.showTablePropertiesFormItem1);
            this.richEditBarController1.BarItems.Add(this.deleteTableElementsItem1);
            this.richEditBarController1.BarItems.Add(this.showDeleteTableCellsFormItem1);
            this.richEditBarController1.BarItems.Add(this.deleteTableColumnsItem1);
            this.richEditBarController1.BarItems.Add(this.deleteTableRowsItem1);
            this.richEditBarController1.BarItems.Add(this.deleteTableItem1);
            this.richEditBarController1.BarItems.Add(this.insertTableRowAboveItem1);
            this.richEditBarController1.BarItems.Add(this.insertTableRowBelowItem1);
            this.richEditBarController1.BarItems.Add(this.insertTableColumnToLeftItem1);
            this.richEditBarController1.BarItems.Add(this.insertTableColumnToRightItem1);
            this.richEditBarController1.BarItems.Add(this.mergeTableCellsItem1);
            this.richEditBarController1.BarItems.Add(this.showSplitTableCellsForm1);
            this.richEditBarController1.BarItems.Add(this.splitTableItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableAutoFitItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableAutoFitContentsItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableAutoFitWindowItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableFixedColumnWidthItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsTopLeftAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsMiddleLeftAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsBottomLeftAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsTopCenterAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsMiddleCenterAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsBottomCenterAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsTopRightAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsMiddleRightAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.toggleTableCellsBottomRightAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.showTableOptionsFormItem1);
            this.richEditBarController1.BarItems.Add(this.changeFloatingObjectFillColorItem1);
            this.richEditBarController1.BarItems.Add(this.changeFloatingObjectOutlineColorItem1);
            this.richEditBarController1.BarItems.Add(this.changeFloatingObjectOutlineWeightItem1);
            this.richEditBarController1.BarItems.Add(this.changeFloatingObjectTextWrapTypeItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectSquareTextWrapTypeItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectTightTextWrapTypeItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectThroughTextWrapTypeItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectTopAndBottomTextWrapTypeItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectBehindTextWrapTypeItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectInFrontOfTextWrapTypeItem1);
            this.richEditBarController1.BarItems.Add(this.changeFloatingObjectAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectTopLeftAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectTopCenterAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectTopRightAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectMiddleLeftAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectMiddleCenterAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectMiddleRightAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectBottomLeftAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectBottomCenterAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.setFloatingObjectBottomRightAlignmentItem1);
            this.richEditBarController1.BarItems.Add(this.floatingObjectBringForwardSubItem1);
            this.richEditBarController1.BarItems.Add(this.floatingObjectBringForwardItem1);
            this.richEditBarController1.BarItems.Add(this.floatingObjectBringToFrontItem1);
            this.richEditBarController1.BarItems.Add(this.floatingObjectBringInFrontOfTextItem1);
            this.richEditBarController1.BarItems.Add(this.floatingObjectSendBackwardSubItem1);
            this.richEditBarController1.BarItems.Add(this.floatingObjectSendBackwardItem1);
            this.richEditBarController1.BarItems.Add(this.floatingObjectSendToBackItem1);
            this.richEditBarController1.BarItems.Add(this.floatingObjectSendBehindTextItem1);
            this.richEditBarController1.Control = this.richTemplate;
            // 
            // insertPageBreakItem2
            // 
            this.insertPageBreakItem2.Id = -1;
            this.insertPageBreakItem2.Name = "insertPageBreakItem2";
            // 
            // frmEditor
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            //this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1046, 563);
            this.Controls.Add(this.xtraTabControl);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.backstageViewControl1);
            this.Controls.Add(this.ribbonControl1);
            this.Name = "frmShowMailMerge";
            this.Ribbon = this.ribbonControl1;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.StatusBar = this.ribbonStatusBar1;
            this.Text = "Editor";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.frmShowMailMerge_Load);
            ((ISupportInitialize)(this.repositoryItemFontEdit1)).EndInit();
            ((ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit1)).EndInit();
            ((ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((ISupportInitialize)(this.applicationMenu1)).EndInit();
            ((ISupportInitialize)(this.repositoryItemZoomTrackBar1)).EndInit();
            ((ISupportInitialize)(this.repositoryItemFontEdit3)).EndInit();
            ((ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit3)).EndInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineStyle4)).EndInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineWeight4)).EndInit();
            ((ISupportInitialize)(this.repositoryItemFloatingObjectOutlineWeight3)).EndInit();
            ((ISupportInitialize)(this.lookupBieuMau)).EndInit();
            ((ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineStyle1)).EndInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineWeight1)).EndInit();
            ((ISupportInitialize)(this.repositoryItemRichEditStyleEdit1)).EndInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineStyle2)).EndInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineWeight2)).EndInit();
            ((ISupportInitialize)(this.repositoryItemFloatingObjectOutlineWeight1)).EndInit();
            ((ISupportInitialize)(this.repositoryItemFontEdit2)).EndInit();
            ((ISupportInitialize)(this.repositoryItemRichEditFontSizeEdit2)).EndInit();
            ((ISupportInitialize)(this.repositoryItemRichEditStyleEdit2)).EndInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineStyle3)).EndInit();
            ((ISupportInitialize)(this.repositoryItemBorderLineWeight3)).EndInit();
            ((ISupportInitialize)(this.repositoryItemFloatingObjectOutlineWeight2)).EndInit();
            this.backstageViewControl1.ResumeLayout(false);
            ((ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.tabDetail.ResumeLayout(false);
            this.tabTemplate.ResumeLayout(false);
            this.tabMaster.ResumeLayout(false);
            this.tabMaster1.ResumeLayout(false);
            this.tabDetail1.ResumeLayout(false);
            this.tabResult.ResumeLayout(false);
            ((ISupportInitialize)(this.richEditBarController1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraRichEdit.RichEditControl richTemplate;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage tabTemplate;
        private DevExpress.XtraTab.XtraTabPage tabResult;
        private DevExpress.XtraRichEdit.RichEditControl richResult;
        private DevExpress.XtraBars.BarButtonItem mergeToNewDocumentItem;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit1;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit repositoryItemRichEditFontSizeEdit1;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle repositoryItemBorderLineStyle1;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight repositoryItemBorderLineWeight1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemZoomTrackBar repositoryItemZoomTrackBar1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup2;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup3;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup4;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup5;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup6;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup7;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit repositoryItemRichEditStyleEdit1;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle repositoryItemBorderLineStyle2;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight repositoryItemBorderLineWeight2;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemFloatingObjectOutlineWeight repositoryItemFloatingObjectOutlineWeight1;
        private DevExpress.XtraRichEdit.UI.FileRibbonPage fileRibbonPage1;
        private DevExpress.XtraRichEdit.UI.CommonRibbonPageGroup commonRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.HomeRibbonPage homeRibbonPage1;
        private DevExpress.XtraRichEdit.UI.ClipboardRibbonPageGroup clipboardRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.FontRibbonPageGroup fontRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.ParagraphRibbonPageGroup paragraphRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.StylesRibbonPageGroup stylesRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.EditingRibbonPageGroup editingRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.InsertRibbonPage insertRibbonPage1;
        private DevExpress.XtraRichEdit.UI.PagesRibbonPageGroup pagesRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TablesRibbonPageGroup tablesRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.IllustrationsRibbonPageGroup illustrationsRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.LinksRibbonPageGroup linksRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.HeaderFooterRibbonPageGroup headerFooterRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TextRibbonPageGroup textRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.SymbolsRibbonPageGroup symbolsRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.PageLayoutRibbonPage pageLayoutRibbonPage1;
        private DevExpress.XtraRichEdit.UI.PageSetupRibbonPageGroup pageSetupRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.MailingsRibbonPage mailingsRibbonPage1;
        private DevExpress.XtraRichEdit.UI.MailMergeRibbonPageGroup mailMergeRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.ViewRibbonPage viewRibbonPage1;
        private DevExpress.XtraRichEdit.UI.DocumentViewsRibbonPageGroup documentViewsRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.ShowRibbonPageGroup showRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.ZoomRibbonPageGroup zoomRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.ReviewRibbonPage reviewRibbonPage1;
        private DevExpress.XtraRichEdit.UI.DocumentProofingRibbonPageGroup documentProofingRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.DocumentProtectionRibbonPageGroup documentProtectionRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.ReferencesRibbonPage referencesRibbonPage1;
        private DevExpress.XtraRichEdit.UI.TableOfContentsRibbonPageGroup tableOfContentsRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.CaptionsRibbonPageGroup captionsRibbonPageGroup1;
        private DevExpress.XtraTab.XtraTabPage tabDetail;
        private DevExpress.XtraRichEdit.RichEditControl richDetail;
        //private DevExpress.XtraBars.BarButtonItem btnMergeDocument;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup8;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit2;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit repositoryItemRichEditFontSizeEdit2;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup9;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup10;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup11;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup12;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup13;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup14;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditStyleEdit repositoryItemRichEditStyleEdit2;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle repositoryItemBorderLineStyle3;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight repositoryItemBorderLineWeight3;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemFloatingObjectOutlineWeight repositoryItemFloatingObjectOutlineWeight2;
        private DevExpress.XtraTab.XtraTabPage tabMaster;
        private DevExpress.XtraRichEdit.UI.FileNewItem fileNewItem1;
        private DevExpress.XtraRichEdit.UI.FileOpenItem fileOpenItem1;
        private DevExpress.XtraRichEdit.UI.FileSaveItem fileSaveItem1;
        private DevExpress.XtraRichEdit.UI.FileSaveAsItem fileSaveAsItem1;
        private DevExpress.XtraRichEdit.UI.QuickPrintItem quickPrintItem1;
        private DevExpress.XtraRichEdit.UI.PrintItem printItem1;
        private DevExpress.XtraRichEdit.UI.PrintPreviewItem printPreviewItem1;
        private DevExpress.XtraRichEdit.UI.UndoItem undoItem1;
        private DevExpress.XtraRichEdit.UI.RedoItem redoItem1;
        private DevExpress.XtraRichEdit.UI.PasteItem pasteItem1;
        private DevExpress.XtraRichEdit.UI.CutItem cutItem1;
        private DevExpress.XtraRichEdit.UI.CopyItem copyItem1;
        private DevExpress.XtraRichEdit.UI.PasteSpecialItem pasteSpecialItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup15;
        private DevExpress.XtraRichEdit.UI.ChangeFontNameItem changeFontNameItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemFontEdit repositoryItemFontEdit3;
        private DevExpress.XtraRichEdit.UI.ChangeFontSizeItem changeFontSizeItem1;
        private DevExpress.XtraRichEdit.Design.RepositoryItemRichEditFontSizeEdit repositoryItemRichEditFontSizeEdit3;
        private DevExpress.XtraRichEdit.UI.FontSizeIncreaseItem fontSizeIncreaseItem1;
        private DevExpress.XtraRichEdit.UI.FontSizeDecreaseItem fontSizeDecreaseItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup16;
        private DevExpress.XtraRichEdit.UI.ToggleFontBoldItem toggleFontBoldItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontItalicItem toggleFontItalicItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontUnderlineItem toggleFontUnderlineItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontDoubleUnderlineItem toggleFontDoubleUnderlineItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontStrikeoutItem toggleFontStrikeoutItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontDoubleStrikeoutItem toggleFontDoubleStrikeoutItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontSuperscriptItem toggleFontSuperscriptItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFontSubscriptItem toggleFontSubscriptItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup17;
        private DevExpress.XtraRichEdit.UI.ChangeFontColorItem changeFontColorItem1;
        private DevExpress.XtraRichEdit.UI.ChangeFontBackColorItem changeFontBackColorItem1;
        private DevExpress.XtraRichEdit.UI.ChangeTextCaseItem changeTextCaseItem1;
        private DevExpress.XtraRichEdit.UI.MakeTextUpperCaseItem makeTextUpperCaseItem1;
        private DevExpress.XtraRichEdit.UI.MakeTextLowerCaseItem makeTextLowerCaseItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTextCaseItem toggleTextCaseItem1;
        private DevExpress.XtraRichEdit.UI.ClearFormattingItem clearFormattingItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup18;
        private DevExpress.XtraRichEdit.UI.ToggleBulletedListItem toggleBulletedListItem1;
        private DevExpress.XtraRichEdit.UI.ToggleNumberingListItem toggleNumberingListItem1;
        private DevExpress.XtraRichEdit.UI.ToggleMultiLevelListItem toggleMultiLevelListItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup19;
        private DevExpress.XtraRichEdit.UI.DecreaseIndentItem decreaseIndentItem1;
        private DevExpress.XtraRichEdit.UI.IncreaseIndentItem increaseIndentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleShowWhitespaceItem toggleShowWhitespaceItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup20;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentLeftItem toggleParagraphAlignmentLeftItem1;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentCenterItem toggleParagraphAlignmentCenterItem1;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentRightItem toggleParagraphAlignmentRightItem1;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphAlignmentJustifyItem toggleParagraphAlignmentJustifyItem1;
        private DevExpress.XtraBars.BarButtonGroup barButtonGroup21;
        private DevExpress.XtraRichEdit.UI.ChangeParagraphLineSpacingItem changeParagraphLineSpacingItem1;
        private DevExpress.XtraRichEdit.UI.SetSingleParagraphSpacingItem setSingleParagraphSpacingItem1;
        private DevExpress.XtraRichEdit.UI.SetSesquialteralParagraphSpacingItem setSesquialteralParagraphSpacingItem1;
        private DevExpress.XtraRichEdit.UI.SetDoubleParagraphSpacingItem setDoubleParagraphSpacingItem1;
        private DevExpress.XtraRichEdit.UI.ShowLineSpacingFormItem showLineSpacingFormItem1;
        private DevExpress.XtraRichEdit.UI.AddSpacingBeforeParagraphItem addSpacingBeforeParagraphItem1;
        private DevExpress.XtraRichEdit.UI.RemoveSpacingBeforeParagraphItem removeSpacingBeforeParagraphItem1;
        private DevExpress.XtraRichEdit.UI.AddSpacingAfterParagraphItem addSpacingAfterParagraphItem1;
        private DevExpress.XtraRichEdit.UI.RemoveSpacingAfterParagraphItem removeSpacingAfterParagraphItem1;
        private DevExpress.XtraRichEdit.UI.ChangeParagraphBackColorItem changeParagraphBackColorItem1;
        private DevExpress.XtraRichEdit.UI.GalleryChangeStyleItem galleryChangeStyleItem1;
        private DevExpress.XtraRichEdit.UI.FindItem findItem1;
        private DevExpress.XtraRichEdit.UI.ReplaceItem replaceItem1;
        private DevExpress.XtraRichEdit.UI.InsertPageBreakItem insertPageBreakItem1;
        private DevExpress.XtraRichEdit.UI.InsertTableItem insertTableItem1;
        private DevExpress.XtraRichEdit.UI.InsertPictureItem insertPictureItem1;
        private DevExpress.XtraRichEdit.UI.InsertFloatingPictureItem insertFloatingPictureItem1;
        private DevExpress.XtraRichEdit.UI.InsertBookmarkItem insertBookmarkItem1;
        private DevExpress.XtraRichEdit.UI.InsertHyperlinkItem insertHyperlinkItem1;
        private DevExpress.XtraRichEdit.UI.EditPageHeaderItem editPageHeaderItem1;
        private DevExpress.XtraRichEdit.UI.EditPageFooterItem editPageFooterItem1;
        private DevExpress.XtraRichEdit.UI.InsertPageNumberItem insertPageNumberItem1;
        private DevExpress.XtraRichEdit.UI.InsertPageCountItem insertPageCountItem1;
        private DevExpress.XtraRichEdit.UI.InsertTextBoxItem insertTextBoxItem1;
        private DevExpress.XtraRichEdit.UI.InsertSymbolItem insertSymbolItem1;
        private DevExpress.XtraRichEdit.UI.ChangeSectionPageMarginsItem changeSectionPageMarginsItem1;
        private DevExpress.XtraRichEdit.UI.SetNormalSectionPageMarginsItem setNormalSectionPageMarginsItem1;
        private DevExpress.XtraRichEdit.UI.SetNarrowSectionPageMarginsItem setNarrowSectionPageMarginsItem1;
        private DevExpress.XtraRichEdit.UI.SetModerateSectionPageMarginsItem setModerateSectionPageMarginsItem1;
        private DevExpress.XtraRichEdit.UI.SetWideSectionPageMarginsItem setWideSectionPageMarginsItem1;
        private DevExpress.XtraRichEdit.UI.ShowPageMarginsSetupFormItem showPageMarginsSetupFormItem1;
        private DevExpress.XtraRichEdit.UI.ChangeSectionPageOrientationItem changeSectionPageOrientationItem1;
        private DevExpress.XtraRichEdit.UI.SetPortraitPageOrientationItem setPortraitPageOrientationItem1;
        private DevExpress.XtraRichEdit.UI.SetLandscapePageOrientationItem setLandscapePageOrientationItem1;
        private DevExpress.XtraRichEdit.UI.ChangeSectionPaperKindItem changeSectionPaperKindItem1;
        private DevExpress.XtraRichEdit.UI.ChangeSectionColumnsItem changeSectionColumnsItem1;
        private DevExpress.XtraRichEdit.UI.SetSectionOneColumnItem setSectionOneColumnItem1;
        private DevExpress.XtraRichEdit.UI.SetSectionTwoColumnsItem setSectionTwoColumnsItem1;
        private DevExpress.XtraRichEdit.UI.SetSectionThreeColumnsItem setSectionThreeColumnsItem1;
        private DevExpress.XtraRichEdit.UI.ShowColumnsSetupFormItem showColumnsSetupFormItem1;
        private DevExpress.XtraRichEdit.UI.InsertBreakItem insertBreakItem1;
        private DevExpress.XtraRichEdit.UI.InsertColumnBreakItem insertColumnBreakItem1;
        private DevExpress.XtraRichEdit.UI.InsertSectionBreakNextPageItem insertSectionBreakNextPageItem1;
        private DevExpress.XtraRichEdit.UI.InsertSectionBreakEvenPageItem insertSectionBreakEvenPageItem1;
        private DevExpress.XtraRichEdit.UI.InsertSectionBreakOddPageItem insertSectionBreakOddPageItem1;
        private DevExpress.XtraRichEdit.UI.ChangeSectionLineNumberingItem changeSectionLineNumberingItem1;
        private DevExpress.XtraRichEdit.UI.SetSectionLineNumberingNoneItem setSectionLineNumberingNoneItem1;
        private DevExpress.XtraRichEdit.UI.SetSectionLineNumberingContinuousItem setSectionLineNumberingContinuousItem1;
        private DevExpress.XtraRichEdit.UI.SetSectionLineNumberingRestartNewPageItem setSectionLineNumberingRestartNewPageItem1;
        private DevExpress.XtraRichEdit.UI.SetSectionLineNumberingRestartNewSectionItem setSectionLineNumberingRestartNewSectionItem1;
        private DevExpress.XtraRichEdit.UI.ToggleParagraphSuppressLineNumbersItem toggleParagraphSuppressLineNumbersItem1;
        private DevExpress.XtraRichEdit.UI.ShowLineNumberingFormItem showLineNumberingFormItem1;
        private DevExpress.XtraRichEdit.UI.ChangePageColorItem changePageColorItem1;
        private DevExpress.XtraRichEdit.UI.InsertTableOfContentsItem insertTableOfContentsItem1;
        private DevExpress.XtraRichEdit.UI.UpdateTableOfContentsItem updateTableOfContentsItem1;
        private DevExpress.XtraRichEdit.UI.AddParagraphsToTableOfContentItem addParagraphsToTableOfContentItem1;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem1;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem2;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem3;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem4;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem5;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem6;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem7;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem8;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem9;
        private DevExpress.XtraRichEdit.UI.SetParagraphHeadingLevelItem setParagraphHeadingLevelItem10;
        private DevExpress.XtraRichEdit.UI.InsertCaptionPlaceholderItem insertCaptionPlaceholderItem1;
        private DevExpress.XtraRichEdit.UI.InsertFiguresCaptionItems insertFiguresCaptionItems1;
        private DevExpress.XtraRichEdit.UI.InsertTablesCaptionItems insertTablesCaptionItems1;
        private DevExpress.XtraRichEdit.UI.InsertEquationsCaptionItems insertEquationsCaptionItems1;
        private DevExpress.XtraRichEdit.UI.InsertTableOfFiguresPlaceholderItem insertTableOfFiguresPlaceholderItem1;
        private DevExpress.XtraRichEdit.UI.InsertTableOfFiguresItems insertTableOfFiguresItems1;
        private DevExpress.XtraRichEdit.UI.InsertTableOfTablesItems insertTableOfTablesItems1;
        private DevExpress.XtraRichEdit.UI.InsertTableOfEquationsItems insertTableOfEquationsItems1;
        private DevExpress.XtraRichEdit.UI.InsertMergeFieldItem insertMergeFieldItem1;
        private DevExpress.XtraRichEdit.UI.ShowAllFieldCodesItem showAllFieldCodesItem1;
        private DevExpress.XtraRichEdit.UI.ShowAllFieldResultsItem showAllFieldResultsItem1;
        private DevExpress.XtraRichEdit.UI.ToggleViewMergedDataItem toggleViewMergedDataItem1;
        private DevExpress.XtraRichEdit.UI.CheckSpellingItem checkSpellingItem1;
        private DevExpress.XtraRichEdit.UI.ProtectDocumentItem protectDocumentItem1;
        private DevExpress.XtraRichEdit.UI.ChangeRangeEditingPermissionsItem changeRangeEditingPermissionsItem1;
        private DevExpress.XtraRichEdit.UI.UnprotectDocumentItem unprotectDocumentItem1;
        private DevExpress.XtraRichEdit.UI.SwitchToSimpleViewItem switchToSimpleViewItem1;
        private DevExpress.XtraRichEdit.UI.SwitchToDraftViewItem switchToDraftViewItem1;
        private DevExpress.XtraRichEdit.UI.SwitchToPrintLayoutViewItem switchToPrintLayoutViewItem1;
        private DevExpress.XtraRichEdit.UI.ToggleShowHorizontalRulerItem toggleShowHorizontalRulerItem1;
        private DevExpress.XtraRichEdit.UI.ToggleShowVerticalRulerItem toggleShowVerticalRulerItem1;
        private DevExpress.XtraRichEdit.UI.ZoomOutItem zoomOutItem1;
        private DevExpress.XtraRichEdit.UI.ZoomInItem zoomInItem1;
        private DevExpress.XtraRichEdit.UI.GoToPageHeaderItem goToPageHeaderItem1;
        private DevExpress.XtraRichEdit.UI.GoToPageFooterItem goToPageFooterItem1;
        private DevExpress.XtraRichEdit.UI.GoToNextHeaderFooterItem goToNextHeaderFooterItem1;
        private DevExpress.XtraRichEdit.UI.GoToPreviousHeaderFooterItem goToPreviousHeaderFooterItem1;
        private DevExpress.XtraRichEdit.UI.ToggleLinkToPreviousItem toggleLinkToPreviousItem1;
        private DevExpress.XtraRichEdit.UI.ToggleDifferentFirstPageItem toggleDifferentFirstPageItem1;
        private DevExpress.XtraRichEdit.UI.ToggleDifferentOddAndEvenPagesItem toggleDifferentOddAndEvenPagesItem1;
        private DevExpress.XtraRichEdit.UI.ClosePageHeaderFooterItem closePageHeaderFooterItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFirstRowItem toggleFirstRowItem1;
        private DevExpress.XtraRichEdit.UI.ToggleLastRowItem toggleLastRowItem1;
        private DevExpress.XtraRichEdit.UI.ToggleBandedRowsItem toggleBandedRowsItem1;
        private DevExpress.XtraRichEdit.UI.ToggleFirstColumnItem toggleFirstColumnItem1;
        private DevExpress.XtraRichEdit.UI.ToggleLastColumnItem toggleLastColumnItem1;
        private DevExpress.XtraRichEdit.UI.ToggleBandedColumnItem toggleBandedColumnItem1;
        private DevExpress.XtraRichEdit.UI.GalleryChangeTableStyleItem galleryChangeTableStyleItem1;
        private DevExpress.XtraRichEdit.UI.ChangeTableBorderLineStyleItem changeTableBorderLineStyleItem1;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineStyle repositoryItemBorderLineStyle4;
        private DevExpress.XtraRichEdit.UI.ChangeTableBorderLineWeightItem changeTableBorderLineWeightItem1;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemBorderLineWeight repositoryItemBorderLineWeight4;
        private DevExpress.XtraRichEdit.UI.ChangeTableBorderColorItem changeTableBorderColorItem1;
        private DevExpress.XtraRichEdit.UI.ChangeTableBordersItem changeTableBordersItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomBorderItem toggleTableCellsBottomBorderItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsTopBorderItem toggleTableCellsTopBorderItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsLeftBorderItem toggleTableCellsLeftBorderItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsRightBorderItem toggleTableCellsRightBorderItem1;
        private DevExpress.XtraRichEdit.UI.ResetTableCellsAllBordersItem resetTableCellsAllBordersItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsAllBordersItem toggleTableCellsAllBordersItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsOutsideBorderItem toggleTableCellsOutsideBorderItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideBorderItem toggleTableCellsInsideBorderItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideHorizontalBorderItem toggleTableCellsInsideHorizontalBorderItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsInsideVerticalBorderItem toggleTableCellsInsideVerticalBorderItem1;
        private DevExpress.XtraRichEdit.UI.ToggleShowTableGridLinesItem toggleShowTableGridLinesItem1;
        private DevExpress.XtraRichEdit.UI.ChangeTableCellsShadingItem changeTableCellsShadingItem1;
        private DevExpress.XtraRichEdit.UI.SelectTableElementsItem selectTableElementsItem1;
        private DevExpress.XtraRichEdit.UI.SelectTableCellItem selectTableCellItem1;
        private DevExpress.XtraRichEdit.UI.SelectTableColumnItem selectTableColumnItem1;
        private DevExpress.XtraRichEdit.UI.SelectTableRowItem selectTableRowItem1;
        private DevExpress.XtraRichEdit.UI.SelectTableItem selectTableItem1;
        private DevExpress.XtraRichEdit.UI.ShowTablePropertiesFormItem showTablePropertiesFormItem1;
        private DevExpress.XtraRichEdit.UI.DeleteTableElementsItem deleteTableElementsItem1;
        private DevExpress.XtraRichEdit.UI.ShowDeleteTableCellsFormItem showDeleteTableCellsFormItem1;
        private DevExpress.XtraRichEdit.UI.DeleteTableColumnsItem deleteTableColumnsItem1;
        private DevExpress.XtraRichEdit.UI.DeleteTableRowsItem deleteTableRowsItem1;
        private DevExpress.XtraRichEdit.UI.DeleteTableItem deleteTableItem1;
        private DevExpress.XtraRichEdit.UI.InsertTableRowAboveItem insertTableRowAboveItem1;
        private DevExpress.XtraRichEdit.UI.InsertTableRowBelowItem insertTableRowBelowItem1;
        private DevExpress.XtraRichEdit.UI.InsertTableColumnToLeftItem insertTableColumnToLeftItem1;
        private DevExpress.XtraRichEdit.UI.InsertTableColumnToRightItem insertTableColumnToRightItem1;
        private DevExpress.XtraRichEdit.UI.MergeTableCellsItem mergeTableCellsItem1;
        private DevExpress.XtraRichEdit.UI.ShowSplitTableCellsForm showSplitTableCellsForm1;
        private DevExpress.XtraRichEdit.UI.SplitTableItem splitTableItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableAutoFitItem toggleTableAutoFitItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableAutoFitContentsItem toggleTableAutoFitContentsItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableAutoFitWindowItem toggleTableAutoFitWindowItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableFixedColumnWidthItem toggleTableFixedColumnWidthItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsTopLeftAlignmentItem toggleTableCellsTopLeftAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleLeftAlignmentItem toggleTableCellsMiddleLeftAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomLeftAlignmentItem toggleTableCellsBottomLeftAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsTopCenterAlignmentItem toggleTableCellsTopCenterAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleCenterAlignmentItem toggleTableCellsMiddleCenterAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomCenterAlignmentItem toggleTableCellsBottomCenterAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsTopRightAlignmentItem toggleTableCellsTopRightAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsMiddleRightAlignmentItem toggleTableCellsMiddleRightAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ToggleTableCellsBottomRightAlignmentItem toggleTableCellsBottomRightAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.ShowTableOptionsFormItem showTableOptionsFormItem1;
        private DevExpress.XtraRichEdit.UI.ChangeFloatingObjectFillColorItem changeFloatingObjectFillColorItem1;
        private DevExpress.XtraRichEdit.UI.ChangeFloatingObjectOutlineColorItem changeFloatingObjectOutlineColorItem1;
        private DevExpress.XtraRichEdit.UI.ChangeFloatingObjectOutlineWeightItem changeFloatingObjectOutlineWeightItem1;
        private DevExpress.XtraRichEdit.Forms.Design.RepositoryItemFloatingObjectOutlineWeight repositoryItemFloatingObjectOutlineWeight3;
        private DevExpress.XtraRichEdit.UI.ChangeFloatingObjectTextWrapTypeItem changeFloatingObjectTextWrapTypeItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectSquareTextWrapTypeItem setFloatingObjectSquareTextWrapTypeItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectTightTextWrapTypeItem setFloatingObjectTightTextWrapTypeItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectThroughTextWrapTypeItem setFloatingObjectThroughTextWrapTypeItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectTopAndBottomTextWrapTypeItem setFloatingObjectTopAndBottomTextWrapTypeItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectBehindTextWrapTypeItem setFloatingObjectBehindTextWrapTypeItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectInFrontOfTextWrapTypeItem setFloatingObjectInFrontOfTextWrapTypeItem1;
        private DevExpress.XtraRichEdit.UI.ChangeFloatingObjectAlignmentItem changeFloatingObjectAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectTopLeftAlignmentItem setFloatingObjectTopLeftAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectTopCenterAlignmentItem setFloatingObjectTopCenterAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectTopRightAlignmentItem setFloatingObjectTopRightAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleLeftAlignmentItem setFloatingObjectMiddleLeftAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleCenterAlignmentItem setFloatingObjectMiddleCenterAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectMiddleRightAlignmentItem setFloatingObjectMiddleRightAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomLeftAlignmentItem setFloatingObjectBottomLeftAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomCenterAlignmentItem setFloatingObjectBottomCenterAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.SetFloatingObjectBottomRightAlignmentItem setFloatingObjectBottomRightAlignmentItem1;
        private DevExpress.XtraRichEdit.UI.FloatingObjectBringForwardSubItem floatingObjectBringForwardSubItem1;
        private DevExpress.XtraRichEdit.UI.FloatingObjectBringForwardItem floatingObjectBringForwardItem1;
        private DevExpress.XtraRichEdit.UI.FloatingObjectBringToFrontItem floatingObjectBringToFrontItem1;
        private DevExpress.XtraRichEdit.UI.FloatingObjectBringInFrontOfTextItem floatingObjectBringInFrontOfTextItem1;
        private DevExpress.XtraRichEdit.UI.FloatingObjectSendBackwardSubItem floatingObjectSendBackwardSubItem1;
        private DevExpress.XtraRichEdit.UI.FloatingObjectSendBackwardItem floatingObjectSendBackwardItem1;
        private DevExpress.XtraRichEdit.UI.FloatingObjectSendToBackItem floatingObjectSendToBackItem1;
        private DevExpress.XtraRichEdit.UI.FloatingObjectSendBehindTextItem floatingObjectSendBehindTextItem1;
        private DevExpress.XtraRichEdit.UI.HeaderFooterToolsRibbonPageCategory headerFooterToolsRibbonPageCategory1;
        private DevExpress.XtraRichEdit.UI.HeaderFooterToolsDesignRibbonPage headerFooterToolsDesignRibbonPage1;
        private DevExpress.XtraRichEdit.UI.HeaderFooterToolsDesignNavigationRibbonPageGroup headerFooterToolsDesignNavigationRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.HeaderFooterToolsDesignOptionsRibbonPageGroup headerFooterToolsDesignOptionsRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.HeaderFooterToolsDesignCloseRibbonPageGroup headerFooterToolsDesignCloseRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TableToolsRibbonPageCategory tableToolsRibbonPageCategory1;
        private DevExpress.XtraRichEdit.UI.TableDesignRibbonPage tableDesignRibbonPage1;
        private DevExpress.XtraRichEdit.UI.TableStyleOptionsRibbonPageGroup tableStyleOptionsRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TableStylesRibbonPageGroup tableStylesRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TableDrawBordersRibbonPageGroup tableDrawBordersRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TableLayoutRibbonPage tableLayoutRibbonPage1;
        private DevExpress.XtraRichEdit.UI.TableTableRibbonPageGroup tableTableRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TableRowsAndColumnsRibbonPageGroup tableRowsAndColumnsRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TableMergeRibbonPageGroup tableMergeRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TableCellSizeRibbonPageGroup tableCellSizeRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.TableAlignmentRibbonPageGroup tableAlignmentRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.FloatingPictureToolsRibbonPageCategory floatingPictureToolsRibbonPageCategory1;
        private DevExpress.XtraRichEdit.UI.FloatingPictureToolsFormatPage floatingPictureToolsFormatPage1;
        private DevExpress.XtraRichEdit.UI.FloatingPictureToolsShapeStylesPageGroup floatingPictureToolsShapeStylesPageGroup1;
        private DevExpress.XtraRichEdit.UI.FloatingPictureToolsArrangePageGroup floatingPictureToolsArrangePageGroup1;
        private DevExpress.XtraRichEdit.UI.HomeRibbonPage homeRibbonPage2;
        private DevExpress.XtraRichEdit.UI.ClipboardRibbonPageGroup clipboardRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.FontRibbonPageGroup fontRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.ParagraphRibbonPageGroup paragraphRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.StylesRibbonPageGroup stylesRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.EditingRibbonPageGroup editingRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.InsertRibbonPage insertRibbonPage2;
        private DevExpress.XtraRichEdit.UI.PagesRibbonPageGroup pagesRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.TablesRibbonPageGroup tablesRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.IllustrationsRibbonPageGroup illustrationsRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.LinksRibbonPageGroup linksRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.HeaderFooterRibbonPageGroup headerFooterRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.TextRibbonPageGroup textRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.SymbolsRibbonPageGroup symbolsRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.PageLayoutRibbonPage pageLayoutRibbonPage2;
        private DevExpress.XtraRichEdit.UI.PageSetupRibbonPageGroup pageSetupRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.PageBackgroundRibbonPageGroup pageBackgroundRibbonPageGroup1;
        private DevExpress.XtraRichEdit.UI.ReferencesRibbonPage referencesRibbonPage2;
        private DevExpress.XtraRichEdit.UI.TableOfContentsRibbonPageGroup tableOfContentsRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.CaptionsRibbonPageGroup captionsRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.MailingsRibbonPage mailingsRibbonPage2;
        private DevExpress.XtraRichEdit.UI.MailMergeRibbonPageGroup mailMergeRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.ReviewRibbonPage reviewRibbonPage2;
        private DevExpress.XtraRichEdit.UI.DocumentProofingRibbonPageGroup documentProofingRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.DocumentProtectionRibbonPageGroup documentProtectionRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.ViewRibbonPage viewRibbonPage2;
        private DevExpress.XtraRichEdit.UI.DocumentViewsRibbonPageGroup documentViewsRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.ShowRibbonPageGroup showRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.ZoomRibbonPageGroup zoomRibbonPageGroup2;
        private DevExpress.XtraRichEdit.UI.RichEditBarController richEditBarController1;
        private DevExpress.XtraRichEdit.UI.InsertPageBreakItem insertPageBreakItem2;
        private DevExpress.XtraRichEdit.RichEditControl richMaster;
        private DevExpress.XtraBars.BarButtonItem btnMergeDocuments;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.BackstageViewControl backstageViewControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewClientControl backstageViewClientControl1;
        private DevExpress.XtraBars.Ribbon.BackstageViewTabItem backstageViewTabItem1;
        private DevExpress.XtraBars.BarButtonItem btnSave;
        private DevExpress.XtraBars.BarButtonItem btnInsertMergeField;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.ApplicationMenu applicationMenu1;
        private DevExpress.XtraTab.XtraTabPage tabDetail1;
        private DevExpress.XtraRichEdit.RichEditControl richDetail1;
        private DevExpress.XtraTab.XtraTabPage tabMaster1;
        private DevExpress.XtraRichEdit.RichEditControl richMaster1;
        private DevExpress.XtraBars.BarEditItem barBieuMau;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit lookupBieuMau;
        private DevExpress.XtraBars.BarEditItem barDonViTinh;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
    }
}