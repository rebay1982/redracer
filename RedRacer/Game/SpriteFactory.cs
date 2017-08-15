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
    private readonly string ASSET_PATH;

    private static SpriteFactory Instance;
    private Dictionary<string, Sprite> SpriteCache = new Dictionary<string, Sprite>();

    private SpriteFactory()
    {
      ASSET_PATH =
        Windows.ApplicationModel.Package.Current.InstalledLocation.Path
        + "\\Assets\\GameAssets\\";
    }

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

        // Prep the output array and load the bitmap data into it.
        byte[] bitmapPixels = new byte[(640 * 480) << 2];
        
        RetrieveBitmap(fileName, bitmapPixels);

        // Cache the sprite.
        SpriteCache.Add(fileName, new Sprite(640, 480, bitmapPixels));
      }

      return SpriteCache[fileName];
    }

    // TODO: Not sure about this method's performance and the fact that we have to 
    // make everything async......
    private async void RetrieveBitmap(string fileName, byte[] dst)
    {
      StorageFile sf = await StorageFile.GetFileFromPathAsync(ASSET_PATH + fileName);
      
      using (IRandomAccessStream stream = await sf.OpenAsync(FileAccessMode.Read))
      {
        BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

        // Scale image to appropriate size 
        BitmapTransform transform = new BitmapTransform()
        {
          ScaledWidth = 640,
          ScaledHeight = 480
        };
        PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
            BitmapPixelFormat.Bgra8, // WriteableBitmap uses BGRA format 
            BitmapAlphaMode.Straight,
            transform,
            ExifOrientationMode.IgnoreExifOrientation, // This sample ignores Exif orientation 
            ColorManagementMode.DoNotColorManage
        );

        // An array containing the decoded image data, which could be modified before being displayed 
        byte[] sourcePixels = pixelData.DetachPixelData();

        System.Buffer.BlockCopy(sourcePixels, 0, dst, 0, sourcePixels.Length);
      }
    }
  }
}
