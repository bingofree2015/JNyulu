using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JNyuluSoft.Model
{
    [Serializable]
    public class Result
    {
        #region Model
        private int _code;
        private object _data;

        /// <summary>
        /// 信息代码
        /// </summary>
        public int Code
        {
            set { _code = value; }
            get { return _code; }
        }

        /// <summary>
        /// 内容体
        /// </summary>
        public object Data
        {
            set { _data = value; }
            get { return _data; }
        }
        #endregion Model
    }

}
