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

public partial class LinqXElement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //用XElement创建XML Tree
        XElement ModelsData = new XElement("Models",
            new XElement("Model",
                new XElement("Name", "Kevin"),
                new XElement("Sex", "Male"),
                new XElement("Height", 185),
                new XElement("Weight", 72),
                new XElement("Country", "USA"),
                new XElement("Phone", "0933154286")),
            new XElement("Model",
                new XElement("Name", "Cindy"),
                new XElement("Sex", "Female"),
                new XElement("Height", 180),
                new XElement("Weight", 72),
                new XElement("Country", "France"),
                new XElement("Phone", "0945133456")),
            new XElement("Model",
                new XElement("Name", "Clare"),
                new XElement("Sex", "Female"),
                new XElement("Height", 178),
                new XElement("Weight", 65),
                new XElement("Country", "USA"),
                new XElement("Phone", "0955283283"))
                );

        //定义LINQ查询语法
        var models = from x in ModelsData.Elements()
                     //where (string)x.Element("Sex") == "Female" && (int)x.Element("Height") >=180
                     select new
                     {
                         Name = (string)x.Element("Name"),
                         Sex = (string)x.Element("Sex"),
                         Height = (int)x.Element("Height"),
                         Weight = (int)x.Element("Weight"),
                         Country = (string)x.Element("Country"),
                         Phone = (string)x.Element("Phone")
                     };

        gvModels.DataSource = models;
        gvModels.DataBind();
    }
}
