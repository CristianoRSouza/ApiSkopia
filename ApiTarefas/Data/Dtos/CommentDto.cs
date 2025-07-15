public class CommentDto
{
    public int Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
