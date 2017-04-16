/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Article
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
    public class ArticleService
    {
        #region 私有字段
        private static ArticleService _instance = new ArticleService();
        //private IDaoManager _daoManager = null;
        private IArticleDao _articleDao = null;

        #endregion

        #region 构造函数

        private ArticleService() 
        {
            /*
            _daoManager = ServiceConfig.GetInstance().DaoManager;
            _articleDao = _daoManager.GetDao(typeof(IArticleDao)) as IArticleDao;
            */
            _articleDao =  new ArticleSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static ArticleService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Article列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Article集合</returns>
        public IList<Article> GetBaseArticle(Article article)
        {
            return _articleDao.GetBaseArticle(article);
        }

        /// <summary>
        /// 获取Article列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Article</returns>
        public Article GetBaseArticleByID(int id)
        {
            return _articleDao.GetBaseArticleByID(id);
        }

        /// <summary>
        /// 获取Article列表(包含父对象)
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Article集合</returns>
        public IList<Article> GetArticle(Article article)
        {
            return _articleDao.GetArticle(article);
        }

        /// <summary>
        /// 获取Article列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Article</returns>
        public Article GetArticleByID(int id)
        {
            return _articleDao.GetArticleByID(id);
        }

        /// <summary>
        /// 插入Article
        /// </summary>
        /// <param name="article">Article</param>
        public int InsertArticle(Article article)
        {
            int _id = -1;
            try
            {
                //_daoManager.BeginTransaction();
                _id = _articleDao.InsertArticle(article);
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
        /// 分页 Article
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Article> PaginatedArticle(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _articleDao.PaginatedArticle(pageIndex, pageSize, sortFields, conAttr, ref totalNum); 
        }

        /// <summary>
        /// 更新Article
        /// </summary>
        /// <param name="article">Article</param>
        public int UpdateArticle(Article article)
        {
            return _articleDao.UpdateArticle(article);
        }

        /// <summary>
        /// 删除Article
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteArticleByID(int id)
        {
            return _articleDao.DeleteArticleByID(id);
        }

        #endregion
    }
}
