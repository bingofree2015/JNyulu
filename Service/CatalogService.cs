/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Catalog
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using IBatisNet.DataAccess;

using JNyuluSoft.Persistence;
using JNyuluSoft.Persistence.Impl;
using JNyuluSoft.Model;


namespace JNyuluSoft.Service
{
    public class CatalogService
    {
        #region 私有字段

        private static CatalogService _instance = new CatalogService();

        private ICatalogDao _catalogDao = null;
        private ArticleService _articleService = ArticleService.GetInstance();

        #endregion

        #region 构造函数

        private CatalogService() 
        {
            _catalogDao = new CatalogSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static CatalogService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Catalog列表
        /// </summary>
        /// <param name="catalog">Catalog</param>
        /// <returns>Catalog集合</returns>
        public IList<Catalog> GetCatalog(Catalog catalog)
        {
            return _catalogDao.GetCatalog(catalog);
        }

        /// <summary>
        /// 获取Catalog列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Catalog</returns>
        public Catalog GetCatalogByID(int id)
        {
            return _catalogDao.GetCatalogByID(id);
        }

        /// <summary>
        /// 插入Catalog
        /// </summary>
        /// <param name="catalog">Catalog</param>
        public int InsertCatalog(Catalog catalog)
        {
            int _id = -1;
            try
            {
                //_daoManager.BeginTransaction();
                _id = _catalogDao.InsertCatalog(catalog);
                //_daoManager.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_daoManager.RollBackTransaction();
                throw ex;
            }
            return _id;
        }

        /// <summary>
        /// 分页 Catalog
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Catalog> PaginatedCatalog(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _catalogDao.PaginatedCatalog(pageIndex, pageSize, sortFields, conAttr, ref totalNum); 
        }

        /// <summary>
        /// 更新Catalog
        /// </summary>
        /// <param name="catalog">Catalog</param>
        public int UpdateCatalog(Catalog catalog)
        {
            return _catalogDao.UpdateCatalog(catalog);
        }

        /// <summary>
        /// 删除Catalog
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteCatalogByID(int id)
        {
            Article _article = new Article();
            _article.CatalogID = id;
            IList<Article> _articleList = _articleService.GetBaseArticle(_article);
            foreach (Article article in _articleList)
            {
              _articleService.DeleteArticleByID(article.ID);  
            }

            return _catalogDao.DeleteCatalogByID(id);
        }

        #endregion
    }
}
