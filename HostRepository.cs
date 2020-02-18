using System;
using System.Collections.Generic;
using belong.Models;

// TODO: Move to its own class and namespace (belong.Repositories)
public interface IHostRepository
{
    Host GetHost (int id);
    IEnumerable<Host> GetHosts();
}

// TODO: Move to its own class and namespace (belong.Repositories)
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

public class HostRepository : IHostRepository
{
    public Host GetHost(int id)
    {
        // connect to sql server and retrieve host by ID
        throw new NotImplementedException();
    }

    public IEnumerable<Host> GetHosts()
    {
        throw new NotImplementedException();
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
