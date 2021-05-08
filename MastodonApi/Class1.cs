using System;

namespace MastodonApi
{
    abstract record AOrB
    {
        public record A(): AOrB;
        public record B(): AOrB;
    }

    public class Class1
    {
        public static void Main()
        {

            var a = new AOrB.A();
            var b = new AOrB.B();

            void PrintAOrB(AOrB aorb) {
                switch (aorb)
                {
                    case AOrB.A a:
                        Console.WriteLine($"A: {a}");
                        break;
                    case AOrB.B b:
                        Console.WriteLine($"B: {b}");
                        break;
                };
            }

            PrintAOrB(a);
            PrintAOrB(b);
        }
    }
}
