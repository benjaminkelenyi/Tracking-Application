<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ShowMarkesGPS.Main" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style3 {
            width: 254px;
        }
        .auto-style4 {
            width: 375px;
        }
        .auto-style6 {
            text-align: center;
            font-size: xx-large;
        }
        .auto-style7 {
            width: 309px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3 class="auto-style6">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Markers Show GPS</h3>
        <table class="auto-style1">
            <tr>
                <td class="auto-style4">
                    &nbsp;</td>
                <td class="auto-style7">
    <cc1:GMap ID="GMap1" runat="server" Width="800px" Height="500px"/>
                </td>
                <td class="auto-style3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style7">
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList2" runat="server">
                    </asp:DropDownList>
                    <asp:DropDownList ID="DropDownList3" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="Button1" runat="server" Text="ShowCoordinates" OnClick="Button1_Click" />
                </td>
                <td class="auto-style3">&nbsp;</td>
            </tr>
        </table>
        <script async defer src="https://maps.googleapis.com/maps/api/js?key=AIzaSyA0moCKlLJ1jiBOuf8c2u2ZjjYLJwBbVzA&callback=initMap"
  type="text/javascript"></script>
    </div>
        <p>
                    &nbsp;</p>
    </form>
</body>
</html>
