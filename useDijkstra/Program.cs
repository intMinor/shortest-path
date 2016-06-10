using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shortest_path;
using System.IO;

namespace useDijkstra
{
    class Program
    {
        private struct observation
        {
            public string Be;
            public string En;
            public double S;
        }

        private static observation[] my_observation;

        private static string[] Pname;

        static void Main(string[] args)
        {
            int Hn = 0;
            int Un = 0;
            int Kn = 0;

            string strData = String.Empty;
            string[] strTemp;
            int dataIdx = 0;

            FileStream aFile = new FileStream("示例数据.txt", FileMode.Open);
            StreamReader streamReader = new StreamReader(aFile);

            while (streamReader.Peek() != -1)
            {
                dataIdx++;

                //"strData"存储剔除空格逐行读取文档的当前行所有字符，
                // "strTemp"以数组形式存储当前行，以逗号分隔
                strData = streamReader.ReadLine().Trim();
                strTemp = strData.Trim().Split(',');

                if (dataIdx == 1)
                {
                    Kn = Convert.ToInt16(strTemp[0]);
                    Un = Convert.ToInt16(strTemp[1]);
                    Hn = Convert.ToInt16(strTemp[2]);
                    Pname = new string[Kn + Un];
                    my_observation = new observation[Hn];
                }

                else if (dataIdx == 2)
                {
                    for (int i = 0; i < Kn + Un; i++)
                    {
                        Pname[i] = Convert.ToString(strTemp[i]);
                    }
                }

                else
                {
                    my_observation[dataIdx - 3].Be = Convert.ToString(strTemp[0]);
                    my_observation[dataIdx - 3].En = Convert.ToString(strTemp[1]);
                    my_observation[dataIdx - 3].S = Convert.ToDouble(strTemp[2]);
                }
            }

            Console.WriteLine(" 数据读入成功 ！！！ ");

            Console.WriteLine("/////////////////////////////////////////////////////////////");

            Console.WriteLine(" Hn = {0}, Kn = {1}, Un = {2} ", Hn, Kn, Un);

            Console.WriteLine("/////////////////////////////////////////////////////////////");

            for (int i = 0; i < Kn + Un; i++)
            {
                Console.WriteLine("第 {0} 点 ：{1}", i+1, Pname[i]);
            }

            Console.WriteLine("/////////////////////////////////////////////////////////////");

            for (int i = 0; i < Hn; i++)
            {
                Console.WriteLine("起点 ：{0} 终点 ：{1} 距离 ：{2}", my_observation[i].Be, my_observation[i].En, my_observation[i].S.ToString("0.0000"));
            }

            Console.WriteLine("/////////////////////////////////////////////////////////////");

            string[] singlePath = new string[Kn + Un];
            string[,] mixPath = new string[Kn + Un, Kn + Un];

            double[] singleDistance = new double[Kn + Un];
            double[,] mixDistance = new double[Kn + Un, Kn + Un];

            shortest_path.shortest_path my_shortest_path = new shortest_path.shortest_path(Hn, Kn + Un, Kn, Pname);

            for (int i = 0; i < Hn; i++)
            {
                my_shortest_path.createObservation(i, my_observation[i].Be, my_observation[i].En, my_observation[i].S);
            }

            my_shortest_path.createGraph();

            for (int i = 0; i < Un; i++)
            {
                my_shortest_path.Dijkstra(i + Kn);

                singlePath = my_shortest_path.getmixPath();
                singleDistance = my_shortest_path.getmixDistance();

                for (int j = 0; j < Kn + Un; j++)
                {
                    mixPath[i, j] = singlePath[j];
                    mixDistance[i, j] = singleDistance[j];
                    if (mixDistance[i, j] != 0.0)
                    {
                        Console.Write ("mixPath[{0}]:{1}    ", j, mixPath[i, j]);
                        Console.Write("mixDistance[{0}]:{1}", j, mixDistance[i, j]);

                        Console.Write("\n");
                    }
                } 
                Array.Clear(singlePath, 0, Kn + Un);
                Array.Clear(singleDistance, 0, Kn + Un);
            }

            Console.ReadKey();
            Console.ReadKey();
            Console.ReadKey();
        }
    }
}
