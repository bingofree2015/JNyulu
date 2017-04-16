/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Article
===================================================== */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;
using JNyuluSoft.Persistence;


namespace JNyuluSoft.Persistence.Impl
{
    /// <summary>
    /// Article数据库持久化处理类
    /// </summary>
    public class ArticleSqlMapDao : BaseSqlMapDao, IArticleDao
    {
        /// <summary>
        /// 获取Article列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Article集合</returns>
        public IList<Article> GetBaseArticle(Article article)
        {
            return ExecuteQueryForList<Article>("JNyulu.GetBaseArticle", article);
        }

        /// <summary>
        /// 获取Article列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Article</returns>
        public Article GetBaseArticleByID(int id)
        {
            return ExecuteQueryForObject<Article>("JNyulu.GetBaseArticleByID", id);
        }

        /// <summary>
        /// 获取Article列表(包含父对象)
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Article集合</returns>
        public IList<Article> GetArticle(Article article)
        {
            return ExecuteQueryForList<Article>("JNyulu.GetArticle", article);
        }

        /// <summary>
        /// 获取Article列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Article</returns>
        public Article GetArticleByID(int id)
        {
            return ExecuteQueryForObject<Article>("JNyulu.GetArticleByID", id);
        }

        /// <summary>
        /// 插入Article
        /// </summary>
        /// <param name="Article">Article</param>
        public int InsertArticle(Article article)
        {
            Hashtable _map = new Hashtable();
            _map.Add("CatalogID", article.CatalogID);
            _map.Add("Subject", article.Subject);
            _map.Add("Context", article.Context);
            _map.Add("ID", article.ID);

            article.ID = (int)ExecuteInsert("JNyulu.InsertArticle", _map);

            return article.ID;
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
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Article> _listObj = ExecuteQueryForList<Article>("JNyulu.PaginatedArticle", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Article
        /// </summary>
        /// <param name="Article">Article</param>
        public int UpdateArticle(Article article)
        {
            int i = ExecuteUpdate("JNyulu.UpdateArticle", article);
            return i;
        }

        /// <summary>
        /// 删除Article
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteArticleByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteArticleByID", id);
        }
    }
}
