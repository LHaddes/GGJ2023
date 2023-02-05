using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AppleSlot : MonoBehaviour
{
    public Image appleImage;
    public TMP_Text appleName;
    
    [HideInInspector] public Sprite appleSprite;
    [HideInInspector] public string appleNameString;
    
    // Start is called before the first frame update
    void Start()
    {
        UnlockSlot();
    }

    public void UnlockSlot()
    {
        appleImage.sprite = appleSprite;
        appleName.text = appleNameString;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
