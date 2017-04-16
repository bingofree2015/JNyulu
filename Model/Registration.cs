using System;
using System.Collections;
using System.Collections.Generic;

namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����Registration ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Registration
	{
		public Registration()
		{}
		#region Model
		private int _id;
		private int? _employeeid;
        private string _employeename;
        private string _employeepwd;
        private string _truename;
        private string _mobile;
        private string _phone;
        private string _schoolname;
        private int? _gradeid;
        private string _gradename;
        private string _campusname;
        private string _schooltermname;
        private int? _stid;
		private decimal? _schoolfee;
		private int? _status;
        private decimal? _refund;
		private string _reasons;
		private string _assessor;
		private string _remark;
        private string _judgment;

        private IList<ReportCard> _reportcardlist;
        private IList<Syllabus> _syllabuslist;
        
		private DateTime _recorddate = DateTime.Now;
		/// <summary>
		/// ������ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// ע���û�ID
		/// </summary>
		public int? EmployeeID
		{
			set{ _employeeid=value;}
			get{return _employeeid;}
		}
        /// <summary>
        /// ע���û���
        /// </summary>
        public string  EmployeeName
        {
            set { _employeename = value; }
            get { return _employeename; }
        }
        /// <summary>
        /// �û�������
        /// </summary>
        public string  EmployeePwd
        {
            set { _employeepwd = value; }
            get { return _employeepwd; }
        }
        /// <summary>
        /// �û�����
        /// </summary>
        public string TrueName
        {
            set { _truename = value; }
            get { return _truename; }
        }
        /// <summary>
        /// �ҳ��ֻ�
        /// </summary>
        public string Mobile
        {
            set { _mobile = value; }
            get { return _mobile; }
        }
        /// <summary>
        /// ��ͥ�绰
        /// </summary>
        public string Phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        /// <summary>
        /// ԭУ
        /// </summary>
        public string SchoolName
        {
            set { _schoolname = value; }
            get { return _schoolname; }
        }
        /// <summary>
        /// ԭ�꼶ID
        /// </summary>
        public int? GradeID
        {
            set { _gradeid = value; }
            get { return _gradeid; }
        }
        /// <summary>
        /// ԭ�꼶
        /// </summary>
        public string GradeName
        {
            set { _gradename = value; }
            get { return _gradename; }
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
		/// ��������
		/// </summary>
		public decimal? SchoolFee
		{
			set{ _schoolfee=value;}
			get{return _schoolfee;}
		}
		/// <summary>
		/// 0:����;-1:���˿�
		/// </summary>
		public int? Status
		{
			set{ _status=value;}
			get{return _status;}
		}

		/// <summary>
		/// �˻�����
		/// </summary>
        public decimal? Refund
		{
            set { _refund = value; }
            get { return _refund; }
		}

		/// <summary>
		/// �˿�ԭ��
		/// </summary>
		public string Reasons
		{
			set{ _reasons=value;}
			get{return _reasons;}
		}
		/// <summary>
		/// ������
		/// </summary>
		public string Assessor
		{
			set{ _assessor=value;}
			get{return _assessor;}
		}
		/// <summary>
		/// ��ע
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
        /// <summary>
        /// ����
        /// </summary>
        public string Judgment
        {
            set { _judgment = value; }
            get { return _judgment; }
        }
        /// <summary>
        /// �Ծ�
        /// </summary>
        public IList<ReportCard> ReportCardList
        {
            set { _reportcardlist = value; }
            get { return _reportcardlist; }
        }
        /// <summary>
        /// �γ̱�
        /// </summary>
        public IList<Syllabus> SyllabusList
        {
            set { _syllabuslist = value; }
            get { return _syllabuslist; }
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

