namespace ElsherbinyTask05.API.Contracts.Req;

public class UserSignupDtoReq
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
