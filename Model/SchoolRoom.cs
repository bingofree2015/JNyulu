using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����SchoolRoom ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ������ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// У��ID
		/// </summary>
		public int CampusID
		{
			set{ _campusid=value;}
			get{return _campusid;}
		}

        /// <summary>
		/// У������
		/// </summary>
		public string CampusName
		{
            set { _campusname = value; }
            get { return _campusname; }
		}
        
		/// <summary>
		/// ��������
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}

        /// <summary>
		/// ��������
		/// </summary>
        public int TotalNum
		{
            set { _totalnum = value; }
            get { return _totalnum; }
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public DateTime RecordDate
		{
			set{ _recorddate=value;}
			get{return _recorddate;}
		}
		#endregion Model

	}
}

