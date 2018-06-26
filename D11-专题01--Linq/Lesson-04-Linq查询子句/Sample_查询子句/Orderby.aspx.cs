using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Collections.Generic;

public partial class Orderby : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		//OrderQuery();
		OrderOtherQuery();
    }

	private void OrderQuery()
	{   ///构建数据源
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///根据用户的Username的值进行倒序排序
		var values = from u in users
					 where u.ID < 6
					 orderby u.Username descending
					 select u;
		///显示查询结果
		foreach (UserInfo u in values)
		{
			Response.Write(u.Username + "</br>");
		}
	}

	private void OrderOtherQuery()
	{   ///构建数据源
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///根据用户的Username的值进行倒序排序
		var values = from u in users
					 where u.ID < 6
					 orderby u.Username descending,u.ID ascending
					 select u;
		///显示查询结果
		foreach (UserInfo u in values)
		{
			Response.Write(u.Username+"..."+ u.ID.ToString() + "</br>");
		}
	}
}
