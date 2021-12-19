using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class SlotMachine : MonoBehaviour
{
    public GameObjectPool symbolPool;
    public float spinningSpeed;

    private Reel[] reels;
    public Sprite[] sprites;
    public Action<int, int> OnWin;

    public int SymbolTypeCount
    {
        get
        {
            return sprites.Length;
        }
    }

    private void Awake()
    {
        reels = GetComponentsInChildren<Reel>();
    }

    [Button]
    public void Stop()
    {
        StartCoroutine(StopCoroutine());
    }

    private IEnumerator StopCoroutine()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].Stop();
            yield return new WaitForSeconds(0.5f);
        }
        Stopped();
    }

    private void Stopped()
    {
        int targetSymbol = reels[0].centerSymbol.SymbolIndex;
        int hitSymbolCount = 1;
        for (int i = 1; i < reels.Length; i++)
        {
            if (reels[i].centerSymbol.SymbolIndex == targetSymbol)
            {
                hitSymbolCount++;
            }
            else
            {
                break;
            }
        }

        if (hitSymbolCount == reels.Length)
        {
            OnWin(targetSymbol, hitSymbolCount);
        }
    }

    [Button]
    public void Spin()
    {
        // StartCoroutine(SpinCoroutine());
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].Spin();
        }
    }

    // private IEnumerator SpinCoroutine()
    // {
    //     for (int i = 0; i < reels.Length; i++)
    //     {
    //         reels[i].Spin();
    //         yield return new WaitForSeconds(0.05f);
    //     }
    // }
}
