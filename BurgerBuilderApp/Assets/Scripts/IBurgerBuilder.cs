using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBurgerBuilder
{
    void AddCheese();
    void AddSalad();
    void AddCucumbers();
    void AddTomatoes();
    void AddOnions();
    void AddKetchup();
    void AddVetchina();
    void CreateBurger();
    void BuildBurger();
}
