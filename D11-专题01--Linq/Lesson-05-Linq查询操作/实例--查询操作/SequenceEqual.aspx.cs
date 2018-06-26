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

public partial class SequenceEqual : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		SequenceEqualQuery();
    }

	private void SequenceEqualQuery()
	{   ///构建数据源		
		int[] ints = { 0,1,2,3,4,5,6,7,8,9 };
		int[] intsOther = { 9,8,7,6,5,4,3,2,1,0 };
		///判断两个序列是否相等
		bool equal = ints.SequenceEqual(intsOther);
		///显示查询结果
		Response.Write("SequenceEqual:" + equal.ToString() + "</br>");	
	}
}
