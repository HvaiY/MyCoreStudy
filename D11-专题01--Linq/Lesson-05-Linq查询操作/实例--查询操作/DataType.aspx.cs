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

public partial class DataType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		AsEnumerableQuery();
		AsQueryableQuery();
		CastQuery();
		OfTypeQuery();
		ToListQuery();
		ToArrayQuery();
		ToDictionaryQuery();
		ToLookupQuery();
	}

	private void AsEnumerableQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for(int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i % 2,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///使用AsEnumerable()转换数据类型
		var values = from u in users.AsEnumerable<UserInfo>()
					 where u.Username.IndexOf("05") > -1
					 select u;
		///显示查询结果
		Response.Write("AsEnumerable操作的结果：");
		foreach (var v in values)
		{
			Response.Write(v.Username + ",");
		}
		Response.Write("<br />");
	}

	private void AsQueryableQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i % 2,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///使用AsQueryable()转换数据类型
		var values = from u in users.AsQueryable<UserInfo>()
					 where u.Username.IndexOf("01") > -1
					 select u;
		///显示查询结果
		Response.Write("AsQueryable操作的结果：");
		foreach (var v in values)
		{
			Response.Write(v.Username + ",");
		}
		Response.Write("<br />");
	}

	private void CastQuery()
	{   ///构建数据源		
		ArrayList ints = new ArrayList();
		for (int i = 0; i < 100; i++)
		{
			ints.Add(i.ToString());
		}
		///使用Cast()转换数据类型
		var values = from i in ints.Cast<string>()
					 where i.IndexOf("2") > -1
					 select i;
		///显示查询结果
		Response.Write("Cast操作的结果：");
		foreach (var v in values)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
	}

	private void OfTypeQuery()
	{   ///构建数据源		
		ArrayList ints = new ArrayList();
		for(int i = 0; i < 100; i++)
		{
			ints.Add(i.ToString());
		}
		///使用OfType()转换数据类型
		var values = from i in ints.OfType<string>()
					 where i.IndexOf("0") > -1
					 select i;	
		///显示查询结果
		Response.Write("OfType操作的结果：");
		foreach(var v in values)
		{
			Response.Write(v + ",");
		}
		Response.Write("<br />");
	}

	private void ToListQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///转换为List<T>类型的数据
		var values = from u in users
					 where u.ID < 7
					 select u;
		List<UserInfo> listValues = values.ToList<UserInfo>();
		///显示查询结果
		Response.Write("ToList操作的结果：");
		foreach (var v in listValues)
		{
			Response.Write(v.Username + ",");
		}
		Response.Write("<br />");
	}

	private void ToArrayQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///转换为数组类型的数据
		var values = from u in users
					 where u.ID < 7
					 select u;
		UserInfo[] listValues = values.ToArray<UserInfo>();
		///显示查询结果
		Response.Write("ToArray操作的结果：");
		foreach (UserInfo v in listValues)
		{
			Response.Write(v.Username + ",");
		}
		Response.Write("<br />");
	}

	private void ToDictionaryQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///转换为Dictionary<T,T>类型的数据
		var values = from u in users
					 where u.ID < 7
					 select u;
		Dictionary<int,UserInfo> dictionaryValues = values.ToDictionary(u => u.ID);
		///显示查询结果
		Response.Write("ToDictionary操作的结果：");
		foreach (var v in dictionaryValues)
		{
			Response.Write(v.Value.Username + ",");
		}
		Response.Write("<br />");
	}

	private void ToLookupQuery()
	{   ///构建数据源		
		List<UserInfo> users = new List<UserInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com"));
		}
		///转换为Lookup<T,T>类型的数据
		var values = from u in users
					 where u.ID < 7
					 select u;
		ILookup<int,UserInfo> lookupValues = values.ToLookup(u => u.ID);
		///显示查询结果
		Response.Write("ToLookup操作的结果：");
		foreach (var lu in lookupValues)
		{
			foreach (var v in lu)
			{
				Response.Write(v.Username + ",");
			}
		}
		Response.Write("<br />");
	}
}
