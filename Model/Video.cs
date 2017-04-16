using System;
namespace JNyuluSoft.Model
{
	/// <summary>
    /// ʵ����Video ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Video
	{
		public Video()
		{}
		#region Model
        private int _id;
        private int _stid;
        private string _schooltermname;
        private string _title;
        private string _coversurl;
        private string _mediaurl;
        private string _mediahtml;
        private int _sortnum;
        private int _permit;
		private DateTime _uploadtime;
		/// <summary>
		/// ������ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
        /// �༶����
        /// </summary>
        public string SchoolTermName
        {
            set { _schooltermname = value; }
            get { return _schooltermname; }
        }

		/// <summary>
		/// ͼƬ����
		/// </summary>
        public string Title
		{
            set { _title = value; }
            get { return _title; }
		}

        /// <summary>
        /// ͼƬ��ַ
        /// </summary>
        public string CoversUrl
        {
            set { _coversurl = value; }
            get { return _coversurl; }
        }
        /// <summary>
        /// ��Ƶ·��
        /// </summary>
        public string MediaUrl 
        {
            set { _mediaurl = value; }
            get { return _mediaurl; }
        }

        /// <summary>
        /// ý������
        /// </summary>
        public string MediaHtml
        {
            set { _mediahtml = value; }
            get { return _mediahtml; }
        }

        public int SortNum
        {
            set { _sortnum = value; }
            get { return _sortnum; }
        }

        public int Permit
        {
            set { _permit = value; }
            get { return _permit; }
        }

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime UploadTime
		{
			set{ _uploadtime=value;}
			get{return _uploadtime;}
		}
		#endregion Model

	}
}

