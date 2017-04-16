/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Video
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Video: Video的数据库操作接口.
    /// </summary>
    public interface IVideoDao
    {
        /// <summary>
        /// 获取Video列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>Video集合</returns>
        IList<Video> GetBaseVideo(Video video);

        /// <summary>
        /// 获取Video列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Video</returns>
        Video GetBaseVideoByID(int id);

        /// <summary>
        /// 获取Video列表(包含父对象)
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>Video集合</returns>
        IList<Video> GetVideo(Video video);

        /// <summary>
        /// 获取Video列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Video</returns>
        Video GetVideoByID(int id);

        /// <summary>
        /// 插入Video
        /// </summary>
        /// <param name="video">Video</param>
        int InsertVideo(Video video);

        /// <summary>
        /// 分页 Video
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Video> PaginatedVideo(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Video
        /// </summary>
        /// <param name="video">Video</param>
        int UpdateVideo(Video video);

        /// <summary>
        /// 删除Video
        /// </summary>
        /// <param name="id">自增型ID</param>
        int DeleteVideoByID(int id);

    }
}
