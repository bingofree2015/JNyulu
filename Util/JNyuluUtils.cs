using System;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Configuration;

using JNyuluSoft.Model;

namespace JNyuluSoft.Util
{
    public class JNyuluUtils
    {

        public static string MailServerUserName = ConfigurationManager.AppSettings["MailServerUserName"];
        public static string MailServerPassWord = ConfigurationManager.AppSettings["MailServerPassWord"];
        public static string MailServer = ConfigurationManager.AppSettings["MailServer"];

        /// <summary>
        /// byte[] 转换成二进制字符串
        /// </summary>
        /// <param name="permission">权限值</param>
        /// <param name="toBase">转换格式</param>
        public static string PermissionString(byte[] permission, int toBase)
        {
            StringBuilder _stringBuilder = new StringBuilder();

            foreach (byte _b in permission)
            {
                _stringBuilder.Append(Convert.ToString(_b, toBase));
            }

            return _stringBuilder.ToString();
        }

        public static string TXT2Html(string txtString)
        {
            if (string.IsNullOrEmpty(txtString))
                return string.Empty;
            else
            {
                txtString = HttpContext.Current.Server.HtmlEncode(txtString);
                txtString = txtString.Replace("\r\n", "<br/>");
                txtString = txtString.Replace("\n", "<br/>");
                txtString = txtString.Replace(" ", "&nbsp;");

                return txtString;
            }
        }

        /// <summary>
        /// MD5加密字符串处理
        /// </summary>
        /// <param name="Half">加密是16位还是32位；如果为true为16位</param>
        /// <param name="Input">待加密码字符串</param>
        /// <returns></returns>
        public static string MD5(string Input, bool Half)
        {
            string output = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
            if (Half)//16位MD5加密（取32位加密的9~25字符）
                output = output.Substring(8, 16);
            return output;
        }

        public static void WeekOfStartDateEndDate(int year, int week, ref DateTime startDateOfWeek, ref DateTime endDateOfWeek)
        {
            DateTime _startDateOfYear = new DateTime(year, 1, 1);

            startDateOfWeek = _startDateOfYear.AddDays(0 - Convert.ToInt16(_startDateOfYear.DayOfWeek) + 7 * (week - 1));

            endDateOfWeek = startDateOfWeek.AddDays(6);
        }

        #region Excel 转换成DataTable对象

        /// <summary> 
        /// 将指定excel文件中的数据转换成DataTable对象，供应用程序进一步处理 
        /// </summary> 
        /// <param name="filePath"></param> 
        /// <returns></returns> 
        public static DataTable ImportExcel(string filePath)
        {
            DataTable _dataTable = new DataTable();
            bool _canOpen = false;

            OleDbConnection _conn = new OleDbConnection("provider=microsoft.jet.oledb.4.0;" +
            "data source=" + filePath + ";" +
            "extended properties=\"excel 8.0;hdr=yes;imex=1;\"");

            try//尝试数据连接是否可用 
            {
                _conn.Open();
                _conn.Close();
                _canOpen = true;
            }
            catch { }

            if (_canOpen)
            {
                try//如果数据连接可以打开则尝试读入数据 
                {
                    LogHelper.Error("以数据连接方式打开 " + filePath + ", 正在读入数据...");
                    OleDbCommand _cmd = new OleDbCommand("select * from [sheet1$]", _conn);
                    OleDbDataAdapter _adapter = new OleDbDataAdapter(_cmd);

                    _adapter.Fill(_dataTable);
                    _conn.Close();

                }
                catch//如果数据连接可以打开但是读入数据失败，则从文件中提取出工作表的名称，再读入数据 
                {
                    LogHelper.Error("从文件中提取出工作表的名称，再读入数据");

                    string _sheetName = GetSheetName(filePath);
                    if (_sheetName.Length > 0)
                    {
                        OleDbCommand _cmd = new OleDbCommand("select * from [" + _sheetName + "$]", _conn);
                        OleDbDataAdapter _adapter = new OleDbDataAdapter(_cmd);
                        _adapter.Fill(_dataTable);
                        _conn.Close();
                    }
                }
            }
            else
            {
                LogHelper.Error(" 以html字符串的数据转换成DataTable对象");

                StreamReader _sr = File.OpenText(filePath);
                string _textString = _sr.ReadToEnd();
                _sr.Close();
                _dataTable = GetDataTableFromString(_textString);
                _textString = "";
            }
            return _dataTable;
        }

        /// <summary> 
        /// 将指定excel文件中读取第一张工作表的名称 
        /// </summary> 
        /// <param name="filepath"></param> 
        /// <returns></returns> 
        private static string GetSheetName(string filePath)
        {
            string _sheetName = string.Empty;

            FileStream _fs = File.OpenRead(filePath);
            byte[] _fileByte = new byte[_fs.Length];
            _fs.Read(_fileByte, 0, _fileByte.Length);
            _fs.Close();

            byte[] _byte = new byte[]{
                Convert.ToByte(11),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0), 
                Convert.ToByte(11),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0),Convert.ToByte(0), 
                Convert.ToByte(30),Convert.ToByte(16),Convert.ToByte(0),Convert.ToByte(0)
            };

            int _index = GetSheetIndex(_fileByte, _byte);
            if (_index > -1)
            {

                _index += 16 + 12;
                ArrayList _sheetNameList = new ArrayList();

                for (int i = _index; i < _fileByte.Length - 1; i++)
                {
                    byte _temp = _fileByte[i];
                    if (_temp != Convert.ToByte(0))
                        _sheetNameList.Add(_temp);
                    else
                        break;
                }
                byte[] _sheetNameByte = new byte[_sheetNameList.Count];
                for (int i = 0; i < _sheetNameList.Count; i++)
                    _sheetNameByte[i] = Convert.ToByte(_sheetNameList[i]);

                _sheetName = Encoding.Default.GetString(_sheetNameByte);
            }
            return _sheetName;
        }

        /// <summary> 
        /// 将指定html字符串的数据转换成DataTable对象 --根据“<tr><td>”等特殊字符进行处理 
        /// </summary> 
        /// <param name="htmlString">html字符串</param> 
        /// <returns></returns> 
        private static DataTable GetDataTableFromString(string htmlString)
        {
            string _htmlString = htmlString;
            DataTable _dataTable = new DataTable();
            //先处理一下这个字符串，删除第一个<tr>之前合最后一个</tr>之后的部分 
            int _index = _htmlString.IndexOf("<tr");
            if (_index > -1)
                _htmlString = _htmlString.Substring(_index);
            else
                return _dataTable;

            _index = _htmlString.LastIndexOf("</tr>");
            if (_index > -1)
                _htmlString = _htmlString.Substring(0, _index + 5);
            else
                return _dataTable;

            bool _existsSparator = false;
            char _separator = Convert.ToChar("^");

            //如果原字符串中包含分隔符“^”则先把它替换掉 
            if (_htmlString.IndexOf(_separator.ToString()) > -1)
            {
                _existsSparator = true;
                _htmlString = _htmlString.Replace("^", "^$&^");
            }

            //先根据“</tr>”分拆 
            string[] _tmpRow = _htmlString.Replace("</tr>", "^").Split(_separator);

            for (int i = 0; i < _tmpRow.Length - 1; i++)
            {
                DataRow _newRow = _dataTable.NewRow();

                string _rowText = _tmpRow[i];
                if (_rowText.IndexOf("<tr") > -1)
                {
                    _rowText = _rowText.Substring(_rowText.IndexOf("<tr"));
                    if (_rowText.IndexOf("display:none") < 0 || _rowText.IndexOf("display:none") > _rowText.IndexOf(">"))
                    {
                        _rowText = _rowText.Replace("</td>", "^");
                        string[] _fieldText = _rowText.Split(_separator);

                        for (int j = 0; j < _fieldText.Length - 1; j++)
                        {
                            _fieldText[j] = RemoveString(_fieldText[j], "<font>");
                            _index = _fieldText[j].LastIndexOf(">") + 1;
                            if (_index > 0)
                            {
                                string _cellText = _fieldText[j].Substring(_index, _fieldText[j].Length - _index);
                                if (_existsSparator) _cellText = _cellText.Replace("^$&^", "^");
                                if (i == 0)
                                {
                                    string _tmpFieldName = _cellText;
                                    int _sn = 1;
                                    while (_dataTable.Columns.Contains(_tmpFieldName))
                                    {
                                        _tmpFieldName = _cellText + _sn.ToString();
                                        _sn += 1;
                                    }
                                    _dataTable.Columns.Add(_tmpFieldName);
                                }
                                else
                                {
                                    _newRow[j] = _cellText;
                                }
                            }//end of if(_index>0) 
                        }

                        if (i > 0)
                            _dataTable.Rows.Add(_newRow);
                    }
                }
            }

            _dataTable.AcceptChanges();
            return _dataTable;
        }

        /// <summary> 
        /// 从指定html字符串中剔除指定的对象 
        /// </summary> 
        /// <param name="htmlString">html字符串</param> 
        /// <param name="remove">需要剔除的对象--例如输入"<font>"则剔除"<font ???????>"和"</font>>"</param> 
        /// <returns></returns> 
        public static string RemoveString(string htmlString, string remove)
        {
            htmlString = htmlString.Replace(remove.Replace("<", "</"), "");
            htmlString = RemoveStringHead(htmlString, remove);
            return htmlString;
        }

        /// <summary> 
        /// 只供方法removestring()使用 
        /// </summary> 
        /// <returns></returns> 
        private static string RemoveStringHead(string htmlString, string remove)
        {
            //为了方便注释，假设输入参数remove="<font>" 
            if (remove.Length < 1) return htmlString;//参数remove为空：不处理返回 
            if ((remove.Substring(0, 1) != "<") || (remove.Substring(remove.Length - 1) != ">")) return htmlString;//参数remove不是<?????>：不处理返回 

            int _indexStart = htmlString.IndexOf(remove.Replace(">", ""));//查找“<font”的位置 
            int _indexEnd = -1;
            if (_indexStart > -1)
            {
                string _rightString = htmlString.Substring(_indexStart, htmlString.Length - _indexStart);
                _indexEnd = _rightString.IndexOf(">");
                if (_indexEnd > -1)
                    htmlString = htmlString.Substring(0, _indexStart) + htmlString.Substring(_indexStart + _indexEnd + 1);
                if (htmlString.IndexOf(remove.Replace(">", "")) > -1)
                    htmlString = RemoveStringHead(htmlString, remove);
            }
            return htmlString;
        }

        /// <summary> 
        /// 只供方法GetSheetName()使用 
        /// </summary> 
        /// <returns></returns> 
        private static int GetSheetIndex(byte[] findTarget, byte[] findItem)
        {
            int _index = -1;

            int _findItemLength = findItem.Length;
            if (_findItemLength < 1) return -1;
            int _findTargetLength = findTarget.Length;
            if ((_findTargetLength - 1) < _findItemLength) return -1;

            for (int i = _findTargetLength - _findItemLength - 1; i > -1; i--)
            {
                ArrayList _arrayList = new ArrayList();
                int _find = 0;
                for (int j = 0; j < _findItemLength; j++)
                {
                    if (findTarget[i + j] == findItem[j]) _find += 1;
                }
                if (_find == _findItemLength)
                {
                    _index = i;
                    break;
                }
            }
            return _index;
        }

        #endregion

        /// <summary>
        /// 不签名的参数名，格式|参数1|参数2|
        /// </summary>
        public static string NoSignField
        {
            get { return ConfigurationManager.AppSettings["NoSignField"]; }
        }

        /// <summary>
        /// 密钥
        /// </summary>
        public static string AppSecret
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["AppSecret"]); }
        }

        /// <summary>
        /// 应用所在目录
        /// </summary>
        public static string AppPath
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            }
        }

        /// <summary>
        /// 是否检查签名
        /// </summary>
        public static bool CheckSign
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["CheckSign"]); }
        }

        /// <summary>
        /// 当前域名地址
        /// </summary>
        public static string Domain
        {
            get
            {
                string _domain = ConfigurationManager.AppSettings["Domain"];

                return _domain??string.Empty;
            }
        }



        /// <summary>
        /// 上传文件目录
        /// </summary>
        public static string UploadFilePath
        {
            get
            {
                string _uploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"];
                if (string.IsNullOrEmpty(_uploadFilePath))
                    return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                else
                {
                    if (_uploadFilePath.IndexOf(@":\") < 0)
                    {
                        return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + _uploadFilePath;
                    }
                    else
                    {
                        return _uploadFilePath;
                    }
                }
            }
        }

        /// <summary>
        /// 临时文件目录
        /// </summary>
        public static string TempUploadFilePath
        {
            get
            {
                string _tempUploadFilePath = ConfigurationManager.AppSettings["TempUploadFilePath"];
                if (string.IsNullOrEmpty(_tempUploadFilePath))
                    return AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                else
                {
                    if (_tempUploadFilePath.IndexOf(@":\") < 0)
                    {
                        return AppDomain.CurrentDomain.SetupInformation.ApplicationBase + _tempUploadFilePath;
                    }
                    else
                    {
                        return _tempUploadFilePath;
                    }
                }
            }
        }
    }
}
