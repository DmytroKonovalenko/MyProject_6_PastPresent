using UnityEngine;
using DG.Tweening;

public class PopupAnimation : MonoBehaviour
{
    [SerializeField] private float animationDuration = 0.5f; 
    [SerializeField] private float animationOffset = 500f;   

    private RectTransform rectTransform;
    private Vector2 initialPosition; 

    private void Awake()
    {
        
        rectTransform = GetComponent<RectTransform>();

        
        initialPosition = rectTransform.anchoredPosition;
    }

    private void OnEnable()
    {
        
        rectTransform.anchoredPosition = new Vector2(initialPosition.x, initialPosition.y + animationOffset);

        
        AnimateShow();
    }

    

   
    public void AnimateShow()
    {
        
        rectTransform.DOAnchorPosY(initialPosition.y, animationDuration).SetEase(Ease.OutBounce);
    }

    public void ClosePopup()
    {
       
        rectTransform.DOAnchorPosY(initialPosition.y - animationOffset, animationDuration).SetEase(Ease.InBack)
            .OnComplete(() => gameObject.SetActive(false)); 
    }
}
