using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task_22
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите размерность массива:");
            int n = Convert.ToInt32(Console.ReadLine()); //ввод с консоли размерность массива

            Func<object, int[]> func1 = new Func <object, int[]>(GetArray); //создание делегата для task1 (создание массива случ.чисел)
            Task<int[]> task1 = new Task<int[]>(func1, n);//стартовая задача на создание массива

            Action<Task<int[]>> action1 = new Action<Task<int[]>>(PrintArray); //создание делегата для task2 (вывод массива случ.чисел на консоль)
            Task task2 = task1.ContinueWith(action1); //задача продолжения задачи 1 - вывод массива
            
            Func<Task<int[]>, int> func2 = new Func<Task<int[]>, int>(SumArray); //создание делегата для task3 (метод суммы чисел массива)
            Task<int> task3 = task1.ContinueWith<int>(func2); //задача продолжения задачи 1 - нахождение суммы чисел

            Action<Task<int>> action2 = new Action<Task<int>>(PrintResault); //создание делегата для task4 (вывод суммы чисел массива)
            Task task4 = task3.ContinueWith(action2); //задача продолжения задачи 3 - вывод результата

            Func<Task<int[]>, int> func3 = new Func<Task<int[]>, int>(MaxArray); //создание делегата для task5 (метод макс.числа в массиве)
            Task<int> task5 = task1.ContinueWith<int>(func3); //задача продолжения задачи 1 - нахождение макс. числа

            Action<Task<int>> action3 = new Action<Task<int>>(PrintResault); //создание делегата для task6 (вывод макс.числа в массиве)
            Task task6 = task5.ContinueWith(action3); //задача продолжения задачи 5 - вывод результата

            task1.Start(); //запуск задачи 1
           

            Console.ReadKey();
        }

        static int[] GetArray(object a) //метод создания массиваслучайных чисел
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 50);
            }
            return array;
        }

        static int MaxArray(Task<int[]> task) //метод нахождения максимального числа в массиве
        {
            int[] array = task.Result;
            int max = array[0];
            foreach (int a in array)
            {
                if (a > max)
                    max = a;
            }
            Thread.Sleep(900);
            return max;
        }

        static int SumArray(Task<int[]> task) //метод нахождения суммы чисел в массиве
        {
            int[] array = task.Result;
            int S = 0;
            for (int i = 0; i < array.Count(); i++)
            {
                S += array[i];
            }
            Thread.Sleep(700);
            return S;
            
            
        }
        static void PrintArray(Task<int[]> task) //метод вывода массива
        {
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
            Console.WriteLine();
        }
        static void PrintResault(Task<int> task) //метод вывода результатов
        {
            Console.WriteLine($"{task.Result}");
        }

    }
}


