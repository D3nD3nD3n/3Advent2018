using System;
using System.Collections.Generic;

namespace _3Advent2018
{
    class Program
    {


        static void Main()
        {
            int[,] grid = new int[1000, 1000];
            string line;
            List<int> uniqueClaims = new List<int>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"Input.txt");
            while ((line = file.ReadLine()) != null)
            {
                FlipClaim(GetClaim(line), grid, uniqueClaims);
            }
            
            file.Close();
            Console.WriteLine("final: " + CountOverlaps(grid));

            foreach(int i in uniqueClaims)
            {
                Console.WriteLine("Unique Claim: " + i);
            }

        }

        
        public struct Claim
        {
            public int ClaimID;
            public int x;
            public int y;
            public int length;
            public int width;
        }
        public static void FlipClaim(Claim s, int[,] a, List<int> claims)
        {
            claims.Add(s.ClaimID);
            for (int i = s.x; i < s.length + s.x; i++)
            {
                for (int j = s.y; j < s.width + s.y; j++)
                {
                    if (a[i, j] == 0 && a[i, j] != -1)
                    {
                        a[i, j] = s.ClaimID;
                    }
                    else
                    {
                        if (claims.Contains(a[i, j]))
                            claims.Remove(a[i, j]);
                        if (claims.Contains(s.ClaimID))
                            claims.Remove(s.ClaimID);

                        a[i, j] = -1;
                    }
                }
            }
        }

        public static int CountOverlaps(int[,] a)
        {
            int count = 0;
            foreach(int i in a)
            {
                if (i == -1)
                    count++;
            }
            return count;
        }
        public static Claim GetClaim(string input)
        {
            //#1 @ 49,222: 19x20
            Claim s = new Claim();

            string[] firstSplit = input.Split(' ');
            string[] CoordinateSplit = firstSplit[2].Split(',');
            CoordinateSplit[1] = CoordinateSplit[1].Remove(CoordinateSplit[1].Length - 1);
            string[] DimensionSplit = firstSplit[3].Split('x');

            s.ClaimID = int.Parse(firstSplit[0].Substring(1));
            s.x = int.Parse(CoordinateSplit[0]);
            s.y = int.Parse(CoordinateSplit[1]);
            s.length = int.Parse(DimensionSplit[0]);
            s.width = int.Parse(DimensionSplit[1]);

            return s;
        }
    }
}
