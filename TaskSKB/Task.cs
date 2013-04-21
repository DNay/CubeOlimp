using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSKB
{
    class Task
    {
        public Cube BigCube;
        public List<string> RotateMap = new List<string>();//карта поворотов
        public List<Cube> SmallCube = new List<Cube>(); //список с исходными данными
        public List<int> ListWidth = new List<int>(); //список размеров
        public List<int> ListDepth = new List<int>();
        public List<int> ListHeight = new List<int>();
        //public List<int> IndexCornres = new List<int>();//список индексов углов в списке SmallCube 
        public bool[,,] BoolFrame; //каркас размеров с флагами заполнения

        public int Capacity; //количество кусков в кубе

        public Task()
        {

        }

        public void WriteAnswer()
        {
            for (int i = 0; i < SmallCube.Count; i++)
            {
                var l = SmallCube[i];
                Console.WriteLine(l.Faces[0] + ' ' + l.Faces[2] + ' ' + l.Pos[0] + ' ' + l.Pos[1] + ' ' + l.Pos[2]);
            }
        }

        public void ReadIn() //считали кубики, пополнили списки
        {
            var str = Console.ReadLine();
            BigCube = new Cube(str);

            str = Console.ReadLine();
            Capacity = int.Parse(str);

            for (int i = 0; i < Capacity; i++) 
            {
                str = Console.ReadLine();
                SmallCube.Add(new Cube(str));
            }
        }

        public string Reindex(string colorstr, int []ind)
        {
            string result = "";
            for (int i = 0; i < 6; i++)
            {
                result = result + colorstr[ind[i]];
            }
            return result;
        }

        public void BuildBoolFrame() //строим массив булевских нулей для заполнения
        {
            ListDepth.Sort();
            ListHeight.Sort();
            ListWidth.Sort();
            BoolFrame = new bool[ListWidth.Count, ListDepth.Count, ListHeight.Count];
        }

        public int CountChar(string str, char sym)
        {
            int result = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == sym)
                    result++;
            }
            return result;
        }

        public int RecognizeType(string str)
        {
            switch (CountChar(str, '.'))
            {
                case 6:
                    return 4; //внутренний куб
                case 5:
                    return 3; //грань
                case 1:
                    return 1; //угол
                case 0:
                    return 1; //целый куб, считаем как угол
                case 2:
                    if (str[0] == str[1] || str[2] == str[3] || str[4] == str[5])
                        return 2;
                    else
                        return 1;
                case 3:
                    if (str[0] == str[1] || str[2] == str[3] || str[4] == str[5])
                        return 2;
                    else
                        return 1;
                case 4:
                    if ((str[0] == str[1] && str[2] == str[3]) || (str[0] == str[1] && str[4] == str[5])
                        || (str[2] == str[3] && str[4] == str[5]))
                        return 3;
                    else
                        return 2;
                default:
                    Console.WriteLine("type error");
                    return -1;
            }
        }

        public void GetRotateMap(string str)
        {
            RotateMap.Add(Reindex(str, new int[] { 0, 1, 2, 3, 4, 5 }));
            RotateMap.Add(Reindex(str, new int[] { 0, 1, 4, 5, 3, 2 }));
            RotateMap.Add(Reindex(str, new int[] { 0, 1, 3, 2, 5, 4 }));
            RotateMap.Add(Reindex(str, new int[] { 0, 1, 5, 4, 2, 3 }));
            RotateMap.Add(Reindex(str, new int[] { 1, 0, 2, 3, 4, 5 }));
            RotateMap.Add(Reindex(str, new int[] { 1, 0, 5, 4, 3, 2 }));
            RotateMap.Add(Reindex(str, new int[] { 1, 0, 3, 2, 4, 5 }));
            RotateMap.Add(Reindex(str, new int[] { 1, 0, 4, 5, 2, 3 }));
            RotateMap.Add(Reindex(str, new int[] { 5, 4, 2, 3, 1, 0 }));
            RotateMap.Add(Reindex(str, new int[] { 5, 4, 0, 1, 3, 2 }));
            RotateMap.Add(Reindex(str, new int[] { 5, 4, 3, 2, 1, 0 }));
            RotateMap.Add(Reindex(str, new int[] { 5, 4, 1, 0, 2, 3 }));
            RotateMap.Add(Reindex(str, new int[] { 4, 5, 2, 3, 1, 0 }));
            RotateMap.Add(Reindex(str, new int[] { 4, 5, 1, 0, 3, 2 }));
            RotateMap.Add(Reindex(str, new int[] { 4, 5, 3, 2, 0, 1 }));
            RotateMap.Add(Reindex(str, new int[] { 4, 5, 0, 1, 2, 3 }));
            RotateMap.Add(Reindex(str, new int[] { 3, 2, 0, 1, 4, 5 }));
            RotateMap.Add(Reindex(str, new int[] { 3, 2, 5, 4, 0, 1 }));
            RotateMap.Add(Reindex(str, new int[] { 3, 2, 1, 0, 5, 4 }));
            RotateMap.Add(Reindex(str, new int[] { 3, 2, 4, 5, 1, 0 }));
            RotateMap.Add(Reindex(str, new int[] { 2, 3, 1, 0, 4, 5 }));
            RotateMap.Add(Reindex(str, new int[] { 2, 3, 5, 4, 1, 0 }));
            RotateMap.Add(Reindex(str, new int[] { 2, 3, 0, 1, 4, 5 }));
            RotateMap.Add(Reindex(str, new int[] { 2, 3, 4, 5, 0, 1 }));
            return;
        }
        
    }   
}



public class Cube //в узле храним тип кубика, объемы, цвет передней грани, и его позицию (нахуя?)
{
    public int Type = 0; //тип кубика
    public string Colors = "UUUUUU"; //цвета граней
    public string Faces = "FBDURL";
    public int []Dim = new int[3] {0, 0, 0};
    public int []Pos = new int[3] {0, 0, 0};

    public Cube(int type, int []dim, string colors)
    {
        Type = type;
        Dim  = dim;
        Colors = colors;
    }

    public Cube(string foo)
    {
        var strs = foo.Split(' ');
        Dim[0] = int.Parse(strs[0]);
        Dim[1] = int.Parse(strs[1]);
        Dim[2] = int.Parse(strs[2]);
        Colors = strs[3];
        Type = GetCubeType();
    }

    public void Rotate(int[] ind)
    {
        string resultc = "";
        string resultf = "";
        for (int i = 0; i < 6; i++)
        {
            resultc = resultc + Colors[ind[i]];
            resultf = resultf +  Faces[ind[i]];
        }
        Colors = resultc;
        Faces  = resultf;

        var dimTemp = new int[3] {0, 0, 0};
        for (int i = 0; i < 3; i++)
        {
            dimTemp[i] = Dim[(ind[i*2] - 1) / 2];
        }
        Dim = dimTemp;
    }


    private int GetCubeType()
    {
        return 0;
    }
}
