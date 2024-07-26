namespace Game.Users.Endpoints;

public record CreateRequest(string UserName,string Email,string Password,string ConfirmedPassword);