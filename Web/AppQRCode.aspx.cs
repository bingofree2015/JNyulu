using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JNyuluSoft.Web
{
    public partial class AppQRCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string _appUrl = "http://www.jnyulu.com/App/jnyulu.apk";
            string _agent = HttpContext.Current.Request.UserAgent; 

            if(!string.IsNullOrEmpty(_agent))
            {
                _agent = _agent.ToLower();
                if(_agent.Contains("android") || _agent.Contains("linux"))
                {
                    _appUrl = "http://www.jnyulu.com/App/jnyulu.apk";
                }
                else if(_agent.Contains("iphone") || _agent.Contains("ipad"))
                {
                    _appUrl = "https://itunes.apple.com/cn/app/ji-nan-yu-lu/id805270381?mt=8";
                }
            }
            Response.Redirect(_appUrl,true);
        }
    }
}