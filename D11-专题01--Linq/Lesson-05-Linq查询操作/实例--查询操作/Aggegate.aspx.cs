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

public partial class Aggegate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		CountQuery();
		SumQuery();
		MaxQuery();
		MinQuery();
		AggregateQuery();
		LongCountQuery();
    }

	private void CountQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///查询用户，并统计数量
		int count = users.Count(u => u.ID > 3 && u.ID < 8); 
		///显示查询结果
		Response.Write("count:" + count.ToString() + "</br>");		
	}

	private void SumQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///计算和
		int sum = users.Sum(u => u.ID);
		///显示查询结果
		Response.Write("sum:" + sum.ToString() + "</br>");
	}

	private void MaxQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///获取ID值的最大值
		int max = users.Max(u => u.ID);
		///显示查询结果
		Response.Write("max:" + max.ToString() + "</br>");
	}

	private void MinQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///获取ID值的最小值
		int min = users.Min(u => u.ID);
		///显示查询结果
		Response.Write("min:" + min.ToString() + "</br>");
	}

	private void AverageQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///获取ID值的平均值
		double average = users.Average(u => u.ID);
		///显示查询结果
		Response.Write("average:" + average.ToString() + "</br>");
	}

	private void AggregateQuery()
	{   ///构建数据源		
		int[] ints = new int[100];
		for(int i = 0; i < 100; i++)
		{
			ints[i] = i;
		}		
		///计算数据源的和
		int aggregate = ints.Aggregate((a,b) => a + b);
		///显示查询结果
		Response.Write("aggregate:" + aggregate.ToString() + "</br>");
	}

	private void LongCountQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 1000000; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///计算数据源中的元素的数量
		long count = users.LongCount(u => u.ID > 3);
		///显示查询结果
		Response.Write("longCount:" + count.ToString() + "</br>");
	}

	
}
