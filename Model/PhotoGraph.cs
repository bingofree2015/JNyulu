using System;
namespace JNyuluSoft.Model
{
	/// <summary>
    /// 实体类PhotoGraph 。(属性说明自动提取数据库字段的描述信息)
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
		/// 自增型ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CatalogID
        {
            set { _catalogid = value; }
            get { return _catalogid; }
        }

		/// <summary>
		/// 图片名称
		/// </summary>
        public string Subject
		{
            set { _subject = value; }
            get { return _subject; }
		}

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }

        /// <summary>
        /// 链接地址
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
		/// 创建时间
		/// </summary>
		public DateTime RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		#endregion Model

	}
}

