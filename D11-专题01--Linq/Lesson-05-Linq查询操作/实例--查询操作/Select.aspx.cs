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

public partial class Select : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		SelectQuery();
		SelectManyQuery();
	}

	private void SelectQuery()
	{   ///构建数据源		
		int[] ints = new int[100];
		for(int i = 0; i < 100; i++)ints[i] = i;
		///查询数据
		var values = ints.Select(i => i % 5);
		///显示查询结果
		foreach(var v in values)
		{
			Response.Write(v + "</br>");
		}
	}

	private void SelectManyQuery()
	{   ///构建数据源		
		int[] intsOne = {1,2,3};
		int[] intsTwo = {4,5,6};
		///添加intsOne和intsTwo到list列表中
		List<int[]> list = new List<int[]>();
		list.Add(intsOne);
		list.Add(intsTwo);
		///查询两个序列中的所有元素
		var values = list.SelectMany(i => i);			
		///显示查询结果
		foreach(var v in values)
		{
			Response.Write(v + "</br>");
		}
	}
}
