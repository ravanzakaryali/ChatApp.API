using ChatApp.Core.Interface;
using Microsoft.Extensions.Configuration;
using TableDependency.SqlClient;

namespace ChatApp.Data.Subscription
{
    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class, new()
    {
        private readonly IConfiguration _configuration;
        public DatabaseSubscription(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private SqlTableDependency<T> _tableDependency;
        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("Default"), tableName);
            _tableDependency.OnChanged += (o, e) =>
            {

            };
            _tableDependency.OnError += (o, e) =>
            {

            };
            _tableDependency.Start();   
        }
        ~DatabaseSubscription()
        {
            _tableDependency.Dispose();
        }
    }
}
