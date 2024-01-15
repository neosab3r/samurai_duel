using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GrayscaleController : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    [SerializeField] private TouchController touchController;
    [SerializeField] private ImageModel ReadyImage;
    [SerializeField] private ImageModel DuelImage;
    private Action OnStartGame;

    private float duration = 2f;
    
    void Start()
    {
        OnStartGame += RandomTimeForStartGame;
        StartCoroutine(GrayscaleRoutine(duration, true, true));
        ReadyImage.StartTweener();
    }

    void Update()
    {
        
    }

    /*public void ShowWinner(bool isPlayerOneWin)
    {
        cashParticleSystem.Play();
        if (isPlayerOneWin == true)
        {
            
        }
    }*/

    private void RandomTimeForStartGame()
    {
        float startTime = Random.Range(1.2f, 3.3f);
        Debug.Log("Random time: " + startTime);
        StartCoroutine(ReadyGrayScaleCoroutine(startTime));
    }

    private IEnumerator ReadyGrayScaleCoroutine(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(DuelImageTime(1f));
        StartCoroutine(GrayscaleRoutine(0.1f, false));
        DuelImage.StartTweener();
        touchController.StartGame();
    }

    private IEnumerator DuelImageTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Debug.Log("0.5");
        DuelImage.StopTweener();
    }
    
    private IEnumerator GrayscaleRoutine(float duration, bool isGrayscale, bool isShowReadyImage = false)
    {
        float time = 0;
        while (duration > time)
        {
            float durationFrame = Time.deltaTime;
            float ratio = time / duration;
            float grayAmount = isGrayscale ? ratio : 1 - ratio;
            SetGrayscale(grayAmount);
            time += durationFrame;
            yield return null;
        }

        SetGrayscale(isGrayscale ? 1 : 0);
        if (isShowReadyImage)
        {
            OnStartGame?.Invoke();
            ReadyImage.StopTweener();
        }
    }

    private void SetGrayscale(float grayAmount)
    {
        foreach (var spriteRenderer in spriteRenderers)
        {
            spriteRenderer.material.SetFloat("_GrayscaleAmount", grayAmount);
        }
    }
}
