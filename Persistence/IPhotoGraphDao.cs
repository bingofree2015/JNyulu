/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : PhotoGraph
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// PhotoGraph: PhotoGraph的数据库操作接口.
    /// </summary>
    public interface IPhotoGraphDao
    {
        /// <summary>
        /// 获取PhotoGraph列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        /// <returns>PhotoGraph集合</returns>
        IList<PhotoGraph> GetBasePhotoGraph(PhotoGraph photoGraph);

        /// <summary>
        /// 获取PhotoGraph列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>PhotoGraph</returns>
        PhotoGraph GetBasePhotoGraphByID(int id);

        /// <summary>
        /// 获取PhotoGraph列表(包含父对象)
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        /// <returns>PhotoGraph集合</returns>
        IList<PhotoGraph> GetPhotoGraph(PhotoGraph photoGraph);

        /// <summary>
        /// 获取PhotoGraph列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>PhotoGraph</returns>
        PhotoGraph GetPhotoGraphByID(int id);

        /// <summary>
        /// 插入PhotoGraph
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        int InsertPhotoGraph(PhotoGraph photoGraph);

        /// <summary>
        /// 分页 PhotoGraph
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<PhotoGraph> PaginatedPhotoGraph(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新PhotoGraph
        /// </summary>
        /// <param name="photoGraph">PhotoGraph</param>
        int UpdatePhotoGraph(PhotoGraph photoGraph);

        /// <summary>
        /// 删除PhotoGraph
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeletePhotoGraphByID(int id);

    }
}
