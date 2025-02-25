using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelUserName : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;  
    [SerializeField] private TextMeshProUGUI displayNameText;  
    [SerializeField] private TextMeshProUGUI welcomeText;  
    [SerializeField] private Button saveButton;  

    private const string PlayerNameKey = "PlayerName";  

    private void Start()
    {
       
        if (PlayerPrefs.HasKey(PlayerNameKey))
        {
            string savedName = PlayerPrefs.GetString(PlayerNameKey);
            displayNameText.text = savedName;
          

           
            UpdateWelcomeText(savedName);
        }

      
        saveButton.onClick.AddListener(SaveName);
    }

    
    public void SaveName()
    {
        string playerName = inputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            
            PlayerPrefs.SetString(PlayerNameKey, playerName);
            PlayerPrefs.Save();

            
            UpdateDisplayName(playerName);
            UpdateWelcomeText(playerName);
        }
        else
        {
            Debug.LogWarning("Name field is empty!");
        }
    }
    public void PlacehosdfsfwlderMethod1() { }
    public void PlaceholderewfwMethod2() { }
    public void PlaceholderMexcvxcvthod3() { }
    public void Placeholderewrf23ewMethod4() { }
    public void PlaceholderMsdfdsethod5() { }

    public void UpdateDisplayName(string newName)
    {
        displayNameText.text = newName;
    }

    
    public void UpdateWelcomeText(string playerName)
    {
        welcomeText.text = $"Hello {playerName}!";
    }
}
