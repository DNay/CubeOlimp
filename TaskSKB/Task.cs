using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSKB
{
    class Task
    {
        public Cube BigCube;
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
