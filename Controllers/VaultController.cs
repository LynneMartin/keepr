using keepr.Models;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
//NOTE Used KeepsController for reference
{
[Route("api/[controller]")]
  [ApiController]
  public class VaultController : ControllerBase
  {
    private readonly VaultRepository _repository;
    public VaultController(VaultRepository _repository)
    {
      _repository = repository;
    }

    //used Petshop and BurgerShack for reference examples
    //STUB would GET ALL VAULTS even be needed? Return to this.
    [HttpGet]
    public ActionResult<IEnumerable<Vault>> Get()
    {
      try
      {
        return Ok(_repository.GetVault());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }
    //NOTE GET VAULT BY ID
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
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

    //NOTE CREATE VAULT (POST)
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

    //NOTE DELETE VAULT
    [HttpDelete("{id")]
    public ActionResult<string> Delete(int id)
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