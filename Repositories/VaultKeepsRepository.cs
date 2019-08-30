using System.Data;
using keepr.Models;
using Dapper;
using System.Collections.Generic;
using System;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
//NOTE SQL referenced from TABLES in db-setup
//NOTE GET ALL VAULTKEEPS (BY ID, BECAUSE A USER HAS TO BE LOGGED IN TO SEE VAULTS)
    public IEnumerable<VaultKeeps> GetVaultKeepsById(string userId)
    {
      return _db.Query<VaultKeeps>("SELECT * FROM vaultKeeps WHERE userId = @userId", new { userId });
    }

    //NOTE ADD KEEPS TO VAULTKEEP (with an ID)

    public VaultKeeps AddKeepToVault(VaultKeeps vaultKeeps)
    {
      int id = _db.ExecuteScalar<int>(@" 
      INSERT INTO vaultKeeps (vaultId, keepId, userId)
      VALUES (@VaultId, @KeepId, @UserId);
      SELECT LAST_INSERT_ID();", vaultKeeps);
      vaultKeeps.Id = id;
      return vaultKeeps;
    }

//NOTE REMOVE KEEPS FROM VAULTS
//NOTE referenced petshop for delete. needs review
    public void RemoveKeepsFromVaults(int id) //REVIEW VaultKeeps vaultKeeps?
    {
      var success = _db.Execute("DELETE FROM vaultKeeps WHERE vaultId = @vaultId AND keepId = @keepId AND userId = @userId", new {id}); // vaultKeeps?
      if (success != 1) //REVIEW == 0?
      {
        throw new Exception("Delete Request Failed.");
      };
    }
  }
}