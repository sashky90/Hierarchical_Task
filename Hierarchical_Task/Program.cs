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
            string employeeInput1 = Console.ReadLine().Trim();
            string employeeInput2 = Console.ReadLine().Trim();
            string hierarchy;

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
                    Console.WriteLine(ex.Message);
                }
                
                personList.Add(emp);
            }

            var employeePerson1 = personList.Find(employee => employee.Name == employeeInput1);
            var employeePerson2 = personList.Find(employee => employee.Name == employeeInput2);
            var listBossesPerson1 = GetBossList(employeePerson1);
            var listBossesPerson2 = GetBossList(employeePerson2);

            var commonBoss = GetCommonBoss(listBossesPerson1, listBossesPerson2).Name;
            Console.WriteLine(commonBoss);
        }

        public static List<Person> GetBossList(Person person)
        {
            var bossList = new List<Person>();

            while (person.HasBoss())
            {
                var boss = person.GetBoss();
                bossList.Add(boss);
                person = boss;
            }

            return bossList;
        }

        public static Person GetCommonBoss(List<Person> list1, List<Person> list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                var index = list2.IndexOf(list1[i]);
                if (index != -1)
                {
                    return list1[i];
                }
            }

            throw new ArgumentNullException();
        }
    }
}
