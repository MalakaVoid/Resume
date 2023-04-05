using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burger : MonoBehaviour
{
    [SerializeField] private List<GameObject> BurgerParts;
    public enum PartsOfBurger
    {
        top,
        salad,
        cheese,
        cucumbers,
        tomatoes,
        onions,
        ketchup,
        vetchina,
        bottom
    }
    public void SetActivePart(PartsOfBurger part,bool active)
    {
        BurgerParts[(int)part].SetActive(active);
    }
    public void Build()
    {
        int count = 1;
        for(int i= BurgerParts.Count-2; i>=0;i--)
        {
            if (BurgerParts[i].active==true)
            {
                BurgerParts[i].transform.position = new Vector3(BurgerParts[i].transform.position.x, BurgerParts[BurgerParts.Count - 1].transform.position.y + 0.2f * count, BurgerParts[i].transform.position.z);
                if (BurgerParts[i].TryGetComponent(out SpriteRenderer sr))
                {
                    sr.sortingOrder = count;
                }
                count++;
            }
        }
    }
}
