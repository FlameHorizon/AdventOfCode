using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
            Console.WriteLine("How many characters need to be processed before the "
                            + "first start-of-packet marker is detected?");

            var device = new CommunicationDevice();
            device.LockOn();

            Console.WriteLine(device.SignalInfo.StartOfPacketMarker);
        }

        private enum MarkerTypes
        {
            StartOfPacket = 0,
            StartOfMessage
        }

        private class CommunicationDevice
        {
            public SignalInfo SignalInfo { get; private set; } = null!;

            public void LockOn()
            {
                SignalInfo = new SignalInfo(File.ReadAllText(@"data\input.txt"));
                SignalInfo.StartOfPacketMarker = DetectMarker(MarkerTypes.StartOfPacket);
                SignalInfo.StartOfMessageMarker = DetectMarker(MarkerTypes.StartOfMessage);
            }

            private int DetectMarker(MarkerTypes type)
            {
                return type switch
                {
                    MarkerTypes.StartOfPacket => DetectMarket(markerLength: 4),
                    MarkerTypes.StartOfMessage => DetectMarket(14),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };
            }

            private int DetectMarket(int markerLength)
            {

                for (int i = 0; i < SignalInfo.Raw.Length - markerLength; i++)
                {
                    var substring = SignalInfo.Raw[i..(i + markerLength)];
                    if (substring.Distinct().Count() == markerLength)
                    {
                        return i + markerLength;
                    }
                }

                return -1;
            }
        }

        private class SignalInfo
        {
            public string Raw { get; init; }
            public int StartOfPacketMarker { get; set; }
            public int StartOfMessageMarker { get; set; }

            public SignalInfo(string rawSignal)
            {
                Raw = rawSignal;
            }
        }

        private static void Part2()
        {
            Console.WriteLine("How many characters need to be processed "
                            + "before the first start-of-message marker is detected?");

            var device = new CommunicationDevice();
            device.LockOn();

            Console.WriteLine(device.SignalInfo.StartOfMessageMarker);
        }
    }
}