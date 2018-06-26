using System;
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

using System.Collections;
using System.Collections.Generic;

public partial class From : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
		//SinlgeFromQuery();
		//ComplexFromQuery();

		MultiFromQuery();
    }

	private void SinlgeFromQuery()
	{   ///构建数据源
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i, "User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}		
		///查询ID值小于3的用户
		var value = from u in users
					where u.ID < 3
					select u;
		///显示查询结果
		foreach (var v in value)
		{
			Response.Write(v.Username + "</br>");
		}
	}

	private void ComplexFromQuery()
	{   ///构建数据源
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,
				"User0" + i.ToString(),
				"User0" + i.ToString() + "@web.com",
				new List<string>{"Alias0" + i.ToString()}
				));
		}
		///查询ID值小于3的用户，且别名包含字符串“1”
		var value = from u in users
					from an in u.AliasName
					where u.ID < 3 && an.ToString().IndexOf("1") > -1
					select u;
		///显示查询结果
		foreach (var v in value)
		{
			Response.Write(v.Username + "</br>");
		}
	}

	private void MultiFromQuery()
	{   ///构建数据源
		List<UserInfo> ausers = new List<UserInfo>();
		List<UserInfo> busers = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			ausers.Add(new UserInfo(i,"AUser0" + i.ToString(),"AUser0" + i.ToString() + "@web.com"));
			busers.Add(new UserInfo(i,"BUser0" + i.ToString(),"BUser0" + i.ToString() + "@web.com"));
		}
		///共包含两个查询，一个查询ID值小于3的用户，另外一个查询ID值大于5的用户
		var value = from au in ausers
					where au.ID < 3
					from bu in busers
					where bu.ID > 5
					select new {au.Username, bu.Email};
		///显示查询结果
		foreach (var v in value)
		{
			Response.Write(v.Username + " " + v.Email + "</br>");
		}
	}
}
