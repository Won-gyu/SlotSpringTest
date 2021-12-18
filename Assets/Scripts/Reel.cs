using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    private SlotMachine slotMachine;
    private Spring spring;
    [SerializeField]
    private bool spin;
    [SerializeField]
    private int topSymbolLevel;
    [SerializeField]
    private int bottomSymbolLevel;
    [SerializeField]
    private float symbolHeight;
    private int totalSymbols;
    public List<Transform> symbols;
    private float speed
    {
        get
        {
            return slotMachine.speed;
        }
    }

    private Transform centerSymbol
    {
        get
        {
            return symbols[topSymbolLevel];
        }
    }

    private void Awake()
    {
        totalSymbols = topSymbolLevel + bottomSymbolLevel + 1;
        // symbols = new List<Transform>(GetComponentsInChildren<Transform>());
        slotMachine = GetComponentInParent<SlotMachine>();
        spring = GetComponent<Spring>();

        symbols = new List<Transform>();
        for (int i = 0; i < totalSymbols; i++)
        {
            GetFromPool(i, true);
        }
    }

    private void ReturnToPool(Transform symbol)
    {
        symbol.GetComponent<PooledGameObject>().ReturnToPool();
        symbols.RemoveAt(totalSymbols - 1);
    }

    private float GetSymbolOriginPosY(int rowIndex)
    {
        return topSymbolLevel * symbolHeight - rowIndex * symbolHeight;
    }

    private void GetFromPool(int rowIndex = 0, bool absolutePosition = false)
    {
        Transform symbol = slotMachine.symbolPool.GetObject().transform;
        symbol.SetParent(transform);
        if (absolutePosition)
        {
            symbol.localPosition = Vector3.up * GetSymbolOriginPosY(rowIndex);
        }
        else
        {
            symbol.localPosition = symbols[0].localPosition + Vector3.up * symbolHeight;
        }
        symbols.Insert(rowIndex, symbol);
    }

    private void FixedUpdate()
    {
        if (!spin)
            return;


        for (int i = 0; i < totalSymbols; i++)
        {
            symbols[i].position += Vector3.down * speed;

            if (symbols[i].localPosition.y < -symbolHeight * bottomSymbolLevel)
            {
                ReturnToPool(symbols[i]);

                GetFromPool(0);
            }
        }
    }

    public void Stop()
    {
        spin = false;

        spring.y = centerSymbol.localPosition.y;
        spring.active = true;

        for (int i = 0; i < totalSymbols; i++)
        {
            symbols[i].localPosition = Vector3.up * GetSymbolOriginPosY(i);
        }
    }

    public void Spin()
    {
        spin = true;
        spring.active = false;
    }
}
