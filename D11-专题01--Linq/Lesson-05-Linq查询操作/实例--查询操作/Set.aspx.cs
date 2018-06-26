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

public partial class Set : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		///DistinctQuery();
		///ExceptQuery();
		IntersectQuery();
		//UnionQuery();
    }

	private void DistinctQuery()
	{   ///构建数据源		
		List<string> dataSourceA = new List<string>();
		///初始化数据源A
		dataSourceA.Add("A");
		dataSourceA.Add("B");
		dataSourceA.Add("B");
		dataSourceA.Add("C");
		dataSourceA.Add("D");
		dataSourceA.Add("D");
		dataSourceA.Add("E");
		///执行Distinct操作
		var values = dataSourceA.Distinct();
		///显示查询结果
		OutputSetInfo(dataSourceA,"集合A");
		OutputSetInfo(values.ToList<string>(),"去掉重复元素之后的集合A");
		Response.Write("<br />");
	}

	private void ExceptQuery()
	{   ///构建数据源		
		List<string> dataSourceA = new List<string>();
		List<string> dataSourceB = new List<string>();
		///初始化数据源A
		dataSourceA.Add("A");
		dataSourceA.Add("B");
		dataSourceA.Add("B");
		dataSourceA.Add("C");
		dataSourceA.Add("D");
		dataSourceA.Add("D");
		dataSourceA.Add("E");
		///初始化数据源B
		dataSourceB.Add("A");
		dataSourceB.Add("C");
		dataSourceB.Add("C");
		dataSourceB.Add("F");
		dataSourceB.Add("H");
		///获取集合A和B的差集
		var values = dataSourceA.Except(dataSourceB);
		///显示查询结果
		OutputSetInfo(dataSourceA,"集合A");
		OutputSetInfo(dataSourceB,"集合B");
		OutputSetInfo(values.ToList<string>(),"A与B差集");
		Response.Write("<br />");
	}

	private void IntersectQuery()
	{   ///构建数据源		
		List<string> dataSourceA = new List<string>();
		List<string> dataSourceB = new List<string>();
		///初始化数据源A
		dataSourceA.Add("A");
		dataSourceA.Add("B");
		dataSourceA.Add("C");
		dataSourceA.Add("C");
		dataSourceA.Add("D");
		dataSourceA.Add("D");
		dataSourceA.Add("E");
		///初始化数据源B
		dataSourceB.Add("A");
		dataSourceB.Add("C");
		dataSourceB.Add("C");
		dataSourceB.Add("F");
		dataSourceB.Add("H");
		///获取集合A和B的交集
		var values = dataSourceA.Intersect(dataSourceB);
		///显示查询结果
		OutputSetInfo(dataSourceA,"集合A");
		OutputSetInfo(dataSourceB,"集合B");
		OutputSetInfo(values.ToList<string>(),"A与B交集");
		Response.Write("<br />");
	}

	private void UnionQuery()
	{   ///构建数据源		
		List<string> dataSourceA = new List<string>();
		List<string> dataSourceB = new List<string>();
		///初始化数据源A
		dataSourceA.Add("A");
		dataSourceA.Add("B");
		dataSourceA.Add("B");
		dataSourceA.Add("C");
		dataSourceA.Add("D");
		dataSourceA.Add("D");
		dataSourceA.Add("E");
		///初始化数据源B
		dataSourceB.Add("A");
		dataSourceB.Add("C");
		dataSourceB.Add("C");
		dataSourceB.Add("F");
		dataSourceB.Add("H");
		///获取集合A和B的并集
		var values = dataSourceA.Union(dataSourceB);
		///显示查询结果
		OutputSetInfo(dataSourceA,"集合A");
		OutputSetInfo(dataSourceB,"集合B");
		OutputSetInfo(values.ToList<string>(),"A与B并集");
		Response.Write("<br />");
	}

	private void OutputSetInfo(List<string> list,string name)
	{
		Response.Write(name + "={");
		for(int i = 0; i < list.Count; i++)
		{
			if (i < list.Count - 1) Response.Write(list[i] + ",");
			else Response.Write(list[i]);				
		}
		Response.Write("}<br />");
	}
}
