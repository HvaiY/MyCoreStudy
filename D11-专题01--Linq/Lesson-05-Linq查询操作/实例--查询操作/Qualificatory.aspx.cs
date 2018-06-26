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

public partial class Qualificatory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		AllQuery();
		AnyQuery();
		ContainsQuery();
    }

	private void AllQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///查询所有用户的ID值都大于3
		bool isAll = users.All(u => u.ID > 3);
		///显示查询结果
		Response.Write("All:" + isAll.ToString() + "</br>");
	}

	private void AnyQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///查询是否存在满足ID值大于3的用户
		bool isAny = users.Any(u => u.ID > 3);
		///显示查询结果
		Response.Write("Any:" + isAny.ToString() + "</br>");
	}

	private void ContainsQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///查询给定的元素是否存在
		bool isContains = users.Contains(new UserInfo(5,"User05","my@web.com"));
		///显示查询结果
		Response.Write("Contains:" + isContains.ToString() + "</br>");
	}
}
