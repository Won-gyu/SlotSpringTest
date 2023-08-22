using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    private enum ReelState
    {
        Stopped,
        Stopping,
        Spinning
    }
    private SlotMachine slotMachine;
    private Spring spring;
    [SerializeField]
    private ReelState reelState;
    [SerializeField]
    private int topSymbolLevel;
    [SerializeField]
    private int bottomSymbolLevel;
    [SerializeField]
    private float symbolHeight;
    private int totalSymbols;
    public List<Symbol> symbols;
    private float speed
    {
        get
        {
            return slotMachine.spinningSpeed;
        }
    }

    public Symbol centerSymbol
    {
        get
        {
            return symbols[topSymbolLevel];
        }
    }

    private void Awake()
    {
        reelState = ReelState.Stopped;
        totalSymbols = topSymbolLevel + bottomSymbolLevel + 1;
        slotMachine = GetComponentInParent<SlotMachine>();
        spring = GetComponent<Spring>();

        symbols = new List<Symbol>();
        for (int i = 0; i < totalSymbols; i++)
        {
            GetFromPool(i, true);
        }
    }

    private void ReturnToPool(Symbol symbol)
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
        Transform trSymbol = slotMachine.symbolPool.GetObject().transform;
        trSymbol.SetParent(transform);
        if (absolutePosition)
        {
            trSymbol.localPosition = Vector3.up * GetSymbolOriginPosY(rowIndex);
        }
        else
        {
            trSymbol.localPosition = symbols[0].transform.localPosition + Vector3.up * symbolHeight;
        }
        Symbol symbol = trSymbol.GetComponent<Symbol>();
        symbols.Insert(rowIndex, symbol);
        symbol.Init(slotMachine, Random.Range(0, slotMachine.SymbolTypeCount));
    }

    private void FixedUpdate()
    {
        if (reelState < ReelState.Stopping)
            return;

        for (int i = 0; i < totalSymbols; i++)
        {
            symbols[i].transform.position += Vector3.down * speed;

            if (symbols[i].transform.localPosition.y < -symbolHeight * bottomSymbolLevel)
            {
                ReturnToPool(symbols[i]);

                GetFromPool(0);
            }
        }

        // TODO: should fix it?
        if (reelState == ReelState.Stopping)
        {
            if (centerSymbol.transform.localPosition.y <= 0)
            {
                BeingStopped();
            }
        }
    }

    private void BeingStopped()
    {
        reelState = ReelState.Stopped;
        spring.v = -speed;
        spring.active = true;
        
        for (int i = 0; i < totalSymbols; i++)
        {
            symbols[i].transform.localPosition = new Vector3(symbols[i].transform.localPosition.x, GetSymbolOriginPosY(i), symbols[i].transform.localPosition.z);
        }
    }

    public void Stop()
    {
        reelState = ReelState.Stopping;
    }

    public void Spin()
    {
        reelState = ReelState.Spinning;
        spring.active = false;
    }
}
