// Nuget Package
// dotnet add package WinSCP --version 6.1.0

// Below is an example to download a file from the FTP server:

var ftpClient = new FtpClient("ftp.example.com", "username", "password");
var remotePath = "/folder/file.txt";
var localPath = "C:\\downloads\\file.txt";
ftpClient.Download(remotePath, localPath);

//--------------------
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WinSCP;

namespace ProjectNamespace
{
    public class FtpClient
    {
        public string FtpHostName { get; set; }
        public string FtpUsername { get; set; }
        public string FtpPassword { get; set; }

        public FtpClient(string ftpHostName, string ftpUsername, string ftpPassword)
        {
            FtpHostName = ftpHostName;
            FtpUsername = ftpUsername;
            FtpPassword = ftpPassword;
        }

        private SessionOptions CreateSessionOptions()
        {
            return new SessionOptions
            {
                Protocol = Protocol.Sftp,
                HostName = FtpHostName,
                UserName = FtpUsername,
                Password = FtpPassword,
                SshHostKeyPolicy = SshHostKeyPolicy.GiveUpSecurityAndAcceptAny
            };
        }

        public void Download(string remotePath, string localPath)
        {
            var sessionOptions = CreateSessionOptions();

            using (var session = new Session())
            {
                session.Open(sessionOptions);

                var directory = Path.GetDirectoryName(localPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var transferResult = session.GetFiles(remotePath, localPath);
                transferResult.Check();
                session.Close();
            }
        }

        public List<string> GetListFiles(string remotePath)
        {
            var sessionOptions = CreateSessionOptions();

            using (var session = new Session())
            {
                session.Open(sessionOptions);

                var files = session.ListDirectory(remotePath);

                var listFiles = new List<string>();

                foreach (var fileInfo in files.Files.Cast<RemoteFileInfo>())
                {
                    listFiles.Add(fileInfo.Name);
                }

                session.Close();

                return listFiles;
            }
        }

        public List<string> GetListDirectories(string remotePath)
        {
            var sessionOptions = CreateSessionOptions();
            using (var session = new Session())
            {
                session.Open(sessionOptions);

                var files = session.ListDirectory(remotePath);

                var listDirectories = new List<string>();

                foreach (var fileInfo in files.Files.Cast<RemoteFileInfo>())
                {
                    if (fileInfo.IsDirectory)
                    {
                        listDirectories.Add(fileInfo.Name);
                    }
                }

                session.Close();

                return listDirectories;
            }
        }
    }
}