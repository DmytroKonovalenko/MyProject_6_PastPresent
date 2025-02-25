using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private PanelUserName panelUserName;
    [SerializeField] private SettingPanel settingPanel;


    [SerializeField] private Image pilotImage;
    [SerializeField] private Image avatarSetting;
    [SerializeField] private Image avatarStart;
    [SerializeField] private Image rankImage;   
    [SerializeField] private TextMeshProUGUI pilotNameText;  
    
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            string savedName = PlayerPrefs.GetString("PlayerName");
            panelUserName.UpdateWelcomeText(savedName);
        }
        settingPanel.LoadAvatar();
        DisplayLastCompletedLevelData();
    }

    
    void Update()
    {
        
    }
    public void DisplayLastCompletedLevelData()
    {
        int lastCompletedLevel = LevelManager.Instance.GetLastCompletedLevel();

        if (lastCompletedLevel >= 0)
        {
            
            LevelButton levelButton = LevelManager.Instance.levelButtons[lastCompletedLevel];

            avatarSetting.sprite = levelButton.pilotImage;
            avatarStart.sprite = levelButton.pilotImage;
            pilotImage.sprite = levelButton.pilotImage;  
            rankImage.sprite = levelButton.rankImage;    
            pilotNameText.text = levelButton.pilotName;  
        }
    }
}
