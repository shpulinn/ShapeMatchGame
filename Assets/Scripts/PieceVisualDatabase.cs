using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Database/Piece Visuals")]
public class PieceVisualDatabase : ScriptableObject
{
    public List<ShapeEntry> Shapes;
    public List<AnimalEntry> Animals;
    public List<ColorEntry> Colors;

    [System.Serializable] public struct ShapeEntry { 
        public ShapeType type; 
        public Sprite sprite;
        public Sprite borderSprite;
        public GameObject collider;
    }
    [System.Serializable] public struct AnimalEntry { public Animal animal; public Sprite sprite; }
    [System.Serializable] public struct ColorEntry { public BorderColor color; public Color unityColor; }

    public Sprite GetShape(ShapeType type) => Shapes.Find(s => s.type == type).sprite;
    public Sprite GetBorderShape(ShapeType type) => Shapes.Find(s => s.type == type).borderSprite;
    public GameObject GetColliderGO(ShapeType type) => Shapes.Find(s => s.type == type).collider;
    public Sprite GetAnimal(Animal animal) => Animals.Find(a => a.animal == animal).sprite;
    public Color GetColor(BorderColor color) => Colors.Find(c => c.color == color).unityColor;
    
}