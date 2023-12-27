using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float offsetX;
    [SerializeField] private float TweenerDuration;
    
    private Transform transform;
    private Tweener tweenerMove;
    private Vector3 startPosition;

    private void Start()
    {
        transform = gameObject.GetComponent<Transform>();

        startPosition = transform.position;

        tweenerMove = DOTween.To(() => transform.position,
                (pos) => transform.position = pos,
                transform.position, TweenerDuration)
            .SetEase(Ease.OutCubic).SetAutoKill(false);

        var positionTo = new Vector3(startPosition.x + offsetX, 0, 0);
        
        tweenerMove.ChangeEndValue(positionTo, true).Restart();
    }

    private void Update()
    {
        var positionTo = new Vector3(startPosition.x + offsetX, 0, 0);
        
        if (Vector3.Distance(transform.position, positionTo) <= 0.1)
        {
            offsetX = -offsetX;
            var newPositionTo = new Vector3(startPosition.x + offsetX, 0, 0);
            tweenerMove.ChangeEndValue(newPositionTo, true).Restart();
        }
    }
}
