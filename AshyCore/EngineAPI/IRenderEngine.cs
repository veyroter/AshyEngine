﻿// 
// Created : 10.03.2016
// Author  : Veyroter
// Copyright (C) AshyCat 2016
// This product are licensed under MICROSOFT REFERENCE SOURCE LICENSE(MS-RSL).
// 

using System;
using AshyCore.EntitySystem;

namespace AshyCore.EngineAPI
{
    /// <summary>
    /// The interface provides methods for working with the renderer.
    /// </summary>
    public interface IRenderEngine : IEngine
    {
        IWindow GameWindow { get; }
    }
}