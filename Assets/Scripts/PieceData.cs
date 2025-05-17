[System.Serializable]
public class PieceData
{
    public Animal Animal;
    public ShapeType Shape;
    public BorderColor Color;
    
    public bool IsMatch(PieceData other)
    {
        return other != null &&
               Animal == other.Animal &&
               Shape == other.Shape &&
               Color == other.Color;
    }
}

public enum ShapeType
{
    Square,
    Circle,
    Hexagon
}

public enum Animal
{
    Cat,
    Jellyfish,
    Frog,
    Koala,
    Turtle
}

public enum BorderColor
{
    Red,
    Blue,
    Green
}