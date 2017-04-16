using System;
using System.Web;
using System.Collections.Specialized;

using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web
{
    public class AuthenticationFilter: IFilter
    {
        public bool Perform(ExecuteEnum exec, IRailsEngineContext context, Controller controller)
        {
            Employee _employee = context.Session["logonUser"] as Employee;

            // Sets the principal as the current user
            
            // Checks if it is OK
            if (_employee == null)
            {
                // Not authenticated, redirect to login
                NameValueCollection parameters = new NameValueCollection();
                parameters.Add("backUrl", context.Url);
                //HttpContext.Current.Response.Write("/Background/Home");
                //HttpContext.Current.Response.End();
                controller.Redirect("Home", "Login", parameters);

                // Prevent request from continue
                return false;
            }
            else
            {
                string _moduleCn = controller.Name;
                if (!string.IsNullOrEmpty(controller.AreaName))
                    _moduleCn = controller.AreaName;

                _moduleCn = _moduleCn.ToLower();
                int _permission = -1;
                if (_moduleCn.Equals("essential"))
                {
                    _permission = Convert.ToInt32(PermissionType.Essential, 2); //系统配置
                }
                if (_moduleCn.Equals("student"))
                {
                    _permission = Convert.ToInt32(PermissionType.Student, 2);  //学生管理 
                }
                if (_moduleCn.Equals("article"))
                {
                    _permission = Convert.ToInt32(PermissionType.Article, 2); //网站内容
                }
                if (_permission > 0)
                {
                    int _authority = _employee.Permission.Value & _permission;
                    if (_authority == 0)
                    {
                        // Not authenticated, redirect to login
                        NameValueCollection parameters = new NameValueCollection();
                        parameters.Add("backUrl", context.Url);
                        controller.Redirect("Home", "Login", parameters);

                        // Prevent request from continue
                        return false;
                    }
                }
            }
            // Everything is ok
            return true;
        }
    }
}
