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
                Texture2D savedTexture = NativeGallery.LoadImageAtPath(savedPath, 512);  
                if (savedTexture != null)
                {
                    
                    Sprite savedSprite = SpriteFromTexture2D(savedTexture);

                    
                    displayImage.sprite = savedSprite;
                    displayWelcomeImage.sprite = savedSprite;
                    Debug.Log("Loaded saved image.");
                }
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
    public void PickImage(int maxSize)
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                
                Texture2D texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                
                Sprite newSprite = SpriteFromTexture2D(texture);

                
                displayImage.sprite = newSprite;
                displayWelcomeImage.sprite = newSprite;

               
                PlayerPrefs.SetString(ImagePathKey, path);
                PlayerPrefs.Save();

                Debug.Log("Image successfully applied and saved.");
            }
        });

        Debug.Log("Permission result: " + permission);
    }
}
