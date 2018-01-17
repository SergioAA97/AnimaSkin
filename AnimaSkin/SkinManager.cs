using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class SkinManager : MonoBehaviour {

    [HideInInspector]
    public List<Skin> skins = new List<Skin>(); //List of skins detected

    public string defatulSkinName = "_default";

    [HideInInspector]
    public Skin defSkin;        //Default character skin created at start of scene
    //[HideInInspector]
    //[SerializeField]
    public Skin currentSkin;    //Current loaded skin

    public string CurrentSkinName
    {
        get { return currentSkin.skinName; }
    }

    public GameObject skinHolder;   //Skin holder GameObject

    private void OnEnable()
    {
        RefreshSkins();
        GetDefSkin();
        SetSkin(defSkin);
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
        }else{
            currSkins = GetComponentsInChildren<Skin>();
        }

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
        defSkin = gameObject.AddComponent<Skin>();
        //Set name
        defSkin.skinName = (string.IsNullOrEmpty(defatulSkinName)) ? defatulSkinName : "_default";
        //Create parts list
        defSkin.skinParts = new List<SkinPart>();

        //Get all instances of Sprite Mesh
        //  -- NOTE :  This can be changed so that it goes to a public variable, in case only certain body parts want to be stored
        SpriteMeshInstance[] instances = GetComponentsInChildren<SpriteMeshInstance>();

        foreach (var instance in instances)
        {
            SkinPart newpart = new SkinPart();
            newpart.bodyPart = instance;
            newpart.spriteMesh = instance.spriteMesh;
            defSkin.skinParts.Add(newpart);
        }

        //Add the default skin to the list
        skins.Add(defSkin);
    }
    /// <summary>
    /// Restores the initial skin (beginning of the scene) on the character
    /// </summary>
    public void RestoreBaseSkin()
    {
        if (defSkin.skinParts.Count != 0)
        {
            /*
            foreach(var part in defSkin.skinParts)
            {
                if (part.spriteMesh != null && part.bodyPart != null)
                {
                    part.bodyPart.spriteMesh = part.spriteMesh;
                }
            }
            */
            SetSkin(defSkin);
        }
    }

    /// <summary>
    /// Loads the skin with the name passed to the function.   	
    /// </summary>
    public void LoadSkinByString(string skinName)
    {     

        //Get index of skin in list
        int index = SearchSkin(skinName);

        if (index != -1)
        {
            //Set the skin
            SetSkin(skins[index]);
        }
        else
        {
            Debug.LogError("ERROR: The skin with name " + skinName + " could not be found in " + gameObject.name);
        }
    }

    /// <summary>
    /// Loads the skin passed to the function.   	
    /// </summary>
    public void LoadSkin(Skin skinToSet)
    {
       
        //Set the skin
        SetSkin(skinToSet);
        
    }

    /// <summary>
    /// Error check the skin	
    /// </summary>
    bool IsValidSkin(Skin skin)
    {
        //Empty name
        if (string.IsNullOrEmpty(skin.skinName))
        {
            Debug.LogError("ERROR: The skin is not valid, the name is empty");
            return false;
        }

        //Empty skin part list
        if (skin.skinParts.Count == 0)
        {
            Debug.LogError("ERROR: The skin is not valid, it has no body parts!");
            return false;
        }

        //Check every part in the skin
        foreach(SkinPart part in skin.skinParts)
        {
            if (part.bodyPart == null)
            {
                Debug.LogError("ERROR: The skin " + skin.skinName + " is not valid, a body part has a null reference");
                return false;
            }
        }

        //No issues found
        return true;
    }

    void SetSkin(Skin skinToSet)
    {
        List<SkinPart> skinParts = skinToSet.skinParts;

        //Error check
        if (!IsValidSkin(skinToSet)) return;

        //Set current skin
        currentSkin = skinToSet;

        //Loop through all parts and assign them
        for (int i = 0; i < skinParts.Count; i++)
        {
            if (skinParts[i].bodyPart != null)
            {
                //Assign
                skinParts[i].bodyPart.spriteMesh = skinParts[i].spriteMesh;
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
