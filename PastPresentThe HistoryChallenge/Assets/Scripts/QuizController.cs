using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuizController : MonoBehaviour
{
    [SerializeField] private QuizLevel[] levels;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI questionCounterText;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TextMeshProUGUI[] answerTexts;
    [SerializeField] private Sprite correctAnswerSprite;
    [SerializeField] private Sprite wrongAnswerSprite;
    [SerializeField] private Sprite neutralAnswerSprite;
    [SerializeField] private Sprite defaultButtonSprite;
    [SerializeField] private Color wrongAnswerTextColor = new Color(0xBA / 255f, 0x38 / 255f, 0x3A / 255f);
    [SerializeField] private Color defaultTextColor = Color.white;

    [SerializeField] private GameObject winPopup;
    [SerializeField] private GameObject losePopup;

    [SerializeField] private StartPanel startPanel;
    [SerializeField] private TitleAchivment rankAchievementsController;

    private int currentLevelIndex = 0;
    private int currentQuestionIndex = 0;
    private int incorrectAnswersCount = 0;

    private QuizQuestion currentQuestion;
    private bool isAnswering;

    private void Start()
    {
        winPopup.SetActive(false);
        losePopup.SetActive(false);
        LoadLevel(currentLevelIndex);
    }

    public void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        currentQuestionIndex = 0;
        incorrectAnswersCount = 0;
        LoadQuestion(currentQuestionIndex);
    }

    public void LoadQuestion(int questionIndex)
    {
        currentQuestion = levels[currentLevelIndex].questions[questionIndex];
        questionText.text = currentQuestion.questionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerTexts[i].text = currentQuestion.answers[i];
            answerButtons[i].image.sprite = defaultButtonSprite;
            answerTexts[i].color = defaultTextColor;
            answerButtons[i].interactable = true;
            int index = i;
            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }

        UpdateQuestionCounter();
        isAnswering = false;
    }

    private void UpdateQuestionCounter()
    {
        questionCounterText.text = (currentQuestionIndex + 1) + "/" + levels[currentLevelIndex].questions.Length;
    }

    public void CheckAnswer(int selectedAnswerIndex)
    {
        if (isAnswering) return;

        isAnswering = true;

        if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            answerButtons[selectedAnswerIndex].image.sprite = correctAnswerSprite;
        }
        else
        {
            answerButtons[selectedAnswerIndex].image.sprite = wrongAnswerSprite;
            answerTexts[selectedAnswerIndex].color = wrongAnswerTextColor;
            answerButtons[currentQuestion.correctAnswerIndex].image.sprite = correctAnswerSprite;
            incorrectAnswersCount++;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i != selectedAnswerIndex && i != currentQuestion.correctAnswerIndex)
            {
                answerButtons[i].image.sprite = neutralAnswerSprite;
            }
            answerButtons[i].interactable = false;
        }

        StartCoroutine(WaitAndLoadNextQuestion());
    }

    private IEnumerator WaitAndLoadNextQuestion()
    {
        yield return new WaitForSeconds(2);
        currentQuestionIndex++;

        if (currentQuestionIndex < levels[currentLevelIndex].questions.Length)
        {
            LoadQuestion(currentQuestionIndex);
        }
        else
        {
            EndQuiz();
        }
    }

    private void EndQuiz()
    {
        if (incorrectAnswersCount == 0)
        {
            winPopup.SetActive(true);
            LevelManager.Instance.CompleteLevel(currentLevelIndex);
            LoadNextLevel();
            startPanel.DisplayLastCompletedLevelData();
            rankAchievementsController.UpdateAchievement(currentLevelIndex, true);
        }
        else
        {
            losePopup.SetActive(true);
        }
    }

    private void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;
        if (nextLevelIndex < LevelManager.Instance.levelButtons.Count)
        {
            LevelManager.Instance.UnlockLevel(nextLevelIndex);
            LevelManager.Instance.LoadLevel(nextLevelIndex);
        }
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }
}
