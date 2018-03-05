namespace Solution5
{
    public class Station
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public Position Position { get; set; }
        public bool Banking { get; set; }
        public bool Bonus { get; set; }
        public string Status { get; set; }
        public string Contract_name { get; set; }
        public int Bike_stands { get; set; }
        public int Available_bike_stands { get; set; }
        public int Available_bikes { get; set; }
        public object Last_update { get; set; }
        public string ToStrings() { return Name + " - " + Available_bikes + " vélos célibataires"; }
    }

    public class Position
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
