using MySql.Data.MySqlClient;
namespace NCSCore.Dao.Implements
{
    public static class MySqlParameter_Ext
    {




        /// <summary>
        /// 扩展MySqlParameter类，不用再写详细参数
        /// </summary>
        /// <param name="mySqlParameter"></param>
        /// <param name="Operation">判断符号默认为等于，[1:等于 2小于 3小于等于 4不等于 5大于 6大于等于]</param>
        /// <param name="Conn">连接符号默认无,[1:and 2:or]</param>
        /// <returns></returns>
        public static MySqlParameterExt Ext(this MySqlParameter mySqlParameter, int Operation = 1, int Conn = 1)
        {
            MySqlParameterExt mySqlParameterExt = new MySqlParameterExt();
            mySqlParameterExt.MySqlParameter = mySqlParameter;
            mySqlParameterExt.Operation = Operation;
            mySqlParameterExt.Conn = Conn;
            return mySqlParameterExt;
        }
    }

    public class MySqlParameterExt
    {

        public MySqlParameter MySqlParameter { get; set; }
        /// <summary>
        /// 1:等于 2小于 3小于等于 4不等于 5大于 6大于等于
        /// </summary>
        public int Operation { get; set; }

        public int Conn { get; set; }
    }


    public class MySqlParameterSql
    {

        public MySqlParameter[] MySqlParameter { get; set; }
        
        /// <summary>
        /// 参数标志 true 有参数 false无参数
        /// </summary>
        public bool HaveParam { get; set; }

        /// <summary>
        /// 参数索引标志
        /// </summary>   
        public string SqlIndex { get; set; }
    }
}
