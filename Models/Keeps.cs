namespace keepr.Models
{
  public class Keeps
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Img { get; set; } //REVIEW NOT REQUIRED? Adding anyway
    public string Description { get; set; }

    //NOTE REQUIRED: "Anytime a keep is viewed or kept in a vault, the count should go up"
    public int KeepsCount { get; set; } //REVIEW naming convention ok?
    //REVIEW Does there need to be a separate counter for # of keeps in a vault?


  }
}