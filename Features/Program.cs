using System;
using System.Collections.Generic;
using System.Linq;

namespace Features
{
    class Program
    {

        //Var is used for local variables only, cant be used across classes
        // var is used implictly and must be inferred/instanstiated
        static void Main(string[] args)
        {
            //Testing Out the 'Func' function.  First is it takes in an INT and returns an INT
            Func<int, int> square = x => x * x;
            //Simple tester
            Console.WriteLine(square(3));
            //Takes in 2 INT's and returns 1 INT
            Func<int, int, int> add = (x, y) =>
            {
                //can also use curly brackets to make a method but then you must designate a return
                int temp = x + y;
                return temp;
            };
            //Simple tester
            Console.WriteLine(add(10, 5));

            //you can put put them into each other if they match inputs
            Console.WriteLine(square(add(3, 5)));

            //Action is also used but not alot of linq to do with it but always returns VOID, no return type
            // only describe the incoming parameters to the method
            Action<int> write = x => Console.WriteLine(x);

            //You have now rewritten Console.Writeline with just write, you can now put things inside of write if it fits such as ---
            write(square(add(2, 3)));

            var developers = new Employee[]
            {
                new Employee {Id = 1, Name = "Scott"},
                new Employee {Id = 2, Name = "Chris"}
            };

            var sales = new List<Employee>()
            {
                new Employee {Id = 3, Name = "Alex"}
            };

            Console.WriteLine(developers.Count());

            //Lamba expression so we dont have to create our own method to find names in our Employee field
            //This is if i want to search with just the start of the name of S
            //        foreach (var employee in developers.Where(x => x.Name.StartsWith("S")))
            //Double lamba, first fileters all names in developers with 5 length and then orders them by name Asscending

            //var can also be used to shorthand something
            //Example, we can make a query and then stick the query where a lamba would go
            //This is the method syntax for a query, using methods such as 'orderby' and '.length' 
            //which is the same both ways but a little different when coding out
            var query = developers.Where(x => x.Name.Length == 5)
                                               .OrderBy(i => i.Name);

            //This is query syntax, for linq it will start with 'from' and always end in either 'select' or 'group'
            //This is selecting a var 'developer' in table 'developers' where name is equal to 5 and then orderby name ASC and
            //then selecting all developer in developers
            var query2 = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;
            //can use 'query' and 'query2' above with no change to result

            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);

            }
            Console.ReadLine();
        }
    }
}
