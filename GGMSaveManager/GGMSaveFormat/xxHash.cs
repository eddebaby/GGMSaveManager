using System;
using System.Runtime.CompilerServices;

namespace GGMSaveManager
{
    /// <summary>
    /// The GGM uses the xxHash64 algorithm to validate/check save data - https://cyan4973.github.io/xxHash/
    /// </summary>
    class XXHash
    {
        /// <summary>
        /// Rotates the specified input value left by the specified number of bits.
        /// </summary>
        private static UInt64 RotateLeft(UInt64 input, int offset)
        {
            return (input << offset) | (input >> (64 - offset));
        }

        /// <summary>
        /// Compute xxHash64 for the given input data (byte array), starting offset,
        /// and seed (default 0). The computed hash is returned as a 64-bit Unsigned Integer.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 xxHash64(ref byte[] inputData, int start = 0, UInt64 seed = 0)
        {
            // xxHash64 algorithm - https://cyan4973.github.io/xxHash/

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
    }
}
