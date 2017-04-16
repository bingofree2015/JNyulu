using System;
namespace JNyuluSoft.Model
{
	/// <summary>
    /// ʵ����PhotoGraph ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class PhotoGraph
	{
		public PhotoGraph()
		{}
		#region Model
        private int _id;
        private int _catalogid;
        private string _subject;
        private string _imageurl;
        private string _linkurl;
        private int _sortnum;
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
            set { _catalogid = value; }
            get { return _catalogid; }
        }

		/// <summary>
		/// ͼƬ����
		/// </summary>
        public string Subject
		{
            set { _subject = value; }
            get { return _subject; }
		}

        /// <summary>
        /// ͼƬ��ַ
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }

        /// <summary>
        /// ���ӵ�ַ
        /// </summary>
        public string LinkUrl
        {
            set { _linkurl = value; }
            get { return _linkurl; }
        }

        public int SortNum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
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

