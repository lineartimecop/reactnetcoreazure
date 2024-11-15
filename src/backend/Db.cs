/* ****************************************************************************
 * File name: Db.cs
 *
 * Author: Tamás Kiss
 * Created: Nov/7/2024
 *
 * Last Editor: Tamás Kiss
 * Last Modified: Nov/7/2024
 *
 * Copyright (C) Tamás Kiss, 2024.
 * ************************************************************************* */

using Microsoft.Data.SqlClient;

namespace WebApp
{
    public abstract class TableRow
    {
        public abstract object this[string column] { get; }

        public abstract IDictionary<string, object> Serialize();
    }

    public abstract class Table
    {
        public abstract TableRow this[int idx] { get; }

        public abstract int Count { get; }

        public abstract IList<IDictionary<string, object>> Serialize();
    }

    class TableRowImpl : TableRow
    {
        public void Add(string column, object cell)
        {
            _cells.Add(column, cell);
        }

        public override object this[string column]
        {
            get
            {
                return _cells[column];
            }
        }

        public override IDictionary<string, object> Serialize()
        {
            return _cells;
        }

        private IDictionary<string, object> _cells = new Dictionary<string, object>();
    }

    class TableImpl : Table
    {
        public void Add(TableRow row)
        {
            _rows.Add(row);
        }

        public override TableRow this[int idx]
        {
            get
            {
                return _rows[idx];
            }
        }

        public override int Count
        {
            get
            {
                return _rows.Count;
            }
        }

        public override IList<IDictionary<string, object>> Serialize()
        {
            IList<IDictionary<string, object>> result = new List<IDictionary<string, object>>();

            for(var i = 0; i < _rows.Count; i++)
            {
                result.Add(_rows[i].Serialize());
            }

            return result;
        }

        private IList<TableRow> _rows = new List<TableRow>();
    }

    public class Db
    {
        public static int Write(string connectionString, string sql)
        {
            var cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(connectionString);
            cmd.CommandText = sql;

            return cmd.ExecuteNonQuery();
        }

        public static Table Read(string connectionString, string sql)
        {
            var result = new TableImpl();

            var cmd = new SqlCommand();
            cmd.Connection = new SqlConnection(connectionString);
            cmd.Connection.Open();
            cmd.CommandText = sql;

            var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                var row = new TableRowImpl();

                for(var i = 0; i < rdr.FieldCount; i++)
                {
                    row.Add(rdr.GetName(i), rdr[i]);
                }

                result.Add(row);
            }

            return result;
        }
    }
}
