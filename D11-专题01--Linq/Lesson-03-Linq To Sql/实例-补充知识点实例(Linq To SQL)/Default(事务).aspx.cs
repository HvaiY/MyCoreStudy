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
using System.Transactions;
using System.Data.Common;

public partial class Default_事务_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //add
        DataClassesDataContext db = new DataClassesDataContext();

        db.JB_khxx.InsertOnSubmit(new JB_khxx
        {
            id = "aaa",
            mingcheng = "aa",
            dianhua = "aa",
            chuanzhen = "aa",
            dizhi = "aa",
            sfid = 1
        });
        db.JB_khxx.InsertOnSubmit(new JB_khxx 
        {
            id = "bbbbbb",
            mingcheng = "bb",
            dianhua = "bb",
            chuanzhen = "bb",
            dizhi = "bb",
            sfid = 1
        });
        
        db.SubmitChanges();
        //
        var khxxs = from k in db.JB_khxx
                    select k;
        GridView1.DataSource = khxxs;
        GridView1.DataBind();        

    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        var khxxs = from k in db.JB_khxx
                    select k;
        GridView1.DataSource = khxxs;
        GridView1.DataBind();  
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        using (TransactionScope ts = new TransactionScope())
        {            
            //
            db.JB_khxx.InsertOnSubmit(new JB_khxx
            {
                id = "aaa",
                mingcheng = "aa",
                dianhua = "aa",
                chuanzhen = "aa",
                dizhi = "aa",
                sfid = 1
            });
            db.JB_khxx.InsertOnSubmit(new JB_khxx
            {
                id = "bb",
                mingcheng = "bb",
                dianhua = "bb",
                chuanzhen = "bb",
                dizhi = "bb",
                sfid = 1
            });
            db.SubmitChanges();
            //
            ts.Complete();
        }
        //
        var khxxs = from k in db.JB_khxx
                    select k;
        GridView1.DataSource = khxxs;
        GridView1.DataBind();    

    }

    protected void Button4_Click(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        if (db.Connection != null) db.Connection.Open();
        DbTransaction tran = db.Connection.BeginTransaction();
        db.Transaction = tran;
        try
        {
            db.JB_khxx.InsertOnSubmit(new JB_khxx
            {
                id = "aaa",
                mingcheng = "aa",
                dianhua = "aa",
                chuanzhen = "aa",
                dizhi = "aa",
                sfid = 1
            });
            db.JB_khxx.InsertOnSubmit(new JB_khxx
            {
                id = "bbb",
                mingcheng = "bb",
                dianhua = "bb",
                chuanzhen = "bb",
                dizhi = "bb",
                sfid = 1
            });
            db.SubmitChanges();   
       
            //
            tran.Commit();
        }
        catch
        {
            tran.Rollback();
            Label1.Text = "事务提交失败!";
        }
        //
    }
}
