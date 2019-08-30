using System.Collections.Generic;
using keepr.Models;
using keepr.Repositories;
using Microsoft.AspNetCore.Mvc;
using System; //yay! removed Exception squigglies
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace keepr.Controllers
//NOTE Used KeepsController for reference
{
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class VaultController : ControllerBase
  {
    private readonly VaultRepository _repository;
    public VaultController(VaultRepository repository)
    {
      _repository = repository;
    }

    //used Petshop and BurgerShack for reference examples
    //STUB would GET ALL VAULTS even be needed? Return to this.
    // [HttpGet]
    // public ActionResult<IEnumerable<Vault>> Get()
    // {
    //   try
    //   {
    //     return Ok(_repository.GetVault());
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }

    // }
//SECTION GET VAULT BY ID
    [HttpGet("{id}")]
    public ActionResult<string> GetVaultById(int id)
    {
      try
      {
        return Ok(_repository.GetVaultById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

//SECTION GET VAULT BY *USER* ID
    public ActionResult<IEnumerable<Vault>> GetVaultByUserId()
    {
      try
      {
        string userId = HttpContext.User.FindFirstValue("Id"); //stackoverflow
        return Ok(_repository.GetVaultByUserId(userId));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

//SECTION CREATE VAULT (POST)
    [HttpPost]
    public ActionResult<Vault> Post([FromBody] Vault vault)
    {
      try
      {
        return Ok(_repository.CreateVault(vault));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

//SECTION DELETE VAULT (by Id)
    [HttpDelete("{id}")]
    public ActionResult<Vault> Delete(int id)
    {
      try
      {
        _repository.DeleteVault(id);
        return Ok("Vault Delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }

}