using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����Syllabus ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Syllabus
	{
		public Syllabus()
		{}
		#region Model
		private int _id;
        private int? _stid;
        private string _schooltermname;
		private int? _employeeid;
        private string _employeename;
        private string _truename;
        private string _syllabusurl;
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
        /// �γ̱�·�� 
        /// </summary>
        public string SyllabusUrl
        {
            set {
                if (!string.IsNullOrEmpty(value) && !value.ToLower().Contains("syllabusfiles"))
                {
                    _syllabusurl = "/UploadFiles/syllabusFiles/" + value;
                }
                else { _syllabusurl = value; }
            }
            get {
                return _syllabusurl; 
            }
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

