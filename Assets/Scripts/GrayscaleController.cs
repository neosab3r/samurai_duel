using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrayscaleController : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> spriteRenderers;
    [SerializeField] private TouchController touchController;
    [SerializeField] private TextMeshProUGUI textReady;
    [SerializeField] private ParticleSystem cashParticleSystem;

    private float duration = 2f;
    
    void Start()
    {
        StartCoroutine(GrayscaleRoutine(duration, true, true));
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

    private IEnumerator GrayscaleRoutine(float duration, bool isGrayscale, bool isShowTextReady = false)
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
        if (isShowTextReady)
        {
            textReady.gameObject.SetActive(true);
            touchController.StartGame();
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
