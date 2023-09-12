using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTest1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int num_count = 38; // 要生成的随机数数量
            //int min_range = 1;  // 随机数范围最小值
            //int max_range = 81; // 随机数范围最大值
            int rangeTotal = 81;

            //if (num_count > max_range - min_range + 1)
            if (num_count > rangeTotal)
            {
                Console.WriteLine("无法生成不重複的數字，因為範圍不足。\n");
            }

            int[] generated_nums = new int[num_count]; // 用于存储生成的随机数
            string[,] nums = new string[9, 9]; // 用于存储最终结果，初始化为0
            string[,] nums2 = new string[9, 9]; // 用于存储最终结果，初始化为0
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    nums[i,j] = "□";
                    nums2[i,j] = "□";
                }
            }

            // 初始化随机数生成器
            //srand(time(NULL));

            int index = 0;
            Random rnd = new Random();
            while (index < num_count)
            {
                //1-81
                //隨機取0-80範圍內的值 AND +1
                int new_num = rnd.Next(1,80);
                bool is_duplicate = false;

                // 检查新生成的数是否已经存在于数组中
                for (int i = 0; i < index; i++)
                {
                    if (generated_nums[i] == new_num)
                    {
                        is_duplicate = true;
                        break;
                    }
                }

                // 如果不是重复的数，则将其存储在数组中
                if (!is_duplicate)
                {
                    generated_nums[index] = new_num;
                    index++;
                }
            }                       

            // 将生成的随机数数组从小到大排序
            insertionSort(generated_nums, num_count);

            //生成9*9的1-9(行列不重複)
            int[,] ints = new int[9, 9];
            for (int item = 0; item < ints.GetLength(0); item++)
            {
                int[] oneToNine = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                int result = 0;
                while (result < ints.GetLength(1))
                {
                    int number = rnd.Next(1, 10);
                    bool num_repeat = numberOfOneToNine(number, ints, result, item);
                    oneToNine = findNumbers(oneToNine, number, ints, result, item);
                    if (num_repeat)
                    {
                        ints[result, item] = number;
                        result++;
                    }
                    else if (oneToNine.Length == 0)
                    {
                        oneToNine = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        for (int i = 0; i < result; i++)
                        {
                            ints[i, item] = 0;
                        }
                        result = 0;

                    }
                }
            }

            // 在nums数组中对应位置设置为1
            for (int i = 0; i < num_count; i++)
            {
                int row = (generated_nums[i] - 1) / 9;
                int col = (generated_nums[i] - 1) % 9;
                nums[row, col] = $"{ints[row, col]} ";
                nums2[row, col] = "■";
            }

            //計算nums各行列總數
            int[,] RowColTotal = new int[2,9];
            for(int i=0; i<9; i++)
            {
                int rows = 0;
                int cols = 0;
                for(int j=0; j<9; j++)
                {
                    if(nums2[j, i] == "■")
                    {
                        rows++;
                    }
                    if(nums2[i, j] == "■")
                    {
                        cols++;
                    }
                }
                RowColTotal[0,i] = rows;
                RowColTotal[1,i] = cols;
            }

            // 打印生成的随机数数组
            Console.WriteLine("生成的隨機數字：\n");
            for (int i = 0; i < num_count; i++)
            {
                Console.Write($"{generated_nums[i]} ");
            }
            Console.WriteLine("\n");

            // 打印nums数组
            Console.WriteLine("nums陣列內容：\n");
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($"  {nums[i, j]}");
                }
                Console.WriteLine("\n");
            }

            // 打印9*9各行列總數
            Console.WriteLine("nums陣列各行列總數：\n");
            for (int i = 0; i < RowColTotal.GetLength(1); i++)
            {
                Console.Write($"   {RowColTotal[0, i]}");
            }
            Console.WriteLine("\n");
            for (int i = 0; i < 9; i++)
            {
                Console.Write($"{RowColTotal[1,i]}");
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($"  {nums2[i, j]}");
                }
                Console.WriteLine("\n");
            }

            Console.ReadLine();

        }
        public static void insertionSort(int[] arr, int n)
        {
            for (int i = 1; i < n; i++)
            {
                int key = arr[i];
                int j = i - 1;
                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
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
                        return nums = nums.Where(x => x != num).ToArray();
                    }
                }
            }
            return nums;
        }
    }
}
