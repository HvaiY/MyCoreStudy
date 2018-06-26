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

public partial class Create : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		DefaultIfEmptyQuery();
		EmptyQuery();
		RangeQuery();
		RepeatQuery();
    }

	private void DefaultIfEmptyQuery()
	{   ///构建数据源		
		int[] ints = {0,1,2,3,4,5,6,7,8,9};
		int[] intsOther = { };
		///查询数据
		var values = ints.DefaultIfEmpty();
		var valuesOther = intsOther.DefaultIfEmpty(-1);
		///显示查询结果
		foreach (var v in values)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
		foreach (var v in valuesOther)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");		
	}

	private void EmptyQuery()
	{   ///查询数据，并返回一个空序列
		var values = Enumerable.Empty<UserInfo>();
		///显示查询结果
		foreach (var v in values)
		{
			Response.Write(v.Username + ",");
		}
		Response.Write("<br />");
	}

	private void RangeQuery()
	{   ///查询数据，并返回0～10范围内的数字序列
		var values = Enumerable.Range(0,10);
		///显示查询结果
		foreach (var v in values)
		{
			Response.Write(v.ToString() + ",");
		}
		Response.Write("<br />");
	}

	private void RepeatQuery()
	{   ///查询数据，并返回包含重复元素的序列
		var values = Enumerable.Repeat<int>(10,3);
		///显示查询结果
		foreach (var v in values)
		{
			Response.Write(v.ToString() + ",");
		}
		Response.Write("<br />");
	}
}
