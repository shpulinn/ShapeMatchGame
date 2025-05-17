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

    [SerializeField] private PieceVisualDatabase visualDB;
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
            controller.Initialize(pieces[i], visualDB);
                
            // Subscribe to the piece's OnDestroyed event
            controller.OnDestroyed += HandlePieceDestroyed;
            _activePieces.Add(controller);

            yield return new WaitForSeconds(0.05f);
        }
    }

    private void HandlePieceDestroyed(PieceController piece)
    {
        // Remove the piece from the active list
        _activePieces.Remove(piece);

        // Check for victory condition
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
        // Unsubscribe from all piece events to prevent memory leaks
        foreach (var piece in _activePieces)
        {
            if (piece != null)
            {
                piece.OnDestroyed -= HandlePieceDestroyed;
            }
        }
    }

    private void ClearLevel()
    {
        // Unsubscribe from all piece events
        foreach (var piece in _activePieces)
        {
            if (piece != null)
            {
                piece.OnDestroyed -= HandlePieceDestroyed;
            }
        }

        _activePieces.Clear();
        StartLevel();
    }

    // Clean up subscriptions when the BoardManager is destroyed
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