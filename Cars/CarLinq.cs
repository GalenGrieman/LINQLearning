using System;
using System.Collections.Generic;
using System.Text;

namespace Cars
{
    public static class CarLinq
    {

        //This is an extension method created by us and taking away the unneeded power tap from Car.cs.  This does the same thing as before
        //but now we can just use the '.ToCar' extenstion
        public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
        {
            ////We are kinda cheating.  We know in our CSV file that all the columns are seperated with a comma ',' so we are just using that
            ////information to split all of the information with this var columns here v--- below
            //// we can then seperate the columns that are made into our ID data 

            foreach (var line in source)
            {
                var columns = line.Split(',');
                yield return new Car
                {
                    Year = int.Parse(columns[0]),
                    Manufacturer = columns[1],
                    Name = columns[2],
                    Displacement = double.Parse(columns[3]),
                    Cylinders = int.Parse(columns[4]),
                    City = int.Parse(columns[5]),
                    Highway = int.Parse(columns[6]),
                    Combined = int.Parse(columns[7])
                };
            }
        }
    }
}
