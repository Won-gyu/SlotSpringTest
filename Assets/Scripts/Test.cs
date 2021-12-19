using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private SlotMachine slotMachine;

    private void Awake()
    {
        slotMachine.OnWin += OnWin;
    }

    private void OnWin(int targetSymbol, int hitSymbolCount)
    {
        Debug.Log("TEST");
    }
}
