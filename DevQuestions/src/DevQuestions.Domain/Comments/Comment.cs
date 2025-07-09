namespace DevQuestions.Comments;

public  class Comment
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public  Comment? Parent { get; set; } //древовидная структура ответов на разные 

    public List<Comment> Children { get; set; } = []; // 

    public required Guid EntityId { get; set; } // для хранения комментариев более не нагружая Questions сущность списком 
}
