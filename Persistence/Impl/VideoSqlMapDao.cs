/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Video
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
    /// Video数据库持久化处理类
    /// </summary>
    public class VideoSqlMapDao : BaseSqlMapDao, IVideoDao
    {
        /// <summary>
        /// 获取Video列表(不包含父对象,仅返回对象本身)
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>Video集合</returns>
        public IList<Video> GetBaseVideo(Video video)
        {
            return ExecuteQueryForList<Video>("JNyulu.GetBaseVideo", video);
        }

        /// <summary>
        /// 获取Video列表,不包含父对象,仅返回对象本身。
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Video</returns>
        public Video GetBaseVideoByID(int id)
        {
            return ExecuteQueryForObject<Video>("JNyulu.GetBaseVideoByID", id);
        }

        /// <summary>
        /// 获取Video列表(包含父对象)
        /// </summary>
        /// <param name="video">Video</param>
        /// <returns>Video集合</returns>
        public IList<Video> GetVideo(Video video)
        {
            return ExecuteQueryForList<Video>("JNyulu.GetVideo", video);
        }

        /// <summary>
        /// 获取Video列表(包含父对象)
        /// </summary>
        /// <param name="id">自增型ID</param>
        /// <returns>Video</returns>
        public Video GetVideoByID(int id)
        {
            return ExecuteQueryForObject<Video>("JNyulu.GetVideoByID", id);
        }

        /// <summary>
        /// 插入Video
        /// </summary>
        /// <param name="Video">Video</param>
        public int InsertVideo(Video video)
        {
            Hashtable _map = new Hashtable();
            _map.Add("STID", video.STID);
            _map.Add("Title", video.Title);
            _map.Add("CoversUrl", video.CoversUrl);
            _map.Add("MediaUrl", video.MediaUrl);
            _map.Add("MediaHtml", video.MediaHtml);
            _map.Add("Permit", video.Permit);

            _map.Add("ID", video.ID);

            video.ID = (int)ExecuteInsert("JNyulu.InsertVideo", _map);

            return video.ID;
        }

        /// <summary>
        /// 分页 Video
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Video> PaginatedVideo(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Video> _listObj = ExecuteQueryForList<Video>("JNyulu.PaginatedVideo", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Video
        /// </summary>
        /// <param name="Video">Video</param>
        public int UpdateVideo(Video video)
        {
            int i = ExecuteUpdate("JNyulu.UpdateVideo", video);
            return i;
        }

        /// <summary>
        /// 删除Video
        /// </summary>
        /// <param name="id">自增型ID</param>
        public int DeleteVideoByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteVideoByID", id);
        }
    }
}
