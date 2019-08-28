namespace keepr.Models
{
  //NOTE Following testing tool data as a general guide, and db-setup tables
  public class Keep
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UserId { get; set; }
    public string Img { get; set; } 
    public bool isPrivate { get; set; }
    public int Views { get; set; }
    public int Shares { get; set; }

    //NOTE REQUIRED: "Anytime a keep is viewed or kept in a vault, the count should go up"
    // public int KeepsCount { get; set; } //REVIEW naming convention ok?
    //REVIEW Does there need to be a separate counter for # of keeps in a vault?
    //NOTE added views and shares because it's in the db-setup tables for keeps


  }
}