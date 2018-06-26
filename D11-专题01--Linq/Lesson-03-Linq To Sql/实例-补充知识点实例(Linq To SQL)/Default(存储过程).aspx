<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default(存储过程).aspx.cs" Inherits="Default_存储过程_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
      
    <title>无标题页</title>
    <style type="text/css">
        .style1
        {
            width: 623px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="tb_ID" runat="server"></asp:TextBox>
        <asp:TextBox ID="tb_MingCheng" runat="server"></asp:TextBox>
        <asp:Button ID="Button3" runat="server" onclick="Button3_Click" Text="单表查询" />
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="多表" 
            Width="81px" />
        <table style="width:100%;">
            <tr>
                <td class="style1">
        <asp:GridView ID="GridView1" runat="server" Width="571px">
        </asp:GridView>
                </td>
                <td>
                    &nbsp;</td>
                <td>
        <asp:GridView ID="GridView2" runat="server" Width="491px">
        </asp:GridView>
    
                </td>
            </tr>
        </table>
        <table style="width:100%;">
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Add" 
                        Width="54px" />
                </td>
                <td>
                    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" Text="select" />
                </td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:GridView ID="GV" runat="server" AutoGenerateColumns="False" 
                        CellPadding="4" ForeColor="#333333" GridLines="None" Width="784px" 
                        DataKeyNames="id" onrowdeleting="GV_RowDeleting">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="id" HeaderText="编号" />
                            <asp:BoundField DataField="MingCheng" HeaderText="名称" />
                            <asp:BoundField DataField="DianHua" HeaderText="电话" />
                            <asp:BoundField DataField="ChuanZhen" HeaderText="传真" />
                            <asp:BoundField DataField="DiZhi" HeaderText="地址" />
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
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
