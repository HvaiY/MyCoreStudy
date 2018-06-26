<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default(增删改).aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    </head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <div style="WIDTH: 100%; HEIGHT: 436px">
        <div style="WIDTH: 100%; HEIGHT: 51px">
            <table style="HEIGHT: 47%" width="100%">
                <tr>
                    <td align="right" style="WIDTH: 32%">
                        编号：</td>
                    <td style="WIDTH: 149px">
                        <asp:TextBox ID="tb_ID" runat="server" BorderStyle="Groove" Font-Size="9pt"></asp:TextBox>
                    </td>
                    <td align="right" style="WIDTH: 60px">
                        名称：</td>
                    <td>
                        <asp:TextBox ID="tb_MingCheng" runat="server" BorderStyle="Groove" 
                            Font-Size="9pt"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4" style="HEIGHT: 4px">
                        <asp:Button ID="Button3" runat="server" onclick="Button1_Click" Text="多条件模糊查询" 
                            Width="121px" />
                    </td>
                </tr>
            </table>
        </div>
        <hr />
        <table style="width:100%;">
            <tr>
                <td class="style1">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                    <asp:TextBox ID="TextBox5" runat="server" style="margin-right: 92px" 
                        Width="126px"></asp:TextBox>
                    <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
                        style="margin-left: 114px" Text="Add" Width="53px" />
                </td>
                <td class="style2">
                    <asp:Button ID="Button6" runat="server" onclick="Button6_Click" Text="Edit" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>   
        </table>

        <div style="WIDTH: 94%; HEIGHT: 72%">
            <table style="HEIGHT: 33px" width="100%">
                <tr>
                    <td>
                        信息列表</td>
                    <td align="right">
                        <asp:Button ID="BtnAdd" runat="server" onclick="Button3_Click" Text="新增" 
                            Width="53px" />
                        <asp:Button ID="BtnDel" runat="server" onclick="BtnDel_Click" 
                            OnClientClick="return GetConfirm(document.form1)" Text="删除" Width="46px" />
                    </td>
                </tr>
            </table>
            <asp:GridView ID="GV" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" CellPadding="4" CssClass="table" DataKeyNames="id" 
                ForeColor="#333333" GridLines="None" Height="10px" PageSize="5" 
                Width="100%" onrowdeleting="GV_RowDeleting" 
                onselectedindexchanged="GV_SelectedIndexChanged" onsorting="GV_Sorting">
                <PagerSettings FirstPageText="第一页" LastPageText="最后页" NextPageText="下一页" 
                    PreviousPageText="前一页" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#EFF3FB" />
                <Columns>
                    <asp:TemplateField HeaderText="选择">
                        <ItemTemplate>
                            <asp:CheckBox ID="select" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            
                        </HeaderTemplate>
                        <HeaderStyle Width="8%" />
                        <ItemStyle HorizontalAlign="Center" Width="8%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="id" HeaderText="编号" ReadOnly="True" 
                        SortExpression="id">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Mingcheng" HeaderText="单位名称" ReadOnly="True" 
                        SortExpression="MingCheng" />
                    <asp:BoundField DataField="Dianhua" HeaderText="联系电话" 
                        SortExpression="Dianhua" />
                    <asp:BoundField DataField="chuanzhen" HeaderText="传真" 
                        SortExpression="ChuanZhen" />
                    <asp:TemplateField HeaderText="地址" SortExpression="DiZhi">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="98px">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("DiZhi") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="id" 
                        DataNavigateUrlFormatString="khxx_child.aspx?id={0}" HeaderText="修改" 
                        Text="修改" />
                    <asp:TemplateField HeaderText="删除">
                        <ItemTemplate>
                            <asp:LinkButton ID="LBDel" runat="server" CausesValidation="False" 
                                CommandName="Delete" Text="删除"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
    </div>

    </form>
</body>
</html>
