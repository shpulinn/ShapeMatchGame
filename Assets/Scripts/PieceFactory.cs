using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PieceFactory : MonoBehaviour
{
    [SerializeField] private int totalPieces = 36;
    [SerializeField] private int uniqueCombinations = 12;
    
    [SerializeField] private List<AnimalSO> animalPool;
    [SerializeField] private List<ShapeSO> shapePool;
    [SerializeField] private List<ColorSO> colorPool;

    private const int MinPiecesPerGroup = 3;

    public List<PieceData> GenerateSet() => GenerateSet(totalPieces);

    public List<PieceData> GenerateSet(int requestedCount)
    {
        List<PieceData> result = new();
        List<PieceData> allCombos = GenerateAllPossibleCombinations();

        Shuffle(allCombos);

        int combosNeeded = Mathf.Min(uniqueCombinations, allCombos.Count);
        int maxGroups = requestedCount / MinPiecesPerGroup;
        int groupCount = Mathf.Min(combosNeeded, maxGroups);

        for (int i = 0; i < groupCount; i++)
        {
            for (int j = 0; j < MinPiecesPerGroup; j++)
                result.Add(allCombos[i]);
        }
        
        while (result.Count < requestedCount)
        {
            var random = allCombos[Random.Range(0, groupCount)];
            result.Add(random);
        }

        Shuffle(result);
        return result;
    }

    private List<PieceData> GenerateAllPossibleCombinations()
    {
        List<PieceData> result = new();
        foreach (var animal in animalPool)
            foreach (var shape in shapePool)
                foreach (var color in colorPool)
                    {
                        result.Add(new PieceData
                        {
                            Animal = animal,
                            Shape = shape,
                            Color = color
                        });
                    }
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