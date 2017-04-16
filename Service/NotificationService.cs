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

using IBatisNet.DataAccess;

using JNyuluSoft.Persistence;
using JNyuluSoft.Persistence.Impl;
using JNyuluSoft.Model;


namespace JNyuluSoft.Service
{
    public class NotificationService
    {
        #region 私有字段

        private static NotificationService _instance = new NotificationService();

        private INotificationDao _notificationDao = null;

        #endregion

        #region 构造函数

        private NotificationService() 
        {
            _notificationDao = new NotificationSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static NotificationService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Notification列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Notification</returns>
        public Notification GetNotificationByID(int id)
        {
            return _notificationDao.GetNotificationByID(id);
        }

        /// <summary>
        /// 插入Notification
        /// </summary>
        /// <param name="notification">Notification</param>
        public int InsertNotification(Notification notification)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _notificationDao.InsertNotification(notification);
                //_daoManager.CommitTransaction();
            }
            catch (Exception ex)
            {
                //_daoManager.RollBackTransaction();
                throw ex;
            }
            return _id;
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
            return _notificationDao.PaginatedNotification(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新Notification
        /// </summary>
        /// <param name="notification">Notification</param>
        public int UpdateNotification(Notification notification)
        {
            return _notificationDao.UpdateNotification(notification);
        }

        /// <summary>
        /// 删除Notification
        /// </summary>
        /// <param name="id"></param>
        public int DeleteNotificationByID(int id)
        {
            return _notificationDao.DeleteNotificationByID(id);
        }

        #endregion
    }
}
