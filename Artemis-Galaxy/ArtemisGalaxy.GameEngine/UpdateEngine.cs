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

        #region "Constructors"

        //Constructor
        public UpdateEngine(Campaign campaignToUpdate, ArtemisGalaxyDevEntities dataContext)
        {
            _campaign = campaignToUpdate;
            _dataContext = dataContext;
        }

        #endregion

        #region "Public methods"
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

        private void processUnClaimedSector(Sector sector)
        {
            //Things we need to do here:
            //  Each sector has a production number. We'll need to look up the production of each sector, and then produce that many ships based on some sort of logic. 
            //  Thinking that the sector difficulty rating adjusts the weight on the ship types that we should produce in this sector. 
            //  Also, each sector has certain ships it should produce and not produce, and how many counts as "full" for that sector. 
            //  Sectors should have a full and max amount for each ship. This allows us to control the maximum number of ships that "backfill" into other sectors. 

            //Get the total weight for production in this sector. This lets us calculate the % chance we'll produce a ship below. 
            var totalProductionWeight = sector.EnemyShipSectorAssignments.Sum(w => w.productionWeight * (w.maxAmount - w.currentAmount));

            //Get the lowest ship cost. We'll stop once we've produced as much as we can. 
            var lowestShipCost = sector.EnemyShipSectorAssignments.Min(p => p.EnemyShip.cost);

            //Get the list of ships we should produce for this sector. 
            var shipsToProduce = sector.EnemyShipSectorAssignments.ToList();

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

        private void processClaimedSector(Sector sector)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region "Private Methods"
        //Reads the config, and beings the update process for sectors. 
        private void initialize()
        {
            //We'll start with weekly updates for now. Eventually a config file will be here that will be read in, letting us adjust how this all works. -MWS.
            //                                                      ^Or it's a column in the campaign table. I haven't decided yet. 
            _updateInterval = new TimeSpan(7, 0, 0, 0);
        }
        #endregion
    }
}
