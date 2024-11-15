using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EsPublicGestionaLib.SolicitarNuevasFacturasResponse
{

    // NOTA: El código generado puede requerir, como mínimo, .NET Framework 4.5 o .NET Core/Standard 2.0.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://webservice.face.gob.es")]
    public partial class solicitarNuevasFacturasResponse
    {
        
        private @return returnField;
        
        [XmlElement("return")]
        public @return @return
        {
            get
            {
                return this.returnField;
            }
            set
            {
                this.returnField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    //[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class @return
    {

        private returnSolicitarNuevasFacturas[] facturasField;

        private returnResultado resultadoField;

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("solicitarNuevasFacturas")]
        public returnSolicitarNuevasFacturas[] facturas
        {
            get
            {
                return this.facturasField;
            }
            set
            {
                this.facturasField = value;
            }
        }

        /// <remarks/>
        public returnResultado resultado
        {
            get
            {
                return this.resultadoField;
            }
            set
            {
                this.resultadoField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnSolicitarNuevasFacturas
    {

        private string oficinaContableField;

        private string organoGestorField;

        private string unidadTramitadoraField;

        private string numeroRegistroField;

        private string fechaHoraRegistroField;

        /// <remarks/>
        public string oficinaContable
        {
            get
            {
                return this.oficinaContableField;
            }
            set
            {
                this.oficinaContableField = value;
            }
        }

        /// <remarks/>
        public string organoGestor
        {
            get
            {
                return this.organoGestorField;
            }
            set
            {
                this.organoGestorField = value;
            }
        }

        /// <remarks/>
        public string unidadTramitadora
        {
            get
            {
                return this.unidadTramitadoraField;
            }
            set
            {
                this.unidadTramitadoraField = value;
            }
        }

        /// <remarks/>
        public string numeroRegistro
        {
            get
            {
                return this.numeroRegistroField;
            }
            set
            {
                this.numeroRegistroField = value;
            }
        }

        /// <remarks/>
        public string fechaHoraRegistro
        {
            get
            {
                return this.fechaHoraRegistroField;
            }
            set
            {
                this.fechaHoraRegistroField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class returnResultado
    {

        private byte codigoField;

        private string descripcionField;

        /// <remarks/>
        public byte codigo
        {
            get
            {
                return this.codigoField;
            }
            set
            {
                this.codigoField = value;
            }
        }

        /// <remarks/>
        public string descripcion
        {
            get
            {
                return this.descripcionField;
            }
            set
            {
                this.descripcionField = value;
            }
        }
    }


}
