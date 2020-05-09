
using NCSCore.Dao.Implements;
using NCSCore.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace NCSCore.Service.Interfaces
{
    public interface IBaseService<T> where T : class, new()
    {
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">添加的实体表</param>
        /// <returns>true：添加成功，false：添加失败</returns>
        bool NCSInsert(T entity, ref string Msg);
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entity">添加的实体表</param>
        /// <returns>true：添加成功，false：添加失败</returns>
        bool NCSInsert(IList<T> entitys, ref string Msg);
        /// <summary>
        /// 通过linq语句执行查询
        /// </summary>
        /// <param name="where">linq条件，默认为空</param>
        /// <param name="Msg">返回消息</param>
        /// <returns></returns>
        IList<T> NCSGetEntities(ref string Msg, Expression<Func<T, bool>> where = null);
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="entity">被编辑的实体</param>
        /// <param name="Msg">返回消息</param>
        /// <returns>true成功，false失败</returns>
        bool NCSUpdate(T entity, ref string Msg);
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entity">编辑的实体表</param>
        /// <returns>true：成功，false：失败</returns>
        bool NCSUpdate(IList<T> entitys, ref string Msg);
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="entity">删除的实体</param>
        /// <param name="Msg">返回消息</param>
        /// <returns>true成功，false失败</returns>
        bool NCSDelete(T entity, ref string Msg);
        /// <summary>
        /// 批量删除事件
        /// </summary>
        /// <param name="entity">删除的实体</param>
        /// <param name="Msg">返回消息</param>
        /// <returns>true成功，false失败</returns>
        bool NCSDeletes(IList<T> entitys, ref string Msg);
        /// <summary>
        /// 通过sql语句进行查询
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="Msg">返回消息</param>
        /// <returns>通过sql语句查询出的实体集合</returns>
        IList<T> NCSSelectToSql(string Sql, ref string Msg);
        /// <summary>
        /// 带参数的sql语句查询
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="Msg"></param>
        /// <param name="parameters">参数SqlParameter[]</param>
        /// <returns></returns>
        IList<T> NCSSelectToSql(string Sql, ref string Msg, params object[] parameters);
        /// <summary>
        /// 执行sql语句操作
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="Msg">执行状态消息</param>
        /// <param name="parameters">参数</param>
        /// <returns>true成功，false失败</returns>
        bool NCSExecuteSql(string Sql, ref string Msg, params object[] parameters);
        /// <summary>
        /// 精简sql语句操作
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="Msg">执行状态消息</param>
        /// <param name="parameters">参数</param>
        /// <returns>true成功，false失败</returns>
        IList<T> NCSSelectToSql(string Sql, ref string Msg, params MySqlParameterExt[] parameters);
        /// <summary>
        /// 通过SQL语句查询自定义的表结构内容
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="Msg">执行状态消息</param>
        /// <returns>返回自定义表结构</returns>
        DataTable NCSSelectToDataTable(string Sql, ref string Msg);
        /// <summary>
        /// 通过SQL查询首列首个单元格值
        /// </summary>
        /// <param name="Sql">sql语句</param>
        /// <param name="Msg">执行状态消息</param>
        /// <returns>返回首列单元格值</returns>
        object NCSSelectToFirstObject(string Sql, ref string Msg);

        int SaveChanges();

        #region 事务
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="entity">添加的实体表</param>
        /// <returns>true：添加成功，false：添加失败</returns>
        void NCSInsert(T entity, ref NCSContext dbcontext);
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entity">添加的实体表</param>
        /// <returns>true：添加成功，false：添加失败</returns>
        void NCSInsert(IList<T> entitys, ref NCSContext dbcontext);
        /// <summary>
        /// 编辑事件
        /// </summary>
        /// <param name="entity">被编辑的实体</param>
        /// <param name="Msg">返回消息</param>
        /// <returns>true成功，false失败</returns>
        void NCSUpdate(T entity, ref NCSContext dbcontext);
        /// <summary>
        /// 批量添加数据
        /// </summary>
        /// <param name="entity">编辑的实体表</param>
        /// <returns>true：成功，false：失败</returns>
        void NCSUpdate(IList<T> entitys, ref NCSContext dbcontext);
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="entity">删除的实体</param>
        /// <param name="Msg">返回消息</param>
        /// <returns>true成功，false失败</returns>
        void NCSDelete(T entity, ref NCSContext dbcontext);
        /// <summary>
        /// 批量删除事件
        /// </summary>
        /// <param name="entity">删除的实体</param>
        /// <param name="Msg">返回消息</param>
        /// <returns>true成功，false失败</returns>
        void NCSDelete(IList<T> entitys, ref NCSContext dbcontext);
        /// <summary>
        /// 获取数据驱动上下文
        /// </summary>
        /// <returns></returns>
        NCSContext GetNCSContext();

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="dbcontext">装载数据后的Context</param>
        /// <returns>true成功，false失败</returns>
        bool ExecuteTransaction(NCSContext dbcontext, ref string Msg);

        bool ExecuteSqlTran(Dictionary<string, MySqlParameterSql> commandinfo, ref string Msg);
        #endregion

    }
}
