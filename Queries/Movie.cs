using System;
using System.Collections.Generic;
using System.Text;

namespace Queries
{
    public class Movie
    {
        public string Title { get; set; }
        public float Rating { get; set; }
        int _year;
        public int Year { get 
            {
                //Simple error for testing purposes
                //throw new Exception("Error!");
                //This is to see how many times it access all the information for the list we have made
                Console.WriteLine($"Returning {_year} for {Title}");
                return _year;
                //When the program runs, we can see it see ALL the information presented in the list,
                //then it changes the list to match our function, this case, any move with year over 2000
                //Then it presents in the way we wanted, this case, just the movies' title
                //Above is how the custom 'Filter' Method we made does the sorting

                //Using the 'Where' linq method is different
                //It will still go through every item on the list but once it has found the match, it will then add it
                //to our query
                //While it still effectively does the same thing,
                //our method and the 'Where' method are slightly different on how they handle it
            }
            set
            {
                _year = value;
            } }
    }
}
