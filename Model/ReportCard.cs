using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����ReportCard ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class ReportCard
	{
		public ReportCard()
		{}
		#region Model
		private int _id;
        private int? _stid;
        private string _schooltermname;
		private int? _employeeid;
        private string _employeename;
        private string _truename;
		private DateTime? _examdate;
        private string _testpaperurl;
		private DateTime _recorddate;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
        /// <summary>
        /// �༶ID
        /// </summary>
        public int? STID
        {
            set { _stid = value; }
            get { return _stid; }
        }
        /// <summary>
        /// �༶����
        /// </summary>
        public string SchoolTermName
        {
            set { _schooltermname = value; }
            get { return _schooltermname; }
        }

		/// <summary>
		/// ѧ��ID
		/// </summary>
		public int? EmployeeID
		{
			set{ _employeeid=value;}
			get{return _employeeid;}
		}
		/// <summary>
		/// ѧ��ѧ��
		/// </summary>
        public string EmployeeName
		{
            set { _employeename = value; }
            get { return _employeename; }
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
		/// ��������
		/// </summary>
		public DateTime? ExamDate
		{
			set{ _examdate=value;}
			get{return _examdate;}
        }
        /// <summary>
        /// �Ծ�·�� 
        /// </summary>
        public string TestPaperUrl
        {
            set {
                if (!string.IsNullOrEmpty(value) && !value.ToLower().Contains("testpaperfiles"))
                {
                    _testpaperurl = "/UploadFiles/testPaperFiles/" + value;
                }
                else { _testpaperurl = value; }
            }
            get {
                return _testpaperurl; 
            }
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

