<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UC_PhanQuyenBaoCao.ascx.cs" Inherits="ERP.Web.Controls.UC_PhanQuyenBaoCao" %>
<%@ Register Assembly="DevExpress.Web.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxTreeList.v16.2, Version=16.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxTreeList" tagprefix="dx" %>


<dx:ASPxButton ID="btnLuu" runat="server" Height="27px" Text="Lưu" Width="109px" OnClick="btnLuu_Click" AutoPostBack="False">
</dx:ASPxButton>
</br>
<dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Tên nhóm quyền">
</dx:ASPxLabel>
<dx:ASPxTextBox ID="txtTen" runat="server" Width="170px">
</dx:ASPxTextBox>
</br>
<dx:ASPxTreeList ID="BaoCao_TreeList" runat="server" AutoGenerateColumns="False" KeyFieldName="OID">
    <Columns>
        <dx:TreeListTextColumn AllowSort="True" Caption="Tên nhóm báo cáo" FieldName="TenNhom" Name="colTenNhomBaoCao" SortIndex="1" SortOrder="Ascending" VisibleIndex="0" Width="300px">
        </dx:TreeListTextColumn>
    </Columns>
    <Settings GridLines="Both" ShowFooter="True" />
    <SettingsBehavior AutoExpandAllNodes="True" />
    <SettingsSelection AllowSelectAll="True" Enabled="True" />
</dx:ASPxTreeList>


