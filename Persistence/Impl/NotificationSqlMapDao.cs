/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Notification
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
    /// Notification数据库持久化处理类
    /// </summary>
    public class NotificationSqlMapDao : BaseSqlMapDao, INotificationDao
    {
        /// <summary>
        /// 获取Notification列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Notification</returns>
        public Notification GetNotificationByID(int id)
        {
            return ExecuteQueryForObject<Notification>("JNyulu.GetNotificationByID", id);
        }

        /// <summary>
        /// 插入Notification
        /// </summary>
        /// <param name="Notification">Notification</param>
        public int InsertNotification(Notification notification)
        {
            Hashtable _map = new Hashtable();
            _map.Add("Title", notification.Title);
            _map.Add("Body", notification.Body);
            _map.Add("Platform", notification.Platform);

            _map.Add("Receivers", notification.Receivers);
            _map.Add("STID", notification.STID);

            notification.ID = (int)ExecuteInsert("JNyulu.InsertNotification", _map);

            return notification.ID;
        }

        /// <summary>
        /// 分页 Notification
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Notification> PaginatedNotification(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Notification> _listObj = ExecuteQueryForList<Notification>("JNyulu.PaginatedNotification", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Notification
        /// </summary>
        /// <param name="Notification">Notification</param>
        public int UpdateNotification(Notification notification)
        {
            int i = ExecuteUpdate("JNyulu.UpdateNotification", notification);
            return i;
        }

        /// <summary>
        /// 删除Notification
        /// </summary>
        /// <param name="id"></param>
        public int DeleteNotificationByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteNotificationByID", id);
        }
    }
}
