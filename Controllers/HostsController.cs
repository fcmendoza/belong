using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using belong.Models;

namespace belong.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HostsController : ControllerBase
    {
        public HostsController(IHostRepository repo) {
            _repo = repo;
        }

        [HttpGet("{id}")]
        public Host Get(int id)
        {
            var host = _repo.GetHost(id);
            return host;
        }

        [HttpGet]
        public IEnumerable<Host> Get()
        {
            var hosts = _repo.GetHosts();
            return hosts;
        }

        IHostRepository _repo;
    }
}
