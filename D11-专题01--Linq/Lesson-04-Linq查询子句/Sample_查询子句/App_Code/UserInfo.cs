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
/// 用户的基本信息
/// </summary>
public class UserInfo
{
	private int id;
	private string username;
	private string email;
	private List<string> aliasName;
	private int roleID;

	/// <summary>
	/// 用户的ID值
	/// </summary>
	public int ID
	{
		get { return id; }
		set { id = value; }
	}
	/// <summary>
	/// 用户的名称
	/// </summary>
	public string Username
	{
		get { return username; }
		set { username = value; }
	}
	/// <summary>
	/// 用户的电子邮件
	/// </summary>
	public string Email
	{
		get { return email; }
		set { email = value; }
	}

	public List<string> AliasName
	{
		get { return aliasName; }
		set { aliasName = value; }
	}

	/// <summary>
	/// 用户的角色ID值
	/// </summary>
	public int RoleID
	{
		get { return roleID; }
		set { roleID = value; }
	}

	/// <summary>
	/// 构造函数，初始化用户的ID值、名称和电子邮件
	/// </summary>
	/// <param name="id"></param>
	/// <param name="username"></param>
	/// <param name="email"></param>
	public UserInfo(int id, string username, string email)
	{
		this.id = id;
		this.username = username;
		this.email = email;
	}

	/// <summary>
	/// 构造函数，初始化用户的ID值、名称、电子邮件和角色ID值
	/// </summary>
	/// <param name="id"></param>
	/// <param name="username"></param>
	/// <param name="email"></param>
	public UserInfo(int id,string username,string email,int roleID)
	{
		this.id = id;
		this.username = username;
		this.email = email;
		this.roleID = roleID;
	}

	/// <summary>
	/// 构造函数，初始化用户的ID值、名称、电子邮件和别名
	/// </summary>
	/// <param name="id"></param>
	/// <param name="username"></param>
	/// <param name="email"></param>
	/// <param name="aliasName"></param>
	public UserInfo(int id,string username,string email,List<string> aliasName)
	{
		this.id = id;
		this.username = username;
		this.email = email;
		this.aliasName = aliasName;
	}

	/// <summary>
	/// 构造函数，初始化用户的ID值、名称、电子邮件、角色ID值和别名
	/// </summary>
	/// <param name="id"></param>
	/// <param name="username"></param>
	/// <param name="email"></param>
	/// <param name="aliasName"></param>
	public UserInfo(int id,string username,string email,int roleID,List<string> aliasName)
	{
		this.id = id;
		this.username = username;
		this.email = email;
		this.roleID = roleID;
		this.aliasName = aliasName;
	}
}
