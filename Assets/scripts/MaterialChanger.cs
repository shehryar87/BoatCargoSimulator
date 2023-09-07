using System.Collections;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material newMaterial; // Assign the new material from the Project panel in the Inspector

    private void Start()
    {

        StartCoroutine (ChangeMaterial());
     //   ChangeMaterial();
    }
   
    IEnumerator  ChangeMaterial()
    {

        yield return new WaitForSeconds(0.08f);
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null && newMaterial != null)
        {
            Material[] newMaterials = new Material[meshRenderer.sharedMaterials.Length];
            for (int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = newMaterial;
            }

            meshRenderer.sharedMaterials = newMaterials;
        }
        else
        {
            Debug.LogWarning("MeshRenderer or newMaterial is missing.");
        }
    }
}
