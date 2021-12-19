using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SlotMachine : MonoBehaviour
{
    public GameObjectPool symbolPool;
    public float spinningSpeed;

    private Reel[] reels;
    public Sprite[] sprites;

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
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].Stop();
        }
    }

    [Button]
    public void Spin()
    {
        for (int i = 0; i < reels.Length; i++)
        {
            reels[i].Spin();
        }
    }
}
