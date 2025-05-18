using UnityEngine;

[CreateAssetMenu(menuName = "Piece/Shape")]
public class ShapeSO : ScriptableObject
{
    public string Id;
    public Sprite shapeSprite;
    public Sprite borderSprite;
    public GameObject colliderPrefab;
}