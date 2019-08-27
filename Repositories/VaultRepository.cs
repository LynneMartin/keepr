using System.Collections.Generic;
using System.Data;
using Dapper;
using keepr.Models;
using System;

namespace keepr.Repositories

//NOTE references from PetShop
{
  public class VaultRepository
{
    private readonly IDbConnection _db;

    public VaultRepository(IDbConnection db)
    {
      _db = db;
    }
//SECTION GET ALL VAULTS
    public IEnumerable<Vault> GetVault() //STUB vault or vaults?
    {
      return _db.Query<Vault>("SELECT * FROM vault");
    }
//SECTION GET VAULT BY ID
    public Vault GetVaultById(int id)
    {
      return _db.QueryFirstOrDefault<Vault>("SELECT * FROM vault WHERE id = @id, new {id}");
    }
//SECTION CREATE VAULT (POST)
    public Vault CreateVault(Vault vault)
    {
      var id = _db.ExecuteScalar<int>(@" 
      INSERT INTO vault (name, description)
      VALUES (@Name, @Description);
      SELECT LAST_INSERT_ID();
      ", vault);
      vault.Id = id;
      return vault;
    }
//SECTION DELETE VAULT
//NOTE Referenced lego and petshop projects
    public void DeleteVault(int id) //STUB internal void or public?
    {
      var success = _db.Execute("DELETE FROM vaults WHERE id = @id", new { id });
      if (success != 1)
      {
        throw new Exception("Unable to delete.");
      }
    }
  }
}