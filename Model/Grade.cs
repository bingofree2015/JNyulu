using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����Grade ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Grade
	{
        public Grade()
		{}
		#region Model
		private int _id;
        private string _sn;
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
        /// �꼶���
        /// </summary>
        public string SN
        {
            set { _sn = value; }
            get { return _sn; }
        }
		/// <summary>
		/// �꼶����
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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

