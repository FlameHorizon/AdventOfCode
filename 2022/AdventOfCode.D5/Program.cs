namespace AdventOfCode.D5
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
            Stack<Crate>[] cargo = CreateCargoFromInput();
            RearrangementProcedure rearrangementProc = CreateRearrangementProcedureFromInput();
            foreach (MoveProcedure moveProc in rearrangementProc.MoveProcedures)
            {
                cargo = CrateMover9000.Move(cargo, moveProc);
            }
            string result = JoinTopCratesNames(cargo);

            Console.WriteLine("After the rearrangement procedure completes, "
                + "what crate ends up on top of each stack?");
            Console.WriteLine(result);
        }

        private static string JoinTopCratesNames(Stack<Crate>[] cargo)
        {
            return string.Join(separator: string.Empty,
                               cargo.Select(x => x.Peek().Name));
        }

        private static Stack<Crate>[] CreateCargoFromInput()
        {
            var output = new Stack<Crate>[9];
            List<string> cargos = new()
            {
                "RGHQSNBT",
                "HSFDPZJ",
                "VHZ",
                "MZJFGH",
                "TZCDLMSR",
                "MTWVHZJ",
                "TFPLZ",
                "QVWS",
                "WHLMTDNC"
            };

            for (int i = 0; i < cargos.Count; i++)
            {
                output[i] = new Stack<Crate>(cargos[i]
                    .ToCharArray()
                    .Select(chr => new Crate(chr.ToString())));
            }

            return output;
        }

        private static RearrangementProcedure CreateRearrangementProcedureFromInput()
        {
            var output = new RearrangementProcedure();
            foreach (string ln in File.ReadAllLines(@"data\input.txt"))
            {
                output.MoveProcedures.Add(new MoveProcedure(ln));
            }

            return output;
        }

        private static class CrateMover9000
        {
            public static Stack<Crate>[] Move(Stack<Crate>[] cargo, MoveProcedure procedure)
            {
                for (int i = 0; i < procedure.Amount; i++)
                {
                    cargo[procedure.To].Push(cargo[procedure.From].Pop());
                }

                return cargo;
            }
        }

        private class Crate
        {
            public Crate(string name)
            {
                Name = name;
            }

            public string Name { get; set; }
        }

        private class RearrangementProcedure
        {
            public List<MoveProcedure> MoveProcedures { get; } = new();
        }

        private class MoveProcedure
        {
            public int Amount { get; }
            public int From { get; }
            public int To { get; }

            public MoveProcedure(string line)
            {
                var split = line.Split();
                Amount = int.Parse(split[1]);
                From = int.Parse(split[3]) - 1;
                To = int.Parse(split[5]) - 1;
            }
        }

        private static void Part2()
        {
            Stack<Crate>[] cargo = CreateCargoFromInput();
            RearrangementProcedure rearrangementProcedure = CreateRearrangementProcedureFromInput();
            foreach (MoveProcedure procedure in rearrangementProcedure.MoveProcedures)
            {
                cargo = CrateMover9001.Move(cargo, procedure);
            }

            string result = JoinTopCratesNames(cargo);

            Console.WriteLine("After the rearrangement procedure completes, "
                + "what crate ends up on top of each stack?");
            Console.WriteLine(result);
        }

        private static class CrateMover9001
        {
            public static Stack<Crate>[] Move(Stack<Crate>[] cargo, MoveProcedure procedure)
            {
                var buffor = new List<Crate>();
                for (int i = 0; i < procedure.Amount; i++)
                {
                    buffor.Add(cargo[procedure.From].Pop());
                }

                foreach (Crate crate in buffor.Reverse<Crate>())
                {
                    cargo[procedure.To].Push(crate);
                }

                return cargo;
            }
        }
    }
}