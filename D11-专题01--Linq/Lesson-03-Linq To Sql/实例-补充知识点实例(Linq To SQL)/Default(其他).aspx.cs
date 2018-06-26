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

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        var khxxs1 = from k in db.JB_khxx
                     where k.mingcheng.Contains("")
                     select k;
        List<JB_khxx> lst = khxxs1.ToList<JB_khxx>();
        this.GridView1.DataSource = lst;
        this.GridView1.DataBind();
        
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        //
        DataClassesDataContext db = new DataClassesDataContext();
        var khxxs1 = (from k in db.JB_khxx where k.mingcheng.Contains("李") select k).Union
                     (from k in db.JB_khxx where k.mingcheng.Contains("赵") select k).OrderBy(k => k.id);
        this.GridView1.DataSource = khxxs1;
        this.GridView1.DataBind();
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        //
        DataClassesDataContext db = new DataClassesDataContext();
        var khxxs1 = from k in db.JB_khxx
                     group k by new { k.sfid } into g
                     where g.Count() >= 1
                     orderby g.Count() descending
                     select new
                     {
                         //名称 = g.Key.mingcheng,
                         //电话 = g.Key.dianhua,
                         省份=g.Key.sfid,
                         数量 = g.Count()
                     };
        this.GridView1.DataSource = khxxs1;
        this.GridView1.DataBind();
    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        var khxxs1 = from k in db.JB_khxx
                     select new
                     {
                         编号 = k.id,
                         名称 = k.mingcheng,
                         联系方式 = new
                         {
                             电话 = k.dianhua,
                             传真 = k.chuanzhen
                         }
                     };
        this.GridView1.DataSource = khxxs1;
        this.GridView1.DataBind();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        //内连接
        DataClassesDataContext db = new DataClassesDataContext();
        var khxxs1 = from k in db.JB_khxx
                     join s in db.JB_SF
                     on k.sfid equals s.id
                     select new
                     {
                         编号 = k.id,
                         名称 = k.mingcheng,
                         电话 = k.dianhua,
                         传真 = k.chuanzhen,
                         地址 = k.dizhi,
                         省份 = s.mingcheng
                     };
        this.GridView1.DataSource = khxxs1;
        this.GridView1.DataBind();
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        //外连接
        DataClassesDataContext db = new DataClassesDataContext();
        var khxxs1 = from k in db.JB_khxx
                     join s in db.JB_SF
                     on k.sfid equals s.id
                     into ks
                     from s in ks.DefaultIfEmpty()
                     select new
                     {
                         编号 = k.id,
                         名称 = k.mingcheng,
                         电话 = k.dianhua,
                         传真 = k.chuanzhen,
                         省份 = s.mingcheng
                     };
        this.GridView1.DataSource = khxxs1;
        this.GridView1.DataBind();
    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        //
        DataClassesDataContext db = new DataClassesDataContext();
        System.Data.Linq.Table<JB_khxx> table = db.GetTable<JB_khxx>();
        GridView1.DataSource = table;
        GridView1.DataBind();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        var khxxs1 = from k in db.JB_khxx
                     select k;
        List<JB_khxx> lst = khxxs1.ToList<JB_khxx>();
        this.GridView1.DataSource = lst;
        this.GridView1.DataBind();
        ViewState["aa"] = lst;//
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        List<JB_khxx> lst = (List<JB_khxx>)ViewState["aa"];
        this.GridView1.DataSource = lst;
        this.GridView1.DataBind();
        
    }
}
