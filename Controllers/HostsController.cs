using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using belong.Models;
using Microsoft.Extensions.Logging;

namespace belong.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HostsController : ControllerBase
    {
        public HostsController(IHostRepository repo, ILogger<HostsController> logger) {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Host Get(int id)
        {
            // _logger.LogInformation("Base URL in controller from static class: {0}", UrlBase.Get());
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
        ILogger<HostsController> _logger;
    }
}
