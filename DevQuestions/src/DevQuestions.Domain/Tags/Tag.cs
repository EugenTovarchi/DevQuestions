namespace DevQuestions.Tags;

public  class Tag
{
    public Guid Id { get; set; }

    public string Descriptions { get; set; } = string.Empty;

    public required string Name { get; set; }

}
