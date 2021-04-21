using System;
using System.Collections.Generic;
using System.Text;

namespace Queries
{
    public static class MyLinq
    {


        public static IEnumerable<double> Random()
        {
            var random = new Random();
            while(true)
            {
                yield return random.NextDouble();
            }
        }



        //This is almost how linq.Where function works

        //This is an extention method, where it returns an IEnum<T>
        //Returns: IEnum<T>
        //source: an IEnum<T> must be passed in as a type
        //predicate: whatever the specific info is goign to be passed in
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, 
                                               Func<T, bool> predicate)
        {
            ////new result to return later
            //var result = new List<T>();

            ////foreach of the items of the type we passed in
            ////in this case we passed in the list of movies we created, source is all 4 of them in this case
            //foreach (var item in source)
            //{
            //    //predicate is what we are comparing it against, in this case, any movie with year over 2000
            //    if(predicate(item))
            //    {
            //        //if true, the result will add the item, each movie with their set of variables, to the new lsit to return
            //        result.Add(item);
            //    }
            //}
            //return result;

            //The following will match linq's 'Where' method much closer, I will be using the 'yeild' cammand
            foreach (var item in source)
            {
                //predicate is what we are comparing it against, in this case, any movie with year over 2000
                if(predicate(item))
                {
                    //if true, the result will add the item, each movie with their set of variables, to the new lsit to return
                    //'yield' will help build an IEnum and will beuild a sequence of items that I can iterate over with a foreach loop
                    //execution will start only when something tries to pull out of iEnum 
                    //Long story short, it will build a list of things untill it hits 'yield'
                    //it will stop until we ask it for soemthing, and then resume right where it left off
                    //in this case it will build a list of movies starting with the Dark knight, it will not write anything 
                    //in the console  as it will hold at the 'yield' below and will not jump out of the foreach loop
                    //once our query asks for the information, it will send the first movie, 'Dark Knight'
                    //and then resume over the remaining movies

                    //This is known as Deferred Execution
                    //Deferred Execution will build the data structure but will not show untill you excute it to do so
                    //Two different types of deferred execution: Streaming or non streaming
                    // Where is an example of a streaming operator
                        // Streaming Operators: read through the source data untill it produces a result then it will yeild
                        // that result so we can process
                    yield return item;
                }
            }

        }
    }
}
