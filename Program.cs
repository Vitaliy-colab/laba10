using System;
class MatrixProcessor
{
    private int[,] matrix;
    private int[] columnCharacteristics;
    
    public int RowCount { get; private set; }
    public int ColCount { get; private set; }

    public int[] ColumnCharacteristics
    {
        get
        {
            if (columnCharacteristics == null)
                ComputeColumnCharacteristics();
            return columnCharacteristics;
        }
    }

    public int this[int i, int j]
    {
        get
        {
            if (i < RowCount && i >= 0 && j < ColCount && j >= 0)
                return matrix[i, j];
            else
                throw new IndexOutOfRangeException("Індекс виходить за межі масиву!");
        }
        set
        {
            if (i < RowCount && i >= 0 && j < ColCount && j >= 0)
                matrix[i, j] = value;
            else
                throw new IndexOutOfRangeException("Індекс виходить за межі масиву!");
        }
    }

    public MatrixProcessor(int rows, int cols)
    {
        matrix = new int[rows, cols];
        RowCount = rows;
        ColCount = cols;
    }

    public void FillRandom(int min, int max)
    {
        Random rand = new Random();
        for (int i = 0; i < RowCount; i++)
            for (int j = 0; j < ColCount; j++)
                matrix[i, j] = rand.Next(min, max + 1);
    }

    public void ComputeColumnCharacteristics()
    {
        columnCharacteristics = new int[ColCount];
        for (int j = 0; j < ColCount; j++)
        {
            for (int i = 0; i < RowCount; i++)
            {
                int element = matrix[i, j];
                if (element < 0 && element % 2 != 0)
                {
                    columnCharacteristics[j] += Math.Abs(element);
                }
            }
        }
    }

    public void SortColumnsByCharacteristic()
    {
        if (columnCharacteristics == null)
            ComputeColumnCharacteristics();

        for (int i = 0; i < ColCount - 1; i++)
        {
            for (int j = i + 1; j < ColCount; j++)
            {
                if (columnCharacteristics[i] > columnCharacteristics[j])
                {
                    for (int k = 0; k < RowCount; k++)
                    {
                        Swap(ref matrix[k, i], ref matrix[k, j]);
                    }
                    Swap(ref columnCharacteristics[i], ref columnCharacteristics[j]);
                }
            }
        }
    }

    public int SumColumnsWithNegatives()
    {
        int totalSum = 0;
        for (int j = 0; j < ColCount; j++)
        {
            bool hasNegative = false;
            int columnSum = 0;

            for (int i = 0; i < RowCount; i++)
            {
                columnSum += matrix[i, j];
                if (matrix[i, j] < 0)
                    hasNegative = true;
            }

            if (hasNegative)
                totalSum += columnSum;
        }
        return totalSum;
    }

    public void PrintMatrix()
    {
        for (int i = 0; i < RowCount; i++)
        {
            for (int j = 0; j < ColCount; j++)
            {
                Console.Write($"{matrix[i, j],5}");
            }
            Console.WriteLine();
        }
    }


    public void PrintCharacteristics()
    {
        Console.WriteLine("\nХарактеристики стовпців:");
        for (int j = 0; j < ColCount; j++)
        {
            Console.Write($"{ColumnCharacteristics[j],5}");
        }
        Console.WriteLine();
    }

    public void Swap(ref int a, ref int b)
    {
        a = a + b;
        b = a - b;
        a = a - b;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Робота з матрицею за варіантом завдання");

        Console.Write("Введіть кількість рядків: ");
        int rows = int.Parse(Console.ReadLine());
        Console.Write("Введіть кількість стовпців: ");
        int cols = int.Parse(Console.ReadLine());

        MatrixProcessor matrix = new MatrixProcessor(rows, cols);

        Console.Write("Введіть мінімальне значення: ");
        int min = int.Parse(Console.ReadLine());
        Console.Write("Введіть максимальне значення: ");
        int max = int.Parse(Console.ReadLine());

        matrix.FillRandom(min, max);

        Console.WriteLine("\nПочаткова матриця:");
        matrix.PrintMatrix();
        matrix.PrintCharacteristics();

        matrix.SortColumnsByCharacteristic();
        Console.WriteLine("\nМатриця після сортування стовпців:");
        matrix.PrintMatrix();
        matrix.PrintCharacteristics();

        int sum = matrix.SumColumnsWithNegatives();
        Console.WriteLine($"\nСума елементів у стовпцях з від'ємними елементами: {sum}");

        Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
        Console.ReadKey();
    }
}