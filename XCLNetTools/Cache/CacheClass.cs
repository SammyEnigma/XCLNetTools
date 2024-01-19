﻿/*
一：基本信息：
开源协议：https://github.com/xucongli1989/XCLNetTools/blob/master/LICENSE
项目地址：https://github.com/xucongli1989/XCLNetTools
Create By: XCL @ 2012

 */

using System;
using System.Collections;
using System.Web;

namespace XCLNetTools.Cache
{
    /// <summary>
    /// 缓存相关的操作类
    /// </summary>
    public static class CacheClass
    {
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">缓存key名</param>
        /// <returns>该缓存的值</returns>
        public static object GetCache(string CacheKey)
        {
            return HttpRuntime.Cache[CacheKey];
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">缓存key名</param>
        /// <param name="objObject">缓存key值</param>
        public static void SetCache(string CacheKey, object objObject)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject);
        }

        /// <summary>
        /// 设置当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey">缓存key名</param>
        /// <param name="objObject">缓存key值</param>
        /// <param name="absoluteExpiration">绝对过期时间，指定缓存项在何时过期。</param>
        /// <param name="slidingExpiration">滑动过期时间，指定缓存项在多长时间内没有访问后过期。可以将其设置为 TimeSpan.Zero，表示不使用滑动过期。</param>
        public static void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(CacheKey, objObject, null, absoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="key">缓存key名</param>
        public static void Clear(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// 指定缓存是否存在
        /// </summary>
        /// <param name="key">缓存名</param>
        /// <returns>true:存在</returns>
        public static bool Exists(string key)
        {
            return HttpRuntime.Cache[key] != null;
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            System.Web.Caching.Cache _cache = HttpRuntime.Cache;
            IDictionaryEnumerator CacheEnum = _cache.GetEnumerator();
            while (CacheEnum.MoveNext())
            {
                _cache.Remove(CacheEnum.Key.ToString());
            }
        }
    }
}