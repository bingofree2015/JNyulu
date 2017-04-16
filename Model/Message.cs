using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// ʵ����Message ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ������ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// �û���
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// ���Ա���
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Issue
        {
            set { _issue = value; }
            get { return _issue; }
        }
        /// <summary>
        /// �ش�
        /// </summary>
        public string Reply
        {
            set { _reply = value; }
            get { return _reply; }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        #endregion Model

    }
}

