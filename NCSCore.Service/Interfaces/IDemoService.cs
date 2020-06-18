using System;
using System.Collections.Generic;
using System.Text;
using NCSCore.Entity;
using NCSCore.Entity.DataTable;

namespace NCSCore.Service.Interfaces
{
   public interface IDemoService
    {
        public void InsertDemo(DemoData demo);
    }
}
