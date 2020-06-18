using Microsoft.EntityFrameworkCore;
using NCSCore.Entity.DataTable;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCSCore.Entity
{
   public class NCSContext : DbContext
    {
        public NCSContext()
        {
        }
        public NCSContext(DbContextOptions<NCSContext> options)
            : base(options)
        {
        }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            base.OnConfiguring(optionsBuilder);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        #region 数据集
        //变量名一定要与数据库名相同，如果不写则无法使用EF core的方法，但是可以用自定义的sql查询
        public DbSet<DemoData> demo { get; set; }
        #endregion
    }
}
