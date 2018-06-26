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

public partial class Element : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		ElementAtQuery();
		ElementAtOrDefaultQuery();
		FirstQuery();
		FirstOrDefaultQuery();
		LastQuery();
		LastOrDefaultQuery();
		SingleQuery();
		SingleOrDefaultQuery();
	}

	private void ElementAtQuery()
	{   ///构建数据源		
		int[] ints = new int[100];
		for (int i = 0; i < 100; i++)ints[i] = i;
		///查询数据
		var values = from i in ints
					 where i % 5 == 0
					 select i;
		///获取指定位置的元素
		int value = values.ElementAt(10);
		///显示查询结果
		Response.Write("ElementAt 10:" + value.ToString() + "</br>");
	}

	private void ElementAtOrDefaultQuery()
	{   ///构建数据源		
		int[] ints = new int[100];
		for (int i = 0; i < 100; i++) ints[i] = i;
		///查询数据
		var values = from i in ints
					 where i % 5 == 0
					 select i;
		///获取指定位置的元素或默认值（指定位置的元素不存在）
		int value = values.ElementAtOrDefault(20);
		///显示查询结果
		Response.Write("ElementAtOrDefault 20:" + value.ToString() + "</br>");
	}

	private void FirstQuery()
	{   ///构建数据源		
		int[] ints = new int[100];
		for (int i = 0; i < 100; i++) ints[i] = i;
		///查询数据
		var values = from i in ints
					 where i % 5 == 0
					 select i;
		///获取第一个元素
		int value = values.First();
		///显示查询结果
		Response.Write("First:" + value.ToString() + "</br>");
	}

	private void FirstOrDefaultQuery()
	{   ///构建数据源		
		int[] ints = new int[100];
		for (int i = 0; i < 100; i++) ints[i] = i;
		///查询数据
		var values = from i in ints
					 where i % 5 == 0
					 select i;
		///获取第一个元素或默认值（第一个元素不存在）
		int value = values.FirstOrDefault();
		///显示查询结果
		Response.Write("FirstOrDefault:" + value.ToString() + "</br>");
	}

	private void LastQuery()
	{   ///构建数据源		
		int[] ints = new int[100];
		for (int i = 0; i < 100; i++) ints[i] = i;
		///查询数据
		var values = from i in ints
					 where i % 5 == 0
					 select i;
		///获取最后一个元素
		int value = values.Last();
		///显示查询结果
		Response.Write("Last:" + value.ToString() + "</br>");
	}

	private void LastOrDefaultQuery()
	{   ///构建数据源		
		int[] ints = new int[100];
		for (int i = 0; i < 100; i++) ints[i] = i;
		///查询数据
		var values = from i in ints
					 where i % 5 == 0
					 select i;
		///获取最后一个元素或默认值（最后一个元素不存在）
		int value = values.LastOrDefault();
		///显示查询结果
		Response.Write("LastOrDefault:" + value.ToString() + "</br>");
	}

	private void SingleQuery()
	{   ///构建数据源，只包含一个元素		
		int[] ints = { 100 };
		///查询数据
		var values = from i in ints
					 where i % 5 == 0
					 select i;
		///获取单个元素
		int value = values.Single();
		///显示查询结果
		Response.Write("Single:" + value.ToString() + "</br>");
	}

	private void SingleOrDefaultQuery()
	{   ///构建数据源，只包含一个元素	
		int[] ints = { 100 };		
		///查询数据
		var values = from i in ints
					 where i % 5 == 0
					 select i;
		///获取单个元素或默认值（单个元素不存在）
		int value = values.SingleOrDefault();
		///显示查询结果
		Response.Write("SingleOrDefault:" + value.ToString() + "</br>");
	}

}
