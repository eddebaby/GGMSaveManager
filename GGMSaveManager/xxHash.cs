using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace GGMSaveManager
{
    class XXHash
    {
        // xxHash64 algorithm - https://cyan4973.github.io/xxHash/

        private static UInt64 RotateLeft(UInt64 input, int offset)
        {
            return (input << offset) | (input >> (64 - offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 xxHash64(ref byte[] inputData, int start = 0, UInt64 seed = 0)
        {
            // References xxHash64 c# implementation from - https://github.com/uranium62/xxHash

            // prime numbers
            UInt64 p1 = 0x9e3779b185ebca87;
            UInt64 p2 = 0xc2b2ae3d27d4eb4f;
            UInt64 p3 = 0x165667b19e3779f9;
            UInt64 p4 = 0x85ebca77c2b2ae63;
            UInt64 p5 = 0x27d4eb2f165667c5;

            int offset = start;
            int length = inputData.Length - start;
            int end = offset + length;
            UInt64 h64;

            if (length >= 32)
            {
                int limit = end - 32;

                UInt64 v1 = seed + p1 + p2;
                UInt64 v2 = seed + p2;
                UInt64 v3 = seed + 0;
                UInt64 v4 = seed - p1;

                do
                {
                    v1 += BitConverter.ToUInt64(inputData, offset) * p2;
                    v1 = RotateLeft(v1, 31); // rotl 31
                    v1 *= p1;
                    offset += 8;

                    v2 += BitConverter.ToUInt64(inputData, offset) * p2;
                    v2 = RotateLeft(v2, 31); // rotl 31
                    v2 *= p1;
                    offset += 8;

                    v3 += BitConverter.ToUInt64(inputData, offset) * p2;
                    v3 = RotateLeft(v3, 31); // rotl 31
                    v3 *= p1;
                    offset += 8;

                    v4 += BitConverter.ToUInt64(inputData, offset) * p2;
                    v4 = RotateLeft(v4, 31); // rotl 31
                    v4 *= p1;
                    offset += 8;

                } while (offset <= limit);

                h64 = RotateLeft(v1, 1) +  // rotl 1
                      RotateLeft(v2, 7) +  // rotl 7
                      RotateLeft(v3, 12) + // rotl 12
                      RotateLeft(v4, 18);  // rotl 18

                // merge round
                v1 *= p2;
                v1 = RotateLeft(v1, 31); // rotl 31
                v1 *= p1;
                h64 ^= v1;
                h64 = h64 * p1 + p4;

                // merge round
                v2 *= p2;
                v2 = RotateLeft(v2, 31); // rotl 31
                v2 *= p1;
                h64 ^= v2;
                h64 = h64 * p1 + p4;

                // merge round
                v3 *= p2;
                v3 = RotateLeft(v3, 31); // rotl 31
                v3 *= p1;
                h64 ^= v3;
                h64 = h64 * p1 + p4;

                // merge round
                v4 *= p2;
                v4 = RotateLeft(v4, 31); // rotl 31
                v4 *= p1;
                h64 ^= v4;
                h64 = h64 * p1 + p4;
            }
            else
            {
                h64 = seed + p5;
            }

            h64 += (UInt64)length;

            // finalize
            while (offset <= end - 8)
            {
                UInt64 t1 = BitConverter.ToUInt64(inputData, offset) * p2;
                t1 = RotateLeft(t1, 31); // rotl 31
                t1 *= p1;
                h64 ^= t1;
                h64 = RotateLeft(h64, 27) * p1 + p4; // (rotl 27) * p1 + p4
                offset += 8;
            }

            if (offset <= end - 4)
            {
                h64 ^= BitConverter.ToUInt64(inputData, offset) * p1;
                h64 = RotateLeft(h64, 23) * p2 + p3; // (rotl 23) * p2 + p3
                offset += 4;
            }

            while (offset < end)
            {
                h64 ^= inputData[offset] * p5;
                h64 = RotateLeft(h64, 11) * p1; // (rotl 11) * p1
                offset += 1;
            }

            // avalanche
            h64 ^= h64 >> 33;
            h64 *= p2;
            h64 ^= h64 >> 29;
            h64 *= p3;
            h64 ^= h64 >> 32;

            return h64;
        }

        /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe UInt64 UnsafeComputeHash(byte* ptr, int length, UInt64 seed = 0)
        {
            // Modified xxHash64 c# implementation from - https://github.com/uranium62/xxHash

            // prime numbers
            UInt64 p1 = 0x9e3779b185ebca87;
            UInt64 p2 = 0xc2b2ae3d27d4eb4f;
            UInt64 p3 = 0x165667b19e3779f9;
            UInt64 p4 = 0x85ebca77c2b2ae63;
            UInt64 p5 = 0x27d4eb2f165667c5;

            byte* end = ptr + length;
            UInt64 h64;

            if (length >= 32)

            {
                byte* limit = end - 32;

                UInt64 v1 = seed + p1 + p2;
                UInt64 v2 = seed + p2;
                UInt64 v3 = seed + 0;
                UInt64 v4 = seed - p1;

                do
                {
                    v1 += *((UInt64*)ptr) * p2;
                    v1 = RotateLeft(v1, 31); // rotl 31
                    v1 *= p1;
                    ptr += 8;

                    v2 += *((UInt64*)ptr) * p2;
                    v2 = RotateLeft(v2, 31); // rotl 31
                    v2 *= p1;
                    ptr += 8;

                    v3 += *((UInt64*)ptr) * p2;
                    v3 = RotateLeft(v3, 31); // rotl 31
                    v3 *= p1;
                    ptr += 8;

                    v4 += *((UInt64*)ptr) * p2;
                    v4 = RotateLeft(v4, 31); // rotl 31
                    v4 *= p1;
                    ptr += 8;

                } while (ptr <= limit);

                h64 = RotateLeft(v1, 1) +  // rotl 1
                      RotateLeft(v2, 7) +  // rotl 7
                      RotateLeft(v3, 12) + // rotl 12
                      RotateLeft(v4, 18);  // rotl 18

                // merge round
                v1 *= p2;
                v1 = RotateLeft(v1, 31); // rotl 31
                v1 *= p1;
                h64 ^= v1;
                h64 = h64 * p1 + p4;

                // merge round
                v2 *= p2;
                v2 = RotateLeft(v2, 31); // rotl 31
                v2 *= p1;
                h64 ^= v2;
                h64 = h64 * p1 + p4;

                // merge round
                v3 *= p2;
                v3 = RotateLeft(v3, 31); // rotl 31
                v3 *= p1;
                h64 ^= v3;
                h64 = h64 * p1 + p4;

                // merge round
                v4 *= p2;
                v4 = RotateLeft(v4, 31); // rotl 31
                v4 *= p1;
                h64 ^= v4;
                h64 = h64 * p1 + p4;
            }
            else
            {
                h64 = seed + p5;
            }

            h64 += (UInt64)length;

            // finalize
            while (ptr <= end - 8)
            {
                UInt64 t1 = *((UInt64*)ptr) * p2;
                t1 = RotateLeft(t1, 31); // rotl 31
                t1 *= p1;
                h64 ^= t1;
                h64 = RotateLeft(h64, 27) * p1 + p4; // (rotl 27) * p1 + p4
                ptr += 8;
            }

            if (ptr <= end - 4)
            {
                h64 ^= *((UInt32*)ptr) * p1;
                h64 = RotateLeft(h64, 23) * p2 + p3; // (rotl 23) * p2 + p3
                ptr += 4;
            }

            while (ptr < end)
            {
                h64 ^= *((byte*)ptr) * p5;
                h64 = RotateLeft(h64, 11) * p1; // (rotl 11) * p1
                ptr += 1;
            }

            // avalanche
            h64 ^= h64 >> 33;
            h64 *= p2;
            h64 ^= h64 >> 29;
            h64 *= p3;
            h64 ^= h64 >> 32;

            return h64;
        }*/

        /*private static unsafe UInt64 CalculateHash()
        {
            // Uses xxHash64 algorithm - https://cyan4973.github.io/xxHash/
            UInt64 newHash = 0;

            byte[] saveSlotMinusHash = new byte[GGMSaveBin.slotLength - GGMSaveBin.hashLength];
            Array.Copy(CreateSlotData(inputData), GGMSaveBin.hashLength, saveSlotMinusHash, 0, GGMSaveBin.slotLength - GGMSaveBin.hashLength);

            fixed (byte* pData = &saveSlotMinusHash[0])
            {
                newHash = XXHash.UnsafeComputeHash(pData, saveSlotMinusHash.Length);
            }

            return newHash;
        }*/
    }
}
