using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConvertTextMesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = GetComponent<TextMeshProUGUI>().text.faConvert();
    }


}
