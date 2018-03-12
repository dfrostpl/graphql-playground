using System.Threading.Tasks;
using CMS.Base.GraphQL;
using CMS.Base.GraphQL.Schemas;
using CMS.Base.ProviderContracts;
using GraphQL;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly BaseSchema _schema;
        private readonly IRepository _repository;

        public HomeController(IDocumentExecuter documentExecuter, BaseSchema schema, IRepository repository)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLParameter query)
        {
            var executionOptions = new ExecutionOptions {Schema = _schema, Query = query.Query, UserContext = _repository };
            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
                return BadRequest(result.Errors);
            return Ok(result);
        }
    }
}