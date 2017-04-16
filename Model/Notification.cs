using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// 实体类Notification 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class Notification
    {
        public Notification()
        { }
        #region Model

        private int _id;
        private string _receivers;
        private int _stid;
        private string _title;
        private string _body;
        private string _platform;
        private DateTime _recordtime;

        /// <summary>
        /// 自增型ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 接收者ID队列
        /// </summary>
        public string Receivers
        {
            set { _receivers = value; }
            get { return _receivers; }
        }
        /// <summary>
        /// 班级ID
        /// </summary>
        public int STID
        {
            set { _stid = value; }
            get { return _stid; }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }

        /// <summary>
        /// 平台
        /// </summary>
        public string Platform
        {
            set { _platform = value; }
            get { return _platform; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime RecordTime
        {
            set { _recordtime = value; }
            get { return _recordtime; }
        }
        #endregion Model

    }
}

