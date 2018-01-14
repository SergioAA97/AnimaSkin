# AnimaSkin

If you have been having issues with skin management in Anima2D, you have found the right asset. As of today, I have not found a good solution to creating and loading skins for Anima characters on the fly, therefore, I decided to create one myself.

### How do I use it?

Anima Skin has 2 simple scripts to use:
- __SkinManager:__ Attach this script to the root GameObject of your character.

![alt text](https://github.com/SergioAA97/AnimaSkin/blob/master/WikiCapture/SkinManager.PNG?raw=true "The amazing Skin Manager")
- __Skin:__ Attach this script to an empty GameObject as a child of your character.

![alt text](https://github.com/SergioAA97/AnimaSkin/blob/master/WikiCapture/SkinCapture.PNG?raw=true "Skin away!")

## The Skin Manager

The skin manager can be added to the root GameObject of the character. The manager has three visible variables:

- __Current Skin:__ A copy of the currently set *Skin*.
- __Skin Holder:__ Drag the *GameObject* that contains the "Skin" scripts. If you don't have / want one, leave it empty and the manager will try to find them.
- __Skins:__ Is a list that will show the *Skins* identified by the manager in the object that you dragged above.
#### The Variables

The following variables are public in the *Skin Manager* script instance:

- __skins__: The list of the current *Skins*.
- __currentSkin:__ A copy of the currently set *Skin*.
- __defSkin__: The default *Skin* the character had when the scene started.
- __skinHolder__: The *GameObject* that contains the skins ( this is the field in the Editor we saw earlier ).
#### The Functions

The *Skin Manager* has the following __public__ functions (At the moment):
- __RefreshSkins()__ : Refreshes the skins in the *GameObject*.
- __RestoreBaseSkin()__: Restores the character to the skin that it had when the scene started.
- __LoadSkinByString(*string* skinName)__: Loads the skin with the name passed as a parameter. It will let you know if it couldn't find it. For better debugging, check the *Skins* list in the manager at runtime!
- __LoadSkin(*Skin* skinToSet)__: Loads the skin passed as a parameter. You can use this to create *Skins* at runetime and set them to the character. Wow!
- __SearchSkin(*string* skinName)__: Searches the skin, with the name passed as a parameter, and returns the index of it in the *skins* list in the *Skin Manager*, it will return " -1 " if it can't find it.

## The Skin Component

The skin component will be used to create the skins for the character. It has the following visible fields in the Editor:

- __Skin Name__: The name of the skin as a string, it will be used to load them with the *Skin Manager*. Do NOT REPEAT NAMES, or it won't work.
- __Skin Parts__: A list with references to the *Sprite Mesh Instances* of the character (or other object). The *SpriteMesh* field will determine what *Sprite Mesh* is loaded to the body part determined by the instance on the left. Pretty easy right?

### Adding References to a Skin

You can add multiple *Sprite Mesh Instance* references to the *Skin Manager* by selecting the parent object that contains the *Skins* in the editor, then press the "+" button in the list and click on "From Selection". This will add references to all the *Sprite Mesh Instances* found as children of the object selected. What a great feature!
