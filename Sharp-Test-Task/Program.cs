using System;
using System.Threading;
using System.Threading.Tasks;

class Program {
    static void Main(string[] args) {
        Task a = SystemA.RunSystemA(100, 5000);
        Task b = SystemB.RunSystemB(250);
        a.Wait();
        b.Wait();
    }
}