using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����SchoolTerm ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ������ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}

		/// <summary>
		/// �꼶ID
		/// </summary>
        public int? GradeID
		{
            set { _gradeid = value; }
            get { return _gradeid; }
		}
        /// <summary>
        /// �꼶
        /// </summary>
        public string GradeName
        {
            set { _gradename = value; }
            get { return _gradename; }
        }
		/// <summary>
		/// �༶����
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// ��������
		/// </summary>
		public DateTime RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		#endregion Model

	}
}

