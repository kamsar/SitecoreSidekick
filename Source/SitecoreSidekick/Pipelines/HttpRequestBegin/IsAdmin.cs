﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;
using Sitecore.Mvc.Extensions;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Security.Accounts;

namespace SitecoreSidekick.Pipelines.HttpRequestBegin
{
	public class IsAdmin : HttpRequestProcessor
	{
		private static readonly ConcurrentDictionary<string,User> UserRoles = new ConcurrentDictionary<string, User>();

		public static bool CurrentUserAdmin()
		{
			HttpCookie myCookie = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"];
			if (myCookie != null && (UserRoles.ContainsKey(myCookie.Value) && UserRoles[myCookie.Value].IsAdministrator))
				return true;
			return false;
		}

		public static bool CurrentUserInRoleList(List<string> roles)
		{
			HttpCookie myCookie = HttpContext.Current.Request.Cookies["ASP.NET_SessionId"];
			if (myCookie == null || !UserRoles.ContainsKey(myCookie.Value))
				return false;
			return roles.Any(role => !role.IsWhiteSpaceOrNull() && UserRoles[myCookie.Value].IsInRole(role));
		}

		public override void Process(HttpRequestArgs args)
		{
			HttpCookie myCookie = args.Context.Request.Cookies["ASP.NET_SessionId"];
			if (myCookie != null)
			{
				UserRoles[myCookie.Value] = Context.User;
			}
		}
	}
}
