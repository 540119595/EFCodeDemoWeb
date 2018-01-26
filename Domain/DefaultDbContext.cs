using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Domain.Models.Sys;
using System.Reflection;
using Domain.Models;

namespace Domain
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext(DbContextOptions options) : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypes = Assembly.Load(new AssemblyName("Domain")).GetTypes()
                .Where(type => !string.IsNullOrWhiteSpace(type.Namespace))//命名空间校验
                .Where(type => type.GetTypeInfo().IsClass)
                .Where(type => type.GetTypeInfo().BaseType != null)//检索对象的类型信息，然后可以使用该信息获取接口的类型信息
                .Where(type => type.GetTypeInfo().BaseType.Equals(typeof(BaseEntity)))//
                .Where(type => typeof(BaseEntity).IsAssignableFrom(type)).ToList();//验证实例是否可以分配给当前类型的实例

            foreach (var entityType in entityTypes)
            {
                //  防止重复附加模型，否则会在生成指令中报错
                if (modelBuilder.Model.FindEntityType(entityType) == null)
                    modelBuilder.Model.AddEntityType(entityType);
            }
        }

        /// <summary>
        /// 编辑实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int Edit<T>(T entity) where T : class
        {
            Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="where"></param>
        /// <returns></returns>
        public int Delete<T>(Expression<Func<T, bool>> @where) where T : class
        {
            //依托于Z.EntityFramework.Plus
            //Set<T>().Where(@where).Delete();
            var list = Set<T>().Where(@where).ToList();
            RemoveRange(list);
            return SaveChanges();
        }

        /// <summary>
        /// 根据列明更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public int Update<T>(T model, params string[] updateColumns) where T : class
        {
            if (updateColumns != null && updateColumns.Length > 0)
            {
                if (Entry(model).State == EntityState.Added ||
                    Entry(model).State == EntityState.Detached) Set<T>().Attach(model);
                foreach (var propertyName in updateColumns)
                {
                    Entry(model).Property(propertyName).IsModified = true;
                }
            }
            else
            {
                Entry(model).State = EntityState.Modified;
            }
            return SaveChanges();
        }
    }
}
