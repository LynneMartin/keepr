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
    //SECTION GET ALL VAULTKEEPS (BY ID, BECAUSE A USER HAS TO BE LOGGED IN TO SEE VAULTS)
    public IEnumerable<Keep> GetKeepsByVaultId(int vaultId, string userId)
    {
      return _db.Query<Keep>(@"
      SELECT * FROM vaultkeeps vk
      INNER JOIN keeps k ON k.id = vk.keepId
      WHERE(vaultId = @vaultId AND vk.userId = @userId)
      ", new { vaultId, userId });
    }

    //SECTION ADD KEEPS TO VAULTKEEP (with an ID)

    public VaultKeeps AddKeepToVault(VaultKeeps vaultKeeps)
    {
      int id = _db.ExecuteScalar<int>(@" 
      INSERT INTO vaultKeeps (vaultId, keepId, userId)
      VALUES (@VaultId, @KeepId, @UserId);
      SELECT LAST_INSERT_ID();", vaultKeeps);
      vaultKeeps.Id = id;
      return vaultKeeps;
    }

    //SECTION REMOVE KEEPS FROM VAULTS
    //NOTE referenced petshop for delete. needs review
    public void RemoveKeepFromVault(VaultKeeps vk)
    {
      var success = _db.Execute("DELETE FROM vaultKeeps WHERE vaultId = @VaultId AND keepId = @KeepId AND userId = @UserId", vk); 
      if (success != 1)
      {
        throw new Exception("Delete Request Failed.");
      };
    }
  }
}