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

public partial class LinqXmlLoad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //取得虚拟目录路径
        string path = HttpContext.Current.Request.MapPath("~/App_Data/");
        //载入XML文件
        XElement xml = XElement.Load((path + "khxx.xml"));

        //用LINQ查询XML
        var emps = from x in xml.Elements()
                   select new
                   {
                       EmployeeID = (string)x.Element("id"),
                       LastName = (string)x.Element("mingcheng"),
                       FirstName = (string)x.Element("dianhua"),
                       City = (string)x.Element("chuanzhen"),
                       Address = (string)x.Element("dizhi"),
                   };       
        
        //绑定到GridView控件
        gvEmployees.DataSource = emps;
        gvEmployees.DataBind();
    }
}
