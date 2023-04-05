using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Director : MonoBehaviour
{
    private IBurgerBuilder burgerBuilder;
    [SerializeField] GameObject burgerBuilderObj;

    private void Start()
    {
        if (burgerBuilderObj.TryGetComponent(out IBurgerBuilder builder))
        {
            burgerBuilder = builder;
        }
    }
    public void CreateBurger()
    {
        burgerBuilder.CreateBurger();
    }
    public void AddCheese()
    {
        burgerBuilder.AddCheese();
    }
    public void AddSalad()
    {
        burgerBuilder.AddSalad();
    }
    public void AddCucumbers()
    {
        burgerBuilder.AddCucumbers();
    }
    public void AddTomatoes()
    {
        burgerBuilder.AddTomatoes();
    }
    public void AddOnions()
    {
        burgerBuilder.AddOnions();
    }
    public void AddKetchup()
    {
        burgerBuilder.AddKetchup();
    }
    public void AddVetchina()
    {
        burgerBuilder.AddVetchina();
    }    
    public void BuildBurger()
    {
        burgerBuilder.BuildBurger();
    }
}
