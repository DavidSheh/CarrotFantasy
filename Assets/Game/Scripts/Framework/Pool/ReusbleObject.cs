using System;
using UnityEngine;
public abstract class ReusbleObject : MonoBehaviour, IReusable
{
    public void OnSpawn()
    {

    }

    public void OnUnspawn()
    {
        throw new NotImplementedException();
    }
}
