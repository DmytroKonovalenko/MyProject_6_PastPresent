using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum LevelStatus
{
    Locked,
    Available,
    Completed
}

public class LevelButton : MonoBehaviour
{
    public int levelIndex;
    public Button button;
    public Image buttonImage;
    public Sprite lockedSprite;
    public Sprite availableSprite;
    public Sprite completedSprite;
    public GameObject lockImage;

    public Image lineImage;
    public Color lockedLineColor = Color.white;
    public Color availableLineColor = Color.red;
    public Color completedLineColor = Color.green;

    
    public Sprite pilotImage;  
    public Sprite rankImage;   
    public string pilotName;   



    private LevelStatus currentStatus;

    [Header("Popup")]
    [SerializeField] private TextMeshProUGUI textONPopup;
    [SerializeField] private Image imageOnPopupStart;
    [SerializeField] private Image imageOnPopupEnd;

    private void Start()
    {
        UpdateButtonState();
    }

    public void SetLevelStatus(LevelStatus status)
    {
        currentStatus = status;
        UpdateButtonState();
    }

    private void UpdateButtonState()
    {
        switch (currentStatus)
        {
            case LevelStatus.Locked:
                buttonImage.sprite = lockedSprite;
                lockImage.SetActive(true);
               // button.interactable = false;
                lineImage.color = lockedLineColor;  
                break;
            case LevelStatus.Available:
                lockImage.SetActive(false);
                buttonImage.sprite = availableSprite;
                button.interactable = true;
                lineImage.color = availableLineColor;  
                break;
            case LevelStatus.Completed:
                lockImage.SetActive(false);
                buttonImage.sprite = completedSprite;
                button.interactable = true;
                lineImage.color = completedLineColor;  
                break;
        }
    }

    public void ChangeInfoOnPopups()
    {
        textONPopup.text = pilotName.ToString();
        imageOnPopupStart.sprite = rankImage;
        imageOnPopupEnd.sprite = rankImage;
    }
}
