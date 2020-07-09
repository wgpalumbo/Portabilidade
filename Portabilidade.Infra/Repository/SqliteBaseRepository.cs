using System;
using System.IO;
using Microsoft.Data.Sqlite;

namespace Portabilidade.Infra.Repository
{
    public class SqliteBaseRepository
    {
        public static string DbFile
        {
            get { return Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\Portabilidade.Infra\Data\Portabilidade.sqlite")); }
        }

        public static SqliteConnection SimpleDbConnection() => new SqliteConnection("Data Source=" + DbFile);
    }
}
