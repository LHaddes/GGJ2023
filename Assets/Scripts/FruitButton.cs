using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Button))]
public class FruitButton : MonoBehaviour
{
  public Fruit fruit;
  public UnityAction<Fruit> onSelect;

  public Text innerText;

  void Start()
  {
    GetComponent<Button>().onClick.AddListener(SelectFruit);
    innerText.text = fruit.name;
  }

  void SelectFruit()
  {
    onSelect.Invoke(fruit);
  }
}
