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

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Catalog: Catalog的数据库操作接口.
    /// </summary>
    public interface ICatalogDao
    {
        /// <summary>
        /// 获取Catalog列表
        /// </summary>
        /// <param name="catalog">Catalog</param>
        /// <returns>Catalog集合</returns>
        IList<Catalog> GetCatalog(Catalog catalog);

        /// <summary>
        /// 获取Catalog列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Catalog</returns>
        Catalog GetCatalogByID(int id);

        /// <summary>
        /// 插入Catalog
        /// </summary>
        /// <param name="catalog">Catalog</param>
        int InsertCatalog(Catalog catalog);

        /// <summary>
        /// 分页 Catalog
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Catalog> PaginatedCatalog(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Catalog
        /// </summary>
        /// <param name="catalog">Catalog</param>
        int UpdateCatalog(Catalog catalog);

        /// <summary>
        /// 删除Catalog
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteCatalogByID(int id);

    }
}
