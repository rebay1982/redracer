using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
  }
}
