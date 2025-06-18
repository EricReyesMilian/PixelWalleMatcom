using UnityEngine;
using TMPro;
public class SetNoWrap : MonoBehaviour
{
    TextMeshProUGUI target;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        target.textWrappingMode = TextWrappingModes.NoWrap;
    }
}
