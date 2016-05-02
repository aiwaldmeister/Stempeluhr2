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


        string dbserverconf_global;
        string dbnameconf_global;
        string dbuserconf_global;
        string dbpwconf_global;

        string jahr_global;
        string monat_global;
        string tag_global;
        string stunde_global;
        string minute_global;
        string zeiteinheit_global;
        string sekunde_global;

        string status_global = "init";
        int countdown_global = 0;

        string activeuser_global = "";
        string activetask_global = "";
        string activetask_zeitbisher_global = "";
        string userwarnung_global = "";


        MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection();
        MySql.Data.MySqlClient.MySqlCommand comm = new MySql.Data.MySqlClient.MySqlCommand();
        System.Media.SoundPlayer Sound = new System.Media.SoundPlayer();

        //TODO passende soundfiles besorgen und zur exe legen

        string logfilename_global;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            //Logfile initialisieren
            init_logfile();

            //Config-laden
            init_config();

            //Datenbank initialisieren
            init_db(dbserverconf_global, dbnameconf_global, dbuserconf_global, dbpwconf_global);

            //Status Ready setzen
            setstatus("ready", "");

        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            log("Programm wird beendet.......................................................................");
        }
        private void Codefeld_KeyPress(object sender, KeyPressEventArgs e)
        {
            //funktion ausgelagert zum KeyDown Event
        }
        private void Codefeld_KeyDown(object sender, KeyEventArgs e)
        {   //Die Taste wird vor dem eigentlichen Tastendruck abgefangen um das Enter-'Ding'-Geräusch zu unterdrücken.
            if (e.KeyCode == Keys.Enter)
            {
                string code = Codefeld.Text;
                codeeingabe(code);
                e.SuppressKeyPress = true;  
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void init_logfile()
        {
            logfilename_global = DateTime.Now.Year.ToString("D4") + DateTime.Now.Month.ToString("D2") + DateTime.Now.Day.ToString("D2") + ".log";
            log("Programm gestartet..........................................................................");
            log("Logfile geladen.");

        }
        private void init_config()
        {
            dbserverconf_global = System.Configuration.ConfigurationManager.AppSettings["dbserver"];
            dbnameconf_global = System.Configuration.ConfigurationManager.AppSettings["dbname"];
            dbuserconf_global = System.Configuration.ConfigurationManager.AppSettings["dbuser"];
            dbpwconf_global = System.Configuration.ConfigurationManager.AppSettings["dbpw"];

            log("Config-File gelesen, Datenbankinfos gesetzt.('" + dbserverconf_global + "','" + dbnameconf_global + "','" + dbuserconf_global + "','" + dbpwconf_global + "')");
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
        private void log(String text)
        {
            using (StreamWriter file = new StreamWriter(logfilename_global, true))
            {
                file.WriteLine(DateTime.Now.ToLongTimeString() + ": " + text);
            }
            Console.WriteLine("Log: " + DateTime.Now.ToLongTimeString() + ": " + text);
        }
        private void message(string text, Color farbe)
        {
            MessageLabel.Text = text;
            MessageLabel.BackColor = farbe;
        }
        private void setCountdown(int sekunden)
        {
            countdown_global = sekunden * 10;
            countdownbar.Minimum = 1;
            countdownbar.Maximum = countdown_global;
            countdownbar.Value = countdown_global;

            countdownbar.Visible = true;
            countdowntimer.Enabled = true;

            log("countdown auf " + sekunden + " gesetzt");
        }
        private void stopCountdown()
        {
            countdowntimer.Enabled = false;
            countdownbar.Visible = false;
        }
        private void playsound(string soundart)
        {
            if(soundart == "error")
            {
                Sound.SoundLocation = "error.wav";
                try
                {
                    Sound.Play();
                }catch (Exception){ log("soundfile error.wav nicht gefunden"); }
                
            }else if(soundart == "erfolg")
            {
                Sound.SoundLocation = "erfolg.wav";
                try
                {
                    Sound.Play();
                }
                catch (Exception) { log("soundfile erfolg.wav nicht gefunden"); }
            }else
            {
                //Sound.SoundLocation = "default.wav";
                //Sound.Play;
            }

        }
        private void setstatus(string zielstatus, string statusmeldung)
        {
            
            if (zielstatus == "ready")
            {
                stopCountdown();
                Codefeld.Text = "";
                message("Bitte Personencode scannen",Color.LightGreen);
                Anzeige.Text = "";
                Stempelliste.Visible = false;
                Detailanzeige.Visible = false;
                status_global = "ready";
                activeuser_global = "";
                activetask_global = "";
                Codefeld.Enabled = true;
                Codefeld.Focus();
                log("Status auf 'ready' gesetzt.");

            }
            else if (zielstatus == "eingeloggt")
            {
                Codefeld.Text = "";
                Codefeld.Enabled = true;
                if (activetask_global != "")
                {
                    Anzeige.Text = "Eingestempelt auf " + activetask_global + ". Bisher " + activetask_zeitbisher_global + " Zeiteinheiten";
                }else
                {
                    Anzeige.Text = "Nicht eingestempelt";
                }
                if (userwarnung_global != "")
                {
                    Anzeige.Text = Anzeige.Text + "\r\n\r\n" + userwarnung_global;
                }
                status_global = "eingeloggt";
                message(statusmeldung, Color.LightSkyBlue);
                log("Status auf 'eingeloggt' gesetzt. (" + statusmeldung + ")");
                setCountdown(10);

            }
            else if (zielstatus == "gestempelt")
            {
                Codefeld.Text = "";
                message(statusmeldung, Color.LightGreen);
                Anzeige.Text = "";
                status_global = "gestempelt";
                Stempelliste.Visible = false;
                Detailanzeige.Visible = false;
                activeuser_global = "";
                activetask_global = "";
                setCountdown(3);
                Codefeld.Enabled = true;
                Codefeld.Focus();
                playsound("erfolg");
                log("Status auf 'gestempelt' gesetzt. (" + statusmeldung + ")");

            }
            else if (zielstatus == "error")
            {
                Codefeld.Enabled = false;
                message(statusmeldung, Color.IndianRed);
                Anzeige.Text = "";
                status_global = "error";
                activeuser_global = "";
                activetask_global = "";
                log("Status auf 'error' gesetzt. (" + statusmeldung + ")");
                setCountdown(2);
                playsound("erfolg");

            }
            else if (zielstatus == "userinfos")
            {
                message(statusmeldung, Color.LightSkyBlue);
                Stempelliste.Visible = true;
                Detailanzeige.Visible = true;
                status_global = "userinfos";
                Anzeige.Text = "";
                Codefeld.Text = "";
                Codefeld.Focus();
                log("Status auf 'userinfos' gesetzt. (" + statusmeldung + ")");
                setCountdown(10);
            }

        }
        private void uhrtimer_Tick(object sender, EventArgs e)
        {
            //Zeitvariablen setzen
            jahr_global = DateTime.Now.Year.ToString("D4");
            monat_global = DateTime.Now.Month.ToString("D2");
            tag_global = DateTime.Now.Day.ToString("D2");
            stunde_global = DateTime.Now.Hour.ToString("D2");
            minute_global = DateTime.Now.Minute.ToString("D2");
            sekunde_global = DateTime.Now.Second.ToString("D2");
            int zeiteinheit_int = ((DateTime.Now.Minute * 60) + DateTime.Now.Second) / 36;
            zeiteinheit_global = zeiteinheit_int.ToString("D2");

            //Zeitanzeige aktualisieren
            Zeitanzeige.Text = stunde_global + ":" + minute_global + ":" + sekunde_global;
            Datumsanzeige.Text = DateTime.Now.ToShortDateString();

            //Debugausgabe auf Konsole
            //Console.WriteLine("Zeitwerte gesetzt... (jahr:" + jahr + " monat:" + monat + " tag:" + tag + " stunde:" + 
            //                    stunde + " minute:" +  minute + " sekunde:" + sekunde+ " zeiteinheit:" + zeiteinheit);

        }
        private void countdowntimer_Tick(object sender, EventArgs e)
        {
            if (countdown_global > 0)
            {   
                countdownbar.Value = countdown_global;
                countdown_global--;
            }
            else
            {
                stopCountdown();
                setstatus("ready", "");
            }
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
            if ((status_global == "ready") || (status_global == "gestempelt"))
            {
                if (typ == "person")
                {   //typ wie erwartet
                    einloggen(code);
                }else
                {   //unerwarteter codetyp (person erwartet)
                    setstatus("error", "Bitte zuerst einloggen.");
                }
            }else if ((status_global == "eingeloggt") || (status_global == "userinfos"))
            {
                if (typ == "person" && code == activeuser_global)
                {   //nochmal der gleiche personencode gescannt
                    showdetails();
                }else if(typ == "auftrag")
                {   //person auf auftrag stempeln
                    stempeln(activeuser_global, code);
                }else if(typ == "sonder")
                {   //sondercodes behandeln
                    stempeln(activeuser_global, code);
                }else if(typ == "abstempeln")
                {   //abstempeln
                    if (activetask_global != "")
                    {
                        abstempeln(activeuser_global, activetask_global);
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
            if(activetask_global != "")
            {
                abstempeln(user, activetask_global);
            }
            anstempeln(user, auftrag);           
        }
        private bool anstempeln(string user, string auftrag)
        {
            open_db();
            comm.CommandText = "INSERT INTO stamps (userid,task,art,jahr,monat,tag,stunde,minute,sekunde,dezimal,quelle) " +
                                "VALUES ('" + user + "','" + auftrag + "','an','" + jahr_global + "','" + monat_global + "','" +
                                tag_global + "','" + stunde_global + "','" + minute_global + "','" + sekunde_global + "','" + zeiteinheit_global + "','INSERT')";
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
                                "VALUES ('"+ user + "','" + auftrag +"','ab','" + jahr_global +"','" + monat_global +"','" + 
                                tag_global +"','" + stunde_global +"','" + minute_global + "','" + sekunde_global +"','" + zeiteinheit_global + "','stempeluhr')";
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
                comm.CommandText = "SELECT * FROM user where userid='" + activeuser_global + "'";
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
                comm.CommandText = "SELECT * FROM stamps where userid='" + activeuser_global +
                                    "' and jahr ='" + jahr_global + "' and monat = '" + monat_global + "' and tag = '" + tag_global +
                                    "' and art in ('ab','an')";
                Reader = comm.ExecuteReader();

                while (Reader.Read())
                {
                    string dbtask = Reader["task"] + "";
                    string dbstunde = Reader["stunde"] + "";
                    string dbminute = Reader["minute"] + "";
                    string dbart = Reader["art"] + "";
                    bool dbstorniert = bool.Parse(Reader["storniert"] + "");

                    string[] row = {dbtask + " " + dbstunde + ":" + dbminute};
                    var listViewItem = new ListViewItem(row);
                    if(dbart == "an")
                    {
                        listViewItem.ImageIndex = 0;
                    }else if(dbart == "ab")
                    {
                        listViewItem.ImageIndex = 1;
                    }
                    if (dbstorniert)
                    {
                        //TODO drittes Icon für stornierte Stempelungen einfügen
                        //listViewItem.ImageIndex = 2;
                    }
                    Stempelliste.Items.Add(listViewItem);
                }
                Reader.Close();
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
            userwarnung_global = "";
            MySql.Data.MySqlClient.MySqlDataReader Reader;
            int count = -1;

            string username = "";
            string currenttask = "";
            string stempelfehler = "";
            int autostempelungen = 0;

            //prüfen ob person angelegt ist
            open_db();
            comm.CommandText = "SELECT count(*) FROM user where userid='" + usercode + "'";
            try
            {
                count = int.Parse(comm.ExecuteScalar() + "");
            }
            catch (Exception ex)
            {
                log(ex.Message);
            }
            close_db();

            if(count == 1)
            {   //User gefunden... wartungen durchführen und einloggen
                wartungslauf(usercode);

                //Die wichtigsten Infos zum User aus der Datenbank holen
                open_db();
                comm.CommandText = "SELECT name, vorname, currenttask, stempelfehler, FROM user where userid='" + usercode + "'";
                Reader = comm.ExecuteReader();
                Reader.Read();
                username = Reader["vorname"] + " " +  Reader["name"] + "";
                currenttask = Reader["currenttask"] + "";
                stempelfehler = Reader["stempelfehler"] + "";
                Reader.Close();

                activeuser_global = usercode;
                activetask_global = currenttask;

                comm.CommandText = "SELECT count(*) FROM stamps WHERE userid='" + usercode + "' AND quelle='wartung' AND storniert = false";
                try
                {
                    autostempelungen = int.Parse(comm.ExecuteScalar() + "");
                }
                catch (Exception ex) { log(ex.Message); }

                if(autostempelungen > 0)
                {
                    userwarnung_global = "Unkorrigierte automatische Abstempelungen vorhanden!";
                }
                
                if(stempelfehler == "true")
                {
                    if(userwarnung_global != "") userwarnung_global = userwarnung_global + "\r\n";
                    userwarnung_global = userwarnung_global + "Fehlerhafte Stempelungen -> Zeitberechnung nicht möglich!";
                }
                if (userwarnung_global != "") userwarnung_global = userwarnung_global + "\r\n Bitte im Büro korrigieren lassen";


                if (currenttask!= "")
                {//Zeit ermitteln, die die Person schon auf dem Auftrag gearbeitet hat
                    berechne_activetask_zeitbisher_global();
                }

                setstatus("eingeloggt", "Angemeldet als " + username);

                close_db();
                return true;

            }else if (count == 0)
            {   //User nicht gefunden
                setstatus("error", "Person nicht gefunden");
                return false;
            }else
            {//Datenbankergebnis weder 1 noch 0 -> vermutlich besteht garkeine Datenbankverbindung
                setstatus("error", "Datenbankverbindung prüfen");
                return false;
            }


        }
        private void berechne_activetask_zeitbisher_global()
        {
            int summe_abstempelungen = 0;
            int summe_anstempelungen = 0;
            string sumstring = "";
            comm.CommandText = "SELECT ((sum(stunde) * 100)  + sum(dezimal)) FROM stamps WHERE userid = '"
                            + activeuser_global + "' and task = '" + activetask_global + "' and art='ab' AND storniert = false";
            try
            {
                sumstring = comm.ExecuteScalar() + "";
                if (sumstring == "")
                {
                    sumstring = "0";
                }
                summe_abstempelungen = int.Parse(sumstring);
                summe_abstempelungen = summe_abstempelungen + int.Parse(stunde_global) * 100 + int.Parse(zeiteinheit_global);
                log("Summe abstempelungen: " + summe_abstempelungen);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                log(ex.Message);
            }

            comm.CommandText = "SELECT ((sum(stunde) * 100)  + sum(dezimal)) FROM stamps WHERE userid = '"
                            + activeuser_global + "' and task = '" + activetask_global + "' and art='an' AND storniert = false";
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
            activetask_zeitbisher_global = activetask_zeitbisher_stunden.ToString() + "," + activetask_zeitbisher_decimal.ToString("D2");
        }
        private void wartungslauf(String usercode)
        {
            MySql.Data.MySqlClient.MySqlDataReader Reader;
            int stempelungenheute = 0;
            bool fehlerflag = false;
            comm.CommandText = "SELECT count(*) FROM stamps where userid='" + usercode + "' AND tag='" + tag_global + "' AND monat='" + monat_global + "' AND jahr = '" + jahr_global + "'";
            try
            {
                stempelungenheute = int.Parse(comm.ExecuteScalar() + "");
            }
            catch (Exception ex) { log(ex.Message); }

            if (stempelungenheute == 0)
            {   //es gab noch keine stempelungen für diese person heute -> prüfen ob der stand sauber ist
                log("Noch keine Stempelungen heute auf " + usercode + ". Prüfe ob Wartung nötig...");

                //prüfen ob der zeitkonto_berechnungsstand weiter zurückliegt als gestern(abend)
                string berechnungsstand_string = "";
                comm.CommandText = "SELECT zeitkonto_berechnungsstand FROM user where userid='" + usercode + "'";
                try
                {
                    berechnungsstand_string = comm.ExecuteScalar() + "";
                }
                catch (Exception ex) { log(ex.Message); }

                DateTime datum_gestern = DateTime.Now.AddDays(-1);
                DateTime datum_letzte_zeitberechnung = DateTime.ParseExact(berechnungsstand_string, "yyyyMMdd", null);

                if (DateTime.Compare(datum_letzte_zeitberechnung, datum_gestern) < 0)
                {   //Berechnungsstand liegt weiter zurück als gestern -> auf stand von gestern bringen
                    log("Letzte Zeitberechnung war am " + datum_letzte_zeitberechnung.ToLongDateString() + ". -> Bringe auf Stand von gestern.");

                    //den letzen gestempelten tag sauber schliessen
                    comm.CommandText = "select * from stamps where userid = '" + usercode + "' AND storniert = false order by jahr DESC, monat desc, tag DESC, stunde desc, minute desc, sekunde desc, art desc limit 1";
                    Reader = comm.ExecuteReader();
                    Reader.Read();
                    string letztestempelung_art = Reader["art"] + "";
                    string letztestempelung_auftrag = Reader["task"] + "";
                    string letztestempelung_jahr = Reader["jahr"] + "";
                    string letztestempelung_monat = Reader["monat"] + "";
                    string letztestempelung_tag = Reader["tag"] + "";
                    string letztestempelung_stunde = Reader["stunde"] + "";
                    string letztestempelung_minute = Reader["minute"] + "";
                    string letztestempelung_sekunde = Reader["sekunde"] + "";
                    string letztestempelung_zeiteinheit = Reader["dezimal"] + "";
                    Reader.Close();

                    if (letztestempelung_art != "ab")
                    {   //letzte stempelung ist keine abstempelung -> nötige daten ermitteln und abstempeln
                        log("Letzter Auftrag (" + letztestempelung_auftrag + ") am " + letztestempelung_tag + "." + letztestempelung_monat + "." + letztestempelung_jahr + " wurde nicht abgestempelt...");


                        comm.CommandText = "INSERT INTO stamps (userid,task,art,jahr,monat,tag,stunde,minute,sekunde,dezimal,quelle,storniert) " +
                                           "VALUES ('" + usercode + "','" + letztestempelung_auftrag + "','ab','" + letztestempelung_jahr + "','" +
                                           letztestempelung_monat + "','" + letztestempelung_tag + "','" + letztestempelung_stunde + "','" +
                                           letztestempelung_minute + "','" + letztestempelung_sekunde + "','" + letztestempelung_zeiteinheit + "','WARTUNG',false)";
                        try
                        {
                            comm.ExecuteNonQuery();
                            log("Automatische Abstempelung wird eingetragen. SQL:" + comm.CommandText);
                        }
                        catch (MySql.Data.MySqlClient.MySqlException ex) { log(ex.Message); }

                    }

                    //tag für tag istzeit berechnen, mit sollzeit abgleichen, auf zeitkonto anrechnen bis zeitkontostand bei gestern abend ist oder ein fehler auftritt
                    while (DateTime.Compare(datum_letzte_zeitberechnung, datum_gestern) < 0  && fehlerflag == false)
                    {
                        DateTime berechnungsdatum = datum_letzte_zeitberechnung.AddDays(1).Date;
                        string berechnungsjahr = berechnungsdatum.Year.ToString("D4");
                        string berechnungsmonat = berechnungsdatum.Month.ToString("D2");
                        string berechnungstag = berechnungsdatum.Day.ToString("D2");

                        double berechneteIstZeit = ermittleIstZeit(usercode, berechnungsjahr, berechnungsmonat, berechnungstag);
                        double berechneteSollZeit = ermittleSollZeit(usercode, berechnungsjahr, berechnungsmonat, berechnungstag);
                        double berechneteUeberstunden = berechneteIstZeit - berechneteSollZeit;
                        double zeitkontostand_bisher = 0;
                        double zeitkontostand_neu = 0;

                        if(berechneteIstZeit == -1 || berechneteSollZeit == -1) //Fehler bei der Berechnung
                        {
                            fehlerflag = true;
                            log("Fehler bei der Zeitberechnung -> Stundenkonto wird nicht aktualisiert");
                            comm.CommandText = "UPDATE user SET stempelfehler='true' where userid = '" + usercode + "'";
                            try
                            {
                                comm.ExecuteNonQuery();
                                log("Fehler beim Mitarbeiter vermerkt. SQL:" + comm.CommandText);
                            }
                            catch (MySql.Data.MySqlClient.MySqlException ex) { log(ex.Message); }
                        }
                        else
                        {//berechnung von Ist- und Soll-Zeit erfolgreich -> neuen Zeitkontostand berechnen

                            //bisherigen zeitkontostand abfragen
                            comm.CommandText = "SELECT zeitkonto FROM user where userid='" + usercode + "'";
                            try
                            {
                                zeitkontostand_bisher = double.Parse(comm.ExecuteScalar() + "");
                            }
                            catch (Exception ex) { log(ex.Message); }

                            zeitkontostand_neu = zeitkontostand_bisher + berechneteUeberstunden;

                            //neuen zeitkontostand beim Mitarbeiter eintragen und berechnungsstand auf das berechnungsdatum setzen
                            comm.CommandText = "UPDATE user SET zeitkonto='" + zeitkontostand_neu +
                                                "',zeitkonto_berechnungsstand = '" + berechnungsjahr + berechnungsmonat + berechnungstag +
                                                "' where userid = '" + usercode + "'";
                            try
                            {
                                comm.ExecuteNonQuery();
                                log("Zeitkonto aktualisiert. SQL:" + comm.CommandText);
                            }
                            catch (MySql.Data.MySqlClient.MySqlException ex){ log(ex.Message); }
    
                            datum_letzte_zeitberechnung = berechnungsdatum;
                            log("Zeitberechnung ist jetzt auf dem Stand vom " + datum_letzte_zeitberechnung.ToLongDateString());
                        }
                    }
                }
            }
        }
        private double ermittleIstZeit(string usercode, string berechnungsjahr, string berechnungsmonat, string berechnungstag)
        {   
            double Istzeit_tmp = 0;
            double Pausenzeit_tmp = 0;
            string Fehler = "";

            open_db();
            comm.CommandText = "SELECT * FROM stamps WHERE userid = '" + usercode + "' AND jahr = '" + berechnungsjahr + 
                                "' AND monat = '" + berechnungsmonat + "' AND tag = '" + berechnungstag + "'  AND storniert = false " +
                                " ORDER BY jahr ASC, monat ASC, tag ASC, stunde ASC, minute ASC, sekunde ASC, art ASC";
            MySql.Data.MySqlClient.MySqlDataReader Reader = comm.ExecuteReader();

            while (Reader.Read())
            {
                //alle nötigen werte der anstempelung aus dem reader holen
                string an_stunde = Reader["stunde"] + "";
                string an_dezimal = Reader["dezimal"] + "";
                string an_task = Reader["task"] + "";
                double an_uhrzeit_dezimal = double.Parse(an_stunde + "," + an_dezimal);
                
                
                Reader.Read();
                //alle nötigen werte der abstempelung aus dem reader holen
                string ab_stunde = Reader["stunde"] + "";
                string ab_dezimal = Reader["dezimal"] + "";
                string ab_task = Reader["task"] + "";
                double ab_uhrzeit_dezimal = double.Parse(ab_stunde + "," + ab_dezimal);

                if (an_task != ab_task)
                {
                    Fehler = "Zeitpaar passt nicht zusammen (verschiedene tasks)";
                    log(Fehler);
                }

                if(an_task == "888001")
                {   //Pausenstempelung

                    Pausenzeit_tmp = Pausenzeit_tmp + ab_uhrzeit_dezimal - an_uhrzeit_dezimal;
                }

                if(an_task != "888000")
                {   //keine Leerlaufstempelung -> Zeit komplett auf Istzeit anrechnen
                    Istzeit_tmp = Istzeit_tmp + ab_uhrzeit_dezimal - an_uhrzeit_dezimal;
                }
                else
                {   //Leerlaufstempelung
                    if(an_uhrzeit_dezimal < 8)
                    {//vor 8 Uhr angestempelt
                        if(ab_uhrzeit_dezimal < 8)
                        {//Leerlaufstempelung komplett vor 8 Uhr -> nicht auf Istzeit anrechnen

                        }else
                        {//Leerlaufstempelung teilweise vor 8 Uhr -> erst ab 8 Uhr auf Istzeit anrechnen
                            Istzeit_tmp = Istzeit_tmp + ab_uhrzeit_dezimal - 8;
                        }
                    }else if(ab_uhrzeit_dezimal > 17.5)
                    {//nach 17:30 abgestempelt
                        if(an_uhrzeit_dezimal > 17.5)
                        {//Leerlaufstempelung komplett nach 17:30 -> nicht auf Istzeit anrechnen

                        }else
                        {//Leerlaufstempelung teilweise nach 17:30 -> nur bis 17:30 auf Istzeit anrechnen
                            Istzeit_tmp = Istzeit_tmp + 17.5 - an_uhrzeit_dezimal;
                        }
                    }
                }
            }
            Istzeit_tmp = Istzeit_tmp - Pausenzeit_tmp;
                
            //Pausenzeit auf mindestlänge bringen
            if(Istzeit_tmp >= 6 && Pausenzeit_tmp < 0.75)
            {
                Istzeit_tmp = Istzeit_tmp - (0.75 - Pausenzeit_tmp);
            }else if(Pausenzeit_tmp < 0.5)
            {
                Istzeit_tmp = Istzeit_tmp - (0.5 - Pausenzeit_tmp);
            }
            //TODO Mindestpausenzeit mit Stefan absprechen und ggf. nochmal anpassen.
            
            if(Fehler == "")
            {
                return Istzeit_tmp;
            }
            else
            {
                return -1;
            }
        }
        private double ermittleSollZeit(string usercode, string berechnungsjahr, string berechnungsmonat, string berechnungstag)
        {   //sollzeit ermitteln (persoenlicher kalendereintrag > allgemeiner kalendereintrag > fallback(wochenende 0, sonst 7,2)
            double sollzeit = 0;
            object tmp = null;
            string sollzeitquelle = "";
            open_db();
            comm.CommandText = "SELECT sollzeit FROM kalender where zuordnung='" + usercode + "' AND jahr = '" + berechnungsjahr + 
                                "' AND monat = '" + berechnungsmonat+ "' AND tag = '" + berechnungstag + "' AND storniert = false";
            try
            {
                tmp = comm.ExecuteScalar();
            }
            catch (Exception ex) { log(ex.Message); }
            close_db();

            if (tmp != null)
            {   //es gab einen persönlichen Kalendereintrag -> dessen Sollzeit verwenden
                sollzeit = double.Parse(tmp + "");
                sollzeitquelle = "persönlicher Kalendereintrag";
            }else
            {   //es gab keinen persönlichen Kalendereintrag -> suche allgemeinen
                open_db();
                comm.CommandText = "SELECT sollzeit FROM kalender where zuordnung='allgemein' AND jahr = '" + berechnungsjahr + 
                                "' AND monat = '" + berechnungsmonat+ "' AND tag = '" + berechnungstag + "' AND storniert = false";
                try
                {
                    tmp = comm.ExecuteScalar();
                }
                catch (Exception ex) { log(ex.Message); }
                close_db();
                if (tmp != null)
                {   //es gab einen allgemeinen Kalendereintrag -> dessen Sollzeit verwenden
                    sollzeit = double.Parse(tmp + "");
                    sollzeitquelle = "allgemeiner Kalendereintrag";
                }else
                {   //es gab weder einen persönlichen noch einen allgemeinen Kalendereintrag -> Sollzeit hängt vom Wochentag ab
                    DateTime berechnungsdatum = DateTime.ParseExact(berechnungsjahr + berechnungsmonat + berechnungstag, "yyyyMMdd", null);
                    int Wochentagscode = (int)berechnungsdatum.DayOfWeek;
                    if (Wochentagscode == 0 || Wochentagscode == 6)
                    {
                        sollzeit = 0;
                        sollzeitquelle = "normaler Wochenendstag";
                    }
                    else
                    {
                        sollzeit = 7.2;
                        sollzeitquelle = "normaler Werktag";
                    }
                }
            }
            log("Ermittelte Sollzeit für " + berechnungstag + "." + berechnungsmonat + "." + berechnungsjahr + ": " + sollzeit + " (" + sollzeitquelle + ")" );
            return sollzeit;

        }
    }
}

