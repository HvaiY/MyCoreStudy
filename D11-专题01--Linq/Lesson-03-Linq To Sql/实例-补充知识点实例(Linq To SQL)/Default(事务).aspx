<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default(事务).aspx.cs" Inherits="Default_事务_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="隐式事务" onclick="Button1_Click" />
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="select" />
        <asp:GridView ID="GridView1" runat="server" Width="372px">
        </asp:GridView>
    
    </div>
    <div>
    
        <asp:Button ID="Button2" runat="server" Text="显示事务" onclick="Button2_Click" />
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="显示事务" />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:GridView ID="GridView2" runat="server" Width="372px">
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
