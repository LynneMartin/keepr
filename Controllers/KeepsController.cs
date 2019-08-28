using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using keepr.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
// using keepr.Repositories

namespace keepr.Controllers
{
//NOTE used Petshop project for reference examples. Contractors project for [Authorize]
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

//NOTE used Petshop and BurgerShack for reference examples
//SECTION GET ALL PUBLIC KEEPS
    [HttpGet]
    public ActionResult<IEnumerable<Keep>> Get() //key is from the Model
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
//SECTION GET KEEPS BY *USER* ID
//NOTE GetKeepsByUserId method created in KeepsRepo
    [HttpGet("user")]
    public ActionResult<IEnumerable<Keep>> GetKeepsByUserId() //REVIEW Adding IEnumerable to ActionResult removed "an explicit conversion exists" error
    {
        try
        {
        string userId = HttpContext.User.FindFirstValue("Id"); //stackoverflow
        return Ok(_repository.GetKeepsByUserId(userId));
        }
        catch (Exception e)
        {
        return BadRequest(e.Message);
        }
      }
  

//SECTION GET A KEEP BY KEEP ID
      [HttpGet("{id}")] //keep id
      public ActionResult<Keep> GetKeepsById(int id) //keep id
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

//SECTION CREATE KEEPS/PINS (POST)
// POST api/values
//NOTE referenced popsicle burger shack project & checking out backapack video
    [HttpPost]
    public ActionResult<Keep> CreateKeep([FromBody] Keep keep)
    {
        try
        {
        keep.UserId = HttpContext.User.FindFirstValue("Id");
        return Ok(_repository.CreateKeep(keep));
        }
        catch (Exception e)
        {
        return BadRequest(e.Message);
        }
    }

//SECTION DELETE KEEPS/PINS
    [HttpDelete("{id")] //keep id
    public ActionResult<string> DeleteKeeps(int id) //keep id
    {
        try
        {
        _repository.DeleteKeeps(id); //keep id
        return Ok("Keep Was Delorted");
        }
        catch (Exception e)
        {
        return BadRequest(e.Message);
        }
    }
  }
}