using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;

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
  //SECTION GET ALL KEEPS
      public IEnumerable<Keeps> GetKeeps()
      {
        return _db.Query<Keeps>("SELECT * FROM keeps");
      }
  //SECTION GET KEEPS BY ID
      public Vault GetKeepsById(int id)
      {
        return _db.QueryFirstOrDefault<Keeps>("SELECT * FROM keeps WHERE id = @id, new {id}"); //FIXME
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
  //SECTION DELETE KEEPS/PINS
  //NOTE Referenced lego and petshop projects
      public void DeleteKeeps(int id) //STUB internal void or public?
      {
        var complete = _db.Execute("DELETE FROM keeps WHERE id = @id", new { id });
        if (success != 1) //FIXME
        {
          throw new Exception("Unable to delete.");
        }
      }
    }
  }
