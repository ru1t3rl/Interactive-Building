using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class SwitchMaterial : MonoBehaviour
{
    [SerializeField] Material defaultMaterial, otherMaterial;
    MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();

        for(int i = renderer.materials.Length > 1 ? renderer.materials.Length - 1 : renderer.materials.Length;i-- > 0; )
        {
            renderer.materials[i] = defaultMaterial;
        }

        GameManager.Instance.onSwitchScene += SwitchColor;
    }

    public void SwitchColor()
    {        
        for (int i = renderer.materials.Length > 1 ? renderer.materials.Length - 1 : renderer.materials.Length; i-- > 0;)
        {
            renderer.materials[i] = GameManager.Instance.Switched ? otherMaterial : defaultMaterial;
        }
    }
}
