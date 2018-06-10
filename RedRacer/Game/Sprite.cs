using System;
using RedRacer.Game.Exception;

namespace RedRacer.Game
{
  /// <summary>
  /// 
  /// Currently only supports ARGB 32bit format.
  /// 
  /// </summary>
  class Sprite
  {
    public int Width { get; private set; }
    public int Height { get; private set; }
    public byte[] SpriteData { get; private set; }

    public Sprite(int width, int height, byte[] spriteData)
    {
      int dataSize = (width * height) << 2;
      Width = width;
      Height = height;
      SpriteData = new byte[dataSize];

      Buffer.BlockCopy(spriteData, 0, SpriteData, 0, dataSize);
    }

    /// <summary>
    /// 
    /// Returns a cropped portion of the sprite data.
    ///  
    /// </summary>
    /// <param name="posX">Horizontal position where to start cropping</param>
    /// <param name="posY">Vertical position where to start cropping</param>
    /// <param name="width">Width to crop</param>
    /// <param name="height">Height to crop</param>
    /// 
    /// <remarks>
    /// Method does not modify sprite data.
    /// A block copy is done to prepare cropped sprite data.
    /// </remarks>
    /// <exception cref="UnsupportedSpriteCropException">Crop size exceeds sprite data size</exception>
    /// <returns>Cropped sprite data</returns>
    public byte[] GetCroppedSpriteData(int posX, int posY, int width, int height)
    {
      // Validation
      if (posX > this.Width)
        throw new UnsupportedSpriteCropException();

      if (posY > this.Height)
        throw new UnsupportedSpriteCropException();

      if (width > this.Width)
        throw new UnsupportedSpriteCropException();

      if (height > this.Height)
        throw new UnsupportedSpriteCropException();

      int croppedDataSize = (height * width) << 2;
      byte[] croppedBytes = new byte[croppedDataSize];

      int spriteDataRowSize = this.Width << 2;
      int cropPosition = (posX << 2) + (posY * spriteDataRowSize);

      for (int i = 0; i < croppedDataSize; i += (width << 2))
      {
        Buffer.BlockCopy(
          this.SpriteData,
          cropPosition,
          croppedBytes,
          i,
          width << 2);

        cropPosition += spriteDataRowSize;
      }
      
      return croppedBytes;
    }
  }
}
