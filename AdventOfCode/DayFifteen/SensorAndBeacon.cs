using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.DayFifteen
{
    public class SensorAndBeacon
    {
        public int SensorX { get; }
        public int SensorY { get; }
        public int BeaconX { get; }
        public int BeaconY { get; }
        public int Distance { get; }

        public SensorAndBeacon(int sensorX, int sensorY, int beaconX, int beaconY)
        {
            SensorX = sensorX;
            SensorY = sensorY;
            BeaconX = beaconX;
            BeaconY = beaconY;
            Distance = Math.Abs(sensorX - beaconX) + Math.Abs(sensorY - beaconY);
        }
    }
}
