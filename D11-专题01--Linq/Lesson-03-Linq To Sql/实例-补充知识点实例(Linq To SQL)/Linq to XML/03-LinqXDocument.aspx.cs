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

public partial class LinqXDocument : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //以XDocument建立XML Tree
        XDocument ModelsData = 
          new XDocument(
            new XComment("这是模特儿基本资料"),
            new XProcessingInstruction("xml-stylesheet", "href='mystyle.css' title='Pro' type='text/css'"),
            new XElement("Models",
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
                        )
                    );


        //定义LINQ查询语法
        var models = from x in ModelsData.Element("Models").Elements()
                     //where (string)x.Element("Sex") == "Male" && (int)x.Element("Height") >= 182
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
