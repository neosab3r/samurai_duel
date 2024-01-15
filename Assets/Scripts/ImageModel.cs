using System;
using DG.Tweening;
using UnityEngine;

public class ImageModel : MonoBehaviour
{
    private Tweener tweenerScale;
    private Vector3 startScale;
    public float TweenerScaleDuration;
    public float ScaleOffset;
    private bool isStarted = false;
    
    public void StartTweener()
    {
        isStarted = true;
        startScale = transform.localScale;
        
        gameObject.SetActive(true);
        
        tweenerScale = DOTween.To(() => transform.localScale,
                (pos) => transform.localScale = pos,
                transform.localScale, TweenerScaleDuration)
            .SetEase(Ease.OutBack).SetAutoKill(false);
        
        var scaleTo = new Vector3(startScale.x + ScaleOffset, startScale.y + ScaleOffset, startScale.z + ScaleOffset);
        
        tweenerScale.ChangeEndValue(scaleTo, true).Restart();
    }

    public void StopTweener()
    {
        isStarted = false;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (isStarted == false)
        {
            return;
        }
        
        var scaleTo = new Vector3(startScale.x + ScaleOffset, startScale.y + ScaleOffset, startScale.z + ScaleOffset);
        if (Vector3.Distance(transform.localScale, scaleTo) <= 0.1)
        {
            ScaleOffset = -ScaleOffset;
            var newPositionTo = new Vector3(startScale.x + ScaleOffset, startScale.y + ScaleOffset, startScale.z + ScaleOffset);
            tweenerScale.ChangeEndValue(newPositionTo, true).Restart();
        }
    }
}