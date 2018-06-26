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

public partial class LinqXAttribute : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //以XElement建立XML Tree
        XElement ModelsData = new XElement("Models",
            new XElement("Model",
                new XElement("Name", "Kevin"),
                new XElement("Sex", "Male"),
                new XElement("Height", 185),
                new XElement("Weight", 72),
                new XElement("Country", "USA"),
                new XElement("PhoneNos",
                    new XElement("Phone", new XAttribute("Type", "Home"), "3701155"),
                    new XElement("Phone", new XAttribute("Type", "Office"), "#111"),
                    new XElement("Phone", new XAttribute("Type", "Mobile"), "0933154286"))),
            new XElement("Model",
                new XElement("Name", "Cindy"),
                new XElement("Sex", "Female"),
                new XElement("Height", 180),
                new XElement("Weight", 72),
                new XElement("Country", "France"),
                new XElement("PhoneNos",
                    new XElement("Phone", new XAttribute("Type", "Home"), "3661524"),
                    new XElement("Phone", new XAttribute("Type", "Office"), "#222"),
                    new XElement("Phone", new XAttribute("Type", "Mobile"), "0945133456"))),
            new XElement("Model",
                new XElement("Name", "Clare"),
                new XElement("Sex", "Female"),
                new XElement("Height", 178),
                new XElement("Weight", 65),
                new XElement("Country", "USA"),
                new XElement("PhoneNos",
                    new XElement("Phone", new XAttribute("Type", "Home"), "4521894"),
                    new XElement("Phone", new XAttribute("Type", "Office"), "#333"),
                    new XElement("Phone", new XAttribute("Type", "Mobile"), "0912456789")))
                );

        //定义LINQ查询语法
        var models = from x in ModelsData.Elements()
                     //where (string)x.Element("Country") == "USA" 
                     select new
                     {
                         Name = (string)x.Element("Name"),
                         Sex = (string)x.Element("Sex"),
                         Height = (int)x.Element("Height"),
                         Weight = (int)x.Element("Weight"),
                         Country = (string)x.Element("Country"),
                         PhoneHome = (string)x.Element("PhoneNos").Elements().First(),
                         PhoneMobile = (string)x.Element("PhoneNos").Elements().ElementAt(1)
                     };

        gvModels.DataSource = models;
        gvModels.DataBind();
    }
}
