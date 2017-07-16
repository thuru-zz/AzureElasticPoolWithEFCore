using AzureElasticPoolWithEFCore.DataContext;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AzureElasticPoolWithEFCore.Application.Services
{
    public interface IContextService
    {
        MassRoverContext MassRoverContext { get; }
    }

    public class ContextService : IContextService
    {
        private readonly HttpContext _httpContext;
        private readonly ConnectionSettings _connectionSettings;

        public ContextService(IHttpContextAccessor httpContentAccessor, IOptions<ConnectionSettings> settings)
        {
            _httpContext = httpContentAccessor.HttpContext;
            _connectionSettings = settings.Value;
        }

        public MassRoverContext MassRoverContext
        {
            get
            {

                var sqlConnectionBuilder = new SqlConnectionStringBuilder(_connectionSettings.ZeroDbConnectionString);

                // determine the mapping based on some value. In this sample the database mapping is determined by a header value tenantid
                sqlConnectionBuilder.Remove("Database");
                sqlConnectionBuilder.Add("Database", _httpContext.Request.Headers["tenantid"].ToString());

                var dbOptionsBuidler = new DbContextOptionsBuilder<MassRoverContext>();
                dbOptionsBuidler.UseSqlServer(sqlConnectionBuilder.ConnectionString);

                return new MassRoverContext(dbOptionsBuidler);
            }
        }
    }
}
