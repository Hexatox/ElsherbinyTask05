namespace ElsherbinyTask05.API.Contracts.Req;
public class ChangeUserRoleDto
    {
        public string Username { get; set; } = string.Empty;
        public string NewRole { get; set; } = string.Empty; 
    }