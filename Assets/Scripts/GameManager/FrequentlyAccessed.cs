using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequentlyAccessed : MonoBehaviour
{
    public PlayerController Player;
    public GameObject Camera;

    public static FrequentlyAccessed Instance;

    private void Start()
    {
        Instance = this;
    }
}
