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

public partial class Where : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		WhereFilterQuery();
    }

	private void WhereFilterQuery()
	{   ///构建数据源
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///查询ID值小于3的用户
		var values = from u in users
					where u.ID < 3
					select u;
		var valuesOther = users.Where(u => u.Username.IndexOf("3") > -1);

		///显示查询结果
		foreach(var v in values)
		{
			Response.Write(v.Username + "</br>");
		}
		foreach(var v in valuesOther)
		{
			Response.Write(v.Username + "</br>");
		}
	}

}
