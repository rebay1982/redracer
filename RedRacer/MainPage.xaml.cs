﻿using Microsoft.Graphics.Canvas;
using RedRacer.Game;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RedRacer
{
  /// <summary>
  /// An empty page that can be used on its own or navigated to within a Frame.
  /// </summary>
  public sealed partial class MainPage : Page
  {
    // Assortment of managers.
    private readonly IGameMngr gameMngr;
    private readonly IInputMngr inputMngr;
    private readonly IRenderMngr renderMngr;
    private readonly IDiagnosticRenderMngr diagRenderMngr;
    private readonly SpriteFactory spriteFactory;


    private bool Quit = false;
    private long LastFrameRenderTime = 0L;

    public MainPage()
    {
      this.InitializeComponent();

      // Initialize managers.
      gameMngr = new RedRacerGame();

      var rMngr = new RedRacerRenderMngr();
      renderMngr = rMngr;
      diagRenderMngr = rMngr;
      spriteFactory = SpriteFactory.GetInstance();

      
      RunTest();
      //Run();
    }
    
    public async void RunTest()
    {
      while (!Quit)
      {
        LastFrameRenderTime = diagRenderMngr.TimedRender(null);
        await Task.Delay(TimeSpan.FromMilliseconds(15));
      }
    }

    /// <summary>
    // Using game loop inspired from
    // http://gafferongames.com/game-physics/fix-your-timestep/
    // http://gameprogrammingpatterns.com/game-loop.html
    /// </summary>
    public void Run()
    {
      const double timeStep = 10;   // MS

      double currentTime = Utils.GetCurrentTimeStamp();
      double timeAccumulator = 0;   // MS

      // Initial gamestate.
      IGameState previousState = gameMngr.GetGameState();

      while (!Quit)
      {
        double newTime = Utils.GetCurrentTimeStamp();
        double frameTime = newTime - currentTime;

        // Clamp frameTime to 250ms.
        if (frameTime > 250)
          frameTime = 250;

        currentTime = newTime;
        timeAccumulator += frameTime;

        // Update user input state.
        inputMngr.Update();

        while(timeAccumulator >= timeStep)
        {
          // I've got a feeling this extrapolation of states is overkill for the job at hand.
          previousState = gameMngr.GetGameState();
          gameMngr.Update(inputMngr.GetInputState(), timeStep);

          timeAccumulator -= timeStep;
        }

        double alpha = timeAccumulator / timeStep;
        IGameState extrapolatedState = gameMngr.ExtrapolateGameState(
          inputMngr.GetInputState(),
          previousState,
          alpha);

        // Render the picture
        renderMngr.Render(extrapolatedState);
      }
    }

    private void CanvasAnimatedControl_Draw(Microsoft.Graphics.Canvas.UI.Xaml.ICanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedDrawEventArgs args)
    {
      Stopwatch rendersw = Stopwatch.StartNew();
      byte[] imgBytes = renderMngr.GetFrameBuffer();
      rendersw.Stop();

      CanvasBitmap bitmap = CanvasBitmap.CreateFromBytes(
        sender.Device,
        imgBytes,
        640,
        480,
        Windows.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized);

      args.DrawingSession.DrawImage(bitmap, 0, 0);

      args.DrawingSession.DrawText("GetFrameBuffer time: " + rendersw.ElapsedMilliseconds.ToString(), 0, 0, Colors.White);
      args.DrawingSession.DrawText("LastFrameRenderTime: " + LastFrameRenderTime.ToString(), 0, 20, Colors.White);
    }

    private void CanvasAnimatedControl_CreateResources(Microsoft.Graphics.Canvas.UI.Xaml.CanvasAnimatedControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args)
    {
      // Track async method while loading sprites here.
      args.TrackAsyncAction(LoadResourcesAsync(sender).AsAsyncAction());
    }

    private async Task LoadResourcesAsync(Microsoft.Graphics.Canvas.ICanvasResourceCreator sender)
    {
      // Wait to load every needed sprite or else renderers break in their init (they look for the file in memory).
      await spriteFactory.LoadAndCacheSpriteFromFile("RedRacer.png");
      await spriteFactory.LoadAndCacheSpriteFromFile("RoadWide.png");
      await spriteFactory.LoadAndCacheSpriteFromFile("RoadWideDark.png");

      // TODO: Sloppy logic, fix this -- see above comment.
      initRenderers();
    }

    private void initRenderers()
    {
      renderMngr.RegisterRenderer(new RedRacerRoadRenderer());
    }
  }
}
