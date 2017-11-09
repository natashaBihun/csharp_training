using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.FtpClient;

namespace mantis_tests
{
    public class FtpHelper : HelperBase
    {
        private FtpClient _client;
        public FtpHelper(ApplicationManager manager)
            :base(manager) {
            _client = new FtpClient();
            _client.Host = "localhost";
            _client.Credentials = new System.Net.NetworkCredential("mantis", "mantis");
            _client.Connect();
        }

        public void BackupFile(string path) {
            String backUpPath = path + ".bak";
            if (_client.FileExists(backUpPath)) {
                return;
            }
            _client.Rename(path, backUpPath);
        }
        public void RestoreBackupFile(string path) {
            String backUpPath = path + ".bak";
            if (!_client.FileExists(backUpPath))
            {
                return;
            }
            if (_client.FileExists(path))
            {
                _client.DeleteFile(path);
            }
            _client.Rename(backUpPath, path);
        }
        public void UploadFile(string path, Stream localFile) {
            if (_client.FileExists(path))
            {
                _client.DeleteFile(path);
            }
            using (Stream ftpStream = _client.OpenWrite(path)) {
                byte[] buffer = new byte[8 * 1024];
                int count = localFile.Read(buffer, 0, buffer.Length);
                while (count > 0) {
                    ftpStream.Write(buffer, 0, count);
                    count = localFile.Read(buffer, 0, buffer.Length);
                }
            }
        }
    }
}
