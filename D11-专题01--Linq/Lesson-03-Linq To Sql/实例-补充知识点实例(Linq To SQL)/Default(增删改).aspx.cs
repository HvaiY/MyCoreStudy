using System;
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
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Collections.Generic;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            var khxxs = from k in db.JB_khxx
                        where (k.dizhi.Contains("") && k.id.Contains(""))
                        select k;
            List<JB_khxx> lst = khxxs.ToList<JB_khxx>();
            GV.DataSource = khxxs;
            GV.DataBind();
        }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {
        //多条件模糊查询
        DataClassesDataContext db = new DataClassesDataContext();
        //var khxxs = from k in db.JB_khxx
        //            where (k.id.Contains(tb_ID.Text) && k.mingcheng.Contains(tb_MingCheng.Text))
        //            select k;
        /////////////////////////////////////////// StartsWith 用法 /////////////////////////////////////////////////////////
        //var khxxs = from k in db.JB_khxx
        //            where (k.id.StartsWith(tb_ID.Text) && k.mingcheng.StartsWith(tb_MingCheng.Text))
        //            select k;
        /////////////////////////////////////////// EndsWith 用法 ////////////////////////////////////////////////////////
        //var khxxs = from k in db.JB_khxx
        //            where (k.id.EndsWith(tb_ID.Text) && k.mingcheng.EndsWith(tb_MingCheng.Text))
        //            select k;
        //////////////////////////////////////////////////////////////////////////////
        //多条件模糊查询 利用SqlMethods类
        //DataClassesDataContext db = new DataClassesDataContext();
        var khxxs = from k in db.JB_khxx
                    where SqlMethods.Like(k.id, "%" + tb_ID.Text + "%") && SqlMethods.Like(k.mingcheng, tb_MingCheng.Text + "%")
                    select k;
        
        /////
        GV.DataSource = khxxs;
        GV.DataBind();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        //
    }
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        //
    }
    protected void Button4_Click(object sender, EventArgs e)
    {       
        //add
        DataClassesDataContext db = new DataClassesDataContext();
        JB_khxx khxx = new JB_khxx();
        khxx.id        = TextBox1.Text;
        khxx.mingcheng = TextBox2.Text;
        khxx.dianhua   = TextBox3.Text;
        khxx.chuanzhen = TextBox4.Text;
        khxx.dizhi     = TextBox5.Text;

        db.JB_khxx.InsertOnSubmit(khxx);
        db.SubmitChanges();
        //
        var khxxs = from k in db.JB_khxx
                    where (k.dizhi.Contains("") && k.id.Contains(""))
                    select k;
        GV.DataSource = khxxs;
        GV.DataBind();
    }

    protected void GV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string sid = GV.DataKeys[e.RowIndex].Value.ToString();
        DataClassesDataContext db = new DataClassesDataContext();
        JB_khxx khxx = db.JB_khxx.Single<JB_khxx>(kh => kh.id == sid);
        //var khs = from kh in db.JB_khxx
        //          where kh.id == sid
        //          select kh;
        //db.JB_khxx.DeleteAllOnSubmit<JB_khxx>(khs);
        //
        //List<JB_khxx> lst = khs.ToList<JB_khxx>();
        db.JB_khxx.DeleteOnSubmit(khxx);
        db.SubmitChanges();
        //
        var khxxs = from k in db.JB_khxx
                    select k;
        GV.DataSource = khxxs;
        GV.DataBind();
    }

    protected void GV_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox1.Text = GV.SelectedRow.Cells[1].Text;
        TextBox2.Text = GV.SelectedRow.Cells[2].Text;
        TextBox3.Text = GV.SelectedRow.Cells[3].Text;
        TextBox4.Text = GV.SelectedRow.Cells[4].Text;
        TextBox5.Text = GV.SelectedRow.Cells[5].Text;
    }
    protected void Button6_Click(object sender, EventArgs e)
    {
        //修改
        DataClassesDataContext db = new DataClassesDataContext();
        JB_khxx khxx = db.JB_khxx.Single<JB_khxx>(kh=>kh.id==TextBox1.Text);

        khxx.mingcheng = TextBox2.Text;
        khxx.dianhua = TextBox3.Text;
        khxx.chuanzhen = TextBox4.Text;
        khxx.dizhi = TextBox5.Text;        
        db.SubmitChanges();
        //
        var khxxs = from k in db.JB_khxx
                    where (k.dizhi.Contains("") && k.id.Contains(""))
                    select k;
        GV.DataSource = khxxs;
        GV.DataBind();
    }

    protected void GV_Sorting(object sender, GridViewSortEventArgs e)
    {
        //排序
        DataClassesDataContext db = new DataClassesDataContext();
        //var khxxs = from k in db.JB_khxx
        //            where (k.dizhi.Contains("") && k.id.Contains(""))
        //            select k;
        var lst = db.JB_khxx.OrderBy(ee=>ee.id);

        switch (e.SortExpression)
        {
            case "mingcheng":
                lst = db.JB_khxx.OrderBy(ee => ee.mingcheng); ;
                break;
            case "Dianhua":
                lst = db.JB_khxx.OrderBy(ee => ee.dianhua); ;
                break;
            case "ChuanZhen":
                lst = db.JB_khxx.OrderBy(ee => ee.chuanzhen); ;
                break;
        }
        //var lst = khxxs.OrderByDescending(ee=>ee.mingcheng);
        GV.DataSource = lst;
        GV.DataBind();
    }
    
}
