using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppleSlot : MonoBehaviour
{
    public Image appleImage;
    public Sprite appleSprite;
    public string appleName;
    
    // Start is called before the first frame update
    void Start()
    {
        appleImage.sprite = appleSprite;
        GetComponentInChildren<TMP_Text>().text = appleName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
