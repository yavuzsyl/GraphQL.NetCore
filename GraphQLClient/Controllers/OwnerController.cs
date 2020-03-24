using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLClient.Consumer;
using GraphQLClient.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly OwnerConsumer _consumer;
        public OwnerController(OwnerConsumer consumer)
        {
            _consumer = consumer;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var owners = await _consumer.GetAllOwnersAsync();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var owner = await _consumer.GetOwnerAsync(id);
            return Ok(owner);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OwnerInput model)
        {
            var owner = await _consumer.CreateOwner(model);
            return Ok(owner);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] OwnerInput model, Guid id)
        {
            var owner = await _consumer.UpdateOwner(id, model);
            return Ok(owner);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _consumer.DeleteOwner(id);
            return Ok(result);
        }
    }
}