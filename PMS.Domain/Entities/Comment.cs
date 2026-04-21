public class Comment : BaseEntity
{
    public string Content { get; private set; }

    public Guid TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; }

    public Comment(string content, Guid taskItemId)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Comment cannot be empty");

        if (taskItemId == Guid.Empty)
            throw new ArgumentException("TaskItemId is required");

        Content = content;
        TaskItemId = taskItemId;
    }
}