using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SystemReporter
{
    class Templates
    {
        private String _name; //Any string
        private String _value; //Any string
        private Boolean _group; //Is a Group
        private List<Templates> _detail; //group content

        public Templates()
        {
            this._name = "KEY";
            this._value = "VALUE";
            this._group = false;
            this._detail = null;
        }

        public Templates(String Clave)
        {
            this._name = Clave;
            this._value = null;
            this._group = true;
            this._detail = new List<Templates>();
        }

        public Templates(String Clave, Templates child)
        {
            this._name = Clave;
            this._value = null;
            this._group = true;
            this._detail = new List<Templates>();
            this._detail.Add(child);
        }

        public Templates(String Clave, List<Templates> childs)
        {
            this._name = Clave;
            this._value = null;
            this._group = true;
            this._detail = childs;
        }

        public Templates(String Clave, String Valor)
        {
            this._name = Clave;
            this._value = Valor;
            this._group = false;
            this._detail = null;   
        }

        public String valor
        {
            get { return this._value; }
            set { if (this._group == false) { this._value = value; } }
        }

        public String clave
        {
            get { return this._name; }
        }

        public void group() 
        {
            group(true, null, new List<Templates>());
        }

        public void group(Boolean flag)
        {
            group(flag, "VALUE", new List<Templates>());
        }

        public void group(Templates child)
        {
            group(true,null,child);
        }

        public void group(List<Templates> childs)
        {
            group(true, null, childs);
        }

        private void group(Boolean flag, String Valor, Templates child)
        {
            List<Templates> childs = new List<Templates>();
            childs.Add(child);
            group(flag, Valor, childs);
        }

        private void group(Boolean flag, String Valor, List<Templates> childs)
        {
            this._group = flag;
            if (flag)
            {
                this._value = null;
                clearChilds();
                this._detail = childs;
            }
            else
            {
                this._value = Valor;
                this._detail = null;
            }
        }

        public void addChild(Templates child){
            if (this._group)
            {
                this._detail.Add(child);
            }
        }

        public void setChilds(List<Templates> childs)
        {
            if (this._group)
            {
                this._detail = childs;
            }
        }

        private void clearChilds()
        {
            this._detail = new List<Templates>();
        }

        public Templates getChild(String clave) 
        {
            if (this._group == true)
            {
                foreach(Templates t in this._detail)
                {
                    if(t.clave == clave){
                        return t;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
    }
}
