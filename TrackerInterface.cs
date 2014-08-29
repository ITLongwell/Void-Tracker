using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Security.Cryptography;

namespace VoidTracker
{
    public partial class TrackerInterface : Form
    {
        #region Data

        enum RecordState
        {
            None,
            Started,
            Stopped
        }

        static RecordState state = RecordState.None;

        static Dictionary<DateTime, DateTime> intervals = new Dictionary<DateTime, DateTime>();

        static string directoryPath = "C:\\VoidTracker\\";
        static string textPath = Path.Combine(directoryPath, "logsheet.txt");

        static string xmlName = "New Log Sheet";
        static string xmlPath = Path.Combine(directoryPath, xmlName);

        static string settingsPath = Path.Combine(directoryPath, "settings.xml");

        protected static string encryptionKey = "25012501250125012501250125012501";
        protected static string iVector = "1234567887654321";

        static bool cached = false; static TimeSpan totalTime; static bool secureLogging = false;

        DateTime startTime; DateTime endTime;

        static bool activeLogging = false;
        static Point lastMousePosition = new Point();

        #endregion

        //Constructor
        public TrackerInterface()
        {
            InitializeComponent(); QuerySettings();
            FileNameEntry.Text = xmlName;
            PathTextBox.Text = directoryPath;          
        }

        #region Functions

        void CreateNewLog(bool overwrite)
        {
            if (!(Directory.Exists(directoryPath)))
                Directory.CreateDirectory(directoryPath);

            xmlName = FileNameEntry.Text;
            xmlPath = Path.Combine(directoryPath, xmlName.Replace(" ", "_") + ".xml");

            if (File.Exists(xmlPath) && !overwrite)
            {
                SendMessage("Error: File with matching name already exists."); return;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(xmlPath))
                {
                    XmlTextWriter xml = new XmlTextWriter(writer);

                    xml.Formatting = Formatting.Indented;
                    xml.IndentChar = '\t';
                    xml.Indentation = 1;

                    xml.WriteStartDocument(true);
                    xml.WriteStartElement("log_file");
                    xml.WriteAttributeString("Name", xmlName);
                    xml.WriteAttributeString("Encrypted", secureLogging.ToString());

                    if (overwrite)
                    {
                        foreach (KeyValuePair<DateTime, DateTime> interval in intervals)
                        {
                            string key = interval.Key.ToString();
                            string val = interval.Value.ToString();

                            if (secureLogging)
                            {
                                xml.WriteStartElement("interval");
                                xml.WriteAttributeString("start", ReturnEncrypted(key));
                                xml.WriteAttributeString("stop", ReturnEncrypted(val));
                                xml.WriteEndElement();
                            }

                            else
                            {
                                xml.WriteStartElement("interval");
                                xml.WriteAttributeString("start", key);
                                xml.WriteAttributeString("stop", val);
                                xml.WriteEndElement();
                            }
                        }
                    }

                    xml.WriteEndElement();
                    xml.WriteEndDocument();
                    xml.Close();
                }

                if (!overwrite)
                {
                    SendMessage("Success: You have created a new log file.");
                    totalTime = TimeSpan.FromSeconds(0);
                    Time.Text = totalTime.ToString("hh' : 'mm' : 'ss");
                    intervals.Clear();
                }

                ModifySettings(); if(!secureLogging) FormTextFile();
            }

            catch(ArgumentException e) { 
                SendMessage("Error: Unable to create file " + xmlPath);
                SendMessage(e.Message);
            }
        }

        void SendMessage(string msg)
        {
            TextDisplay.Text += "[" + msg + "]";
            TextDisplay.Text += "\n";
            TextDisplay.SelectionStart = TextDisplay.Text.Length;
            TextDisplay.ScrollToCaret();
        }

        void ModifySettings()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(settingsPath))
                {
                    XmlTextWriter xml = new XmlTextWriter(writer);

                    xml.Formatting = Formatting.Indented;
                    xml.IndentChar = '\t';
                    xml.Indentation = 1;

                    xml.WriteStartDocument(true);
                    xml.WriteStartElement("settings_file");
                    xml.WriteAttributeString("path", directoryPath);
                    xml.WriteAttributeString("cache", xmlPath);
                    xml.WriteAttributeString("secure", secureLogging.ToString());
                    xml.WriteAttributeString("active", activeLogging.ToString());
                    xml.WriteEndElement();
                    xml.WriteEndDocument();
                    xml.Close();
                }
            }
            catch { }
        }

        void QuerySettings()
        {
            if(!(File.Exists(settingsPath)))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(settingsPath))
                    {
                        XmlTextWriter xml = new XmlTextWriter(writer);

                        xml.Formatting = Formatting.Indented;
                        xml.IndentChar = '\t';
                        xml.Indentation = 1;

                        xml.WriteStartDocument(true);
                        xml.WriteStartElement("settings_file");
                        xml.WriteAttributeString("path", directoryPath);
                        xml.WriteAttributeString("cache", xmlPath);
                        xml.WriteAttributeString("secure", secureLogging.ToString());
                        xml.WriteAttributeString("active", activeLogging.ToString());
                        xml.WriteEndElement();
                        xml.WriteEndDocument();
                        xml.Close();
                    } 
                }

                catch { }
            }

            else if (File.Exists(settingsPath))
            {
                XmlDocument doc = new XmlDocument();
                XmlElement root;

                try
                {
                    doc.Load(settingsPath);
                    root = doc["settings_file"];

                    directoryPath = root.GetAttribute("path");
                    xmlPath = root.GetAttribute("cache");

                    secureLogging = Convert.ToBoolean(root.GetAttribute("secure"));
                    activeLogging = Convert.ToBoolean(root.GetAttribute("active"));

                    if (activeLogging)
                        ActiveLog_checkBox.Checked = true;

                    if (secureLogging)
                        SecureLog_checkBox.Checked = true;

                    doc.Load(xmlPath); root = doc["log_file"];
                    xmlName = FileNameEntry.Text = root.GetAttribute("Name");

                    cached = true;

                    foreach (XmlElement element in root.GetElementsByTagName("interval"))
                    {
                        string startElement = element.GetAttribute("start");
                        string stopElement = element.GetAttribute("stop");

                        string decryptedStart = ReturnDecrypted(startElement);
                        string decryptedStop = ReturnDecrypted(stopElement);

                        DateTime start = DateTime.Parse(decryptedStart);
                        DateTime stop = DateTime.Parse(decryptedStop);

                        intervals.Add(start, stop); totalTime += stop - start;
                        Time.Text = totalTime.ToString("hh' : 'mm' : 'ss");
                    }
                }

                catch { }
            }
        }

        void FormTextFile()
        {
            if (!(Directory.Exists(directoryPath)))
                Directory.CreateDirectory(directoryPath);

            try
            {   //FileStream stream = new FileStream(fullPath, FileMode.Open, FileAccess.Write, FileShare.Write); ??

                TextWriter writer = new StreamWriter(textPath);

                writer.WriteLine("[" + xmlName + " initialized @: " + DateTime.Now.ToString() + "]");

                foreach (KeyValuePair<DateTime, DateTime> interval in intervals)
                {
                    DateTime start = interval.Key;
                    DateTime end = interval.Value;

                    TimeSpan span = end - start;

                    writer.WriteLine("[Shift Logged: (" + start.ToString() + " to " + end.ToString() + ") Total: (" + span.ToString("hh' : 'mm' : 'ss") + ")]");
                }

                writer.Close(); writer.Dispose();
            }

            catch { SendMessage("Error: Unable to write (" + textPath + ")."); }

        }

        void LoadFromXML()
        {
            using (OpenFileDialog fileBrowser = new OpenFileDialog())
            {
                fileBrowser.FileName = directoryPath;
                if (fileBrowser.ShowDialog() == DialogResult.OK)
                {
                    string extension = System.IO.Path.GetExtension(fileBrowser.FileName);

                    if (extension == ".xml")
                    {
                        xmlPath = fileBrowser.FileName;

                        if (File.Exists(xmlPath))
                        {
                            XmlDocument doc = new XmlDocument();
                            XmlElement root;

                            try
                            {
                                doc.Load(xmlPath);
                                root = doc["log_file"];

                                xmlName = FileNameEntry.Text = root.GetAttribute("Name");
                                bool encrypted = Convert.ToBoolean(root.GetAttribute("Encrypted"));
                                xmlPath = Path.Combine(directoryPath, xmlName.Replace(" ", "_") + ".xml");

                                ModifySettings();

                                intervals.Clear(); int count = 0; totalTime = TimeSpan.FromSeconds(0);

                                foreach (XmlElement element in root.GetElementsByTagName("interval"))
                                {
                                    string startElement = element.GetAttribute("start");
                                    string stopElement = element.GetAttribute("stop");

                                    DateTime start;
                                    DateTime stop;

                                    if (encrypted)
                                    {
                                        string decryptedStart = ReturnDecrypted(startElement);
                                        string decryptedStop = ReturnDecrypted(stopElement);

                                        start = DateTime.Parse(decryptedStart);
                                        stop = DateTime.Parse(decryptedStop);
                                    }

                                    else
                                    {
                                        start = DateTime.Parse(startElement);
                                        stop = DateTime.Parse(stopElement);
                                    }

                                    intervals.Add(start, stop); count++;
                                    totalTime += stop - start;
                                    Time.Text = totalTime.ToString("hh' : 'mm' : 'ss");
                                }

                                SendMessage(String.Format("Loading ({0}) {2} from {1}", count, xmlPath, count > 1 ? "Intervals" : "Interval"));
                            }

                            catch (ArgumentException e) { SendMessage("Error: Unable to parse xml file."); SendMessage(e.Message); }
                        }
                    }

                    else SendMessage("Error: You must select  an .xml file.");

                    fileBrowser.Dispose();
                }
            }
        }

        void BeginShift()
        {
            if (state != RecordState.Started)
            {
                state = RecordState.Started;
                startTime = DateTime.Now;

                SendMessage("Logging Initialized @: " + startTime);

                if (activeLogging)
                {
                    MovementTimer timer = new MovementTimer(this);
                    lastMousePosition = Cursor.Position;
                }
            }

            else SendMessage("Error: Logging Already in Progress!");
        }

        void EndShift()
        {
            if (state == RecordState.Started)
            {
                state = RecordState.Stopped;
                endTime = DateTime.Now;
                totalTime += endTime - startTime;

                intervals.Add(startTime, endTime);

                SendMessage("Logging Complete @: " + endTime);
                CreateNewLog(true);

                TextDisplay.SelectionStart = TextDisplay.Text.Length;
                TextDisplay.ScrollToCaret();

                Time.Text = totalTime.ToString("hh' : 'mm' : 'ss");

                if (MovementTimer.Ticker != null)
                {
                    MovementTimer.Ticker.Stop();
                    MovementTimer.Ticker.Dispose();
                }
            }
        }

        #region Encryption/Decryption

        private static string ReturnEncrypted(string toEncrypt)
        {
            byte[] plainTextBytes = ASCIIEncoding.ASCII.GetBytes(toEncrypt);

            using (AesCryptoServiceProvider rijndael = new AesCryptoServiceProvider())
            {
                rijndael.BlockSize = 128;
                rijndael.KeySize = 256;

                rijndael.Key = ASCIIEncoding.ASCII.GetBytes(encryptionKey);
                rijndael.IV = ASCIIEncoding.ASCII.GetBytes(iVector);

                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Mode = CipherMode.CBC;

                ICryptoTransform encryptor = rijndael.CreateEncryptor(rijndael.Key, rijndael.IV);
                byte[] encryptedText = encryptor.TransformFinalBlock(plainTextBytes, 0, plainTextBytes.Length);

                return Convert.ToBase64String(encryptedText);
            }
        }

        private static string ReturnDecrypted(string toDecrypt)
        {
            byte[] encryptedBytes = Convert.FromBase64String(toDecrypt);

            using (AesCryptoServiceProvider rijndael = new AesCryptoServiceProvider())
            {
                rijndael.BlockSize = 128;
                rijndael.KeySize = 256;

                rijndael.Key = ASCIIEncoding.ASCII.GetBytes(encryptionKey);
                rijndael.IV = ASCIIEncoding.ASCII.GetBytes(iVector);

                rijndael.Padding = PaddingMode.PKCS7;
                rijndael.Mode = CipherMode.CBC;

                ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);
                byte[] decryptedText = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);

                return ASCIIEncoding.ASCII.GetString(decryptedText);
            }
        }

        #endregion

        #endregion

        #region Events
        private void Tracker_Load       (object sender, EventArgs e)
        {
            if (cached)
                SendMessage("Previous Log Loaded from " + xmlPath);
        }

        private void PathButton_Click   (object sender, EventArgs e)
        {
            folderBrowser.SelectedPath = directoryPath;

            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                directoryPath = folderBrowser.SelectedPath;
                PathTextBox.Text = directoryPath;
                folderBrowser.Dispose();
            }
        }

        private void loadButton_Click   (object sender, EventArgs e)
        {
            LoadFromXML();
        }

        private void newButton_Click    (object sender, EventArgs e)
        {
            CreateNewLog(false);
        }

        private void StartButton_Click  (object sender, EventArgs e)
        {
            BeginShift();
        }

        private void StopButton_Click   (object sender, EventArgs e)
        {
            EndShift();
        }

        private void ActiveLog_checkBox_Changed (object sender, EventArgs e)
        {
            if (ActiveLog_checkBox.Checked)
            {
                if (state != RecordState.Started)
                {
                    activeLogging = true;
                    SendMessage("Active Logging Enabled...");
                }

                else
                {
                    SendMessage("Error: Unable to activate while logging in-progress.");
                    ActiveLog_checkBox.Checked = false;
                }
            }

            else
            {
                activeLogging = false;
                SendMessage("Active Logging Disabled..");
            }
        }

        private void SecureLog_checkBox_Changed (object sender, EventArgs e)
        {
            if (SecureLog_checkBox.Checked)
            {
                secureLogging = true;
                SendMessage("Secure Logging Enabled...");
            }

            else
            {
                secureLogging = false;
                SendMessage("Secure Logging Disabled...");
            }
        }

        #endregion

        private class MovementTimer
        {
            static Timer timer;
            static TrackerInterface tracker;

            public static Timer Ticker
            {
                get { return timer; }
                set { timer = value; }
            }

            static int Minutes(int i)
            {
                return i * 60000;
            }

            public MovementTimer(TrackerInterface t)
            {
                tracker = t;

                timer = new Timer();
                timer.Interval = Minutes(5);

                timer.Tick += new EventHandler(OnTick);
                timer.Start();
            }

            void OnTick(object sender, EventArgs e)
            {
                QueryPosition();
            }

            void QueryPosition()
            {
                if (Cursor.Position != lastMousePosition)
                {
                    lastMousePosition = Cursor.Position;
                }

                else if (Cursor.Position == lastMousePosition)
                {
                    if (tracker != null)
                    {
                        tracker.SendMessage("#! Logging Suspended Due to Inactivity..");

                        tracker.EndShift();
                        ((Form)tracker).Activate();

                        timer.Stop(); timer.Dispose();                     
                    }
                }
            }
        }
    }
}
