using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Encyclopedia : MonoBehaviour
{
    public GameObject uiPanel;
    
    [Header("Apple List")]
    public GameObject appleListPanel;
    public GameObject appleSlotPrefab;
    public Sprite unknownSprite;
    public List<GameObject> allFruitSlots = new List<GameObject>();

    [Header("Info Page")] public TMP_Text appleInfoName;
    public Image appleInfoImage;
    public TMP_Text appleInfoRarity;
    public TMP_Text appleInfoDescription;
    public Image appleRecipeSlot1;
    public Image appleRecipeSlot2;
    public Image appleRecipeSlot3;
    public Image appleRecipeSlot4;
    
    public static Encyclopedia Instance;
    private GameplayManager _gameplayManager;


    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _gameplayManager = GameplayManager.Instance;
       
        InitEncyclopedia();
    }

    // Update is called once per frame
    void Update()
        {
        
        }
        
    public void InitEncyclopedia()
    {
        for (int i = 0; i < _gameplayManager.allFruits.Count; i++)
        {
            GameObject slotObj = Instantiate(appleSlotPrefab, appleListPanel.transform);
            AppleSlot slot = slotObj.GetComponent<AppleSlot>();
            allFruitSlots.Add(slotObj);

            if (_gameplayManager.allFruits[i].isUnlocked)
            {
                slot.appleName = _gameplayManager.allFruits[i].name;
                slot.appleSprite = _gameplayManager.allFruits[i].img;
            }
            else
            {
                slot.appleName = "?????";
                slot.appleSprite = unknownSprite;
            }
            
            slot.GetComponent<Button>().onClick.AddListener(() => DisplayAppleInfo(EventSystem.current.currentSelectedGameObject));
        }
    }

    public void UpdateEncyclopedia()
    {
        for (int i = 0; i < _gameplayManager.allFruits.Count; i++)
        {
            AppleSlot slot = allFruitSlots[i].GetComponent<AppleSlot>();
            if (_gameplayManager.allFruits[i].isUnlocked)
            {
                slot.name = _gameplayManager.allFruits[i].name;
                slot.appleSprite = _gameplayManager.allFruits[i].img;
            }
            else
            {
                slot.name = "?????";
                slot.appleSprite = unknownSprite;
            }
        }
    }
        
    public void DisplayAppleInfo(GameObject slot)
    {
        Debug.Log("Display");
        AppleSlot info = slot.GetComponent<AppleSlot>();
        Fruit apple = null;
        for (int i = 0; i < _gameplayManager.allFruits.Count; i++)
        {
            if (_gameplayManager.allFruits[i].name == info.appleName)
            {
                apple = _gameplayManager.allFruits[i];
                break;
    }
        }

        if (apple != null)
    {
            Debug.Log("apple found");
            appleInfoName.text = apple.name;
            appleInfoImage.sprite = apple.img;
            appleInfoDescription.text = apple.description;
        
            foreach (Recipe r in apple.recipeList)
            {
                appleRecipeSlot1.sprite = r.fruit.img;
                //TODO Ajouter le sprite de l'outil
        
    }

            for (int i = 0; i < apple.recipeList.Count; i++)
            {
                
}
        }
    }
}
