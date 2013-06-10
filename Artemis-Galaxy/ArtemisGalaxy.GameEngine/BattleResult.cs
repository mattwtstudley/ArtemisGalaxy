using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtemisGalaxy.GameEngine
{
    public class BattleResult
    {
        /// <summary>
        /// The sector that the battle took place in. 
        /// </summary>
        public long embattledSectorId { get; set; } //The sector where the battle happened. 
        /// <summary>
        /// Tracks the destroyed enemy ships in that battle. 
        /// </summary>
        public Dictionary<int,int> DestroyedShips { get; set; } 
        /// <summary>
        /// Tracks the munitions fired from the player ship. 
        /// </summary>
        public Dictionary<int, int> MunitionsFired { get; set; }
        /// <summary>
        /// The player ship that took part in this battle. 
        /// </summary>
        public int PlayerShipId { get; set; }
        /// <summary>
        /// Notes if the Player ship was destroyed during this mission. This will set the player ship's destroyed flag. 
        /// </summary>
        public bool PlayerShipDestroyed { get; set; } //Determines if the player ship was destroyed in battle. 
        /// <summary>
        /// Is true if the sector is claimed - meaning the player ship destroyed all enemy vessels in the sector. 
        /// </summary>
        public bool AllEnemiesDestroyed { get; set; }
        /// <summary>
        /// Is true if all friendly stations in the sector were destroyed. 
        /// </summary>
        public bool AllStationsDestroyed { get; set; }
    }
}
