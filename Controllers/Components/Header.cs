using System;
using System.Web;
using System.Text;
using System.Collections;
using System.Collections.Specialized;
using Castle.MonoRail.Framework;
using Castle.MonoRail.Framework.Internal;

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web.Components
{
    public class Header : ViewComponent
    {
        public override void Render()
        {
            RenderView("default");
        }
    }
}
