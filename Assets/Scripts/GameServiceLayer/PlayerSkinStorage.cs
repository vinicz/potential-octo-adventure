using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class PlayerSkinStorage : MonoBehaviour {

    [Serializable]
    public class PlayerSkinStruct
    {
        public Mesh mesh;
        public Material material;
    }

    public GameObject playerSckinPrototype;
    public List<PlayerSkinStruct> playerSkinAssets;

    private List<GameObject> playerSkins = new List<GameObject>();

    void Start()
    {
        foreach (PlayerSkinStruct playerSkinAsset in playerSkinAssets)
        {
            GameObject newPLayerSkin = (GameObject) Instantiate(playerSckinPrototype);
            newPLayerSkin.renderer.material = playerSkinAsset.material;
            newPLayerSkin.GetComponent<MeshFilter>().mesh = playerSkinAsset.mesh;
        }

    }

}
