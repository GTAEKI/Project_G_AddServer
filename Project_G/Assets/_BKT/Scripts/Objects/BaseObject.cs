using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BaseObject : InitBase
{
    public Define.EObjectType ObjectType { get; protected set; } = Define.EObjectType.None;
    public Collider Collider { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        ObjectType = Define.EObjectType.Creature;
        Collider = GetComponentInChildren<Collider>();
        Rigidbody = GetComponentInChildren<Rigidbody>();
        Collider.isTrigger = true;
        Rigidbody.isKinematic = false;
        Rigidbody.useGravity = false;

        return true;
    }
}
