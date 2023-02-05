using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DigitalRuby.Tween;


public class Plant : MonoBehaviour
{
  private bool clickable = false;

  public UnityEvent OnClick = new UnityEvent();
  public UnityEvent OnResetAnimationEnd = new UnityEvent();

  public MeshRenderer pot;
  public Material potDefaultMaterial;
  public Material potHighlightMaterial;

  public MeshRenderer bud;
  public MeshRenderer tree;
  public MeshRenderer ashes;

  void Start()
  {
    bud.gameObject.SetActive(true);
    tree.gameObject.SetActive(false);
    ashes.gameObject.SetActive(false);
  }

  public void Evolve(bool success)
  {
    SetClickable(false);

    // TODO Cloud of smokes
    bud.gameObject.SetActive(false);
    if (success)
    {
      tree.gameObject.SetActive(true);
    }
    else
    {
      ashes.gameObject.SetActive(true);
    }
  }

  void OnMouseEnter()
  {
    if (clickable)
    {
      pot.material = potHighlightMaterial;
    }
  }

  void OnMouseExit()
  {
    pot.material = potDefaultMaterial;
  }

  void OnMouseDown()
  {
    if (clickable)
    {
      OnClick.Invoke();
    }
  }

  public void SetClickable(bool value)
  {
    clickable = value;
    pot.material = potDefaultMaterial;
  }

  public void ResetPot()
  {
    StartCoroutine(ResetPotAnimation());
  }

  IEnumerator ResetPotAnimation()
  {
    float MOVEMENT_DURATION = 0.9f;

    // Move up
    Vector3 currentPos = transform.position;
    Vector3 upPos = new Vector3(currentPos.x, currentPos.y + 4f, currentPos.z);
    gameObject.Tween("MoveUp", currentPos, upPos, MOVEMENT_DURATION, TweenScaleFunctions.SineEaseIn, (p) =>
    {
      transform.position = p.CurrentValue;
    });
    yield return new WaitForSeconds(MOVEMENT_DURATION);

    // Change plant
    tree.gameObject.SetActive(false);
    ashes.gameObject.SetActive(false);
    bud.gameObject.SetActive(true);

    // Move down
    gameObject.Tween("MoveDown", upPos, currentPos, MOVEMENT_DURATION, TweenScaleFunctions.SineEaseOut, (p) =>
    {
      transform.position = p.CurrentValue;
    });
    yield return new WaitForSeconds(MOVEMENT_DURATION);

    // Done!
    OnResetAnimationEnd.Invoke();
  }
}
