using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAssetFrom : MonoBehaviour
{
    GameObject model;
    public string assetBundleName = "testbundle";
    public string objectNameToLoad = "bishopblack";
    public Transform container3D;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadAsset(assetBundleName, objectNameToLoad));

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator LoadAsset(string assetBundleName, string objectNameToLoad)
    {
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
        filePath = System.IO.Path.Combine(filePath, assetBundleName);

        Debug.Log("file path is " + filePath);

        //Load "animals" AssetBundle
        var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
        yield return assetBundleCreateRequest;

        AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

        //Load the "dog" Asset (Use Texture2D since it's a Texture. Use GameObject if prefab)
        AssetBundleRequest asset = asseBundle.LoadAssetAsync<GameObject>(objectNameToLoad);
        yield return asset;

        //Retrieve the object (Use Texture2D since it's a Texture. Use GameObject if prefab)
        GameObject loadedAsset = asset.asset as GameObject;


        //Do something with the loaded loadedAsset  object (Load to RawImage for example) 
        //    image.texture = loadedAsset;
        Instantiate(loadedAsset);
        Debug.Log("loadedAsset is " + loadedAsset.name + loadedAsset.transform);

        model = loadedAsset;

    }
    public void InstantiateObject()
    {
        //  model3D = obj;
        GameObject realWorldItem = GameObject.Instantiate(model);
        Utils.DestroyChildren(container3D);

        realWorldItem.transform.parent = container3D;
    }

    private class Utils
    {
        public static void DestroyChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

    }
}
