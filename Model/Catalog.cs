using System;
using System.Collections.Generic;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// 实体类Catalog 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Catalog
	{
		public Catalog()
		{}
		#region Model
		private int _id;
        private int _fatherid;
        private string _modulecn;
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
        /// 父级ID
        /// </summary>
        public int FatherID
        {
            set { _fatherid = value; }
            get { return _fatherid; }
        }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleCn
        {
            set { _modulecn = value; }
            get { return _modulecn; }
        }
		/// <summary>
		/// 分类名称
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

