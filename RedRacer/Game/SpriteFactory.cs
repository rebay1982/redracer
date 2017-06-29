using System;
using System.Collections.Generic;
using System.IO;
using Windows.Graphics.Imaging;
using Windows.Storage;

namespace RedRacer.Game
{
  class SpriteFactory
  {
    private const string ASSET_PATH = ".\\Assets\\GameAssets\\";

    private static SpriteFactory Instance;
    private Dictionary<string, Sprite> SpriteCache = new Dictionary<string, Sprite>();

    // Singleton.
    public static SpriteFactory GetInstance()
    {
      if (Instance == null)
      {
        Instance = new SpriteFactory();
      }

      return Instance;
    }
    
    public Sprite GetSpriteFromFile(string fileName)
    {
      // If we don't already have the sprite in cache, load it.
      if (!SpriteCache.ContainsKey(fileName))
      {
        //try
        //{
        //
        //}
        //catch (Exception e)
        //{
        //
        //}
        
        // Load the bitmap.
        this.SpriteCache.Add(fileName, new Sprite(10, 10, new byte[100]));
      }

      return SpriteCache[fileName];
    }


    //private Bitmap RetrieveBitmap(string fileName)
    //{
    //  FileStream fStream = File.OpenRead(ASSET_PATH + fileName);
    //
    //  using (StorageFile sf = await StorageFile.GetFileFromPathAsync(ASSET_PATH + fileName))
    //  {
    //
    //  }
    //
    //    Windows.Storage.StorageFile sf = new Windows.Storage.StorageFile();
    //
    //
    //  BitmapDecoder.CreateAsync(fStream);
    //
    //
    //  return null;
    //}
  }
}
