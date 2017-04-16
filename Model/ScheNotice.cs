using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����ScheNotice ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
        /// �û���
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }

        /// <summary>
        /// ѧ������
        /// </summary>
        public string TrueName
		{
            set { _truename = value; }
            get { return _truename; }
		}

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public string Context
        {
            set { _context = value; }
            get { return _context; }
        }

		/// <summary>
        /// ��������
		/// </summary>
		public DateTime CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
		}

		#endregion Model
	}
}

