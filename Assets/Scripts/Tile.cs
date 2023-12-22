using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public void Initalize(string name)
    {
        gameObject.name = name;
    }
    public void Remove()
    {
        Destroy(gameObject);
        //Transform enemy = transform.Find("Enemy");
        //if (enemy == null)
        //    Destroy(gameObject);
        //else
        //{
        //    enemy.SetSiblingIndex(enemy.GetSiblingIndex() + 1);
        //    Destroy(gameObject);
        //}
    }
}