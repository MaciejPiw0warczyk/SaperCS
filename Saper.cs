using System;
using System.Globalization;

namespace SaperCS
{
    public class Saper
    {
        int[,] intfield;
        public string field;
        Random rand = new Random();

        public void CreateField(int width, int height, int BombCount)
        {
            int x=0, y=0;

            intfield = new int[height, width];

            for(int i = 0; i < BombCount; i++)
                intfield[rand.Next(0,height), rand.Next(0,width)] = 9;


            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                    if (intfield[i, j] == 9)
                    {
                        if (i - 1 > 0) y = i - 1; else y = 0;
                        x = j - 1;
                        for (int k = 0; k < 3; k++)
                        {
                            if (x < 0) { x++;continue;}
                            if (intfield[y, x]!=9) intfield[y, x]++;
                            if (x + 1 < width) x++; else break;
                        }

                        if (i + 1 < height) y = i + 1; else y = height-1;
                        x = j - 1;
                        for (int k = 0; k < 3; k++)
                        {
                            if (x < 0) { x++; continue; }
                            if (intfield[y, x] != 9) intfield[y, x]++;
                            if (x + 1 < width) x++; else break;
                        }

                        y = i;
                        x = j - 1;
                        for (int k = 0; k < 2; k++)
                        {
                            if (x < 0) { x++; continue; }
                            if (intfield[y, x] != 9) intfield[y, x]++;
                            if (x + 2 < width) x += 2; else break;
                        }
                    }

            foreach (int block in intfield)
            {
                if (block == 9)
                    field+= 'B';
                else
                    field += block;
            }
        }
    }
}
