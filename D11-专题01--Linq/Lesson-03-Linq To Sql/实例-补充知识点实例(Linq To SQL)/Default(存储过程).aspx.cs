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

public partial class Default_存储过程_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ///存储过程查询多表数据
        DataClassesDataContext db = new DataClassesDataContext();
        var results = db.sp_MultiResultSet();
        var khxxs = results.GetResult<JB_khxx>();
        var gysxxs = results.GetResult<JB_gysxx>();
        ///
        this.GridView1.DataSource = khxxs;
        this.GridView1.DataBind();
        this.GridView2.DataSource = gysxxs;
        this.GridView2.DataBind();
    }
    
    protected void Button3_Click(object sender, EventArgs e)
    {
        ///存储过程查询单表数据
        DataClassesDataContext db = new DataClassesDataContext();
        //var data = db.sp_JB_khxx1_GetList(tb_ID.Text + "%", tb_MingCheng.Text + "%");
        //
        var khxxs1 = from k in db.sp_JB_khxx1_GetList(tb_ID.Text + "%", tb_MingCheng.Text + "%")
                     select k;
        GridView1.DataSource = khxxs1;
        GridView1.DataBind();
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //添加
        DataClassesDataContext db = new DataClassesDataContext();           
        //
        db.UP_JB_khxx_ADD(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text);
        db.SubmitChanges();

        var data = from k in db.JB_khxx
                   select k;
        GV.DataSource = data;
        GV.DataBind();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        //查询
        DataClassesDataContext db = new DataClassesDataContext();
        var data = db.UP_JB_khxx_GetList();
        GV.DataSource = data;
        GV.DataBind();

    }
    protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //删除
        string sid = GV.DataKeys[e.RowIndex].Value.ToString();
        ///
        DataClassesDataContext db = new DataClassesDataContext();
        db.UP_JB_khxx_Delete(sid);
        var data = db.UP_JB_khxx_GetList();
        GV.DataSource = data;
        GV.DataBind();
    }
}
