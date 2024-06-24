using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DebugTexturepack : MonoBehaviour
{
    public string Folder;
    public Wheel wheel;

    //public static string AssetsFolder = "C:\\Users\\bment\\Documents\\My Games\\CircleOfRiches\\TexturePacks";

    void Start()
    {
        Dictionary<string, Sprite> texturePack = new Dictionary<string, Sprite>();
        foreach (var texture in Resources.LoadAll<Sprite>(Folder))
        {
            texturePack.Add(texture.name, texture);
        }
        wheel.SetTexturePack(texturePack);
        //    foreach(var dir in Directory.GetDirectories(AssetsFolder))
        //    {
        //        Debug.Log(dir);
        //        foreach(var file in Directory.GetFiles(dir))
        //        {
        //            Debug.Log(file);
        //            Sprite sprite = LoadNewSprite(file);
        //            texturePack.Add(sprite.name, sprite);
        //        }
        //    }
    }

    //}

    //public Sprite LoadNewSprite(string FilePath)
    //{

    //    // Load a PNG or JPG image from disk to a Texture2D, assign this texture to a new sprite and return its reference

    //    Sprite NewSprite;
    //    Texture2D SpriteTexture = LoadTexture(FilePath);
    //    NewSprite = Sprite.Create(SpriteTexture, new Rect(0, 0, SpriteTexture.width, SpriteTexture.height), new Vector2(0.5f, 1.0f), SpriteTexture.height);
    //    NewSprite.name = Path.GetFileNameWithoutExtension(FilePath);

    //    return NewSprite;
    //}

    //public Texture2D LoadTexture(string FilePath)
    //{

    //    // Load a PNG or JPG file from disk to a Texture2D
    //    // Returns null if load fails

    //    Texture2D Tex2D;
    //    byte[] FileData;

    //    if (File.Exists(FilePath))
    //    {
    //        FileData = File.ReadAllBytes(FilePath);
    //        Tex2D = new Texture2D(2, 2);           // Create new "empty" texture
    //        if (Tex2D.LoadImage(FileData))           // Load the imagedata into the texture (size is set automatically)
    //            return Tex2D;                 // If data = readable -> return texture
    //    }
    //    return null;                     // Return null if load failed
    //}
}
