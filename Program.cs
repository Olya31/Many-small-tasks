using System;
using System.Collections.Generic;
using System.Linq;

namespace Many_small_tasks
{
    //ИСПОЛЬЗОВАНИЕ СТЕКА И ОЧЕРЕДИ
    class Student1
    {
        public string Name { get; set; }
        public bool Work { get; set; }
    }

    //ПЕРЕГРУЗКА ОПЕРАТОРА
    class My_String
    {
        public string FirstName, SecondName, Family;
        private string locationName;
        char[] array1 = { 'p', 'r', 'o', 'g', 'r', 'a', 'm', 'm', 'i', 'n', 'g', '!' };
        public void Enter()
        {
            for (int i = 0; i < array1.Length; i++)
                Console.Write(array1[i]);
            Console.WriteLine();
        }
        public My_String() { }
        public My_String(string name) => Name = name;

        public string Name
        {
            get => locationName;
            set => locationName = value;
        }

        public My_String(string FirstName, string SecondName, string Family)
        {
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.Family = Family;
        }

        public string Country { get; set; }

        public static My_String operator +(My_String c1, My_String c2) => new My_String { Country = c1.Country + c2.Country };
    }
    //КОЛЛЕКЦИИ
    class Student
    {
        private string FirstName;
        private string SecondName;
        private List<int> Marks = new List<int>();
        public double AvgMark;

        public Student()
        {
            Console.WriteLine("Введите свое имя:");
            this.FirstName = Console.ReadLine();

            Console.WriteLine("Введите фамилию:");
            this.SecondName = Console.ReadLine();

            Console.WriteLine("Введите 6 отметок за семестр:");
            Console.WriteLine("Введите 'выход', чтобы выйти");

            while (true)
            {
                string mark = Console.ReadLine();
                if (mark == "выход")
                {
                    break;
                }
                else
                {
                    try
                    {
                        Marks.Add(Convert.ToInt32(mark));
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine("Введено неверное знчение");
                        continue;
                    }
                }
            }

            double summ = 0;
            foreach (double mark in Marks)
            {
                summ += mark;
            }

            double count = Marks.Count;
            this.AvgMark = summ / count;
            Console.WriteLine($"Средний бал: {AvgMark:F2}");
        }

        public void Show()
        {
            Console.Write($"| { this.FirstName} | {this.SecondName} | {this.AvgMark:F2}");
        }
    }
    //КОЛЛЕКЦИИ
    class Group
    {
        public List<Student> Students = new List<Student>();

        public void Show()
        {
            foreach (var student in Students)
            {
                student.Show();
            }

            Console.WriteLine($" Средний балл группы: {AvgAll():F2}");
        }

        private double AvgAll()
        {
            double avg = 0;
            foreach (var student in Students)
            {
                avg += student.AvgMark;
            }

            return avg / Students.Count;
        }
    }

    //EXTENSIONS
    public static class StringExtensions
    {
        public static string ToPriceString(this decimal amount)
        {
            string amountStr = Math.Round(amount, 2).ToString();
            var result = amountStr.Split(",");
            return $"{result[0]}$ {result[1]} cents";

        }
        public static string Capitalize(this string line)
        {
            return string.Join(" ",
                line.Split(" ").Where(s => !String.IsNullOrEmpty(s)).Select(word => char.ToUpper(word[0]) + word[1..]));

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //LINQ
            int[] array = new int[] { 1, 8, 9, 12, 3, 15 };
            string str = "Programming";
            char[] vs = new char[] { 'a', 'e', 'u', 'y', 'i', 'o' };

            //Найти 3 самых больших значений в массиве
            var resultA = array.OrderByDescending(a => a).Take(3);

            //Исключить из массива 2 наименьшие значения - то есть вернуть массив без 2 наименьших значений
            var resultB = array.OrderBy(b => b).Skip(2);

            //Вернуть массив сумм четных и нечетных значений массива
            var resultC = array.GroupBy(x => x % 2).Select(x => x.Sum()).ToList();

            //Подсчитать среднее значение для массива, исключая максимальное и минимальное число
            var resultD = array.OrderBy(d => d).Skip(1).OrderByDescending(d => d).Skip(1).Average();

            //Из строки удалить все гласные
            var resultE = str.Where(e => !vs.Contains(e)).ToArray();

            //Из исходного списка чисел составить новый список, в котором четные числа возведены во вторую степень, а нечетные - в третью
            var resultF = array.GroupBy(x => new { IsEven = (x % 2 == 0), Value = x }).Select(x => x.Key.IsEven ? Math.Pow(x.Key.Value, 2) : Math.Pow(x.Key.Value, 3)).ToList();


            //КОЛЛЕКЦИИ
            Student student1 = new Student();
            Student student2 = new Student();
            Student student3 = new Student();
            Group group = new Group();
            group.Students.AddRange(new[] { student1, student2, student3 });
            group.Show();

            //ПЕРЕГРУЗКА ОПЕРАТОРА
            My_String c1 = new My_String { Country = "Республика" };
            My_String c2 = new My_String { Country = " Беларусь" };

            My_String c3 = c1 + c2;
            Console.WriteLine(c3.Country);
            My_String word = new My_String();
            word.Enter();
            Console.ReadKey();

            //ИСПОЛЬЗОВАНИЕ СТЕКА И ОЧЕРЕДИ
            Stack<Student1> students = new Stack<Student1>();
            students.Push(new Student1() { Name = "Tom", Work = true });
            students.Push(new Student1() { Name = "Bill", Work = true });
            students.Push(new Student1() { Name = "John", Work = false });
            students.Push(new Student1() { Name = "Olya", Work = true });


            Console.WriteLine("Stack\n");

            var listOfStudent = new List<Student1>();
            for (int i = 0; i < 4; i++)
            {
                Student1 student = students.Pop();

                if (student.Work == true)
                {
                    Console.WriteLine($"Task from {student.Name} is received");
                    listOfStudent.Add(student);
                }
            }
            foreach (var allStuedent in listOfStudent)
            {
                Console.WriteLine($"{allStuedent.Name} got a cup of coffee");
            }

            Queue<Student1> pupils = new Queue<Student1>();
            pupils.Enqueue(new Student1() { Name = "Tom", Work = true });
            pupils.Enqueue(new Student1() { Name = "Bill", Work = true });
            pupils.Enqueue(new Student1() { Name = "John", Work = false });
            pupils.Enqueue(new Student1() { Name = "Olya", Work = true });

            Console.WriteLine("\nQueue\n");

            var listOfPupils = new List<Student1>();
            for (int i = 0; i < 4; i++)
            {
                Student1 pupil = pupils.Dequeue();

                if (pupil.Work == true)
                {
                    Console.WriteLine($"Task from {pupil.Name} is received");
                    listOfPupils.Add(pupil);
                }
            }
            foreach (var allPupils in listOfPupils)
            {
                Console.WriteLine($"{allPupils.Name} got a cup of coffee");
            }

            //РАБОТА С МАССИВОМ
            int[] mas = new int[8];

            for (int i = 0; i < 8; i++)
            {
                Console.Write("Введите {0} элемент массива: ", i.ToString());
                mas[i] = Convert.ToInt32(Console.ReadLine());
            }

            bool flag = false; //если есть отрицательные = true, если нет отрицательных = false
            for (int i = 8 - 1; i >= 0; i--)
            {
                if (mas[i] < 0)
                {
                    Console.WriteLine($"Индекс последнего отрицательного элемента {i.ToString()}\n" + $"Значение{mas[i]}");
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                Console.WriteLine("Нет отрицательных значений");
            }

            //EXTENSIONS
            decimal amount = 1111.65466m;
            Console.WriteLine(amount.ToPriceString());

            string line = "hello olya";
            Console.WriteLine(str.Capitalize());

            //ЗАДАНИЕ:ЗАМЕНИТЬ ОТРИЦАТЕЛЬНЫЕ ЗНАЧЕНИЯ ПОЛОЖИТЕЛЬНЫМИ
            List<int> digits = new List<int>() { 1, 2, 4, -5, 8, 9, 8, -11 };
            List<int> result = digits.Select(d => d < 0 ? -d : d).ToList();

            foreach (var r in digits)
            {
                if (r < 0)
                {
                    var res = r * -1;
                    result.Add(res);
                }
                else
                {
                    result.Add(r);
                }
            }
            Console.WriteLine(string.Join(", ", result));

            foreach (var r in digits)
            {
                result.Add(r < 0 ? r * -1 : r);
            }
            Console.WriteLine(string.Join(", ", result));
        }
    }
}
