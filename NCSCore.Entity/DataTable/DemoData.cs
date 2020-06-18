using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NCSCore.Entity.DataTable
{
   public class DemoData
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; }
    }
}
