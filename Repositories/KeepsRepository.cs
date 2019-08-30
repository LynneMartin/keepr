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
    //SECTION GET ALL PUBLIC KEEPS (No login necessary)
    public IEnumerable<Keep> GetPublicKeeps()
    {
      return _db.Query<Keep>("SELECT * FROM keeps");
    }

    //SECTION GET KEEPS BY ID
    public Keep GetKeepsById(int id) //REVIEW Get All Public Keeps By Id?
    {
      return _db.QueryFirstOrDefault<Keep>("SELECT * FROM keeps WHERE id = @id", new { id });
    }
    //SECTION GET KEEPS BY *USER* ID
    public IEnumerable GetKeepsByUserId(string UserId) //string, matching Keeps.cs
    {
      return _db.Query<Keep>("SELECT * FROM keeps WHERE UserId = @UserId", new { UserId });
    }
    //SECTION CREATE KEEPS (POST)
    //NOTE Referenced burger shack
    public Keep CreateKeep(Keep keep)
    {
      var id = _db.ExecuteScalar<int>(@" 
      INSERT INTO keeps (id, name, description, userId, img, isPrivate)
      VALUES (@Id, @Name, @Description, @userId, @Img, @isPrivate);
      SELECT LAST_INSERT_ID();
      ", keep);
      keep.Id = id;
      return keep;
    }

    // //SECTION EDIT KEEPS (PUT)

    // public void EditKeep(int id)
    // {
    //   var sucess = _db.Execute("UPDATE keeps ")
    // }

    //SECTION DELETE KEEPS/PINS BY THE ID
    //NOTE Referenced lego and petshop projects
    public void DeleteKeeps(int id) //REVIEW internal void or public or public bool?
    {
      var success = _db.Execute("DELETE FROM keeps WHERE id = @id AND userId = @userId", new { id });
      if (success != 1)
      {
        throw new Exception("Unable to delete.");
      }
    }
  }
}
