using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ItemMng : MonoBehaviour{

    private static ItemMng instance;
    public static ItemMng Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<ItemMng>();
                if (instance == null) {
                    GameObject go = new GameObject();
					instance = go.AddComponent<ItemMng>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
    //TODO : CreateItem ÀÌ¶û PrefabList ¸¸µé±â

    [SerializeField] List<Item> ItemPrefabList;
    [SerializeField] List<Item> ItemList;

    GameObject GetNameToGameObject(string name) {
        for(int i=0; i<ItemPrefabList.Count; i++) {
            if (ItemPrefabList[i].name.Equals(name)) {
                return ItemPrefabList[i].gameObject;
            }
        }
        return null;
    }

    public void CreateItem(Vector2 pos, string name) {
        bool IsFind = false;
        for (int i=0; i<ItemList.Count; i++) {
            if (ItemList[i].gameObject.activeSelf == false && ItemList[i].name.Equals(name)) {
                IsFind = true;
                ItemList[i].Spawn(pos);
                break;
            }
        }
		if (IsFind == false) {
			Item item = Instantiate(GetNameToGameObject(name)).GetComponent<Item>();
			item.transform.parent = transform;
			item.Spawn(pos);
			item.name = name;
			ItemList.Add(item);
		}

	}

}