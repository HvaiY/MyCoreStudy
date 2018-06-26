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
using System.Data.Linq;

public partial class Default_DataLoadOptions_ : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //var processes = Process

        //DataLoadOptions 提高性能
        DataClassesDataContext db = new DataClassesDataContext();
        DataLoadOptions dlo = new DataLoadOptions();
        dlo.LoadWith<JB_khxx>(r => r.id);

        db.LoadOptions = dlo;
        var khxxs = from k in db.JB_khxx
                    select k;

        this.GridView1.DataSource = khxxs;
        this.GridView1.DataBind();
    }
}
