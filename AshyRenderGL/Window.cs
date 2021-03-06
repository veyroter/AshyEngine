﻿//   
// Created : 04.07.2016
// Author  : vadik
// Copyright (C) AshyCat 2016
// This product are licensed under MICROSOFT REFERENCE SOURCE LICENSE(MS-RSL).
//  

using System.Drawing;
using System.Runtime.ConstrainedExecution;
using AshyCommon;
using AshyCore;
using AshyCore.Input;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace AshyRenderGL
{
    internal class Window : CriticalFinalizerObject, IWindow
    {
        #region Fields

        private Point               _windowCenter;
      //private float               _mouseSense;

        #endregion


        #region Properties

        private GameWindow          PrivateWindow { get; }

        public event DrawFrameFunc  RenderFrame;
        public event MouseDownFunc  MouseButtonDown;
        public event MouseUpFunc    MouseButtonUp;
        public event MouseMoveFunc  MouseMove;
        public event KeyDownFunc    KeyDown;
        public event KeyUpFunc      KeyUp;
        public event ResizeFunc     Resize;

        public bool                 CaptureMouse { get; set; } 
        public bool[]               PressedKeys { get; }
        public int                  Width       => PrivateWindow.Width;
        public int                  Height      => PrivateWindow.Height;
        public bool                 IsExiting   => PrivateWindow.IsExiting;
        
        #endregion


        #region Constructor and destructor

        internal Window(string title = null, int width = 1920, int height = 1080, bool fullScreen = true)
        {
            if (title == null)
                title                       = "AshyEngine build " + AssemblyUtils.BuildNumber;

            PrivateWindow                   = new GameWindow( width, 
                                                              height, 
                                                              GraphicsMode.Default, 
                                                              title, 
                                                              fullScreen 
                                                                  ? GameWindowFlags.Fullscreen 
                                                                  : GameWindowFlags.Default);

            _windowCenter                   = new Point(PrivateWindow.Width/2, PrivateWindow.Height/2);
            PressedKeys                     = new bool[131];
            KeyDown                         += button   => PressedKeys[(uint) button] = true;
            KeyUp                           += button   => PressedKeys[(uint) button] = false;
            Resize                          += size     => _windowCenter = new Point(size.Width/2, size.Height/2);

            PrivateWindow.Mouse.ButtonDown  += (sender, args) => 
                MouseButtonDown?.Invoke     ( (MouseButton) args.Button, args.Position );

            PrivateWindow.Mouse.ButtonUp    += (sender, args) => 
                MouseButtonUp?.Invoke       ( (MouseButton) args.Button, args.Position );

            //PrivateWindow.RenderFrame       += (sender, args) => 
            //    RenderFrame?.Invoke         ( (float) args.Time );

            PrivateWindow.Mouse.Move        += (sender, args) => 
                MouseMove?.Invoke           ( new Point(args.XDelta, args.YDelta), args.Position );

            PrivateWindow.KeyDown           += (sender, args) => 
                KeyDown?.Invoke             ( (Key) args.Key );

            PrivateWindow.Resize            += (sender, args) => 
                Resize?.Invoke              ( PrivateWindow.ClientSize );

            PrivateWindow.KeyUp             += (sender, args) => 
                KeyUp?.Invoke               ( (Key) args.Key );

            PrivateWindow.VSync             = VSyncMode.Adaptive;
            PrivateWindow.Visible           = true;
        }

        ~Window()
        {
            PrivateWindow?.Close            ();
            PrivateWindow?.Dispose          ();
        }

        #endregion


        #region Methods

        public void ProcessEvents()
        {
            RenderFrame?.Invoke             ( 30f );
            PrivateWindow.ProcessEvents     ();
        }

        public void SwapBuffers()
        {
            if ( ! PrivateWindow.IsExiting)
            {
                PrivateWindow.SwapBuffers   ();
            }
        }

        #endregion
    }
}