using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTest2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[,] ints = new int[9,9];
            for (int item = 0; item < ints.GetLength(0); item++)
            {
                int[] nums = new int[] { 1,2,3,4,5,6,7,8,9 };
                int result = 0;
                while (result < ints.GetLength(1))
                {
                    int number = rnd.Next(1, 10);
                    bool num_repeat = numberOfOneToNine(number, ints, result, item);
                    nums = findNumbers( nums,number, ints, result, item);
                    if (num_repeat)
                    {
                        ints[result, item] = number;
                        result++;
                    }
                    else if(nums.Length==0)
                    {
                        nums = new int[] { 1,2,3,4,5,6,7,8,9 };
                        for(int i = 0; i < result; i++)
                        {
                            ints[i, item] = 0;
                        }
                        result = 0;
                        
                    }
                }
            }
            // 打印ints数组
            Console.WriteLine("ints陣列內容：\n");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($"  {ints[i, j]}");
                }
                Console.WriteLine("\n");
            }

            Console.ReadLine();
        }
        public static bool numberOfOneToNine(int num, int[,] ints, int row, int col)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i == row || j == col) && ints[i, j] == num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static int[] findNumbers(int[] nums, int num, int[,] ints, int row, int col)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if ((i == row || j == col) && ints[i, j] == num)
                    {
                        return nums = nums.Where(x=>x!=num).ToArray();
                    }
                }
            }
            return nums;
        }
    }
}
