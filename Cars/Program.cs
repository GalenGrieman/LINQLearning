using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cars
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is our file path to our CSV file
            var cars = ProcessCars("fuel.csv");
            var manufacturers = ProcessManufacturers("manufacturers.csv");

            //This uses a secondary sort, for each sort beyond the first, always use 'ThenBy' as using 'OrderBy' goes back to original list
            //not the new list that we just filtered
            //var query = cars.OrderByDescending(c => c.Combined)
            //                .ThenBy(c => c.Name);
            //query syntax of above.  Filters such as orderby are on their own line no 'ThenBy'  the comma seperating car.Combined and car.name 
            //makes them follow that sorting in the order it is read, THE COMMA is effectively 'thenby'
            //note that when we use 'where' it comes first before 'orderby'  remeber to filter and then sort
            //var query =
            //        from car in cars
            //        where car.Manufacturer == "BMW" && car.Year == 2016
            //        orderby car.Combined descending, car.Name ascending
            //        select new
            //        {
            //            car.Manufacturer,              
            //            car.Name,
            //            car.Combined
            //        };

            //We will now be using the Join part of this query syntax, a lot like 'from'
            //we are using two different 'databases' or files.  The fuel.csv and manufacturers.csv.
            //var query =
            //        from car in cars
            //        join manufacturer in manufacturers on car.Manufacturer equals manufacturer.Name
            //        orderby car.Combined descending, car.Name ascending
            //        select new
            //        {
            //            manufacturer.Headquarters,
            //            car.Name,
            //            car.Combined
            //        };

            //This query is the same as above but we are now using a composite key
            //We are going to be using  two parts of data, ythe year as well as the manuf.
            //we can use an object that contains both to join against this data
            //var query =
            //            from car in cars
            //            join manufacturer in manufacturers 
            //            on new { car.Manufacturer, car.Year } 
            //            //because we are joining two objects instead of two data types
            //            //the inside data types of the composite object key have to match
            //            //easily fixed just adding Manufacturer = ''
            //            //both years are the same so we dont need to change that
            //            //now we have a manufactuer joining on a manufacturer and a year joining on a year
            //            equals new { Manufacturer = manufacturer.Name, manufacturer.Year }
            //            orderby car.Combined descending, car.Name ascending
            //            select new
            //            {
            //                manufacturer.Headquarters,
            //                car.Name,
            //                car.Combined
            //            };

            //extension syntax for above query
            //intellisence for join first asks what we want to join against.  we want to join it against the manufacturers
            //typically have the inner sequence smaller than the outer sequence.
            // we have 12000 cars but less than 50 manufacturers
            //var query2 =
            //    cars.Join(manufacturers, //With join, we are taking two data lists, cars' and 'manuf'
            //                             // we then take the manuf from cars 'c', and match it with the name from 'manuf' with 'm'
            //                             // we then make a new anon typing of 'c,m' for a new data struct
            //                c => c.Manufacturer, //inner key selector given a car 'c' we want to join on the cars manufacturer
            //                m => m.Name, //outer key selector when linq is looking at the manufacterur what value will it use to join the cars with manufacturer
            //                (c, m) => new //this is taking a car 'c' and manuf 'm'
            //                {
            //                    //because we are working against a new set of data with 'c' and 'm'
            //                    //we have to use those letters for shorthand here
            //                    m.Headquarters,
            //                    c.Name,
            //                    c.Combined
            //                })
            //        .OrderByDescending(c => c.Combined) //because we have now made a new set of data with 'c,m' all other sorting will be done against this new data structure
            //        .ThenBy(c => c.Name);


            //var query2 =
            //    cars.Join(manufacturers, //This is the same as the compostie key query syntax of above
            //                c => new { c.Manufacturer, c.Year }, 
            //                m => new { Manufacturer = m.Name, m.Year }, //we still have to match a manuf with a manuf and a year with a year
            //                (c, m) => new //
            //                {
            //                    m.Headquarters,
            //                    c.Name,
            //                    c.Combined
            //                })
            //        .OrderByDescending(c => c.Combined) //because we have now made a new set of data with 'c,m' all other sorting will be done against this new data structure
            //        .ThenBy(c => c.Name);

            //This is anon typing. Sort of like the use of 'var' it is contextual and when going through our query, it will only 
            //send out the below. For this instance, nice to only show this query for these three columns instead of the potential thousands
            //var queryResult = cars.Select(c => new { c.Manufacturer, c.Name, c.Combined });


            ////this is grouping of db items

            //var queryGrouping =
            //    from car in cars
            //    group car by car.Manufacturer.ToUpper() into manuf  //Here we have each 'x' in cars sorted by their Manufactuers, just a list of all manufacturers
            //    orderby manuf.Key
            //    select manuf;
            //// but behind the scenes keeps track of how many cars are in each manuf

            //var queryGrouping2 =
            //    cars.GroupBy(c => c.Manufacturer.ToUpper()) //extenstion method syntax of above
            //        .OrderBy(g => g.Key);


            //foreach (var group in queryGrouping2)
            //{
            //    Console.WriteLine(group.Key); //we grouped things on Manuf so the 'key is the list of manuf
            //    foreach(var car in group.OrderByDescending(c=> c.Combined).Take(2)) // for each manuf take the top 2 cars with the best CFE
            //    {
            //        Console.WriteLine($"\t{car.Name} has the Combined Fuel Efficiency of {car.Combined}");
            //    }
            //}


            //this is groupjoin
            //var queryGroupJoin =
            //    from manuf in manufacturers //we start with manuf because linq wants to use manuf to group things underneath of.
            //                                // so all the bmws will be placed under the bmw manufacturers
            //    join car in cars on manuf.Name equals car.Manufacturer //so this means we are joining the manuf Name where the car's manufacturers are the same
            //        into carGroup         //we then put that into a new group called carGroup. So we have allthe manufacters and their cars 
            //    select new //anon typing we can make new vars here
            //    {
            //        Manufacturer = manuf,
            //        Cars = carGroup
            //    };

            //var queryGroupJoin2 =
            ////syntax of above
            ////we take the manufacturers data, we tell it to group join with the cars data
            //// with this syntax the m on the left represents the manufacturers data
            //// the c on the right represents the car data
            ////we are telling it to join on the manufactuers name and the cars manufactuere and join that data wehre it equals
            //// we then have the result (m,g) with the anon typing with that lamda new
            ////we then tell it that the assign Manufacturer to (m, and assign Cars to g)
            //    manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
            //    (m, g) =>
            //      new
            //      {
            //          Manufacturer = m,
            //          Cars = g
            //      })
            //    .OrderBy(m => m.Manufacturer.Name);

            //foreach (var group in queryGroupJoin2)
            //{
            //    Console.WriteLine($"{group.Manufacturer.Name}: Country: {group.Manufacturer.Headquarters}"); //we cant use 'KEY' here. 
            //                                                                                                 //but with anon typing we have acess to both manufacturers and cars data
            //    foreach (var car in group.Cars.OrderByDescending(c => c.Combined).Take(2))
            //    {
            //        Console.WriteLine($"\t{car.Name} has the Combined Fuel Efficiency of {car.Combined}");
            //    }
            //}

            // TODO: Show the fuel efficient cars by country
            //pretty much identical to above but we but it 'into' a result so we can group them all by headquarters, which is now the 'Key'
            //var queryGroupJoin3 =
            //     from manuf in manufacturers
            //     join car in cars on manuf.Name equals car.Manufacturer
            //         into carGroup
            //     select new
            //     {
            //         Manufacturer = manuf,
            //         Cars = carGroup
            //     } into resultGroupJoin3
            //     group resultGroupJoin3 by resultGroupJoin3.Manufacturer.Headquarters;


            //Syntax of above
            //var queryGroupJoin3 =
            //    manufacturers.GroupJoin(cars, m => m.Name, c => c.Manufacturer,
            //    (m, g) =>
            //    new
            //    {
            //        Manufacturer = m,
            //        Cars = g
            //    })
            //    .GroupBy(m => m.Manufacturer.Headquarters);

            //foreach (var group in queryGroupJoin3)
            //{
            //    Console.WriteLine($"Country: {group.Key}"); //we CAN use key here because it has a key property it was grouped on

            //    foreach (var car in group.SelectMany(g => g.Cars) //Select many is very good here.  Each 'Headquarters' or country has multiple cars
            //                                                      //With select many it takes each county's list of cars and 'flattens the array'
            //                                                      // l
            //                                                      // i
            //                                                      // k
            //                                                      // e
            //                                                      // t
            //                                                      // h
            //                                                      // i
            //                                                      // s
            //                                                      // so we can just take the top 3
            //                             .OrderByDescending(c=> c.Combined).Take(3))
            //    {
            //        Console.WriteLine($"\t{car.Name} has the Combined Fuel Efficiency of {car.Combined}");
            //    }
            //}


            //var queryGroupJoin4 =
            //    from car in cars
            //    group car by car.Manufacturer into carGroup
            //    select new
            //    {
            //        Name = carGroup.Key,
            //        Max = carGroup.Max(c => c.Combined),          //this will iterate through the whole carGoup 3 times, 1 for max,
            //        Min = carGroup.Min(c => c.Combined),          // 1 for min
            //        Average = carGroup.Average(c => c.Combined)   // 1 for average, 3 loops
            //    } into resultGroupJoin4
            //    orderby resultGroupJoin4.Max descending
            //    select resultGroupJoin4;

            //var queryGroupJoin4 =
            //    cars.GroupBy(c => c.Manufacturer)
            //        .Select(g =>
            //        {
            //            //Aggregate needs a lot of stuff but once it has it, it does everything above but only iterates once
            //            //Too offset the load needed we created our own class for 'CarStatistics' down below, usually would create a new class in 
            //            // a seperate file but this is small project
            //            //Aggreate needs an accumulator for initial state, car statistics offers all 3 for min, max, and average.
            //            var resultsGroupJoin4 = g.Aggregate(new CarStatistics(),
            //                                                        (acc, c) => acc.Accumulate(c),
            //                                                        acc => acc.Compute());
            //            return new
            //            {
            //                Name = g.Key,
            //                Average = resultsGroupJoin4.Average,
            //                Min = resultsGroupJoin4.Min,
            //                Max = resultsGroupJoin4.Max,

            //            };
            //        })
            //        .OrderByDescending(r => r.Max);

            //foreach (var car in queryGroupJoin4)
            //{
            //    Console.WriteLine($"{car.Name}");
            //    Console.WriteLine($"\t Max Combined Fuel Rating: {car.Max}");
            //    Console.WriteLine($"\t Min Combined Fuel Rating: {car.Min}");
            //    Console.WriteLine($"\t Average Combined Fuel Rating: {car.Average}");
            //}

            //Select Many is the short way of going to the second foreach below.  It goes into the IEnum of the IEnum.
            //In this example, Name could be Ford F150.  The 'Ford' is its only array of 'F' 'O' 'R' 'D'.  so it singles them out into their own
            // console line.
            // with select many, it does that for us.
            //var selectManyResult = cars.SelectMany(c => c.Name);

            //var selectManyResult = cars.Select(c => c.Name);
            //foreach (var name in selectManyResult)
            //{
            //    foreach (var character  in name)
            //    {
            //        Console.WriteLine(character);
            //    }

            //Because we have selectMany used above, we get the same as the double foreach loop above.
            //foreach (var name in selectManyResult)
            //{
            //    Console.WriteLine(name);
            //}


            //}


            Console.WriteLine();

            //this is where we use the extenstion 'First'
            //It can be used last in our extenstions, and it does not offer deffered execution and it must execute immediately
            //It can also be used like 'where'  and offers a bool operator in its overload.
            //this is scary and must be done carefully.  If done before oour sorting, most likely you will get wrong answer to what you want
            //in this case you would filter first and then select the 'First' compared to your filter such as this
            //First(c => c.Manufacturer == "BMW" && c.Year == 2016)
            //But that means 'OrderBy' must go through ALL the data in our enumerable and that could potentially take a lot of time
            //Last also exists and functions the same way
            //First is also not available in query syntax
            //Both 'First' and 'Last' also have a OrDefault function
            //you can use these against a potentially NULL set so your query doesnt fuck up


            //var top =
            //        cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
            //            .OrderByDescending(c => c.Combined)
            //            .ThenBy(c => c.Name)
            //            .First();

            //This takes our var 'query' and finds the top ten cars for what 'query' is, this case. best combines fuel rating
            //We then take the top ten from 'query' and write out their names for each car
            //Console.WriteLine($"Combined Fuel Rating of the following:");
            //foreach (var car in query2.Take(10))
            //{
            //    Console.WriteLine($"{car.Headquarters} {car.Name} : {car.Combined}");
            //}


            //These operators, 'Any' and 'All' are both extremely lazy and explicit, both are BOOL
            //Any will see if only 1 is named Ford and says its true
            //All will see if only 1 doesnt match and say its false
            //this means however Any will check all of them untill it sees Ford
            //All will check all of them untill it sees soemthing other than Ford
            var result = cars.Any(c => c.Manufacturer == "Ford");
            var result2 = cars.All(c => c.Manufacturer == "Ford");

            Console.WriteLine($"\n{result}\n{result2}");



            //This lists all cars by name in the order we have it in our file.  WHich is alphabetical in our case
            //foreach (var car in cars)
            //{
            //    Console.WriteLine(car.Name);
            //}

            Console.ReadLine();
        }

        private static List<Car> ProcessCars(string path) //This is our file path that returns our query.
        {
            //Using Extenstion method for linq
            //We made an extenstion method 'ToCar' and therefore have to cast this as a variable because we cannot implicity change from an
            //IENUM to a LIST, sorta like a rectangle isnt a square but a square is a rectangle type dealio

            var query =
                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToCar();

            //using query syntax
            // have to wrap it all in paranthesis because query syntax ends in at select and cannot append a .Tolist();
            // simple workaround below.
            //var query =
            //    from line in File.ReadAllLines(path).Skip(1)
            //    where line.Length > 1
            //    select Car.ParseFromCSV(line);
            return query.ToList();
        }

        public class CarStatistics
        {
            public CarStatistics()
            {
                Max = Int32.MinValue;
                Min = Int32.MaxValue;
            }


            internal CarStatistics Accumulate(Car car)
            {
                Total += car.Combined;
                Count += 1;
                Max = Math.Max(Max, car.Combined);
                Min = Math.Min(Min, car.Combined);
                return this;
            }

            public CarStatistics Compute()
            {
                Average = Total / Count;
                return this;
            }

            public int Max { get; set; }
            public int Min { get; set; }
            public int Total { get; set; }
            public int Count { get; set; }
            public double Average { get; set; }
        }


        private static List<Manufacturer> ProcessManufacturers(string path)
        {
            var query =
                File.ReadAllLines(path)
                    .Where(l => l.Length > 1)
                    .Select(l =>
                    {
                        var columns = l.Split(',');
                        return new Manufacturer
                        {
                            Name = columns[0],
                            Headquarters = columns[1],
                            Year = int.Parse(columns[2])
                        };
                    });

            return query.ToList();
        }
    }
}
