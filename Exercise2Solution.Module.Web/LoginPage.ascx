<%@ Control Language="C#" CodeBehind="LoginPage.ascx.cs" ClassName="LoginPage" Inherits="Exercise2Solution.Module.Web.LoginPage" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v16.2, Version=16.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates.ActionContainers"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v16.2, Version=16.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates.Controls"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v16.2, Version=16.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Controls"
    TagPrefix="xaf" %>
<%@ Register Assembly="DevExpress.ExpressApp.Web.v16.2, Version=16.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.ExpressApp.Web.Templates"
    TagPrefix="xaf" %>
<meta name="viewport" content="width=device-width, user-scalable=no, maximum-scale=1.0, minimum-scale=1.0">
<div class="LogonTemplate">
    <xaf:XafUpdatePanel ID="UPPopupWindowControl" runat="server">
        <xaf:XafPopupWindowControl runat="server" id="PopupWindowControl" />
    </xaf:XafUpdatePanel>
    <xaf:XafUpdatePanel ID="UPHeader" runat="server">
        <div class="white borderBottom width100" id="headerTableDiv">
            <div class="paddings sizeLimit" style="margin: auto">
                <table id="headerTable" class="headerTable xafAlignCenter white width100 sizeLimit" style="height: 60px;">
                    <tbody>
                        <tr>
                            <td>
                                <asp:HyperLink runat="server" NavigateUrl="#" ID="LogoLink">
                                    <xaf:ThemedImageControl id="TIC" defaultthemeimagelocation="Images" imagename="Logo.png" borderwidth="0px" runat="server" />
                                </asp:HyperLink>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </xaf:XafUpdatePanel>

    <div style="top: 25%; width: 100%; position: absolute">
        <table class="LogonMainTable LogonContentWidth">
            <tr>
                <td>
                    <xaf:XafUpdatePanel ID="UPEI" runat="server">
                        <xaf:ErrorInfoControl ID="ErrorInfo" Style="margin: 10px 0px 10px 0px" runat="server" />
                    </xaf:XafUpdatePanel>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="LogonContent LogonContentWidth">
                        <tr>
                            <td class="LogonContentCell">
                                <xaf:XafUpdatePanel ID="UPVSC" runat="server">
                                    <xaf:ViewSiteControl ID="viewSiteControl" runat="server" />
                                </xaf:XafUpdatePanel>

                                <xaf:XafUpdatePanel ID="UPPopupActions" runat="server" CssClass="right">
                                    <xaf:ActionContainerHolder ID="PopupActions" runat="server" Orientation="Horizontal" ContainerStyle="Buttons">
                                        <Menu width="100%" ItemAutoWidth="False" />
                                        <ActionContainers>
                        <xaf:WebActionContainer ContainerId="PopupActions" />
                    </ActionContainers>
                                    </xaf:ActionContainerHolder>
                                </xaf:XafUpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>