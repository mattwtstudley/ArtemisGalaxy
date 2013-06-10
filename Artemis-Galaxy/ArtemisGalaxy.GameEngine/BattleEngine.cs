using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtemisGalaxy.GameEngine
{
    public class BattleEngine
    {
        public ArtemisGalaxyDevEntities _context;
        #region "Constructors / Initialization"
        public BattleEngine(ArtemisGalaxyDevEntities context)
        {
            _context = context;
        }

        public void processBattleReport(BattleResult result)
        {
            var sectorEmbattled = _context.Sectors.Where(s => s.id == result.embattledSectorId).SingleOrDefault();

            //Walk through the various mission end conditions. 
            if (result.AllEnemiesDestroyed)
            {
                //All enemies destroyed. Set the sector as claimed (it doesn't matter if the sector was already claimed or not.)
                sectorEmbattled.isClaimed = true;

                //Fetch all the enemy ships that were in this sector. And since they are dead...
                var enemyShips = sectorEmbattled.EnemyShipSectorAssignments.ToList();

                //...zero out the enemy ship counts. 
                foreach (var enemyShip in enemyShips)
                {
                    enemyShip.currentAmount = 0;
                }
            }
            else if (result.AllStationsDestroyed)
            {
                //The player lost all stations in the sector.

                //If the sector was claimed, then it was lost to the enemies and is not claimed anymore. 
                if (sectorEmbattled.isClaimed == true) sectorEmbattled.isClaimed = false;

                //Process the casualties of the ship. 
                processEnemyShipCasulaties(result);

                //Zero out the friendly production numbers since the sector was LOST!
                var munitions = sectorEmbattled.MunitionsSectorAssignments.ToList();

                foreach (var munition in munitions)
                {
                    munition.currentAmount = 0;
                }
            }
            else
            {
                //If we get here, the player ship was destroyed. This doesn't mean the sector was lost (Other bridge crews may have won the day), but for this bridge crew, it's over!
                var shipEnrollement = _context.CampaignEnrollments.Where(e => e.campaignId == sectorEmbattled.campaignId && e.shipId == result.PlayerShipId).Single();
                shipEnrollement.isDestroyed = true;

                //Process Enemy Ship Casulaties. 
                processEnemyShipCasulaties(result);

                processMunitionUsage(result);
            }
        }

        /// <summary>
        /// Processes the munition from this battle. 
        /// </summary>
        /// <param name="result"></param>
        private void processMunitionUsage(BattleResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Processes the enemy ship casualities.
        /// </summary>
        /// <param name="result"></param>
        private void processEnemyShipCasulaties(BattleResult result)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
