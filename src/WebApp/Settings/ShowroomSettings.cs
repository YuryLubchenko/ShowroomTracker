using System;
using DomainModel.Settings;

namespace WebApp.Settings
{
    public class ShowroomSettings: IShowroomSettings
    {
        public string GetCarsAction { get; set; }
        public string GetModelsAction { get; set; }
        public TimeSpan RequestInterval { get; set; }
    }
}