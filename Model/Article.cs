using System;
namespace JNyuluSoft.Model
{
	/// <summary>
    /// 实体类Article 。(属性说明自动提取数据库字段的描述信息)
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
            set { _catalogID = value; }
            get { return _catalogID; }
        }

		/// <summary>
		/// 文章标题
		/// </summary>
        public string Subject
		{
            set { _subject = value; }
            get { return _subject; }
		}

        /// <summary>
        /// 文章内容
        /// </summary>
        public string Context
        {
            set { _context = value; }
            get { return _context; }
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

