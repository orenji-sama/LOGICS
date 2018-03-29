<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PopUP.aspx.cs" Inherits="WebApplication1.Properties.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           
            <asp:Panel ID="contentPanel" runat="server">
                <asp:HyperLink ID="HyperLink1" runat="server" Text="More ..." 
NavigateUrl="#" ></asp:HyperLink>

            </asp:Panel>
            <asp:Panel ID="popupPanel" runat="server" CssClass="popup">

                Login to get more information!
            </asp:Panel>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
PopupControlID="popupPanel" TargetControlID="HyperLink1" 
OkControlID="Button1" BackgroundCssClass="content" />



        </div>
    </form>
</body>
</html>
