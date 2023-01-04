using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalSetter : MonoBehaviour
{
    [SerializeField] private GameObject decalPrefab;

    private Decal decal;
    enum DecalType
    {
        Metal,
        Concrete,
        Wood,
        Flesh,
        Dirt
    }
    [SerializeField] private DecalType decalType;

    void Start()
    {
        decal = decalPrefab.GetComponentInChildren<Decal>();
    }

    void Update()
    {
        setDecal();
    }

    void setDecal()
    {
        switch (decalType)
        {
            case DecalType.Metal:
                decal.setDecalMaterial(decal.MetalDecal);
                break;
            case DecalType.Concrete:
                decal.setDecalMaterial(decal.ConcreteDecal);
                break;
            case DecalType.Wood:
                decal.setDecalMaterial(decal.WoodDecal);
                break;
            case DecalType.Flesh:
                decal.setDecalMaterial(decal.FleshDecal);
                break;
        }
    }
}
