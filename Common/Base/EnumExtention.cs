using System;
using System.Reflection;

namespace Common.Base
{
    /// <summary>
    /// 扩展枚举EnumDisplayName特性显示获取
    /// <para>作用：直接使用Enum.Value.GetDisplayName()获取显示信息</para>
    /// </summary>
    public static class EnumExtention
    {
        /// <summary>
        /// 获取枚举设置的EnumDisplayName特性显示值
        /// </summary>
        /// <param name="enumValue">扩展枚举</param>
        /// <returns>特性显示值</returns>
        public static string GetDisplayName(this Enum enumValue)
        {
            string sResult = "";
            //1
            //FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            //EnumDisplayNameAttribute enumDisplayNameAttribute = (EnumDisplayNameAttribute)(field.GetCustomAttributes(false).FirstOrDefault(x => x is EnumDisplayNameAttribute));
            //Type type = enumValue.GetType()
            //2
            FieldInfo field = enumValue.GetType().GetField(enumValue.ToString());
            EnumDisplayNameAttribute enumDisplayNameAttribute = Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute)) as EnumDisplayNameAttribute;
            if (null != enumDisplayNameAttribute)
                sResult = enumDisplayNameAttribute.DisplayName;
            else
                sResult = enumValue.ToString();
            return sResult;
        }

        /// <summary>
        /// 获取枚举设置的EnumDisplayCode特性显示值
        /// </summary>
        /// <returns>特性显示值</returns>
        public static string GetDisplayCode()
        {
            string sResult = "";

            //2
            //this._type = (ArticleType)Enum.Parse(typeof(ArticleType), ds.Tables[0].Rows[0]["Type"].ToString());
            //EnumDisplayNameAttribute enumDisplayNameAttribute = Attribute.GetCustomAttribute(field, typeof(EnumDisplayNameAttribute)) as EnumDisplayNameAttribute;
            //if (null != enumDisplayNameAttribute)
            //    sResult = enumDisplayNameAttribute.DisplayName;
            //else
            //    sResult = enumValue.ToString();
            return sResult;
        }
    }

    /// <summary>
    /// 枚举显示特性
    /// <para>作用：方便获取枚举值的显示信息</para>
    /// </summary>
    public class EnumDisplayNameAttribute : Attribute
    {
        public string DisplayName { get; private set; }

        /// <summary>
        /// 枚举显示特性
        /// </summary>
        /// <param name="displayName">显示文字</param>
        public EnumDisplayNameAttribute(string displayName)
        {
            this.DisplayName = displayName;
        }
    }

    //*************** 公用枚举 ***************//

    /// <summary>
    /// Action活动操作枚举
    /// </summary>
    public enum OperationEnum
    {
        [EnumDisplayName("读取")]
        READ = 0,
        [EnumDisplayName("新增")]
        ADDNEW = 1,
        [EnumDisplayName("更新")]
        UPDATE = 10,
        [EnumDisplayName("删除")]
        DELETE = 11,
        [EnumDisplayName("执行")]
        EXECUTE = 100,
        [EnumDisplayName("其他")]
        OTHER = 1000
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum UserTypeEnum
    {
        [EnumDisplayName("用户")]
        PERSONNEL = 1,
        [EnumDisplayName("系统用户")]
        SYSUSER = 99,
        [EnumDisplayName("其他")]
        OTHER = 0
    }

    /// <summary>
    /// 角色类型
    /// </summary>
    public enum RoleTypeEnum
    {
        [EnumDisplayName("自定义")]
        CUSTOMIZE = 1,
        [EnumDisplayName("预设")]
        PRESET = 0
    }

    /// <summary>
    /// 新闻动态类型
    /// </summary>
    public enum NewsTypeEnum
    {
        [EnumDisplayName("默认")]
        DEFAULT = 0,
        [EnumDisplayName("神农新闻")]
        SNNEWS = 1,
        [EnumDisplayName("行业动态")]
        HYNEWS = 2,
        [EnumDisplayName("精彩视频")]
        VIDEO = 3
    }

    /// <summary>
    /// 文章状态类型
    /// </summary>
    public enum ArticleStateEnum
    {
        [EnumDisplayName("保存")]
        SAVED = 1,
        [EnumDisplayName("未发布")]
        UNPUBLISHED = 10,
        [EnumDisplayName("已发布")]
        PUBLISHED = 11,
        [EnumDisplayName("删除")]
        DELETED = 100
    }

    /// <summary>
    /// 文章状态类型
    /// </summary>
    public enum UserGroupEnum
    {
        [EnumDisplayName("神农集团")]
        SNGROUP = 1,
        [EnumDisplayName("EAS")]
        EAS = 2,
        [EnumDisplayName("客户")]
        CUSTOMER = 3,
        [EnumDisplayName("游客")]
        DELETED = 0
    }
}
