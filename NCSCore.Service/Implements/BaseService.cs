﻿
using NCSCore.Dao.Implements;
using NCSCore.Dao.Interfaces;
using NCSCore.Entity;
using NCSCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;


namespace OMS.BLL.Implements
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        /// <summary>
        /// 数据库服务
        /// </summary>
        protected IBaseDao<T> _dal;
        protected bool Flag = false;
        public BaseService(IBaseDao<T> dal)
        {
            _dal = dal;
        }

        public bool OMSInsert(T entity, ref string Msg)
        {

            try
            {

                _dal.AddEntity(entity);
                if (SaveChanges() > 0)
                {
                    Flag = true;
                  
                }
                else
                {
                    Flag = false;
  
                }
            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return Flag;
        }

        public bool OMSInsert(IList<T> entitys, ref string Msg)
        {

            try
            {

                _dal.AddEntity(entitys);
                if (SaveChanges() > 0)
                {
                    Flag = true;
                    
                }
                else
                {
                    Flag = false;
             
                }
            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return Flag;
        }
        public bool OMSUpdate(IList<T> entitys, ref string Msg)
        {

            try
            {

                _dal.EditEntity(entitys);
                if (SaveChanges() > 0)
                {
                    Flag = true;
                   
                }
                else
                {
                    Flag = false;

                }
            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return Flag;
        }

        public bool OMSDelete(T entity, ref string Msg)
        {
            try
            {
                _dal.RemoveEntity(entity);
                if (SaveChanges() > 0)
                {
                    Flag = true;
                  
                }
                else
                {
                    Flag = false;
          
                }

            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return Flag;
        }

        public bool OMSDeletes(IList<T> entitys, ref string Msg)
        {
            try
            {
                _dal.RemoveEntity(entitys);
                if (SaveChanges() > 0)
                {
                    Flag = true;
                  
                }
                else
                {
                    Flag = false;
         
                }

            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return Flag;
        }

        public bool OMSUpdate(T entity, ref string Msg)
        {
            try
            {
                _dal.EditEntity(entity);
                if (SaveChanges() > 0)
                {
                    Flag = true;
                   
                }
                else
                {
                    Flag = false;
        
                }

            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return Flag;
        }

        public bool OMSExecuteSql(string Sql, ref string Msg, params object[] parameters)
        {
            try
            {
                _dal.ExecuteSql(Sql, parameters);
               
            }
            catch (Exception)
            {

                Flag = false;

            }
            return Flag;
        }

        public IList<T> OMSGetEntities(ref string Msg, Expression<Func<T, bool>> where = null)
        {

            try
            {
              
                return _dal.GetEntities(where);

            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public IList<T> OMSSelectToSql(string Sql, ref string Msg)
        {
            IList<T> ilQuery = null;
            try
            {
                ilQuery = _dal.QueryToSql(Sql);
                Flag = true;
             

            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return ilQuery;
        }

        public IList<T> OMSSelectToSql(string Sql, ref string Msg, params object[] parameters)
        {
            IList<T> ilQuery = null;
            try
            {
                ilQuery = _dal.QueryToSql(Sql, parameters);
                Flag = true;
            

            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return ilQuery;
        }

        public IList<T> OMSSelectToSql(string Sql, ref string Msg, params MySqlParameterExt[] parameters)
        {
            IList<T> ilQuery = null;
            try
            {
                ilQuery = _dal.QueryToSql(Sql, parameters);
                Flag = true;
               

            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return ilQuery;
        }

        public int SaveChanges()
        {
            return _dal.SaveChanges();

        }

        public DataTable OMSSelectToDataTable(string Sql, ref string Msg)
        {
            DataTable dt = null;
            try
            {
                dt = _dal.QueryToDataTable(Sql);
                Flag = true;
                
            }
            catch (Exception ex)
            {
                Flag = false;
                Msg = ex.Message;
            }
            return dt;
        }

        public object OMSSelectToFirstObject(string Sql, ref string Msg)
        {
            object obj = null;
            try
            {
                DataTable dt = _dal.QueryToDataTable(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj = dt.Rows[0][0];
                }
                Flag = true;
               
            }
            catch (Exception ex)
            {
                Flag = false;
                Msg = ex.Message;
            }
            return obj;
        }

        #region 

        public void OMSInsert(T entity, ref NCSContext dbcontext) 
        {
            _dal.AddEntity(entity, ref dbcontext);
        }

        public void OMSInsert(IList<T> entitys, ref NCSContext dbcontext) 
        {
            _dal.AddEntity(entitys, ref dbcontext);
        }

        public void OMSUpdate(T entity, ref NCSContext dbcontext) 
        {
            _dal.EditEntity(entity, ref dbcontext);
        }

        public void OMSUpdate(IList<T> entitys, ref NCSContext dbcontext) 
        {
            _dal.EditEntity(entitys, ref dbcontext);
        }

        public void OMSDelete(T entity, ref NCSContext dbcontext) 
        {
            _dal.RemoveEntity(entity ,ref dbcontext);
        }

        public void OMSDelete(IList<T> entitys, ref NCSContext dbcontext) 
        {
            _dal.RemoveEntity(entitys, ref dbcontext);
        }
        public NCSContext GetOMSContext() 
        {
            return _dal.GetOMSContext();
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="dbcontext">装载数据后的Context</param>
        /// <returns>true成功，false失败</returns>
        public bool ExecuteTransaction(NCSContext dbcontext, ref string Msg) 
        {
            using (var tran = dbcontext.Database.BeginTransaction())
            {
                try
                {
                    dbcontext.SaveChanges();
                    tran.Commit();
                   
                    Flag = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    Flag = false;
                    Msg = ex.Message;
                }
                return Flag;
            }
        }

        public bool ExecuteSqlTran(Dictionary<string, MySqlParameterSql> commandinfo, ref string Msg)
        {           
            try
            {
                int rows = _dal.ExecuteSqlTran(commandinfo);
                if (rows > 0)
                {
                    Flag = true;
                }                               
                

            }
            catch (Exception ex)
            {

                Flag = false;
                Msg = ex.Message;
            }
            return Flag;
        }
        #endregion


    }
}
