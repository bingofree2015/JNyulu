/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Message
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
    public class MessageService
    {
        #region 私有字段

        private static MessageService _instance = new MessageService();

        private IMessageDao _messageDao = null;

        #endregion

        #region 构造函数

        private MessageService() 
        {
            _messageDao = new MessageSqlMapDao();
        }

        #endregion

        #region 公共方法

        public static MessageService GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// 获取Message列表(包含父对象)
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Message集合</returns>
        public IList<Message> GetMessage(Message message)
        {
            return _messageDao.GetMessage(message);
        }

        /// <summary>
        /// 获取Message列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message</returns>
        public Message GetMessageByID(int id)
        {
            return _messageDao.GetMessageByID(id);
        }

        /// <summary>
        /// 插入Message
        /// </summary>
        /// <param name="message">Message</param>
        public int InsertMessage(Message message)
        {
            int _id = -1;

            try
            {
                //_daoManager.BeginTransaction();
                _id = _messageDao.InsertMessage(message);
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
        /// 分页 Message
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        public IList<Message> PaginatedMessage(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum)
        {
            return _messageDao.PaginatedMessage(pageIndex, pageSize, sortFields, conAttr, ref totalNum);
        }
        /// <summary>
        /// 更新Message
        /// </summary>
        /// <param name="message">Message</param>
        public int UpdateMessage(Message message)
        {
            return _messageDao.UpdateMessage(message);
        }

        /// <summary>
        /// 删除Message
        /// </summary>
        /// <param name="id"></param>
        public int DeleteMessageByID(int id)
        {
            return _messageDao.DeleteMessageByID(id);
        }

        #endregion
    }
}
