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
            throw new NotImplementedException();
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
