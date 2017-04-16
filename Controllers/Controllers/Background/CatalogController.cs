using System;
using System.IO;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;

using Castle.MonoRail.Framework;

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web.Controllers
{
    /* 网站分类  */
    [ControllerDetails(Area = "Background")]
    public class CatalogController : DefaultController
    {
        protected readonly CatalogService _catalogService = CatalogService.GetInstance();


        public void List(int fatherID = 0, string moduleCn = "Article")
        {
            int _totalNums = 0, _pageSize = 12, pageIndex = 1;
            string _conAttr = "fatherID = " + fatherID + " AND moduleCn = '" + moduleCn + "'";

            IList<Catalog> _catalogList = _catalogService.PaginatedCatalog(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);
            PropertyBag["moduleCn"] = moduleCn;
            PropertyBag["catalog"] = _catalogService.GetCatalogByID(fatherID);
            PropertyBag["catalogList"] = _catalogList;
        }

        public void PostDelete(int catalogID)
        {
            int _code = 0;
            string _message = "删除分类失败";

            _code = _catalogService.DeleteCatalogByID(catalogID);

            if (_code > 0)
            {
                _message = "删除分类成功";
            }
            else
            {
                _message = "对不起，删除失败！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int catalogID)
        {

            Catalog _catalog = _catalogService.GetCatalogByID(catalogID);
            PropertyBag["catalog"] = _catalog;
        }

        public void PostEdit(int catalogID, string name)
        {
            int _code = 0;
            string _message = "修改分类失败";
            if (!string.IsNullOrEmpty(name))
            {
                Catalog _catalog = new Catalog();
                _catalog.ID = catalogID;

                _catalog.Name = name;

                _code = _catalogService.UpdateCatalog(_catalog);

                if (_code > 0)
                {
                    _message = "修改分类档案";
                }
                else
                {
                    _message = "对不起，修改失败！";
                }
            }
            else
            {
                _message = "分类名称不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostCreate(string name, int fatherID = 0, string moduleCn = "Article")
        {
            int _code = 0;
            string _message = "创建分类失败";
            if (!string.IsNullOrEmpty(name))
            {
                Catalog _catalog = new Catalog();
                _catalog.ModuleCn = moduleCn;
                _catalog.FatherID = fatherID;
                _catalog.Name = name;
                _code = _catalogService.InsertCatalog(_catalog);

                if (_code > 0)
                {
                    _message = "创建分类成功";
                }
                else
                {
                    _message = "对不起，创建失败，可能有重名！";
                }
            }
            else
            {
                _message = "分类名称不能为空！";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
