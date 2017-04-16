using System;
using System.Globalization;
using System.Data;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Collections;

using JNyuluSoft.Model;
using JNyuluSoft.Service;

namespace JNyuluSoft.Web
{
    public class FormatHelper
    {
        public string EscapeXML(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text.Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public string ToHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return EscapeXML(text).Replace("\r\n", "<br/>").Replace("\n", "<br/>").Replace("\"", "&quot;").Replace("  ", "&nbsp;&nbsp;");
        }

        public static string ClearTag(string text, string tag)
        {
            string _reString = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                _reString = Regex.Replace(text.Trim(), @"<" + tag + "\\s*(.+?)</" + tag + ">", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);

            }
            return _reString;
        }

        /**
        * ת��ΪHTML���ֱ���.<br/>
        */
        public static String htmlTextDecoder(String src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return "";
            }

            String dst = src;
            dst = dst.Replace("&lt;", "<")
                .Replace("&rt;", ">")
                .Replace("&quot;", "\"")
                .Replace("&#039;", "'")
                .Replace("&nbsp;", " ")
                .Replace("<br/>", "\r\n")
                .Replace("<br/>", "\r")
                .Replace("<br/>", "\n");

            return dst;
        }

        //���ʱ��Ŀ�ȣ���ʽ����Сʱ�����Ӻ��루�������С��λ��
        public static string DateFormat(DateTime dateTime)
        {
            string _dateFormat = string.Empty;

            DateTime _startTime = DateTime.Now;
            DateTime _endTime = dateTime;
            TimeSpan _runLength = _startTime.Subtract(_endTime);
            TimeSpan _lowts = new TimeSpan(0, 0, 60);

            if (_runLength < _lowts)
            {
                _dateFormat = _runLength.Seconds + "��ǰ";
                return _dateFormat;
            }

            TimeSpan _hights = new TimeSpan(0, 60, 0);
            if (_runLength > _lowts && _runLength < _hights)
            {
                _dateFormat = _runLength.Minutes + "����ǰ";
                return _dateFormat;
            }

            _lowts = _hights;
            _hights = new TimeSpan(24, 0, 0);
            if (_runLength > _lowts && _runLength < _hights)
            {
                _dateFormat = _runLength.Hours + "Сʱǰ";
                return _dateFormat;
            }

            _lowts = _hights;
            _hights = new TimeSpan(7, 0, 0, 0);
            if (_runLength > _lowts && _runLength < _hights)
            {
                _dateFormat = _runLength.Days + "��ǰ";
                return _dateFormat;
            }

            _dateFormat = _endTime.ToString("yyyy-MM-dd hh:mm");
            return _dateFormat;
        }

        public static String htmlTextEncoder(String src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return "";
            }
            String dst = HttpContext.Current.Server.HtmlEncode(src);

            return dst;
        }

        public static string wipeScript(string htmlString)
        {
            htmlString = Regex.Replace(htmlString, @"<script[\s\s]+</script *>", string.Empty, RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @" href *= *[\s\s]*script *:", string.Empty, RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @" on[\s\s]*=", string.Empty, RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<iframe[\s\s]+</iframe *>", string.Empty, RegexOptions.IgnoreCase);
            htmlString = Regex.Replace(htmlString, @"<frameset[\s\s]+</frameset *>", string.Empty, RegexOptions.IgnoreCase);

            return htmlString;
        }

        public static string UrlEncoder(String src)
        {
            if (String.IsNullOrEmpty(src))
            {
                return "";
            }
            String dst = HttpContext.Current.Server.UrlEncode(src);

            return dst;
        }

        public static int GetWeekOfYear(DateTime curDate)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(curDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
        }

        public static string DayOfWeekName(int dayOfWeek) 
        {
            dayOfWeek = dayOfWeek % 7;
            string _dayOfWeekName = string.Empty;  
            switch (dayOfWeek)
            {
                case 1:
                    _dayOfWeekName = "������";
                    break;
                case 2:
                    _dayOfWeekName = "����һ";
                    break;
                case 3:
                    _dayOfWeekName = "���ڶ�";
                    break;
                case 4:
                    _dayOfWeekName = "������";
                    break;
                case 5:
                    _dayOfWeekName = "������";
                    break;
                case 6:
                    _dayOfWeekName = "������";
                    break;
                case 0:
                    _dayOfWeekName = "������";
                    break;
                default:
                    break;
            }
            return _dayOfWeekName;
        }

        public static string PeriodName(string period)
        {
            string _periodName = string.Empty;
            switch (period)
            {
                case "am":
                    _periodName = "����";
                    break;
                case "pm":
                    _periodName = "����";
                    break;
                case "xnight":
                    _periodName = "����";
                    break;
                default:
                    break;
            }
            return _periodName;
        }

        public static string NumberName(int number)
        {
            string _numberName = string.Empty;
            switch (number)
            {
                case 1:
                    _numberName = "��һ��";
                    break;
                case 2:
                    _numberName = "�ڶ���";
                    break;
                case 3:
                    _numberName = "������";
                    break;
                case 4:
                    _numberName = "���Ľ�";
                    break;
                default:
                    break;
            }
            return _numberName;
        }

        public static string StatusName(int status)
        {
            string _statusName = string.Empty;
            switch (status)
            {
                case -1:
                    _statusName = "���˿�";
                    break;
                case 0:
                    _statusName = "����";
                    break;
                default:
                    break;
            }
            return _statusName;
        }

        public static string SCGStatusName(int status)
        {
            string _scgStatusName = string.Empty;
            switch (status)
            {
                case 0:
                    _scgStatusName = "���ڱ���";
                    break;
                case 1:
                    _scgStatusName = "��������";
                    break;
                default:
                    break;
            }
            return _scgStatusName;
        }

        public static string CatalogName(int catalogID)
        {
            string _catalogName = string.Empty;
            switch (catalogID)
            {
                case 1:
                    _catalogName = "����ͼƬ��";
                    break;
                case 2:
                    _catalogName = "�ײ�ͼƬ��";
                    break;
                case 3:
                    _catalogName = "�ֻ��õ�Ƭ";
                    break;
                case 4:
                    _catalogName = "Ư�����";
                    break;
                default:
                    break;
            }
            return _catalogName;
        }

        public static string DelayName(int delay)
        {
            string _delayName = string.Empty;
            switch (delay)
            {
                case 0:
                    _delayName = "����";
                    break;
                case 1:
                    _delayName = "�ٵ�";
                    break;
                case 2:
                    _delayName = "����";
                    break;
                case 3:
                    _delayName = "ȱ��";
                    break;
                default:
                    break;
            }
            return _delayName;
        }

        public string LeftString(string text, int len)
        {

            String _reString = String.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                int _nLength = 0;
                char[] _strCharSet = text.ToCharArray();
                for (int i = 0; i < text.Length; i++)
                {
                    if (i > len)
                        break;
 
                    if ((int)_strCharSet[i] == 63)
                    {
                        _nLength += 1;
                    }
                }
                len = len - _nLength;

                _reString = text.Length > len ? text.Substring(0, len) + "..." : text;

            }
            return _reString;
        }
    }
}
