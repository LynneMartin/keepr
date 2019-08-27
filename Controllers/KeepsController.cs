using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
// using keepr.Repositories

namespace keepr.Controllers
{
  //used Petshop project for reference examples. Contractors project for [Authorize]
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class KeepsController : ControllerBase
  {
    private readonly KeepsRepository _repository;
    public KeepsController(KeepsRepository repository)
    {
      _repository = repository;
    }

    //used Petshop and BurgerShack for reference examples
    //NOTE GET ALL PUBLIC KEEPS
    [HttpGet]
    public ActionResult<IEnumerable<Keeps>> Get()
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
    //NOTE GET KEEPS BY *USER* ID
    //NOTE GetKeepsByUserId method created in KeepsRepo
    [HttpGet("{userId}")]
    public ActionResult<IEnumerable<Keeps>> GetKeepsByUserId(string userId) //REVIEW Adding IEnumerable to ActionResult removed "an explicit conversion exists" error
    {
        try
        {
        var UserId = HttpContext.User.FindFirstValue("Id");
        return Ok(_repository.GetKeepsByUserId(UserId));
        }
        catch (Exception e)
        {
        return BadRequest(e.Message);
        }
      }
  

      //NOTE GET A KEEP BY KEEP ID
      [HttpGet("{id}")] //keep id
      public ActionResult<Keeps> GetKeepsById(int id) //keep id
      {
        try
        {
        return Ok(_repository.GetKeepsById(id)); //keep id
        }
        catch (Exception e)
        {
        return BadRequest(e.Message);
        }
      }

    //NOTE CREATE KEEPS/PINS (POST)
    [HttpPost]
    public ActionResult<Keeps> CreateKeeps([FromBody] Keeps keeps)
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
    [HttpDelete("{id")] //keep id
    public ActionResult<string> DeleteKeeps(int id) //keep id
    {
      try
      {
        _repository.DeleteKeeps(id); //keep id
        return Ok("Keep Delorted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    }
}