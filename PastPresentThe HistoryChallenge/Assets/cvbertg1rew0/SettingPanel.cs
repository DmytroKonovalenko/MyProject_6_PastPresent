using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;



public class SettingPanel : MonoBehaviour
{
    [SerializeField] private PanelUserName panelUserName;
    [SerializeField] private Image displayImage;
    [SerializeField] private Image displayWelcomeImage;

    private const string ImagePathKey = "SelectedImagePath";
    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerName"))
        {
            string savedName = PlayerPrefs.GetString("PlayerName");
            panelUserName.UpdateDisplayName(savedName);
        }
        LoadAvatar();
    }
    public void LoadAvatar()
    {
        
        if (PlayerPrefs.HasKey(ImagePathKey))
        {
            string savedPath = PlayerPrefs.GetString(ImagePathKey);
            if (File.Exists(savedPath))  
            {
              
             
                
            }
            else
            {
                Debug.LogWarning("Saved image path no longer exists.");
            }
        }
    }
        
    
    void Update()
    {
        
    }
    private Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
    }
   
}
