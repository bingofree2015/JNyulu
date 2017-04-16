/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Catalog
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
    /// Catalog数据库持久化处理类
    /// </summary>
    public class CatalogSqlMapDao : BaseSqlMapDao, ICatalogDao
    {
        /// <summary>
        /// 获取Catalog列表
        /// </summary>
        /// <param name="catalog">Catalog</param>
        /// <returns>Catalog集合</returns>
        public IList<Catalog> GetCatalog(Catalog catalog)
        {
            return ExecuteQueryForList<Catalog>("JNyulu.GetCatalog", catalog);
        }

        /// <summary>
        /// 获取Catalog列表
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Catalog</returns>
        public Catalog GetCatalogByID(int id)
        {
            return ExecuteQueryForObject<Catalog>("JNyulu.GetCatalogByID", id);
        }

        /// <summary>
        /// 插入Catalog
        /// </summary>
        /// <param name="Catalog">Catalog</param>
        public int InsertCatalog(Catalog catalog)
        {
            Hashtable _map = new Hashtable();
            _map.Add("ModuleCn", catalog.ModuleCn);
            _map.Add("FatherID", catalog.FatherID);
            _map.Add("Name", catalog.Name);

            _map.Add("ID", catalog.ID);

            catalog.ID = (int)ExecuteInsert("JNyulu.InsertCatalog", _map);

            return catalog.ID;
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
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Catalog> _listObj = ExecuteQueryForList<Catalog>("JNyulu.PaginatedCatalog", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Catalog
        /// </summary>
        /// <param name="Catalog">Catalog</param>
        public int UpdateCatalog(Catalog catalog)
        {
            int i = ExecuteUpdate("JNyulu.UpdateCatalog", catalog);
            return i;
        }

        /// <summary>
        /// 删除Catalog
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteCatalogByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteCatalogByID", id);
        }
    }
}
