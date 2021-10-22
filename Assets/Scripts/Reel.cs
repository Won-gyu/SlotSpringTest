using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reel : MonoBehaviour
{
    [SerializeField]
    private int topSymbolLevel;
    [SerializeField]
    private int bottomSymbolLevel;
    [SerializeField]
    private float symbolHeight;
    private int totalSymbols;
    private void Awake()
    {
        totalSymbols = topSymbolLevel + bottomSymbolLevel + 1;
    }
    private List<GameObject> symbols;
    private void FixedUpdate()
    {
        for (int i = 0; i < totalSymbols; i++)
        {
            symbols[i].transform.position += Vector3.down;

            if (symbols[i].transform.localPosition.y < -symbolHeight * bottomSymbolLevel)
            {
                symbols[i].GetComponent<PooledGameObject>().ReturnToPool();
                symbols.RemoveAt(totalSymbols - 1);
                totalSymbols--;
            }
        }
    }
}
