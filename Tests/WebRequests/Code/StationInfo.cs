using System.Collections;
using System.Collections.Generic;

namespace Test
{
    public class StationInfo
    {
        // This finds the address of a station via its id.
        public string GetStationAddress(string endpoint, int? stationId)
        {
            WebRequests webRequests = new WebRequests(endpoint);
            Dictionary<string, object> data = webRequests.HttpDataToDictionary();
            dynamic altFuelStation = data["alt_fuel_station"];

            string stationAddress = "";
            foreach (string addressPart in new[]{"street_address", "city", "state", "zip"}) stationAddress += altFuelStation[addressPart] + ", ";
            return stationAddress.Substring(0, stationAddress.Length-2);
        }

        // This finds the idea of a station name.
        public int? GetStationId(string endpoint, string stationName)
        {
            WebRequests webRequests = new WebRequests(endpoint);
            Dictionary<string, object> data = webRequests.HttpDataToDictionary();

            IList fuelStations = (IList) data["fuel_stations"];

            for (int i=0; i<fuelStations.Count; i++)
            {
                dynamic fuelStation = fuelStations[i];
                if (fuelStation["station_name"] == stationName) return fuelStation["id"];
            }

            return null;
        } 
    }
}