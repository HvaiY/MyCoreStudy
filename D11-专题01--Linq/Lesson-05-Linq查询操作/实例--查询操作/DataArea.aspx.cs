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

public partial class DataArea : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		SkipQuery();
		SkipWhileQuery();
		TakeQuery();
		TakeWhileQuery();
    }

	private void SkipQuery()
	{   ///构建数据源		
		int[] ints = { 0,1,2,3,4,5,6,7,8,9 };		
		///查询数据
		var values = ints.Skip(5);
		var valuesOther = ints.Skip(20);
		///显示查询结果
		Response.Write("ints.Skip(5):");
		foreach (var v in values)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
		Response.Write("ints.Skip(20):");
		foreach (var v in valuesOther)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
	}

	private void SkipWhileQuery()
	{   ///构建数据源		
		int[] ints = { 0,1,2,3,4,5,6,7,8,9 };
		///查询数据
		var values = ints.SkipWhile((x,i) => i < 5);		
		///显示查询结果
		Response.Write("ints.SkipWhile((x,i) => i < 5):");
		foreach (var v in values)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");		
	}

	private void TakeQuery()
	{   ///构建数据源		
		int[] ints = { 0,1,2,3,4,5,6,7,8,9 };
		///查询数据
		var values = ints.Take(5);
		var valuesOther = ints.Take(20);
		///显示查询结果
		Response.Write("ints.Take(5):");
		foreach (var v in values)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
		Response.Write("ints.Take(20):");
		foreach (var v in valuesOther)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
	}

	private void TakeWhileQuery()
	{   ///构建数据源		
		int[] ints = { 0,1,2,3,4,5,6,7,8,9 };
		///查询数据
		var values = ints.TakeWhile((x,i) => i < 5);
		///显示查询结果
		Response.Write("ints.TakeWhile((x,i) => i < 5):");
		foreach (var v in values)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
	}
}
