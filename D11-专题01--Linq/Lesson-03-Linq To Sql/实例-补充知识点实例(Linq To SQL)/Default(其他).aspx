<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default(其他).aspx.cs" Inherits="Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style type="text/css">
        .style1
        {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:76%;">
            <tr>
                <td class="style1">
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
                        Text="Contains" />
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Union" 
                        Width="61px" />
                    <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                        Text="group into" Width="84px" Height="21px" />
                    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="匿名类嵌套" />
                    <asp:Button ID="Button5" runat="server" onclick="Button5_Click" Text="内连接" />
                    <asp:Button ID="Button6" runat="server" onclick="Button6_Click" Text="外连接" />
                    <asp:Button ID="Button7" runat="server" onclick="Button7_Click" 
                        Text="GetTable" />
                    <asp:Button ID="Button8" runat="server" onclick="Button8_Click" 
                        Text="保存于ViewState" />
                    <asp:Button ID="Button9" runat="server" onclick="Button9_Click" 
                        Text="从ViewState获取" />
                </td>
                <td class="style1">
                    </td>
                <td class="style1">
                    </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="3">
    
        <asp:GridView ID="GridView1" runat="server" Height="127px" Width="782px" CellPadding="4" ForeColor="#333333" 
                        GridLines="None">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
        </asp:GridView>
    
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
