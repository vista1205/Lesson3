using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SApp01
{
    public enum Sex
    {
        Male,
        Female,
        Undefined
    }

    public class BaseClass
    {

    }
    
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public object Sex { get; set; }

    }

    public class PersonV2<T> where T : new()
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public T Sex { get; set; }

        public PersonV2(string name, int age, T sex)
        {
            Name = name;
            Age = age;
            Sex = sex;
        }

    }


    class Program
    {


        static void Swap<T>(ref T A, ref T B)
        {
            T t;
            t = A;
            A = B;
            B = t;
        }


        static void Main(string[] args)
        {
            var person1 = new Person() { Name = "Андрей", Age = 22, Sex = "Мужчина" };
            var person2 = new Person() { Name = "Андрей", Age = 22, Sex = Sex.Male };

            var person3 = new PersonV2<Sex>("Андрей", 22, Sex.Male);
            var person4 = new PersonV2<object>("Андрей", 22, "Мужчина");
        }
    }
}
