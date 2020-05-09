using MySql.Data.MySqlClient;
using NCSCore.Dao.Implements;
using NCSCore.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq.Expressions;

namespace NCSCore.Dao.Interfaces
{
    public interface IBaseDao<T> where T : class, new()
    {
        /// <summary>
        /// 单表添加
        /// </summary>
        /// <param name="entity">添加的表实体</param>
        void AddEntity(T entity);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="ilEntity">批量数据集合</param>
        void AddEntity(IList<T> ilEntity);

        /// <summary>
        /// 通过linq方式查询实体集合
        /// </summary>
        /// <param name="where">linq条件,默认为空</param>
        /// <returns>结果集合</returns>
        IList<T> GetEntities(Expression<Func<T, bool>> where = null);
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">数据实体</param>
        void RemoveEntity(T entity);
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="ilEntity">批量数据集合</param>
        void RemoveEntity(IList<T> ilEntity);
        /// <summary>
        /// 编辑实体
        /// </summary>
        /// <param name="entity">数据实体</param>
        void EditEntity(T entity);
        /// <summary>
        /// 编辑多个实体
        /// </summary>
        /// <param name="ilEntity">批量数据集合</param>
        void EditEntity(IList<T> ilEntity);
        /// <summary>
        /// 保存修改返回状态
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 无参数sql语句操作
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <returns></returns>
        IList<T> QueryToSql(string Sql);
        /// <summary>
        /// 有参数sql语句操作
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        IList<T> QueryToSql(string Sql, params object[] parameters);
        /// <summary>
        /// 执行除查询以外的sql操作
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>true成功，false失败</returns>
        bool ExecuteSql(string Sql, params object[] parameters);
        /// <summary>
        /// 纯sql语句以及参数，无需写where
        /// </summary>
        /// <param name="Sql">sql语句[select * from table]，注意不要写 where后参数</param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<T> QueryToSql(string Sql, params MySqlParameterExt[] mySqlParameterExts);
        /// <summary>
        /// 查询自定义返回结果
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <returns></returns>
        DataTable QueryToDataTable(string Sql);


        #region 事务使用
        /// <summary>
        /// 单表添加
        /// </summary>
        /// <param name="entity">添加的表实体</param>
        void AddEntity(T entity,ref NCSContext dbcontext);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="ilEntity">批量数据集合</param>
        void AddEntity(IList<T> ilEntity, ref NCSContext dbcontext);
     
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">数据实体</param>
        void RemoveEntity(T entity, ref NCSContext dbcontext);
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="ilEntity">批量数据集合</param>
        void RemoveEntity(IList<T> ilEntity, ref NCSContext dbcontext);
        /// <summary>
        /// 编辑实体
        /// </summary>
        /// <param name="entity">数据实体</param>
        void EditEntity(T entity, ref NCSContext dbcontext);
        /// <summary>
        /// 编辑多个实体
        /// </summary>
        /// <param name="ilEntity">批量数据集合</param>
        void EditEntity(IList<T> ilEntity, ref NCSContext dbcontext);
        /// <summary>
        /// 获取数据驱动上下文
        /// </summary>
        /// <returns></returns>
        NCSContext GetNCSContext();

        int ExecuteSqlTran(Dictionary<string, MySqlParameterSql> CommandInfo);
        #endregion
    }
}
