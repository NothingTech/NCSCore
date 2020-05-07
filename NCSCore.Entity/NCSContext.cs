using Microsoft.EntityFrameworkCore;
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

        #endregion
    }
}
