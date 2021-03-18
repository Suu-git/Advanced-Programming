using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Advanced_Programming
{
    internal class Management
    {
        private List<Person> _listPerson;

        public Management(List<Person> listPerson) => _listPerson = listPerson;

        public void MainManage()
        {
            Console.Clear();
            Console.WriteLine(
                "=======================\n" +
                "1.Manage Students\n" +
                "2.Manage Lecturers\n" +
                "3.Exit\n" +
                "=======================");

            var choice = int.Parse(Console.ReadLine());
            do
            {
                var cases = new Dictionary<Func<int, bool>, Action>
                {
                    { x => x == 1, () => PersonManagement(PersonPosition.Student)   },
                    { x => x == 2, () => PersonManagement(PersonPosition.Lecturer)  },
                    { x => x == 3, () => Environment.Exit(0)                        },
                };
                cases.FirstOrDefault(c => c.Key(choice)).Value();
            } while (choice > 0 || choice < 4);
            Console.ReadLine();
        }

        private void PersonManagement(PersonPosition position)
        {
            OperationDisplay(position);

            // Opetation determination process
            int choice;
            do
            {
                OperationDisplay(position);
                choice = int.Parse(Console.ReadLine());

                var cases = new Dictionary<Func<int, bool>, Action>
                {
                    { x => x == 1, () => Add(position)      },
                    { x => x == 2, () => ViewAll(position)  },
                    { x => x == 3, () => Search(position)   },
                    { x => x == 4, () => Delete(position)   },
                    { x => x == 5, () => Update(position)   },
                    { x => x == 6, () => MainManage()       },
                };
                cases.FirstOrDefault(c => c.Key(choice)).Value();
            } while (choice > 0 || choice < 7);
        }

        private void Add(PersonPosition position)
        {
            Console.Clear();
            Console.WriteLine("Please enter the following fields!");
            var personToAdd = inputFields(position);
            if (Validate.InputIsNull(personToAdd[0])
                || Validate.InputIsNull(personToAdd[3]))
                return;
            // TODO: Please test this - PersonManagement(position);

            var creationCases = new Dictionary<Func<PersonPosition, bool>, Action>
            {
                { x => x == PersonPosition.Student, 
                    () => _listPerson.Add(new Student(personToAdd)) },
                { x => x == PersonPosition.Lecturer, 
                    () => _listPerson.Add(new Lecturer(personToAdd)) },
            };
            creationCases.FirstOrDefault(c => c.Key(position)).Value();

            Console.WriteLine("\nAdd Successfully!");
            Console.ReadKey();
        }

        private void ViewAll(PersonPosition position)
        {
            Console.Clear();
            if (Validate.ListIsNull(_listPerson, position)) return;
            else
            {
                var resultList = _listPerson.Where(a => a.Position == position).ToList();
                Console.WriteLine(
                    $"LIST OF {position}s:\n" +
                    "=======================");
                foreach (var p in resultList) Console.WriteLine(p.DiplayPerson());
                Console.ReadLine();
            }
        }

        private void Search(PersonPosition position)
        {
            Console.Clear();
            if (Validate.ListIsNull(_listPerson, position)) return;
            else
            {
                Console.Write($"Enter the {position}'s name: ");
                var personToSearch = Console.ReadLine();

                var resultList = _listPerson.Where(p => p.GetName() == personToSearch
                                                        || p.Position == position)
                                            .ToArray();
                Console.WriteLine(
                    "=======================\n" +
                    "Results:");
                if (resultList is null)
                {
                    Console.WriteLine("No results!");
                    Console.ReadLine();
                    return;
                }
                else foreach (var p in resultList) Console.WriteLine(p.DiplayPerson());
                Console.ReadLine();
            }
        }

        private void Delete(PersonPosition position)
        {
            Console.Clear();
            if (Validate.ListIsNull(_listPerson, position)) return;
            else
            {
                Console.Write($"Enter the {position}'s ID to Delete: ");
                var foo = Console.ReadLine();
                var personToDelete = _listPerson.SingleOrDefault(p => p.GetID() == foo
                                                                || p.Position == position);

                if (Validate.KeyIsNull(personToDelete, foo)) return;
                else
                {
                    _listPerson.Remove(personToDelete);
                    Console.WriteLine(
                            "=======================\n" +
                            $"{personToDelete}\n\n" +
                            "Delete succesfully!");
                    Console.ReadLine();
                }
            }
        }

        private void Update(PersonPosition position)
        {
            Console.Clear();
            if (Validate.ListIsNull(_listPerson, position)) return;
            else
            {
                Console.Write($"Enter the {position}'s ID to Update: ");
                var foo = Console.ReadLine();
                var personToUpdate = _listPerson.FirstOrDefault(p => p.GetID() == foo
                                                                     || p.Position == position);
                Console.WriteLine(
                    $"\n{personToUpdate.DiplayPerson()}\n" +
                    "=======================\n");

                var listOfUpdate = inputFields(position);

                if (Validate.KeyIsNull(personToUpdate, foo)) return;
                // TODO: Please test this - PersonManagement(position);

                // Basically switch cases
                var updateCases = new Dictionary<Func<int, bool>, Action>
                {
                    { c => c == 0, () => personToUpdate.SetID(listOfUpdate[0]) },
                    { c => c == 1, () => personToUpdate.SetName(listOfUpdate[1]) },
                    { c => c == 2, () => personToUpdate.SetDateOfBirth(DateTime.Parse(listOfUpdate[2], CultureInfo.CreateSpecificCulture("de-DE"))) },
                    { c => c == 3, () => personToUpdate.SetEmail(listOfUpdate[3]) },
                    { c => c == 4, () => personToUpdate.SetAddress(listOfUpdate[4]) },
                    { c => c == 5, () => personToUpdate.SetDivision(listOfUpdate[5]) },
                };
                for (int i = 0; i < listOfUpdate.Length; i++)
                    if (!string.IsNullOrWhiteSpace(listOfUpdate[i]))
                        updateCases.First(c => c.Key(i)).Value();

                Console.WriteLine("\nUpdate successfully!");
            }
        }

        private void OperationDisplay(PersonPosition position)
        {
            Console.Clear();
            Console.WriteLine(
                "=======================\n" +
                "1.Add new {0}\n" +
                "2.View all {0}\n" +
                "3.Search {0}\n" +
                "4.Delete {0}\n" +
                "5.Update {0}\n" +
                "6.Back to main menu\n" +
                "=======================\n", position);
        }

        private string[] inputFields(PersonPosition position)
        {
            var division = position == PersonPosition.Lecturer ? "DEPARTMENT" : "CLASS";
            string[] fieldNames =
            {
                "ID: ",
                "NAME: ",
                "DATE OF BIRTH: ",
                "EMAIL: ",
                "ADDRESS: ",
                $"{division}: "
            };

            var fieldToOperate = new List<string>();
            for (int i = 0; i < 6; i++)
            {
                Console.Write(fieldNames[i]);
                fieldToOperate.Add(Console.ReadLine());
            }

            return fieldToOperate.ToArray();
        }
    }
}