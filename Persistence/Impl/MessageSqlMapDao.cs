/* =====================================================
    作    者 : bingofree
    创建时间 : 2009-10-26 21:53:31
    修 改 者 : 
    修改时间 : 
    描    述 : Message
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
    /// Message数据库持久化处理类
    /// </summary>
    public class MessageSqlMapDao : BaseSqlMapDao, IMessageDao
    {
        /// <summary>
        /// 获取Message列表(包含父对象)
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Message集合</returns>
        public IList<Message> GetMessage(Message message)
        {
            return ExecuteQueryForList<Message>("JNyulu.GetMessage", message);
        }

        /// <summary>
        /// 获取Message列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message</returns>
        public Message GetMessageByID(int id)
        {
            return ExecuteQueryForObject<Message>("JNyulu.GetMessageByID", id);
        }

        /// <summary>
        /// 插入Message
        /// </summary>
        /// <param name="message">Message</param>
        public int InsertMessage(Message message)
        {
            Hashtable _map = new Hashtable();
            _map.Add("UserName", message.UserName);
            _map.Add("Subject", message.Subject);
            _map.Add("Issue", message.Issue);

            message.ID = (int)ExecuteInsert("JNyulu.InsertMessage", _map);

            return message.ID;
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
            Hashtable _map = new Hashtable();
            _map.Add("pageIndex", pageIndex);
            _map.Add("pageSize", pageSize);
            _map.Add("sortFields", sortFields);
            _map.Add("conAttr", conAttr);
            _map.Add("totalNum", totalNum);

            IList<Message> _listObj = ExecuteQueryForList<Message>("JNyulu.PaginatedMessage", _map);

            totalNum = (int)_map["totalNum"];

            return _listObj;
        }

        /// <summary>
        /// 更新Message
        /// </summary>
        /// <param name="Message">Message</param>
        public int UpdateMessage(Message message)
        {
            int i = ExecuteUpdate("JNyulu.UpdateMessage", message);
            return i;
        }

        /// <summary>
        /// 删除Message
        /// </summary>
        /// <param name="id"></param>
        public int DeleteMessageByID(int id)
        {
            return ExecuteDelete("JNyulu.DeleteMessageByID", id);
        }
    }
}
