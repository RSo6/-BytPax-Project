namespace BytPax.Models;

public class User  {
    public int    Id { get; private set; }
    public bool   TypeOfUser { get; private set; }
    public string FullName { get; private set; }
    public string ImagePath { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
}