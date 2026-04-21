public class Project : BaseEntity
{
    public string Name { get; private set; }

    //public ICollection<ProjectMember> Members { get; set; }
    //public ICollection<TaskItem> Tasks { get; set; }

    private readonly List<ProjectMember> _members = new();
    public IReadOnlyCollection<ProjectMember> Members => _members;

    private readonly List<TaskItem> _tasks = new();
    public IReadOnlyCollection<TaskItem> Tasks => _tasks;

    public Project(string name)
    {
        SetName(name);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Project name cannot be empty");

        Name = name;
    }

    public void AddMember(ProjectMember member)
    {
        if (member == null)
            throw new ArgumentNullException(nameof(member));

        if (_members.Any(m => m.UserId == member.UserId))
            throw new Exception("User is already a member of this project");

        _members.Add(member);
    }

    public void AddTask(TaskItem task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        _tasks.Add(task);
    }
}