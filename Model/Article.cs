using System;
namespace JNyuluSoft.Model
{
	/// <summary>
    /// ʵ����Article ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Article
	{
		public Article()
		{}
		#region Model
        private int _id;
        private int _catalogID;
        private string _subject;
        private string _context;
		private DateTime _recorddate;
		/// <summary>
		/// ������ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}

        /// <summary>
        /// ����ID
        /// </summary>
        public int CatalogID
        {
            set { _catalogID = value; }
            get { return _catalogID; }
        }

		/// <summary>
		/// ���±���
		/// </summary>
        public string Subject
		{
            set { _subject = value; }
            get { return _subject; }
		}

        /// <summary>
        /// ��������
        /// </summary>
        public string Context
        {
            set { _context = value; }
            get { return _context; }
        }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		#endregion Model

	}
}

