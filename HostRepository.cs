using System;
using System.Collections.Generic;
using System.Linq;
using belong.Models;
using Microsoft.EntityFrameworkCore;

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
        public DateTime CreatedOn { get; set; }
    }
}
