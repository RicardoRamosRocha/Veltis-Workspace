using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Veltis.Workspace.Application.Agents.Execution;

namespace Veltis.Workspace.Web.Controllers;

[Authorize(Roles = "Administrator")]
public class AgentTestController : Controller
{
    private readonly IAgentExecutionPipeline _pipeline;

    public AgentTestController(IAgentExecutionPipeline pipeline)
    {
        _pipeline = pipeline;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(string title, string description)
    {
        var request = new AgentExecutionRequest
        {
            UserId = Guid.NewGuid(),
            WorkspaceId = Guid.NewGuid(),
            ProfessionId = Guid.NewGuid(),
            AgentId = Guid.NewGuid(),
            FormValues = new Dictionary<string, string>
            {
                { "Título", title },
                { "Descrição", description }
            }
        };

        var result = await _pipeline.ExecuteAsync(request);

        ViewBag.Prompt = result.Prompt;
        ViewBag.Response = result.Response;

        return View();
    }
}