using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// 实体类ScheNotice 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class ScheNotice
	{
		public ScheNotice()
		{}
		#region Model
		private int _id;
        private string _username;
        private string _truename;
        private string _context;
		private DateTime _createtime;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// 学生姓名
        /// </summary>
        public string TrueName
		{
            set { _truename = value; }
            get { return _truename; }
		}

        /// <summary>
        /// 上下文信息
        /// </summary>
        public string Context
        {
            set { _context = value; }
            get { return _context; }
        }

		/// <summary>
        /// 创建日期
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}

		#endregion Model
	}
}

