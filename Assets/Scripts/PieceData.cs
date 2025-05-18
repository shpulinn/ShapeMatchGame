[System.Serializable]
public class PieceData
{
    public ShapeSO Shape;
    public AnimalSO Animal;
    public ColorSO Color;

    public bool IsMatch(PieceData other)
    {
        return Shape == other.Shape && Animal == other.Animal && Color == other.Color;
    }
}