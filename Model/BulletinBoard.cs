using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// ʵ����BulletinBoard ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// ������ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// ������ID
        /// </summary>
        public int? Sender
        {
            set { _sender = value; }
            get { return _sender; }
        }
        /// <summary>
        /// ������ID
        /// </summary>
        public int? Receiver
        {
            set { _receiver = value; }
            get { return _receiver; }
        }

        /// <summary>
        /// ������ID����
        /// </summary>
        public int[] Receivers
        {
            set { _receivers = value; }
            get { return _receivers; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string Subject
        {
            set { _subject = value; }
            get { return _subject; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        public string MsgText
        {
            set { _msgtext = value; }
            get { return _msgtext; }
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

