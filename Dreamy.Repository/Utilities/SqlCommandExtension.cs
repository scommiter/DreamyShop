using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dreamy.Repository.Utilities
{
    public static class SqlCommandExtension
    {
        /// <summary>
        /// Create sql cmd get all record of table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetAllSqlCmd<T>() where T : class
        {
            var tableName = GetTableName<T>();
            return $"SELECT * FROM {tableName}";
        }

        /// <summary>
        /// Create sql cmd get record by id from table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetByIdSqlCmd<T>(int id) where T : class
        {
            var tableName = GetTableName<T>();
            return $"SELECT * FROM {tableName} WHERE Id = {id}";
        }

        /// <summary>
        /// Create sql cmd for delete record from table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string DeleteSqlCmd<T>(int id) where T : class
        {
            var tableName = GetTableName<T>();
            return $"DELETE FROM {tableName} WHERE Id = {id}";
        }

        /// <summary>
        /// Create sql cmd for update record to table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string UpdateSqlCmd<T>(T entity, int id) where T : class
        {
            var props = entity.GetType().GetProperties();
            var tableName = GetTableName<T>();
            string phay;
            string query = $"UPDATE {tableName} SET ";
            foreach (var prop in props)
            {
                if (prop.Name == "Id") continue;
                if (prop.GetGetMethod().IsVirtual)
                {
                    query = query.Substring(0, query.Length - 1);
                    continue;
                }
                phay = "";
                query += $"{phay}{prop.Name}={phay}'{prop.GetValue(entity)}'";
                if (prop != props.Last())
                {
                    query += ",";
                }
            }
            return query + $" WHERE Id = {id}";
        }

        /// <summary>
        /// Create sql cmd for insert multiple record to table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string InsertMultipleSqlCmd<T>(List<T> entites) where T : class
        {
            var tableName = GetTableName<T>();
            var sqlColumnValue = new StringBuilder();
            var sqlColumnName = new StringBuilder();
            foreach (var entity in entites)
            {
                var sqlColumnValueItem = new StringBuilder();
                var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                string quotes = "";
                foreach (var prop in props)
                {
                    if (prop.Name == "Id") continue;
                    if (prop.GetGetMethod().IsVirtual) continue;
                    if (sqlColumnName == null)
                    {
                        sqlColumnName.Append($"{quotes}{prop.Name}");
                    }
                    sqlColumnValueItem.Append($"{quotes}'{prop.GetValue(entity)}'");
                    sqlColumnValue.Append($"{quotes}'{sqlColumnValueItem}'");
                    quotes = ",";
                }
            }
            return $"INSERT INTO {tableName} ({sqlColumnName}) VALUES ({sqlColumnValue})";
        }

        /// <summary>
        /// Create sql cmd for insert record to table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string InsertSqlCmd<T>(T entity) where T : class
        {
            var sqlColumnName = new StringBuilder();
            var sqlColumnValue = new StringBuilder();
            var tableName = GetTableName<T>();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string quotes = "";
            foreach (var prop in props)
            {
                if (prop.Name == "Id") continue;
                if (prop.GetGetMethod().IsVirtual) continue;
                sqlColumnName.Append($"{quotes}{prop.Name}");
                sqlColumnValue.Append($"{quotes}'{prop.GetValue(entity)}'");
                quotes = ",";
            }
            return $"INSERT INTO {tableName} ({sqlColumnName}) VALUES ({sqlColumnValue})";
        }

        public static string RegisterUser<T>(T entity) where T : class
        {
            var sqlColumnName = new StringBuilder();
            var sqlColumnValue = new StringBuilder();
            var tableName = GetTableName<T>();
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string quotes = "";
            foreach (var prop in props)
            {
                if (prop.Name == "Id") continue;
                if (prop.GetGetMethod().IsVirtual) continue;
                sqlColumnName.Append($"{quotes}{prop.Name}");
                sqlColumnName.Append($"{quotes}'{prop.GetValue(entity)}'");
                if (prop.PropertyType.FullName == "System.Byte[]")
                {
                    var t = prop.GetValue(entity);
                    sqlColumnValue.Append($"{quotes}'CONVERT(varbinary, {prop.GetValue(entity)})'");
                }
                else
                {
                    sqlColumnValue.Append($"{quotes}'{prop.GetValue(entity)}'");
                }
                quotes = ",";
            }
            var test = $"INSERT INTO {tableName} ({sqlColumnName}) VALUES ({sqlColumnValue})";
            return $"INSERT INTO {tableName} ({sqlColumnName}) VALUES ({sqlColumnValue})";
        }

        /// <summary>
        /// Create select specific column in specific table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static string CreateSelectSpecificColumnSqlCmd<T>(List<string> columnNames) where T : class
        {
            string tableName = GetTableName<T>();
            string tableShortName = GetTableShortName(tableName);
            return string.Join(", ", columnNames.Select(e => $"{(tableShortName + ".")}{e} {CreateColumnSelectName(e, tableName)}"));
        }

        /// <summary>
        /// Create select all column in specific table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static string CreateSelectColumnSqlCmd<T>() where T : class
        {
            var columnNames = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(property => !property.GetGetMethod().IsVirtual)
                            .Select(property => property.Name)
                            .ToList();
            string tableName = GetTableName<T>();
            string tableShortName = GetTableShortName(tableName);
            return string.Join(", ", columnNames.Select(e => $"{(tableShortName + ".")}{e} {CreateColumnSelectName(e, tableName)}"));
        }

        private static string CreateColumnSelectName(string columnName, string tableName)
        {
            return columnName + "_" + tableName.ToLower();
        }

        /// <summary>
        /// Create from specific table
        /// </summary>
        /// <typeparam name="TableFrom"></typeparam>
        /// <returns></returns>
        public static string CreateFromTableSqlCmd<TableFrom>()
           where TableFrom : class
        {
            var nameTableFrom = GetTableName<TableFrom>();
            var shortNameTableFrom = GetTableShortName(nameTableFrom);
            return $"dbo.{nameTableFrom} {shortNameTableFrom}";
        }

        /// <summary>
        /// Create sql join table with specific column
        /// </summary>
        /// <typeparam name="From"></typeparam>
        /// <typeparam name="To"></typeparam>
        /// <param name="fromColumn"></param>
        /// <param name="toColumn"></param>
        /// <returns></returns>
        public static string CreateJoinTableSqlCmd<From, To>(
            string fromColumn,
            string toColumn)
            where From : class where To : class
        {
            var fromTable = GetTableName<From>();
            var shortNameTableFrom = GetTableShortName(fromTable);
            var toTable = GetTableName<To>();
            var shortNameTableTo = GetTableShortName(toTable);
            return $"dbo.{toTable} {shortNameTableTo} ON {shortNameTableFrom}.{fromColumn} = {shortNameTableTo}.{toColumn}";
        }

        private static string GetTableName<T>() where T : class
        {
            string tableName = typeof(T).GetCustomAttributes(typeof(TableAttribute), true)
                .OfType<TableAttribute>()
                .FirstOrDefault()?.Name ?? typeof(T).Name;
            return tableName;
        }

        /// <summary>
        /// Get uppercase character in name
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private static string GetTableShortName(string tableName)
        {
            Regex regex = new Regex("[^A-Z]");
            return regex.Replace(tableName, "");
        }

        //private readonly static Dictionary<Type, DbDefinition> DB_DEFINITION = new Dictionary<System.Type, DbDefinition>();

        //static DbCommandExtension()
        //{
        //    var entityTypes = Assembly.GetExecutingAssembly().GetTypes().ToArray();
        //    foreach (var entityType in entityTypes)
        //    {
        //        var tableName = entityType.GetField("DB_TABLE_NAME")?.GetValue(entityType) as string;
        //        var columns = entityType.GetField("DB_COLUMNS")?.GetValue(entityType) as List<string>;
        //        if (tableName == null || columns == null)
        //        {
        //            continue;
        //        }
        //        DB_DEFINITION.Add(entityType, new DbDefinition(tableName, columns));
        //    }
        //}

        //private class DbDefinition
        //{
        //    public DbDefinition(string tableName, List<string> columns)
        //    {
        //        TableName = tableName;
        //        Columns = columns;
        //    }

        //    public string TableName { get; }
        //    public List<string> Columns { get; }
        //}
    }
}
