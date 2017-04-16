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
    /* ��վ��Ѷ  */
    [ControllerDetails(Area = "Background")]
    public class ArticleController : DefaultController
    {
        protected readonly CatalogService _catalogService = CatalogService.GetInstance();
        protected readonly ArticleService _articleService = ArticleService.GetInstance();

        public void List(int catalogID, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                CatalogID = catalogID,
                PageIndex = pageIndex
            };

            int _totalNums = 0, _pageSize = 12;

            IList<Catalog> _catalogList = _catalogService.PaginatedCatalog(1, 120, "ID", "ModuleCn = 'Article'", ref _totalNums);

            PropertyBag["catalogList"] = _catalogList;

            string _conAttr = "catalogID = " + catalogID + "";

            IList<Article> _artilceList = _articleService.PaginatedArticle(pageIndex, _pageSize, "ID", _conAttr, ref _totalNums);

            string uri = "List.do?catalogID=" + catalogID + "&pageIndex={0}";
            string ltlShowPager = Paginated.BuildPager(uri, _totalNums, pageIndex, _pageSize);

            PropertyBag["articleList"] = _artilceList;
            PropertyBag["ltlShowPager"] = ltlShowPager;
        }

        public void Create(int catalogID, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                CatalogID = catalogID,
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            string _conAttr = "ModuleCn = 'Article'";

            IList<Catalog> _catalogList = _catalogService.PaginatedCatalog(1, 120, "ID", _conAttr, ref _totalNums);

            PropertyBag["catalogList"] = _catalogList;
        }

        public void PostCreate(int catalogID, string subject, string context)
        {
            int _code = 0;
            string _message = "�ϴ�����ʧ��";
            if (!string.IsNullOrEmpty(subject))
            {
                Article _article = new Article();
                _article.Subject = subject;
                _article.CatalogID = catalogID;
                _article.Context = context;

                _code = _articleService.InsertArticle(_article);

                if (_code > 0)
                {
                    _message = "�ϴ����³ɹ�";
                }
                else
                {
                    _message = "�Բ����ϴ�ʧ�ܣ�������������";
                }
            }
            else
            {
                _message = "���±��ⲻ��Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void Edit(int articleID, int catalogID, int pageIndex)
        {
            pageIndex = pageIndex <= 0 ? 1 : pageIndex;
            PropertyBag["params"] = new
            {
                CatalogID = catalogID,
                PageIndex = pageIndex
            };

            int _totalNums = 0;
            string _conAttr = "ModuleCn = 'Article'";

            IList<Catalog> _catalogList = _catalogService.PaginatedCatalog(1, 120, "ID", _conAttr, ref _totalNums);

            PropertyBag["catalogList"] = _catalogList;
            Article _article = _articleService.GetArticleByID(articleID);

            PropertyBag["article"] = _article;
        }

        public void PostEdit(int articleID, int catalogID, string subject, string context)
        {
            int _code = 0;
            string _message = "�޸�����ʧ��";
            if (!string.IsNullOrEmpty(subject))
            {
                Article _article = _articleService.GetArticleByID(articleID);
                _article.Subject = subject;
                _article.CatalogID = catalogID;
                _article.Context = context;

                _code = _articleService.UpdateArticle(_article);

                if (_code > 0)
                {
                    _message = "�޸����³ɹ�";
                }
            }
            else
            {
                _message = "���±��ⲻ��Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostDelete(int articleID)
        {
            int _code = 0;
            string _message = "ɾ������ʧ��";

            _code = _articleService.DeleteArticleByID(articleID);

            if (_code > 0)
            {
                _message = "ɾ�����³ɹ�";
            }
            else
            {
                _message = "�Բ�������ʧ�ܣ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
