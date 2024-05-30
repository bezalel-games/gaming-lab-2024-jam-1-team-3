using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
   public static Music _musicInstance;
   private void Awake()
   {
      if (_musicInstance == null)
      {
         DontDestroyOnLoad(this.gameObject);
         _musicInstance = this;
      }
      else
      {
         Destroy(this.gameObject);
      }
   }
}
