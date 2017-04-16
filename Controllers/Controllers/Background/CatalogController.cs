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
    /* ��վ����  */
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
            string _message = "ɾ������ʧ��";

            _code = _catalogService.DeleteCatalogByID(catalogID);

            if (_code > 0)
            {
                _message = "ɾ������ɹ�";
            }
            else
            {
                _message = "�Բ���ɾ��ʧ�ܣ�";
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
            string _message = "�޸ķ���ʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                Catalog _catalog = new Catalog();
                _catalog.ID = catalogID;

                _catalog.Name = name;

                _code = _catalogService.UpdateCatalog(_catalog);

                if (_code > 0)
                {
                    _message = "�޸ķ��൵��";
                }
                else
                {
                    _message = "�Բ����޸�ʧ�ܣ�";
                }
            }
            else
            {
                _message = "�������Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }

        public void PostCreate(string name, int fatherID = 0, string moduleCn = "Article")
        {
            int _code = 0;
            string _message = "��������ʧ��";
            if (!string.IsNullOrEmpty(name))
            {
                Catalog _catalog = new Catalog();
                _catalog.ModuleCn = moduleCn;
                _catalog.FatherID = fatherID;
                _catalog.Name = name;
                _code = _catalogService.InsertCatalog(_catalog);

                if (_code > 0)
                {
                    _message = "��������ɹ�";
                }
                else
                {
                    _message = "�Բ��𣬴���ʧ�ܣ�������������";
                }
            }
            else
            {
                _message = "�������Ʋ���Ϊ�գ�";
            }

            RenderText("{\"code\":\"" + _code + "\",\"message\":\"" + _message + "\"}");

            CancelLayout();
        }
    }
}
