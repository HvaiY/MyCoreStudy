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

public partial class Let : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		LetQuery();
    }

	private void LetQuery()
	{   ///构建数据源
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///查询ID值小于9，且序号为偶数的用户
		var value = from u in users
					let number = Int32.Parse(u.Username.Substring(u.Username.Length - 1))
					where u.ID < 9 && number % 2 == 0
					select u;
		///显示查询结果
		foreach (var v in value)
		{
			Response.Write(v.Username + "</br>");
		}
	}

}
