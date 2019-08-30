using System;
using System.Security.Claims;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]

  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsController _repository;
    public VaultKeepsController(VaultKeepsRepository repository)
    {
      _repository = repository; //FIXME whyyyyy???
    }

//SECTION GET VAULTKEEPS BY ID
//NOTE GET api/vaultkeeps/5
    [HttpGet("{id}")]
    public ActionResult<string> GetVaultKeepsById(int id)
    {
      try
      {
        return Ok(_repository.GetVaultKeepsById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

//SECTION ADD KEEP TO VAULT
//NOTE POST api/vaultkeeps
      [HttpPost]
      public ActionResult<VaultKeeps> AddKeepToVault([FromBody] VaultKeeps vaultKeeps)
      {
      vaultKeeps.UserId = HttpContext.User.FindFirstValue("Id"); //FIXME ???
      try 
      {
        return Ok(_repository.AddKeepToVault(vaultKeeps));
      }
        catch (Exception e)
        {
        return BadRequest("Unable to add this Keep to this Vault.");
      }
    }

//SECTION REMOVE KEEP FROM VAULT
//NOTE PUT api/vaultkeeps/5
      [HttpPut]
      public ActionResult<VaultKeeps> RemoveKeepFromVault(int id)
      {
      try
      {
        _repository.RemoveKeepFromVault(id);
        return Ok("Keep successfully removed from Vault.");
      }
        catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}






