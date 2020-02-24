using System;
using System.Collections.Generic;
using System.Linq;
using belong.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// TODO: Move to its own file and namespace.
public class BelongDbContext : DbContext {
    public BelongDbContext(DbContextOptions<BelongDbContext> options) 
        : base(options) {
    }

    // Find a way to make this plural as it has to match the db table.
    public DbSet<Host> Host { get; set; }
}

// TODO: Move to its own file and namespace (belong.Repositories)
public interface IHostRepository
{
    Host GetHost (int id);
    IEnumerable<Host> GetHosts();
}

// TODO: Move to its own file and namespace (belong.Repositories)
public class HostDapperRepository : IHostRepository
{
    public HostDapperRepository(IConfiguration configuration) {
        _configuration = configuration;
    }

    public Host GetHost(int id) {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("BelongDb"))) {
            connection.Open();
            var host = connection.QueryFirstOrDefault<Host>(@"SELECT ID, Name + ' Dapper' as Name, CreatedOn FROM Host WHERE ID = @id", new {id = id});
            return host;
        }
    }

    public IEnumerable<Host> GetHosts() {
        using (var connection = new SqlConnection(_configuration.GetConnectionString("BelongDb"))) {
            connection.Open();
            var hosts = connection.Query<Host>(@"SELECT TOP 5 ID, Name + ' Dapper' as Name, CreatedOn FROM Host (NOLOCK)");
            return hosts;
        }
    }

    IConfiguration _configuration;
}

// TODO: Move to its own file and namespace (belong.Repositories)
public class HostSqlRepository : IHostRepository
{
    public HostSqlRepository(BelongDbContext context) {
        _context = context;
    }

    public Host GetHost(int id) {
        var host = _context.Host.Find(id); // Connect to sql server and retrieve host by ID
        return host;
    }

    public IEnumerable<Host> GetHosts() {
        var hosts = _context.Host.Take(5);
        return hosts.ToList();
    }

    private readonly BelongDbContext _context;
}

// TODO: Move to its own file and namespace (belong.Repositories)
public class HostInMemoryRepository : IHostRepository
{
    public Host GetHost(int id)
    {
        return new Host {
            ID = id, 
            Name = "Jon Host", 
            CreatedOn = DateTime.Now.AddDays(-10)
        };
    }

    public IEnumerable<Host> GetHosts()
    {
        return new List<Host>() {
            new Host {
                ID = 1, 
                Name = "Jon Host", 
                CreatedOn = DateTime.Now.AddDays(-10)
            }, 
            new Host {
                ID = 2, 
                Name = "Jane Host", 
                CreatedOn = DateTime.Now.AddDays(-10)
            }
        };
    }
}

// TODO: Move to its own class and namespace (belong.Models)
namespace belong.Models 
{
    public class Host {
        public int ID { get; set; }
        public string Name { get; set; }
        public string HousesUri {
            get {
                return string.Format("{0}/hosts/{1}/houses", UrlBase.Get(), ID);
            }
        }
        public DateTime CreatedOn { get; set; }
    }

    public class House {
        public int Id { get; set; }
        public int HostId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
