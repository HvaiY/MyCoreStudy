using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 角色的基本信息
/// </summary>
public class RoleInfo
{
	private int id;
	private string roleName;

	/// <summary>
	/// 角色的ID值
	/// </summary>
	public int ID
	{
		get { return id; }
		set { id = value; }
	}
	/// <summary>
	/// 角色的名称
	/// </summary>
	public string RoleName
	{
		get { return roleName; }
		set { roleName = value; }
	}

	public RoleInfo(int id,string roleName)
	{
		this.id = id;
		this.roleName = roleName; 
	}
}
