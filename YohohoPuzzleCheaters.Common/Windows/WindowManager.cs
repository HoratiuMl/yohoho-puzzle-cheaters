﻿using System.Drawing;

using NuciXNA.Primitives;

namespace YohohoPuzzleCheaters.Common.Windows
{
    public class WindowManager
    {
        static volatile WindowManager instance;
        static object syncRoot = new object();

        Bitmap windowBitmap;
        Graphics graphicsHandle;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static WindowManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new WindowManager();
                        }
                    }
                }

                return instance;
            }
        }

        public Point2D WindowLocation { get; set; }

        public Size2D WindowSize { get; set; }

        public ScreenType CurrentScreen { get; set; }

        public void LoadContent()
        {
            windowBitmap = new Bitmap(WindowSize.Width, WindowSize.Height);
            graphicsHandle = Graphics.FromImage(windowBitmap);
        }

        public void UnloadContent()
        {
            graphicsHandle.Dispose();
            windowBitmap.Dispose();
        }

        public void Update(double elapsedTime)
        {
            UpdateWindowBitmap();
            CalculateCurrentScreen();
        }

        public Color GetPixel(int x, int y)
        {
            Color colour = windowBitmap.GetPixel(x, y);

            return colour;
        }

        void UpdateWindowBitmap()
        {
            Point source = new Point(WindowLocation.X, WindowLocation.Y);
            Point destination = new Point(0, 0);

            graphicsHandle.CopyFromScreen(source, destination, windowBitmap.Size, CopyPixelOperation.SourceCopy);
        }

        void CalculateCurrentScreen()
        {
            Color clr000x000 = GetPixel(000, 000);
            Color clr025x025 = GetPixel(025, 025);
            Color clr449x000 = GetPixel(449, 000);

            if (clr000x000.R == 141 && clr000x000.G == 116 && clr000x000.B == 070 &&
                clr025x025.R == 105 && clr025x025.G == 081 && clr025x025.B == 041 &&
                clr449x000.R == 105 && clr449x000.G == 081 && clr449x000.B == 041)
            {
                CurrentScreen = ScreenType.BilgingScreen;
            }
            else if (clr000x000.R == 170 && clr000x000.G == 115 && clr000x000.B == 056 &&
                     clr025x025.R == 122 && clr025x025.G == 080 && clr025x025.B == 040 &&
                     clr449x000.R == 170 && clr449x000.G == 115 && clr449x000.B == 056)
            {
                CurrentScreen = ScreenType.PokerScreen;
            }
            else if (clr000x000.R == 008 && clr000x000.G == 041 && clr000x000.B == 080 &&
                     clr025x025.R == 008 && clr025x025.G == 041 && clr025x025.B == 080 &&
                     clr449x000.R == 033 && clr449x000.G == 082 && clr449x000.B == 115)
            {
                CurrentScreen = ScreenType.PatchingScreen;
            }
            else
            {
                CurrentScreen = ScreenType.UnknownScreen;
            }
        }
    }
}
