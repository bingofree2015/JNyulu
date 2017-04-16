
using System.Text; 
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;
using JNyuluSoft.Web;

namespace JNyuluSoft.Web.Api.Controllers
{
    [Helper(typeof(FormatHelper))]
    [Filter(ExecuteEnum.BeforeAction, typeof(AuthFilter))]
    public abstract class ApiController : SmartDispatcherController 
    {
        public Result _result = new Result
        {
            Code = -1,
            Data = "Î´Öª´íÎó"
        };
    }
}
