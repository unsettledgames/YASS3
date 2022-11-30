using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targettable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        FrequentlyAccessed.Instance.Player.SetTarget(this.gameObject);
    }

    private void OnMouseExit()
    {
        FrequentlyAccessed.Instance.Player.ResetTarget();
    }
}
