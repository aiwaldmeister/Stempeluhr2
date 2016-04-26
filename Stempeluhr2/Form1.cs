using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Configuration;     //Zum lesen der Config-File
using MySql.Data;               //der MySql-Connector
using System.IO;                //zum schreiben der Logfiles
using System.Media;             //zum abspielen der Systemsounds

namespace Stempeluhr2
{
    public partial class Form1 : Form
    {


        string dbserverconf;
        string dbnameconf;
        string dbuserconf;
        string dbpwconf;

        string jahr;
        string monat;
        string tag;
        string stunde;
        string minute;
        string zeiteinheit;
        string sekunde;

        string status = "init";
        int countdown = 0;

        string activeuser = "";
        string activetask = "";
        string activetask_zeitbisher = "";


        MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
        MySql.Data.MySqlClient.MySqlCommand comm = new MySql.Data.MySqlClient.MySqlCommand();
        string logfilename;

        public Form1()
        {
            InitializeComponent();
        }
        private void init_logfile()
        {
            logfilename = DateTime.Now.Year.ToString("D4") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2") + ".log";
            log("Programm gestartet..........................................................................");
            log("Logfile geladen.");

        }
        private void log(String text)
        {
            using (StreamWriter file = new StreamWriter(logfilename, true))
            {
                file.WriteLine(DateTime.Now.ToLongTimeString() + ": " + text);
            }
            Console.WriteLine("Log: " + DateTime.Now.ToLongTimeString() + ": " + text);
        }
        private void init_config()
        {
            dbserverconf = System.Configuration.ConfigurationManager.AppSettings["dbserver"];
            dbnameconf = System.Configuration.ConfigurationManager.AppSettings["dbname"];
            dbuserconf = System.Configuration.ConfigurationManager.AppSettings["dbuser"];
            dbpwconf = System.Configuration.ConfigurationManager.AppSettings["dbpw"];

            log("Config-File gelesen, Datenbankinfos gesetzt.('" + dbserverconf + "','" + dbnameconf + "','" + dbuserconf + "','" + dbpwconf + "')");
        }
        private void init_db(string server, string database, string uid, string password)
        {
            conn.ConnectionString = "SERVER = " + server + "; " + "DATABASE = " + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            log("Datenbank ConnectionString initialisiert");
        }
        private bool open_db()
        {
            try
            {
                conn.Open();
                comm.Connection = conn;
                log("Datenbankverbindung geoeffnet. (" + conn.ToString() + ")");
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log("Fehler beim oeffnen der Datenbank...");
                log(ex.Message);
                return false;
            }
        }
        private bool close_db()
        {
            try
            {
                conn.Close();
                log("Datenbankverbindung geschlossen.");
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log("Fehler beim schliessen der Datenbank...");
                log(ex.Message);
                return false;
            }
        }
        private void message(string text)
        {
            MessageLabel.Text = text;
        }
        private void setCountdown(int sekunden)
        {
            countdown = sekunden * 10;
            countdownbar.Minimum = 1;
            countdownbar.Maximum = countdown;
            countdownbar.Value = countdown;

            countdownbar.Visible = true;
            countdowntimer.Enabled = true;

            log("countdown auf " + sekunden + " gesetzt");
        }
        private void stopCountdown()
        {
            countdowntimer.Enabled = false;
            countdownbar.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            //Logfile initialisieren
            init_logfile();

            //Config-laden
            init_config();

            //Datenbank initialisieren
            init_db(dbserverconf, dbnameconf, dbuserconf, dbpwconf);

            //Status Ready setzen
            setstatus("ready", "");

        }
        private void setstatus(string zielstatus, string statusmeldung)
        {
            //TODO Farbwerte und oder sounds einbauen.


            if (zielstatus == "ready")
            {
                stopCountdown();
                Codefeld.Text = "";
                MessageLabel.BackColor = Color.LightGreen;
                message("Bitte Personencode scannen");
                Anzeige.Text = "";
                Stempelliste.Visible = false;
                Detailanzeige.Visible = false;
                status = "ready";
                activeuser = "";
                activetask = "";
                Codefeld.Enabled = true;
                Codefeld.Focus();
                log("Status auf 'ready' gesetzt.");

            }
            else if (zielstatus == "eingeloggt")
            {
                Codefeld.Text = "";
                Codefeld.Enabled = true;
                MessageLabel.BackColor = Color.LightSkyBlue;
                if (activetask != "")
                {
                    Anzeige.Text = "Eingestempelt auf " + activetask + ". Bisher " + activetask_zeitbisher + " Zeiteinheiten";
                }else
                {
                    Anzeige.Text = "Nicht eingestempelt";
                }
                status = "eingeloggt";
                message(statusmeldung);
                log("Status auf 'eingeloggt' gesetzt. (" + statusmeldung + ")");
                setCountdown(10);

            }
            else if (zielstatus == "gestempelt")
            {
                Codefeld.Text = "";
                MessageLabel.BackColor = Color.LightGreen;
                message(statusmeldung);
                Anzeige.Text = "";
                status = "gestempelt";
                Stempelliste.Visible = false;
                Detailanzeige.Visible = false;
                SystemSounds.Hand.Play();
                activeuser = "";
                activetask = "";
                setCountdown(3);
                Codefeld.Enabled = true;
                Codefeld.Focus();
                log("Status auf 'gestempelt' gesetzt. (" + statusmeldung + ")");

            }
            else if (zielstatus == "error")
            {
                Codefeld.Enabled = false;
                MessageLabel.BackColor = Color.IndianRed;
                message(statusmeldung);
                Anzeige.Text = "";
                status = "error";
                activeuser = "";
                activetask = "";
                log("Status auf 'error' gesetzt. (" + statusmeldung + ")");
                setCountdown(2);

            }
            else if (zielstatus == "userinfos")
            {
                message(statusmeldung);
                //TODO details anzeigen
                Stempelliste.Visible = true;
                Detailanzeige.Visible = true;
                status = "userinfos";
                Anzeige.Text = "";
                Codefeld.Text = "";
                MessageLabel.BackColor = Color.LightSkyBlue;
                Codefeld.Focus();
                log("Status auf 'userinfos' gesetzt. (" + statusmeldung + ")");
                setCountdown(10);
            }

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            log("Programm wird beendet.......................................................................");
        }
        private void uhrtimer_Tick(object sender, EventArgs e)
        {
            //Zeitvariablen setzen
            jahr = DateTime.Now.Year.ToString("D4");
            monat = DateTime.Now.Month.ToString("D2");
            tag = DateTime.Now.Day.ToString("D2");
            stunde = DateTime.Now.Hour.ToString("D2");
            minute = DateTime.Now.Minute.ToString("D2");
            sekunde = DateTime.Now.Second.ToString("D2");
            int zeiteinheit_int = ((DateTime.Now.Minute * 60) + DateTime.Now.Second) / 36;
            zeiteinheit = zeiteinheit_int.ToString("D2");

            //Zeitanzeige aktualisieren
            Zeitanzeige.Text = stunde + ":" + minute + ":" + sekunde;
            Datumsanzeige.Text = DateTime.Now.ToShortDateString();

            //Debugausgabe auf Konsole
            //Console.WriteLine("Zeitwerte gesetzt... (jahr:" + jahr + " monat:" + monat + " tag:" + tag + " stunde:" + 
            //                    stunde + " minute:" +  minute + " sekunde:" + sekunde+ " zeiteinheit:" + zeiteinheit);

        }
        private void countdowntimer_Tick(object sender, EventArgs e)
        {
            if (countdown > 0)
            {   
                countdownbar.Value = countdown;
                countdown--;
            }
            else
            {
                stopCountdown();
                setstatus("ready", "");
            }
        }
        private void Codefeld_KeyPress(object sender, KeyPressEventArgs e)
        {
            //funktion ausgelagert zum KeyDown Event
        }
        private void codeeingabe(string code)
        {
            int tmpcode;    //wegwerfvariable fuer den out-parameter von tryparse

            //fehler abfangen
            if (code.Length != 6)
            {
                //fehlerhafter code
                setstatus("error", "Code '" + code + "' nicht 6-stellig ");

            }
            else if (!int.TryParse(code, out tmpcode))
            {

                setstatus("error", "Code '" + code + "' ist keine Zahl): ");
            }
            else if (code.StartsWith("999"))
            {
                //personencode erkannt... verarbeiten...
                progresscode(code, "person");

            }
            else if (code.StartsWith("888"))
            {
                if(code == "888888")
                {   //abstempelcode erkannt
                    progresscode(code, "abstempeln");
                }
                else
                {   //sondercode erkannt... verarbeiten...
                    progresscode(code, "sonder");
                }

            }
            else
            {
                //auftragscode erkannt... verarbeiten...
                progresscode(code, "auftrag");
            }
        }
        private void progresscode(string code, string typ)
        {
            if ((status == "ready") || (status == "gestempelt"))
            {
                if (typ == "person")
                {   //typ wie erwartet
                    einloggen(code);
                }else
                {   //unerwarteter codetyp (person erwartet)
                    setstatus("error", "Bitte zuerst einloggen.");
                }
            }else if ((status == "eingeloggt") || (status == "userinfos"))
            {
                if (typ == "person" && code == activeuser)
                {   //nochmal der gleiche personencode gescannt
                    showdetails();
                }else if(typ == "auftrag")
                {   //person auf auftrag stempeln
                    stempeln(activeuser, code);
                }else if(typ == "sonder")
                {   //sondercodes behandeln
                    stempeln(activeuser, code);
                }else if(typ == "abstempeln")
                {   //abstempeln
                    if (activetask != "")
                    {
                        abstempeln(activeuser, activetask);
                    }else
                    {
                        setstatus("error", "Nicht eingestempelt");
                    }
                }else
                {   //unerwarteter code
                    setstatus("error", "Unerwarteter Code");
                }
            }
        }
        private void stempeln(string user, string auftrag)
        {
            if(activetask != "")
            {
                abstempeln(user, activetask);
            }
            anstempeln(user, auftrag);           
        }
        private bool anstempeln(string user, string auftrag)
        {
            open_db();
            comm.CommandText = "INSERT INTO stamps (userid,task,art,jahr,monat,tag,stunde,minute,sekunde,dezimal,quelle) " +
                                "VALUES ('" + user + "','" + auftrag + "','an','" + jahr + "','" + monat + "','" +
                                tag + "','" + stunde + "','" + minute + "','" + sekunde + "','" + zeiteinheit + "','INSERT')";
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log(ex.Message);
            }

            comm.CommandText = "UPDATE user SET currenttask='" + auftrag + "' where userid = '" + user + "'";
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log(ex.Message);
            }

            close_db();
            setstatus("gestempelt", user + " auf Auftrag " + auftrag + " eingestempelt.");
            return true;
        }
        private bool abstempeln(string user, string auftrag)
        {
            open_db();
            comm.CommandText = "INSERT INTO stamps (userid,task,art,jahr,monat,tag,stunde,minute,sekunde,dezimal,quelle) "+
                                "VALUES ('"+ user + "','" + auftrag +"','ab','" + jahr +"','" + monat +"','" + 
                                tag +"','" + stunde +"','" + minute + "','" + sekunde +"','" + zeiteinheit + "','INSERT')";
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log(ex.Message);
            }

            comm.CommandText = "UPDATE user SET currenttask='' where userid = '" + user + "'";
            try
            {
                comm.ExecuteNonQuery();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log(ex.Message);
            }

            close_db();
            setstatus("gestempelt", user + " von Auftrag " + auftrag + " ausgestempelt.");
            return true;
        }
        private bool showdetails()
        {
            string name = "";
            string zeitkonto = "";
            string verrechenbare_zeit = "";
            string resturlaub = "";
            string planurlaub = "";

            try
            {
                open_db();
                comm.CommandText = "SELECT * FROM user where userid='" + activeuser + "'";
                MySql.Data.MySqlClient.MySqlDataReader Reader = comm.ExecuteReader();

                //Detailanzeige fuellen
                Reader.Read();
                name = Reader["vorname"] + " " + Reader["name"] + "";
                zeitkonto = Reader["zeitkonto"] + "";
                verrechenbare_zeit = Reader["verrechenbare_zeit"] + "";
                resturlaub = Reader["resturlaub"] + "";
                planurlaub = Reader["geplante_urlaubstage"] + "";
                Reader.Close();
                Detailanzeige.Text = "Zeitkonto\r\n" + zeitkonto + "\r\n\r\n" +
                                     "Verrechnete Zeit\r\n" + verrechenbare_zeit + "\r\n\r\n" +
                                    "Resturlaub\r\n" + resturlaub + "\r\n\r\n" +
                                    "Geplanter Urlaub\r\n" + planurlaub;
                
                //Stempelliste fuellen
                Stempelliste.Items.Clear();
                comm.CommandText = "SELECT * FROM stamps where userid='" + activeuser +
                                    "' and jahr ='" + jahr + "' and monat = '" + monat + "' and tag = '" + tag +
                                    "' and art in ('ab','an')";
                Reader = comm.ExecuteReader();

                while (Reader.Read())
                {
                    string dbtask = Reader["task"] + "";
                    string dbstunde = Reader["stunde"] + "";
                    string dbminute = Reader["minute"] + "";
                    string dbart = Reader["art"] + "";

                    string[] row = {dbtask + " " + dbstunde + ":" + dbminute};
                    var listViewItem = new ListViewItem(row);
                    if(dbart == "an")
                    {
                        listViewItem.ImageIndex = 0;
                    }else if(dbart == "ab")
                    {
                        listViewItem.ImageIndex = 1;
                    }
                    Stempelliste.Items.Add(listViewItem);
                }
                close_db();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log(ex.Message);
            }
            
            setstatus("userinfos", "Details zu " + name);
            return true;
        }
        private bool einloggen(string usercode)
        {
            
            int count = -1;

            string username = "";
            string currenttask = "";

            //prüfen ob person angelegt ist
            open_db();
            comm.CommandText = "SELECT count(*) FROM user where userid='" + usercode + "'";
            try
            {
                count = int.Parse(comm.ExecuteScalar()+"");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log(ex.Message);
            }
            close_db();

            if(count == 1)
            {   //User gefunden... einloggen
                open_db();
                comm.CommandText = "SELECT name, vorname, currenttask FROM user where userid='" + usercode + "'";
                MySql.Data.MySqlClient.MySqlDataReader Reader = comm.ExecuteReader();
                //Read the data and store them in the list
                Reader.Read();
                username = Reader["vorname"] + " " +  Reader["name"] + "";
                currenttask = Reader["currenttask"] + "";
                Reader.Close();

                activeuser = usercode;
                activetask = currenttask;

                if(currenttask!= "")
                {//Zeit ermitteln, die die Person schon auf dem Auftrag gearbeitet hat

                    int summe_abstempelungen = 0;
                    int summe_anstempelungen = 0;
                    string sumstring = "";
                    comm.CommandText = "SELECT ((sum(stunde) * 100)  + sum(dezimal)) FROM stamps WHERE userid = '"
                                    + activeuser + "' and task = '" + currenttask + "' and art='ab'" ;
                    try
                    {  
                        sumstring = comm.ExecuteScalar() + "";
                        if(sumstring == "")
                        {
                            sumstring = "0";
                        }
                        summe_abstempelungen = int.Parse(sumstring);
                        summe_abstempelungen = summe_abstempelungen + int.Parse(stunde) * 100 + int.Parse(zeiteinheit) ;
                        log("Summe abstempelungen: " + summe_abstempelungen);
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        log(ex.Message);
                    }

                    comm.CommandText = "SELECT ((sum(stunde) * 100)  + sum(dezimal)) FROM stamps WHERE userid = '"
                                    + activeuser + "' and task = '" + currenttask + "' and art='an'";
                    try
                    {
                        sumstring = comm.ExecuteScalar() + "";
                        if (sumstring == "")
                        {
                            sumstring = "0";
                        }
                        summe_anstempelungen = int.Parse(sumstring);
                        log("Summe anstempelungen: " + summe_abstempelungen);
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        log(ex.Message);
                    }
                    int activetask_zeitbisher_stunden = (summe_abstempelungen - summe_anstempelungen) / 100;
                    int activetask_zeitbisher_decimal = (summe_abstempelungen - summe_anstempelungen) % 100;
                    activetask_zeitbisher = activetask_zeitbisher_stunden.ToString() + "," + activetask_zeitbisher_decimal.ToString("D2");

                    //debugausgaben für die Berechnung
                    //log("summe abstempelungen: " + summe_abstempelungen);
                    //log("summe anstempelungen: " + summe_anstempelungen);
                    //log("stunden bisher: " + activetask_zeitbisher_stunden);
                    //log("dezimal bisher: " + activetask_zeitbisher_decimal);
                    //log("zeitbisher: " + activetask_zeitbisher);

                }

                


                setstatus("eingeloggt", "Angemeldet als " + username);

                close_db();
                return true;

            }else
            {   //User nicht gefunden
                setstatus("error", "Person nicht gefunden");
                return false;
            }


        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Codefeld_KeyDown(object sender, KeyEventArgs e)
        {
            //Tastendruck auswerten
            if (e.KeyCode == Keys.Enter)
            {
                string code = Codefeld.Text;
                codeeingabe(code);
                e.SuppressKeyPress = true;
            }
        }
    }
}

