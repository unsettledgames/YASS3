using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeDestroyer : MonoBehaviour
{
    [SerializeField]
    private float LifeTime;
    [SerializeField]
    private bool ApplyToChildren; 

    [Header("Fading Options")]
    [SerializeField]
    private float FadeSpeed;
    [SerializeField]
    private bool FadeMeshOpacity;

    MeshRenderer m_MeshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        m_MeshRenderer = GetComponent<MeshRenderer>();

        if (ApplyToChildren)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                TimeDestroyer td = transform.GetChild(i).gameObject.AddComponent<TimeDestroyer>();
                td.LifeTime = LifeTime;
                td.FadeSpeed = FadeSpeed;
                td.FadeMeshOpacity = FadeMeshOpacity;
            }

            Destroy(GetComponent<TimeDestroyer>());
        }

        StartCoroutine(StartDestroying());
    }

    private IEnumerator StartDestroying()
    {
        yield return new WaitForSeconds(LifeTime);
        
        if (FadeMeshOpacity)
        {
            if (m_MeshRenderer == null)
            {
                Destroy(this.gameObject);
                yield return new WaitForEndOfFrame();
            }
            Color currColor = m_MeshRenderer.material.GetColor("_Color");

            while (currColor.a > 0.0f)
            {
                currColor.a -= FadeSpeed * Time.deltaTime;
                m_MeshRenderer.material.SetColor("_Color", currColor);
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
