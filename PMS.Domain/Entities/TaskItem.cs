namespace PMS.Domain.Entities;

public class TaskItem : BaseEntity
{
    public string Title { get; private set; }
    public string Description { get; private set; }

    // This jus means that this task belongs to a project.
    // We store the ProjectId for database purposes, which is a foreign key to the Projects table.
    // We also have a Project property that lets us access the full Project object when we need it.
    public Guid ProjectId { get; private set; } // Stores the ID
    public Project Project { get; private set; } // Lets us access the full Project object

    // This might be empty. Task may or may not have a user assigned to it.
    public Guid? AssignedUserId { get; private set; } // Stores the ID of the assigned user
    public User AssignedUser { get; private set; } // Lets us access the full User object

    public DateTime DueDate { get; private set; }

    // A task can have multiple comments, so we use a collection to represent that relationship.
    // public ICollection<Comment> Comments { get; set; }

    private readonly List<Comment> _comments = new();
    public IReadOnlyCollection<Comment> Comments => _comments;

    public TaskItem(string title, string description, Guid projectId, DateTime dueDate)
    {
        SetTitle(title);
        SetDescription(description);

        if (projectId == Guid.Empty)
            throw new ArgumentException("ProjectId is required");

        if (dueDate < DateTime.UtcNow)
            throw new ArgumentException("Due date cannot be in the past");

        ProjectId = projectId;
        DueDate = dueDate;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");

        Title = title;
    }

    public void SetDescription(string description)
    {
        Description = description ?? string.Empty;
    }

    public void AssignUser(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        AssignedUser = user;
        AssignedUserId = user.Id;
    }

    public void AddComment(Comment comment)
    {
        if (comment == null)
            throw new ArgumentNullException(nameof(comment));

        _comments.Add(comment);
    }

}