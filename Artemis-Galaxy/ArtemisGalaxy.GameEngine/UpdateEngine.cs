using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtemisGalaxy.GameEngine
{
    /// <summary>
    /// Class that contains the update engine for Artemis Galaxy. Purpose of this is to iterate through all the sectors of a given campaign. 
    /// </summary>
    public class UpdateEngine
    {
        //Private member variables. 
        private TimeSpan _updateInterval;
        private Campaign _campaign;
        private ArtemisGalaxyDevEntities _dataContext;

        #region "Constructors / Setup"

        //Constructor
        public UpdateEngine(Campaign campaignToUpdate, ArtemisGalaxyDevEntities dataContext)
        {
            _campaign = campaignToUpdate;
            _dataContext = dataContext;
        }

        //Reads the config, and beings the update process for sectors. 
        private void initialize()
        {
            //We'll start with weekly updates for now. Eventually a config file will be here that will be read in, letting us adjust how this all works. -MWS.
            //                                                      ^Or it's a column in the campaign table. I haven't decided yet. 
            _updateInterval = new TimeSpan(7, 0, 0, 0);     
        }

        #endregion

        #region "Sector Processing"
        public void updateSectors()
        {
            //Read all sectors. 
            var sectorsToUpdate = _dataContext.Sectors.Where(s => s.campaignId == _campaign.id).OrderBy(s => s.locationX).OrderBy(s => s.locationY).ToList();

            foreach (var sector in sectorsToUpdate)
            {
                //Claimed sectors act very differently than unclaimed sectors, so lets separate them. 
                if (sector.isClaimed)
                {
                    processClaimedSector(sector);
                }
                else
                {
                    processUnClaimedSector(sector);
                }
            }
            
        }
        
        /// <summary>
        /// Processes all the changes required for an enemy sector, populating enemy ships.
        /// </summary>
        /// <param name="sector">The sector that is to be updated.</param>
        private void processUnClaimedSector(Sector sector)
        {
            //Things we need to do here:
            //  Each sector has a production number. We'll need to look up the production of each sector, and then produce that many ships based on some sort of logic. 
            //  Thinking that the sector difficulty rating adjusts the weight on the ship types that we should produce in this sector. 
            //  Also, each sector has certain ships it should produce and not produce, and how many counts as "full" for that sector. 
            //  Sectors should have a full and max amount for each ship. This allows us to control the maximum number of ships that "backfill" into other sectors. 

            //Get the list of ships we should produce for this sector. We only pick the ones that still need to be produced. Maxed out ship/fleet types do not get picked. 
            var shipsToProduce = sector.EnemyShipSectorAssignments.Where(s => s.currentAmount < s.maxAmount).ToList();

            //Get the total weight for production in this sector. This lets us calculate the % chance we'll produce a ship below. 
            var totalProductionWeight = shipsToProduce.Sum(w => w.productionWeight * (w.maxAmount - w.currentAmount));

            //Get the lowest ship cost. We'll stop once we've produced as much as we can. 
            var lowestShipCost = shipsToProduce.Min(p => p.EnemyShip.cost);


            //Set the initial amount of production we have available. 
            int productionRemaining = sector.production;
            Random numberGenerator = new Random();

            //This loop keeps us producing ships until we've spent all of our production. 
            while (productionRemaining >= lowestShipCost)
            {
                foreach (var shipToProduce in shipsToProduce)
                {
                    if (shipToProduce.EnemyShip.cost > productionRemaining)
                    {
                        //If we can't afford to make any more of this ship, move on. 
                        continue;
                    }
                    else if (shipToProduce.currentAmount < shipToProduce.maxAmount)
                    {
                        var chanceToProduce = ((double)shipToProduce.productionWeight / (double)totalProductionWeight);
                        double randNumber = numberGenerator.NextDouble();

                        if (randNumber <= chanceToProduce)
                        {
                            //Produce the ship.
                            shipToProduce.currentAmount++;
                            //Remove the production remaining from the sector...
                            productionRemaining -= shipToProduce.EnemyShip.cost;
                            //Remove this ship's weight out of the total weight, making other ships more likely to be picked.
                            totalProductionWeight -= shipToProduce.productionWeight;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Processes the production updates for a sector that has been claimed. This is ship points and ammunition production, mostly. 
        /// </summary>
        /// <param name="sector"></param>
        private void processClaimedSector(Sector sector)
        {
            //Only grab the munition assignments that need to be filled. This prevents an infinite loop below. 
            var ammunitionTypesToProduce = sector.MunitionsSectorAssignments.Where(m => m.currentAmount < m.maxAmount).ToList();

            //Set the current amount of production
            int productionRemaining = sector.production;

            //If there are any munitions to build, then we step into the production logic, where we produce different types of munition until we are done. 
            if (ammunitionTypesToProduce.Count > 0)
            {
                //Produce ammunition, and produce ship points. 
                var totalProductionWeight = ammunitionTypesToProduce.Sum(m => m.productionWeight * (m.maxAmount - m.currentAmount));

                //Fetch the minimum cost. We'll stop producing if our available production levels fall below this level. 
                var minimumProductionCost = ammunitionTypesToProduce.Min(m => m.Munition.cost);

                Random numberGenerator = new Random();

                int munitionRemaining = ammunitionTypesToProduce.Count();

                //This loop keeps us producing ammunition until we are out of production. 
                while ((productionRemaining >= minimumProductionCost) && (munitionRemaining > 0))
                {
                    foreach (var munitionToProduce in ammunitionTypesToProduce)
                    {
                        if (munitionToProduce.Munition.cost > productionRemaining)
                        {
                            //If we can't afford to make any more of this munition, move on. 
                            continue;
                        }
                        else if (munitionToProduce.currentAmount < munitionToProduce.maxAmount)
                        {
                            var chanceToProduce = ((double)munitionToProduce.productionWeight / (double)totalProductionWeight);
                            double randNumber = numberGenerator.NextDouble();

                            if (randNumber <= chanceToProduce)
                            {
                                //Produce the ship.
                                munitionToProduce.currentAmount++;
                                //Remove the production remaining from the sector...
                                productionRemaining -= munitionToProduce.Munition.cost;
                                //Remove this ship's weight out of the total weight, making other ships more likely to be picked.
                                totalProductionWeight -= munitionToProduce.productionWeight;
                            }
                        }
                    }//End Foreach. 

                    //Count how many munitions are left to build before everything is maxed out. 
                    munitionRemaining = ammunitionTypesToProduce.Where(m => m.currentAmount < m.maxAmount).Count();
                } //End while loop.
            } //End "if there are munitions to build". 
        }
        #endregion

        #region "BattleResultProcessing"
        #endregion




    }
}
