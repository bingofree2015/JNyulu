/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : PhotoGraph
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
    /// PhotoGraph数据库持久化处理类
    /// </summary>
    public class PhotoGraphSqlMapDao : BaseSqlMapDao, IPhotoGraphDao
    {
        /// <summary>
        /// 获取PhotoGraph列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        /// <returns>PhotoGraph集合</returns>
        public IList<PhotoGraph> GetBasePhotoGraph(PhotoGraph photoGraph)
        {
            return ExecuteQueryForList<PhotoGraph>("JNyulu.GetBasePhotoGraph", photoGraph);
        }

        /// <summary>
        /// 获取PhotoGraph列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>PhotoGraph</returns>
        public PhotoGraph GetBasePhotoGraphByID(int id)
        {
            return ExecuteQueryForObject<PhotoGraph>("JNyulu.GetBasePhotoGraphByID", id);
        }

        /// <summary>
        /// 获取PhotoGraph列表(包含父对象)
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        /// <returns>PhotoGraph集合</returns>
        public IList<PhotoGraph> GetPhotoGraph(PhotoGraph photoGraph)
        {
            return ExecuteQueryForList<PhotoGraph>("JNyulu.GetPhotoGraph", photoGraph);
        }

        /// <summary>
        /// 获取PhotoGraph列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>PhotoGraph</returns>
        public PhotoGraph GetPhotoGraphByID(int id)
        {
            return ExecuteQueryForObject<PhotoGraph>("JNyulu.GetPhotoGraphByID", id);
        }

        /// <summary>
        /// 插入PhotoGraph
        /// </summary>
        /// <param name="PhotoGraph">PhotoGraph</param>
        public int InsertPhotoGraph(PhotoGraph photoGraph)
        {
            Hashtable _map = new Hashtable();
            _map.Add("CatalogID", photoGraph.CatalogID);
            _map.Add("Subject", photoGraph.Subject);
            _map.Add("ImageUrl", photoGraph.ImageUrl);
            _map.Add("LinkUrl", photoGraph.LinkUrl);

            _map.Add("ID", photoGraph.ID);

            photoGraph.ID = (int)ExecuteInsert("JNyulu.InsertPhotoGraph", _map);

            return photoGraph.ID;
        }

        /// <summary>
        /// 分页 PhotoGraph
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<PhotoGraph> PaginatedPhotoGraph(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<PhotoGraph> _listObj = ExecuteQueryForList<PhotoGraph>("JNyulu.PaginatedPhotoGraph", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新PhotoGraph
        /// </summary>
        /// <param name="PhotoGraph">PhotoGraph</param>
        public int UpdatePhotoGraph(PhotoGraph photoGraph)
        {
            int i = ExecuteUpdate("JNyulu.UpdatePhotoGraph", photoGraph);
            return i;
        }

        /// <summary>
        /// 删除PhotoGraph
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeletePhotoGraphByID(int id)
        {
            return ExecuteDelete("JNyulu.DeletePhotoGraphByID", id);
        }
    }
}
