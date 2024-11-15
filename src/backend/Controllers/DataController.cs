/* ****************************************************************************
 * File name: WebAppDataController.cs
 *
 * Author: Tamás Kiss
 * Created: Oct/15/2024
 *
 * Last Editor: Tamás Kiss
 * Last Modified: Nov/6/2024
 *
 * Copyright (C) Tamás Kiss, 2024.
 * ************************************************************************* */

using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    public enum Status
    {
        Ok = 0,
        Error = -1,
    }

    public class ErrorMessage
    {
        private ErrorMessage(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }

        public static ErrorMessage NotExist
        {
            get
            {
                return new ErrorMessage("Table or view doesn't exist.");
            }
        }

        public static ErrorMessage FilterNameValueMismatch
        {
            get
            {
                return new ErrorMessage("Number of filter names and filter values must match.");
            }
        }
    }

    public class Result
    {
        public Result()
        {
            Status = Status.Ok;
            Data = new List<IDictionary<string, object>>();
        }

        public Status Status { get; set; }

        public ErrorMessage? ErrorMessage { get; set; }

        public IList<IDictionary<string, object>> Data { get; set; }
    }

    [Route("[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        [HttpGet("{tableName}")]
        // /Data/Table1
        // /Data/Table1?filtername=Col1&filtervalue=aaa&filtername=Col2&filtervalue=bbb
        public ActionResult<Result> Get(string tableName,
            [FromQuery] string[] filterName, [FromQuery] string[] filterValue)
        {
            Result result = new Result();

            if (filterName.Length != filterValue.Length)
            {
                result.Status = Status.Error;
                result.ErrorMessage = ErrorMessage.FilterNameValueMismatch;

                return result;
            }

            // Mock data
            if (tableName == "Test")
            {
                var colNames = new[] { "Col1", "Col2", "Col3" };

                var row1 = new Dictionary<string, object>();

                row1.Add(colNames[0], "Val11");
                row1.Add(colNames[1], "Val12");
                row1.Add(colNames[2], "Val13");

                var row2 = new Dictionary<string, object>();

                row2.Add(colNames[0], "Val21");
                row2.Add(colNames[1], "Val22");
                row2.Add(colNames[2], "Val23");

                result.Data.Add(row1);
                result.Data.Add(row2);

                result.Status = Status.Ok;
            }
            // Real data
            else
            {
                var connStr = App.Instance().Config.GetConnectionString("WebApp");

                // Check if table or view exists
                var tableExists = Db.Read(connStr,
                    string.Format(@"select count(*) as Cnt
                                    from information_schema.tables
                                    where table_name = '{0}' and
                                        (table_type = 'BASE TABLE' or table_type = 'VIEW');", tableName));

                if ((int)tableExists[0]["Cnt"] == 0)
                {
                    result.Status = Status.Error;
                    result.ErrorMessage = ErrorMessage.NotExist;

                    return result;
                }

                // Get table definition
                var columns = Db.Read(connStr,
                    string.Format(@"select column_name as [Name], data_type as [Type], is_nullable as Nullable
                                    from information_schema.columns
                                    where table_name = '{0}'
                                    order by ordinal_position;", tableName));

                // Get data
                var colNames = string.Empty;
                for(var i = 0; i < columns.Count; i++)
                {
                    if (colNames != string.Empty)
                    {
                        colNames += ", ";
                    }

                    colNames += columns[i]["Name"];
                }

                string filters = "1 = 1";
                for (int i = 0; i < filterName.Count(); i++)
                {
                    filters += " and ";
                    filters += filterName[i] + " = '" + filterValue[i] + "'";
                }

                var data = Db.Read(connStr,
                    string.Format(@"select {0}
                                    from {1}
                                    where {2};", colNames, tableName, filters));

                result.Data = data.Serialize();
                result.Status = Status.Ok;
            }

            return result;
        }
    }
}
