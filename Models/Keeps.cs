namespace keepr.Models
{
  //NOTE Following testing tool data as a general guide
  public class Keeps
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Img { get; set; } 
    public string UserId { get; set; }
    public bool isPrivate { get; set; }

    //NOTE REQUIRED: "Anytime a keep is viewed or kept in a vault, the count should go up"
    // public int KeepsCount { get; set; } //REVIEW naming convention ok?
    //REVIEW Does there need to be a separate counter for # of keeps in a vault?
    //STUB Check to see if I need a UserId, Shares and Views


  }
}