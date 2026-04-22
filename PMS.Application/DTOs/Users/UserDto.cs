namespace PMS.Application.DTOs.Users;

public class UserDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public int ProjectCount { get; set; }
    public int AssignedTaskCount { get; set; }
}