using System;
using System.Collections.Generic;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����Catalog ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
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
		/// ������ID
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
        /// <summary>
        /// ����ID
        /// </summary>
        public int FatherID
        {
            set { _fatherid = value; }
            get { return _fatherid; }
        }
        /// <summary>
        /// ģ������
        /// </summary>
        public string ModuleCn
        {
            set { _modulecn = value; }
            get { return _modulecn; }
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

