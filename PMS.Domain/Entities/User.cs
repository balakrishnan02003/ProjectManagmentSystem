namespace PMS.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    // public ICollection<ProjectMember> ProjectMembers { get; set; }
    // public ICollection<TaskItem> AssignedTasks { get; set; }

    // Store tasks privately, but let others only read them - not to modify them.
    private readonly List<ProjectMember> _projectMembers = new(); // This is the private list that we can modify within the User class.
    public IReadOnlyCollection<ProjectMember> ProjectMembers => _projectMembers; //This exposes the list to outside code, but as read-only.

    private readonly List<TaskItem> _assignedTasks = new();
    public IReadOnlyCollection<TaskItem> AssignedTasks => _assignedTasks;

    public User(string name, string email)
    {
        SetName(name);
        SetEmail(email);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        Name = name;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");

        Email = email;
    }

}