using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class PieceController : MonoBehaviour
{
    public event Action<PieceController> OnDestroyed;
    
    [SerializeField] private PieceView _pieceView;

    private PieceData _data;
    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
    }

    public void Initialize(PieceData data, PieceVisualDatabase visualDB)
    {
        _data = data;
        
        _pieceView.Apply(data, visualDB);

        var collider = Instantiate(visualDB.GetColliderGO(data.Shape), transform);
    }

    private void OnMouseDown()
    {
        if (_gameManager.State == GameState.Playing && _gameManager.GetActionBarManager().TryAddPiece(_data))
        {
            GameManager.Instance.GetAudioManager().PlayRandomSound(SoundType.Pick);
            StartCoroutine(ScaleAndDeleteCoroutine());
        }
    }

    private IEnumerator ScaleAndDeleteCoroutine()
    {
        Tween myTween = transform.DOScale(Vector3.zero, .2f);
        yield return myTween.WaitForCompletion();
        DestroyPiece();
    }

    private void DestroyPiece()
    {
        OnDestroyed?.Invoke(this);
        Destroy(gameObject);
    }
}