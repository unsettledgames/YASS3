using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private float ZoomSpeed;
    [SerializeField] private float ColorChangeSpeed;

    [SerializeField] private Vector2 SizeRange;
    [SerializeField] private Color StartColor;
    [SerializeField] private Color EndColor;

    private Material m_Material;

    private bool m_Locked = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float scale = SizeRange.x;
        Color endColor = StartColor;

        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);

        if (FrequentlyAccessed.Instance.Player.Target != null)
        {
            Vector3 targetPos = FrequentlyAccessed.Instance.Camera.WorldToScreenPoint(FrequentlyAccessed.Instance.Player.Target.transform.position);
            scale = Mathf.Min(transform.localScale.x + Time.deltaTime * ZoomSpeed, SizeRange.y);
            transform.position = new Vector3(targetPos.x, targetPos.y, transform.position.z);

            if (Input.GetButton("Fire1"))
            {
                endColor = EndColor;
                m_Locked = true;
            }
            else
            {
                endColor = StartColor;
                m_Locked = false;
            }
        }
        else if (!m_Locked)
        {
            scale = Mathf.Max(transform.localScale.x - Time.deltaTime * ZoomSpeed, SizeRange.x);
            endColor = StartColor;
        }

        transform.localScale = new Vector3(scale, scale, scale);
        m_Material.color = Color.Lerp(m_Material.color, endColor, Time.deltaTime * ColorChangeSpeed);
    }
}
