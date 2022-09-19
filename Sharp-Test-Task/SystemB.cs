using System.IO;
using System.Threading;
using System.Threading.Tasks;

class SystemB {
    private const string Path = "./../../../FILES";
    private const string Mask = "*.txt";
    private const string PathMessagesRender = "./../../../messagesGot.txt";

    public static List<string> GetAllFileNames() {
        DirectoryInfo directory = new DirectoryInfo(Path);
        FileInfo[] files = directory.GetFiles(Mask);
        List<string> list = new List<string>();
        foreach (FileInfo file in files) {
            list.Add(Path + "/" + file.Name);
        }
        return list;
    } 

    private static string DeleteRubbishFromMsg(string message) {
        string resMsg = message.Split(Path)[1];
        resMsg = resMsg.Split(".txt")[0];
        resMsg = resMsg.Split("/")[1];
        return resMsg;
    }

    private static void RenderMessageToFile(string message) {
        StreamWriter f = File.AppendText(PathMessagesRender);
        string resMsg = DeleteRubbishFromMsg(message);
        f.WriteLine(resMsg);
        f.Close();
    }

    private static string GetMaxPriorityMessage(List<string> list) {
        string resultMessage = string.Empty;
        int maxPriority = -1;
        foreach (string element in list) {
            string bfr = element.Split('_')[1];
            bfr = bfr.Split(".txt")[0];
            int priority = int.Parse(bfr);
            if (priority > maxPriority) {
                maxPriority = priority;
                resultMessage = element;
            }
        }
        return resultMessage;
    }

    private static void DeleteFile(string filePath) {
        File.Delete(filePath);
    }

    public static Task RunSystemB(int delayMs) {
        return Task.Run(() => {
            while (true) {
                List<string> list = GetAllFileNames();
                string maxPriorMsg = GetMaxPriorityMessage(list);
                if (maxPriorMsg != string.Empty) {
                    RenderMessageToFile(maxPriorMsg);
                    DeleteFile(maxPriorMsg);
                }
                Thread.Sleep(delayMs);
            }
        });
    }
}