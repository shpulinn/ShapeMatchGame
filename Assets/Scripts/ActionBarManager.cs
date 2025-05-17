using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;


public class ActionBarManager : MonoBehaviour
{
    [SerializeField] private Transform slotParent;
    [SerializeField] private int maxCapacity = 7;
    
    [SerializeField] private PieceVisualDatabase visualDB;

    private List<PieceData> dataList = new();
    private List<PieceView> slots = new();

    public UnityEvent OnDefeat;
    public UnityEvent<PieceData> OnTripletMatched;

    private void Awake()
    {
        slots = slotParent.GetComponentsInChildren<PieceView>().ToList();
        ClearAllSlots();
    }

    public bool TryAddPiece(PieceData data)
    {
        if (dataList.Count + 1 >= maxCapacity)
        {
            OnDefeat?.Invoke();
            GameManager.Instance.GetAudioManager().PlayRandomSound(SoundType.Failure);
            return false;
        }
        
        dataList.Add(data);
        
        UpdateVisuals();

        CheckTriplet();

        return true;
    }

    private void CheckTriplet()
    {
        var groups = dataList
            .GroupBy(p => (p.Animal, p.Shape, p.Color))
            .FirstOrDefault(g => g.Count() >= 3);

        if (groups == null) return;
        
        var match = groups.Take(3).ToList();
        foreach (var m in match)
            dataList.Remove(m);

        OnTripletMatched?.Invoke(match[0]);
        GameManager.Instance.GetAudioManager().PlayRandomSound(SoundType.Success);
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < dataList.Count)
                slots[i].SetData(dataList[i], visualDB);
            else
                slots[i].Clear();
        }
    }

    public void ClearAllSlots()
    {
        dataList.Clear();
        UpdateVisuals();
    }

    public bool IsFull => dataList.Count >= maxCapacity;
}