using System;
namespace JNyuluSoft.Model
{
    /// <summary>
    /// ʵ����Assessment ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
    /// </summary>
    [Serializable]
    public class Assessment
    {
        public Assessment()
        { }
        #region Model
        private int _id;
        private int? _stid;
        private string _schooltermname;
        private int? _employeeid;
        private string _truename;
        private string _disciplinerank;
        private string _integraterank;
        private string _judgment;
        private DateTime _recorddate;

        /// <summary>
        /// ������ID
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
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
            set { _employeeid = value; }
            get { return _employeeid; }
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
        public string DisciplineRank
        {
            set { _disciplinerank = value; }
            get { return _disciplinerank; }
        }
        /// <summary>
        /// �ۺ�����
        /// </summary>
        public string IntegrateRank
        {
            set { _integraterank = value; }
            get { return _integraterank; }
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
        /// ����ʱ��
        /// </summary>
        public DateTime RecordDate
        {
            set { _recorddate = value; }
            get { return _recorddate; }
        }
        #endregion Model

    }
}

