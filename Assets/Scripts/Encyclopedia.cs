using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encyclopedia : MonoBehaviour
{
    public GameObject uiPanel;
    public static Encyclopedia Instance;
    private GameplayManager _gameplayManager;

    public Dictionary<Fruit, bool> discoveredFruits;

    void Awake()
    {
        Instance = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _gameplayManager = GameplayManager.Instance;
        discoveredFruits = new Dictionary<Fruit, bool>();
        foreach (Fruit f in _gameplayManager.allFruits)
        {
            discoveredFruits.Add(f, f.isUnlocked);
        }
        
        Debug.Log(discoveredFruits);
        UpdateEncyclopedia();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateEncyclopedia()
    {
        
        
    }
}
