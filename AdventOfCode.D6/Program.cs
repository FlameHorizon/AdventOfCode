using System.IO;
using System.Linq;

namespace AdventOfCode.D6
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Part1();
            Part2();

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();
        }

        private static void Part1()
        {
            System.Console.WriteLine("How many characters need to be processed before the "
                                     + "first start-of-packet marker is detected?");

            var device = new CommunicationDevice();
            device.LockOn();

            int result = device.SignalInfo.StartOfPacketMarker;
            System.Console.WriteLine(result);
        }

        private class CommunicationDevice
        {
            public SignalInfo SignalInfo { get; private set; } = null;

            public void LockOn()
            {
                SignalInfo = new SignalInfo(File.ReadAllText(@"data\input.txt"));
                SignalInfo.StartOfPacketMarker = DetectStartOfPackerMarker();
                SignalInfo.StartOfMessageMarker = DetectStartOfMessageMarker();
            }

            private int DetectStartOfPackerMarker()
            {
                const int markerLength = 4;

                for (int i = 0; i < SignalInfo.SignalRaw.Length - markerLength; i++)
                {
                    var range = SignalInfo.SignalRaw[i..(i + markerLength)];
                    if (range.Distinct().Count() == markerLength)
                    {
                        return i + markerLength;
                    }
                }

                return -1;
            }

            private int DetectStartOfMessageMarker()
            {
                const int markerLength = 14;

                for (int i = 0; i < SignalInfo.SignalRaw.Length - markerLength; i++)
                {
                    var range = SignalInfo.SignalRaw[i..(i + markerLength)];
                    if (range.Distinct().Count() == markerLength)
                    {
                        return i + markerLength;
                    }
                }

                return -1;
            }
        }

        private class SignalInfo
        {
            public string SignalRaw = string.Empty;
            public int StartOfPacketMarker { get; set; }
            public int StartOfMessageMarker { get; set; }

            public SignalInfo(string signal)
            {
                SignalRaw = signal;
            }
        }

        private static void Part2()
        {
            System.Console.WriteLine("How many characters need to be processed "
                + "before the first start-of-message marker is detected?");

            var device = new CommunicationDevice();
            device.LockOn();

            int result = device.SignalInfo.StartOfMessageMarker;
            System.Console.WriteLine(result);
        }
    }
}