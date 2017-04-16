using System;
namespace JNyuluSoft.Model
{
	/// <summary>
    /// 实体类Video 。(属性说明自动提取数据库字段的描述信息)
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
		/// 自增型ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
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
        /// 班级名称
        /// </summary>
        public string SchoolTermName
        {
            set { _schooltermname = value; }
            get { return _schooltermname; }
        }

		/// <summary>
		/// 图片名称
		/// </summary>
        public string Title
		{
            set { _title = value; }
            get { return _title; }
		}

        /// <summary>
        /// 图片地址
        /// </summary>
        public string CoversUrl
        {
            set { _coversurl = value; }
            get { return _coversurl; }
        }
        /// <summary>
        /// 视频路径
        /// </summary>
        public string MediaUrl 
        {
            set { _mediaurl = value; }
            get { return _mediaurl; }
        }

        /// <summary>
        /// 媒体内容
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
		/// 创建时间
		/// </summary>
		public DateTime UploadTime
		{
			set{ _uploadtime=value;}
			get{return _uploadtime;}
		}
		#endregion Model

	}
}

