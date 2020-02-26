using System;
using System.ComponentModel;

namespace Wolf_Server_Manager
{
    public class WolfPlayer : IEquatable<WolfPlayer>
    {
        [DisplayName("Player Name")]
        public string PlayerName { get; set; }
        [DisplayName("Score")]
        public int Score { get; set; }
        [DisplayName("Ping")]
        public int Ping { get; set; }
        public int Duplicate;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }

            var Player = other as WolfPlayer;
            if (Player == null)
            {
                return false;
            }

            return Equals(Player);
        }

        public bool Equals(WolfPlayer other)
        {
            if (other == null)
            {
                return false;
            }

            return PlayerName == other.PlayerName && Duplicate == other.Duplicate;
        }
    }
}
