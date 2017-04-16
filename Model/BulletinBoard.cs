using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// 实体类BulletinBoard 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class BulletinBoard
    {
        public BulletinBoard()
        { }
        #region Model

        private int _id;
        private int? _sender;
        private int? _receiver;
        private int[] _receivers; 
        private string _subject;
        private string _msgtext;
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
        /// 发送者ID
        /// </summary>
        public int? Sender
        {
            set { _sender = value; }
            get { return _sender; }
        }
        /// <summary>
        /// 接收者ID
        /// </summary>
        public int? Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }

        /// <summary>
        /// 接收者ID队列
        /// </summary>
        public int[] Receivers
        {
            set { _receivers = value; }
            get { return _receivers; }
        }

        /// <summary>
        /// 公告标题
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }

        /// <summary>
        /// 公告内容
        /// </summary>
        public string MsgText
        {
            set { _msgtext = value; }
            get { return _msgtext; }
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

