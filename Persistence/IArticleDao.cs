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

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Article: Article的数据库操作接口.
    /// </summary>
    public interface IArticleDao
    {
        /// <summary>
        /// 获取Article列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Article集合</returns>
        IList<Article> GetBaseArticle(Article article);

        /// <summary>
        /// 获取Article列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Article</returns>
        Article GetBaseArticleByID(int id);

        /// <summary>
        /// 获取Article列表(包含父对象)
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Article集合</returns>
        IList<Article> GetArticle(Article article);

        /// <summary>
        /// 获取Article列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Article</returns>
        Article GetArticleByID(int id);

        /// <summary>
        /// 插入Article
        /// </summary>
        /// <param name="article">Article</param>
        int InsertArticle(Article article);

        /// <summary>
        /// 分页 Article
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Article> PaginatedArticle(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Article
        /// </summary>
        /// <param name="article">Article</param>
        int UpdateArticle(Article article);

        /// <summary>
        /// 删除Article
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteArticleByID(int id);

    }
}
