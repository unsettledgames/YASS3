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
    // Start is called before the first frame update
    void Start()
    {
        m_Material = GetComponent<Image>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float scale;
        Color endColor;
        transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);

        if (FrequentlyAccessed.Instance.Player.Target != null)
        {
            scale = Mathf.Min(transform.localScale.x + Time.deltaTime * ZoomSpeed, SizeRange.y);
            endColor = EndColor;
        }
        else
        {
            scale = Mathf.Max(transform.localScale.x - Time.deltaTime * ZoomSpeed, SizeRange.x);
            endColor = StartColor;
        }

        transform.localScale = new Vector3(scale, scale, scale);
        m_Material.color = Color.Lerp(m_Material.color, endColor, Time.deltaTime * ColorChangeSpeed);
    }
}
