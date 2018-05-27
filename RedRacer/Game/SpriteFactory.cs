using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    
    public Sprite GetSprite(string fileName)
    {
      return SpriteCache[fileName];
    }

    public async Task LoadAndCacheSpriteFromFile(string fileName)
    {
      // Don't overwrite what's in the cache.
      if (!SpriteCache.ContainsKey(fileName))
      {
        // Cache the sprite.
        SpriteCache.Add(fileName, await NewSpriteFromFile(fileName));
      }
    }
    
    private async Task<Sprite> NewSpriteFromFile(string fileName)
    {
      StorageFile sf = await StorageFile.GetFileFromPathAsync(ASSET_PATH + fileName);
      
      using (IRandomAccessStream stream = await sf.OpenAsync(FileAccessMode.Read))
      {
        BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);

        // Scale image to appropriate size 
        BitmapTransform transform = new BitmapTransform()
        {
          ScaledWidth = decoder.PixelWidth,
          ScaledHeight = decoder.PixelHeight
        };
        PixelDataProvider pixelData = await decoder.GetPixelDataAsync(
            BitmapPixelFormat.Bgra8, // WriteableBitmap uses BGRA format 
            BitmapAlphaMode.Straight,
            transform,
            ExifOrientationMode.IgnoreExifOrientation, // This sample ignores Exif orientation 
            ColorManagementMode.DoNotColorManage
        );

        // An array containing the decoded image data, which could be modified before being displayed 
        return new Sprite((int)decoder.PixelWidth, (int)decoder.PixelHeight, pixelData.DetachPixelData());
      }
    }
  }
}
