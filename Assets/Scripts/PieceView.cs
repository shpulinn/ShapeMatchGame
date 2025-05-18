using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PieceView : MonoBehaviour
{
    [SerializeField] private Image ShapeImage;
    [SerializeField] private Image AnimalImage;
    [SerializeField] private Image Frame;

    public void Apply(PieceData data)
    {
        SetVisualData(data);
    }

    public void SetData(PieceData data)
    {
        SetVisualData(data);

        gameObject.SetActive(true);
    }
    
    private void SetVisualData(PieceData data)
    {
        ShapeImage.sprite = data.Shape.shapeSprite;
        AnimalImage.sprite = data.Animal.sprite;
        Frame.sprite = data.Shape.borderSprite;
        Frame.color = data.Color.unityColor;
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