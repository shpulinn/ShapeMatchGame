using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    [SerializeField] private int columns = 6;
    [SerializeField] private int rows = 6;

    [SerializeField] private GameObject piecePrefab;
    [SerializeField] private Transform spawnTop;
    [SerializeField] private Transform boardArea;

    [SerializeField] private PieceFactory factory;

    private List<PieceController> _activePieces = new();

    public void StartLevel()
    {
        StartCoroutine(SpawnPieces());
    }

    private IEnumerator SpawnPieces()
    {
        List<PieceData> pieces = factory.GenerateSet();
        float spacing = 1f;
        float startX = -((columns - 1) * spacing) / 2f;

        for (int i = 0; i < pieces.Count; i++)
        {
            int col = i % columns;
            float x = startX + col * spacing;
            Vector2 spawnPos = new Vector2(x, spawnTop.position.y);

            GameObject obj = Instantiate(piecePrefab, spawnPos, Quaternion.identity, boardArea);
            var controller = obj.GetComponent<PieceController>();
            controller.Initialize(pieces[i]);
            
            controller.OnDestroyed += HandlePieceDestroyed;
            _activePieces.Add(controller);

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void HandlePieceDestroyed(PieceController piece)
    {
        _activePieces.Remove(piece);
        
        if (_activePieces.Count == 0)
        {
            OnBoardCleared();
        }
    }

    private void OnBoardCleared()
    {
        GameManager.Instance.WinGame();
    }

    public void ResetBoard()
    {
        foreach (var piece in _activePieces)
        {
            if (piece != null)
            {
                piece.OnDestroyed -= HandlePieceDestroyed;
            }
        }
    }
    
    // used by button (onClick action)
    public void ReshuffleBoard()
    {
        StartCoroutine(ReshuffleRoutine());
    }

    private IEnumerator ReshuffleRoutine()
    {
        
        foreach (var piece in _activePieces)
        {
            if (piece != null)
            {
                piece.OnDestroyed -= HandlePieceDestroyed;
            }
            Destroy(piece.gameObject);
        }

        int count = _activePieces.Count;
        _activePieces.Clear();

        yield return new WaitForSeconds(0.3f);
        
        List<PieceData> newPieces = factory.GenerateSet(count);

        float spacing = 1f;
        float startX = -((columns - 1) * spacing) / 2f;

        for (int i = 0; i < newPieces.Count; i++)
        {
            int col = i % columns;
            float x = startX + col * spacing;
            Vector2 spawnPos = new Vector2(x, spawnTop.position.y);

            GameObject obj = Instantiate(piecePrefab, spawnPos, Quaternion.identity, boardArea);
            var controller = obj.GetComponent<PieceController>();
            controller.Initialize(newPieces[i]);

            controller.OnDestroyed += HandlePieceDestroyed;
            _activePieces.Add(controller);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    private void OnDestroy()
    {
        foreach (var piece in _activePieces)
        {
            if (piece != null)
            {
                piece.OnDestroyed -= HandlePieceDestroyed;
            }
        }
    }
}