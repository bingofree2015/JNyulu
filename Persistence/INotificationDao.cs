/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Notification
===================================================== */

using System;
using System.Collections.Generic;
using System.Text;

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Notification: Notification的数据库操作接口.
    /// </summary>
    public interface INotificationDao
    {
        /// <summary>
        /// 获取Notification列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Notification</returns>
        Notification GetNotificationByID(int id);

        /// <summary>
        /// 插入Notification
        /// </summary>
        /// <param name="notification">Notification</param>
        int InsertNotification(Notification notification);

        /// <summary>
        /// 分页 Notification
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Notification> PaginatedNotification(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Notification
        /// </summary>
        /// <param name="notification">Notification</param>
        int UpdateNotification(Notification notification);

        /// <summary>
        /// 删除Notification
        /// </summary>
        /// <param name="id"></param>
        int DeleteNotificationByID(int id);

    }
}
