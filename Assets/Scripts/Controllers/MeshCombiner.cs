using System.Collections.Generic;
using UnityEngine;

public class MeshCombiner : MonoBehaviour
{
    [Header("Tags to Combine")]
    public string wallsTag = "Wall";
    public string floorsTag = "Floor";

    [Header("Parents")]
    public Transform wallsParent;
    public Transform floorsParent;

    void Start()
    {
        CombineByTag(wallsTag, "CombinedWalls", wallsParent);
        CombineByTag(floorsTag, "CombinedFloors", floorsParent);
    }

    void CombineByTag(string tagToCombine, string newName, Transform parent)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tagToCombine);
        List<CombineInstance> combine = new List<CombineInstance>();

        foreach (GameObject obj in objects)
        {
            MeshFilter mf = obj.GetComponent<MeshFilter>();
            if (mf == null) continue;

            CombineInstance ci = new CombineInstance();
            ci.mesh = mf.sharedMesh;
            ci.transform = obj.transform.localToWorldMatrix;
            combine.Add(ci);

            MeshRenderer mr = obj.GetComponent<MeshRenderer>();
            if (mr != null) mr.enabled = false;
        }
        if (combine.Count == 0) return;

        GameObject combinedObj = new GameObject(newName);
        combinedObj.transform.parent = parent;

        MeshFilter combinedMF = combinedObj.AddComponent<MeshFilter>();
        MeshRenderer combinedMR = combinedObj.AddComponent<MeshRenderer>();

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combine.ToArray());
        combinedMF.mesh = combinedMesh;

        // Material temporal
        combinedMR.material = new Material(Shader.Find("Standard"));
    }
}
