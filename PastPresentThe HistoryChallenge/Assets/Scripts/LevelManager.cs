using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;  
    public List<LevelButton> levelButtons;  
    private const string LevelStatusKey = "LevelStatus_";  
    private const string LastCompletedLevelKey = "LastCompletedLevel";  

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadLevelStatuses();
    }

    private void LoadLevelStatuses()
    {
        int lastCompletedLevel = PlayerPrefs.GetInt(LastCompletedLevelKey, -1);  

        for (int i = 0; i < levelButtons.Count; i++)
        {
           
            int status = PlayerPrefs.GetInt(LevelStatusKey + i, (int)LevelStatus.Locked);

            
            if (i == 0)
            {
                
                if (status == (int)LevelStatus.Completed)
                {
                    levelButtons[i].SetLevelStatus(LevelStatus.Completed);
                }
                else
                {
                    levelButtons[i].SetLevelStatus(LevelStatus.Available);
                }
            }
            else
            {
                
                levelButtons[i].SetLevelStatus((LevelStatus)status);
            }
        }

        
        if (lastCompletedLevel >= 0 && lastCompletedLevel < levelButtons.Count)
        {
            levelButtons[lastCompletedLevel].SetLevelStatus(LevelStatus.Completed);
        }
    }

    public void CompleteLevel(int levelIndex)
    {
       
        levelButtons[levelIndex].SetLevelStatus(LevelStatus.Completed);
        PlayerPrefs.SetInt(LevelStatusKey + levelIndex, (int)LevelStatus.Completed);

        
        if (levelIndex + 1 < levelButtons.Count)
        {
            levelButtons[levelIndex + 1].SetLevelStatus(LevelStatus.Available);
            PlayerPrefs.SetInt(LevelStatusKey + (levelIndex + 1), (int)LevelStatus.Available);
        }

        
        PlayerPrefs.SetInt(LastCompletedLevelKey, levelIndex);
        PlayerPrefs.Save();
        Debug.Log($"Completed level {levelIndex}");
    }

    public void UnlockLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelButtons.Count)
        {
            levelButtons[levelIndex].SetLevelStatus(LevelStatus.Available);
            PlayerPrefs.SetInt(LevelStatusKey + levelIndex, (int)LevelStatus.Available);
            PlayerPrefs.Save();
        }
    }

    
    public void LoadLevel(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < levelButtons.Count)
        {
            levelButtons[levelIndex].SetLevelStatus(LevelStatus.Available);
        }
    }
    public int GetLastCompletedLevel()
    {
        return PlayerPrefs.GetInt(LastCompletedLevelKey, -1);  
    }
}
