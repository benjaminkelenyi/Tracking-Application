using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShowMarkesGPS
{
    public class GetCoordonates
    {
        private double latitude, longitude;

        public GetCoordonates(double lt, double lg){
            latitude = lt;
            longitude = lg;
        }

        public void setLatitude(double lt){
            latitude = lt;
        }
        public void setLongitude(double lg){
            longitude = lg;
        }
        public double getLatitude(){
            return latitude;
        }
        public double getLongitude(){
            return longitude;
        }
    }
}