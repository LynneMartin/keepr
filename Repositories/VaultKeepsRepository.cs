using System.Data;
using keepr.Models;
using Dapper;
using System.Collections.Generic;

namespace keepr.Repositories
{
  public class VaultKeepsRepository
  {
    private readonly IDbConnection _db;
    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }
//NOTE GET ALL VAULTKEEPS (BY ID, BECAUSE A USER HAS TO BE LOGGED IN)


  }

}