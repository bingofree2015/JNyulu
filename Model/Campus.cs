using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// 实体类Campus 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Campus
	{
        public Campus()
		{}
		#region Model
		private int _id;
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
		/// 校区名称
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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

