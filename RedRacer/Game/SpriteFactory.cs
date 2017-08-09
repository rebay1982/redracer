using System;
using System.Collections.Generic;
using System.IO;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;

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

        byte[] bitmapPixels = new byte[(640 * 480) << 2];


        //await RetrieveBitmap(fileName, bitmapPixels);

        // Load the bitmap.
        this.SpriteCache.Add(fileName, new Sprite(10, 10, new byte[100]));
      }

      return SpriteCache[fileName];
    }

    // TODO: Not sure about this method's performance and the fact that we have to 
    // make everything async......
    private async void RetrieveBitmap(string fileName, byte[] dst)
    {
      //FileStream fStream = File.OpenRead(ASSET_PATH + fileName);

      //StorageFile sf = await StorageFile.GetFileFromPathAsync(ASSET_PATH + fileName);

      //using (IRandomAccessStream stream = await sf.OpenAsync(FileAccessMode.Read))
      //{
      //  BitmapDecoder decoder = await BitmapDecoder.C(stream);
        
      //  // Scale image to appropriate size 
      //  BitmapTransform transform = new BitmapTransform()
      //  {
      //    ScaledWidth = 640,
      //    ScaledHeight = 480
      //  };
      //  PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
      //      BitmapPixelFormat.Bgra8, // WriteableBitmap uses BGRA format 
      //      BitmapAlphaMode.Straight,
      //      transform,
      //      ExifOrientationMode.IgnoreExifOrientation, // This sample ignores Exif orientation 
      //      ColorManagementMode.DoNotColorManage
      //  );

      //  // An array containing the decoded image data, which could be modified before being displayed 
      //  byte[] sourcePixels = pixelData.DetachPixelData();

      //  System.Buffer.BlockCopy(sourcePixels, 0, dst, 0, sourcePixels.Length);
      //}
    }
  }
}
