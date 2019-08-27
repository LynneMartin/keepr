//STUB Do I need a GET KEEPS BY USER ID if I already have a GET KEEPS BY ID?

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;
// using keepr.Controllers

namespace keepr.Controllers
//NOTE Referenced lego and petshop projects
{
    public class KeepsRepository
    {
      private readonly IDbConnection _db;
      public KeepsRepository(IDbConnection db)
      {
        _db = db;
      }
  //SECTION GET ALL KEEPS (No login necessary)
      public IEnumerable<Keeps> GetKeeps()
      {
        return _db.Query<Keeps>("SELECT * FROM keeps");
      }

  //SECTION GET KEEPS BY ID
      public Keeps GetKeepsById(int id) //REVIEW Get All Public Keeps By Id?
      {
        return _db.QueryFirstOrDefault<Keeps>("SELECT * FROM keeps WHERE id = @id, new {id}");
      }
  //SECTION CREATE KEEPS (POST)
      public Keeps CreateKeeps(Keeps keeps)
      {
        var id = _db.ExecuteScalar<int>(@" 
      INSERT INTO keeps (name, img, description)
      VALUES (@Name, @Img, @Description);
      SELECT LAST_INSERT_ID();
      ", keeps);
        keeps.Id = id;
        return keeps;
      }
    //SECTION GET KEEPS BY *USER* ID
    public IEnumerable GetKeepsByUserId(string UserId) //string, matching Keeps.cs
    {
      return _db.Query<Keeps>("SELECT * FROM keeps WHERE UserId = @UserId, new {UserId}");
    }

    //SECTION DELETE KEEPS/PINS BY THE ID
    //NOTE Referenced lego and petshop projects
    public void DeleteKeeps(int id) //REVIEW internal void or public or public bool?
      {
        var success = _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
        if (success != 1)
        {
          throw new Exception("Unable to delete.");
        }
      }
    }
  }
