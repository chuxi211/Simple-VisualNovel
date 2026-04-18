using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Story;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    public Image image;
    //有问题
    
    public Position position;
    private void Awake()
    {
        image = GetComponent<Image>();
        image.preserveAspect = true;
    }
    
    public void SetSprite( Sprite sprite)
    {
        image.sprite = sprite;
        //为空则隐藏图片
        if(sprite==null)
        {
            image.color = new Color(1, 1, 1, 0);
            return;
        }
        image.color = new Color(1, 1, 1, 1);
        //根据图片宽高比调整图片大小，保持高度为80
        float height = 80;
        float ratio =(float) sprite.rect.width / sprite.rect.height;
        RectTransform rt = image.rectTransform;
        rt.sizeDelta = new Vector2(height * ratio, height);
    }
    IEnumerator InterpolationMovement(Vector2 endPosition)
    {
        Vector2 startPosition = image.rectTransform.anchoredPosition;
        float t = 0;
        while (t < 0.5f)
        {
            t += Time.deltaTime;
            image.rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, t);
            yield return null;
        }
        image.rectTransform.anchoredPosition = endPosition;
    }
    public void SetPosition(Position position)
    {
        this.position = position;
        switch (position)
        {
            case Position.Left:
                StartCoroutine( InterpolationMovement(new Vector2(-400,-200)) );
                break;
            case Position.Middle:
                StartCoroutine(InterpolationMovement(new Vector2(0, -200)));
                break;
            case Position.Right:
                StartCoroutine(InterpolationMovement(new Vector2(400, -200)));
                break;
            case Position.Unknown:
                break;
        }
        Debug.Log("Position:" + position);   
    }
}
