using System;

namespace SaperCS
{
    public static class Saper
    {
        public static string field = "";
        private static Random rand = new Random();

        public static void CreateField(int width, int height, int BombCount)
        {

            int x, y;
            int[,] intfield = new int[height, width]; ;

            for (int i = 0; i < BombCount; i++)
              intfield[rand.Next(0, height), rand.Next(0, width)] = 9;
              
            
            //intfield[2,1]=9;
            //intfield[4,1]=9;
            //intfield[7,2]=9;
            //intfield[7,4]=9;
            //intfield[6,7]=9;
            //intfield[5,7]=9;
            //intfield[6,9]=9;
            //intfield[4,9]=9;
            //intfield[1,6]=9;
            //intfield[2,8]=9;




            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (intfield[i, j] == 9)
                    {
                        y = i - 1;
                        x = j - 1;
                        if (y >= 0)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                if (x < 0) { x++; continue; }
                                if (intfield[y, x] != 9) intfield[y, x]++;
                                if (x + 1 < width) x++; else break;
                            }
                        }

                        y = i + 1;
                        x = j - 1;
                        if (y < height)
                        {
                            for (int k = 0; k < 3; k++)
                            {
                                if (x < 0) { x++; continue; }
                                if (intfield[y, x] != 9) intfield[y, x]++;
                                if (x + 1 < width) x++; else break;
                            }
                        }

                        y = i;
                        x = j - 1;
                        for (int k = 0; k < 2; k++)
                        {
                            if (x < 0) { x+=2; continue; }
                            if (intfield[y, x] != 9) intfield[y, x]++;
                            if (x + 2 < width) x += 2; else break;
                        }
                    }

            foreach (int block in intfield)
            {
                if (block == 9)
                    field += 'B';
                else if (block == 0)
                    field += " ";
                else
                    field += block;
            }
        }

        public static void Init()
        {
            field = String.Empty;
        }
    }
}
