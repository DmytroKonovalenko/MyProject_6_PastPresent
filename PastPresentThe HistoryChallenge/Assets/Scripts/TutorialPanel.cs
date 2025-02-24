using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TutorialPanel : MonoBehaviour
{
    public Sprite[] images;
    public string[] titles;
    public string[] descriptions;

    public Image currentImage;
    public TextMeshProUGUI currentTitle;
    public TextMeshProUGUI currentDescription;

    public Button switchButton;
    public TextMeshProUGUI buttonText;

    private int currentIndex = 0;
    private float fadeDuration = 0.3f;
    private Vector3 hiddenScale = new Vector3(0.7f, 0.7f, 0.7f);

    private void Start()
    {
        if (PlayerPrefs.GetInt("TutorialPanelShown", 0) == 1)
            gameObject.SetActive(false);

        UpdateContent(false);
    }

    public void SwitchContent()
    {
        currentIndex++;
        if (currentIndex >= images.Length)
        {
            currentIndex = 0;
            PlayerPrefs.SetInt("TutorialPanelShown", 1);
            PlayerPrefs.Save();
            gameObject.SetActive(false);
        }
        AnimatePageTurn();
    }

    private void AnimatePageTurn()
    {
        var fadeOutSequence = DOTween.Sequence();

        fadeOutSequence.Append(currentImage.DOFade(0, fadeDuration))
            .Join(currentImage.transform.DOScale(hiddenScale, fadeDuration))
            .Join(currentTitle.DOFade(0, fadeDuration))
            .Join(currentDescription.DOFade(0, fadeDuration));

        fadeOutSequence.AppendCallback(() => UpdateContent(true));

        fadeOutSequence.Append(currentImage.DOFade(1, fadeDuration))
            .Join(currentImage.transform.DOScale(Vector3.one, fadeDuration))
            .Join(currentTitle.DOFade(1, fadeDuration))
            .Join(currentDescription.DOFade(1, fadeDuration));
    }

    private void UpdateContent(bool animate)
    {
        currentImage.sprite = images[currentIndex];
        currentTitle.text = titles[currentIndex];
        currentDescription.text = descriptions[currentIndex];

        buttonText.text = currentIndex == 0 ? "START" : currentIndex == images.Length - 1 ? "LET'S GO!" : "CONTINUE";

        if (!animate)
        {
            currentImage.transform.localScale = Vector3.one;

            currentImage.color = Color.white;
            currentTitle.color = Color.white;
            currentDescription.color = Color.white;
        }
        else
        {
            currentImage.transform.localScale = hiddenScale;

            currentImage.color = new Color(1, 1, 1, 0);
            currentTitle.color = new Color(1, 1, 1, 0);
            currentDescription.color = new Color(1, 1, 1, 0);

            var fadeInSequence = DOTween.Sequence();

            fadeInSequence.Append(currentImage.DOFade(1, fadeDuration))
                .Join(currentImage.transform.DOScale(Vector3.one, fadeDuration))
                .Join(currentTitle.DOFade(1, fadeDuration))
                .Join(currentDescription.DOFade(1, fadeDuration));
        }
    }
}
