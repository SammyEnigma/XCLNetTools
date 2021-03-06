﻿/*
一：基本信息：
开源协议：https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
项目地址：https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

 */

using System.Text;

namespace XCLNetTools.Language
{
    /// <summary>
    /// 中文处理
    /// </summary>
    public static class CN
    {
        /// <summary>
        /// 将汉字转化为全拼
        /// </summary>
        /// <param name="strChinese">汉字</param>
        /// <returns>全拼</returns>
        public static string ConvertToAllSpell(string strChinese)
        {
            byte[] array;
            string returnstr = "";
            int chrasc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] nowchar = strChinese.ToCharArray();
            for (int j = 0; j < nowchar.Length; j++)
            {
                if (XCLNetTools.Common.Consts.ChineseRegex.IsMatch(nowchar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(nowchar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrasc = i1 * 256 + i2 - 65536;
                    if (chrasc > 0 && chrasc < 160)
                    {
                        returnstr += nowchar[j];
                    }
                    else
                    {
                        for (int i = (XCLNetTools.Common.Consts.PinyinValue.Length - 1); i >= 0; i--)
                        {
                            if (XCLNetTools.Common.Consts.PinyinValue[i] <= chrasc)
                            {
                                returnstr += XCLNetTools.Common.Consts.Pinyin[i];
                                break;
                            }
                        }
                    }
                }
                else
                {
                    returnstr += nowchar[j].ToString();
                }
            }
            return returnstr;
        }

        /// <summary>
        /// 将汉字转化为拼音首字母（大写）
        /// </summary>
        /// <param name="strChinese">汉字</param>
        /// <returns>拼音首字母</returns>
        public static string ConvertToFirstSpell(string strChinese)
        {
            int len = strChinese.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += GetFirstSpell(strChinese.Substring(i, 1));
            }
            return myStr;
        }

        /// <summary>
        /// 获取第一个汉字的首字母（大写）；
        /// </summary>
        /// <param name="charChinese">汉字</param>
        /// <returns>首字母</returns>
        public static string GetFirstSpell(string charChinese)
        {
            byte[] arrCN = Encoding.Default.GetBytes(charChinese);
            if (arrCN.Length > 1)
            {
                int area = (short)arrCN[0];
                int pos = (short)arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        byte[] bytes = new byte[] { (byte)(65 + i) };
                        return Encoding.Default.GetString(bytes, 0, bytes.Length);
                    }
                }
                return "*";
            }
            else return charChinese;
        }

        /// <summary>
        /// 获取第一个汉字的拼音
        /// </summary>
        /// <param name="charChinese">汉字</param>
        /// <returns>拼音</returns>
        public static string ConvertFirstSpell(string charChinese)
        {
            byte[] array;
            string returnstr = "";
            int chrasc = 0;
            int i1 = 0;
            int i2 = 0;
            char[] nowchar = charChinese.ToCharArray();
            for (int j = 0; j < 1; j++)
            {
                if (XCLNetTools.Common.Consts.ChineseRegex.IsMatch(nowchar[j].ToString()))
                {
                    array = System.Text.Encoding.Default.GetBytes(nowchar[j].ToString());
                    i1 = (short)(array[0]);
                    i2 = (short)(array[1]);
                    chrasc = i1 * 256 + i2 - 65536;
                    if (chrasc > 0 && chrasc < 160)
                    {
                        returnstr += nowchar[j];
                    }
                    else
                    {
                        for (int i = (XCLNetTools.Common.Consts.PinyinValue.Length - 1); i >= 0; i--)
                        {
                            if (XCLNetTools.Common.Consts.PinyinValue[i] <= chrasc)
                            {
                                returnstr += XCLNetTools.Common.Consts.Pinyin[i];
                                break;
                            }
                        }
                    }
                }
                else
                {
                    returnstr += nowchar[j].ToString();
                }
            }
            return returnstr;
        }
    }
}