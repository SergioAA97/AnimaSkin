using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class SkinManager : MonoBehaviour {

    [HideInInspector]
    public List<Skin> skins = new List<Skin>();
    [HideInInspector]
    public Skin defSkin;
    public GameObject skinHolder;

    private void OnEnable()
    {
        RefreshSkins();
        GetDefSkin();
    }

    /// <summary>
    /// Refresh the skins in the gameObject 	
    /// </summary>
    public void RefreshSkins()
    {
        skins = new List<Skin>();

        Skin[] currSkins;
        List<string> skinNames = new List<string>();

        //If reference for holder
        if (skinHolder != null)
        {
             currSkins = skinHolder.GetComponentsInChildren<Skin>();
        }
        else
        {
            currSkins = GetComponents<Skin>();
        }

        //Get reference to the other skins in the character
        
        //Add to list
        if (currSkins.Length == 0)
        {
            Debug.LogWarning("SKIN MANAGER: No skin components found in " + gameObject.name);
        }

        foreach (Skin skin in currSkins)
        {

            bool duplicate = skinNames.Contains(skin.skinName);

            if (duplicate)
            {
                Debug.LogError("SKIN MANAGER: 2 or more skins have the same name!");
                return;
            }
            //Double name check
            skinNames.Add(skin.skinName);

            skins.Add(skin);
        }

    }
    /// <summary>
    /// Saves the current skin as a list
    /// </summary>
    private void GetDefSkin()
    {
        //Init
        defSkin = new Skin();
        defSkin.skinName = "initBase";
        defSkin.skinParts = new List<SkinPart>();

        SpriteMeshInstance[] instances = GetComponentsInChildren<SpriteMeshInstance>();

        foreach (var instance in instances)
        {
            SkinPart newpart = new SkinPart();
            newpart.bodyPart = instance;
            newpart.spriteMesh = instance.spriteMesh;
            defSkin.skinParts.Add(newpart);
        }
    }
    /// <summary>
    /// Restores the initial skin (beginning of the scene) on the character
    /// </summary>
    private void RestoreBaseSkin()
    {
        if (defSkin.skinParts.Count != 0)
        {
            foreach(var part in defSkin.skinParts)
            {
                if (part.spriteMesh != null && part.bodyPart != null)
                {
                    part.bodyPart.spriteMesh = part.spriteMesh;
                }
            }
        }
    }

    /// <summary>
    /// Loads the skin with the name passed to the function.   	
    /// </summary>
    public void LoadSkin(string skinName)
    {
        //Error check
        if (string.IsNullOrEmpty(skinName)) return;

        //Get index of skin in list
        int index = SearchSkin(skinName);

        if (index != -1)
        {

            List<SkinPart> skinParts = skins[index].skinParts;

            //Loop through all parts and assign them
            for (int i = 0; i < skinParts.Count; i++)
            {
                if(skinParts[i].bodyPart != null)
                {
                    //Assign
                    skinParts[i].bodyPart.spriteMesh = skinParts[i].spriteMesh;
                }
            }
        }
    }

    

    /// <summary>
    /// Returns the index of the skin, if the skin is not found, it returns -1.
    /// </summary>
    public int SearchSkin(string skinName)
    {
        //Error check
        if (string.IsNullOrEmpty(skinName)) return -1;

        //Loop the skins
        for (int i = 0; i < skins.Count; i++)
        {
            //Return the index
            if (skins[i].skinName == skinName)
            {
                return i;
            }
        }
        //Return -1 if not found
        return -1;
    }

}
