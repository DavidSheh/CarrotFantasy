﻿using System;
using UnityEngine;
public abstract class ReusbleObject : MonoBehaviour, IReusable
{
    public abstract void OnSpawn();

    public abstract void OnUnspawn();
}

