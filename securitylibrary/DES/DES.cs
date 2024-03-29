﻿using System;
using System.Collections.Generic;

namespace SecurityLibrary.DES
{
    /// <summary>
    /// If the string starts with 0x.... then it's Hexadecimal not string
    /// </summary>
    public class DES : CryptographicTechnique
    {
        public static int[] PC1 =
        {
              57, 49, 41, 33, 25, 17, 9,
              1, 58, 50, 42, 34, 26, 18,
              10, 2, 59, 51, 43, 35, 27,
              19, 11, 3, 60, 52, 44, 36,
              63, 55, 47, 39, 31, 23, 15,
              7, 62, 54, 46, 38, 30, 22,
              14, 6, 61, 53, 45, 37, 29,
              21, 13, 5, 28, 20, 12, 4
        };

        public static int[] PC2 =
             {
             14, 17, 11, 24, 1, 5,
             3, 28, 15, 6, 21, 10,
             23, 19, 12, 4, 26, 8,
             16, 7, 27, 20, 13, 2,
             41, 52, 31, 37, 47, 55,
             30, 40, 51, 45, 33, 48,
             44, 49, 39, 56, 34, 53,
             46, 42, 50, 36, 29, 32
        };

        public static int[] IP =
        {
            58, 50, 42, 34, 26, 18, 10, 2,
            60, 52, 44, 36, 28, 20, 12, 4,
            62, 54, 46, 38, 30, 22, 14, 6,
            64, 56, 48, 40, 32, 24, 16, 8,
            57, 49, 41, 33, 25, 17, 9, 1,
            59, 51, 43, 35, 27, 19, 11, 3,
            61, 53, 45, 37, 29, 21, 13, 5,
            63, 55, 47, 39, 31, 23, 15, 7
        };
        public static int[] Expansion =
        {
             32,1,2,3,4,5,
             4,5,6,7,8,9,
             8,9,10,11,12,13,
             12,13,14,15,16,17,
             16,17,18,19,20,21,
             20,21,22,23,24,25,
             24,25,26,27,28,29,
            28,29,30,31,32,1
        };

        public static int[] P =
        {
             16,7,20,21,
             29,12,28,17,
             1,15,23,26,
             5,18,31,10,
             2,8,24,14,
             32,27,3,9,
             19,13,30,6,
             22,11,4,25
        };

        public static int[] IPinverse =
        {
             40, 8, 48, 16, 56, 24, 64, 32,
             39, 7, 47, 15, 55, 23, 63, 31,
             38, 6, 46, 14, 54, 22, 62, 30,
             37, 5, 45, 13, 53, 21, 61, 29,
             36, 4, 44, 12, 52, 20, 60, 28,
             35, 3, 43, 11, 51, 19, 59, 27,
             34, 2, 42, 10, 50, 18, 58, 26,
             33, 1, 41, 9, 49, 17, 57, 25
        };


        public static List<int[,]> S = new List<int[,]>
        {
            new int[,] {{14,4,13,1,2,15,11,8,3,10,6,12,5,9,0,7},
                        {0,15,7,4,14,2,13,1,10,6,12,11,9,5,3,8},
                        {4,1,14,8,13,6,2,11,15,12,9,7,3,10,5,0},
                        {15,12,8,2,4,9,1,7,5,11,3,14,10,0,6,13}},

            new int[,] {{15,1,8,14,6,11,3,4,9,7,2,13,12,0,5,10},
                        {3,13,4,7,15,2,8,14,12,0,1,10,6,9,11,5},
                        {0,14,7,11,10,4,13,1,5,8,12,6,9,3,2,15},
                        {13,8,10,1,3,15,4,2,11,6,7,12,0,5,14,9}},

            new int[,] {{10,0,9,14,6,3,15,5,1,13,12,7,11,4,2,8},
                        {13,7,0,9,3,4,6,10,2,8,5,14,12,11,15,1},
                        {13,6,4,9,8,15,3,0,11,1,2,12,5,10,14,7},
                        {1,10,13,0,6,9,8,7,4,15,14,3,11,5,2,12}},

            new int[,] {{7,13,14,3,0,6,9,10,1,2,8,5,11,12,4,15},
                        {13,8,11,5,6,15,0,3,4,7,2,12,1,10,14,9},
                        {10,6,9,0,12,11,7,13,15,1,3,14,5,2,8,4},
                        {3,15,0,6,10,1,13,8,9,4,5,11,12,7,2,14}},

            new int[,] {{2,12,4,1,7,10,11,6,8,5,3,15,13,0,14,9},
                        {14,11,2,12,4,7,13,1,5,0,15,10,3,9,8,6},
                        {4,2,1,11,10,13,7,8,15,9,12,5,6,3,0,14},
                        {11,8,12,7,1,14,2,13,6,15,0,9,10,4,5,3}},
            new int[,] {{12,1,10,15,9,2,6,8,0,13,3,4,14,7,5,11},
                        {10,15,4,2,7,12,9,5,6,1,13,14,0,11,3,8},
                        {9,14,15,5,2,8,12,3,7,0,4,10,1,13,11,6},
                        {4,3,2,12,9,5,15,10,11,14,1,7,6,0,8,13}},
            new int[,] {{4,11,2,14,15,0,8,13,3,12,9,7,5,10,6,1},
                        {13,0,11,7,4,9,1,10,14,3,5,12,2,15,8,6},
                        {1,4,11,13,12,3,7,14,10,15,6,8,0,5,9,2},
                        {6,11,13,8,1,4,10,7,9,5,0,15,14,2,3,12}},
            new int[,] {{13,2,8,4,6,15,11,1,10,9,3,14,5,0,12,7},
                        {1,15,13,8,10,3,7,4,12,5,6,11,0,14,9,2},
                        {7,11,4,1,9,12,14,2,0,6,10,13,15,3,5,8},
                        {2,1,14,7,4,10,8,13,15,12,9,0,3,5,6,11}},
        };

        static string permutation(string binaryNumber, int[] P)
        {

            string tmp = "";
            for (int i = 0; i < P.Length; i++)
            {
                tmp += binaryNumber[P[i] - 1];
            }
            binaryNumber = tmp;
            return binaryNumber;
        }
        static string dePermutation(string binaryNumber, int[] P)
        {
            char[] arr = new char[binaryNumber.Length];
            for (int i = 0; i < P.Length; i++)
            {
                arr[P[i] - 1] = binaryNumber[i];
            }
            string charsStr = new string(arr);
            return charsStr;
        }
        static string shiftLeft(string binaryNumber)
        {
            string shiftedBinaryNumber = binaryNumber.Substring(1, binaryNumber.Length - 1);
            shiftedBinaryNumber += binaryNumber[0];
            return shiftedBinaryNumber;
        }


        static string fun(string right, string key)
        {
            string exp_right = "";
            for (int i = 0; i < 48; i++)
                exp_right += right[Expansion[i] - 1];
            //return exp_right;
            string xr = XOR(key, exp_right); ;
            string xrComp = "";
            for (int i = 0; i < 8; i++)
            {
                string b = xr.Substring(i * 6, 6);
                string rowBi = "";
                rowBi += (char)b[0];
                rowBi += (char)b[5];
                string colBi = b.Substring(1, 4);
                int row = Convert.ToInt32(rowBi, 2);
                int col = Convert.ToInt32(colBi, 2);

                int tmp = S[i][row, col];

                string str = Convert.ToString(tmp, 2);//Convert Hex to Bin
                str = missingZeros(str, 4);

                xrComp += str;
            }
            xrComp = permutation(xrComp, P);
            return xrComp;
        }
        static string missingZeros(string number, int len)
        {
            string tmp = "";
            for (int i = 0; i < len - number.Length; i++)
            {
                tmp += '0';
            }
            tmp += number;
            return tmp;
        }

        static string XOR(string x, string y)
        {
            string res = "";
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] == y[i])
                    res += "0";
                else
                    res += "1";
            }
            return res;
        }
        static string HexConverted(string strBinary)
        {
            strBinary = Convert.ToInt64(strBinary, 2).ToString("X");
            strBinary = missingZeros(strBinary, 16);
            string strHex = "0x" + strBinary;
            return strHex;
        }


        static List<string> generateKeys(string key)
        {
            List<string> keys = new List<string>();

            key = Convert.ToString(Convert.ToInt64(key, 16), 2);//Convert Hex to Bin
            key = missingZeros(key, 64);
            key = permutation(key, PC1);//key permutation 64 bit -> 56 bit
            //initialize C & D list for key of 16 round 
            List<string> C = new List<string>();
            List<string> D = new List<string>();
            //split the key and add it to the head of the lists
            C.Add(key.Substring(0, 28));
            D.Add(key.Substring(28, 28));
            //C & D permutation
            for (int i = 1; i <= 16; i++)
            {
                C.Add(C[i - 1]);
                D.Add(D[i - 1]);
                if (i == 1 || i == 2 || i == 9 || i == 16)
                {
                    C[i] = shiftLeft(C[i]);
                    D[i] = shiftLeft(D[i]);
                }
                else
                {
                    C[i] = shiftLeft(C[i]);
                    C[i] = shiftLeft(C[i]);
                    D[i] = shiftLeft(D[i]);
                    D[i] = shiftLeft(D[i]);
                }

            }
            //Concatenate C & D then permutation for 16 rounds keys
            keys.Add("yalahwaaaaaaaaaay");
            for (int i = 1; i <= 16; i++)
                keys.Add(C[i] + D[i]);

            for (int i = 1; i <= 16; i++)
                keys[i] = permutation(keys[i], PC2);

            return keys;
        }

        public override string Decrypt(string cipherText, string key)
        {
            List<string> keys = generateKeys(key);

            cipherText = Convert.ToString(Convert.ToInt64(cipherText, 16), 2);//Convert Hex to Bin
            cipherText = missingZeros(cipherText, 64);
            //decrypt steps
            //IPinv
            cipherText = dePermutation(cipherText, IPinverse);
            //swap
            cipherText = cipherText.Substring(32, 32) + cipherText.Substring(0, 32); ;
            //rounds
            for (int i = 16; i >= 1; i--)
            {
                string left_n_Plus1 = cipherText.Substring(0, 32);
                string right_n_Plus1 = cipherText.Substring(32, 32);
                string right_n = left_n_Plus1;
                string left_n = XOR(right_n_Plus1, fun(right_n, keys[i]));
                cipherText = left_n + right_n;
            }
            //IP
            cipherText = dePermutation(cipherText, IP);
            return HexConverted(cipherText);
        }

        public override string Encrypt(string plainText, string key)
        {

            List<string> keys = generateKeys(key);

            plainText = Convert.ToString(Convert.ToInt64(plainText, 16), 2);//Convert Hex to Bin
            plainText = missingZeros(plainText, 64);
            plainText = permutation(plainText, IP);//plain permutation
            // Console.WriteLine(plain);
            string left_n_Plus1 = "", right_n_Plus1 = "";
            for (int i = 1; i <= 16; i++)
            {
                string left_n = plainText.Substring(0, 32);
                string right_n = plainText.Substring(32, 32);
                left_n_Plus1 = right_n;
                right_n_Plus1 = XOR(left_n, fun(right_n, keys[i]));
                plainText = left_n_Plus1 + right_n_Plus1;
            }
            plainText = right_n_Plus1 + left_n_Plus1;
            plainText = permutation(plainText, IPinverse);

            return HexConverted(plainText);
        }
    }
}