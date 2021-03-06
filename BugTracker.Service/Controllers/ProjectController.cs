using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BugTracker.Service.HttpHandler;
using BugTracker.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// dotnet add package Newtonsoft.Json --version 12.0.3

namespace BugTracker.Service.Controllers
{
  [Route("api/[controller]")]
  // [Route("api/[controller]/[action]")]
  [ApiController]
  public class ProjectController : ControllerBase
  {
    private readonly ProjectHttpHandler httpHandler = new ProjectHttpHandler();

    [HttpGet]
    [ActionName("GetProjects")] // when you specify the action in the routing
    public async Task<ActionResult<IEnumerable<Project>>> Get()
    {
      // TODO add exception handling
      return await httpHandler.GetProjectsAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetById(int id)
    {
      return await httpHandler.GetProjectByIdAsync(id);
    }

    [HttpGet]
    [Route("[action]/{id}")]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjectsByUserId(int id)
    {
      System.Console.WriteLine("get by user Id");
      return await httpHandler.GetProjectsByUserId(id);
    }
    [HttpPut]
    public IActionResult PutProject(int id, Project project)
    {
      if (id != project.ID)
      {
        return BadRequest();
      }
      var success = httpHandler.PutProjectAsync(id, project);
      if (success.Result)
      {
        System.Console.WriteLine("Is Succesful - API");
        return NoContent();
      }
      else
      {
        System.Console.WriteLine("is not succesful - API");
        return NotFound();
      }
    }

    [HttpPost]
    public IActionResult PostAsync(Project project)
    {
      var success = httpHandler.PostProjectAsync(project);
      if (success.Result)
      {
        System.Console.WriteLine("Is Succesful - API");
        return CreatedAtAction(nameof(GetById), new { id = project.ID }, project);
      }
      else
      {
        System.Console.WriteLine("is not succesful - API");
        return NotFound(); // FIXME
      }
    }
    // DELETE: api/Project/5
    [HttpDelete("{id}")]
    public ActionResult DeleteProject(int id)
    {
      var success = httpHandler.DeleteProjectAsync(id);
      if (success.Result)
      {
        System.Console.WriteLine("Delete Succesful - API");
        return StatusCode(200);
      }
      else
      {
        System.Console.WriteLine("Delete not succesful - API");
        return NotFound(); // FIXME
      }
    }
  }
}