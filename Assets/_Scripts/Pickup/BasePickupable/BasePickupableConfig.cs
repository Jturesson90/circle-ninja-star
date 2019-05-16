using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePickupableConfig : ScriptableObject
{
    [Header("Standard Settings")]
    [SerializeField] private AudioClip _pickupSfx = null;
    public AudioClip PickupSfx => _pickupSfx;
    [SerializeField] private Material _spriteMaterial = null;
    public Material SpriteMaterial => _spriteMaterial;
    public abstract Pickupable GetPickupable(GameObject objectToAttachTo);

}

