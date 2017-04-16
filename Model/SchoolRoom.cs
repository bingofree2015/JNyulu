using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// 实体类SchoolRoom 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class SchoolRoom
	{
		public SchoolRoom()
		{}
		#region Model
		private int _id;
		private int _campusid;
        private string _campusname;
		private string _name;
        private int _totalnum;
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
		/// 校区ID
		/// </summary>
		public int CampusID
		{
			set{ _campusid=value;}
			get{return _campusid;}
		}

        /// <summary>
		/// 校区名称
		/// </summary>
		public string CampusName
		{
            set { _campusname = value; }
            get { return _campusname; }
		}
        
		/// <summary>
		/// 教室名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}

        /// <summary>
		/// 教室人数
		/// </summary>
        public int TotalNum
		{
            set { _totalnum = value; }
            get { return _totalnum; }
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

