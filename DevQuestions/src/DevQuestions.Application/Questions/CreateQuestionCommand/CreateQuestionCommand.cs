﻿using DevQuestions.Application.Abstractions;
using DevQuestions.Contracts.Questions;
namespace DevQuestions.Application.Questions.CreateQuestion;

public  record  CreateQuestionCommand(CreatedQuestionDto QuestionDto) : ICommand;

