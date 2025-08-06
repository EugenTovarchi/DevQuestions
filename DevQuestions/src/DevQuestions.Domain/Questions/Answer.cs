﻿namespace DevQuestions.Domain.Questions;

public class Answer
{
    public Guid Id { get; set; }

    public  Guid UserId { get; set; }

    public  string Text  { get; set; }

    public  Question? Question { get; set; }

    public Guid QuestionId { get; set; }

    public List<Guid> Comments { get; set; } = [];

    public long Rating { get; set; }

    public Answer(
        Guid id, Guid userId, string text, Guid questionId)
    {
        Id = id;
        UserId = userId;
        Text = text;
        QuestionId = questionId;
        Rating = 0;
    }

}

