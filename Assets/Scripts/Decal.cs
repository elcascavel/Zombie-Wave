using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decal : MonoBehaviour
{

    [SerializeField] private Material metalDecal;
    [SerializeField] private Material concreteDecal;
    [SerializeField] private Material woodDecal;
    [SerializeField] private Material fleshDecal;

    public Material MetalDecal { get => metalDecal; set => metalDecal = value; }
    public Material ConcreteDecal { get => concreteDecal; set => concreteDecal = value; }
    public Material WoodDecal { get => woodDecal; set => woodDecal = value; }
    public Material FleshDecal { get => fleshDecal; set => fleshDecal = value; }

    public void setDecalMaterial(Material material)
    {
        GetComponent<ParticleSystemRenderer>().sharedMaterial = material;
    }
}
