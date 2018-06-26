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

public partial class Contact : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		ContactQuery();
	}

	private void ContactQuery()
	{   ///构建数据源		
		int[] ints = { 0,1,2,3,4,5,6,7,8,9 };
		int[] intsOther = { 9,8,7,6,5,4,3,2,1,0 };
		///串联两个序列
		var contactList = ints.Concat(intsOther);
		///显示串联之后的序列
		OutputSetInfo(ints.ToList(),"Ints");
		OutputSetInfo(intsOther.ToList(),"intsOther");
		OutputSetInfo(contactList.ToList(),"ContactList");
	}

	private void OutputSetInfo(List<int> list,string name)
	{
		Response.Write(name + "={");
		for (int i = 0; i < list.Count; i++)
		{
			if (i < list.Count - 1) Response.Write(list[i].ToString() + ",");
			else Response.Write(list[i].ToString());
		}
		Response.Write("}<br />");
	}
}
