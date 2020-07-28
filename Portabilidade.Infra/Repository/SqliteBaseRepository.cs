using Microsoft.Data.Sqlite;
using System.IO;

namespace Portabilidade.Infra.Repository
{
    public class SqliteBaseRepository
    {
        protected string GetDbFile()
        {
            string localDB = "C:/netcorefontes/PortabilidadeContext/Portabilidade.Infra/Data/Portabilidade.sqlite";
            //return Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\Portabilidade.Infra\Data\Portabilidade.sqlite")); 
            return localDB;
        }

        protected SqliteConnection SimpleDbConnection() => new SqliteConnection("Data Source=" + GetDbFile());
    }
}
