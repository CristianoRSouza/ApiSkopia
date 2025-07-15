public class HistoryDto
{
    public int Id { get; set; }
    public string FieldModified { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public DateTime ModifiedAt { get; set; }
}
