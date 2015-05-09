using System;
using OpenQA.Selenium;
using NUnit.Framework;

namespace Test.Tests
{
    public class WebRequestTests
    {
        public IWebDriver driver;
        
        [SetUp]
        public void Create_Driver()
        {
        }

        [TearDown]
        public void Teardown()
        {
        }

        
        [Test]
        public void Request()
        {
            string demoKey = "Z8gg5MvWhSTvkWFA6ZL3JzG6UqHVOYvULj7iQnOd";
            string austinEvNetworks = "http://api.data.gov/nrel/alt-fuel-stations/v1/nearest.json?api_key={0}&location=Austin+TX&ev_network=ChargePoint%20Network".CustomFormat(demoKey);

            string station = "HYATT AUSTIN";
            StationInfo stationInfo = new StationInfo();
            int? stationId = stationInfo.GetStationId(austinEvNetworks, station);
            Console.WriteLine("{0} ID: {1}".CustomFormat(station, stationId));

            string hyattEvNetworkLocation = "http://api.data.gov/nrel/alt-fuel-stations/v1/{0}.json?api_key={1}".CustomFormat(stationId, demoKey);
            string stationAddress = stationInfo.GetStationAddress(hyattEvNetworkLocation, stationId);
            Console.WriteLine("{0} Address: {1}".CustomFormat(station, stationAddress));
        }
    }
}