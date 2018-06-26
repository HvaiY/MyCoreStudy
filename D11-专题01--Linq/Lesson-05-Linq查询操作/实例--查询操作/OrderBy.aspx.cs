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

public partial class OrderBy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		ReverseQuery();
    }

	private void OrderByQuery()
	{   ///构建数据源		
		int[] ints = { 1,4,2,5,3,6 };
		///查询数据，倒序排序
		var values = from i in ints
					 orderby i descending
					 select i;
		var valuesOther = ints.OrderBy(i => i);
		///显示查询结果
		foreach(var v in values)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
		foreach(var v in valuesOther)
		{
			Response.Write(v + ",");
		}
	}

	private void OrderByDescendingQuery()
	{   ///构建数据源		
		int[] ints = { 1,4,2,5,3,6 };
		///查询数据，倒序排序	
		var valuesOther = ints.OrderByDescending(i => i);
		///显示查询结果		
		Response.Write("<br />");
		foreach(var v in valuesOther)
		{
			Response.Write(v + ",");
		}
	}

	private void ThenByQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i % 2,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///查询ID值小于3的用户，升序排序
		var values = from u in users
					orderby u.ID,u.Username
					select u;		
		var valuesOther = users.OrderBy(u => u.ID).ThenBy(u => u.Username);
		///显示查询结果
		foreach(var v in values)
		{
			Response.Write("(" + v.ID.ToString() + "," + v.Username + ")</br>");
		}
		Response.Write("<br />");
		foreach(var v in valuesOther)
		{
			Response.Write("(" + v.ID.ToString() + "," + v.Username + ")</br>");
		}
	}

	private void ThenByDescendingQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i % 2,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///查询ID值小于3的用户，倒序排序
		var values = from u in users
					 orderby u.ID,u.Username descending
					 select u;
		var valuesOther = users.OrderBy(u => u.ID).ThenByDescending(u => u.Username);
		///显示查询结果
		foreach(var v in values)
		{
			Response.Write("(" + v.ID.ToString() + "," + v.Username + ")</br>");
		}
		Response.Write("<br />");
		foreach(var v in valuesOther)
		{
			Response.Write("(" + v.ID.ToString() + "," + v.Username + ")</br>");
		}
	}

	private void ReverseQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i % 2,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}		
		///反转序列
		users.Reverse();
		///显示查询结果
		foreach(var v in users)
		{
			Response.Write("(" + v.ID.ToString() + "," + v.Username + ")</br>");
		}		
	}
}
