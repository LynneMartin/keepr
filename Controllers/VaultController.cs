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
  public class VaultsController : ControllerBase
  {
    private readonly VaultsRepository _repository;
    public VaultsController(VaultsRepository repository)
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
    public ActionResult<Vault> GetVaultById(int id, string userId)
    {
      try
      {
        // string userId = HttpContext.User.FindFirstValue("Id"); //stacko //FIXME CANNOT READ PROPERTY 'ID' OF UNDEFINED
        return Ok(_repository.GetVaultById(id, userId));
      }
        catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    //SECTION GET VAULT BY *USER* ID
    [HttpGet]
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
        //FIXME ATTACH THE USER ID // DONE
        vault.UserId = HttpContext.User.FindFirstValue("Id");
        return Ok(_repository.CreateVault(vault));
      }
        catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    //SECTION DELETE VAULT (by Id)
    [HttpDelete("{id}")]
    public ActionResult<Vault> Delete(Vault vault, int id, string userId) //REVIEW is this correct?
    {
      try
      {
        //FIXME
        vault.UserId = HttpContext.User.FindFirstValue("Id"); 
        _repository.DeleteVault(id, userId);
        return Ok("Vault Delorted");
      }
        catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }

}