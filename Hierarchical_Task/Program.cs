namespace Hierarchical_Task
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            const string ExitMessage = "exit";

            List<Person> personList = new List<Person>();
            string hierarchy;

            Console.WriteLine("This program aims to find the first common boss of two employees in hierarchical structure of manager - employee");
            Console.WriteLine("Please enter first name: ");
            string employeeInput1 = Console.ReadLine().Trim();

            Console.WriteLine("Please enter second name: ");
            string employeeInput2 = Console.ReadLine().Trim();

            Console.WriteLine("Please enter N relations of {{manager}} - {{employee}} or click exit to execute the programe and find the common boss:");
            while ((hierarchy = Console.ReadLine().Trim()) != ExitMessage)
            {
                if (string.IsNullOrEmpty(hierarchy) || !hierarchy.Contains(" - "))
                {
                    Console.WriteLine("Wrong Input. Try again!");
                    continue;
                }

                string[] hierarchyArray = hierarchy.Split(new string[] { " - " }, StringSplitOptions.None);
                Person manager;
                if (personList.Count == 0)
                {
                    manager = new Person(hierarchyArray[0]);
                    personList.Add(manager);
                }
                else
                {
                    manager = personList.Find(employee => employee.Name == hierarchyArray[0]);
                }

                Person emp = new Person(hierarchyArray[1], manager);
                try
                {
                    manager.AddEmployee(emp);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.ParamName);
                }
                
                personList.Add(emp);
            }

            // Find the equivalent from the input of the employees from string to type Person
            Person employeePerson1 = personList.Find(employee => employee.Name == employeeInput1);
            Person employeePerson2 = personList.Find(employee => employee.Name == employeeInput2);
            
            List<Person> listBossesPerson1 = GetBossList(employeePerson1);
            List<Person> listBossesPerson2 = GetBossList(employeePerson2);
            try
            {              
                string commonBoss = GetCommonBoss(listBossesPerson1, listBossesPerson2).Name;
                Console.WriteLine("Common boss of {0} and {1} is {2}", employeeInput1, employeeInput2, commonBoss);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine(ex.ParamName);
            }        
        }

        // Get boss list of an employee from bottom to top
        public static List<Person> GetBossList(Person person)
        {
            List<Person> bossList = new List<Person>();

            while (person.HasBoss())
            {
                Person boss = person.GetBoss();
                bossList.Add(boss);
                person = boss;
            }

            return bossList;
        }

        // Get the common boss of two employees by first match from the list of bosses of one employee
        // in the list of bosses in other emloyee
        public static Person GetCommonBoss(List<Person> list1, List<Person> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                int index = list2.IndexOf(list1[i]);
                if (index != -1)
                {
                    Person commonBoss = list1[i];
                    return commonBoss;
                }
            }

            throw new ArgumentNullException("Common boss doesn't exist!");
        }
    }
}
