using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        transform.DOScale(Vector3.one, .2f);
    }
    
    public void Clear()
    {
        transform.DOScale(Vector3.zero, .2f);
        ShapeImage.sprite = null;
        Frame.sprite = null;
        gameObject.SetActive(false);
    }
}