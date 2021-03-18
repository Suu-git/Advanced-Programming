using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advanced_Programming
{
    class Validate
    {
        private Validate() { }

        public static bool ListIsNull(List<Person> listToCheck, PersonPosition position)
        {
            var foo = !listToCheck.Where(a => a.Position == position).Any();
            if (foo)
            {
                Console.WriteLine(
                    "=======================\n" +
                    $"There are no {position}s in the current list!\n" +
                    "Please add more!"
                );
                Console.ReadLine();
            }
            return foo;
        }

        public static bool KeyIsNull(Person personToCheck, string id)
        {
            var foo = personToCheck is null;
            if (foo)
            {
                Console.WriteLine(
                    "=======================\n" +
                    $"This {id} is not in the list");
                Console.ReadLine();
            }
            return foo;
        }

        public static bool InputIsNull(string field)
        {
            var foo = string.IsNullOrWhiteSpace(field);
            if (foo)
            {
                Console.WriteLine(
                    "=======================\n" +
                    $"This {field} is required!");
                Console.ReadLine();
            }
            return foo;
        }

        public static bool IsDuplicate(IEnumerable<Person> listToCheck, string field)
        {
            var foo = listToCheck.Any();
            if (foo)
            {
                Console.WriteLine(
                    "=======================\n" +
                    $"This {field} is already existed");
                Console.ReadLine();
            }
            return foo;
        }
    }
}
