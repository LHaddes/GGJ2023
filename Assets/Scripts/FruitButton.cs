using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button), typeof(Image))]
public class FruitButton : MonoBehaviour
{
    public Fruit fruit;
    public UnityAction<Fruit> onSelect;

    public Text innerText;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SelectFruit);
        GetComponent<Image>().sprite = fruit.sprite;

        innerText.text = fruit.appleName;
    }

    void SelectFruit()
    {
        onSelect.Invoke(fruit);
    }
}