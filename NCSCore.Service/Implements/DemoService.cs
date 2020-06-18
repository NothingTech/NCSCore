using NCSCore.Dao.Interfaces;
using NCSCore.Entity.DataTable;
using NCSCore.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace NCSCore.Service.Implements
{
    public class DemoService : BaseService<DemoData>, IDemoService
    {
        public DemoService(IBaseDao<DemoData> dal) : base(dal)
        {
          
        }
        //如果需要调用其他业务类可以使用下面这种构造方式
        //private IDemoService2 _demoService2;
        //private IDemoService3 _demoService3;
        //public HRAttendanceCalendarService(IBaseDao<DemoData> dal, IDemoService2 DemoService2, IDemoService3 DemoService3) : base(dal)
        //{
        //    _demoService2 = DemoService2;
        //    IDemoService3 = DemoService3;
        //}
        public void InsertDemo(DemoData demo)
        {
            string msg = "";
            NCSInsert(demo, ref msg);
        }
    }
}
