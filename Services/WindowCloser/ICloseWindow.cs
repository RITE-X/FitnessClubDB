using System;

namespace FitnessClubDB.Services.WindowCloser;

public interface ICloseWindow
{
    Action? Close { get; set; }

    bool CanClose();

}