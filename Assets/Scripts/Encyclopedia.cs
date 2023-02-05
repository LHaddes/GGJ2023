using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Events;

public class Encyclopedia : MonoBehaviour
{
    public GameObject uiPanel;
    private bool _panelIsActive;

    [Header("Apple List")] public GameObject appleListPanel;
    public GameObject appleSlotPrefab;
    public Sprite unknownSprite;
    public List<GameObject> allFruitSlots = new List<GameObject>();

    [Header("Info Page")] public TMP_Text appleInfoName;
    public Image appleInfoImage;
    public TMP_Text appleInfoRarity;
    public TMP_Text appleInfoDescription;
    public Image appleRecipeSlot1;
    public Image appleToolSlot1;
    public Image appleRecipeSlot2;
    public Image appleToolSlot2;
    public GameObject firstRecipePanel;
    public GameObject secondRecipePanel;

    public static Encyclopedia Instance;
    public UnityEvent<bool> ToggleEncyclopedia = new UnityEvent<bool>();
    public UnityEvent<bool> ClickInEncyclopedia = new UnityEvent<bool>();
    private GameplayManager _gameplayManager;

    public ToolPropertiesDefinition toolDef;


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _gameplayManager = GameplayManager.Instance;
        _gameplayManager.inventory.unlockAppleInEncyclopedia.AddListener(UpdateEncyclopedia);
        InitEncyclopedia();
    }


    public void ToggleEncyclopediaPanel()
    {
        if (_panelIsActive)
        {
            uiPanel.SetActive(false);
            _panelIsActive = false;
            ToggleEncyclopedia.Invoke(true);
        }
        else
        {
            uiPanel.SetActive(true);
            _panelIsActive = true;
            ToggleEncyclopedia.Invoke(false);
        }
    }

    void Update ()
    {
        if (_panelIsActive && Input.GetMouseButtonDown(0))
            ClickInEncyclopedia.Invoke(true);
    }


    public void InitEncyclopedia()
    {
        for (int i = 0; i < _gameplayManager.inventory.allFruits.Count; i++)
        {
            GameObject slotObj = Instantiate(appleSlotPrefab, appleListPanel.transform);
            AppleSlot slot = slotObj.GetComponent<AppleSlot>();
            allFruitSlots.Add(slotObj);

            if (_gameplayManager.inventory.allFruits[i].isUnlocked)
            {
                slot.name = _gameplayManager.inventory.allFruits[i].name;
                slot.appleNameString = _gameplayManager.inventory.allFruits[i].name;
                slot.appleSprite = _gameplayManager.inventory.allFruits[i].sprite;
            }
            else
            {
                slot.name = "?????";
                slot.appleNameString = "?????";
                slot.appleSprite = unknownSprite;
            }

            slot.GetComponent<Button>().onClick.AddListener(() => DisplayAppleInfo(EventSystem.current.currentSelectedGameObject));

            slot.UnlockSlot();

            if (i == 0)
                DisplayAppleInfo(slot.gameObject);
        }
    }

    public void UpdateEncyclopedia()
    {
        for (int i = 0; i < _gameplayManager.inventory.allFruits.Count; i++)
        {
            AppleSlot slot = allFruitSlots[i].GetComponent<AppleSlot>();
            if (_gameplayManager.inventory.allFruits[i].isUnlocked)
            {
                slot.name = _gameplayManager.inventory.allFruits[i].name;
                slot.appleNameString = _gameplayManager.inventory.allFruits[i].name;
                slot.appleSprite = _gameplayManager.inventory.allFruits[i].sprite;
            }
            else
            {
                slot.name = "?????";
                slot.appleNameString = "?????";
                slot.appleSprite = unknownSprite;
            }

            slot.UnlockSlot();
        }
    }

    public void DisplayAppleInfo(GameObject slot)
    {
        AppleSlot info = slot.GetComponent<AppleSlot>();
        Fruit apple = null;
        for (int i = 0; i < _gameplayManager.inventory.allFruits.Count; i++)
        {
            if (_gameplayManager.inventory.allFruits[i].name == info.appleNameString)
            {
                apple = _gameplayManager.inventory.allFruits[i];
                break;
            }
        }

        if (apple != null)
        {
            appleInfoName.text = apple.name;
            appleInfoImage.sprite = apple.sprite;
            appleInfoDescription.text = apple.description;
            appleInfoRarity.text = apple.rarity.ToString();
            
            firstRecipePanel.SetActive(true);
            secondRecipePanel.SetActive(true);

            if (apple.recipeList.Count == 0)
            {
                firstRecipePanel.SetActive(false);
                secondRecipePanel.SetActive(false);
            }
            else
            {
                if (apple.recipeList.Count > 1)
                {
                    appleRecipeSlot1.sprite = apple.recipeList[0].fruit.sprite;
                    appleToolSlot1.sprite = toolDef.tools.Find(propertie => apple.recipeList[0].tool == propertie.tool).sprite;
                    appleRecipeSlot2.sprite = apple.recipeList[1].fruit.sprite;
                    appleToolSlot2.sprite = toolDef.tools.Find(propertie => apple.recipeList[1].tool == propertie.tool).sprite;
                }
                else
                {
                    foreach (Recipe r in apple.recipeList)
                    {
                        appleRecipeSlot1.sprite = r.fruit.sprite;
                        appleToolSlot1.sprite = toolDef.tools.Find(propertie => apple.recipeList[0].tool == propertie.tool).sprite;
                    }

                    secondRecipePanel.SetActive(false);
                }
            }

            
        }
    }
}