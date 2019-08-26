using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;

namespace keepr.Controllers
{
  //used Petshop project for reference examples
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsRepository _repository;
    public KeepsController(KeepsRepository _repository)
    {
      _repository = repository;
    }

    //used Petshop and BurgerShack for reference examples
    //STUB would GET ALL KEEPS even be needed? Return to this.
    [HttpGet]
    public ActionResult<IEnumerable<KeepsController>> Get()
    {
      try
      {
        return Ok(_repository.GetKeeps());
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }

    }
      //NOTE GET KEEPS BY ID
      [HttpGet("{id}")]
      public ActionResult<string> Get(int id)
      {
        try
        {
        return Ok(_repository.GetKeepsById(id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
      }

    //NOTE CREATE KEEPS/PINS
    [HttpPost]
    public ActionResult<Keeps> Post([FromBody] Keeps keeps)
    {
      try
      {
        return Ok(_repository.CreateKeeps(keeps));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    //NOTE DELETE KEEPS/PINS
    [HttpDelete("{id")]
    public ActionResult<string> Delete(int id)
    {
      try
      {
        _repository.DeleteKeeps(id);
        return Ok("Keep Delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    }
}