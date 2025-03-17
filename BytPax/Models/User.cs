namespace BytPax.Models;

public class User  {
    public int Id { get; private set; }
    public string Full_name { get; private set; }
    public string Image_path { get; private set; }
    public string Email { get; private set; }
    public bool Type_of_user { get; private set; }
    public string Password { get; private set; }
}