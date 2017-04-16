using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;

namespace JNyuluSoft.Service
{
    public class Paginated
    {
        /// <summary>
        /// 创建并显示分页器
        /// <td><a href="#">&lt;&lt;</a></td> <td><a href="#">1</a></td> <span class="current"><a href="#">2</a></td> <td><a href="#">3</a></td> <td><a href="#">4</a></td> <td><a href="#">5</a></td> <td><a href="#">&gt;&gt;</a></td>
        /// <td><a href="#">&lt;</a></td><td><a href="#">&lt;&lt;</a></td><td><a class="hover" href="#">1</a></td><td><a href="#">2</a></td><td><a href="#">3</a></td><td><a href="#">4</a></td><td><a href="#">5</a></td><td><a href="#">6</a></td><td><a href="#">7</a></td><td><a href="#">8</a></td><td><a href="#">9</a></td><td><a href="#">10</a></td><td><a href="#">&gt;</a></td><td><a href="#">&gt;&gt;</a></td>
        /// </summary>
        public static String BuildPager(String uri, int totalRecords, int currentPage, int pageSize)
        {
            String _ltlShowPager = String.Empty;

            int alter = 4;
            int startPage = 1;
            int endPage = currentPage + alter;
            int totalPages = Paginated.CalculateTotalPages(totalRecords, pageSize);

            if (currentPage > alter)
            {
                startPage = currentPage - alter;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
            }

            String strTemp = String.Empty;

            strTemp = "<li><a href=\"" + uri + "\">{1}</a></li> ";

            StringBuilder sb = new StringBuilder("");
            if (currentPage > startPage)
            {
                sb.Append(string.Format(strTemp, currentPage - 1, "&lt;"));
            }
            else
            {
                sb.Append("<li><a href=\"#\">&lt;</a></li>");
            }

            for (int i = startPage; i <= endPage; i++)
            {
                if (currentPage == i)
                {
                    sb.Append("<li><a href=\"#\">" + i + "</a></li>");
                }
                else
                {
                    sb.Append(string.Format(strTemp, i, "" + i + ""));
                }
            }

            if (currentPage < endPage)
            {
                sb.Append(string.Format(strTemp, currentPage + 1, "&gt;"));
            }
            else
            {
                sb.Append("<li><a href=\"#\">&gt;</a></li>");
            }

            _ltlShowPager = sb.ToString();

            return _ltlShowPager;
        }

        public static String BuildPager1(String uri, int totalRecords, int currentPage, int pageSize)
        {
            String _ltlShowPager = String.Empty;

            int alter = 4;
            int startPage = 1;
            int endPage = currentPage + alter;
            int totalPages = Paginated.CalculateTotalPages(totalRecords, pageSize);

            if (currentPage > alter)
            {
                startPage = currentPage - alter;
            }

            if (endPage > totalPages)
            {
                endPage = totalPages;
            }

            String strTemp = String.Empty;

            strTemp = "<a class=\"{2}\" href=\"" + uri + "\">{1}</a> ";

            StringBuilder sb = new StringBuilder("");
            if (currentPage > startPage)
            {
                sb.Append(string.Format(strTemp, currentPage - 1, "&nbsp;", "pre"));
            }
            else
            {
                sb.Append("<a class=\"pre\" href=\"#\">&nbsp;</a>");
            }

            for (int i = startPage; i <= endPage; i++)
            {
                if (currentPage == i)
                {
                    sb.Append("<a class=\"on\" href=\"#\">" + i + "</a>");
                }
                else
                {
                    sb.Append(string.Format(strTemp, i, "" + i + "", ""));
                }
            }

            if (currentPage < endPage)
            {
                sb.Append(string.Format(strTemp, currentPage + 1, "&nbsp;", "next"));
            }
            else
            {
                sb.Append("<a class=\"next\" href=\"#\"&nbsp;</a>");
            }

            _ltlShowPager = sb.ToString();

            return _ltlShowPager;
        }

        /**/
        /// <summary>
        /// 计算总页数
        /// </summary>
        /// <param name="totalRecords">总记录数</param>
        /// <param name="pageSize">每页记录数</param>
        private static int CalculateTotalPages(int totalRecords, int pageSize)
        {
            int totalPagesAvailable;

            totalPagesAvailable = totalRecords / pageSize;

            //由于C#的整形除法 会把所有余数舍入为0，所以需要判断是否需要加1
            if ((totalRecords % pageSize) > 0)
                totalPagesAvailable++;

            return totalPagesAvailable;
        }

    }
}
