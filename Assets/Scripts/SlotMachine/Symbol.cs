using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private int symbolIndex;
    public int SymbolIndex
    {
        get
        {
            return symbolIndex;
        }
    }

    public void Init(SlotMachine slotMachine, int symbolIndex)
    {
        this.symbolIndex = symbolIndex;
        spriteRenderer.sprite = slotMachine.sprites[symbolIndex];
    }
}
