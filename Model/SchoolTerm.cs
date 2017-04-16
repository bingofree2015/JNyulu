using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// 实体类SchoolTerm 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class SchoolTerm
	{
		public SchoolTerm()
		{}
		#region Model
		private int _id;
        private int? _gradeid;
        private string _gradename;
		private string _name;
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
		/// 年级ID
		/// </summary>
        public int? GradeID
		{
            set { _gradeid = value; }
            get { return _gradeid; }
		}
        /// <summary>
        /// 年级
        /// </summary>
        public string GradeName
        {
            set { _gradename = value; }
            get { return _gradename; }
        }
		/// <summary>
		/// 班级名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		#endregion Model

	}
}

