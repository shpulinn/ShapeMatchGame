using System.Collections.Generic;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
    [SerializeField] private int totalPieces = 36;
    [SerializeField] private int uniqueCombinations = 12;

    private int minPiecesCount = 3;

    public List<PieceData> GenerateSet()
    {
        List<PieceData> result = new();
        List<PieceData> allCombos = GenerateAllPossibleCombinations();
        
        Shuffle(allCombos);
        for (int i = 0; i < uniqueCombinations && i < allCombos.Count; i++)
        {
            for (int j = 0; j < minPiecesCount; j++)
                result.Add(allCombos[i]);
        }

        Shuffle(result);
        return result;
    }

    private List<PieceData> GenerateAllPossibleCombinations()
    {
        List<PieceData> result = new();
        foreach (Animal animal in System.Enum.GetValues(typeof(Animal)))
        foreach (ShapeType shape in System.Enum.GetValues(typeof(ShapeType)))
        foreach (BorderColor color in System.Enum.GetValues(typeof(BorderColor)))
            result.Add(new PieceData { Animal = animal, Shape = shape, Color = color });
        return result;
    }

    private void Shuffle<T>(List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int rnd = Random.Range(0, i + 1);
            (list[i], list[rnd]) = (list[rnd], list[i]);
        }
    }
}