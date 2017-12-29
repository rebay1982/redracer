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

    public async Task LoadSpriteFromFile(string fileName)
    {
      // Don't overwrite what's in the cache.
      if (!SpriteCache.ContainsKey(fileName))
      {

        // Prep the output array and load the bitmap data into it.
        byte[] bitmapPixels = new byte[(640 * 480) << 2];
        
        await RetrieveBitmap(fileName, bitmapPixels);

        // Cache the sprite.
        SpriteCache.Add(fileName, new Sprite(640, 480, bitmapPixels));
      }
    }

    private async Task RetrieveBitmap(string fileName, byte[] dst)
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
