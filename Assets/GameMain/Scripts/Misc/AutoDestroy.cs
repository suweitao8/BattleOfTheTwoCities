using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameMain
{
    public class AutoDestroy : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject);
        }
    }
}