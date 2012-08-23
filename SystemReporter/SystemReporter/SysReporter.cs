using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemReporter
{
    class SysReporter
    {
        //GENERAL
        private Object _configuration;
        private String _version;
        private Templates _plantillas;

        //CONFIGURATOR
        private IniFile _configFile;
        private Boolean _validCfg;
        private List<String> _cfgErrors; 

        //ENCRYPTOR SETTINGS
        private String _sPassPhrase;
        private String _sSaltValue;
        private String _sHashAlgorithm;
        private int _iPassIterations;
        private String _sInitVector;
        private int _iKeySize;

        public SysReporter()
        {
            this._configuration = new Object();
            this._version = "1.0";
            this._validCfg = false;

            fillTemplates();

            //datos cifrado
            this._sPassPhrase = "Pas5pr@se";        // can be any string
            this._sSaltValue = "s@1tValue";         // can be any string
            this._sHashAlgorithm = "SHA1";          // can be "MD5"
            this._iPassIterations = 2;              // can be any number
            this._sInitVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            this._iKeySize = 256;                   // can be 192 or 128
        }

        public String getVersion
        {
            get { return this._version; }
            set { this._version = value; }
        }

        public String cypherString(String sCadena)
        {
            return cypherString(sCadena, false);
        }

        public Boolean verifyConfig(IniFile configuracion)
        {
            //asigno archivo de configuracion
            this._configFile = configuracion;
            
            Boolean isCorrect = true;
            
            //leer plantilla

            //verificar segun plantilla

            //retornar respuesta
            this._validCfg = isCorrect;
            return isCorrect;
        }

        public String evaluedConfig()
        {
            String response = "";

            if (this._validCfg)
            {
                foreach (String s in this._cfgErrors)
                {
                    response = ", " + response;
                }
                response = response.Substring(2);
            }
            else
            {
                response = "Configuracion no fue verificada";
            }
            
            return response;
        }

        public String cypherString(String sCadena, Boolean enc)
        {
            String response = "";
            if (enc)
            {
                response = RSCrypter.Encrypt(sCadena, this._sPassPhrase, this._sSaltValue, this._sHashAlgorithm, this._iPassIterations, this._sInitVector, this._iKeySize);
            }
            else
            {
                response = RSCrypter.Decrypt(sCadena, this._sPassPhrase, this._sSaltValue, this._sHashAlgorithm, this._iPassIterations, this._sInitVector, this._iKeySize);
            }
            return response;
        }

        private void fillTemplates(){
            //manually
            this._plantillas = new Templates("PLANTILLA");

            Templates tpl01 = new Templates("TPL01");
            tpl01.addChild(new Templates("nombre", "BLUE"));
            tpl01.addChild(new Templates("code", "001"));
            tpl01.addChild(new Templates("cfgfile", "filename.ext"));

            this._plantillas.addChild(tpl01);
            

            //to do get Database template data
        }
    }
}
