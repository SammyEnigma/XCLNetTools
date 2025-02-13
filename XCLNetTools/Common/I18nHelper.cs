﻿using I18Next.Net;
using I18Next.Net.Backends;
using I18Next.Net.Plugins;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Web;
using XCLNetTools.Entity;

namespace XCLNetTools.Common
{
    /// <summary>
    /// 多语言帮助类
    /// </summary>
    public static class I18nHelper
    {
        public const string 简体中文语言代码 = "zh-CN";

        /// <summary>
        /// 常见语言列表
        /// </summary>
        public enum LanguageEnum
        {
            [Description(简体中文语言代码)]
            简体中文,

            [Description("zh-TW")]
            繁体中文,

            [Description("en-US")]
            英语,

            [Description("fr-FR")]
            法语,

            [Description("de-DE")]
            德语,

            [Description("es-ES")]
            西班牙语,

            [Description("ja-JP")]
            日语,

            [Description("ko-KR")]
            韩语
        }

        /// <summary>
        /// 根据当前环境获取当前语言，如：zh-CN，顺序为：get 或 post 中的 language 参数 -> cookie 中的 language 值
        /// </summary>
        public static string GetCurrentLanguage(LanguageEnum? defaultValue = null)
        {
            var name = "language";

            if (null != HttpContext.Current)
            {
                //从get/post中获取
                var langFromQuery = GetStandardLanguageCode(XCLNetTools.StringHander.FormHelper.GetString(name));
                if (!string.IsNullOrWhiteSpace(langFromQuery) && !string.IsNullOrWhiteSpace(XCLNetTools.Enum.EnumHelper.GetEnumTextByDescription(typeof(LanguageEnum), langFromQuery)))
                {
                    return langFromQuery;
                }

                //从cookie中获取
                var langFromCookie = GetStandardLanguageCode(XCLNetTools.Http.CookieHelper.GetCookies(name));
                if (!string.IsNullOrWhiteSpace(langFromCookie) && !string.IsNullOrWhiteSpace(XCLNetTools.Enum.EnumHelper.GetEnumTextByDescription(typeof(LanguageEnum), langFromCookie)))
                {
                    return langFromCookie;
                }
            }

            return null == defaultValue ? string.Empty : XCLNetTools.Enum.EnumHelper.GetEnumDesc(defaultValue);
        }

        /// <summary>
        /// 将语言代码转换为标准的语言代码，比如：zh-cn -> zh-CN, EN -> en
        /// </summary>
        public static string GetStandardLanguageCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                return string.Empty;
            }
            code = code.Trim().ToLower();
            if (!new Regex("^(([a-z]{2})|([a-z]{2}-[a-z]{2}))$").IsMatch(code))
            {
                return string.Empty;
            }
            var arr = code.Split('-');
            if (arr.Length == 2)
            {
                return $"{arr[0]}-{arr[1].ToUpper()}";
            }
            return arr[0];
        }

        /// <summary>
        /// 创建多语言实例
        /// </summary>
        public static I18NextNet CreateI18nInstance(List<Tuple<LanguageEnum, string, string>> transList)
        {
            var backend = new InMemoryBackend();

            transList.ForEach(k =>
            {
                backend.AddTranslation(XCLNetTools.Enum.EnumHelper.GetEnumDesc(k.Item1), "translation", k.Item2, k.Item3);
            });

            var translator = new DefaultTranslator(backend);
            var i18Next = new I18NextNet(backend, translator);
            i18Next.SetFallbackLanguages("zh-CN");
            return i18Next;
        }

        /// <summary>
        /// 根据自定义配置的 json 返回翻译配置列表
        /// </summary>
        public static List<I18NCustomConfigItem> GetI18NCustomConfigList(string json)
        {
            try
            {
                var lst = XCLNetTools.Serialize.JSON.DeSerialize<List<I18NCustomConfigItem>>(json);
                return lst ?? new List<I18NCustomConfigItem>();
            }
            catch
            {
                return new List<I18NCustomConfigItem>();
            }
        }

        /// <summary>
        /// 根据指定的语言和key返回自定义翻译配置中的值
        /// </summary>
        public static string GetI18NCustomConfigValue(string configJson, string language, string key, string defaultValue = null)
        {
            var lst = GetI18NCustomConfigList(configJson);
            var item = lst.Find(k => string.Equals(k.Language, language, StringComparison.OrdinalIgnoreCase) && string.Equals(k.Key, key, StringComparison.OrdinalIgnoreCase));
            return string.IsNullOrWhiteSpace(item?.Value) ? defaultValue : item.Value;
        }
    }
}