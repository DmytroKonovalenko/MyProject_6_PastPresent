using System;

[Serializable]
public class QuizLevel
{
    public string levelName; 
    public QuizQuestion[] questions = new QuizQuestion[10];
}