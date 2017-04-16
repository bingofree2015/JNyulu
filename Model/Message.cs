using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// 实体类Message 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Message
    {
        public Message()
        { }
        #region Model

        private int _id;
        private string _username;
        private string _subject;
        private string _issue;
        private string _reply;
        private DateTime _recorddate;

        /// <summary>
        /// 自增型ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 留言标题
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// 问题
        /// </summary>
        public string Issue
        {
            set { _issue = value; }
            get { return _issue; }
        }
        /// <summary>
        /// 回答
        /// </summary>
        public string Reply
        {
            set { _reply = value; }
            get { return _reply; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        #endregion Model

    }
}

