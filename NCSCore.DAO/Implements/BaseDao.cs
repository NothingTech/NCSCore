using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using NCSCore.Dao.Interfaces;
using NCSCore.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Transactions;

namespace NCSCore.Dao.Implements
{
    public class BaseDao<T> : IDisposable, IBaseDao<T> where T : class, new()
    {
        private readonly NCSContext _context;
        private readonly ILogger<NCSContext> _logger;
        public BaseDao(NCSContext context, ILogger<NCSContext> logger)
        {
            this._context = context;
            this._logger = logger;
        }



        public void AddEntity(T entity)
        {
            try
            {
                this._logger.LogInformation("方法：AddEntity" + "参数T:" + entity);

                _context.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：AddEntity" + "参数T:" + entity + "错误信息：" + ex.Message);
            }
        }

        public void AddEntity(IList<T> ilEntity)
        {
            try
            {

                this._logger.LogInformation("方法：AddEntitys" + "参数IList<T>的数量:" + ilEntity.Count);
                _context.Set<T>().AddRange(ilEntity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：AddEntitys" + "参数IList<T>的数量:" + ilEntity.Count + "错误信息：" + ex.Message);
            }
        }

        public IList<T> GetEntities(Expression<Func<T, bool>> where = null)
        {
            try
            {
                this._logger.LogInformation("方法：GetEntities" + "参数where:" + where);
                if (where != null)
                {
                    return _context.Set<T>().Where(where).ToList();
                }
                else
                {
                    return _context.Set<T>().ToList();
                }
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：GetEntities" + "参数where:" + where + "错误信息：" + ex.Message);
                return null;
            }

        }

        public int SaveChanges()
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    _context.SaveChanges();
                    transactionScope.Complete();
                    return 1;
                }
                catch (Exception ex)
                {
                    this._logger.LogError("方法：SaveChanges" + "错误信息：" + ex.Message);
                    return 0;
                }
            }


        }
        public void Dispose()
        {
            try
            {
                this._logger.LogInformation("方法：Dispose");
                _context.Dispose();
            }
            catch (Exception ex)
            {
                this._logger.LogError("方法：Dispose" + "错误信息：" + ex.Message);

            }
        }

        public void RemoveEntity(T entity)
        {
            try
            {
                this._logger.LogInformation("方法：RemoveEntity" + "参数T:" + entity);
                _context.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：RemoveEntity" + "参数T:" + entity + "错误信息：" + ex.Message);
            }
        }
        public void RemoveEntity(IList<T> ilEntity)
        {
            try
            {
                this._logger.LogInformation("方法：RemoveEntity" + "参数T:" + ilEntity);
                _context.Set<T>().RemoveRange(ilEntity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：RemoveEntity" + "参数T:" + ilEntity + "错误信息：" + ex.Message);
            }
        }
        public void EditEntity(T entity)
        {
            try
            {
                this._logger.LogInformation("方法：EditEntity" + "参数T:" + entity);
                _context.Set<T>().Update(entity);
            }
            catch (Exception ex)
            {
                this._logger.LogError("方法：EditEntity" + "参数T:" + entity + "错误信息：" + ex.Message);
            }
        }
        public void EditEntity(IList<T> ilEntity)
        {
            try
            {

                this._logger.LogInformation("方法：EditEntitys" + "参数IList<T>的数量:" + ilEntity.Count);
                _context.Set<T>().UpdateRange(ilEntity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：EditEntitys" + "参数IList<T>的数量:" + ilEntity.Count + "错误信息：" + ex.Message);
            }
        }
        public IList<T> QueryToSql(string Sql)
        {
            try
            {
                this._logger.LogInformation("方法：QueryToSql" + "参数：sql:[" + Sql + "]");
                return _context.Database.SqlQuery<T>(Sql).ToList();
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：QueryToSql" + "参数：sql:[" + Sql + "]" + ",错误信息：" + ex.Message);
                return null;
            }
        }

        public IList<T> QueryToSql(string Sql, params object[] parameters)
        {
            try
            {
                this._logger.LogInformation("方法：QueryToSql" + "参数：sql:[" + Sql + "],[parameters:" + Fomat(parameters) + "]");
                return _context.Database.SqlQuery<T>(Sql, parameters).ToList();
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：QueryToSql" + "参数：sql:[" + Sql + "],[parameters:" + Fomat(parameters) + "],错误信息：" + ex.Message);
                return null;
            }
        }

        public bool ExecuteSql(string Sql, params object[] parameters)
        {
            try
            {
                this._logger.LogInformation("方法：ExecuteSql" + "参数：sql[" + Sql + "]，[parameters:" + Fomat(parameters) + "]");
                return _context.Database.ExecuteSqlCommand(Sql, parameters);
            }
            catch (Exception ex)
            {
                this._logger.LogError("方法：ExecuteSql" + "参数：sql[" + Sql + "],[parameters:" + Fomat(parameters) + "],错误信息：" + ex.Message);
                throw;
            }
        }

        protected string Fomat(params object[] parameters)
        {

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < parameters.Length; i++)
            {
                stringBuilder.Append(((MySql.Data.MySqlClient.MySqlParameter)parameters[0]).ParameterName + "=" +
                    ((MySql.Data.MySqlClient.MySqlParameter)parameters[0]).Value);
            }
            return stringBuilder.ToString();
        }

        public IList<T> QueryToSql(string Sql, params MySqlParameterExt[] parameters)
        {

            MySqlParameter[] mySqlParameters = new MySqlParameter[parameters.Length];
            int i = 0;
            string Where = "";
            try
            {
                foreach (MySqlParameterExt item in parameters)
                {
                    mySqlParameters[i] = item.MySqlParameter;
                    if (i == 0 && item.Conn == 1)
                    {
                        Where = " where ";
                        item.Conn = 0;
                    }
                    Where += FomatConn(item.Conn) + item.MySqlParameter.ParameterName.Substring(1) + FomatOperation(item.Operation) + item.MySqlParameter.ParameterName + " ";
                    i++;
                }
                this._logger.LogInformation("方法：QueryToSql" + "参数：sql[" + Sql + Where + "]，[parameters:" + Fomat(mySqlParameters) + "]");
                return _context.Database.SqlQuery<T>(Sql + Where, mySqlParameters).ToList();
            }
            catch (Exception ex)
            {
                this._logger.LogError("方法：QueryToSql" + "参数：sql[" + Sql + "],[parameters:" + Fomat(mySqlParameters) + "],错误信息：" + ex.Message);
                return null;
            }


        }
        //1:等于 2小于 3小于等于 4不等于 5大于 6大于等于
        private string FomatOperation(int Operation)
        {
            string Op = "";
            switch (Operation)
            {
                case 1: Op = "="; break;
                case 2: Op = "<"; break;
                case 3: Op = "<="; break;
                case 4: Op = "!="; break;
                case 5: Op = ">"; break;
                case 6: Op = ">="; break;
                default:
                    Op = "=";
                    break;
            }
            return Op;
        }

        private string FomatConn(int Conn)
        {
            string strConn = "";
            switch (Conn)
            {
                case 0: strConn = ""; break;
                case 1: strConn = " and "; break;
                case 2: strConn = " or "; break;

                default:
                    strConn = " and ";
                    break;
            }
            return strConn;
        }

        public DataTable QueryToDataTable(string Sql)
        {
            try
            {
                this._logger.LogInformation("方法：QueryToDataTable" + "参数：sql:[" + Sql + "]");
                return _context.Database.SqlQuery(Sql);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：QueryToDataTable" + "参数：sql:[" + Sql + "]" + ",错误信息：" + ex.Message);
                return null;
            }
        }


        #region 事务使用
        public void AddEntity(T entity, ref NCSContext dbcontext)
        {
            try
            {
                this._logger.LogInformation("方法：AddEntity" + "参数T:" + entity);
                _context.Set<T>().Add(entity);
            }
            catch (Exception ex)
            {
                this._logger.LogError("方法：AddEntity" + "参数T:" + entity + "错误信息：" + ex.Message);
            }
        }
        public void AddEntity(IList<T> ilEntity, ref NCSContext dbcontext)
        {
            try
            {

                this._logger.LogInformation("方法：AddEntitys" + "参数IList<T>的数量:" + ilEntity.Count);
                _context.Set<T>().AddRange(ilEntity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：AddEntitys" + "参数IList<T>的数量:" + ilEntity.Count + "错误信息：" + ex.Message);
            }
        }
        public void RemoveEntity(T entity, ref NCSContext dbcontext)
        {
            try
            {
                this._logger.LogInformation("方法：RemoveEntity" + "参数T:" + entity);
                _context.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：RemoveEntity" + "参数T:" + entity + "错误信息：" + ex.Message);
            }
        }
        public void RemoveEntity(IList<T> ilEntity, ref NCSContext dbcontext)
        {
            try
            {
                this._logger.LogInformation("方法：RemoveEntity" + "参数T:" + ilEntity);
                _context.Set<T>().RemoveRange(ilEntity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：RemoveEntity" + "参数T:" + ilEntity + "错误信息：" + ex.Message);
            }
        }
        public void EditEntity(T entity, ref NCSContext dbcontext)
        {
            try
            {
                this._logger.LogInformation("方法：EditEntity" + "参数T:" + entity);
                _context.Set<T>().Update(entity);
            }
            catch (Exception ex)
            {
                this._logger.LogError("方法：EditEntity" + "参数T:" + entity + "错误信息：" + ex.Message);
            }
        }
        public void EditEntity(IList<T> ilEntity, ref NCSContext dbcontext)
        {
            try
            {

                this._logger.LogInformation("方法：EditEntitys" + "参数IList<T>的数量:" + ilEntity.Count);
                _context.Set<T>().UpdateRange(ilEntity);
            }
            catch (Exception ex)
            {

                this._logger.LogError("方法：EditEntitys" + "参数IList<T>的数量:" + ilEntity.Count + "错误信息：" + ex.Message);
            }
        }
        public NCSContext GetNCSContext() 
        {
            return this._context;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="CommandInfo">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        public int ExecuteSqlTran(Dictionary<string, MySqlParameterSql> CommandInfo)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                string Sql = string.Empty;
                MySqlParameter[] parameters = { };
                try
                {
                    int rows=0;
                    foreach (KeyValuePair<string, MySqlParameterSql> kvp in CommandInfo)
                    {
                        MySqlParameterSql mySqlParameterSql = kvp.Value;
                        Sql = kvp.Key;

                        if (mySqlParameterSql.HaveParam)
                        {
                            parameters = mySqlParameterSql.MySqlParameter;
                            Sql = Sql.Replace(mySqlParameterSql.SqlIndex, "");

                            this._logger.LogInformation("方法：ExecuteSqlTran" + "参数：sql[" + Sql + "]，[parameters:" + Fomat(parameters) + "]");
                            rows += _context.Database.ExecuteSql(Sql, parameters);
                        }
                        else 
                        {
                            Sql = Sql.Replace(mySqlParameterSql.SqlIndex, "");

                            this._logger.LogInformation("方法：ExecuteSqlTran" + "参数：sql[" + Sql + "]，[parameters:" + Fomat(parameters) + "]");
                            rows += _context.Database.ExecuteSql(Sql);
                        }                 
                    }
                    tran.Commit();
                    return rows;
                }
                catch (Exception ex)
                {
                    this._logger.LogError("方法：ExecuteSqlTran" + "参数：sql[" + Sql + "],[parameters:" + Fomat(parameters) + "],错误信息：" + ex.Message);
                    tran.Rollback();
                    return 0;
                }
            }
        }
        #endregion
    }
}