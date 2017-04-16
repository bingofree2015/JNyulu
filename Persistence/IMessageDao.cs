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

using JNyuluSoft.Model;

namespace JNyuluSoft.Persistence
{
    /// <summary>
    /// Message: Message的数据库操作接口.
    /// </summary>
    public interface IMessageDao
    {
        /// <summary>
        /// 获取Message列表(包含父对象)
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns>Message集合</returns>
        IList<Message> GetMessage(Message message);

        /// <summary>
        /// 获取Message列表(包含父对象)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Message</returns>
        Message GetMessageByID(int id);

        /// <summary>
        /// 插入Message
        /// </summary>
        /// <param name="message">Message</param>
        int InsertMessage(Message message);

        /// <summary>
        /// 分页 Message
        /// </summary>
        /// <param name="pageIndex">pageIndex</param>
        /// <param name="pageSize">pageSize</param>
        /// <param name="sortFields">sortFields</param>
        /// <param name="conAttr">conAttr</param>
        /// <param name="totalNum">totalNum</param>
        IList<Message> PaginatedMessage(int pageIndex, int pageSize, string sortFields, string conAttr, ref int totalNum);

        /// <summary>
        /// 更新Message
        /// </summary>
        /// <param name="message">Message</param>
        int UpdateMessage(Message message);

        /// <summary>
        /// 删除Message
        /// </summary>
        /// <param name="id"></param>
        int DeleteMessageByID(int id);
    }
}
