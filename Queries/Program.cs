using System;
using System.Collections.Generic;
using System.Linq;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {

            //This is to simple showcase streamed and no nstreamed execution.
            //Where is streamed, it will constanly push out info from previous loops and then go back while still in the loop.
            //Take is non streamed.  for this simple showcase, it must reach 10 numbers before it can execute.  if we get rid of Take()
            // This is an infinite loop because it is always true in the MyLinq class.  It is always yeilding a new double
            //With take, once it reaches 10, the system now knows it can get those 10 and stop the streaming service of Where
            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            //I will be using Linq queries to extract this info
            // Uses the class 'Movie' which has the following below var's to make a list
            var movies = new List<Movie>
            {
                new Movie {Title = "The Dark Knight", Rating = 8.9f, Year = 2008 },
                new Movie {Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Movie {Title = "Casablanca", Rating = 8.5f, Year = 1942 },
                new Movie {Title = "Star Wars V", Rating = 8.7f, Year = 1980 },
            };

            //Queries


            // this is actual variable we want to use
            // /* use this when using streaming deferred execution*/var query = movies.Filter(m => m.Year > 2000).ToList();

            // this is a non streaming deferred execution, using 'OrderBy'
            // If we were to get rid if of the movenext operator below, the console will show nothing because 'OrderBy'
            //uses non streamind deferred execution.  It does not hold any partial information of our list becuase it needs
            //to look at all the information for it to know how to order our list correctly.
            //For example, below, we are sorting by the rating, but it cannot confirm if the list is correct if it cannot see all the results
            //These need to be carefully used because some information might be 40,000 things.  If we use streaming, and something like the TAKE operator
            // we can go through very little information
            // if we use non streaming, Orderby has to go through ALL the information to sort them correctly

            //if we have iEnumerables think of this expression as a pipeline:
            //Where is your Filter,
            //Orderby is your Sort,
            //Much easier to first filter and then sort, and not the other way around.

            //Extention syntax
            var query = movies.Where(m => m.Year > 2000)
                              .OrderByDescending(m => m.Rating);

            //query syntax
            var query2 = from movie in movies
                         where movie.Year > 2000
                         orderby movie.Rating descending
                         select movie;


            //      when using the following block of code make sure to uncomment exception in Movie.cs for testing purposes
            //      want to test how deferred execution works
            //    //Testing Variable for error
            //var query = Enumerable.Empty<Movie>();
            ////This is an error that will try to make a list out of empty query above
            ////If you remove 'ToList()' it doesnt have concrete data to count through when execution 'Count' below, causing a failure
            ////Hence deferred execution isnt always your friend
            //try
            //{
            //    query = movies.Where(m => m.Year > 2000);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            ////1.Getting our query to show above movie after year 2000
            //1.foreach (var movie in query)
            //{
            //    Console.WriteLine(movie.Title);
            //}
            var enumerator = query.GetEnumerator();
            //Just want to see how many objects in query, which is using Filter, which is using deferred excution,
            //This hurts us because it loads everything and then loads it agin to do second query below writeline
            //If we add a simple 'ToList()' above to the end of our query, it creats a list that has our filter and doesnt need to go over all the
            //items in original list so we can circumvent the deferred execution.
            //Even shorter note, deferred execution is abstract, as it holds the data out there untill you explicity call for it,
            //while the 'ToList()' takes that abstract data and creates the list, which we can sorta hide in the background untill we need it.
            //-----sidenote, this will free up that memory that will just hold it in no mans land untill called.
            //Console.WriteLine(query.Count());
            while(enumerator.MoveNext())
            {
                //This will show how deffered execution actually works
                //While there is more information to be had, 'MoveNext()' above, it will step out of the foreach in 'Mylinq.cs
                //when it goes through each of the Movies in 'List Movies'.  So when it goes through 'Dark Knight', 'Kings Speech', 'Casablanca',
                // 'Star Wars V' it will stop at yield and send the info here if it matches or not to our Filter query above.  if yes, sends info here
                // and then yields untill moved into the 'movenext' enum
                Console.WriteLine(enumerator.Current.Title);
            }



            Console.ReadLine();
        }
    }
}
