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

public partial class Join : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		JoinQuery();
		//GroupJoinQuery();
    }

	private void JoinQuery()
	{	///构建数据源
		List<UserInfo> users = new List<UserInfo>();
		List<RoleInfo> roles = new List<RoleInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com",i * 2));
			roles.Add(new RoleInfo(i,"RoleName0" + i.ToString()));
		}
		///查询ID值小于9，且角色包含在roles中的用户
		var value = users.Join(roles,
			user => user.RoleID,
			role => role.ID,
			(user,role) => 
				new {Username = user.Username,RoleName = role.RoleName});			
		///显示查询结果
		foreach (var v in value)
		{
			Response.Write("Username:" + v.Username + ",RoleName:" + v.RoleName + "</br>");
		}
	}

	private void GroupJoinQuery()
	{	///构建数据源
		List<UserInfo> users = new List<UserInfo>();
		List<RoleInfo> roles = new List<RoleInfo>();
		for (int i = 1; i < 10; i++)
		{
			users.Add(new UserInfo(i,"User0" + i.ToString(),"User0" + i.ToString() + "@web.com",i * 2));
			roles.Add(new RoleInfo(i,"RoleName0" + i.ToString()));
		}
		///查询ID值小于9，且角色包含在roles中的用户
		var value = users.GroupJoin(roles,
			user => user.RoleID,
			role => role.ID,
			(user,role) => new 
					{
						ID = user.ID,
						Username = user.Username,
						Email = user.Email,
						RoleID = user.RoleID,
						Roles = role.ToList()
					});
			
		///显示查询结果
		foreach (var v in value)
		{
			Response.Write(v.Username + "," + (v.Roles.Count > 0 ? v.Roles[0].RoleName : string.Empty) + "</br>");
		}
	}
}
