using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Text.RegularExpressions;

namespace DreamyShop.Common.Extensions
{
    public static class SqlCommandExtension
    {
        /// <summary>
        /// Create select all column in specific table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="columnNames"></param>
        /// <returns></returns>
        public static string CreateSelectColumnSqlCmd<T>() where T : class
        {
            var columnNames = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                            .Where(property => !property.PropertyType.IsGenericType && !property.GetGetMethod().IsVirtual)
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
