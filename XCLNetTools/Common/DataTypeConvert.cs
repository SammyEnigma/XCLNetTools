﻿/*
一：基本信息：
开源协议：https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
项目地址：https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

 */

using System;
using System.Linq;

namespace XCLNetTools.Common
{
    /// <summary>
    /// C#数据类型转换
    /// </summary>
    public static class DataTypeConvert
    {
        #region byte

        /// <summary>
        /// 将source转为byte，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(byte)</param>
        /// <returns>转换后的值</returns>
        public static byte ToByte<T>(T source, byte defaultValue = default(byte))
        {
            return ToByteNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为byte?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static byte? ToByteNull<T>(T source, byte? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            byte result;
            if (byte.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将source转为sbyte，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(sbyte)</param>
        /// <returns>转换后的值</returns>
        public static sbyte ToSbyte<T>(T source, sbyte defaultValue = default(sbyte))
        {
            return ToSbyteNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为sbyte?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static sbyte? ToSbyteNull<T>(T source, sbyte? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            sbyte result;
            if (sbyte.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion byte

        #region int

        /// <summary>
        /// 将source转为int，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(int)</param>
        /// <returns>转换后的值</returns>
        public static int ToInt<T>(T source, int defaultValue = default(int))
        {
            return ToIntNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为int?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static int? ToIntNull<T>(T source, int? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            int result;
            if (int.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串数组转为int?数组（如果某项转换失败，则该项为Null）
        /// </summary>
        public static int?[] GetIntNullArrayByStringArray(string[] str)
        {
            int?[] s = null;
            if (null != str && str.Length > 0)
            {
                s = str.ToList().ConvertAll(k => XCLNetTools.Common.DataTypeConvert.ToIntNull(k)).ToArray();
            }
            return s;
        }

        /// <summary>
        /// 将字符串数组转为INT数组(若某项转换失败则该项为0)
        /// </summary>
        public static int[] GetIntArrayByStringArray(string[] str)
        {
            return GetIntArrayByStringArray(str, 0);
        }

        /// <summary>
        /// 将字符串数组转为INT数组(若某项转换失败则该项为默认值)
        /// </summary>
        public static int[] GetIntArrayByStringArray(string[] str, int defaultValue)
        {
            int[] result = null;
            int?[] intTemp = GetIntNullArrayByStringArray(str);
            if (null != intTemp && intTemp.Length > 0)
            {
                result = intTemp.ToList().ConvertAll(k => (int)(k == null ? defaultValue : k)).ToArray();
            }
            return result;
        }

        /// <summary>
        /// 将source转为uint，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(uint)</param>
        /// <returns>转换后的值</returns>
        public static uint ToUint<T>(T source, uint defaultValue = default(uint))
        {
            return ToUintNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为uint?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static uint? ToUintNull<T>(T source, uint? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            uint result;
            if (uint.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion int

        #region short

        /// <summary>
        /// 将source转为short，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(short)</param>
        /// <returns>转换后的值</returns>
        public static short ToShort<T>(T source, short defaultValue = default(short))
        {
            return ToShortNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为short?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static short? ToShortNull<T>(T source, short? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            short result;
            if (short.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将source转为ushort，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(ushort)</param>
        /// <returns>转换后的值</returns>
        public static ushort ToUshort<T>(T source, ushort defaultValue = default(ushort))
        {
            return ToUshortNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为ushort?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static ushort? ToUshortNull<T>(T source, ushort? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            ushort result;
            if (ushort.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion short

        #region long

        /// <summary>
        /// 将source转为long，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(long)</param>
        /// <returns>转换后的值</returns>
        public static long ToLong<T>(T source, long defaultValue = default(long))
        {
            return ToLongNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为long?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static long? ToLongNull<T>(T source, long? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            long result;
            if (long.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符串数组转为long?数组（如果某项转换失败，则该项为Null）
        /// </summary>
        public static long?[] GetLongNullArrayByStringArray(string[] str)
        {
            long?[] s = null;
            if (null != str && str.Length > 0)
            {
                s = str.ToList().ConvertAll(k => XCLNetTools.Common.DataTypeConvert.ToLongNull(k)).ToArray();
            }
            return s;
        }

        /// <summary>
        /// 将字符串数组转为long数组(若某项转换失败则该项为0)
        /// </summary>
        public static long[] GetLongArrayByStringArray(string[] str)
        {
            return GetLongArrayByStringArray(str, 0);
        }

        /// <summary>
        /// 将字符串数组转为long数组(若某项转换失败则该项为默认值)
        /// </summary>
        public static long[] GetLongArrayByStringArray(string[] str, long defaultValue)
        {
            long[] result = null;
            long?[] longTemp = GetLongNullArrayByStringArray(str);
            if (null != longTemp && longTemp.Length > 0)
            {
                result = longTemp.ToList().ConvertAll(k => (long)(k == null ? defaultValue : k)).ToArray();
            }
            return result;
        }

        /// <summary>
        /// 将source转为ulong，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(ulong)</param>
        /// <returns>转换后的值</returns>
        public static ulong ToUlong<T>(T source, ulong defaultValue = default(ulong))
        {
            return ToUlongNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为ulong?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static ulong? ToUlongNull<T>(T source, ulong? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            ulong result;
            if (ulong.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion long

        #region float

        /// <summary>
        /// 将source转为float，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(float)</param>
        /// <returns>转换后的值</returns>
        public static float ToFloat<T>(T source, float defaultValue = default(float))
        {
            return ToFloatNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为float?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static float? ToFloatNull<T>(T source, float? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            float result;
            if (float.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion float

        #region double

        /// <summary>
        /// 将source转为double，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(double)</param>
        /// <returns>转换后的值</returns>
        public static double ToDouble<T>(T source, double defaultValue = default(double))
        {
            return ToDoubleNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为double?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static double? ToDoubleNull<T>(T source, double? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            double result;
            if (double.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion double

        #region bool

        /// <summary>
        /// 将source转为bool，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(bool)</param>
        /// <returns>转换后的值</returns>
        public static bool ToBool<T>(T source, bool defaultValue = default(bool))
        {
            return ToBoolNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为bool?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static bool? ToBoolNull<T>(T source, bool? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            bool result;
            if (bool.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        /// <summary>
        /// 将字符T/F转为bool，如果为'T'，则true; 否则为false
        /// </summary>
        public static bool ToBoolFromTF(string key)
        {
            return string.Equals("T", key, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// 将【是、否】转换为 Bool
        /// </summary>
        public static bool ConvertYesOrNoToBool(string str)
        {
            return str?.Trim() == "是";
        }

        /// <summary>
        /// 将 Bool 转换为【是、否】
        /// </summary>
        public static string ConvertBoolToYesOrNo(bool? val)
        {
            return val.GetValueOrDefault() ? "是" : "否";
        }

        #endregion bool

        #region decimal

        /// <summary>
        /// 将source转为decimal，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(decimal)</param>
        /// <returns>转换后的值</returns>
        public static decimal ToDecimal<T>(T source, decimal defaultValue = default(decimal))
        {
            return ToDecimalNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为decimal?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static decimal? ToDecimalNull<T>(T source, decimal? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            decimal result;
            if (decimal.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion decimal

        #region string

        /// <summary>
        /// 将source转为string，若source为null或Empty，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(string)</param>
        /// <returns>转换后的值</returns>
        public static string ToStr<T>(T source, string defaultValue = default(string))
        {
            if (null == source)
            {
                return defaultValue;
            }
            return string.IsNullOrEmpty(source.ToString()) ? defaultValue : source.ToString();
        }

        /// <summary>
        /// 将int数组转为string数组
        /// </summary>
        public static string[] GetStringArrayByIntArray(int[] str)
        {
            if (null == str || str.Length == 0)
            {
                return new string[] { };
            }
            return str.ToList().ConvertAll(k =>
            {
                return k.ToString();
            }).ToArray();
        }

        #endregion string

        #region DateTime相关

        /// <summary>
        /// 将source转为DateTime，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为default(DateTime)</param>
        /// <returns>转换后的值</returns>
        public static DateTime ToDateTime<T>(T source, DateTime defaultValue = default(DateTime))
        {
            return ToDateTimeNull(source, defaultValue).Value;
        }

        /// <summary>
        /// 将source转为DateTime?，若source为null或转换失败，则返回defaultValue
        /// </summary>
        /// <typeparam name="T">要转换的类型</typeparam>
        /// <param name="source">要转换的值</param>
        /// <param name="defaultValue">默认值，默认为null</param>
        /// <returns>转换后的值</returns>
        public static DateTime? ToDateTimeNull<T>(T source, DateTime? defaultValue = null)
        {
            if (null == source)
            {
                return defaultValue;
            }
            DateTime result;
            if (DateTime.TryParse(source.ToString(), out result))
            {
                return result;
            }
            return defaultValue;
        }

        #endregion DateTime相关
    }
}