using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerBuilder : MonoBehaviour, IBurgerBuilder
{
    [SerializeField] private GameObject RowBurger;
    private GameObject currentBurger;
    private float burgerPositionX=0;
    public void AddCheese()
    {
        if (currentBurger != null)
        {
            if (currentBurger.TryGetComponent(out Burger br))
            {
                br.SetActivePart(Burger.PartsOfBurger.cheese, true);
            }
        }
    }

    public void AddCucumbers()
    {
        if (currentBurger != null)
        {
            if (currentBurger.TryGetComponent(out Burger br))
            {
                br.SetActivePart(Burger.PartsOfBurger.cucumbers, true);
            }
        }
    }

    public void AddKetchup()
    {
        if (currentBurger != null)
        {
            if (currentBurger.TryGetComponent(out Burger br))
            {
                br.SetActivePart(Burger.PartsOfBurger.ketchup, true);
            }
        }
    }

    public void AddOnions()
    {
        if (currentBurger != null)
        {
            if (currentBurger.TryGetComponent(out Burger br))
            {
                br.SetActivePart(Burger.PartsOfBurger.onions, true);
            }
        }
    }

    public void AddSalad()
    {
        if (currentBurger != null)
        {
            if (currentBurger.TryGetComponent(out Burger br))
            {
                br.SetActivePart(Burger.PartsOfBurger.salad, true);
            }
        }
    }

    public void AddTomatoes()
    {
        if (currentBurger != null)
        {
            if (currentBurger.TryGetComponent(out Burger br))
            {
                br.SetActivePart(Burger.PartsOfBurger.tomatoes, true);
            }
        }
    }

    public void AddVetchina()
    {
        if (currentBurger != null)
        {
            if (currentBurger.TryGetComponent(out Burger br))
            {
                br.SetActivePart(Burger.PartsOfBurger.vetchina, true);
            }
        }
    }

    public void CreateBurger()
    {
        
        currentBurger = Instantiate(RowBurger, new Vector3(burgerPositionX, 0, 0), Quaternion.identity);
        burgerPositionX += 2f;
    }
    public void BuildBurger()
    {
        if (currentBurger != null)
        {
            if (currentBurger.TryGetComponent(out Burger br))
            {
                br.Build();
            }
        }
    }
}
