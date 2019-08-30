//NOTE RELATIONSHIP ONLY

using System;
using System.Collections.Generic;
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
    private readonly VaultKeepsRepository _repository;
    public VaultKeepsController(VaultKeepsRepository repository)
    {
      _repository = repository;
    }

    //SECTION GET KEEPS BY VAULT ID
    [HttpGet("{id}")]
    public ActionResult<IEnumerable<Keep>> GetKeepsByVaultId(int vaultId) //added by Jake
    {
      try
      {
        string userId = HttpContext.User.FindFirstValue("Id");
        return Ok(_repository.GetKeepsByVaultId(vaultId, userId)); //vaultkeep id
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
      try
      {
        vaultKeeps.UserId = HttpContext.User.FindFirstValue("Id"); //fixed in model
        return Ok(_repository.AddKeepToVault(vaultKeeps));
      }
        catch (Exception e)
      {
        return BadRequest(e.Message);
        // return BadRequest("Unable to add this Keep to this Vault."); //<---brooklyn change
      }
    }

    //SECTION REMOVE KEEP FROM VAULT
    //NOTE PUT api/vaultkeeps/5
    [HttpPut]
    public ActionResult<VaultKeeps> RemoveKeepFromVault([FromBody] VaultKeeps vk) //jake change to vk
    {
      try
      {
        // attach the your userId to vk here
        vk.UserId = HttpContext.User.FindFirstValue("Id");
        _repository.RemoveKeepFromVault(vk);
        return Ok("Keep successfully removed from Vault.");
      }
        catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}






