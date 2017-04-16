
using System.Text; 
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Web;

namespace JNyuluSoft.Web.Controllers
{
    [Helper(typeof(FormatHelper))]
    [Layout("default")]
    [Filter(ExecuteEnum.BeforeAction, typeof(AuthenticationFilter))]
    public abstract class DefaultController : SmartDispatcherController 
    {

    }
}
