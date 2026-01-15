using CMS.DataEngine;
using System.IO;
using System.Reflection;

namespace CloudinaryDam.Logic.DAL
{
    public class DatabaseUpdater
    {
        private void RunQuery(string resourceName)
        {
            string sqlQuery;
            using (var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream($"CloudinaryDam.DatabaseScripts.{resourceName}"))
            using (var reader = new StreamReader(stream))
            {
                sqlQuery = reader.ReadToEnd();
            }
            ConnectionHelper.ExecuteNonQuery(sqlQuery, [], QueryTypeEnum.SQLQuery);
        }

        public void Update()
        {
            RunQuery("01.CreateTableCloudinaryDamSettings.sql");
        }
    }
}
