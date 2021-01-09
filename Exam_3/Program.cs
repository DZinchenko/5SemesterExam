using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam_3
{
    class Program
    {
        static string word;
        static CancellationTokenSource tokenSource = new CancellationTokenSource();

        static void Main(string[] args)
        {
            Console.Write("New word: ");
            word = Console.ReadLine();

            Task.Run(() => Write());

            while (true)
            {
                if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                {
                    tokenSource.Cancel();
                }
            }
        }

        static void Write()
        {
            WriteTheWord(tokenSource.Token).ContinueWith((arg) =>
            {
                Console.Write("New word: ");
                word = Console.ReadLine();
                tokenSource = new CancellationTokenSource();
                Write();
            });
        }

        static async Task WriteTheWord(CancellationToken token)
        {
            while (true)
            {
                token.ThrowIfCancellationRequested();
                Console.WriteLine(word);
                await Task.Delay(1000, token);
            }
        }
    }

}
