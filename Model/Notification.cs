using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// ʵ����Notification ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ������ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ������ID����
        /// </summary>
        public string Receivers
        {
            set { _receivers = value; }
            get { return _receivers; }
        }
        /// <summary>
        /// �༶ID
        /// </summary>
        public int STID
        {
            set { _stid = value; }
            get { return _stid; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string Body
        {
            set { _body = value; }
            get { return _body; }
        }

        /// <summary>
        /// ƽ̨
        /// </summary>
        public string Platform
        {
            set { _platform = value; }
            get { return _platform; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime RecordTime
        {
            set { _recordtime = value; }
            get { return _recordtime; }
        }
        #endregion Model

    }
}

