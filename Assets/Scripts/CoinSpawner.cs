using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    [SerializeField] private int count;
    List<Transform> coinList;
    // Start is called before the first frame update
    void Start()
    {
        coinList = new List<Transform>();
        for (int i = 0; i < count; ++i)
        {
            Transform c = Instantiate(coin.transform);

            c.parent = transform;
            c.localPosition = new Vector3(i, i, i);

            coinList.Add(c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
