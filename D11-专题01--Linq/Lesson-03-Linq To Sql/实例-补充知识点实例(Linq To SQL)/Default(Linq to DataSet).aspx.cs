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
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.CacheControl = "no-cache";
        //
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string connection = "server=harry;database=Lesson;uid=sa;pwd=sa";
        string command = "Select * from JB_khxx";
        SqlDataAdapter adapter = new SqlDataAdapter(command, connection);
        DataSet ds = new DataSet();
        adapter.Fill(ds);

        var data = from k in ds.Tables[0].AsEnumerable()
                   select new
                   {
                       编号 = k.Field<string>("id"),
                       名称 = k.Field<string>("mingcheng"),
                       电话 = k.Field<string>("dianhua"),
                       传真 = k.Field<string>("chuanzhen"),
                       地址 = k.Field<string>("dizhi")
                   };
        GridView1.DataSource = data;
        GridView1.DataBind();
    }
}
