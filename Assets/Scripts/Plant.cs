using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DigitalRuby.Tween;


public class Plant : MonoBehaviour
{
    private bool clickable;

    public UnityEvent OnClick = new UnityEvent();
    public UnityEvent OnResetAnimationEnd = new UnityEvent();
    public UnityEvent<bool> OnMouseHover = new UnityEvent<bool>();

    public GameInventory inventory;

    public MeshRenderer pot;
    public Material potDefaultMaterial;
    public Material potHighlightMaterial;

    public MeshRenderer bud;
    public MeshRenderer tree;
    public MeshRenderer ashes;
    public ParticleSystem smoke;

    public Transform fruitsGrowth;

    void Start()
    {
        bud.gameObject.SetActive(true);
        tree.gameObject.SetActive(false);
        ashes.gameObject.SetActive(false);
        fruitsGrowth.gameObject.SetActive(false);
        smoke.Stop();

        inventory.onFruitObtained += GrowFruits;
    }

    public void Evolve(bool success)
    {
        SetClickable(false);

        // TODO Cloud of smokes
        bud.gameObject.SetActive(false);
        if (success)
        {
            tree.gameObject.SetActive(true);
            fruitsGrowth.gameObject.SetActive(true);
        }
        else
        {
            ashes.gameObject.SetActive(true);
        }

        StartCoroutine(ActivateSmoke());
    }

    IEnumerator ActivateSmoke()
    {
        smoke.Play();
        yield return new WaitForSeconds(0.1f);
        smoke.Stop();
    }

    public void GrowFruits(Fruit fruit)
    {
        Transform position = fruitsGrowth.GetChild(Random.Range(0, fruitsGrowth.childCount));
        Instantiate(fruit.mesh, position);
    }

    void OnMouseEnter()
    {
        if (clickable)
        {
            pot.material = potHighlightMaterial;
            OnMouseHover.Invoke(true);
        }
    }

    void OnMouseExit()
    {
        if (clickable)
        {
            pot.material = potDefaultMaterial;
            OnMouseHover.Invoke(false);
        }
    }

    void OnMouseDown()
    {
        if (clickable)
        {
            OnClick.Invoke();
            OnMouseHover.Invoke(false);
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
        gameObject.Tween("MoveUp", currentPos, upPos, MOVEMENT_DURATION, TweenScaleFunctions.SineEaseIn,
            (p) => { transform.position = p.CurrentValue; });
        yield return new WaitForSeconds(MOVEMENT_DURATION);

        // Change plant
        tree.gameObject.SetActive(false);
        ashes.gameObject.SetActive(false);
        bud.gameObject.SetActive(true);
        // Clear fruits
        foreach (Transform pos in fruitsGrowth)
        {
            foreach (Transform child in pos)
            {
                Destroy(child.gameObject);
            }
        }

        fruitsGrowth.gameObject.SetActive(false);

        // Move down
        gameObject.Tween("MoveDown", upPos, currentPos, MOVEMENT_DURATION, TweenScaleFunctions.SineEaseOut,
            (p) => { transform.position = p.CurrentValue; });
        yield return new WaitForSeconds(MOVEMENT_DURATION);

        // Done!
        OnResetAnimationEnd.Invoke();
    }
}