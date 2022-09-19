using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class SystemA {
    private const int SizeOfMessage = 20;
    private const int MaxPriorityValue = 5;
    private const string Path = "./../../../FILES/";

    private static int GetRandomNumber() {
        Random rnd = new Random();
        int value = rnd.Next();
        if(value < 0) {
            value = -1 * value;
        }
        return value;
    }

    private static string GenerateRandomMessage() {
        StringBuilder builder = new StringBuilder();
        for(int i = 0; i < SizeOfMessage; i++) {
            int valueRandom = GetRandomNumber() % 10;
            builder.Append("" + valueRandom);
        }
        return builder.ToString();
    }

    private static int GetRandomPriority() {
        int priority = GetRandomNumber() % MaxPriorityValue;
        priority += 1;
        return priority;
    }

    private static void SaveMessageAsFile(string message) {
        StreamWriter f = File.CreateText(Path + message + ".txt");
        f.Close();
    }

    public static Task RunSystemA(int delayMs, int bigDelayMs) {
        return Task.Run(() => {
            int count = 0;
            while(true) {
                count++;
                string msg = GenerateRandomMessage();
                int priority = GetRandomPriority();
                SaveMessageAsFile(msg + "_" + priority);
                Thread.Sleep(delayMs);
                if(count > 20) {
                    count = 0;
                    Thread.Sleep(bigDelayMs);
                }
            }
        });
    }
}