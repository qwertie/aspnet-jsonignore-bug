using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcWebApp
{
    [Route("api/controller")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        ExampleDbContext _context;
        public ExampleController(ExampleDbContext context)
        {
            _context = context;
        }

        //// GET: api/controller
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/controller/5
        [HttpGet("{id}")]
        public Example Get(int id)
        {
            if (_context.Examples.Count() == 0) {
                _context.Examples.Add(new Example { Child = new ExampleChild() });
                _context.SaveChanges();
            }
            return _context.Examples.First();
        }

        [HttpGet]
        public IEnumerable<Example> GetAll()
        {
            return _context.Examples.ToList();
        }

        //// POST api/controller
        //[HttpPost]
        //public int Post([FromBody] CreateEntityRequest value)
        //{
        //    return 1;
        //}

        //// PUT api/controller/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/controller/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }

    public class CreateEntityRequest
    {
        public List<EntityDBO> Entities { get; set; }
        public int ScenarioId { get; set; }
    }

    public class EntityDBO
    {
        public int Id { get; set; }

        [JsonIgnore]
        public virtual List<ConnectionDBO> Inflows { get; set; } = null!; // Assigned by EF Core
        [JsonIgnore]
        public virtual List<ConnectionDBO> Outflows { get; set; } = null!; // Assigned by EF Core
    }

    public class ConnectionDBO
    {
        public ConnectionDBO() { }
        public int Id { get; set; }

        public int ScenarioId { get; set; }
        public int SourceId { get; set; }
        public int DestinationId { get; set; }
        
        [JsonIgnore]
        public virtual EntityDBO Source { get; set; } = null!; // Populated by EF Core
        
        [JsonIgnore]
        public virtual EntityDBO Destination { get; set; } = null!; // Populated by EF Core
    }


}
