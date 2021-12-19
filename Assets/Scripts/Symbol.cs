using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Symbol : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public void Init(SlotMachine slotMachine, int symbolIndex)
    {
        spriteRenderer.sprite = slotMachine.sprites[symbolIndex];
    }
}
