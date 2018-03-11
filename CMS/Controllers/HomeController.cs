using System.Diagnostics;
using System.Threading.Tasks;
using CMS.GraphQL;
using CMS.GraphQL.Queries;
using CMS.GraphQL.Schemas;
using CMS.Portal.Models;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Portal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly BaseSchema _schema;
        public HomeController(IDocumentExecuter documentExecuter, BaseSchema schema)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLParameter query)
        {
            var executionOptions = new ExecutionOptions { Schema = _schema, Query = query.Query };
            var result = await _documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result);
        }


    }
}
