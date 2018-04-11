using PlayerInterface.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerInterface.Interfaces
{
    public interface IPlayer
    {
        void Initialize(Action<PlayerState> StateChangeCallback);
        /// <exception cref="Exceptions.InvalidUrlException"></exception>
        void SetUrl(string url);
        /// <exception cref="Exceptions.InvalidUrlException"></exception>
        /// <exception cref="Exceptions.PlayerStartFailedException"></exception>
        void Start();
        /// <exception cref="Exceptions.PlayerStopFailedException"></exception>
        void Stop();
        /// <exception cref="Exceptions.VolumeOutOfRagenException"></exception>
        void SetVolumeFrom0to100(int volume);
        int GetVolumeFrom0to100();
        string Name { get; }
        Guid Identifier { get; }
    }
}
