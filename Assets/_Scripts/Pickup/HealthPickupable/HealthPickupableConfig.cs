using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Pickup/Health"))]
public class HealthPickupableConfig : BasePickupableConfig
{
    [Header("Health Settings")]
    [SerializeField] private int _heal = 10;
    public int Heal => _heal;

    public override Pickupable GetPickupable(GameObject objectToAttachTo) => new HealthPickupable(objectToAttachTo, this);

}
