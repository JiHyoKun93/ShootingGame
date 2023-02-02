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

//[AddComponentMenu("ComponentCategory/ComponentName")] // Add ComponentMenu to this script
//[ExecuteAlways]
//[RequireComponent(typeof(SpriteRenderer))]
public class ItemLevelUp : MonoBehaviour{
    //[HideInInspector] public int iHideMember;
    
    //[Header("My Fields")] // Description for members
    
    //[Range(0, 1)] // Slider on Inspector
    //[SerializeField]  private int iMember;

    //[TextArea] // TextArea on Inspector (string)
    //[SerializeField] private string strTextArea;
    //[Space(10)] // Space for another variables (10px)

    //[Tooltip("fMember Tooltip!!")] // Tooltip for member in Inspector
    //[SerializeField] private float fMember;

    //[ColorUsage(true), SerializeField] Color cMember; // Color Picker for Color

    //[SerializeField] private string guid = "";

    void Awake(){
        
    }

    void Start(){
        
        //if(this.guid == "") this.guid = System.Guid.NewGuid();
    }

    void Update(){
        
    }

    void FixedUpdate(){
        
    }

#if UNITY_EDITOR
    /*[MenuItem("CustomMenu/CustomMenu01")] // Add Menu to Menu Bar
    private static void CustomMenuExecute(){
        Debug.Log("Menu Execution!!");
    }*/
    
    /*[ContextMenu("ContextMenu")] // Add Context Menu to Script Right Click
    private void ContextMenuExecute(){
        Debug.Log("Menu Execution!!");
    }*/
#endif
}

/*
#if UNITY_EDITOR
[CustomEditor(typeof(ItemLevelUp))]
public class ItemLevelUp_Editor : Editor{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();
        var o = (ItemLevelUp)target;
        if(GUILayout.Button("Do with your button")){
            Debug.Log("Editor Button Clicked!!");
        }
    }
}
#endif
*/


// More Attributes on https://github.com/dbrizov/NaughtyAttributes, check and install it