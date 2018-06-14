using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Base.Abstractions;
using CMS.Base.GraphQL;
using CMS.Base.GraphQL.Schemas;
using GraphQL;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Portal.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    public class BaseGraphController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly BaseSchema _schema;
        private readonly IRepository _repository;

        public BaseGraphController(IDocumentExecuter documentExecuter, BaseSchema schema, IRepository repository)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _repository = repository;
        }

        [HttpPost]
        [Route("/graph/base")]
        public async Task<IActionResult> Post([FromBody] GraphQLParameter query)
        {
            var executionOptions = new ExecutionOptions { Schema = _schema, Query = query.Query, UserContext = _repository };
            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);
            if (result.Errors?.Count > 0)
                return BadRequest(result.Errors);
            return Ok(result);
        }
    }
}
