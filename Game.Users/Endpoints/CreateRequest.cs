namespace Game.Users.Endpoints;

public record CreateRequest(string Email,string Password,string ConfirmedPassword);