using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskSKB
{
    class Task
    {
        public List<Node> SmallCube = new List<Node>(); //список с исходными данными
        public List<int> ListWidth = new List<int>(); //список размеров
        public List<int> ListDepth = new List<int>();
        public List<int> ListHeight = new List<int>();
        public List<int> IndexCornres = new List<int>();//список индексов углов в списке SmallCube 
        public bool[,,] BoolFrame; //каркас размеров с флагами заполнения

        public int TypeOfCube = -1; //0, 1, 2, 3, 4, 5, 6, 7
        public int Capacity; //количество кусков в кубе

        public char Front = ' '; //цвета граней исходного куба
        public char Back = ' ';
        public char Bottom = ' ';
        public char Top = ' ';
        public char Left = ' ';
        public char Right = ' ';

        public int Width = -1; //размеры исходного куба
        public int Depth = -1;
        public int Height = -1;

        public int ThreeCorner; //количество типов маленьких кубиков //НАХУЯ?
        public int BiEdge;
        public int OnlyFace;
        public int Inside;
        public int TetraCorner;
        public int ThreeEdge;
        public int BiFace;
        public int PentaCorner;
        public int TetraEdge;
        public int Hex;
        
        public Task()
        {

        }

        public class  Node //в узле храним тип кубика, объемы, цвет передней грани, и его позицию (нахуя?)
        {
            public string Map;
            public char FrontColor;
            public char BottomColor;
            public int ListType; //тип кубика
            public char First; //передняя грань
            public char Second;//нижняя грань
            public int []Volume = new int[2];
            public int []Position = new int[2];

            public Node(int type, int []vol, string map)
            {
                ListType = type;
                Volume = vol;
                Position = null;
                Map = map;
            }

            public Node()
            {
                ListType = -1;
                First = ' ';
                Second = ' ';
                Volume = null;
                Position = null;
                Map = null;
            }
        }

        public void ReadIn() //считали кубики, пополнили списки
        {
            var str = Console.ReadLine();
            FirstRead(str);
            var strn = Console.ReadLine();
            Capacity = int.Parse(strn);
            for (int i = 0; i < Capacity; i++)
            {
                var strloop = Console.ReadLine();
                AddCube(strloop);
            }
            SetType();

            if (TypeOfCube == 0) //если кубик один
            {
                var l = SmallCube[0];
                char first = ' ';
                char second = ' ';
                switch (l.Map.IndexOf(Front))
                {
                    case 0:
                        first = 'F';
                        break;
                    case 1:
                        first = 'B';
                        break;
                    case 2:
                        first = 'D';
                        break;
                    case 3:
                        first = 'U';
                        break;
                    case 4:
                        first = 'L';
                        break;
                    case 5:
                        first = 'R';
                        break;
                }

                switch (l.Map.IndexOf(Bottom))
                {
                    case 0:
                        second = 'F';
                        break;
                    case 1:
                        second = 'B';
                        break;
                    case 2:
                        second = 'D';
                        break;
                    case 3:
                        second = 'U';
                        break;
                    case 4:
                        second = 'L';
                        break;
                    case 5:
                        second = 'R';
                        break;
                }
                Console.WriteLine(first + ' ' + second + " 0 0 0");
            }
                
            if (TypeOfCube == 2)
            {
                int vector = -1;//направление роста
                char first = ' ';
                char second = ' ';
                int length = 0;
                //TODO сначала найти начальную вершину, сделать длину, после обрабатывать "тело" змеи
                var j = IndexCornres[0];
                var t = SmallCube[j];//нашли один из углов куба
                if ((!t.Map.Contains(Right) || !t.Map.Contains(Back) || !t.Map.Contains(Top)))
                {
                }
                else
                {
                    t = SmallCube[IndexCornres[1]];
                }

                for (int i = 0; i < 3; i++) //ищем координаты роста куба
                {
                    if ((t.Volume[i] != Width && t.Volume[i] != Height && t.Volume[i] != Depth))
                    {
                        length = t.Volume[i];
                        break;
                    }
                }

                if (t.Map.Contains(Right) == false)
                    vector = 1;
                else
                {
                    if (t.Map.Contains(Back) == false)
                        vector = 0;
                    else
                    {
                        if (t.Map.Contains(Top) == false)
                            vector = 2;
                    }
                }
            

            for (int i = 0; i < SmallCube.Count; i++) //выводим, проходя по всему списку
            {
                var l = SmallCube[i];
                if (l.ListType == 7) //если кубик - угол
                {
                    if (l.Equals(t))
                    {
                        switch (l.Map.IndexOf(Front))
                        {
                            case 0:
                                first = 'F';
                                break;
                            case 1:
                                first = 'B';
                                break;
                            case 2:
                                first = 'D';
                                break;
                            case 3:
                                first = 'U';
                                break;
                            case 4:
                                first = 'L';
                                break;
                            case 5:
                                first = 'R';
                                break;
                        }
                        switch (l.Map.IndexOf(Bottom))
                        {
                            case 0:
                                second = 'F';
                                break;
                            case 1:
                                second = 'B';
                                break;
                            case 2:
                                second = 'D';
                                break;
                            case 3:
                                second = 'U';
                                break;
                            case 4:
                                second = 'L';
                                break;
                            case 5:
                                second = 'R';
                                break;
                        }
                        Console.WriteLine(first + ' ' + second + " 0 0 0");
                    }
                    else //если угол крайний
                    {
                        string outkoord = "";
                        int koord = 0;
                        for (int k = 0; k < 3; k++) //ищем координату
                        {
                            if ((l.Volume[k] != Width && l.Volume[k] != Height && l.Volume[k] != Depth))
                            {
                                koord = l.Volume[k];
                                break;
                            }
                        }

                        if (l.Map.Contains(Front) == false)
                        {
                            outkoord = (Width - koord).ToString() + " 0 0";
                            switch (l.Map.IndexOf('.'))
                            {
                                case 0:
                                    first = 'F';
                                    break;
                                case 1:
                                    first = 'B';
                                    break;
                                case 2:
                                    first = 'D';
                                    break;
                                case 3:
                                    first = 'U';
                                    break;
                                case 4:
                                    first = 'L';
                                    break;
                                case 5:
                                    first = 'R';
                                    break;
                            }
                            switch (l.Map.IndexOf(Bottom))
                            {
                                case 0:
                                    second = 'F';
                                    break;
                                case 1:
                                    second = 'B';
                                    break;
                                case 2:
                                    second = 'D';
                                    break;
                                case 3:
                                    second = 'U';
                                    break;
                                case 4:
                                    second = 'L';
                                    break;
                                case 5:
                                    second = 'R';
                                    break;
                            }
                        }

                        if (l.Map.Contains(Left) == false)
                        {
                            outkoord = "0 0 " + (Height - koord).ToString();
                            switch (l.Map.IndexOf(Front))
                            {
                                case 0:
                                    first = 'F';
                                    break;
                                case 1:
                                    first = 'B';
                                    break;
                                case 2:
                                    first = 'D';
                                    break;
                                case 3:
                                    first = 'U';
                                    break;
                                case 4:
                                    first = 'L';
                                    break;
                                case 5:
                                    first = 'R';
                                    break;
                            }
                            switch (l.Map.IndexOf(Bottom))
                            {
                                case 0:
                                    second = 'F';
                                    break;
                                case 1:
                                    second = 'B';
                                    break;
                                case 2:
                                    second = 'D';
                                    break;
                                case 3:
                                    second = 'U';
                                    break;
                                case 4:
                                    second = 'L';
                                    break;
                                case 5:
                                    second = 'R';
                                    break;
                            }
                        }

                        if (l.Map.Contains(Bottom) == false)
                        {
                            switch (l.Map.IndexOf(Front))
                            {
                                case 0:
                                    first = 'F';
                                    break;
                                case 1:
                                    first = 'B'; 
                                    break;
                                case 2:
                                    first = 'D';
                                    break;
                                case 3:
                                    first = 'U';
                                    break;
                                case 4:
                                    first = 'L';
                                    break;
                                case 5:
                                    first = 'R';
                                    break;
                            }
                        }
                        Console.WriteLine(first + ' ' + second + ' ' + outkoord);
                    }

                }
                else
                {
                    //TODO: доделать середину змеи
                   
                }
                }
            }
          
            
        }








        public void BuildBoolFrame() //строим массив булевских нулей для заполнения
        {
            ListDepth.Sort();
            ListHeight.Sort();
            ListWidth.Sort();
            BoolFrame = new bool[ListWidth.Count, ListDepth.Count, ListHeight.Count];
        }

        public void AddCube(string all) //обработка строки в кубик
        {
            var strs = all.Split(' ');
            string inColor = strs[3]; //получили раскраску
            int recognizedType = Recognize(ToBinary(inColor)); //распознали тип
            int[] volume = {int.Parse(strs[0]), int.Parse(strs[1]), int.Parse(strs[2])}; //считали координаты
            SmallCube.Add(new Node(recognizedType, volume, inColor));//внесли в список
           

            if (recognizedType == 5) //если ребро тонкого куба
            {
                if ((inColor.Contains(Bottom)) && (inColor.Contains(Front)))
                {
                    if ((recognizedType == 11) || (recognizedType == 14) || (recognizedType == 7) || (recognizedType == 13))
                    {
                        ListHeight.Add(int.Parse(strs[0]));
                    }
                    if ((recognizedType == 28) || (recognizedType == 52) || (recognizedType == 44) || (recognizedType == 56))
                    {
                        ListHeight.Add(int.Parse(strs[2]));
                    }
                    if ((recognizedType == 49) || (recognizedType == 50) || (recognizedType == 35) || (recognizedType == 19))
                    {
                        ListHeight.Add(int.Parse(strs[1]));
                    }
                }
                if ((inColor.Contains(Left)) && (inColor.Contains(Front)))
                {
                    if ((recognizedType == 11) || (recognizedType == 14) || (recognizedType == 7) || (recognizedType == 13))
                    {
                        ListDepth.Add(int.Parse(strs[0]));
                    }
                    if ((recognizedType == 28) || (recognizedType == 52) || (recognizedType == 44) || (recognizedType == 56))
                    {
                        ListDepth.Add(int.Parse(strs[2]));
                    }
                    if ((recognizedType == 49) || (recognizedType == 50) || (recognizedType == 35) || (recognizedType == 19)) 
                    {
                        ListDepth.Add(int.Parse(strs[1]));
                    }
                }

                if((inColor.Contains(Bottom)) && (inColor.Contains(Left)))
                {
                    if ((recognizedType == 11) || (recognizedType == 14) || (recognizedType == 7) || (recognizedType == 13))
                    {
                        ListWidth.Add(int.Parse(strs[0]));
                    }
                    if ((recognizedType == 28) || (recognizedType == 52) || (recognizedType == 44) || (recognizedType == 56))
                    {
                        ListWidth.Add(int.Parse(strs[2]));
                    }
                    if ((recognizedType == 49) || (recognizedType == 50) || (recognizedType == 35) || (recognizedType == 19)) 
                    {
                        ListWidth.Add(int.Parse(strs[1]));
                    }
                }
                
            }

            if (recognizedType == 2) //если у нас ребро куба, то его нужно обработать отдельно
               //ребра куба представляют собой каркас, в котором указаны размеры
           {
               if ((inColor.Contains(Bottom)) && (inColor.Contains(Front))) //если нашли нижнее переднее ребро
               {
                   if ((recognizedType == 40) || (recognizedType == 24) || (recognizedType == 20) || (recognizedType == 36))
                   {
                       ListHeight.Add(int.Parse(strs[2]));
                   }

                   if ((recognizedType == 10) || (recognizedType == 9) || (recognizedType == 6) || (recognizedType == 5))
                   {
                       ListHeight.Add(int.Parse(strs[0]));
                   }

                   else
                   {
                       ListHeight.Add(int.Parse(strs[1]));
                   }
               }
               
               if ((inColor.Contains(Front)) && (inColor.Contains(Left))) //верхнее левое
               {
                   if ((recognizedType == 40) || (recognizedType == 24) || (recognizedType == 20) || (recognizedType == 36))
                   {
                       ListDepth.Add(int.Parse(strs[2]));
                   }

                   if ((recognizedType == 10) || (recognizedType == 9) || (recognizedType == 6) || (recognizedType == 5))
                   {
                       ListDepth.Add(int.Parse(strs[0]));
                   }

                   else
                   {
                       ListDepth.Add(int.Parse(strs[1]));
                   }
               }

               if ((inColor.Contains(Bottom)) && (inColor.Contains(Left))) //нижнее левое
               {
                   if ((recognizedType == 40) || (recognizedType == 24) || (recognizedType == 20) || (recognizedType == 36))
                   {
                       ListWidth.Add(int.Parse(strs[2]));
                   }

                   if ((recognizedType == 10) || (recognizedType == 9) || (recognizedType == 6) || (recognizedType == 5))
                   {
                       ListWidth.Add(int.Parse(strs[0]));
                   }

                   else
                   {
                       ListWidth.Add(int.Parse(strs[1]));
                   }
               }
           }//все, у нас есть три  размерности
           // if((TypeOfCube == 0) || (TypeOfCube == 4) || (TypeOfCube == 7))
             //   IndexCornres.Add(SmallCube.Count);//составляем список расположений вершин в листе
        }
      
        public void SetType() //устанавливаем тип большого куба
        {
            if (TypeOfCube != -1) //уже установили
                return;

            if (Capacity == 1)
            {
                TypeOfCube = 0;//единичный кубик
                return;
            }

            if (TetraCorner == 4)//тонкий
            {
                TypeOfCube = 1;
                return;
            }

            if (PentaCorner == 2)//змея
            {
                TypeOfCube = 2;
                return;
            }

            if (ThreeCorner == 8)
            {
                TypeOfCube = 3;
                return;
            }
        }

        public int ToBinary(string par) //"хэширование" развертки для определения типа
        {
            int result = 0;

            for (int i = 5; i > -1; i--)
            {
                if (par[i] != '.')
                    result = result + (int)Math.Pow(2, i);
            }

            return result;
        }

        public int Recognize(int par) //передаем число, по которому определяем тип кубика
        {
            switch (par)
            {
                case 41:
                case 42:
                case 25:
                case 26:
                case 22:
                case 21:
                case 38:
                case 37:
                    return 0; //угол в обычном кубе
                case 40:
                case 24:
                case 20:
                case 36:
                case 10:
                case 9:
                case 34:
                case 33:
                case 18:
                case 17:
                case 6:
                case 5:
                    return 1; //ребро в обычном кубе
                case 32:
                case 16:
                case 8:
                case 4:
                case 2:
                case 1:
                    return 2;//грань в обычном кубе
                case 0:
                    return 3; //внутренность
                case 23:
                case 39:
                case 43:
                case 27:
                case 53:
                case 54:
                case 29:
                case 30:
                case 45:
                case 46:
                case 57:
                case 58:
                    return 4; //угол в "тонком" кубе
                case 56:
                case 11:
                case 28:
                case 52:
                case 44: 
                case 14:
                case 7:
                case 13:
                case 49:
                case 50:
                case 35:
                case 19:
                    return 5; //ребро в тонком кубе
                case 48:
                case 12:
                case 3:
                    return 6;//грань в "тонком" кубе
                case 31:
                case 47:
                case 55:
                case 59:
                case 61:
                case 62:
                    return 7;//угол в "змее"
                case 15:
                case 51:
                case 60:
                    return 8;
                case 63:
                    return 9;//кубик
                default:
                    return -1;
            }   
        }

        public void FirstRead(string main) //считываем параметры большого куба
        {
            //2 2 2 ROYGBV
            var strs = main.Split(' ');
         
            try
            {
                Width = int.Parse(strs[0]);
                Depth = int.Parse(strs[1]);
                Height = int.Parse(strs[2]);
                Front = strs[3][0];
                Back = strs[3][1];
                Bottom = strs[3][2];
                Top = strs[3][3];
                Left = strs[3][4];
                Right = strs[3][5];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something is wrong" + ex.Message);
            }
        }
    }   
}
