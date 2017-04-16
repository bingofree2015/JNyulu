using System;
using System.Collections;
using System.Web;
using System.Data;
using System.Data.SqlClient;  
using System.Text.RegularExpressions;  
using System.Web.SessionState;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Util;

namespace JNyuluSoft.Web
{
    public class AutoLoginHttpModule : IHttpModule, IRequiresSessionState
    {
        #region IHttpModule 成员

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += (new EventHandler(this.Context_PreRequestHandlerExecute));
        }
        #endregion

        private void Context_PreRequestHandlerExecute(Object source, EventArgs e)
        {
            HttpApplication Application = (HttpApplication)source;
            HttpContext ctx = Application.Context;
            if (ctx.Session == null)
            {
                return;
            }
            else
            {
                HttpCookie cookie = ctx.Request.Cookies["JNyulu"];
                if (cookie != null)
                {
                    int userId = Int32.Parse(cookie["UserId"]);
                    string flag = cookie["Flag"];

                    if (ctx.Session["logonUser"] != null)
                    {
                        //已经登录过
                        Employee _logonUser = ctx.Session["logonUser"] as Employee;

                        if (_logonUser.ID != userId)
                        {
                            IDictionary result = null;



                            int code = (int)result["code"];
                            if (code != 0)
                            {
                                ctx.Response.Cookies.Remove("JNyulu");
                                return;
                            }
                            ctx.Session["logonUser"] = _logonUser;
                        }
                        return;
                    }
                    else
                    {
                        try
                        {
                            IDictionary result = null;



                            int code = (int)result["code"];
                            if (code != 0)
                            {
                                ctx.Response.Cookies.Remove("JNyulu");
                                return;
                            }
                            //校验成功，写自动登录日志
                            Employee _logonUser = result["employee"] as Employee;

                            ctx.Session["logonUser"] = _logonUser;
                        }
                        catch (Exception ex)
                        {
                            LogHelper.Error("Context_PreRequestHandlerExecute " + ex.ToString()); 
                            ctx.Response.Cookies.Remove("JNyulu");
                        }
                    }
                }
                else
                {
                    Employee _logonUser = ctx.Session["logonUser"] as Employee;
                    if (_logonUser != null)
                    {
                        ctx.Session.Remove("logonUser");
                        ctx.Session.Clear();
                    }
                }
            }
        }
    }
}
