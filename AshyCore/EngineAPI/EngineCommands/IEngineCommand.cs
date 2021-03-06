﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AshyCore.EngineAPI.EngineCommands
{
    public enum EngineCommandType
    {
        LoadLevel,          //< from nothing to loading
        DestroyLevel,       //< from level to unloading
        ChangeLevel,        //< from one level to enother (unloading first, then loading)   //todo: delete this

        OpenConsole,
        CloseConsole,

        PauseGame,

        AddEntity,
        DeleteEntity
    }

    public interface IEngineCommand
    {
        EngineCommandType       Type { get; }
    }

    public static class EngineCommandResultPeaker
    {
        public static EngineCommandResult Worst(this EngineCommandResult a, EngineCommandResult b)
        {
            return (a > b ? a : b);
        }
    }
}
