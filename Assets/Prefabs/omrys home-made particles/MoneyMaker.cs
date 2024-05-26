using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoneyMaker : MonoBehaviour
{
   public Transform _target;
   public GameObject _prefab;

   private void CreateCoin()
   {
       GameObject go = Instantiate(_prefab, transform.position, quaternion.identity);
       particle coin = go.GetComponent<particle>();

       coin._target = _target;
   }

   public void CreateSplash()
   {
       for (int i = 0; i < 10; i++)
       {
           CreateCoin();
       }
   }

  

}
