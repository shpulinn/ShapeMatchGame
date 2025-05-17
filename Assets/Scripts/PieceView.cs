using UnityEngine;
using UnityEngine.UI;

public class PieceView : MonoBehaviour
{
    [SerializeField] private Image ShapeImage;
    [SerializeField] private Image AnimalImage;
    [SerializeField] private Image Frame;

    public void Apply(PieceData data, PieceVisualDatabase visualDB)
    {
        SetVisualData(data, visualDB);
    }

    public void SetData(PieceData data, PieceVisualDatabase visualDB)
    {
        SetVisualData(data, visualDB);

        gameObject.SetActive(true);
    }
    
    private void SetVisualData(PieceData data, PieceVisualDatabase visualDB)
    {
        ShapeImage.sprite = visualDB.GetShape(data.Shape);
        AnimalImage.sprite = visualDB.GetAnimal(data.Animal);
        Frame.sprite = visualDB.GetBorderShape(data.Shape);
        Frame.color = visualDB.GetColor(data.Color);
    }
    
    public void Clear()
    {
        ShapeImage.sprite = null;
        Frame.sprite = null;
        gameObject.SetActive(false);
    }
}