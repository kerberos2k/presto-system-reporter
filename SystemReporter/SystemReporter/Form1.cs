using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SystemReporter
{
    public partial class Splash : Form
    {
        private Boolean _haveError;
        private Timer _timer;
        private Boolean _try;
        private String _sConfigFile;
        public Splash()
        {
            //inicio formulario
            InitializeComponent();

            this._haveError = false;

            //creo aplicacion y muestro la version
            SysReporter mySysReporter = new SysReporter();
            this.versionLabel.Text = "version " + mySysReporter.getVersion;
            MessageBox.Show("INFOREST.INI" + mySysReporter.cypherString("INFOREST.INI", true));
            MessageBox.Show("ALMACEN.INI" + mySysReporter.cypherString("ALMACEN.INI", true));
            MessageBox.Show("USUARIO.INI" + mySysReporter.cypherString("USUARIO.INI", true));

            //inicio cuenta para cerrar formulario
            this._timer = new Timer();
            this._timer.Interval = 1500; //1.5 segundos carga
            this._timer.Enabled = true;
            this._timer.Tick += new EventHandler(this.TimerMethod);

            //parametros, obtener ruta y leer configuracion si existe
            this._sConfigFile = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(Splash)).CodeBase)+"\\SysReporter.ini";
            this._sConfigFile = this._sConfigFile.Substring(6);
            IniFile config_ini = new IniFile();
            this._try = System.IO.File.Exists(this._sConfigFile);
            if (this._try == true)
            {
                config_ini.Load(this._sConfigFile);
                if (!mySysReporter.verifyConfig(config_ini))
                {
                    MessageBox.Show("Error:\n  La Configuración no es válida.\n  Contacte con el administador.\n\n" + mySysReporter.evaluedConfig());
                }
            }
            else
            {
                MessageBox.Show("Error:\n  No se puede encontrar configuración.\n  Contacte con el administrador.\n\n" + this._sConfigFile);
                this._haveError = true;
            }
            //cargo configuración
            // config_ini.GetSection("ALMACEN");
            // MessageBox.Show("LEIDO >" + config_ini.GetKeyValue("INFOREST", "SERVIDOR"));

        }

        private void TimerMethod(object sender, EventArgs e)
        {
            this._timer.Enabled = false;
            // si hay error cierra la aplicacion, sino lanzar login
            if (this._haveError)
            {
                closeForm();
            }
            else
            {
                launchApp();
            }
        }

        public void launchApp() 
        {
            //launch
            this.Close();
        }
        public void closeForm()
        {   
            this.Close();            
        }
    }
}

/*  USEFULL CODE
            IniFile ini = new IniFile();

            ini.AddSection("TEST_SECTION").AddKey("Key1").Value = "Value1";
            ini.AddSection("TEST_SECTION").AddKey("Key2").Value = "Value2";
            ini.AddSection("TEST_SECTION").AddKey("Key3").Value = "Value3";
            ini.AddSection("TEST_SECTION").AddKey("Key4").Value = "Value4";
            ini.AddSection("TEST_SECTION").AddKey("Key5").Value = "Value5";
            ini.AddSection("TEST_SECTION").AddKey("Key6").Value = "Value6";
            ini.AddSection("TEST_SECTION").AddKey("Key7").Value = "Value7";

            ini.AddSection("TEST_SECTION_2").AddKey("Key1").Value = "Value1";
            ini.AddSection("TEST_SECTION_2").AddKey("Key2").Value = "Value2";
            ini.AddSection("TEST_SECTION_2").AddKey("Key3").Value = "Value3";
            ini.AddSection("TEST_SECTION_2").AddKey("Key4").Value = "Value4";
            ini.AddSection("TEST_SECTION_2").AddKey("Key5").Value = "Value5";
            ini.AddSection("TEST_SECTION_2").AddKey("Key6").Value = "Value6";
            ini.AddSection("TEST_SECTION_2").AddKey("Key7").Value = "Value7";

            // Key Rename Test
            Trace.Write("Key Rename Key1 -> KeyTemp Test: ");
            if (ini.RenameKey("TEST_SECTION", "Key1", "KeyTemp"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Section Rename Test
            Trace.Write("Test section rename TEST_SECTION -> TEST_SECTION_3: ");
            if (ini.RenameSection("TEST_SECTION", "TEST_SECTION_3"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Check Key Rename Post Section Rename
            Trace.Write("Test TEST_SECTION_3 rename key KeyTemp -> Key1: ");
            if (ini.RenameKey("TEST_SECTION_3", "KeyTemp", "Key1"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Check Section Rename Post Section Rename
            Trace.Write("Test section rename TEST_SECTION_3 -> TEST_SECTION: ");
            if (ini.RenameSection("TEST_SECTION_3", "TEST_SECTION"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Check Key Rename Key1 -> Key2 where Key2 exists
            Trace.Write("Test TEST_SECTION key rename Key1 -> Key2 where Key2 exists: ");
            if (ini.RenameKey("TEST_SECTION", "Key2", "Key1"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Check Key Rename
            Trace.Write("Test TEST_SECTION key rename Key2 -> Key2Renamed: ");
            if (ini.RenameKey("TEST_SECTION", "Key2", "Key2Renamed"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Test rename other section
            Trace.Write("Test section rename TEST_SECTION_2 -> TEST_SECTION_1 : ");
            if (ini.RenameSection("TEST_SECTION_2", "TEST_SECTION_1"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Check remove key
            Trace.Write("Test TEST_SECTION_1 remove key Key1: ");
            if (ini.GetSection("TEST_SECTION_1").RemoveKey("Key1"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Check remove key no exist
            Trace.Write("Test TEST_SECTION_1 remove key Key1: ");
            if (ini.GetSection("TEST_SECTION_1").RemoveKey("Key1"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Check remove section
            Trace.Write("Test remove section TEST_SECTION_1: ");
            if (ini.RemoveSection("TEST_SECTION_1"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            // Check remove section no exist
            Trace.Write("Test remove section TEST_SECTION_1: ");
            if (ini.RemoveSection("TEST_SECTION_1"))
                Trace.WriteLine("Pass");
            else
                Trace.WriteLine("Fail");

            Trace.WriteLine(String.Format("Section Count {0}", ini.Sections.Count));
            foreach (IniFile.IniSection sec in ini.Sections)
            {
                Trace.WriteLine(String.Format("Section {0} Key Count {1}", sec.Name, sec.Keys.Count));
                foreach (IniFile.IniSection.IniKey key in sec.Keys)
                {
                    Trace.WriteLine(String.Format("Section {0} Key={1} Value={2}", sec.Name, key.Name, key.Value));
                }
            }

            //Save the INI
            ini.Save("C:\\temp\\test.ini");
            */