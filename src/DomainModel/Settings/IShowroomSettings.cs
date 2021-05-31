using System;

namespace DomainModel.Settings
{
    public interface IShowroomSettings
    {
        string GetCarsAction { get; }

        string GetModelsAction { get; }

        TimeSpan RequestInterval { get; }
    }
}