using System;
namespace JNyuluSoft.Model
{
	/// <summary>
	/// ʵ����Campus ��(����˵���Զ���ȡ���ݿ��ֶε�������Ϣ)
	/// </summary>
	[Serializable]
	public class Campus
	{
        public Campus()
		{}
		#region Model
		private int _id;
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
		/// У������
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

