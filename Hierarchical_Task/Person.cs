namespace Hierarchical_Task
{
    using System;

    public class Person
    {
        private const int MinimumEmployeesPerBoss = 2;

        public Person(string name, Person boss = null)
        {
            this.Name = name;
            this.Boss = boss;
            this.Employees = new Person[MinimumEmployeesPerBoss];
        }

        public string Name { get; private set; }

        public Person Boss { get; private set; }

        public Person[] Employees { get; private set; }

        public void AddEmployee(Person employee)
        {
            if (this.Employees[MinimumEmployeesPerBoss - 1] == null)
            {
                for (int i = 0; i < this.Employees.Length; i++)
                {
                    if (this.Employees[i] == null)
                    {
                        this.Employees[i] = employee;
                        break;
                    }
                }
            }
            else
            {
                throw new System.ArgumentOutOfRangeException("Cannot add more than " + MinimumEmployeesPerBoss + " employees per person");
            }
        }

        public Person GetBoss()
        {
            return this.Boss;
        }

        public bool HasBoss()
        {
            if (this.Boss != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
