using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;
using System;

namespace keepr.Repositories

//NOTE references from PetShop
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }
    // //SECTION GET ALL VAULTS
    //     public IEnumerable<Vault> GetVaults() 
    //     {
    //       return _db.Query<Vault>("SELECT * FROM vault");
    //     }

    //SECTION GET VAULT BY ID
    public Vault GetVaultById(int id, string userId)
    {
      //FIXME ATTACH THE USERID
      return _db.QueryFirstOrDefault<Vault>("SELECT * FROM vaults WHERE id = @id", new { id, userId });
    }
    //SECTION GET VAULT BY USER ID
    public IEnumerable<Vault> GetVaultByUserId(string userId)
    {
      return _db.Query<Vault>("SELECT * FROM vaults WHERE userId = @userId", new { userId });
    }
    //SECTION CREATE VAULT (POST)
    public Vault CreateVault(Vault vault)
    {
      var id = _db.ExecuteScalar<int>(@" 
      INSERT INTO vaults (name, description)
      VALUES (@Name, @Description);
      SELECT LAST_INSERT_ID();
      ", vault);
      vault.Id = id;
      return vault;
    }
    //SECTION DELETE VAULT
    //NOTE Referenced lego and petshop projects
    public void DeleteVault(int id, string userId) //STUB internal void or public?
    {
      var success = _db.Execute("DELETE FROM vaults WHERE id = @id AND userId = @userId", new { id, userId });
      if (success != 1)
      {
        throw new Exception("Unable to delete.");
      }
    }
  }
}