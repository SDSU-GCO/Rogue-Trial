using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayImageRectData : MonoBehaviour
{
    [SerializeField, HideInInspector]
    RectTransform rectTransform = null;
    private void OnValidate()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"Rect->{rectTransform.position.x/Screen.width}:{rectTransform.position.y/Screen.height}");
    }
}
