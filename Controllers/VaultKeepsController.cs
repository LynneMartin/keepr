using System;
using keepr.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keepr.Controllers
{ 

[Authorize]
  [Route("api/[controller]")]
  [ApiController]

  public class ValutKeepsController : ControllerBase
  {
    private readonly ValutKeepsController _repository;
    public VaultKeepsController(VaultKeepsRepository repository)
    {
      _repository = repository;
    }

    //SECTION GET VAULTKEEPS BY ID
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
}

   




