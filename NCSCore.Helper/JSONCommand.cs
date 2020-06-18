using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text.Unicode;

namespace NCSCore.Helper
{
    public class JSONCommand
    {
        /// <summary>
        /// 强类型转JSON字符串
        /// </summary>
        /// <param name="obj">强类型，建议使用 Hashtable，IList,DataTable,Dictionary</param>
        /// <returns>json字符串</returns>
        public static string ObjectToJson(object obj)
        {
            var options = new JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);
            if (obj.GetType().Name == "DataTable")
            {
                DataTable table = (DataTable)obj;
                IList<Dictionary<string, object>> ilDataTable = new List<Dictionary<string, object>>();
                foreach (DataRow dr in table.Rows)
                {
                    Dictionary<string, object> dicColumn = new Dictionary<string, object>();
                    foreach (DataColumn dc in table.Columns)
                    {
                        dicColumn.Add(dc.ColumnName, dr[dc.ColumnName]);
                    }
                    ilDataTable.Add(dicColumn);
                }
                obj = ilDataTable;
            }
            return JsonSerializer.Serialize(obj, options);
        }
        /// <summary>
        /// Json转强类型
        /// </summary>
        /// <typeparam name="T">转换成的类型</typeparam>
        /// <param name="Json">Json字符串</param>
        /// <returns>强类型</returns>
        public static T JsonToObject<T>(string Json)
        {
            var options = new JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);
            return JsonSerializer.Deserialize<T>(Json);
        }
        /// <summary>
        /// Json转字典
        /// </summary>
        /// <param name="Json">Json字符串</param>
        /// <returns>字典数据格式</returns>
        public static Dictionary<string, object> JsonToDictionary(string Json)
        {
            var options = new JsonSerializerOptions();
            options.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(UnicodeRanges.All);
            return JsonSerializer.Deserialize<Dictionary<string, object>>(Json);
        }
        /// <summary>
        /// datatable转集合
        /// </summary>
        /// <param name="dtObj">datata</param>
        /// <returns>返回集合数据</returns>
        public static IList<Dictionary<string, object>> DataTableToList(DataTable dtObj)
        {

            IList<Dictionary<string, object>> ilDataTable = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dtObj.Rows)
            {
                Dictionary<string, object> dicColumn = new Dictionary<string, object>();
                foreach (DataColumn dc in dtObj.Columns)
                {
                    dicColumn.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                ilDataTable.Add(dicColumn);

            }
            return ilDataTable;
        }

    }
}
