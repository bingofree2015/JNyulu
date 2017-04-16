using System;
using System.Text;
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

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string AUTHID = Request.Cookies[FormsAuthentication.FormsCookieName] == null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value;

            //var _jsonString = @"E:\\WebSite\\JnYuLu2.0\Web\UploadFiles\xls\2014-11-30\36l4toub.xls”的一部分。";
            //Response.Write(_jsonString);

            Response.Redirect("/Web/index.do"); 
        }
    }
}
