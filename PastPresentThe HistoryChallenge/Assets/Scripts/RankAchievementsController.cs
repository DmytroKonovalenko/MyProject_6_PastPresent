using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleAchivment  : MonoBehaviour
{
    [SerializeField] private List<Image> achievementImages;  
    [SerializeField] private List<Sprite> closedAchievementSprites;  
    [SerializeField] private List<Sprite> openedAchievementSprites;  

    private const string AchievementStatusKey = "AchievementStatus_";

    void Start()
    {
        
        LoadAchievements();
    }

    public void UpdateAchievement(int levelIndex, bool isCompleted)
    {
        if (levelIndex < 0 || levelIndex >= achievementImages.Count) return;

        
        PlayerPrefs.SetInt(AchievementStatusKey + levelIndex, isCompleted ? 1 : 0);
        PlayerPrefs.Save();

        
        if (isCompleted)
        {
            achievementImages[levelIndex].sprite = openedAchievementSprites[levelIndex];
        }
        else
        {
            achievementImages[levelIndex].sprite = closedAchievementSprites[levelIndex];
        }
    }

    
    private void LoadAchievements()
    {
        for (int i = 0; i < achievementImages.Count; i++)
        {
            int status = PlayerPrefs.GetInt(AchievementStatusKey + i, 0);
            if (status == 1)
            {
                achievementImages[i].sprite = openedAchievementSprites[i];
            }
            else
            {
                achievementImages[i].sprite = closedAchievementSprites[i];
            }
        }
    }
}
